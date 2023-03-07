using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class WriterManager : IWriterService
    {
        IWriterDal _writerDal;

        public WriterManager(IWriterDal writerDal)
        {
            _writerDal = writerDal;
        }

        public Writer GetById(int id)
        {
            return _writerDal.GetById(id);
        }

        public Writer GetByMailAndPass(string mail, string pass)
        {
            return _writerDal.ListCondition(x => x.WriterMail == mail && x.WriterPassword == pass).FirstOrDefault();
        }

        public List<Writer> GetList()
        {
            return _writerDal.GetListAll();
        }

        public List<Writer> GetListActive()
        {
            return _writerDal.ListCondition(x => x.WriterStatus == true);
        }

		public List<Writer> GetListByMail(string mail)
		{
			return _writerDal.ListCondition(x => x.WriterMail == mail);
		}

        public void TAdd(Writer t)
        {
            _writerDal.Insert(t);
        }

        public void TDelete(Writer t)
        {
            _writerDal.Delete(t);
        }

        public void TUpdate(Writer t)
        {
            _writerDal.Update(t);
        }
    }
}
