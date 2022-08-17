using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowClone.DomainModels;

namespace StackOverflowClone.Repositories
{
    public interface ICategoriesrepositories
    {
        void InsertCategory(Category cat);
        void UpdateCategory(Category cat);
        void DeleteCategory(Category cat);
        List<Category> GetAllCategories();
        List<Category> GetCategoryByCategoryID(int CategoryID);

    }

    public class CategoriesRepository : ICategoriesrepositories
    {

        StackOverflowDbContext db;

        public CategoriesRepository()
        {
            db = new StackOverflowDbContext();
        }

        public void DeleteCategory(Category cat)
        {
            Category existingCategory = db.Categories.Where(temp => temp.CategoryID == cat.CategoryID).FirstOrDefault();
            if (existingCategory != null)
            {
                db.Categories.Remove(existingCategory);

                db.SaveChanges();
            }
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = db.Categories.ToList();
            return categories;
        }

        public List<Category> GetCategoryByCategoryID(int CategoryID)
        {
            List<Category> categoriesByID = db.Categories.Where(temp => temp.CategoryID == CategoryID).ToList();
            return categoriesByID;
        }

        public void InsertCategory(Category cat)
        {
            db.Categories.Add(cat);
            db.SaveChanges();
        }

        public void UpdateCategory(Category cat)
        {
            Category existingCategory = db.Categories.Where(temp => temp.CategoryID == cat.CategoryID).FirstOrDefault();
            if (existingCategory != null)
            {
                existingCategory.CategoryName = cat.CategoryName;
                db.SaveChanges();
            }
        }
    }
}
