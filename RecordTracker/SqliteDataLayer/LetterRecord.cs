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
        public Nullable<long> OfficeDispatchNumber { get; set; }
        public Nullable<long> Source { get; set; }
        public string OfficeDispatchDate { get; set; }
        public string OfficeReceiptDate { get; set; }
        public string OrganizationName { get; set; }
        public string SanhaDetail { get; set; }
        public string VerificationDetail { get; set; }
        public Nullable<long> Subject { get; set; }
        public Nullable<long> PsDispatchNumber { get; set; }
        public string PsDispatchDate { get; set; }
        public long TopicAreaID { get; set; }
        public long PoliceStationID { get; set; }
        public long PoliceOfficerID { get; set; }
        public Nullable<long> StatusID { get; set; }
        public string Remarks { get; set; }




        public string FormatOfficeDispatchDate
        {
            get
            {
                var dty = DateTime.Parse(OfficeDispatchDate);
                return dty.ToString("yyyy-MM-dd");
            }
           
        }

        public string FormatOfficeReceiptDate
        {
            get
            {
                var dty = DateTime.Parse(OfficeReceiptDate);
                return dty.ToString("yyyy-MM-dd");
            }

        }

        public string FormatPsDispatchDate
        {
            get
            {
                var dty = DateTime.Parse(PsDispatchDate);
                return dty.ToString("yyyy-MM-dd");
            }

        }

    }
}
