using EntityLayer.Concrete;
using EntityLayer.DtoChart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICategoryService : IGenericService<Category>
    {
        List<CategoryChart> GetCategoryChart();
    }
}
