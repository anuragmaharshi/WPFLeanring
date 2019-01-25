using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqliteDataLayer;

namespace LearnWpf.Services
{
    public interface IPoliceStationRepository
    {
        Task<List<PoliceStation>> GetPoliceStationsAsync();
        Task<PoliceStation> GetPoliceStationAsync(long id);
        Task<PoliceStation> AddPoliceStationAsync(PoliceStation policeStation);
        Task<PoliceStation> UpdatePoliceStationAsync(PoliceStation policeStation);
        Task DeletePoliceStationAsync(long Id);
    }
}
