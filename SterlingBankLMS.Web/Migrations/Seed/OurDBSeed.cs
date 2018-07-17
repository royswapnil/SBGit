using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Web.Infrastructure.Auth;
using SterlingBankLMS.Web.Infrastructure.DataContext;
using SterlingBankLMS.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SterlingBankLMS.Web.Migrations.Seed
{
    public class OurDBSeed
    {
        // This method will be called after migrating to the latest version.

        //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //  to avoid creating duplicate seed data.

        public static void CreateDB(DbContext context, int orgId)
        {
            var user = context.Set<User>().FirstOrDefault();

            var organization = new Organization
            {
                Name = "Sterling Bank PLC",
                Email = "info@sterlingbank.com",
                IsLockedOut = false,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedById = user.Id,
            };

            context.Set<Organization>().AddOrUpdate(p => p.Name, organization);
            context.SaveChanges();

            var grades = new List<Grade>()
            {
                new Grade
                {
                    CreatedById=user.Id,
                    CreatedDate=DateTime.Now,
                    Name="Executive Trainee",
                    SortOrder=1,
                    IsDeleted=false,
                    OrganizationId = orgId,
                },
                new Grade
                {
                    CreatedById=user.Id,
                    CreatedDate=DateTime.Now,
                    Name="Senior Executive",
                    SortOrder=2,
                    IsDeleted=false,
                    OrganizationId=orgId,
                },
                new Grade
                {
                    CreatedById=user.Id,
                    CreatedDate=DateTime.Now,
                    Name="Assistant Banking Officer",
                    SortOrder=3,
                    IsDeleted=false,
                    OrganizationId=orgId,
                },
                new Grade
                {
                    CreatedById=user.Id,
                    CreatedDate=DateTime.Now,
                    Name="Banking Officer",
                    SortOrder=4,
                    IsDeleted=false,
                    OrganizationId=orgId,
                },
                new Grade
                {
                    CreatedById=user.Id,
                    CreatedDate=DateTime.Now,
                    Name="Senior Banking Officer",
                    SortOrder=5,
                    IsDeleted=false,
                    OrganizationId=orgId,
                },
                new Grade
                {
                    CreatedById=user.Id,
                    CreatedDate=DateTime.Now,
                    Name="Assistant Manager",
                    SortOrder=6,
                    IsDeleted=false,
                    OrganizationId = orgId,
                },
                new Grade
                {
                    CreatedById=user.Id,
                    CreatedDate=DateTime.Now,
                    Name="Deputy Manager",
                    SortOrder=7,
                    IsDeleted=false,
                    OrganizationId = orgId,
                },
                new Grade
                {
                    CreatedById=user.Id,
                    CreatedDate=DateTime.Now,
                    Name="Manager",
                    SortOrder=8,
                    IsDeleted=false,
                    OrganizationId = orgId,
                },
                new Grade
                {
                    CreatedById=user.Id,
                    CreatedDate=DateTime.Now,
                    Name="Senior Manager",
                    SortOrder=9,
                    IsDeleted=false,
                    OrganizationId = orgId,
                },
                new Grade
                {
                    CreatedById=user.Id,
                    CreatedDate=DateTime.Now,
                    Name="Assistant General Manager",
                    SortOrder=10,
                    IsDeleted=false,
                    OrganizationId = orgId,
                },
                new Grade
                {
                    CreatedById=user.Id,
                    CreatedDate=DateTime.Now,
                    Name="Deputy General Manager",
                    SortOrder=11,
                    IsDeleted=false,
                    OrganizationId = orgId,
                },
                new Grade
                {
                    CreatedById=user.Id,
                    CreatedDate=DateTime.Now,
                    Name="General Manager",
                    SortOrder=12,
                    IsDeleted=false,
                    OrganizationId = orgId,
                },
                new Grade
                {
                    CreatedById=user.Id,
                    CreatedDate=DateTime.Now,
                    Name="Executive Director",
                    SortOrder=13,
                    IsDeleted=false,
                    OrganizationId = orgId,
                },
                new Grade
                {
                    CreatedById=user.Id,
                    CreatedDate=DateTime.Now,
                    Name="Deputy Managing Director",
                    SortOrder=14,
                    IsDeleted=false,
                    OrganizationId = orgId,
                },
                new Grade
                {
                    CreatedById=user.Id,
                    CreatedDate=DateTime.Now,
                    Name="Managing Director",
                    SortOrder=15,
                    IsDeleted=false,
                    OrganizationId = orgId,
                },
            };
            context.Set<Grade>().AddOrUpdate(p => p.Name, grades.ToArray());
            context.SaveChanges();


            var branches = new List<Branch>
            {
                new Branch
                {
                    CreatedDate = DateTime.Now,
                    Name = "Head Office Annex- Ilupeju",
                      OrganizationId = orgId,
                    IsDeleted = false,
                    CreatedById = 1
                },
                new Branch
                {
                    CreatedDate = DateTime.Now,
                    Name = "Head Office ",
                      OrganizationId = orgId,
                    IsDeleted = false,
                    CreatedById = 1
                },new Branch
                {
                    CreatedDate = DateTime.Now,
                    Name = "Sterling Boulevard, Abuja",
                      OrganizationId = orgId,
                    IsDeleted = false,
                    CreatedById = 1
                },new Branch
                {
                    CreatedDate = DateTime.Now,
                    Name = "Adeola Odeku",
                      OrganizationId = orgId,
                    IsDeleted = false,
                    CreatedById = 1
                },
        };
            context.Set<Branch>().AddOrUpdate(p => p.Name, branches.ToArray());
            context.SaveChanges();

            var groups = new List<Group>()
            {
                new Group
                {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.Now,
                      OrganizationId = orgId,
                    IsDeleted = false,
                    Name = "Enterprise Risk  Management"
                },
                new Group
                {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.Now,
                      OrganizationId = orgId,
                    IsDeleted = false,
                    Name = "Internal Audit"
                },
                new Group
                {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.Now,
                      OrganizationId = orgId,
                    IsDeleted = false,
                    Name = "Office of the Managing Director"
                },
                new Group
                {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.Now,
                      OrganizationId = orgId,
                    IsDeleted = false,
                    Name = "Retail and Consumer Banking"
                },
                new Group
                {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.Now,
                      OrganizationId = orgId,
                    IsDeleted = false,
                    Name = "Agric Finance"
                },
            };
            context.Set<Group>().AddOrUpdate(x => x.Name, groups.ToArray());
            context.SaveChanges();

            var depts = new List<Department>()
                {
                    new Department
                    {
                        CreatedById = user.Id,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        GroupId = groups[0].Id,
                        OrganizationId = orgId,
                        Name = "Instutional Banking"
                    },
                    new Department
                    {
                        CreatedById = user.Id,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        GroupId = groups[1].Id,
                          OrganizationId = orgId,
                        Name = "Legal Services"
                    },
                    new Department
                    {
                        CreatedById = user.Id,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        GroupId = groups[1].Id,
                          OrganizationId = orgId,
                        Name = "Internal Audit"
                    },
                    new Department
                    {
                        CreatedById = user.Id,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        GroupId = groups[2].Id,
                          OrganizationId = orgId,
                        Name = "Corporate Banking"
                    },
            };
            context.Set<Department>().AddOrUpdate(p => p.Name, depts.ToArray());
            context.SaveChanges();

            var group = context.Set<Group>().ToList();

            var region = new List<Region>()
            {
                new Region
                {
                    CreatedById = user.Id,
                    IsDeleted = false,
                      OrganizationId = orgId,
                    Name = "Yaba and Beyond",
                    CreatedDate = DateTime.Now
                },
                new Region
                {
                    CreatedById = user.Id,
                    IsDeleted = false,
                      OrganizationId = orgId,
                    Name = "Head Office",
                    CreatedDate = DateTime.Now
                },
                new Region
                {
                    CreatedById = user.Id,
                    IsDeleted = false,
                      OrganizationId = orgId,
                    Name = "Ikeja and Beyond",
                    CreatedDate = DateTime.Now
                },
                new Region
                {
                    CreatedById = user.Id,
                    IsDeleted = false,
                      OrganizationId = orgId,
                    Name = "South West",
                    CreatedDate = DateTime.Now
                },
                new Region
                {
                    CreatedById = user.Id,
                    IsDeleted = false,
                      OrganizationId = orgId,
                    Name = "Abuja",
                    CreatedDate = DateTime.Now
                },
                new Region
                {
                    CreatedById = user.Id,
                    IsDeleted = false,
                      OrganizationId = orgId,
                    Name = "South South",
                    CreatedDate = DateTime.Now
                },
                new Region
                {
                    CreatedById = user.Id,
                    IsDeleted = false,
                      OrganizationId = orgId,
                    Name = "Lagos- Victoria Island",
                    CreatedDate = DateTime.Now
                },
            };
            context.Set<Region>().AddOrUpdate(p => p.Name, region.ToArray());
            context.SaveChanges();


            var region1 = context.Set<Region>().ToList();
            var budgets = new List<DepartmentBudget>()
            {
                new DepartmentBudget
                {
                    AmountSpent=7800000m,
                    Budget=11000000m,
                    IsActive=true,
                    CreatedById=user.Id,
                    GroupId=group[1].Id,
                    RegionId=region1[2].Id,
                    CreatedDate=DateTime.Now,
                    IsDeleted=false,
                    OrganizationId = orgId,
                    Year=DateTime.Now.Year
                },
                new DepartmentBudget
                {
                    AmountSpent=4470000m,
                    Budget=14000000m,
                    IsActive=true,
                    CreatedById=user.Id,
                    GroupId=group[0].Id,
                    RegionId=region1[1].Id,
                    CreatedDate=DateTime.Now,
                    IsDeleted=false,
                    OrganizationId = orgId,
                    Year=2017
                },
                new DepartmentBudget
                {
                    AmountSpent=8810000m,
                    Budget=11079000m,
                    IsActive=true,
                    CreatedById=user.Id,
                    GroupId=group[4].Id,
                    RegionId=region1[2].Id,
                    CreatedDate=DateTime.Now,
                    IsDeleted=false,
                    OrganizationId = orgId,
                    Year=2017
                },
                new DepartmentBudget
                {
                    AmountSpent=3900000m,
                    Budget=10500000m,
                    IsActive=true,
                    CreatedById=user.Id,
                    GroupId=group[3].Id,
                    RegionId=region1[5].Id,
                    CreatedDate=DateTime.Now,
                    IsDeleted=false,
                    OrganizationId = orgId,
                    Year=DateTime.Now.Year
                },
                new DepartmentBudget
                {
                    AmountSpent=7809990m,
                    Budget=11220000m,
                    IsActive=true,
                    CreatedById=user.Id,
                    GroupId=group[2].Id,
                    RegionId=region1[6].Id,
                    CreatedDate=DateTime.Now,
                    IsDeleted=false,
                    OrganizationId = orgId,
                    Year=2017
                },
                new DepartmentBudget
                {
                    AmountSpent=1100000m,
                    Budget=21000000m,
                    IsActive=true,
                    CreatedById=user.Id,
                    GroupId=group[1].Id,
                    RegionId=region1[0].Id,
                    CreatedDate=DateTime.Now,
                    IsDeleted=false,
                    OrganizationId = orgId,
                    Year=DateTime.Now.Year
                },

            };

            context.Set<DepartmentBudget>().AddOrUpdate(p => p.Budget, budgets.ToArray());
            context.SaveChanges();





            var trainingVideo = new List<TrainingVideo>()
            {
                new TrainingVideo
                {
                    CreatedDate=DateTime.Now,
                    ImageUrl="/Content/img/img9.jpg",
                    IsDeleted=false,
                    OrganizationId = orgId,
                    TrainingVideoName="Basic Banking Training",
                    TrainingVideoUrl="/Content/testvideos/001 Course Introduction.mp4",
                    CreatedById=user.Id
                },
                 new TrainingVideo
                {
                    CreatedDate=DateTime.Now,
                    ImageUrl="/Content/img/img8.jpg",
                    IsDeleted=false,
                    OrganizationId = orgId,
                    TrainingVideoName="Communications",
                    TrainingVideoUrl="/Content/testvideos/002 What is Angular_.mp4",
                    CreatedById=user.Id

                },
                  new TrainingVideo
                {
                    CreatedDate=DateTime.Now,
                    ImageUrl="/Content/img/img7.jpg",
                    IsDeleted=false,
                    OrganizationId = orgId,
                    TrainingVideoName="Fiscal Integrity",
                    TrainingVideoUrl="/Content/testvideos/003 Angular vs Angular 2 vs Angular 4.mp4",
                    CreatedById=user.Id

                },
                   new TrainingVideo
                {
                    CreatedDate=DateTime.Now,
                    ImageUrl="/Content/img/img6.jpg",
                    IsDeleted=false,
                    OrganizationId = orgId,
                    TrainingVideoName="Sales Training",
                    TrainingVideoUrl="/Content/testvideos/009 What is TypeScript_.mp4",
                    CreatedById=user.Id

                }
            };

            context.Set<TrainingVideo>().AddOrUpdate(x => x.TrainingVideoName, trainingVideo.ToArray());
            context.SaveChanges();


            var learningArea = new List<LearningArea>()
            {
                 new LearningArea
                {
                    Name="Information Technology",
                    OrganizationId = orgId,

                    CreatedDate=DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedById = user.Id,
                },
                 new LearningArea
                {
                    Name="Human Resources",
                    OrganizationId = orgId,
                    CreatedDate=DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedById = user.Id,
                }
            };
            context.Set<LearningArea>().AddOrUpdate(x => x.Name, learningArea.ToArray());
            context.SaveChanges();

            var courses = new List<Course>()
            {
                new Course
                {
                    Name="Learning How to Learn",
                    ShortDescription="A television station employee was shot dead on Friday in the northwestern city of Peshawar as violent crowds filled the streets of several cities on a day of government-sanctioned protests against an anti-Islam film made in the United States.",
                    Description="This course gives you easy access to the invaluable learning techniques used by experts in art, music, literature, math, science, sports, and many other disciplines. We’ll learn about the how the brain uses two very different learning modes and how it encapsulates (“chunks”) information. We’ll also cover illusions of learning, memory techniques, dealingwith procrastination, and best practices shown by research to be most effective in helping you master tough subjects.",
                    HasCertificate = true,
                    LearningAreaId = learningArea[0].Id,
                    OrganizationId = orgId,
                    CreatedDate=DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedById= user.Id,
                    DueDate=new DateTime(2018,06,03),
                    ImageUrl="/Content/img/img10.jpg",
                    IsPublished=true

                },
                new Course
                {
                    Name="Foundations of Everyday Leadership",
                    ShortDescription="A television station employee was shot dead on Friday in the northwestern city of Peshawar as violent crowds filled the streets of several cities on a day of government-sanctioned protests against an anti-Islam film made in the United States.",
                    Description="In this course you will learn about the “head and heart” of everyday leadership, individual decision making, group decision making, and managing motivation. The objectives are to understand why and how leadership skills are so critical to organizational success, and learn the foundations of effective leadership skills.",

                    LearningAreaId = learningArea[0].Id,
                    OrganizationId = orgId,
                    CreatedDate=DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedById = user.Id,
                    DueDate=new DateTime(2018,06,03),
                    ImageUrl="/Content/img/img6.jpg",
                    IsPublished=true

                },
                new Course
                {
                    Name="Applications of Everyday Leadership",
                    ShortDescription="A television station employee was shot dead on Friday in the northwestern city of Peshawar as violent crowds filled the streets of several cities on a day of government-sanctioned protests against an anti-Islam film made in the United States.",
                    Description="This course covers the following topics: negotiation, feedback and coaching, conflict management, and leading change. The objectives are to learn how to use leadership skills to work more effectively with others, how to use leadership skills to organize others to work more effectively together, and to apply the foundations of effective leadership skills to everyday situations faced by leaders.",

                    ImageUrl="/Content/img/img2.jpg",
                    LearningAreaId = learningArea[0].Id,
                    OrganizationId = orgId,
                    CreatedDate=DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedById = user.Id,
                    DueDate=new DateTime(2018,06,03),
                    IsPublished=true

                },
                new Course
                {
                    Name="Designing the Organization: From Strategy to Organizational Structure",
                    ShortDescription="A television station employee was shot dead on Friday in the northwestern city of Peshawar as violent crowds filled the streets of several cities on a day of government-sanctioned protests against an anti-Islam film made in the United States.",
                    Description="In this course you will understand how firms are organized, what factors must be taken into account in making critical design decisions, and what role managers play in making these choices. In order to answer these questions, we will first develop a conceptual process model that links business models, external and internal contingencies, and organizational design. Second, we will focus on the fundamental principles of organization design and what alternative design choices are available for managers. Finally, we will apply these concepts and ideas to organizational situations to develop the critical insights and decision making skills to build effective organizations",

                    ImageUrl="/Content/img/img5.jpg",
                    LearningAreaId = learningArea[1].Id,
                    OrganizationId = orgId,
                    CreatedDate=DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedById = user.Id,
                    DueDate=new DateTime(2018,06,03),
                    IsPublished=true
                },

                new Course
                {
                    Name="Managing the Organization: From Organizational Design to Execution",
                    ShortDescription="A television station employee was shot dead on Friday in the northwestern city of Peshawar as violent crowds filled the streets of several cities on a day of government-sanctioned protests against an anti-Islam film made in the United States.",
                    Description="In this course you will build a practical framework to understand the critical linkages  between organization design and the creation of economic value through execution. We will focus on four critical linkages",

                    ImageUrl="/Content/img/img7.jpg",
                    LearningAreaId = learningArea[1].Id,
                    OrganizationId = orgId,
                    CreatedDate=DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedById = user.Id,
                    DueDate=new DateTime(2018,06,03),
                    IsPublished=true

                },
                new Course
                {
                    Name="Business Strategy",
                    ShortDescription="A television station employee was shot dead on Friday in the northwestern city of Peshawar as violent crowds filled the streets of several cities on a day of government-sanctioned protests against an anti-Islam film made in the United States.",
                    Description="In this course you will learn how organizations create, capture, and maintain value, and how it is fundamental for sustainable competitive advantage. You will be able to better understand value creation and capture, and learn the tools to analyze both competition and cooperation from a variety of perspectives, including the industry level (e.g., five forces analysis), and the firm level (e.g., business models and strategic positioning)",

                    ImageUrl="/Content/img/img9.jpg",
                    LearningAreaId = learningArea[1].Id,
                    OrganizationId = orgId,
                    CreatedDate=DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedById = user.Id,
                    DueDate=new DateTime(2018,06,03),
                    IsPublished=true

                },

                new Course
                {
                    Name="Corporate Strategy",
                    ShortDescription="A television station employee was shot dead on Friday in the northwestern city of Peshawar as violent crowds filled the streets of several cities on a day of government-sanctioned protests against an anti-Islam film made in the United States.",
                    Description="In this course you will learn how organizations create, capture, and maintain value, and how it is fundamental for sustainable competitive advantage. You will be able to better understand economic value creation and value appropriation, and learn the tools to analyze both competition and cooperation from a corporate level perspective, (e.g., through vertical integration, diversification, and geographic scope decisions).",

                    CreatedDate=DateTime.Now,
                    ImageUrl="/Content/img/img8.jpg",
                    LearningAreaId = learningArea[1].Id,
                    OrganizationId = orgId,
                    ModifiedDate = DateTime.Now,
                    CreatedById = user.Id,
                    DueDate=new DateTime(2018,06,03),
                    IsPublished=true
                }

            };

            context.Set<Course>().AddOrUpdate(x => x.Name, courses.ToArray());
            context.SaveChanges();





            var modules = new List<Module>()
            {
                new Module
                {
                    CourseId=courses[0].Id,
                    CreatedDate=DateTime.Now,
                    SortOrder=1,
                    Name="What is Learning?",
                    ModifiedDate=DateTime.Now,
                    //OrganizationId=orgId,
                    CreatedById = user.Id,
                },
                new Module
                {
                    CourseId=courses[0].Id,
                    CreatedDate=DateTime.Now,
                    SortOrder=2,
                    Name="Chunking",

                    ModifiedDate=DateTime.Now,
                    //OrganizationId=orgId,
                    CreatedById = user.Id,

                },
                new Module
                {
                    CourseId=courses[1].Id,
                    CreatedDate=DateTime.Now,
                    SortOrder=3,
                    Name="Proscatination and Memory",

                    ModifiedDate=DateTime.Now,
                    //OrganizationId=orgId,
                    CreatedById = user.Id,

                },
                new Module
                {
                    CourseId=courses[1].Id,
                    CreatedDate=DateTime.Now,
                    SortOrder=4,
                    Name="Renaissance Learning and unlocking your potential",

                    ModifiedDate=DateTime.Now,
                    //OrganizationId=orgId,
                    CreatedById = user.Id,
                },
            };
            context.Set<Module>().AddOrUpdate(x => x.Name, modules.ToArray());
            context.SaveChanges();

            //var lessons = new List<Lesson> {
            //            new Lesson
            //        {
            //            ModuleId=modules[0].Id,
            //            Name = "Lesson 1",
            //            Description ="Learn how to draw",
            //            CreatedById = admUser.Id,
            //            LastModifiedById = 1,
            //            ModifiedDate = DateTime.Now,
            //            CreatedDate =DateTime.Now,
            //            OrganizationId = orgId
            //        },
            //                  new Lesson
            //        {
            //            Name = "Lesson 2",
            //            Description ="Learn how to read",
            //            CreatedById = admUser.Id,
            //            LastModifiedById = 1,
            //            ModifiedDate = DateTime.Now,
            //            CreatedDate =DateTime.Now,
            //            OrganizationId = 1,
            //            ModuleId = 1,
            //            SortOrder = 2
            //        }
            //};

            //context.Set<Lesson>().AddOrUpdate(x => x.Name, lessons.ToArray());
            //context.SaveChanges();

            var lessonsContents = new List<Lesson>()
            {
                new Lesson
                {
                    ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                    IsExternalContent = false,
                    ModuleId=modules.FirstOrDefault().Id,
                     Description ="Learn how to swim",
                    Name ="Introduction to Focused and Diffused Modes",
                    CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 1
                },
                new Lesson
                {
                    HtmlContent="This is our content",
                    LessonContentType=Data.Models.Enums.LessonContentType.Text,
                    IsDeleted=false,
                     IsExternalContent = false,
                    ModuleId=modules.FirstOrDefault().Id,
                     Description ="Learn how to draw",
                    Name="Introduction to the course structure",
                    CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 2

                },
                new Lesson
                {
                    HtmlContent="<div><h4>This is another content</h4><p>Learn how to build</p></div>",
                    LessonContentType=Data.Models.Enums.LessonContentType.Text,
                    IsDeleted=false,
                     IsExternalContent = false,
                    ModuleId=modules.FirstOrDefault().Id,
                     Description ="Learn how to swim",
                    Name="Welcome and Course information",
                    CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 3
                },
                new Lesson
                {
                   ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                     IsExternalContent = false,
                    ModuleId=modules.FirstOrDefault().Id,
                     Description ="Learn how to eat",
                    Name="What is learning",
                    CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 4
                },
                //new Lesson
                //{
                //    ContentUrl="file.pdf",
                //    LessonContentType=Data.Models.Enums.LessonContentType.Document,
                //    IsDeleted=false,
                //     IsExternalContent = false,
                //     ModuleId=modules.FirstOrDefault().Id,
                //     Description ="Learn how to catch",
                //    Name="A Procastination preview",
                //    CreatedDate=DateTime.Now,
                //    ModifiedDate=DateTime.Now,
                //    OrganizationId = orgId,
                //    CreatedById=user.Id,
                //    SortOrder = 5
                //},
                new Lesson
                {
                    ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                     IsExternalContent = false,
                   ModuleId=modules.FirstOrDefault().Id,
                     Description ="Learn how to fly",
                    Name="Introduction to Memory",
                    CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 6
                },
                new Lesson
                {
                    ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                     IsExternalContent = false,
                   ModuleId=modules[1].Id,
                     Description ="Learn how to read",
                    Name="Importance of sleep in learning",
                     CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 1
                },
                new Lesson
                {
                    ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                     IsExternalContent = false,
                    ModuleId=modules[1].Id,
                     Description ="Learn how to chunk",
                    Name="Introduction to Chunking",
                    CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 2
                },
                new Lesson
                {
                    ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                     IsExternalContent = false,
                     ModuleId=modules[1].Id,
                     Description ="Learn how to chunk 2",
                    Name="How to form a chunk",
                    CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 3
                },
                new Lesson
                {
                    ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                     IsExternalContent = false,
                     ModuleId=modules[1].Id,
                     Description ="Learn how to think",
                    Name="Illusions of Competence",
                    CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 4
                },
                new Lesson
                {
                    ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                     IsExternalContent = false,
                      ModuleId=modules[1].Id,
                     Description ="Learn how to read",
                    Name="What motivates you",
                    CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 5
                },
                new Lesson
                {
                    ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                     IsExternalContent = false,
                      ModuleId=modules[1].Id,
                     Description ="Learn how to react",
                    Name="Introduction to Procastination and Memory",
                    CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 6
                },
                new Lesson
                {
                    ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                     IsExternalContent = false,
                     ModuleId=modules[1].Id,
                     Description ="Learn how to fast",
                    Name="Tackling Procastination",
                    CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 7
                },
                new Lesson
                {
                   ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                     IsExternalContent = false,
                     ModuleId=modules[2].Id,
                     Description ="Learn how to run",
                    Name="Zombies Everywhere",
                     CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 1
                },
                new Lesson
                {
                    ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                     IsExternalContent = false,
                    ModuleId=modules[2].Id,
                     Description ="Learn how to help",
                    Name="Harnessing your Zombies to help you",
                    CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 2
                },
                new Lesson
                {
                    ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                     IsExternalContent = false,
                    ModuleId=modules[2].Id,
                     Description ="Learn how to div",
                    Name="Diving deeper into memory",
                     CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 3
                },
                new Lesson
                {
                    ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                     IsExternalContent = false,
                    ModuleId=modules[2].Id,
                     Description ="Learn how to learn",
                    Name="How to become a better learner",
                     CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 4
                },
                new Lesson
                {
                    ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                     IsExternalContent = false,
                    ModuleId=modules[2].Id,
                     Description ="Learn how to think sharp",
                    Name="No need for Genius Envy",
                     CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 5
                },
                new Lesson
                {
                    ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                     IsExternalContent = false,
                    ModuleId=modules[2].Id,
                     Description ="Learn how to run",
                    Name="Change your thoughts, Change your life",
                     CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 6
                },
                new Lesson
                {
                    ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                     IsExternalContent = false,
                   ModuleId=modules[2].Id,
                     Description ="Learn how to work",
                    Name="The Value of Team Work",
                     CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 7
                },
                new Lesson
                {
                    ContentUrl="/Content/videos/topic1.webm",
                    MimeType="video/webm",
                    LessonContentType=Data.Models.Enums.LessonContentType.Video,
                    IsDeleted=false,
                     IsExternalContent = false,
                     ModuleId=modules[2].Id,
                     Description ="Learn how to wrap",
                    Name="Wrap up to the course",
                    CreatedDate=DateTime.Now,
                    ModifiedDate=DateTime.Now,
                    OrganizationId = orgId,
                    CreatedById=user.Id,
                    SortOrder = 8
                }

            };
            context.Set<Lesson>().AddOrUpdate(x => x.Name, lessonsContents.ToArray());
            context.SaveChanges();

            var training = new List<Training>
            {
                new Training
                {
                    AmountPerStaff=1200m,
                    BudgetExpended=100000m,
                    CreatedById = user.Id,
                    IsActive=true,
                    CreatedDate=DateTime.Now,
                    Description="This is the first of its kind",
                    DurationInMinutes=100,
                    StartPeriod=new DateTime(2018,03,01),
                    EndPeriod=new DateTime(2018,07,15),
                    Name="Safety and Security in Financial Institutions",
                    OrganizationId=orgId,
                    Year=DateTime.Now.Year,
                    Venue="Training School Ikeja",
                    Vendor="Sterling Bank plc/ITF",
                    TrainingType=Data.Models.Enums.TrainingType.ClassRoom,
                    PeriodFormat = "[{\"Id\":1,\"Day\":1,\"DayName\":\"Monday\",\"StartTime\":\"05:30 PM\",\"EndTime\":\"07:30 PM\",\"StartSpan\":\"17:30:00\",\"EndSpan\":\"19:30:00\"},{\"Id\":2,\"Day\":3,\"DayName\":\"Wednesday\",\"StartTime\":\"05:30 PM\",\"EndTime\":\"07:30 PM\",\"StartSpan\":\"17:30:00\",\"EndSpan\":\"19:30:00\"}]",
                    Location ="Lagos",
                    TrainingCategory=Data.Models.Enums.TrainingCategory.Generic,

                },

                new Training
                {
                    AmountPerStaff=1500m,
                    BudgetExpended=330000m,
                    CreatedById = user.Id,
                    IsActive=true,
                    CreatedDate=DateTime.Now,
                    Description="Training that will bore the life out of you",
                    DurationInMinutes=420,
                    StartPeriod=new DateTime(2018,10,11),
                    EndPeriod=new DateTime(2018,10,25),
                    Name="Basic Credit Course",
                    OrganizationId=orgId,
                    Year=DateTime.Now.Year,
                    Venue="Sterling Boulevard, Abuja",
                    Vendor="Sterling Bank plc/LeapsAndBounds",
                    TrainingType=Data.Models.Enums.TrainingType.ClassRoom,

                    Location="Abuja",
                    TrainingCategory=Data.Models.Enums.TrainingCategory.Regulatory

                },

                new Training
                {
                    AmountPerStaff=19300m,
                    BudgetExpended=1780000m,
                    CreatedById = user.Id,
                    IsActive=true,
                    CreatedDate=DateTime.Now,
                    Description="This is the training that will change your mind",
                    DurationInMinutes=900,
                    StartPeriod=new DateTime(2018,06,01),
                    EndPeriod=new DateTime(2018,06,10),
                    Name="Financial Inclusion and Micro Banking:Extending the Frontiers",
                    OrganizationId=orgId,
                    Year=DateTime.Now.Year,
                    Venue="Banker's House, Victoria Island",
                    Vendor="CIBN",
                    TrainingType=Data.Models.Enums.TrainingType.External,

                    Location="Lagos",
                    TrainingCategory=Data.Models.Enums.TrainingCategory.Induction
                },
                 new Training
                {
                    AmountPerStaff=4300m,
                    BudgetExpended=2290000m,
                    CreatedById = user.Id,
                    IsActive=true,
                    CreatedDate=DateTime.Now,
                    Description="This is the training that will change your life",
                    DurationInMinutes=400,
                    StartPeriod=new DateTime(2018,07,01),
                    EndPeriod=new DateTime(2018,07,15),
                    Name="Compliance Risk Management",
                    OrganizationId=orgId,
                    Year=DateTime.Now.Year,
                    Venue="NECA House, Alausa Ikeja",
                    Vendor="Sterling Bank",
                    TrainingType=Data.Models.Enums.TrainingType.External,

                    Location="Lagos",
                    TrainingCategory=Data.Models.Enums.TrainingCategory.Functional
                },
                  new Training
                {
                    AmountPerStaff=5700m,
                    BudgetExpended=1580000m,
                    CreatedById = user.Id,
                    IsActive=true,
                    CreatedDate=DateTime.Now,
                    Description="This is the training that will make you a better sales man",
                    DurationInMinutes=100,
                    StartPeriod=new DateTime(2018,07,01),
                    EndPeriod=new DateTime(2018,07,15),
                    Name="Sales Training Program",
                    OrganizationId=orgId,
                    Year=DateTime.Now.Year,
                    Venue="LCCI, Alausa Ikeja",
                    Vendor="Sterling Bank plc/Filigri",
                    TrainingType=Data.Models.Enums.TrainingType.External,

                    Location="Lagos",
                    TrainingCategory=Data.Models.Enums.TrainingCategory.Regulatory
                },
                   new Training
                {
                    AmountPerStaff=8200m,
                    BudgetExpended=1780000m,
                    CreatedById = user.Id,
                    IsActive=true,
                    CreatedDate=DateTime.Now,
                    Description="This is the training that will change your life",
                    DurationInMinutes=100,
                    StartPeriod=new DateTime(2018,07,01),
                    EndPeriod=new DateTime(2018,07,15),
                    Name="AML/CLF Compliance",
                    OrganizationId=orgId,
                    Year=DateTime.Now.Year,
                    Venue="LCCI, Alausa Ikeja",
                    Vendor="Sterling Bank",
                    TrainingType=Data.Models.Enums.TrainingType.External,

                    Location="Lagos",
                    TrainingCategory=Data.Models.Enums.TrainingCategory.Generic
                }

            };

            context.Set<Training>().AddOrUpdate(x => x.Name, training.ToArray());
            context.SaveChanges();

            var userTraining = new UserTraining
            {
                CreatedById = user.Id,
                CreatedDate = DateTime.Now,
                LastModifiedById = user.Id,
                ModifiedDate = DateTime.Now,
                OrganizationId = organization.Id,
                TrainingId = training[0].Id,
                UserId = user.Id
            };

            context.Set<UserTraining>().Add(userTraining);
            context.SaveChanges();

            var trainingPeriod = new List<TrainingPeriod>()
            {
                new TrainingPeriod
                {
                    CreatedById = user.Id,
                    CreatedDate = training[0].CreatedDate,
                    Day = DayOfWeek.Monday,
                    EndSpan = new TimeSpan(19,30,00),
                    EndTime = "07:30 PM",
                    LastModifiedById = user.Id,
                    ModifiedDate = training[0].CreatedDate,
                    OrganizationId = organization.Id,
                    StartSpan = new TimeSpan(17,30,00),
                    StartTime = "05:30 PM",
                    TrainingId = training[0].Id
                }
                ,
                new TrainingPeriod
                {
                    CreatedById = user.Id,
                    CreatedDate = training[0].CreatedDate,
                    Day = DayOfWeek.Wednesday,
                    EndSpan = new TimeSpan(19,30,00),
                    EndTime = "07:30 PM",
                    LastModifiedById = user.Id,
                    ModifiedDate = training[0].CreatedDate,
                    OrganizationId = organization.Id,
                    StartSpan = new TimeSpan(17,30,00),
                    StartTime = "05:30 PM",
                    TrainingId = training[0].Id
                }
            };
            context.Set<TrainingPeriod>().AddOrUpdate(x => x.Day, trainingPeriod.ToArray());
            context.SaveChanges();

            var learningGrp = new List<LearningGroup>{ new LearningGroup
                    {
                        CreatedById = user.Id,
                        Name = "Required Learning Group",
                        OrganizationId = orgId,
                        CreatedDate = DateTime.UtcNow,
                        TagFormat = "[{\"TagType\":2,\"TagValue\":1,\"Name\":\"Gender_Male\"}]"
                        //Availability =Data.Models.Enums.UserCourseAvailability.Required
                    },
                    new LearningGroup
                    {
                        CreatedById = user.Id,
                        Name = "Requested Learning Group",
                        OrganizationId = orgId,
                        CreatedDate = DateTime.UtcNow,
                       TagFormat = "[{\"TagType\":2,\"TagValue\":2,\"Name\":\"Gender_Female\"}]"
                    }
            };

            context.Set<LearningGroup>().AddOrUpdate(x => x.Name, learningGrp.ToArray());
            context.SaveChanges();

           

            var learningGrpCourse = new List<LearningGroupCourse> {
                new LearningGroupCourse  {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.UtcNow,
                    CourseId=courses[0].Id,
                    Availability=Data.Models.Enums.UserCourseAvailability.Required,
                    LearningGroupId=learningGrp[0].Id
                },
                 new LearningGroupCourse  {
                     CreatedById = user.Id,
                     CreatedDate = DateTime.UtcNow,
                     CourseId=courses[1].Id,
                     Availability =Data.Models.Enums.UserCourseAvailability.Required,
                     LearningGroupId=learningGrp[0].Id
                },
                 new LearningGroupCourse  {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.UtcNow,
                    LearningGroupId =learningGrp[0].Id,
                    Availability=Data.Models.Enums.UserCourseAvailability.Required,
                    CourseId=courses[2].Id
                },
                 new LearningGroupCourse  {
                     CreatedById = user.Id,
                     CreatedDate = DateTime.UtcNow,
                     CourseId=courses[3].Id,
                     Availability=Data.Models.Enums.UserCourseAvailability.Generic,
                     LearningGroupId=learningGrp[1].Id
                },
                 new LearningGroupCourse  {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.UtcNow,
                    LearningGroupId =learningGrp[1].Id,
                     Availability=Data.Models.Enums.UserCourseAvailability.Generic,
                    CourseId=courses[4].Id
                }
            };

            context.Set<LearningGroupCourse>().AddOrUpdate(x => x.CourseId, learningGrpCourse.ToArray());
            context.SaveChanges();

            var trainingGroupTraining = new List<LearningGroupTraining> {
                new LearningGroupTraining  {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.UtcNow,
                    TrainingId=training[0].Id,
                    LearningGroupId=learningGrp[0].Id
                },
                 new LearningGroupTraining  {
                     CreatedById = user.Id,
                     CreatedDate = DateTime.UtcNow,
                     TrainingId=training[1].Id,
                      LearningGroupId=learningGrp[0].Id
                },
                 new LearningGroupTraining  {
                    CreatedById = user.Id,
                    CreatedDate = DateTime.UtcNow,
                     LearningGroupId=learningGrp[1].Id,
                    TrainingId=training[2].Id
                },
                 new LearningGroupTraining  {
                     CreatedById = user.Id,
                     CreatedDate = DateTime.UtcNow,
                     TrainingId=training[3].Id,
                     LearningGroupId=learningGrp[1].Id
                }
                
            };

            context.Set<LearningGroupTraining>().AddOrUpdate(x => x.TrainingId, trainingGroupTraining.ToArray());
            context.SaveChanges();

            var learningGrpTagFilter = new List<LearningGroupTag> {
                new LearningGroupTag {
                    TagType = Data.Models.Enums.LearningGroupTagType.Gender,
                      TagValue = (int)Data.Models.Enums.Gender.Male,
                      LearningGroupId=learningGrp[0].Id,
                CreatedDate = DateTime.UtcNow,
                },
                new LearningGroupTag {
                      TagType = Data.Models.Enums.LearningGroupTagType.Gender,
                      TagValue = (int)Data.Models.Enums.Gender.Female,
                      LearningGroupId=learningGrp[1].Id,
                CreatedDate = DateTime.UtcNow,
                }
            };

            context.Set<LearningGroupTag>().AddOrUpdate(x => x.Id, learningGrpTagFilter.ToArray());
            context.SaveChanges();


            var notificationList = Enum.GetValues(typeof(Data.Models.Enums.NotificationType)).Cast<Data.Models.Enums.NotificationType>();

            foreach(var item in notificationList) 
            {
                var notificationType = new NotificationTypes
                {
                    NotificationType = (NotificationType)item,
                    NotificationAction = CommonHelper.GetDescription(item)
                };
                context.Set<NotificationTypes>().AddOrUpdate(notificationType);
                context.SaveChanges();
            }


            var notification = new List<Notification>()
            {
                new Notification
                {
                     NotificationType= Data.Models.Enums.NotificationType.BudgetChange,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,03),
                    IsDeleted=false,
                    IsForEmployee=false,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=true,
                    IsForManagement=false,
                    MailMessage="Dear {FirstName}, The training budget for {Name} has been changed to {NewBudget}. Please redirect to {ActionURL} to view this change.",
                    NotificationMessage="The training budget for {Name} has been changed to {NewBudget}",
                    MailSubject="{AppName}: Training Budget Change",
                    OrganizationId=1,
                    CanIgnoreMail = false,
                    IsMail = true,
                    IsNotification = true,
                    UsedTags = "[\"FirstName\",\"Name\",\"NewBudget\",\"ActionURL\",\"AppName\"]"
                },

                 new Notification{

                    NotificationType=Data.Models.Enums.NotificationType.CourseAssignment,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,03),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                    MailMessage="Dear {FirstName}, a course {Name} has been assigned to you on the Learning Management System. Please ensure to do it as soon as possible because the due date is in {EndDate}."+
                    "Take course on {ActionURL} <br><br> {IgnoreMail}",
                    NotificationMessage="New course assignment, {Name}",
                    MailSubject="{AppName}: New Course Assignment",
                    OrganizationId=1,
                    CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                    UsedTags = "[\"FirstName\",\"EndDate\",\"ActionURL\",\"IgnoreMail\",\"Name\",\"AppName\"]"
                },

                   new Notification
                {
                     NotificationType=Data.Models.Enums.NotificationType.CourseInactivity,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,03),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                    MailMessage="Dear {FirstName}, you have not completed this course {Name}. Please redirect to {ActionURL} to continue. <br><br> {IgnoreMail}",
                    NotificationMessage="Course Pending completion: {Name}",
                    MailSubject="{AppName}: Course Inactivity",
                    OrganizationId=1,
                     CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                     UsedTags = "[\"FirstName\",\"IgnoreMail\",\"Name\",\"AppName\"]"

                   },

                       new Notification
                {
                     NotificationType=Data.Models.Enums.NotificationType.NewAccount,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,03),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                    MailMessage="Dear {FirstName}, you have been created on. Please redirect to {ActionURL} to activate your account.",
                    MailSubject="{AppName}: Activate Account",
                    OrganizationId=1,
                      CanIgnoreMail = false,
                    IsMail = true,
                    IsNotification = false,
                    MailSetupDisabled = true,
                     UsedTags = "[\"FirstName\",\"ActionURL\",\"AppName\"]"

               },

                 new Notification
                {
                     NotificationType=Data.Models.Enums.NotificationType.EmployeeUpdate,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,03),
                    IsDeleted=false,
                    IsForEmployee=false,
                    IsForHR=true,
                    IsForIT=false,
                    IsForLD=true,
                    IsForManagement=false,
                    MailMessage="Dear {FirstName}, {ActionUser} account pending update. Please redirect to {ActionURL} to update.",
                    NotificationMessage ="User account {ActionUser} pending update",
                    MailSubject="{AppName}: Update User Account",
                    OrganizationId=1,
                      CanIgnoreMail = false,
                    IsMail = true,
                    IsNotification = true,
                    UsedTags = "[\"FirstName\",\"ActionUser\",\"ActionURL\",\"AppName\"]"

               },

                          new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.ExamAssignment,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                    MailMessage="Dear {FirstName}, an exam {Name} has been assigned to you on the Learning Management System. " +
                    "Please ensure to do it as soon as possible because the due date is in {EndDate}. Take exam on {ActionURL}"+
                    "<br><br> {IgnoreMail}",
                    NotificationMessage = "New exam assignment, {Name}",
                    MailSubject = "{AppName}: New Exam Assignment",
                    OrganizationId =1,
                      CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                     UsedTags = "[\"FirstName\",\"Name\",\"EndDate\",\"ActionURL\",\"IgnoreMail\",\"AppName\"]"
                },


                               new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.ExamDueDatePrompt,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                    MailMessage="Dear {FirstName}, the due date for {Name} is {EndDate}. Please ensure you take this exam before the stated date. " +
                    "Take exam now on {ActionURL}"+
                    "<br><br> {IgnoreMail}",
                    NotificationMessage = "Exam: {Name} will end on {EndDate}",
                    MailSubject = "{AppName}: Exam Due date Reminder",
                    OrganizationId =1,
                      CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                    UsedTags = "[\"FirstName\",\"Name\",\"EndDate\",\"ActionURL\",\"IgnoreMail\",\"AppName\"]"
                },


                                       new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.ExamInactivity,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                       MailMessage="Dear {FirstName}, you have not completed this exam {Name}. Please redirect to {ActionURL} to continue."+
                       "<br><br> {IgnoreMail}",
                    NotificationMessage="Exam Pending completion: {Name}",
                    MailSubject="{AppName}: Examination Inactivity",
                    OrganizationId =1,
                     CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                    UsedTags = "[\"FirstName\",\"Name\",\"ActionURL\",\"IgnoreMail\",\"AppName\"]"
                },

                                         new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.ForgotPassword,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                       MailMessage="Dear {FirstName}, Please redirect to {ActionURL} to change your password.",
                    MailSubject="{AppName}: Forgot Password",
                    OrganizationId =1,
                     CanIgnoreMail = false,
                    IsMail = true,
                    IsNotification = false,
                    MailSetupDisabled = true,
                    UsedTags = "[\"FirstName\",\"ActionURL\",\"AppName\"]"
                },

                                             new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.NewAdminSupportTicket,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=false,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=true,
                    IsForManagement=false,
                       MailMessage="Dear {FirstName}, a new support issue has been opened by {ActionUser}. <br> Details: {Message},<br> Please redirect to {ActionURL} to view this.",
                    MailSubject="{AppName}: New Support Issue",
                     NotificationMessage="New support issue: {Title}",
                    OrganizationId =1,
                       CanIgnoreMail = false,
                    IsMail = true,
                    IsNotification = true,
                     UsedTags = "[\"FirstName\",\"ActionUser\",\"Message\",\"Title\",\"ActionURL\",\"AppName\"]"
                },

                                             new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.NewDiscussionReply,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                       MailMessage="Dear {FirstName}, {ActionUser} replied your comment {PrevComment} on {CourseName} . <br> Details: {Message},<br> Please redirect to {ActionURL} to view this."+
                       "<br><br> {IgnoreMail}",
                    MailSubject="{AppName}: New comment reply",
                      NotificationMessage="New comment reply on {CourseName} by {ActionUser}",
                    OrganizationId =1,
                       CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                      UsedTags = "[\"FirstName\",\"ActionUser\",\"PrevComment\",\"CourseName\",\"Message\",\"IgnoreMail\",\"ActionURL\",\"AppName\"]"
                },
  new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.NewITSupportTicket,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=false,
                    IsForHR=false,
                    IsForIT=true,
                    IsForLD=false,
                    IsForManagement=false,
                      MailMessage="Dear {FirstName}, a new support issue has been opened by {ActionUser}. <br> Details: {Message},<br> Please redirect to {ActionURL} to view this.",
                    MailSubject="{AppName}: New Support Issue",
                     NotificationMessage="New support issue: {Title}",
                    OrganizationId =1,
                       CanIgnoreMail = false,
                    IsMail = true,
                    IsNotification = true,
                    UsedTags = "[\"FirstName\",\"ActionUser\",\"Title\",\"Message\",\"ActionURL\",\"AppName\"]"
                },

 new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.NewModerateComment,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=false,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=true,
                    IsForManagement=false,
                      MailMessage="Dear {FirstName}, a new flag was raised on a comment by {ActionUser}. <br> Comment Made: {Message},<br> Please redirect to {ActionURL} to view this.",
                    MailSubject="{AppName}: Moderate Comment",
                     NotificationMessage=" Moderate Comment made by {ActionUser}: {DateCreated}",
                    OrganizationId =1,
                       CanIgnoreMail = false,
                    IsMail = true,
                    IsNotification = true,
                    UsedTags = "[\"FirstName\",\"ActionUser\",\"DateCreated\",\"Message\",\"ActionURL\",\"AppName\"]"
                },
               new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.NewTrainingRequest,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=false,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=true,
                    IsForManagement=false,
                      MailMessage="Dear {FirstName}, {ActionUser} has sent a training request for {Name} <br> Please redirect to {ActionURL} to view this.",
                    MailSubject="{AppName}: New Training Request",
                     NotificationMessage=" New Training Request:{ActionUser} - {Name}",
                    OrganizationId =1,
                       CanIgnoreMail = false,
                    IsMail = true,
                    IsNotification = true,
                    UsedTags = "[\"FirstName\",\"ActionUser\",\"Name\",\"ActionURL\",\"AppName\"]"
                }
               ,

                new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.NewTrainingVideo,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                      MailMessage="Dear {FirstName}, A new training video {Name} has been uploaded <br> Please redirect to {ActionURL} to view this."+
                        "<br><br> {IgnoreMail}",
                    MailSubject="{AppName}: New Training Video Uploaded",
                     NotificationMessage=" New Training Video: {Name}",
                    OrganizationId =1,
                       CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                     UsedTags = "[\"FirstName\",\"Name\",\"ActionURL\",\"IgnoreMail\",\"AppName\"]"
                },

               new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.PendingModerateComments,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=false,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=true,
                    IsForManagement=false,
                      MailMessage="Dear {FirstName}, You have {Count} comments pending moderation <br> Please redirect to {ActionURL} to view this."+
                        "<br><br> {IgnoreMail}",
                    MailSubject="{AppName}: Comments pending moderation count",
                     NotificationMessage="{Count} comments pending moderation",
                    OrganizationId =1,
                       CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                     UsedTags = "[\"FirstName\",\"Count\",\"ActionURL\",\"IgnoreMail\",\"AppName\"]"
                },

                  new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.PendingTrainingRequests,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=false,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=true,
                    IsForManagement=false,
                      MailMessage="Dear {FirstName}, You have {Count} unattended training requests <br> Please redirect to {ActionURL} to view this."+
                        "<br><br> {IgnoreMail}",
                    MailSubject="{AppName}: Training requests pending action",
                     NotificationMessage="{Count} training requests pending action",
                    OrganizationId =1,
                       CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                    UsedTags = "[\"FirstName\",\"Count\",\"ActionURL\",\"IgnoreMail\",\"AppName\"]"
                },

                    new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.PendingUserProfileChange,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForHR=true,
                    IsForIT=false,
                    IsForLD=true,
                    IsForManagement=false,
                      MailMessage="Dear {FirstName}, You have {Count} un updated user accounts <br> Please redirect to {ActionURL} to view this."+
                        "<br><br> {IgnoreMail}",
                    MailSubject="{AppName}: User accounts pending update",
                     NotificationMessage="{Count} user accounts pending action",
                    OrganizationId =1,
                       CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                    UsedTags = "[\"FirstName\",\"Count\",\"ActionURL\",\"IgnoreMail\",\"AppName\"]"
                },

                         new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.SupportTicketClosed,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=true,
                    IsForManagement=false,
                      MailMessage="Dear {FirstName}, the ticket you raised <br> Details: {Message} <br> has been successfully treated and thereafter closed.<br> Please do let us know if the issue comes up again.",
                     MailSubject="{AppName}: Support ticket closed",
                     NotificationMessage = "Support ticket closed: {Title}",
                    OrganizationId =1,
                       CanIgnoreMail = false,
                    IsMail = true,
                    IsNotification = true,
                    UsedTags = "[\"FirstName\",\"Message\",\"AppName\"]"
                },

                               new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.SurveyAssignment,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                       MailMessage="Dear {FirstName}, a survey {Name} has been assigned to you on the Learning Management System."+
                    "Take exam on {ActionURL} <br><br> {IgnoreMail}",
                    NotificationMessage="New survey assignment, {Name}",
                    MailSubject="{AppName}: New survey assignment",
                    OrganizationId=1,
                    CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                    UsedTags = "[\"FirstName\",\"Name\",\"ActionURL\",\"IgnoreMail\",\"Message\",\"AppName\"]"
                },

      new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.TrainingAssignment,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                       MailMessage="Dear {FirstName}, a training {Name} has been assigned to you on the Learning Management System. "+
                    "Take exam on {ActionURL} <br><br> {IgnoreMail}",
                    NotificationMessage="New training assignment, {Name}",
                    MailSubject="{AppName}: New training assignment",
                    OrganizationId=1,
                    CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                    UsedTags = "[\"FirstName\",\"Name\",\"ActionURL\",\"IgnoreMail\",\"AppName\"]"
                },

          new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.TrainingBudgetDepletion100percent,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=false,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=true,
                    IsForManagement=false,
                       MailMessage="Dear {FirstName}, this is to inform you that the budget allocated for {GroupName}, has been exhausted.",
                    NotificationMessage="Training budget depleted, {GroupName}",
                    MailSubject="{AppName}: Training budget depleted",
                    OrganizationId=1,
                    CanIgnoreMail = false,
                    IsMail = true,
                    IsNotification = true,
                    UsedTags = "[\"FirstName\",\"GroupName\",\"AppName\"]"
                },


             new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.TrainingBudgetDepletion50percent,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=false,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=true,
                    IsForManagement=false,
                       MailMessage="Dear {FirstName},please note 50% of the training budget for {GroupName} has been used.",
                    NotificationMessage="50% of Training budget depleted, {GroupName}",
                    MailSubject="{AppName}: Training budget 50% depleted",
                    OrganizationId=1,
                    CanIgnoreMail = false,
                    IsMail = true,
                    IsNotification = true,
                     UsedTags = "[\"FirstName\",\"GroupName\",\"AppName\"]"
                },

                new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.TrainingBudgetDepletion90percent,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=false,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=true,
                    IsForManagement=false,
                       MailMessage="Dear {FirstName},please note 90% of the training budget for {GroupName} has been used.",
                    NotificationMessage="90% of Training budget depleted, {GroupName}",
                    MailSubject="{AppName}: Training budget 90% depleted",
                    OrganizationId=1,
                    CanIgnoreMail = false,
                    IsMail = true,
                    IsNotification = true,
                     UsedTags = "[\"FirstName\",\"GroupName\",\"AppName\"]"
                },

           

                        new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.TrainingRequestApprovedByLineManager,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                       MailMessage="Dear {FirstName}, Your training request for {Name} has been approved by your line manager. It is currenly awaiting approval from Learning and Development"
                       +"<br><br> {IgnoreMail}",
                    NotificationMessage ="Training request for {Name} approved by line manager",
                    MailSubject="{AppName}: Training request approved by line manager",
                    OrganizationId=1,
                    CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                     UsedTags = "[\"FirstName\",\"Name\",\"AppName\",\"IgnoreMail\"]"
                },
                        new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.TrainingRequestApprovedByAdmin,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                       MailMessage="Dear {FirstName}, Your training request for {Name} has been approved by Learning and Development."
                       +"<br><br> {IgnoreMail}",
                    NotificationMessage ="Training request for {Name} approved by learning and development",
                    MailSubject="{AppName}: Training request approved by learning and development",
                    OrganizationId=1,
                    CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                      UsedTags = "[\"FirstName\",\"Name\",\"AppName\",\"IgnoreMail\"]"
                },

  new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.TrainingRequestRejectedByLineManager,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                       MailMessage="Dear {FirstName}, Your training request for {Name} has been rejected by your line manager"
                        +"<br><br> {IgnoreMail}",
                    NotificationMessage="Training request for {Name} rejected by your line manager",
                    MailSubject="{AppName}: Training request rejected by your line manager",
                    OrganizationId=1,
                    CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                      UsedTags = "[\"FirstName\",\"Name\",\"AppName\",\"IgnoreMail\"]"
                },

  new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.TrainingRequestRejectedByAdmin,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                       MailMessage="Dear {FirstName}, Your training request for {Name} has been rejected by your learning and development"
                        +"<br><br> {IgnoreMail}",
                    NotificationMessage="Training request for {Name} rejected by learning and development",
                    MailSubject="{AppName}: Training request rejected by learning and development",
                    OrganizationId=1,
                    CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                      UsedTags = "[\"FirstName\",\"Name\",\"AppName\",\"IgnoreMail\"]"
                },

                         new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.UnOpenedTicket,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=false,
                    IsForHR=false,
                    IsForIT=true,
                    IsForLD=true,
                    IsForManagement=false,
                      MailMessage="Dear {FirstName}, You have {Count} tickets un-opened <br> Please redirect to {ActionURL} to view this."+
                        "<br><br> {IgnoreMail}",
                    MailSubject="{AppName}: Support tickets un-opened",
                     NotificationMessage="{Count} Support ticket un-opened",
                    OrganizationId =1,
                    CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                      UsedTags = "[\"FirstName\",\"Count\",\"ActionURL\",\"AppName\",\"IgnoreMail\"]"
                },

                          new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.UpcomingTrainingPeriod,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                      MailMessage="Dear {FirstName}, {Duration} to go till {Name} training begins."+
                        "<br><br> {IgnoreMail}",
                    MailSubject="{AppName}: Upcoming Training",
                     NotificationMessage="Upcoming training - {Name}",
                    OrganizationId =1,
                    CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                    UsedTags = "[\"FirstName\",\"Name\",\"Duration\",\"ActionURL\",\"AppName\",\"IgnoreMail\"]"
                },


                          new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.UserProfileEdited,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                      MailMessage="Dear {FirstName}, your profile as been updated. Click {ActionURL} to view details"+
                       "<br><br> {IgnoreMail}",
                    MailSubject="{AppName}: Profile Updated",
                     NotificationMessage="Your profile as been update",
                    OrganizationId =1,
                    CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                    UsedTags = "[\"FirstName\",\"ActionURL\",\"AppName\",\"IgnoreMail\"]"
                },

                                   new Notification
                {
                    NotificationType=Data.Models.Enums.NotificationType.WeeklyUpdate,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,11),
                    IsDeleted=false,
                    IsForEmployee=true,
                    IsForHR=false,
                    IsForIT=false,
                    IsForLD=false,
                    IsForManagement=false,
                      MailMessage="Dear {FirstName}, Here is a quick overview of this week's activities<br>{OngoingCourse}<br>{CurrentWeekExam}<br>{CurrentWeekCourse}<br>{CurrentWeekExam}"+
                      "<br>{NextWeekExam}<br>{CurrentWeekSurvey}<br>{NextWeekSurvey}<br>{CurrentWeekCourseEnd}<br>{NextWeekCourseEnd}<br>{CurrentWeekExamEnd}<br>{NextWeekExamEnd}"+
                      "{CurrentWeekSurveyEnd}<br>{NextWeekSurveyEnd}<br>{CurrentWeekTraining}<br>{NextWeekTraining}<br>{CurrentWeekTrainingEnd}<br>{NextWeekTrainingEnd}<br>{MostTakenCourse}"+
                      "{MostTakenCourseInRegion}<br>{MostTakenCourseInGroup}<br>{MostTakenTraining}<br>{MostTakenTrainingInRegion}<br>{MostTakenTrainingInGroup}"+
                       "<br><br> {IgnoreMail}",
                    MailSubject="{AppName}: Weekly Update",
                    NotificationMessage ="View this week status here {ActionURL}",
                    OrganizationId =1,
                    CanIgnoreMail = true,
                    IsMail = true,
                    IsNotification = true,
                     UsedTags = "[\"FirstName\",\"OngoingCourse\",\"CurrentWeekExam\",\"CurrentWeekCourse\",\"CurrentWeekExam\",\"NextWeekExam\",\"CurrentWeekSurvey\",\"NextWeekSurvey\""+
                     ",\"CurrentWeekCourseEnd\",\"NextWeekCourseEnd\",\"CurrentWeekExamEnd\",\"NextWeekExamEnd\",\"CurrentWeekSurveyEnd\",\"NextWeekSurveyEnd\",\"CurrentWeekTraining\""+
                     ",\"NextWeekTraining\",\"CurrentWeekTrainingEnd\",\"NextWeekTrainingEnd\",\"MostTakenCourse\",\"MostTakenCourseInRegion\",\"MostTakenCourseInGroup\"" +
                     ",\"MostTakenTraining\",\"MostTakenTrainingInRegion\",\"MostTakenTrainingInGroup\",\"ActionURL\",\"AppName\",\"IgnoreMail\"]"
                }            };
            context.Set<Notification>().AddOrUpdate(x=> x.NotificationType, notification.ToArray());
            context.SaveChanges();

            var faqList = new List<FAQ>()
            {
                new FAQ
                {
                    CreatedById = user.Id,
                    Title = "I'm having trouble signing in",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Morbi leo urna molestie at elementum. Ut diam quam nulla portti",
                    LastModifiedById = user.Id,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    SortOrder = 1
                },
                 new FAQ
                {
                    CreatedById = user.Id,
                    Title = "I forgot my password, How can I reset it?",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Morbi leo urna molestie at elementum. Ut diam quam nulla portti",
                    LastModifiedById = user.Id,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    SortOrder = 2
                },
                  new FAQ
                {
                    CreatedById = user.Id,
                    Title = "I'm not receiving emails",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Morbi leo urna molestie at elementum. Ut diam quam nulla portti",
                    LastModifiedById = user.Id,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    SortOrder = 3
                },
                   new FAQ
                {
                    CreatedById = user.Id,
                    Title = "I have a training I wish to participate in",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Morbi leo urna molestie at elementum. Ut diam quam nulla portti",
                    LastModifiedById = user.Id,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    SortOrder = 4
                },
                    new FAQ
                {
                    CreatedById = user.Id,
                    Title = "How can I reset my courses",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Morbi leo urna molestie at elementum. Ut diam quam nulla portti",
                    LastModifiedById = user.Id,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    SortOrder = 5
                }
            };
            context.Set<FAQ>().AddOrUpdate(x => x.Title, faqList.ToArray());
            context.SaveChanges();


        }

        internal static Organization CreateSterlingOrganization(SterlingBankLmsContext context, int userId)
        {
            var organization = new Organization
            {
                Name = "Sterling Bank PLC",
                Email = "info@sterlingbank.com",
                IsLockedOut = false,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedById = userId,
            };

            context.Set<Organization>().AddOrUpdate(p => p.Name, organization);
            context.SaveChanges();
            return organization;
        }

        public static void UpdateUsersDetails(DbContext context)
        {
            var userStore = new ApplicationUserStore(context);

            var userManager = new ApplicationUserManager(userStore);

            var user = userManager.FindByNameAsync("administrator").Result;

            //user.BranchId = context.Set<Branch>().FirstOrDefault().Id;
            //user.RegionId = context.Set<Region>().FirstOrDefault().Id;
            //user.DepartmentId = context.Set<Department>().FirstOrDefault().Id;
            //user.GroupId = context.Set<Group>().FirstOrDefault().Id;
            user.OrganizationId = context.Set<Organization>().FirstOrDefault().Id;
         //   user.GradeId = context.Set<Grade>().ToList()[14].Id;

            userManager.UpdateAsync(user).Wait();

            //var hr = userManager.FindByNameAsync("felicity").Result;

            //hr.BranchId = context.Set<Branch>().ToList()[1].Id;
            //hr.RegionId = context.Set<Region>().ToList()[5].Id;
            //hr.DepartmentId = context.Set<Department>().ToList()[3].Id;
            //hr.GroupId = context.Set<Group>().ToList()[4].Id;
            //hr.OrganizationId = context.Set<Organization>().FirstOrDefault().Id;
            //hr.GradeId = context.Set<Grade>().ToList()[12].Id;
            //hr.LineManagerFirstName = "Zeplin";
            //hr.LineManagerLastName = "Darrell";
            //hr.LineManagerId = 1;

            //userManager.UpdateAsync(hr).Wait();

            //var it = userManager.FindByNameAsync("mark").Result;

            //it.BranchId = context.Set<Branch>().ToList()[1].Id;
            //it.RegionId = context.Set<Region>().ToList()[6].Id;
            //it.DepartmentId = context.Set<Department>().ToList()[2].Id;
            //it.GroupId = context.Set<Group>().ToList()[3].Id;
            //it.OrganizationId = context.Set<Organization>().FirstOrDefault().Id;
            //it.GradeId = context.Set<Grade>().ToList()[11].Id;
            //it.LineManagerFirstName = "Zeplin";
            //it.LineManagerLastName = "Darrell";
            //it.LineManagerId = 1;

            //userManager.UpdateAsync(it).Wait();

            //var mgt = userManager.FindByNameAsync("irene").Result;

            //mgt.BranchId = context.Set<Branch>().ToList()[0].Id;
            //mgt.RegionId = context.Set<Region>().ToList()[2].Id;
            //mgt.DepartmentId = context.Set<Department>().ToList()[1].Id;
            //mgt.GroupId = context.Set<Group>().ToList()[3].Id;
            //mgt.OrganizationId = context.Set<Organization>().FirstOrDefault().Id;
            //mgt.GradeId = context.Set<Grade>().ToList()[13].Id;
            //mgt.LineManagerFirstName = "Zeplin";
            //mgt.LineManagerLastName = "Darrell";
            //mgt.LineManagerId = 1;

            //userManager.UpdateAsync(mgt).Wait();


        }
    }
}