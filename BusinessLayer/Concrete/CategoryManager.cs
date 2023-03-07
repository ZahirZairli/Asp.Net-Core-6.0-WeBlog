using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using EntityLayer.DtoChart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public Category GetById(int id)
        {
            return _categoryDal.GetById(id);
        }

        public List<CategoryChart> GetCategoryChart()
        {
            List<CategoryChart> categoryChart = new List<CategoryChart>();
            var blogs = bm.GetListActive();
            List<CategoryChart> categories = (from x in blogs.GroupBy(e => e.CategoryId)
                                              select new CategoryChart
                                              {
                                                  Name = Convert.ToString(x.Key),
                                                  Count = x.Count(),
                                              }).ToList();
            foreach (var item in categories)
            {
                var catid = Convert.ToInt32(item.Name);
                var category = _categoryDal.GetById(catid);
                categoryChart.Add(new CategoryChart()
                {
                    Name = category.CategoryName,
                    Count = item.Count,
                });
            }
            return categoryChart;
        }

        public List<Category> GetList()
        {
            return _categoryDal.GetListAll();
        }

        public List<Category> GetListActive()
        {
            return _categoryDal.GetListAll().FindAll(x => x.CategoryStatus == true);
        }

        public void TAdd(Category t)
        {
            _categoryDal.Insert(t);
        }

        public void TDelete(Category t)
        {
            _categoryDal.Delete(t);
        }

        public void TUpdate(Category t)
        {
            _categoryDal.Update(t);
        }
    }
}
