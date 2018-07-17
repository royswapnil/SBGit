using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SterlingBankLMS.Web.Utilities
{
    /// <summary>
    ///  Menu current active item helper class.
    /// </summary>
    public static class MvcUtilities
    {
        /// <summary>
        /// Determines whether the specified controller is selected.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="controller">The controller.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        /// 
        public static MvcHtmlString LiActionLink(this HtmlHelper html, string text, string icon, string area, string controller, string action, string anchorTemplate)
        {
            var context = html.ViewContext;
            if (context.Controller.ControllerContext.IsChildAction)
                context = html.ViewContext.ParentActionViewContext;
            var routeValues = context.RouteData.Values;
            var currentAction = routeValues["action"].ToString();
            var currentController = routeValues["controller"].ToString();
            var currentarea = (string) html.ViewContext.RouteData.DataTokens["area"];

            //var str = string.Format(anchorTemplate,
            //    currentController.Equals(controller, StringComparison.OrdinalIgnoreCase) &&
            //    currentarea.Equals(area, StringComparison.OrdinalIgnoreCase) &&
            //    currentAction.Equals(action, StringComparison.OrdinalIgnoreCase)
            //        ? " class=\"link-active\""
            //        : String.Empty, icon, html.ActionLink(text,action, controller, new { Area = currentarea }, new { }).ToHtmlString()
            //    );

            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);

            var str = string.Format(anchorTemplate,
               currentController.Equals(controller, StringComparison.OrdinalIgnoreCase) &&
               currentarea.Equals(area, StringComparison.OrdinalIgnoreCase) &&
               currentAction.Equals(action, StringComparison.OrdinalIgnoreCase)
                   ? " class=\"link-active\"" : String.Empty,
                urlHelper.Action(action, controller, new { Area = currentarea }), icon, text
               );
            return new MvcHtmlString(str);
        }

        public static List<SelectListItem> GenerateEnumSelectList(Type enumType, object selectedItem)
        {
            return (from object item in Enum.GetValues(enumType)

                    let fi = enumType.GetField(item.ToString())
                    let attribute = fi.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault()
                    let include = fi.GetCustomAttributes(typeof(BrowsableAttribute), true).FirstOrDefault()
                    let title = attribute == null ? item.ToString() : ((DescriptionAttribute) attribute).Description
                    where include == null ? true : ((BrowsableAttribute) include).Browsable ? true : false

                    select new SelectListItem
                    {
                        Value = ((int) (item)).ToString(),
                        Text = title,
                        Selected = selectedItem == item,
                    }).ToList();

        }

    }
}