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
        Task<PoliceStation> GetPoliceStationAsync(int id);
        Task<PoliceStation> AddPoliceStationAsync(PoliceStation policeStation);
        Task<PoliceStation> UpdatePoliceStationAsync(PoliceStation policeStation);
        Task DeletePoliceStationAsync(int Id);
    }
}
