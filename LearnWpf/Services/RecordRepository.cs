using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqliteDataLayer;

namespace LearnWpf.Services
{
    public class RecordRepository : IRecordRepository
    {
         DataLayerContext _context = new DataLayerContext(@"C:\Users\Home\MainApplication.db");
        //DataLayerContext _context = new DataLayerContext();
        public async Task<SqliteDataLayer.LetterRecord> AddLetterRecordAsync(SqliteDataLayer.LetterRecord letterRecord)
        {
            _context.LetterRecords.Add(letterRecord);
     
            await _context.SaveChangesAsync();
            return letterRecord;
        }

        public Task<List<SqliteDataLayer.LetterRecord>> GetRecordsAsync()
        {
            //return _context.LetterRecords.ToListAsync();
            var records = (from record in _context.LetterRecords where record.StatusID==1 select record ).ToListAsync();
            return records;

        }

        public List<SqliteDataLayer.LetterRecord> GetRecordsAsync(PoliceOfficer officer,PoliceStation stations,TopicsAndArea topics)
        {
            var records = (from record in _context.LetterRecords select record).ToList();
            return records;
            //_context.LetterRecords.Select(r => (r.PoliceStationID == stations.Id) && (r.PoliceOfficerID == officer.Id) && (r.TopicAreaID == topics.Id) && (r.StatusID == 1));
        }

        public async Task<SqliteDataLayer.LetterRecord> UpdateLetterRecordAsync(SqliteDataLayer.LetterRecord record)
        {
            if (!_context.LetterRecords.Local.Any(c => c.Id == record.Id))
            {
                _context.LetterRecords.Attach(record);
            }
            _context.Entry(record).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return record;
        }
    }
}
