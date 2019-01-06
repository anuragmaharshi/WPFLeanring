using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWpf
{
    class InitialiseAndLoadData
    {
        public static List<LetterRecord> GetData()
        {
            List<LetterRecord> Data = new List<LetterRecord>();
            Data.Add(new LetterRecord() {
                PoliceStation = "Simariya",
                TopicArea = "High Court",
                ReciptDate = new DateTime(2018, 10, 5),
                LetterNumber = 1,
                DRNumber = 1,
                DRDate = new DateTime(2018, 10, 9),
                PoliceOfficer = "Abc Singh",
                Remarks = "This case is still open"
               
            });

            Data.Add(new LetterRecord()
            {
                PoliceStation = "Ranchi",
                TopicArea = "RTI",
                ReciptDate = new DateTime(2018, 11, 20),
                LetterNumber = 2,
                DRNumber = 2,
                DRDate = new DateTime(2018, 11, 29),
                PoliceOfficer = "Abc Singh",
                Remarks = "This is super important and need to taken care properly"
                
            });

            Data.Add(new LetterRecord()
            {
                PoliceStation = "Simariya",
                TopicArea = "RTI",
                ReciptDate = new DateTime(2018, 12, 5),
                LetterNumber = 3,
                DRNumber = 3,
                PoliceOfficer = "xyz Singh",
                Remarks = "This case is still open but can be ignored for now./n\n It should come in second line"
               
            });
            return Data;
        }

        public static LetterRecord GetSingleData()
        {
            return new LetterRecord();
        }
    }
}
