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
    public class AddRecordViewModel : ViewModelBase
    {
        IPoliceOfficerRepository POrepo;
        ObservableCollection<PoliceOfficer> _policeOfficers;

        IPoliceStationRepository PSrepo;
        ObservableCollection<PoliceStation> _PoliceStations;

        ITopicAndAreaRepository TArepo;
        ObservableCollection<TopicsAndArea> _topicsAndAreas;

        IAddNewRecordRepository AddRepo;
        public RelayCommand AddNewRecord{ get; private set; }
        public RelayCommand CancelRecord { get; private set; }
        public AddRecordViewModel()
        {
            if (!ViewModelBase.IsInDesignModeStatic)
            {

                POrepo = new PoliceOfficerRepository();
                PSrepo = new PoliceStationRepository();
                TArepo = new TopicAndAreaRepository();
                AddRepo = new AddNewRecordRepository();
                AddNewRecord = new RelayCommand(OnAdd,canAdd);
                CancelRecord= new RelayCommand(OnCancel, canCancel);
            }
        }

        


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
        }

        #region Properties
        private string _receiptDate;

        public string ReciptDate
        {
            get { return _receiptDate; }
            set
            {
                _receiptDate = value;
                AddNewRecord.RaiseCanExecuteChanged();
                CancelRecord.RaiseCanExecuteChanged();
                RaisePropertyChanged("ReciptDate");

            }
        }

        private string _letterNumber ;

        public string LetterNumber
        {
            get { return _letterNumber; }
            set {
                //_letterNumber = value;
                if (value != "")
                    _letterNumber = value;
                else
                    _letterNumber = null;
                RaisePropertyChanged("LetterNumber");
                AddNewRecord.RaiseCanExecuteChanged();
                CancelRecord.RaiseCanExecuteChanged();
            }
        }

        private string _dRnumber;

        public string DrNumber
        {
            get { return _dRnumber; }
            set {
                if (value != "")
                    _dRnumber = value;
                else
                    _dRnumber = null;
                //_dRnumber = value;
                RaisePropertyChanged("DrNumber");
                AddNewRecord.RaiseCanExecuteChanged();
                CancelRecord.RaiseCanExecuteChanged();
            }
        }


        private string _drDate;

        public string DRDate
        {
            get { return _drDate; }
            set
            {
              
                _drDate = value;
                RaisePropertyChanged("DRDate");
                CancelRecord.RaiseCanExecuteChanged();
                AddNewRecord.RaiseCanExecuteChanged();
            }
        }


        private PoliceStation _selectedPS;
        public PoliceStation SelectedPS
        {
          get { return _selectedPS; }
          set {
                _selectedPS = value;
                AddNewRecord.RaiseCanExecuteChanged();
                CancelRecord.RaiseCanExecuteChanged();
                RaisePropertyChanged("SelectedPS");
              }
        }
        private TopicsAndArea _selectedTA;
        public TopicsAndArea SelectedTA
        {
            get { return _selectedTA; }
            set {
                _selectedTA = value;
                AddNewRecord.RaiseCanExecuteChanged();
                CancelRecord.RaiseCanExecuteChanged();
                RaisePropertyChanged("SelectedTA");
            }
        }
        private PoliceOfficer _selectedPO;
        public PoliceOfficer SelectedPO
        {
            get { return _selectedPO; }
            set {
                _selectedPO = value;
                AddNewRecord.RaiseCanExecuteChanged();
                CancelRecord.RaiseCanExecuteChanged();
                RaisePropertyChanged("SelectedPO");
            }
        }

        private string _remarks;

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }

        private string _saveText;
        public string SaveText
        {
            get { return _saveText; }
            set { _saveText = value; RaisePropertyChanged("SaveText"); }
        }
        #endregion
        private bool canAdd()
        {
            return SelectedPS!=null && SelectedTA != null && SelectedPO != null 
                && LetterNumber != null && DrNumber!=null && DRDate!=null && ReciptDate!=null;
        }

        private void OnAdd()
        {
            SqliteDataLayer.LetterRecord record = new SqliteDataLayer.LetterRecord();
            record.LetterNumber = long.Parse(LetterNumber);
            record.ReciptDate = ReciptDate;
            record.TopicAreaID = SelectedTA.Id;
            record.PoliceOfficerID = SelectedPO.Id;
            record.PoliceStationID = SelectedPS.Id;
            record.DRNumber = long.Parse(DrNumber);
            record.DRDate = DRDate;
            record.StatusID = 1;
            record.Remarks = Remarks;
            try
            {
                AddRepo.AddLetterRecordAsync(record).Wait();
                SaveText = "Record added successfully.";
            }
            catch
            {
                SaveText = "Unable to add record.";
            }
            ResetUI();
        }

        private bool canCancel()
        {
            return SelectedPS != null || SelectedTA != null || SelectedPO != null 
                || LetterNumber!=null || DrNumber!=null||DRDate!=null || ReciptDate!=null;
        }

        private void OnCancel()
        {
            ResetUI();
            SaveText = null;
        }

        private void ResetUI()
        {
            SelectedPS = null;
            SelectedTA = null;
            SelectedPO = null;
            LetterNumber = null;
            DrNumber = null;
            DRDate = null;
            ReciptDate = null;
        }
    }
}
