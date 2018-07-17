using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Web.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SterlingBankLMS.Web.Models
{
    public class SbNotificationService
    {

        static string connectionString = ConfigurationManager.ConnectionStrings["SterlingBankDbContext"].ConnectionString;
        internal static SqlCommand command = null;
        internal static SqlDependency dependency = null;

        public static string GetNotification( string userId )
        {
            try
            {
                var messages = new List<NotificationHub>();
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (command = new SqlCommand(@"Select [Id],[Message],[isRead],[Replacements] from [dbo].[NotificationHub] Where [isRead]=0 and [ReceiverId]=" + userId + "", connection))
                    {
                        command.Notification = null;
                        if (dependency == null)
                        {
                            dependency = new SqlDependency(command);
                            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                        }

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var messageContent = reader["Message"].ToString();
                            var replacementTags = reader["Replacements"].ToString();

                            if (replacementTags.ToString() != "")
                            {
                                var replacementArr = replacementTags.ToString().Split(new[] { "|" }, StringSplitOptions.None);
                                Dictionary<string, string> tags = new Dictionary<string, string>();
                                if (replacementArr.Length > 0)
                                {
                                    foreach (var item in replacementArr)
                                    {
                                        var keyTags = item.Split(new char[] { ':' });
                                        tags.Add(keyTags[0], keyTags[1]);
                                    }

                                    foreach (var tagmess in tags)
                                    {
                                        if (messageContent.ToString().Contains(tagmess.Key))
                                        {
                                            messageContent = messageContent.ToString().Replace(tagmess.Key, tagmess.Value);
                                        }
                                    }
                                }
                            }

                            messages.Add(item: new NotificationHub
                            {
                                Id = (int)reader["Id"],
                                Message = messageContent,
                                IsRead = (bool)reader["isRead"]
                            });
                        }
                    }
                }
                var jsonSerializer = new JavaScriptSerializer();
                var json = jsonSerializer.Serialize(messages);
                return json;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static void dependency_OnChange( object sender, SqlNotificationEventArgs e )
        {
            if (dependency != null)
            {
                dependency.OnChange -= dependency_OnChange;
                dependency = null;
            }
            if (e.Type == SqlNotificationType.Change)
            {
                SbLMSHub.Send();
            }
        }

        public static string UpdateNotification( string userId )
        {
            try
            {
                var messages = new List<NotificationHub>();
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (command = new SqlCommand(@"Update [dbo].[NotificationHub] SET [isRead]=1  Where [isRead]=0 and [ReceiverId]=" + userId + "", connection))
                    {
                        command.Notification = null;
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var norows = command.ExecuteNonQuery();
                    }
                }

                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (command = new SqlCommand(@"Select [Id],[Message],[isRead],[Replacements] from [dbo].[NotificationHub] Where [isRead]=0", conn))
                    {
                        command.Notification = null;
                        if (dependency == null)
                        {
                            dependency = new SqlDependency(command);
                            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                        }

                        if (conn.State == ConnectionState.Closed)
                            conn.Open();

                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            messages.Add(item: new NotificationHub
                            {
                                Id = (int)reader["Id"],
                                Message = (string)reader["Message"],
                                IsRead = (bool)reader["isRead"]
                            });
                        }
                    }
                }

                var jsonSerializer = new JavaScriptSerializer();
                var json = jsonSerializer.Serialize(messages);
                return json;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}