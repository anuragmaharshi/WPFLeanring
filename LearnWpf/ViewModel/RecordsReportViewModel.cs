
using GalaSoft.MvvmLight;
using LearnWpf.Services;
using SqliteDataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWpf.ViewModel
{
    public class RecordsReportViewModel : ViewModelBase
    {
        #region variable declaration
        IPoliceOfficerRepository POrepo;
        ObservableCollection<PoliceOfficer> _policeOfficers;

        IPoliceStationRepository PSrepo;
        ObservableCollection<PoliceStation> _PoliceStations;

        ITopicAndAreaRepository TArepo;
        ObservableCollection<TopicsAndArea> _topicsAndAreas;

        IRecordRepository RecRepo;
        ObservableCollection<SqliteDataLayer.LetterRecord> _reportRecords;

        StatusRepository StatusRepo;
        ObservableCollection<Status> _statusS;

        SqliteDataLayer.LetterRecord _selectedRecord;

        PoliceOfficer _selectedPoliceOfficer;
        PoliceStation _selectedPoliceStation;
        TopicsAndArea _selectedTopic;

        public RelayCommand SaveRecord { get; private set; }

        public RelayCommand SearchRecord { get; private set; }
        #endregion

        #region Constructor
        public RecordsReportViewModel()
        {
            if (!ViewModelBase.IsInDesignModeStatic)
            {

                POrepo = new PoliceOfficerRepository();
                PSrepo = new PoliceStationRepository();
                TArepo = new TopicAndAreaRepository();
                RecRepo = new RecordRepository();
                StatusRepo = new StatusRepository();

                SaveRecord = new RelayCommand(OnSave, canSave);
                SearchRecord = new RelayCommand(onSearch, canSearch);
            }
        }

       



        #endregion

        #region Observable collections
        public ObservableCollection<PoliceOfficer> PoliceOfficers
        {
            get { return _policeOfficers; }
            set { _policeOfficers = value; RaisePropertyChanged("PoliceOfficers"); }

        }
        public ObservableCollection<PoliceStation> PoliceStations
        {
            get { return _PoliceStations; }
            set { _PoliceStations = value; RaisePropertyChanged("PoliceStations"); }

        }

        public ObservableCollection<TopicsAndArea> TopicsAndAreas
        {
            get { return _topicsAndAreas; }
            set { _topicsAndAreas = value; RaisePropertyChanged("TopicsAndAreas"); }

        }

        public ObservableCollection<Status> Statuses
        {
            get { return _statusS; }
            set { _statusS = value; RaisePropertyChanged("Statuses"); }
        }

        public ObservableCollection<SqliteDataLayer.LetterRecord> ReportRecords
        {
            get { return _reportRecords; }
            set { _reportRecords = value; RaisePropertyChanged("ReportRecords"); }

        }


       
        public SqliteDataLayer.LetterRecord SelectedRecord
        {
            get { return _selectedRecord; }
            set
            {
                _selectedRecord = value;

                RaisePropertyChanged("SelectedRecord");
            }
        }

        public PoliceOfficer SelectedPoliceOfficer
        {
            get { return _selectedPoliceOfficer; }
            set
            {
                _selectedPoliceOfficer = value;

                RaisePropertyChanged("SelectedPoliceOfficer");
            }
        }


        public PoliceStation SelectedPoliceStation
        {
            get { return _selectedPoliceStation; }
            set
            {
                _selectedPoliceStation = value;

                RaisePropertyChanged("SelectedPoliceStation");
            }
        }

        public TopicsAndArea SelectedTopic
        {
            get { return _selectedTopic; }
            set
            {
                _selectedTopic = value;

                RaisePropertyChanged("SelectedTopic");
            }
        }
        #endregion

        public void LoadData()
        {
            var POList = POrepo.GetPoliceOfficersAsync().Result.ToList();
            ObservableCollection<PoliceOfficer> POData = new ObservableCollection<PoliceOfficer>();
            foreach (var item in POList)
                POData.Add(item);
            PoliceOfficers = POData;
            SelectedPoliceOfficer = PoliceOfficers.First(x => x.Name.Equals("All"));

            var PSList = PSrepo.GetPoliceStationsAsync().Result.ToList();
            ObservableCollection<PoliceStation> PSData = new ObservableCollection<PoliceStation>();
            foreach (var item in PSList)
                PSData.Add(item);
            PoliceStations = PSData;
            SelectedPoliceStation = PoliceStations.First(x => x.Name.Equals("All"));

            var TAList = TArepo.GetTopicAndAreasAsync().Result.ToList();
            ObservableCollection<TopicsAndArea> TAData = new ObservableCollection<TopicsAndArea>();
            foreach (var item in TAList)
                TAData.Add(item);
            TopicsAndAreas = TAData;
            SelectedTopic = TopicsAndAreas.First(x => x.Name.Equals("All"));
            //var RecList = RecRepo.GetRecordsAsync().Result.ToList();
            //ObservableCollection<SqliteDataLayer.LetterRecord> RecData = new ObservableCollection<SqliteDataLayer.LetterRecord>();
            //foreach (var item in RecList)
            //    RecData.Add(item);
            //ReportRecords = RecData;

            var StatusList = StatusRepo.GetStatus();
            ObservableCollection<Status> StatusData = new ObservableCollection<Status>();
            foreach (var item in StatusList)
                StatusData.Add(item);
            Statuses = StatusData;
        }

        private bool canSave()
        {
            return true;
        }

        private void OnSave()
        {
            RecRepo.UpdateLetterRecordAsync(SelectedRecord);
        }

        private bool canSearch()
        {
            return true;
        }

        private void onSearch()
        {
            List<long> idPO, idPS, idTA;
            idPO = new List<long>();
            idPS = new List<long>();
            idTA = new List<long>();
            List<SqliteDataLayer.LetterRecord> letterRecords;
            if (SelectedPoliceOfficer.Name.Equals("All"))
            {
                foreach(var item in PoliceOfficers)
                {
                    idPO.Add(item.Id);
                }      
            }
            else
            {
                idPO.Add(SelectedPoliceOfficer.Id);
            }

            if (SelectedPoliceStation.Name.Equals("All"))
            {
                foreach (var item in PoliceStations)
                {
                    idPS.Add(item.Id);
                }
            }
            else
            {
                idPS.Add(SelectedPoliceStation.Id);
            }

            if (SelectedTopic.Name.Equals("All"))
            {
                foreach (var item in TopicsAndAreas)
                {
                    idTA.Add(item.Id);
                }
            }
            else
            {
                idTA.Add(SelectedTopic.Id);
            }

            letterRecords = RecRepo.GetRecordsAsync(idPS,idPO,idTA).Result.ToList();
          
            ObservableCollection<SqliteDataLayer.LetterRecord> RecData = new ObservableCollection<SqliteDataLayer.LetterRecord>();
            foreach (var item in letterRecords)
                RecData.Add(item);
            ReportRecords = RecData;

          
        }

    }
}
