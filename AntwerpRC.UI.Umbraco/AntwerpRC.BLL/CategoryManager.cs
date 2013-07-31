using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntwerpRC.BDO;
using AntwerpRC.DAL;

namespace AntwerpRC.BLL
{
    public class CategoryManager
    {
        public Category GetCategoryById(long id)
        {
            try
            {
                using (var context = new CalendarEntities())
                {
                    return (BDO.Category) context.Categories.FirstOrDefault(c => !c.AuditDeleted && c.CategoryId == id);
                }
            }
            catch (Exception ex)
            {
//                Log.Error("Error fetching Category from database", ex);
                throw;
            }
            return null;
        }

        public List<Category> GetAllCategories()
        {
            try
            {
                var bdoCategories = new List<BDO.Category>();
                using (var context = new CalendarEntities())
                {
                    var dalCategories = context.Categories.Where(c => !c.AuditDeleted).ToList();
                    dalCategories.ForEach(c=>bdoCategories.Add((BDO.Category)c));
                }
                return bdoCategories;
            }
            catch (Exception ex)
            {
//                Log.Error("Error fetching all categories from the database", ex);
                throw;
            }
            return null;
        }
    }
}
