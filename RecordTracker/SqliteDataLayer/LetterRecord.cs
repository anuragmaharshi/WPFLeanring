﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTracker.SqliteDataLayer
{
    public class LetterRecord
    {
        public long Id { get; set; }
        public long StatusID { get; set; }
        public string LetterNumber { get; set; }
        public string OfficeReceiptDate { get; set; }
        public long TopicAreaID { get; set; }
        public long PoliceStationID { get; set; }
        public long PoliceOfficerID { get; set; }
        public string OfficeDispatchNumber { get; set; }
   
        public long SourceID { get; set; }
        public string OfficeDispatchDate { get; set; }
        public string OrganizationName { get; set; }
        public string SanhaDetail { get; set; }
        public string VerificationDetail { get; set; }

        public long SubjectID { get; set; }
        public string PsDispatchNumber { get; set; }
        public string PsDispatchDate { get; set; }
        public string CaseNumber { get; set; }

        public string Remarks { get; set; }

        public string FormatOfficeDispatchDate
        {
            get
            {
                try
                {
                    var dty = DateTime.Parse(OfficeDispatchDate);
                    return dty.ToString("yyyy-MM-dd");
                }
                catch 
                {
                    return " ";
                }
               
            }
           
        }

        public string FormatOfficeReceiptDate
        {
            get
            {
                try
                {
                    var dty = DateTime.Parse(OfficeReceiptDate);
                    return dty.ToString("yyyy-MM-dd");
                }
                catch
                {
                    return " ";
                }
              
            }

        }

        public string FormatPsDispatchDate
        {
            get
            {
                try
                {
                    var dty = DateTime.Parse(PsDispatchDate);
                    return dty.ToString("yyyy-MM-dd");
                }
                catch
                {
                    return " ";
                }
              
            }

        }

        public override string ToString()
        {
            string value = string.Format("letter number {0} , OfficeReceiptDate = {1}, TopicAreaID = {2} ," +
                " OfficeDispatchNumber = {3},FormatPsDispatchDate = {4}",
                LetterNumber, OfficeReceiptDate, TopicAreaID, OfficeDispatchNumber, FormatPsDispatchDate);
            return value;
        }

    }
}
