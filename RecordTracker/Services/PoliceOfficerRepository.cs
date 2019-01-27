using NLog;
using RecordTracker.SqliteDataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTracker.Services
{
    public class PoliceOfficerRepository : IPoliceOfficerRepository
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        //DataLayerContext _context = new DataLayerContext(@"C:\Users\Home\MainApplication.db");
        //DataLayerContext _context = new DataLayerContext();
        DataLayerContext _context = new DataLayerContext(Constants.GetDbFilePath());
        public async Task<PoliceOfficer> AddPoliceOfficerAsync(PoliceOfficer policeOfficer) 
        {
            
            _context.PoliceOfficers.Add(policeOfficer);
            await _context.SaveChangesAsync();
            return policeOfficer;
        }

        public async Task DeletePoliceOfficerAsync(long Id)
        {
            var policeOfficer= _context.PoliceOfficers.FirstOrDefault(c => c.Id == Id);
            if (policeOfficer != null)
            {
                _context.PoliceOfficers.Remove(policeOfficer);

            }
            await _context.SaveChangesAsync();
        }

        public Task<PoliceOfficer> GetPoliceOfficerAsync(long id)
        {
            return _context.PoliceOfficers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<List<PoliceOfficer>> GetPoliceOfficersAsync()
        {
            try
            {
                return _context.PoliceOfficers.ToListAsync();
            }
            catch(Exception e)
            {
                _logger.Error("Some error have occured in PoliceOfficerRepository, stacktrace=" + e.StackTrace);
                _logger.Error("PoliceOfficerRepository error message is " + e.Message + " inner error is " + e.InnerException.Message);
                return null;
            }
            
        }

        public async Task<PoliceOfficer> UpdatePoliceOfficerAsync(PoliceOfficer policeOfficer)
        {
            if (!_context.PoliceOfficers.Local.Any(c => c.Id == policeOfficer.Id))
            {
                _context.PoliceOfficers.Attach(policeOfficer);
            }
            _context.Entry(policeOfficer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return policeOfficer;
        }
    }
}
