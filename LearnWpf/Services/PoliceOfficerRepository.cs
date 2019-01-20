using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqliteDataLayer;

namespace LearnWpf.Services
{
    public class PoliceOfficerRepository : IPoliceOfficerRepository
    {
       // DataLayerContext _context = new DataLayerContext(@"C:\Users\Home\MainApplication.db");
        DataLayerContext _context = new DataLayerContext();
        public async Task<PoliceOfficer> AddPoliceOfficerAsync(PoliceOfficer policeOfficer)
        {
            _context.PoliceOfficers.Add(policeOfficer);
            await _context.SaveChangesAsync();
            return policeOfficer;
        }

        public async Task DeletePoliceOfficerAsync(int Id)
        {
            var policeOfficer= _context.PoliceOfficers.FirstOrDefault(c => c.Id == Id);
            if (policeOfficer != null)
            {
                _context.PoliceOfficers.Remove(policeOfficer);

            }
            await _context.SaveChangesAsync();
        }

        public Task<PoliceOfficer> GetPoliceOfficerAsync(int id)
        {
            return _context.PoliceOfficers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<List<PoliceOfficer>> GetPoliceOfficersAsync()
        {
            return _context.PoliceOfficers.ToListAsync();
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
