using GalaSoft.MvvmLight;
using Microsoft.Win32;
using NLog;
using RecordTracker.Services;
using RecordTracker.SqliteDataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTracker.ViewModel
{
    public class RecordsReportViewModel : ViewModelBase
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        #region variable declaration
        IPoliceOfficerRepository POrepo;
        ObservableCollection<PoliceOfficer> _policeOfficers,_poListFilter;
       
        IPoliceStationRepository PSrepo;
        ObservableCollection<PoliceStation> _PoliceStations, _psListFilter;

        ITopicAndAreaRepository TArepo;
        ObservableCollection<TopicsAndArea> _topicsAndAreas, _taListFilter;

        IRecordRepository RecRepo;
        ObservableCollection<LetterRecord> _reportRecords;

        ISubjectRepository SubRepo;
        ObservableCollection<Subject> _subjects,_subjectListFilter;

        ISourceRepository SourceRepo;
        ObservableCollection<Source> _source,_sourceListFilter;

        StatusRepository StatusRepo;
        ObservableCollection<Status> _statusS,_statusFilter;

        LetterRecord _selectedRecord;
        PoliceOfficer _selectedPoliceOfficer;
        PoliceStation _selectedPoliceStation;
        TopicsAndArea _selectedTopic;
       

        public RelayCommand SaveRecord { get; private set; }

        public RelayCommand SearchRecord { get; private set; }

        public RelayCommand ExportToPDF { get; private set; }

        List<string> Headers;
        List<LetterRecord> letterRecords;
        #endregion

        #region Constructor
        public RecordsReportViewModel()
        {
            try
            {
                _logger.Info("Inside Records viewer view model construtor");
                if (!ViewModelBase.IsInDesignModeStatic)
                {

                    POrepo = new PoliceOfficerRepository();
                    PSrepo = new PoliceStationRepository();
                    TArepo = new TopicAndAreaRepository();
                    RecRepo = new RecordRepository();
                    StatusRepo = new StatusRepository();
                    SourceRepo = new SourceRepository();
                    SubRepo = new SubjectRepository();
                    SaveRecord = new RelayCommand(OnSave, canSave);
                    SearchRecord = new RelayCommand(onSearch, canSearch);
                    ExportToPDF = new RelayCommand(onExportToPdf, canExport);
                    Headers = new List<string>();
                    Headers.Add("Letter Number");
                    Headers.Add("D/R Number");
                    Headers.Add("Received Date");
                    Headers.Add("Police Station");
                   
                }
            }
            catch(Exception e)
            {
                _logger.Error("Some error have occured in RecordsReportViewModel, stacktrace=" + e.StackTrace);
                _logger.Error("RecordsReportViewModel error message is " + e.Message + " inner error is " + e.InnerException.Message);
            }
           
        }

     
        #endregion

        #region Observable collections
        public ObservableCollection<PoliceOfficer> PoliceOfficers
        {
            get { return _policeOfficers; }
            set { _policeOfficers = value; RaisePropertyChanged("PoliceOfficers"); }

        }

        public ObservableCollection<PoliceOfficer> POFilter
        {
            get { return _poListFilter; }
            set { _poListFilter = value; RaisePropertyChanged("POFilter"); }

        }
        public ObservableCollection<PoliceStation> PoliceStations
        {
            get { return _PoliceStations; }
            set { _PoliceStations = value; RaisePropertyChanged("PoliceStations"); }

        }
        public ObservableCollection<PoliceStation> PSFilter
        {
            get { return _psListFilter; }
            set { _psListFilter = value; RaisePropertyChanged("PSFilter"); }

        }
        public ObservableCollection<TopicsAndArea> TopicsAndAreas
        {
            get { return _topicsAndAreas; }
            set { _topicsAndAreas = value; RaisePropertyChanged("TopicsAndAreas"); }

        }
        public ObservableCollection<TopicsAndArea> TAFilter
        {
            get { return _taListFilter; }
            set { _taListFilter = value; RaisePropertyChanged("TAFilter"); }

        }

        public ObservableCollection<Status> Statuses
        {
            get { return _statusS; }
            set { _statusS = value; RaisePropertyChanged("Statuses"); }
        }

        public ObservableCollection<Status> StatusFilter
        {
            get { return _statusFilter; }
            set { _statusFilter = value; RaisePropertyChanged("StatusFilter"); }
        }

        public ObservableCollection<LetterRecord> ReportRecords
        {
            get { return _reportRecords; }
            set { _reportRecords = value; RaisePropertyChanged("ReportRecords");
                ExportToPDF.RaiseCanExecuteChanged();
            }

        }
        public ObservableCollection<Subject> Subjects
        {
            get { return _subjects; }
            set { _subjects = value; RaisePropertyChanged("Subjects"); }

        }
        public ObservableCollection<Subject> SubFilter
        {
            get { return _subjectListFilter; }
            set { _subjectListFilter = value; RaisePropertyChanged("SubFilter"); }

        }
        public ObservableCollection<Source> Sources
        {
            get { return _source; }
            set { _source = value; RaisePropertyChanged("Sources"); }

        }
        public ObservableCollection<Source> SrcFilter
        {
            get { return _sourceListFilter; }
            set { _sourceListFilter = value; RaisePropertyChanged("SrcFilter"); }

        }
        #endregion

        private Subject _selectedSubject;
        public Subject SelectedSubject
        {
            get { return _selectedSubject; }
            set
            {
                _selectedSubject = value;
                RaisePropertyChanged("SelectedSubject");
            }
        }


        private Source _selectedSource;
        public Source SelectedSource
        {
            get { return _selectedSource; }
            set
            {
                _selectedSource = value;
                RaisePropertyChanged("SelectedSource");
            }
        }

        public LetterRecord SelectedRecord
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

        private Status _selectedStatus;
        public Status SelectedStatus
        {
            get { return _selectedStatus; }
            set
            {
                _selectedStatus = value;
                RaisePropertyChanged("SelectedStatus");
            }
        }

        public void LoadData()
        {
            try
            {
                _logger.Info("Inside Records viewer view model Load data");
                var POList = POrepo.GetPoliceOfficersAsync().Result.ToList();
                ObservableCollection<PoliceOfficer> POData = new ObservableCollection<PoliceOfficer>();
                POFilter = new ObservableCollection<PoliceOfficer>();
                foreach (var item in POList)
                {
                    POData.Add(item);
                    POFilter.Add(item);
                }
                   
                PoliceOfficers = POData;
                POFilter.Add(new PoliceOfficer() { Id = 1000, Name = "All" });
                SelectedPoliceOfficer = POFilter.First(x => x.Name.Equals("All"));

                var PSList = PSrepo.GetPoliceStationsAsync().Result.ToList();
                ObservableCollection<PoliceStation> PSData = new ObservableCollection<PoliceStation>();
                PSFilter = new ObservableCollection<PoliceStation>();
                foreach (var item in PSList)
                {
                    PSData.Add(item);
                    PSFilter.Add(item);
                }
                   
                PoliceStations = PSData;
                PSFilter.Add(new PoliceStation() { Id = 1000, Name = "All" });
                SelectedPoliceStation = PSFilter.First(x => x.Name.Equals("All"));

                var TAList = TArepo.GetTopicAndAreasAsync().Result.ToList();
                ObservableCollection<TopicsAndArea> TAData = new ObservableCollection<TopicsAndArea>();
                TAFilter = new ObservableCollection<TopicsAndArea>();
                foreach (var item in TAList)
                {
                    TAData.Add(item);
                    TAFilter.Add(item);
                }
                    
                TopicsAndAreas = TAData;
              
                TAFilter.Add(new TopicsAndArea() { Id = 1000, Name = "All" });
                SelectedTopic = TAFilter.First(x => x.Name.Equals("All"));
                

                var StatusList = StatusRepo.GetStatus();
                ObservableCollection<Status> StatusData = new ObservableCollection<Status>();
                StatusFilter = new ObservableCollection<Status>();
                foreach (var item in StatusList)
                {
                    StatusData.Add(item);
                    StatusFilter.Add(item);
                }
                
                Statuses = StatusData;
                StatusFilter.Add(new Status() { Id = 1000, Name = "All" });
                SelectedStatus = StatusFilter.First(x => x.Name.Equals("Open"));

                var SourceList = SourceRepo.GetSourcesAsync().Result.ToList();
                ObservableCollection<Source> SourceData = new ObservableCollection<Source>();
                SrcFilter = new ObservableCollection<Source>();
                foreach (var item in SourceList)
                {
                    SourceData.Add(item);
                    SrcFilter.Add(item);
                }
                    
                Sources = SourceData;
                SrcFilter.Add(new Source() { Id = 1000, Name = "All" });
                SelectedSource= SrcFilter.First(x => x.Name.Equals("All"));

                var SubjectList = SubRepo.GetSubectsAsync().Result.ToList();
                ObservableCollection<Subject> SubjectData = new ObservableCollection<Subject>();
                SubFilter = new ObservableCollection<Subject>();
                foreach (var item in SubjectList)
                {
                    SubjectData.Add(item);
                    SubFilter.Add(item);
                }
                  
                Subjects = SubjectData;
                SubFilter.Add(new Subject() { Id = 1000, Name = "All" });
                SelectedSubject = SubFilter.First(x => x.Name.Equals("All"));
            }
            catch (Exception e)
            {
                _logger.Error("Some error have occured in Report.xaml" + e.StackTrace);
                _logger.Error("Report.xaml error message is " + e.Message + " inner error is " + e.InnerException.Message);
            }
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
            List<long> idPO, idPS, idTA,idSrc,idSub,idStatus;
            idPO = new List<long>();
            idPS = new List<long>();
            idTA = new List<long>();
            idSrc = new List<long>();
            idSub = new List<long>();
            idStatus = new List<long>();
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

            if (SelectedSource.Name.Equals("All"))
            {
                foreach (var item in Sources)
                {
                    idSrc.Add(item.Id);
                }
            }
            else
            {
                idSrc.Add(SelectedSource.Id);
            }

            if (SelectedSubject.Name.Equals("All"))
            {
                foreach (var item in Subjects)
                {
                    idSub.Add(item.Id);
                }
            }
            else
            {
                idSub.Add(SelectedSubject.Id);
            }

            if (SelectedStatus.Name.Equals("All"))
            {
                foreach (var item in Statuses)
                {
                    idStatus.Add(item.Id);
                }
            }
            else
            {
                idStatus.Add(SelectedStatus.Id);
            }
            letterRecords = RecRepo.GetRecordsAsync(idPS,idPO,idTA,idSrc,idSub,idStatus).Result.ToList();
          
            ObservableCollection<SqliteDataLayer.LetterRecord> RecData = new ObservableCollection<SqliteDataLayer.LetterRecord>();
            foreach (var item in letterRecords)
                RecData.Add(item);
            ReportRecords = RecData;

          
        }

        private bool canExport()
        {
            if (ReportRecords == null)
            {
                return false;
            }
            else
            {
                return ReportRecords.Count > 0;
            }
            
        }


        private void onExportToPdf()
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Pdf Files|*.pdf"
            };

            if (dialog.ShowDialog() == true)
            {
                CreatePDFDataGrid create = new CreatePDFDataGrid(dialog.FileName);
                //create.AddHeader(Headers);
                create.AddFilters("Police Officer", SelectedPoliceOfficer.Name);
                create.AddFilters("Police Station", SelectedPoliceStation.Name);
                create.AddFilters("Topic Or Area", SelectedTopic.Name);
                create.AddRecords(_reportRecords, _policeOfficers, _PoliceStations, _topicsAndAreas);
                create.SaveAndClose();
            }
           
            
           


        }
    }
}
