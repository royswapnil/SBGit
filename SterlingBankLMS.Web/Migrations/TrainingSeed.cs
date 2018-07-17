using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Web.Infrastructure.DataContext;
using SterlingBankLMS.Web.Models.IdentityModels;

namespace SterlingBankLMS.Web.Migrations
{
    internal class TrainingSeed
    {
        internal static void CreateTrainings( SterlingBankLmsContext context )
        {
            var training = context.Set<Training>().ToList();
            var course = context.Set<Course>().ToList();
            var user = context.Set<ApplicationUser>().ToList();

            var requests = new List<TrainingRequest>()
            {
                new TrainingRequest
                {
                    TrainingApprovalStatus=Data.Models.Enums.TrainingApprovalStatus.Approved,
                    CreatedById=1,
                    CreatedDate=new DateTime(2017,10,03),
                    IsDeleted=false,
                    OrganizationId=1,
                    TrainingId=training[2].Id,
                    UserId=2
                },
                new TrainingRequest
                {
                    TrainingApprovalStatus=Data.Models.Enums.TrainingApprovalStatus.PendingLineManagerApproval,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,03),
                    IsDeleted=false,
                    OrganizationId=1,
                    TrainingId=training[3].Id,
                    UserId=3
                },
                new TrainingRequest
                {
                    TrainingApprovalStatus=Data.Models.Enums.TrainingApprovalStatus.RejectedByLineManager,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,02),
                    IsDeleted=false,
                    OrganizationId=1,
                    TrainingId=training[0].Id,
                    UserId=1
                },
                new TrainingRequest
                {
                    TrainingApprovalStatus=Data.Models.Enums.TrainingApprovalStatus.RejectedByLineManager,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,13),
                    IsDeleted=false,
                    OrganizationId=1,
                    TrainingId=training[3].Id,
                    UserId=1
                },
                new TrainingRequest
                {
                    TrainingApprovalStatus=Data.Models.Enums.TrainingApprovalStatus.Approved,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,28),
                    IsDeleted=false,
                    OrganizationId=1,
                    TrainingId=training[1].Id,
                    UserId=2
                },
                new TrainingRequest
                {
                    TrainingApprovalStatus=Data.Models.Enums.TrainingApprovalStatus.RejectedByAdmin,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,13),
                    IsDeleted=false,
                    OrganizationId=1,
                    TrainingId=training[4].Id,
                    UserId=4
                },
                new TrainingRequest
                {
                    TrainingApprovalStatus=Data.Models.Enums.TrainingApprovalStatus.PendingAdminApproval,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,11),
                    IsDeleted=false,
                    OrganizationId=1,
                    TrainingId=training[2].Id,
                    UserId=4
                },
               
                new TrainingRequest
                {
                    TrainingApprovalStatus=Data.Models.Enums.TrainingApprovalStatus.Approved,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,13),
                    IsDeleted=false,
                    OrganizationId=1,
                    TrainingId=training[4].Id,
                    UserId=3
                },
                new TrainingRequest
                {
                    TrainingApprovalStatus=Data.Models.Enums.TrainingApprovalStatus.PendingAdminApproval,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,17),
                    IsDeleted=false,
                    OrganizationId=1,
                    TrainingId=training[0].Id,
                    UserId=3
                },
                new TrainingRequest
                {
                    TrainingApprovalStatus=Data.Models.Enums.TrainingApprovalStatus.RejectedByLineManager,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,10),
                    IsDeleted=false,
                    OrganizationId=1,
                    TrainingId=training[2].Id,
                    UserId=4
                }

            };
            context.Set<TrainingRequest>().AddOrUpdate(x => x.UserId, requests.ToArray());
            context.SaveChanges();


            
            var ticket = new List<Ticket>()
            {
                new Ticket
                {
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,13),
                    IsDeleted=false,
                    OrganizationId=1,
                    TicketStatus=Data.Models.Enums.TicketStatus.Escalate,
                    TicketTitle="Unable to view training videos....",
                    TicketDescription="<strong>Dear Admin</strong>. <p>I have been unable to view training videos. this is have been on for as long as I remember. Please can something be done on it? Read this as you decide.</p><p>The new common language will be more simple and regular than the existing European languages. It will be as simple as Occidental; in fact, it will be Occidental. To an English person, it will seem like simplified English, as a skeptical Cambridge friend of mine told me what Occidental is.The European languages are members of the same family.</p> <p>Best regards</P>",
                    UserId=1,
                    ModifiedDate=new DateTime(2018,02,27)
                },
                new Ticket
                {
                    CreatedById=2,
                    CreatedDate=new DateTime(2018,01,13),
                    IsDeleted=false,
                    OrganizationId=1,
                    TicketStatus=Data.Models.Enums.TicketStatus.Open,
                    TicketTitle="Unable to connect to live feed",
                     TicketDescription="<strong>Dear Admin</strong>. <p>I have been unable to connect to live videos. this is have been on for as long as I remember. Please can something be done on it? Read this as you decide.</p><p>The new common language will be more simple and regular than the existing European languages. It will be as simple as Occidental; in fact, it will be Occidental. To an English person, it will seem like simplified English, as a skeptical Cambridge friend of mine told me what Occidental is.The European languages are members of the same family.</p> <p>Best regards</P>",
                    UserId=2,
                    ModifiedDate=new DateTime(2018,02,27)
                },
                new Ticket
                {
                    CreatedById=3,
                    CreatedDate=new DateTime(2018,03,03),
                    IsDeleted=false,
                    OrganizationId=1,
                    TicketStatus=Data.Models.Enums.TicketStatus.New,
                    TicketTitle="I cannot view my profile..",
                     TicketDescription="<strong>Dear Admin</strong>. <p>I have been unable to view my profile. this is have been on for as long as I remember. Please can something be done on it? Read this as you decide.</p><p>The new common language will be more simple and regular than the existing European languages. It will be as simple as Occidental; in fact, it will be Occidental. To an English person, it will seem like simplified English, as a skeptical Cambridge friend of mine told me what Occidental is.The European languages are members of the same family.</p> <p>Best regards</P>",
                    UserId=3,
                    ModifiedDate=new DateTime(2018,03,04)
                },
                new Ticket
                {
                    CreatedById=4,
                    CreatedDate=new DateTime(2018,02,13),
                    IsDeleted=false,
                    OrganizationId=1,
                    TicketStatus=Data.Models.Enums.TicketStatus.Closed,
                    TicketTitle="I cannot login to take course",
                     TicketDescription="<strong>Dear Admin</strong>. <p>I have been unable to take course on the lms. this is have been on for as long as I remember. Please can something be done on it? Read this as you decide.</p><p>The new common language will be more simple and regular than the existing European languages. It will be as simple as Occidental; in fact, it will be Occidental. To an English person, it will seem like simplified English, as a skeptical Cambridge friend of mine told me what Occidental is.The European languages are members of the same family.</p> <p>Best regards</P>",
                    UserId=4,
                    ModifiedDate=new DateTime(2018,02,28)
                },
                new Ticket
                {
                    CreatedById=2,
                    CreatedDate=new DateTime(2018,02,13),
                    IsDeleted=false,
                    OrganizationId=1,
                    TicketStatus=Data.Models.Enums.TicketStatus.Open,
                    TicketTitle="I could not view my certificates",
                     TicketDescription="<strong>Dear Admin</strong>. <p>I have been unable to view my certificates on the learning management system. this is have been on for as long as I remember. Please can something be done on it? Read this as you decide.</p><p>The new common language will be more simple and regular than the existing European languages. It will be as simple as Occidental; in fact, it will be Occidental. To an English person, it will seem like simplified English, as a skeptical Cambridge friend of mine told me what Occidental is.The European languages are members of the same family.</p> <p>Best regards</P>",
                    UserId=2,
                    ModifiedDate=new DateTime(2018,02,18)
                },
                new Ticket
                {
                    CreatedById=3,
                    CreatedDate=new DateTime(2018,02,27),
                    IsDeleted=false,
                    OrganizationId=1,
                    TicketStatus=Data.Models.Enums.TicketStatus.New,
                    TicketTitle="Unable to view training videos",
                     TicketDescription="<strong>Dear Admin</strong>. <p>I have been unable to view training videos. this is have been on for as long as I remember. Please can something be done on it? Read this as you decide.</p><p>The new common language will be more simple and regular than the existing European languages. It will be as simple as Occidental; in fact, it will be Occidental. To an English person, it will seem like simplified English, as a skeptical Cambridge friend of mine told me what Occidental is.The European languages are members of the same family.</p> <p>Best regards</P>",
                    UserId=3,
                    ModifiedDate=new DateTime(2018,02,27)
                },
                new Ticket
                {
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,17),
                    IsDeleted=false,
                    OrganizationId=1,
                    TicketStatus=Data.Models.Enums.TicketStatus.Closed,
                    TicketTitle="Cannot request for training",
                     TicketDescription="<strong>Dear Admin</strong>. <p>I have been unable to request for training. this is have been on for as long as I remember. Please can something be done on it? Read this as you decide.</p><p>The new common language will be more simple and regular than the existing European languages. It will be as simple as Occidental; in fact, it will be Occidental. To an English person, it will seem like simplified English, as a skeptical Cambridge friend of mine told me what Occidental is.The European languages are members of the same family.</p> <p>Best regards</P>",
                    UserId=1,
                    ModifiedDate=new DateTime(2018,02,24)
                },
                new Ticket
                {
                    CreatedById=2,
                    CreatedDate=new DateTime(2018,02,10),
                    IsDeleted=false,
                    OrganizationId=1,
                    TicketStatus=Data.Models.Enums.TicketStatus.Open,
                    TicketTitle="Unable to view training videos",
                     TicketDescription="<strong>Dear Admin</strong>. <p>I have been unable to view training videos. this is have been on for as long as I remember. Please can something be done on it? Read this as you decide.</p><p>The new common language will be more simple and regular than the existing European languages. It will be as simple as Occidental; in fact, it will be Occidental. To an English person, it will seem like simplified English, as a skeptical Cambridge friend of mine told me what Occidental is.The European languages are members of the same family.</p> <p>Best regards</P>",
                    UserId=2,
                    ModifiedDate=new DateTime(2018,02,20)
                },
                new Ticket
                {
                    CreatedById=3,
                    CreatedDate=new DateTime(2018,02,28),
                    IsDeleted=false,
                    OrganizationId=1,
                    TicketStatus=Data.Models.Enums.TicketStatus.New,
                    TicketTitle="Cannot search for courses",
                     TicketDescription="<strong>Dear Admin</strong>. <p>I have been unable to view search for courses. this is have been on for as long as I remember. Please can something be done on it? Read this as you decide.</p><p>The new common language will be more simple and regular than the existing European languages. It will be as simple as Occidental; in fact, it will be Occidental. To an English person, it will seem like simplified English, as a skeptical Cambridge friend of mine told me what Occidental is.The European languages are members of the same family.</p> <p>Best regards</P>",
                    UserId=3,
                    ModifiedDate=new DateTime(2018,02,28)
                },
                new Ticket
                {
                    CreatedById=3,
                    CreatedDate=new DateTime(2018,02,13),
                    IsDeleted=false,
                    OrganizationId=1,
                    TicketStatus=Data.Models.Enums.TicketStatus.Open,
                    TicketTitle="Application is saying server error.",
                     TicketDescription="<strong>Dear Admin</strong>. <p>whenever i log in to the learning management system. I am greeted with the message Application server error. this is have been on for as long as I remember. Please can something be done on it? Read this as you decide.</p><p>The new common language will be more simple and regular than the existing European languages. It will be as simple as Occidental; in fact, it will be Occidental. To an English person, it will seem like simplified English, as a skeptical Cambridge friend of mine told me what Occidental is.The European languages are members of the same family.</p> <p>Best regards</P>",
                    UserId=3,
                    ModifiedDate=new DateTime(2018,02,14)
                },
                new Ticket
                {
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,13),
                    IsDeleted=false,
                    OrganizationId=1,
                    TicketStatus=Data.Models.Enums.TicketStatus.Escalate,
                    TicketTitle="Unable to view training videos..",
                     TicketDescription="<strong>Dear Admin</strong>. <p>I have been unable to view training videos. this is have been on for as long as I remember. Please can something be done on it? Read this as you decide.</p><p>The new common language will be more simple and regular than the existing European languages. It will be as simple as Occidental; in fact, it will be Occidental. To an English person, it will seem like simplified English, as a skeptical Cambridge friend of mine told me what Occidental is.The European languages are members of the same family.</p> <p>Best regards</P>",
                    UserId=1,
                    ModifiedDate=new DateTime(2018,02,19)
                },
                new Ticket
                {
                    CreatedById=2,
                    CreatedDate=new DateTime(2018,02,13),
                    IsDeleted=false,
                    OrganizationId=1,
                    TicketStatus=Data.Models.Enums.TicketStatus.Escalate,
                    TicketTitle="Unable to view training video...",
                     TicketDescription="<strong>Dear Admin</strong>. <p>I have been unable to view training videos. this is have been on for as long as I remember. Please can something be done on it? Read this as you decide.</p><p>The new common language will be more simple and regular than the existing European languages. It will be as simple as Occidental; in fact, it will be Occidental. To an English person, it will seem like simplified English, as a skeptical Cambridge friend of mine told me what Occidental is.The European languages are members of the same family.</p> <p>Best regards</P>",
                    UserId=2,
                    ModifiedDate=new DateTime(2018,02,27)
                }
            };
            context.Set<Ticket>().AddOrUpdate(x => x.TicketDescription, ticket.ToArray());
            context.SaveChanges();
            

            var certificates = new List<UserCertificate>()
            {
                new UserCertificate
                {
                    UserId=user[3].Id,
                    CreatedById=user[0].Id,
                    CreatedDate=DateTime.Now,
                    DateObtained=new DateTime(2011,03,23),
                    Description="Obtained after successfull completion of a Microsoft Executive Training",
                    Name="MVP Certificate",
                    OrganizationId=1,
                    TemplateUrl="/Content/img/cert.jpg",
                    IsDeleted=false,
                    CertificateApprovalStatus=Data.Models.Enums.CertificateApprovalStatus.Approved
                },
                new UserCertificate
                {
                    UserId=user[1].Id,
                    CreatedById=user[0].Id,
                    CreatedDate=DateTime.Now,
                    DateObtained=new DateTime(2014,02,10),
                    ExpiryDate=new DateTime(2020,02,02),
                    Description="Obtained after successfull completion of my Bsc",
                    Name="Bsc Certificate 2014",
                    OrganizationId=1,
                    TemplateUrl="/Content/img/cert.jpg",
                    IsDeleted=false,
                    CertificateApprovalStatus=Data.Models.Enums.CertificateApprovalStatus.Approved
                },
                new UserCertificate
                {
                    UserId=user[2].Id,
                    CreatedById=user[0].Id,
                    CreatedDate=DateTime.Now,
                    DateObtained=new DateTime(2017,07,23),
                    ExpiryDate=new DateTime(2025,02,04),
                    Description="Obtained after successfull completion of this course",
                    Name="Certificate 1",
                    OrganizationId=1, 
                    TemplateUrl="/Content/img/cert.jpg",
                    IsDeleted=false,
                    CertificateApprovalStatus=Data.Models.Enums.CertificateApprovalStatus.Approved
                },
                new UserCertificate
                {

                    UserId=user[3].Id,
                    CreatedById=user[0].Id,
                    CreatedDate=DateTime.Now,
                    DateObtained=new DateTime(2011,03,23),
                    ExpiryDate=new DateTime(2021,01,01),
                    Description="Obtained after successfull completion of my NYSC",
                    Name="NYSC Certificate 2011",
                    OrganizationId=1,
                    TemplateUrl="/Content/img/cert.jpg",
                    IsDeleted=false,
                    CertificateApprovalStatus=Data.Models.Enums.CertificateApprovalStatus.Pending
                },
                new UserCertificate
                {
                    UserId=user[2].Id,
                    CreatedById=user[0].Id,
                    CreatedDate=DateTime.Now,
                    DateObtained=new DateTime(2011,03,23),
                    ExpiryDate=new DateTime(2019,01,09),
                    Description="Obtained after successfull completion of MBA",
                    Name="MBA Certificate",
                    OrganizationId=1,
                    TemplateUrl="/Content/img/cert.jpg",
                    IsDeleted=false,
                    CertificateApprovalStatus=Data.Models.Enums.CertificateApprovalStatus.Pending
                },
                new UserCertificate
                {
                    UserId=user[3].Id,
                    CreatedById=user[0].Id,
                    CreatedDate=DateTime.Now,
                    DateObtained=new DateTime(2018,03,23),
                    ExpiryDate=new DateTime(2020,05,05),
                    Description="Obtained after successfull completion of this course",
                    Name="Basic Banking Certificate",
                    OrganizationId=1,
                    TemplateUrl="/Content/img/cert.jpg",
                    IsDeleted=false,
                    CertificateApprovalStatus=Data.Models.Enums.CertificateApprovalStatus.Approved
                },
                new UserCertificate
                {
                    UserId=user[1].Id,
                    CreatedById=user[0].Id,
                    CreatedDate=DateTime.Now,
                    DateObtained=new DateTime(2017,03,23),
                    ExpiryDate=new DateTime(2022,01,03),
                    Description="Obtained after successfull completion of CCNA",
                    Name="CCNA Certificate 2017",
                    OrganizationId=1,
                    TemplateUrl="/Content/img/cert.jpg",
                    IsDeleted=false,
                    CertificateApprovalStatus=Data.Models.Enums.CertificateApprovalStatus.Pending
                },
                new UserCertificate
                {
                    UserId=user[0].Id,
                    CreatedById=user[0].Id,
                    CreatedDate=DateTime.Now,
                    DateObtained=new DateTime(2010,03,23),
                    ExpiryDate=new DateTime(2023,01,05),
                    Description="Obtained after successfull completion of my ICAN",
                    Name="ICAN Certificate 2010",
                    OrganizationId=1,
                    TemplateUrl="/Content/img/cert.jpg",
                    IsDeleted=false,
                    CertificateApprovalStatus=Data.Models.Enums.CertificateApprovalStatus.Pending
                },
                new UserCertificate
                {
                    UserId=user[3].Id,
                    CreatedById=user[0].Id,
                    CreatedDate=DateTime.Now,
                    DateObtained=new DateTime(2013,03,23),
                    ExpiryDate=new DateTime(2024,02,01),
                    Description="Obtained after successfull completion of my ICAN",
                    Name="ICAN Certificate 2013",
                    OrganizationId=1,
                    TemplateUrl="/Content/img/cert.jpg",
                    IsDeleted=false,
                    CertificateApprovalStatus=Data.Models.Enums.CertificateApprovalStatus.Pending
                },
                new UserCertificate
                {
                    UserId=user[2].Id,
                    CreatedById=user[0].Id,
                    CreatedDate=DateTime.Now,
                    DateObtained=new DateTime(2011,03,23),
                    ExpiryDate=new DateTime(2030,10,01),
                    Description="Obtained after successfull completion of BSc",
                    Name="BSc Certificate 2011",
                    OrganizationId=1,
                    TemplateUrl="/Content/img/cert.jpg",
                    IsDeleted=false,
                    CertificateApprovalStatus=Data.Models.Enums.CertificateApprovalStatus.Approved
                },
                new UserCertificate
                {
                    UserId=user[1].Id,
                    CreatedById=user[0].Id,
                    CreatedDate=DateTime.Now,
                    DateObtained=new DateTime(2011,03,23),
                    ExpiryDate=new DateTime(2024,02,04),
                    Description="Obtained after successfull completion of my CFA",
                    Name="CFA Certificate 2011",
                    OrganizationId=1,
                    TemplateUrl="/Content/img/cert.jpg",
                    IsDeleted=false,
                    CertificateApprovalStatus=Data.Models.Enums.CertificateApprovalStatus.Pending
                }
            };
            context.Set<UserCertificate>().AddOrUpdate(x => x.Name, certificates.ToArray());
            context.SaveChanges();
        }
    }
}