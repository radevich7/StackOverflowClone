using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowClone.DomainModels;
using StackOverflowClone.ViewModels;
using StackOverflowClone.Repositories;

using AutoMapper;
using AutoMapper.Configuration;


namespace StackOverflowClone.ServiceLayer
{
    public interface ICategoriesService
    {
        void InsertCategory(CategoriesViewModel categoriesViewModel);
        void UpdateCategory(CategoriesViewModel categoriesViewModel);
        void DeleteCategory(CategoriesViewModel categoriesViewModel);
        List<CategoriesViewModel> GetAllCategories();
        CategoriesViewModel GetCategoryByCategoryID(int CategoryID);

    }
    public class CategoriesService : ICategoriesService
    {
        ICategoriesrepositories cr;
        public CategoriesService()
        {
            cr = new CategoriesRepository();
        }

        public void DeleteCategory(CategoriesViewModel categoriesViewModel)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoriesViewModel, Category>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category cat = mapper.Map<CategoriesViewModel, Category>(categoriesViewModel);
            cr.DeleteCategory(cat);
        }

        public List<CategoriesViewModel> GetAllCategories()
        {

            List<Category> categoriesRep = cr.GetAllCategories();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Category, CategoriesViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<CategoriesViewModel> categoriesSer = mapper.Map<List<Category>, List<CategoriesViewModel>>(categoriesRep);
            return categoriesSer;

        }

        public CategoriesViewModel GetCategoryByCategoryID(int CategoryID)
        {
            Category existingCategory = cr.GetCategoryByCategoryID(CategoryID).FirstOrDefault();
            CategoriesViewModel category = null;
            if (existingCategory != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Category, CategoriesViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                category = mapper.Map<Category, CategoriesViewModel>(existingCategory);
            }
            return category;
        }

        public void InsertCategory(CategoriesViewModel categoriesViewModel)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoriesViewModel, Category>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category cat = mapper.Map<CategoriesViewModel, Category>(categoriesViewModel);
            cr.InsertCategory(cat);
        }

        public void UpdateCategory(CategoriesViewModel categoriesViewModel)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoriesViewModel, Category>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category cat = mapper.Map<CategoriesViewModel, Category>(categoriesViewModel);
            cr.UpdateCategory(cat);
        }
    }
}
