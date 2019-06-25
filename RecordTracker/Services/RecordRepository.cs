﻿using NLog;
using RecordTracker.SqliteDataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTracker.Services
{
    public class RecordRepository : IRecordRepository
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
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


            IQueryable<SqliteDataLayer.LetterRecord> AllData;
            AllData = (from record in _context.LetterRecords
                       where record.StatusID == 1 
                       && PSids.Any(x=>x.Equals(record.PoliceStationID))
                       && POids.Any(x=>x.Equals(record.PoliceOfficerID))
                       && TAids.Any(x=>x.Equals(record.TopicAreaID))
                       select record);
          
            return AllData.ToListAsync();

           
        }

        public Task<List<SqliteDataLayer.LetterRecord>> GetRecordsAsync1(List<long> PSids, 
            List<long> POids, List<long> TAids,List<long> Srcids,List<long> Subids, List<long> StatusIds)
        {


            IQueryable<SqliteDataLayer.LetterRecord> AllData;
            AllData = (from record in _context.LetterRecords
                       where StatusIds.Any(x=>x.Equals(record.StatusID))
                       && PSids.Any(x => x.Equals(record.PoliceStationID))
                       && POids.Any(x => x.Equals(record.PoliceOfficerID))
                       && TAids.Any(x => x.Equals(record.TopicAreaID))
                       && Srcids.Any(x => x.Equals(record.SourceID))
                       && Subids.Any(x=> x.Equals(record.SubjectID))
                       select record);

            return AllData.ToListAsync();
            /*AllData = (from record in _context.LetterRecords
                       where StatusIds.Any(x => x.Equals(record.StatusID))
                       && PSids.Any(x => x.Equals(record.PoliceStationID))
                       && POids.Any(x => x.Equals(record.PoliceOfficerID))
                       && TAids.Any(x => x.Equals(record.TopicAreaID))
                       && Srcids.Any(x => x.Equals(record.SourceID))
                       && Subids.Any(x => x.Equals(record.SubjectID))
                       select record);
                       */
        }

        public Task<List<SqliteDataLayer.LetterRecord>> GetRecordsAsync(List<long> PSids,
           List<long> POids, List<long> TAids, List<long> Srcids, List<long> Subids, List<long> StatusIds)
        {


            IQueryable<SqliteDataLayer.LetterRecord> AllData;
            AllData = (from record in _context.LetterRecords
                       join source in _context.Sources on record.SourceID equals source.Id
                       join subject in _context.Subjects on record.SubjectID equals subject.Id
                       join status in _context.Status on record.StatusID equals status.Id
                       join policeStation in _context.PoliceStations on record.PoliceStationID equals policeStation.Id
                       join policeOfficer in _context.PoliceOfficers on record.PoliceOfficerID equals policeOfficer.Id
                       join topic in _context.TopicsAndAreas on record.TopicAreaID equals topic.Id
                       where TAids.Contains(topic.Id)
                       where PSids.Contains(policeStation.Id)
                       where POids.Contains(policeOfficer.Id)
                       where Srcids.Contains(source.Id)
                       where Subids.Contains(subject.Id)
                       where StatusIds.Contains(status.Id)
                       select record);
           
            return AllData.ToListAsync();
            
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

        public async Task DeleteRecordsAsync(long id)
        {
            var LetterRecord = _context.LetterRecords.FirstOrDefault(c => c.Id == id);
            if (LetterRecord != null)
            {
                _context.LetterRecords.Remove(LetterRecord);

            }
            await _context.SaveChangesAsync();

        }

    }
}
