using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfMessageRepository : GenericRepository<Message>, IMessageDal
    {
        public List<Message> GetListWithWriterByReceiverId(int id)
        {
            using (var c = new Context())
            {
                return c.Messages.Include(x=>x.UserSenderUser).Where(x=>x.ReceiverUserId == id).ToList();
            }
        }

        public List<Message> GetListWithWriterBySenderId(int id)
        {
            using (var c = new Context())
            {
                return c.Messages.Include(x => x.UserReceiverUser).Where(x => x.SenderUserId == id).ToList();
            }
        }
    }
}
