using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqliteDataLayer;

namespace LearnWpf.Services
{
    public class AddNewRecordRepository : IAddNewRecordRepository
    {
         DataLayerContext _context = new DataLayerContext(@"C:\Users\Home\MainApplication.db");
        //DataLayerContext _context = new DataLayerContext();
        public async Task<SqliteDataLayer.LetterRecord> AddLetterRecordAsync(SqliteDataLayer.LetterRecord letterRecord)
        {
            _context.LetterRecords.Add(letterRecord);
     
            await _context.SaveChangesAsync();
            return letterRecord;
        }
    }
}
