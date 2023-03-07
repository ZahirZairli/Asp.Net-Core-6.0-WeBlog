using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class BlogManager : IBlogService
    {
        IBlogDal _blogDal;

        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        public List<Blog> GetListWithCategory()
        {
            return _blogDal.GetListWithCategory();
        }

        public Blog GetById(int id)
        {
            return _blogDal.GetById(id);
        }

        public List<Blog> GetList()
        {
            return _blogDal.GetListAll();
        }

		public List<Blog> GetListFilter(int id)
		{
            return _blogDal.ListCondition(x=>x.CategoryId == id);
		}

		public List<Blog> GetListLast()
		{
            return _blogDal.GetLastTwo();
		}

		public List<Blog> GetListWithWriter(int id)
		{
            return _blogDal.ListCondition(x => x.AppUserId == id);
		}

        public void TAdd(Blog t)
        {
            _blogDal.Insert(t);
        }

        public void TDelete(Blog t)
        {
            _blogDal.Delete(t);
        }

        public void TUpdate(Blog t)
        {
            _blogDal.Update(t);
        }

        public List<Blog> GetListActive()
        {
            return _blogDal.ListCondition(x => x.BlogStatus == true);
        }

        public List<Blog> GetListWithCategoryByWriter(int id)
        {
            return _blogDal.GetListWithCategoryByWriter(id);
        }

		public List<Blog> GetListWithSearch(string Search)
		{
            return _blogDal.ListCondition(x => x.BlogContent.Contains(Search) && x.BlogStatus == true);
		}
	}
}
