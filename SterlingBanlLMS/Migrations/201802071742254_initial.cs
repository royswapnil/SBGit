namespace SterlingBankLMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Active = c.Boolean(nullable: false),
                        SystemName = c.String(),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PictureUrl = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        LastLoginDateUtc = c.DateTime(),
                        JoinDateUtc = c.DateTime(nullable: false),
                        IsOwnerAccount = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BudgetId = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DepartmentBudget", t => t.BudgetId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.BudgetId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.DepartmentBudget",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Boolean(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .Index(t => t.DepartmentId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Organization",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsLockedOut = c.Boolean(nullable: false),
                        Email = c.String(),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.ErrorLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Page = c.String(),
                        ErrorCode = c.String(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Examination",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(),
                        ImageUrl = c.String(),
                        ExamType = c.Int(nullable: false),
                        PassGrade = c.Decimal(precision: 18, scale: 2),
                        Name = c.String(),
                        Description = c.String(),
                        HourLimit = c.Int(nullable: false),
                        MinLimit = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.CourseId)
                .ForeignKey("dbo.User", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ImageUrl = c.String(),
                        ExamRetakeCount = c.Int(nullable: false),
                        DueDate = c.DateTime(),
                        LearningAreaId = c.Int(nullable: false),
                        PassGrade = c.Decimal(precision: 18, scale: 2),
                        CertificateId = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Certificate", t => t.CertificateId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.LearningArea", t => t.LearningAreaId, cascadeDelete: true)
                .ForeignKey("dbo.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.LearningAreaId)
                .Index(t => t.CertificateId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.Certificate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        TemplateUrl = c.String(),
                        TotalAwarded = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.CourseId)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .Index(t => t.CourseId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.LearningArea",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.CourseModule",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SortOrder = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .Index(t => t.CourseId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.LessonContent",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentBody = c.String(),
                        ContentUrl = c.String(),
                        MimeType = c.String(),
                        ContentQuizId = c.Int(),
                        LessonContentType = c.Int(nullable: false),
                        ModuleLessonId = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CourseModule_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentQuiz", t => t.ContentQuizId)
                .ForeignKey("dbo.User", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.ModuleLesson", t => t.ModuleLessonId, cascadeDelete: true)
                .ForeignKey("dbo.Organization", t => t.OrganizationId, cascadeDelete: true)
                .ForeignKey("dbo.CourseModule", t => t.CourseModule_Id)
                .Index(t => t.ContentQuizId)
                .Index(t => t.ModuleLessonId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById)
                .Index(t => t.CourseModule_Id);
            
            CreateTable(
                "dbo.ContentQuiz",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LessonContentId = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.LessonContent", t => t.LessonContentId)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .Index(t => t.LessonContentId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.ContentQuizQuestion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        AnswerType = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        ContentQuizId = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentQuiz", t => t.ContentQuizId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.ContentQuizId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.ContentQuestionOption",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentQuizId = c.Int(nullable: false),
                        Title = c.String(),
                        Value = c.String(),
                        IsAnswer = c.Boolean(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentQuizQuestion", t => t.ContentQuizId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .Index(t => t.ContentQuizId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.ModuleLesson",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModuleId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.CourseModule", t => t.ModuleId)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .Index(t => t.ModuleId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.ExaminationQuestion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        AnswerType = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        ExaminationId = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Examination_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.Examination", t => t.ExaminationId)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .ForeignKey("dbo.Examination", t => t.Examination_Id)
                .Index(t => t.ExaminationId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById)
                .Index(t => t.Examination_Id);
            
            CreateTable(
                "dbo.ExaminationQuestionOption",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExaminationQuizId = c.Int(nullable: false),
                        Title = c.String(),
                        Value = c.String(),
                        IsAnswer = c.Boolean(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.ExaminationQuestion", t => t.ExaminationQuizId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.ExaminationQuizId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.ExaminationAttempt",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ExaminationId = c.Int(nullable: false),
                        AttemptCount = c.Int(nullable: false),
                        IsPassed = c.Boolean(),
                        Score = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.Examination", t => t.ExaminationId)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ExaminationId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.ExaminationQuizResponse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExaminationAttempttId = c.Int(nullable: false),
                        ExaminationQuestionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Value = c.String(),
                        IsAnswer = c.Boolean(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ExaminationAttempt_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.ExaminationAttempt", t => t.ExaminationAttempttId)
                .ForeignKey("dbo.ExaminationQuestion", t => t.ExaminationQuestionId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.ExaminationAttempt", t => t.ExaminationAttempt_Id)
                .Index(t => t.ExaminationAttempttId)
                .Index(t => t.ExaminationQuestionId)
                .Index(t => t.UserId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById)
                .Index(t => t.ExaminationAttempt_Id);
            
            CreateTable(
                "dbo.ModuleProgress",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ModuleId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.CourseModule", t => t.ModuleId)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ModuleId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.Survey",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CourseId = c.Int(nullable: false),
                        ExaminationId = c.Int(nullable: false),
                        TrainingId = c.Int(nullable: false),
                        SurveyType = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.Examination", t => t.ExaminationId)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .ForeignKey("dbo.Training", t => t.TrainingId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.ExaminationId)
                .Index(t => t.TrainingId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.Training",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Location = c.String(),
                        TrainingType = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        StartPeriod = c.DateTime(),
                        EndPeriod = c.DateTime(),
                        DurationInMinutes = c.Double(),
                        IsActive = c.Boolean(nullable: false),
                        AmountPerStaff = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BudgetExpended = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CronExpression = c.String(),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.SurveyQuestion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        AnswerType = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        SurveyId = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .ForeignKey("dbo.Survey", t => t.SurveyId)
                .Index(t => t.SurveyId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.SurveyQuestionOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyQuestionId = c.Int(nullable: false),
                        Title = c.String(),
                        Value = c.String(),
                        IsAnswer = c.Boolean(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId, cascadeDelete: true)
                .ForeignKey("dbo.SurveyQuestion", t => t.SurveyQuestionId, cascadeDelete: true)
                .Index(t => t.SurveyQuestionId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.SurveyQuestionResponse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyQuestionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Value = c.String(),
                        IsAnswer = c.Boolean(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .ForeignKey("dbo.SurveyQuestion", t => t.SurveyQuestionId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.SurveyQuestionId)
                .Index(t => t.UserId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.UserTraining",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TrainingId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        ActionById = c.Int(nullable: false),
                        ActionDate = c.DateTime(),
                        Availability = c.Int(nullable: false),
                        Remarks = c.String(),
                        Year = c.Int(nullable: false),
                        StartPeriod = c.DateTime(),
                        EndPeriod = c.DateTime(),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ActionById, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .ForeignKey("dbo.Training", t => t.TrainingId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TrainingId)
                .Index(t => t.ActionById)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.UserCourse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        CertificateId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        Availability = c.Int(nullable: false),
                        CourseStatus = c.Int(nullable: false),
                        CurrentModuleNumber = c.Int(nullable: false),
                        CurrentLessonNumber = c.Int(nullable: false),
                        ModuleProgressId = c.Int(nullable: false),
                        IsPassed = c.Boolean(nullable: false),
                        CertificateDownloaded = c.Boolean(nullable: false),
                        CertificateDate = c.DateTime(),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.ModuleProgress", t => t.ModuleProgressId)
                .ForeignKey("dbo.Organization", t => t.OrganizationId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CourseId)
                .Index(t => t.ModuleProgressId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.LearningGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.ContentQuizResponse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuizId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Value = c.String(),
                        IsAnswer = c.Boolean(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId, cascadeDelete: true)
                .ForeignKey("dbo.ContentQuizQuestion", t => t.QuizId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.QuizId)
                .Index(t => t.UserId)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.Bulletin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.Advert",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        FileUrl = c.String(),
                        StartDate = c.DateTime(),
                        Section = c.Int(nullable: false),
                        EndDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        LastModifiedById = c.Int(),
                        ModifiedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.LastModifiedById)
                .ForeignKey("dbo.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Advert", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.Advert", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.Advert", "CreatedById", "dbo.User");
            DropForeignKey("dbo.Bulletin", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.Bulletin", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.Bulletin", "CreatedById", "dbo.User");
            DropForeignKey("dbo.ContentQuizResponse", "UserId", "dbo.User");
            DropForeignKey("dbo.ContentQuizResponse", "QuizId", "dbo.ContentQuizQuestion");
            DropForeignKey("dbo.ContentQuizResponse", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.ContentQuizResponse", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.ContentQuizResponse", "CreatedById", "dbo.User");
            DropForeignKey("dbo.LearningGroup", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.LearningGroup", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.LearningGroup", "CreatedById", "dbo.User");
            DropForeignKey("dbo.UserCourse", "UserId", "dbo.User");
            DropForeignKey("dbo.UserCourse", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.UserCourse", "ModuleProgressId", "dbo.ModuleProgress");
            DropForeignKey("dbo.UserCourse", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.UserCourse", "CreatedById", "dbo.User");
            DropForeignKey("dbo.UserCourse", "CourseId", "dbo.Course");
            DropForeignKey("dbo.UserTraining", "UserId", "dbo.User");
            DropForeignKey("dbo.UserTraining", "TrainingId", "dbo.Training");
            DropForeignKey("dbo.UserTraining", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.UserTraining", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.UserTraining", "CreatedById", "dbo.User");
            DropForeignKey("dbo.UserTraining", "ActionById", "dbo.User");
            DropForeignKey("dbo.SurveyQuestionResponse", "UserId", "dbo.User");
            DropForeignKey("dbo.SurveyQuestionResponse", "SurveyQuestionId", "dbo.SurveyQuestion");
            DropForeignKey("dbo.SurveyQuestionResponse", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.SurveyQuestionResponse", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.SurveyQuestionResponse", "CreatedById", "dbo.User");
            DropForeignKey("dbo.SurveyQuestion", "SurveyId", "dbo.Survey");
            DropForeignKey("dbo.SurveyQuestion", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.SurveyQuestionOptions", "SurveyQuestionId", "dbo.SurveyQuestion");
            DropForeignKey("dbo.SurveyQuestionOptions", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.SurveyQuestionOptions", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.SurveyQuestionOptions", "CreatedById", "dbo.User");
            DropForeignKey("dbo.SurveyQuestion", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.SurveyQuestion", "CreatedById", "dbo.User");
            DropForeignKey("dbo.Survey", "TrainingId", "dbo.Training");
            DropForeignKey("dbo.Training", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.Training", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.Training", "CreatedById", "dbo.User");
            DropForeignKey("dbo.Survey", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.Survey", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.Survey", "ExaminationId", "dbo.Examination");
            DropForeignKey("dbo.Survey", "CreatedById", "dbo.User");
            DropForeignKey("dbo.Survey", "CourseId", "dbo.Course");
            DropForeignKey("dbo.Notification", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.Notification", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.Notification", "CreatedById", "dbo.User");
            DropForeignKey("dbo.ModuleProgress", "UserId", "dbo.User");
            DropForeignKey("dbo.ModuleProgress", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.ModuleProgress", "ModuleId", "dbo.CourseModule");
            DropForeignKey("dbo.ModuleProgress", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.ModuleProgress", "CreatedById", "dbo.User");
            DropForeignKey("dbo.ExaminationAttempt", "UserId", "dbo.User");
            DropForeignKey("dbo.ExaminationQuizResponse", "ExaminationAttempt_Id", "dbo.ExaminationAttempt");
            DropForeignKey("dbo.ExaminationQuizResponse", "UserId", "dbo.User");
            DropForeignKey("dbo.ExaminationQuizResponse", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.ExaminationQuizResponse", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.ExaminationQuizResponse", "ExaminationQuestionId", "dbo.ExaminationQuestion");
            DropForeignKey("dbo.ExaminationQuizResponse", "ExaminationAttempttId", "dbo.ExaminationAttempt");
            DropForeignKey("dbo.ExaminationQuizResponse", "CreatedById", "dbo.User");
            DropForeignKey("dbo.ExaminationAttempt", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.ExaminationAttempt", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.ExaminationAttempt", "ExaminationId", "dbo.Examination");
            DropForeignKey("dbo.ExaminationAttempt", "CreatedById", "dbo.User");
            DropForeignKey("dbo.Examination", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.Examination", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.ExaminationQuestion", "Examination_Id", "dbo.Examination");
            DropForeignKey("dbo.ExaminationQuestion", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.ExaminationQuestionOption", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.ExaminationQuestionOption", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.ExaminationQuestionOption", "ExaminationQuizId", "dbo.ExaminationQuestion");
            DropForeignKey("dbo.ExaminationQuestionOption", "CreatedById", "dbo.User");
            DropForeignKey("dbo.ExaminationQuestion", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.ExaminationQuestion", "ExaminationId", "dbo.Examination");
            DropForeignKey("dbo.ExaminationQuestion", "CreatedById", "dbo.User");
            DropForeignKey("dbo.Examination", "CreatedById", "dbo.User");
            DropForeignKey("dbo.Course", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.CourseModule", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.LessonContent", "CourseModule_Id", "dbo.CourseModule");
            DropForeignKey("dbo.LessonContent", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.ModuleLesson", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.ModuleLesson", "ModuleId", "dbo.CourseModule");
            DropForeignKey("dbo.ModuleLesson", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.ModuleLesson", "CreatedById", "dbo.User");
            DropForeignKey("dbo.LessonContent", "ModuleLessonId", "dbo.ModuleLesson");
            DropForeignKey("dbo.LessonContent", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.LessonContent", "CreatedById", "dbo.User");
            DropForeignKey("dbo.LessonContent", "ContentQuizId", "dbo.ContentQuiz");
            DropForeignKey("dbo.ContentQuizQuestion", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.ContentQuestionOption", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.ContentQuestionOption", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.ContentQuestionOption", "CreatedById", "dbo.User");
            DropForeignKey("dbo.ContentQuestionOption", "ContentQuizId", "dbo.ContentQuizQuestion");
            DropForeignKey("dbo.ContentQuizQuestion", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.ContentQuizQuestion", "CreatedById", "dbo.User");
            DropForeignKey("dbo.ContentQuizQuestion", "ContentQuizId", "dbo.ContentQuiz");
            DropForeignKey("dbo.ContentQuiz", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.ContentQuiz", "LessonContentId", "dbo.LessonContent");
            DropForeignKey("dbo.ContentQuiz", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.ContentQuiz", "CreatedById", "dbo.User");
            DropForeignKey("dbo.CourseModule", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.CourseModule", "CreatedById", "dbo.User");
            DropForeignKey("dbo.CourseModule", "CourseId", "dbo.Course");
            DropForeignKey("dbo.LearningArea", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.LearningArea", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.LearningArea", "CreatedById", "dbo.User");
            DropForeignKey("dbo.Course", "LearningAreaId", "dbo.LearningArea");
            DropForeignKey("dbo.Course", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.Examination", "CourseId", "dbo.Course");
            DropForeignKey("dbo.Course", "CreatedById", "dbo.User");
            DropForeignKey("dbo.Course", "CertificateId", "dbo.Certificate");
            DropForeignKey("dbo.Certificate", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.Certificate", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.Certificate", "CreatedById", "dbo.User");
            DropForeignKey("dbo.Certificate", "CourseId", "dbo.Course");
            DropForeignKey("dbo.Department", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.Department", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.Department", "CreatedById", "dbo.User");
            DropForeignKey("dbo.Department", "BudgetId", "dbo.DepartmentBudget");
            DropForeignKey("dbo.DepartmentBudget", "OrganizationId", "dbo.Organization");
            DropForeignKey("dbo.Organization", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.Organization", "CreatedById", "dbo.User");
            DropForeignKey("dbo.DepartmentBudget", "LastModifiedById", "dbo.User");
            DropForeignKey("dbo.DepartmentBudget", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.DepartmentBudget", "CreatedById", "dbo.User");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.Advert", new[] { "LastModifiedById" });
            DropIndex("dbo.Advert", new[] { "CreatedById" });
            DropIndex("dbo.Advert", new[] { "OrganizationId" });
            DropIndex("dbo.Bulletin", new[] { "LastModifiedById" });
            DropIndex("dbo.Bulletin", new[] { "CreatedById" });
            DropIndex("dbo.Bulletin", new[] { "OrganizationId" });
            DropIndex("dbo.ContentQuizResponse", new[] { "LastModifiedById" });
            DropIndex("dbo.ContentQuizResponse", new[] { "CreatedById" });
            DropIndex("dbo.ContentQuizResponse", new[] { "OrganizationId" });
            DropIndex("dbo.ContentQuizResponse", new[] { "UserId" });
            DropIndex("dbo.ContentQuizResponse", new[] { "QuizId" });
            DropIndex("dbo.LearningGroup", new[] { "LastModifiedById" });
            DropIndex("dbo.LearningGroup", new[] { "CreatedById" });
            DropIndex("dbo.LearningGroup", new[] { "OrganizationId" });
            DropIndex("dbo.UserCourse", new[] { "LastModifiedById" });
            DropIndex("dbo.UserCourse", new[] { "CreatedById" });
            DropIndex("dbo.UserCourse", new[] { "OrganizationId" });
            DropIndex("dbo.UserCourse", new[] { "ModuleProgressId" });
            DropIndex("dbo.UserCourse", new[] { "CourseId" });
            DropIndex("dbo.UserCourse", new[] { "UserId" });
            DropIndex("dbo.UserTraining", new[] { "LastModifiedById" });
            DropIndex("dbo.UserTraining", new[] { "CreatedById" });
            DropIndex("dbo.UserTraining", new[] { "OrganizationId" });
            DropIndex("dbo.UserTraining", new[] { "ActionById" });
            DropIndex("dbo.UserTraining", new[] { "TrainingId" });
            DropIndex("dbo.UserTraining", new[] { "UserId" });
            DropIndex("dbo.SurveyQuestionResponse", new[] { "LastModifiedById" });
            DropIndex("dbo.SurveyQuestionResponse", new[] { "CreatedById" });
            DropIndex("dbo.SurveyQuestionResponse", new[] { "OrganizationId" });
            DropIndex("dbo.SurveyQuestionResponse", new[] { "UserId" });
            DropIndex("dbo.SurveyQuestionResponse", new[] { "SurveyQuestionId" });
            DropIndex("dbo.SurveyQuestionOptions", new[] { "LastModifiedById" });
            DropIndex("dbo.SurveyQuestionOptions", new[] { "CreatedById" });
            DropIndex("dbo.SurveyQuestionOptions", new[] { "OrganizationId" });
            DropIndex("dbo.SurveyQuestionOptions", new[] { "SurveyQuestionId" });
            DropIndex("dbo.SurveyQuestion", new[] { "LastModifiedById" });
            DropIndex("dbo.SurveyQuestion", new[] { "CreatedById" });
            DropIndex("dbo.SurveyQuestion", new[] { "OrganizationId" });
            DropIndex("dbo.SurveyQuestion", new[] { "SurveyId" });
            DropIndex("dbo.Training", new[] { "LastModifiedById" });
            DropIndex("dbo.Training", new[] { "CreatedById" });
            DropIndex("dbo.Training", new[] { "OrganizationId" });
            DropIndex("dbo.Survey", new[] { "LastModifiedById" });
            DropIndex("dbo.Survey", new[] { "CreatedById" });
            DropIndex("dbo.Survey", new[] { "OrganizationId" });
            DropIndex("dbo.Survey", new[] { "TrainingId" });
            DropIndex("dbo.Survey", new[] { "ExaminationId" });
            DropIndex("dbo.Survey", new[] { "CourseId" });
            DropIndex("dbo.Notification", new[] { "LastModifiedById" });
            DropIndex("dbo.Notification", new[] { "CreatedById" });
            DropIndex("dbo.Notification", new[] { "OrganizationId" });
            DropIndex("dbo.ModuleProgress", new[] { "LastModifiedById" });
            DropIndex("dbo.ModuleProgress", new[] { "CreatedById" });
            DropIndex("dbo.ModuleProgress", new[] { "OrganizationId" });
            DropIndex("dbo.ModuleProgress", new[] { "ModuleId" });
            DropIndex("dbo.ModuleProgress", new[] { "UserId" });
            DropIndex("dbo.ExaminationQuizResponse", new[] { "ExaminationAttempt_Id" });
            DropIndex("dbo.ExaminationQuizResponse", new[] { "LastModifiedById" });
            DropIndex("dbo.ExaminationQuizResponse", new[] { "CreatedById" });
            DropIndex("dbo.ExaminationQuizResponse", new[] { "OrganizationId" });
            DropIndex("dbo.ExaminationQuizResponse", new[] { "UserId" });
            DropIndex("dbo.ExaminationQuizResponse", new[] { "ExaminationQuestionId" });
            DropIndex("dbo.ExaminationQuizResponse", new[] { "ExaminationAttempttId" });
            DropIndex("dbo.ExaminationAttempt", new[] { "LastModifiedById" });
            DropIndex("dbo.ExaminationAttempt", new[] { "CreatedById" });
            DropIndex("dbo.ExaminationAttempt", new[] { "OrganizationId" });
            DropIndex("dbo.ExaminationAttempt", new[] { "ExaminationId" });
            DropIndex("dbo.ExaminationAttempt", new[] { "UserId" });
            DropIndex("dbo.ExaminationQuestionOption", new[] { "LastModifiedById" });
            DropIndex("dbo.ExaminationQuestionOption", new[] { "CreatedById" });
            DropIndex("dbo.ExaminationQuestionOption", new[] { "OrganizationId" });
            DropIndex("dbo.ExaminationQuestionOption", new[] { "ExaminationQuizId" });
            DropIndex("dbo.ExaminationQuestion", new[] { "Examination_Id" });
            DropIndex("dbo.ExaminationQuestion", new[] { "LastModifiedById" });
            DropIndex("dbo.ExaminationQuestion", new[] { "CreatedById" });
            DropIndex("dbo.ExaminationQuestion", new[] { "OrganizationId" });
            DropIndex("dbo.ExaminationQuestion", new[] { "ExaminationId" });
            DropIndex("dbo.ModuleLesson", new[] { "LastModifiedById" });
            DropIndex("dbo.ModuleLesson", new[] { "CreatedById" });
            DropIndex("dbo.ModuleLesson", new[] { "OrganizationId" });
            DropIndex("dbo.ModuleLesson", new[] { "ModuleId" });
            DropIndex("dbo.ContentQuestionOption", new[] { "LastModifiedById" });
            DropIndex("dbo.ContentQuestionOption", new[] { "CreatedById" });
            DropIndex("dbo.ContentQuestionOption", new[] { "OrganizationId" });
            DropIndex("dbo.ContentQuestionOption", new[] { "ContentQuizId" });
            DropIndex("dbo.ContentQuizQuestion", new[] { "LastModifiedById" });
            DropIndex("dbo.ContentQuizQuestion", new[] { "CreatedById" });
            DropIndex("dbo.ContentQuizQuestion", new[] { "OrganizationId" });
            DropIndex("dbo.ContentQuizQuestion", new[] { "ContentQuizId" });
            DropIndex("dbo.ContentQuiz", new[] { "LastModifiedById" });
            DropIndex("dbo.ContentQuiz", new[] { "CreatedById" });
            DropIndex("dbo.ContentQuiz", new[] { "OrganizationId" });
            DropIndex("dbo.ContentQuiz", new[] { "LessonContentId" });
            DropIndex("dbo.LessonContent", new[] { "CourseModule_Id" });
            DropIndex("dbo.LessonContent", new[] { "LastModifiedById" });
            DropIndex("dbo.LessonContent", new[] { "CreatedById" });
            DropIndex("dbo.LessonContent", new[] { "OrganizationId" });
            DropIndex("dbo.LessonContent", new[] { "ModuleLessonId" });
            DropIndex("dbo.LessonContent", new[] { "ContentQuizId" });
            DropIndex("dbo.CourseModule", new[] { "LastModifiedById" });
            DropIndex("dbo.CourseModule", new[] { "CreatedById" });
            DropIndex("dbo.CourseModule", new[] { "OrganizationId" });
            DropIndex("dbo.CourseModule", new[] { "CourseId" });
            DropIndex("dbo.LearningArea", new[] { "LastModifiedById" });
            DropIndex("dbo.LearningArea", new[] { "CreatedById" });
            DropIndex("dbo.LearningArea", new[] { "OrganizationId" });
            DropIndex("dbo.Certificate", new[] { "LastModifiedById" });
            DropIndex("dbo.Certificate", new[] { "CreatedById" });
            DropIndex("dbo.Certificate", new[] { "OrganizationId" });
            DropIndex("dbo.Certificate", new[] { "CourseId" });
            DropIndex("dbo.Course", new[] { "LastModifiedById" });
            DropIndex("dbo.Course", new[] { "CreatedById" });
            DropIndex("dbo.Course", new[] { "OrganizationId" });
            DropIndex("dbo.Course", new[] { "CertificateId" });
            DropIndex("dbo.Course", new[] { "LearningAreaId" });
            DropIndex("dbo.Examination", new[] { "LastModifiedById" });
            DropIndex("dbo.Examination", new[] { "CreatedById" });
            DropIndex("dbo.Examination", new[] { "OrganizationId" });
            DropIndex("dbo.Examination", new[] { "CourseId" });
            DropIndex("dbo.Organization", new[] { "LastModifiedById" });
            DropIndex("dbo.Organization", new[] { "CreatedById" });
            DropIndex("dbo.DepartmentBudget", new[] { "LastModifiedById" });
            DropIndex("dbo.DepartmentBudget", new[] { "CreatedById" });
            DropIndex("dbo.DepartmentBudget", new[] { "OrganizationId" });
            DropIndex("dbo.DepartmentBudget", new[] { "DepartmentId" });
            DropIndex("dbo.Department", new[] { "LastModifiedById" });
            DropIndex("dbo.Department", new[] { "CreatedById" });
            DropIndex("dbo.Department", new[] { "OrganizationId" });
            DropIndex("dbo.Department", new[] { "BudgetId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.Advert");
            DropTable("dbo.Bulletin");
            DropTable("dbo.ContentQuizResponse");
            DropTable("dbo.LearningGroup");
            DropTable("dbo.UserCourse");
            DropTable("dbo.UserTraining");
            DropTable("dbo.SurveyQuestionResponse");
            DropTable("dbo.SurveyQuestionOptions");
            DropTable("dbo.SurveyQuestion");
            DropTable("dbo.Training");
            DropTable("dbo.Survey");
            DropTable("dbo.Notification");
            DropTable("dbo.ModuleProgress");
            DropTable("dbo.ExaminationQuizResponse");
            DropTable("dbo.ExaminationAttempt");
            DropTable("dbo.ExaminationQuestionOption");
            DropTable("dbo.ExaminationQuestion");
            DropTable("dbo.ModuleLesson");
            DropTable("dbo.ContentQuestionOption");
            DropTable("dbo.ContentQuizQuestion");
            DropTable("dbo.ContentQuiz");
            DropTable("dbo.LessonContent");
            DropTable("dbo.CourseModule");
            DropTable("dbo.LearningArea");
            DropTable("dbo.Certificate");
            DropTable("dbo.Course");
            DropTable("dbo.Examination");
            DropTable("dbo.ErrorLog");
            DropTable("dbo.Organization");
            DropTable("dbo.User");
            DropTable("dbo.DepartmentBudget");
            DropTable("dbo.Department");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
