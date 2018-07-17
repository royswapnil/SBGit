using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Service;
using SterlingBankLMS.Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SterlingBankLMS.Core.Factories
{
    public class ForumFactory : GenericService<ForumCategory>
    {
        public ForumFactory(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        
        public List<ForumCategory> GetAllCategories()
        {
            return GetContext().Set<ForumCategory>().OrderBy(p => p.CategoryName).ToList();
        }

        public bool AddCategory(string catname, string Desc)
        {            
            try
            {
                ForumCategory newCat = new ForumCategory()
                {
                    CategoryName = catname,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    Description = Desc
                };
                Add(newCat);
                return true;
            }
            catch (Exception ex)
             {
                return false;
            }
        }



    }
}
