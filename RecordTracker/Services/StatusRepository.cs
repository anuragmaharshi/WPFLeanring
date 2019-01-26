using LearnWpf.SqliteDataLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWpf.Services
{
    public class StatusRepository
    {
      
        //DataLayerContext _context = new DataLayerContext(@"C:\Users\Home\MainApplication.db");
        DataLayerContext _context = new DataLayerContext(Constants.GetDbFilePath());
        //DataLayerContext _context = new DataLayerContext();
        public List<Status> GetStatus()
        {
            return _context.Status.ToList();

        }
    }
}
