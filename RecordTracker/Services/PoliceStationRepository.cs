using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using LearnWpf.SqliteDataLayer;

namespace LearnWpf.Services
{
    public class PoliceStationRepository : IPoliceStationRepository
    {
        //DataLayerContext _context = new DataLayerContext(@"C:\Users\Home\MainApplication.db");
        //DataLayerContext _context = new DataLayerContext();
        DataLayerContext _context = new DataLayerContext(Constants.GetDbFilePath());
        public async Task<PoliceStation> AddPoliceStationAsync(PoliceStation policeStation)
        {
            _context.PoliceStations.Add(policeStation);
            await _context.SaveChangesAsync();
            return policeStation;
        }

        public async Task DeletePoliceStationAsync(long id)
        {
            var policeStation = _context.PoliceStations.FirstOrDefault(c => c.Id == id);
            if (policeStation != null)
            {
                _context.PoliceStations.Remove(policeStation);

            }
            await _context.SaveChangesAsync();

        }

        public Task<PoliceStation> GetPoliceStationAsync(long id)
        {
            return _context.PoliceStations.FirstOrDefaultAsync(c => c.Id == id);
        }

        public  Task<List<PoliceStation>> GetPoliceStationsAsync()
        {
            return _context.PoliceStations.ToListAsync();
        }

        public async Task<PoliceStation> UpdatePoliceStationAsync(PoliceStation policeStation)
        {
            if (!_context.PoliceStations.Local.Any(c => c.Id == policeStation.Id))
            {
                _context.PoliceStations.Attach(policeStation);
            }
            _context.Entry(policeStation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return policeStation;
        }
    }
}
