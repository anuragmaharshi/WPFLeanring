using SqliteDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWpf.Services
{
    public interface IPoliceOfficerRepository
    {
        Task<List<PoliceOfficer>> GetPoliceOfficersAsync();
        Task<PoliceOfficer> GetPoliceOfficerAsync(int id);
        Task<PoliceOfficer> AddPoliceOfficerAsync(PoliceOfficer policeOfficer);
        Task<PoliceOfficer> UpdatePoliceOfficerAsync(PoliceOfficer policeOfficer);
        Task DeletePoliceOfficerAsync(int Id);
    }
}
