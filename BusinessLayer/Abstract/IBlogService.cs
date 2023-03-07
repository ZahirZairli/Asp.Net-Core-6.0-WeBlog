﻿using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IBlogService : IGenericService<Blog>
    {
        List<Blog> GetListWithCategory();
        List<Blog> GetListWithSearch(string Search);
		List<Blog> GetListWithCategoryByWriter(int id);
        List<Blog> GetListWithWriter(int id);
		List<Blog> GetListFilter(int id);
		List<Blog> GetListLast();

	}
}
