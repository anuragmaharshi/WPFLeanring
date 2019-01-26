
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

        #endregion

        public void LoadData()
        {
            var POList = POrepo.GetPoliceOfficersAsync().Result.ToList();
            ObservableCollection<PoliceOfficer> POData = new ObservableCollection<PoliceOfficer>();
            foreach (var item in POList)
                POData.Add(item);
            PoliceOfficers = POData;

            var PSList = PSrepo.GetPoliceStationsAsync().Result.ToList();
            ObservableCollection<PoliceStation> PSData = new ObservableCollection<PoliceStation>();
            foreach (var item in PSList)
                PSData.Add(item);
            PoliceStations = PSData;

            var TAList = TArepo.GetTopicAndAreasAsync().Result.ToList();
            ObservableCollection<TopicsAndArea> TAData = new ObservableCollection<TopicsAndArea>();
            foreach (var item in TAList)
                TAData.Add(item);
            TopicsAndAreas = TAData;

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
            var RecList = RecRepo.GetRecordsAsync().Result.ToList();
            ObservableCollection<SqliteDataLayer.LetterRecord> RecData = new ObservableCollection<SqliteDataLayer.LetterRecord>();
            foreach (var item in RecList)
                RecData.Add(item);
            ReportRecords = RecData;
        }

    }
}
