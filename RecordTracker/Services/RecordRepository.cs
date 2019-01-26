using LearnWpf.SqliteDataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWpf.Services
{
    public class RecordRepository : IRecordRepository
    {
         //DataLayerContext _context = new DataLayerContext(@"C:\Users\Home\MainApplication.db");
        //DataLayerContext _context = new DataLayerContext();
        DataLayerContext _context = new DataLayerContext(Constants.GetDbFilePath());
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

        public Task<List<SqliteDataLayer.LetterRecord>> GetRecordsAsync(List<long> PSids,List<long> POids,List<long> TAids)
        {



            /*
            IQueryable<SqliteDataLayer.LetterRecord> OffData;
            if (officer.Name.Equals("All"))
            {
                OffData= (from record in _context.LetterRecords where record.PoliceOfficerID != officer.Id select record);
            }
            else
            {
                OffData = (from record in _context.LetterRecords where record.PoliceOfficerID == officer.Id select record);
            }

            IQueryable<SqliteDataLayer.LetterRecord> StatData;
            if (stations.Name.Equals("All"))
            {
                StatData = (from record in _context.LetterRecords where record.PoliceStationID != stations.Id select record);
            }
            else
            {
                StatData = (from record in _context.LetterRecords where record.PoliceStationID == stations.Id select record);
            }

            IQueryable<SqliteDataLayer.LetterRecord> TopicData;
            if (stations.Name.Equals("All"))
            {
                TopicData = (from record in _context.LetterRecords where checktopic(record) select record);
            }
            else
            {
                TopicData = (from record in _context.LetterRecords where record.TopicAreaID == topics.Id select record);
            }
            */
            //All not selected
            //IQueryable<SqliteDataLayer.LetterRecord> AllData;
            //AllData = (from record in _context.LetterRecords where record.StatusID == 1 && record.PoliceStationID == stations.Id
            //           && (record.PoliceOfficerID == officer.Id ) && record.TopicAreaID == topics.Id 
            //    select record);
            //return AllData.ToListAsync();

            //List<long> Ids = new List<long>();
            //Ids.Add(1);
            //Ids.Add(2);
            ////Ids.Any(x=>x.Equals())
            IQueryable<SqliteDataLayer.LetterRecord> AllData;
            AllData = (from record in _context.LetterRecords
                       where record.StatusID == 1 
                       && PSids.Any(x=>x.Equals(record.PoliceStationID))
                       && POids.Any(x=>x.Equals(record.PoliceOfficerID))
                       && TAids.Any(x=>x.Equals(record.TopicAreaID))
                       select record);
          
            return AllData.ToListAsync();

            //if (stations != null)
            //{
            //    dataStation = (from record in _context.LetterRecords where record.PoliceStationID == stations.Id select record);
            //    AllData.Intersect(dataStation);
            //}

            //if (officer != null)
            //{
            //    dataOff = (from record in _context.LetterRecords where record.PoliceOfficerID == officer.Id select record);
            //    AllData.Intersect(dataOff);
            //}

            //if (topics != null)
            //{
            //    dataTopic = (from record in _context.LetterRecords where record.TopicAreaID == topics.Id select record);
            //    AllData.Intersect(dataTopic);

            //}

            //return AllData.ToListAsync();
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

        public static bool checkps(SqliteDataLayer.LetterRecord rec,PoliceStation st)
        {
            return rec.PoliceStationID == st.Id;
        }
       
    }
}
