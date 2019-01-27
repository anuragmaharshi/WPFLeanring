using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTracker.SqliteDataLayer
{
    public class LetterRecord
    {
        public long Id { get; set; }
        public long LetterNumber { get; set; }
        public string ReciptDate { get; set; }
        public long TopicAreaID { get; set; }
        public long PoliceStationID { get; set; }
        public long PoliceOfficerID { get; set; }
        public long DRNumber { get; set; }
        public string DRDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<long> StatusID { get; set; }




        public string FormatReciptDate
        {
            get
            {
                var dty = DateTime.Parse(ReciptDate);
                return dty.ToString("yyyy-MM-dd");
            }
           
        }

        public string FormatDRDate
        {
            get
            {
                var dty = DateTime.Parse(DRDate);
                return dty.ToString("yyyy-MM-dd");
            }

        }

    }
}
