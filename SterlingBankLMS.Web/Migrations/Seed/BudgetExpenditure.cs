using SterlingBankLMS.Data.Models;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Web.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterlingBankLMS.Web.Migrations.Seed
{
    internal class BudgetExpenditure
    {
        internal static void CreateExpenditure( SterlingBankLmsContext context )
        {
            var training = context.Set<Training>().ToList();
            var region = context.Set<Region>().ToList();
            var budget = context.Set<DepartmentBudget>().ToList();

            var expenditures = new List<TrainingBudgetExpenditure>()
            {
                new TrainingBudgetExpenditure
                {
                    AmountSpent=1200000m,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,12),
                    DepartmentBudgetId=budget[1].Id,
                    RegionId=budget[1].RegionId ?? region[4].Id,
                    GroupId=budget[1].GroupId,
                    IsDeleted=false,
                    NumberOfParticipants=26,
                    OrganizationId=1,
                    TrainingId=training[3].Id
                },
                new TrainingBudgetExpenditure
                {
                    AmountSpent=1206700m,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,01,19),
                    DepartmentBudgetId=budget[4].Id,
                    RegionId=budget[4].RegionId ?? region[4].Id,
                    GroupId=budget[4].GroupId,
                    IsDeleted=false,
                    NumberOfParticipants=33,
                    OrganizationId=1,
                    TrainingId=training[2].Id
                },
                new TrainingBudgetExpenditure
                {
                    AmountSpent=3100000m,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,12),
                    DepartmentBudgetId=budget[0].Id,
                    RegionId=budget[0].RegionId ?? region[4].Id,
                    GroupId=budget[0].GroupId,
                    IsDeleted=false,
                    NumberOfParticipants=59,
                    OrganizationId=1,
                    TrainingId=training[4].Id
                },
                new TrainingBudgetExpenditure
                {
                    AmountSpent=1209450m,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,12),
                    DepartmentBudgetId=budget[0].Id,
                    RegionId=budget[0].RegionId ?? region[4].Id,
                    GroupId=budget[0].GroupId,
                    IsDeleted=false,
                    NumberOfParticipants=34,
                    OrganizationId=1,
                    TrainingId=training[1].Id
                },
                new TrainingBudgetExpenditure
                {
                    AmountSpent=1244000m,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,03,12),
                    DepartmentBudgetId=budget[0].Id,
                    RegionId=budget[0].RegionId ?? region[4].Id,
                    GroupId=budget[0].GroupId,
                    IsDeleted=false,
                    NumberOfParticipants=17,
                    OrganizationId=1,
                    TrainingId=training[4].Id
                },
                new TrainingBudgetExpenditure
                {
                    AmountSpent=1935000m,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,12),
                    DepartmentBudgetId=budget[1].Id,
                    RegionId=budget[1].RegionId ?? region[4].Id,
                    GroupId=budget[1].GroupId,
                    IsDeleted=false,
                    NumberOfParticipants=39,
                    OrganizationId=1,
                    TrainingId=training[0].Id
                },
                new TrainingBudgetExpenditure
                {
                    AmountSpent=2360000m,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,12),
                    DepartmentBudgetId=budget[1].Id,
                    RegionId=budget[1].RegionId ?? region[4].Id,
                    GroupId=budget[1].GroupId,
                    IsDeleted=false,
                    NumberOfParticipants=37,
                    OrganizationId=1,
                    TrainingId=training[3].Id
                },
                new TrainingBudgetExpenditure
                {
                    AmountSpent=5000000m,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,25),
                    DepartmentBudgetId=budget[2].Id,
                    RegionId=budget[2].RegionId ?? region[4].Id,
                    GroupId=budget[2].GroupId,
                    IsDeleted=false,
                    NumberOfParticipants=30,
                    OrganizationId=1,
                    TrainingId=training[3].Id
                },
                new TrainingBudgetExpenditure
                {
                    AmountSpent=2205000m,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,22),
                    DepartmentBudgetId=budget[4].Id,
                    RegionId=budget[4].RegionId ?? region[4].Id,
                    GroupId=budget[4].GroupId,
                    IsDeleted=false,
                    NumberOfParticipants=22,
                    OrganizationId=1,
                    TrainingId=training[0].Id
                },
                new TrainingBudgetExpenditure
                {
                    AmountSpent=16700000m,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,12),
                    DepartmentBudgetId=budget[1].Id,
                    RegionId=budget[1].RegionId ?? region[4].Id,
                    GroupId=budget[1].GroupId,
                    IsDeleted=false,
                    NumberOfParticipants=20,
                    OrganizationId=1,
                    TrainingId=training[1].Id
                },new TrainingBudgetExpenditure
                {
                    AmountSpent=1200000m,
                    CreatedById=1,
                    CreatedDate=new DateTime(2018,02,17),
                    DepartmentBudgetId=budget[2].Id,
                    RegionId=budget[2].RegionId ?? region[4].Id,
                    GroupId=budget[2].GroupId,
                    IsDeleted=false,
                    NumberOfParticipants=23,
                    OrganizationId=1,
                    TrainingId=training[3].Id
                },
            };
            context.Set<TrainingBudgetExpenditure>().AddOrUpdate(x => x.AmountSpent, expenditures.ToArray());
            context.SaveChanges();
        }
    }
}
