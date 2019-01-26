using SqliteDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWpf.Services
{
    public interface IRecordRepository
    {
        Task<SqliteDataLayer.LetterRecord> AddLetterRecordAsync(SqliteDataLayer.LetterRecord letterRecord);

        Task<List<SqliteDataLayer.LetterRecord>> GetRecordsAsync();

        Task<SqliteDataLayer.LetterRecord> UpdateLetterRecordAsync(SqliteDataLayer.LetterRecord record);

        Task<List<SqliteDataLayer.LetterRecord>> GetRecordsAsync(List<long> PSids, List<long> POids, List<long> TAids);
    }
}
