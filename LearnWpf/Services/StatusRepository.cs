using SqliteDataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWpf.Services
{
    public class StatusRepository
    {
        DataLayerContext _context = new DataLayerContext(@"C:\Users\Home\MainApplication.db");

        public List<Status> GetStatus()
        {
            return _context.Status.ToList();

        }
    }
}
