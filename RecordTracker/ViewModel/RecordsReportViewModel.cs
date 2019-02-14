﻿using GalaSoft.MvvmLight;
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
        ObservableCollection<PoliceOfficer> _policeOfficers;

        IPoliceStationRepository PSrepo;
        ObservableCollection<PoliceStation> _PoliceStations;

        ITopicAndAreaRepository TArepo;
        ObservableCollection<TopicsAndArea> _topicsAndAreas;

        IRecordRepository RecRepo;
        ObservableCollection<SqliteDataLayer.LetterRecord> _reportRecords;

        StatusRepository StatusRepo;
        ObservableCollection<Status> _statusS;

        ISubjectRepository SubRepo;
        ObservableCollection<Subject> _subjects;

        ISourceRepository SourceRepo;
        ObservableCollection<Source> _source;

        LetterRecord _selectedRecord;

        PoliceOfficer _selectedPoliceOfficer;
        PoliceStation _selectedPoliceStation;
        TopicsAndArea _selectedTopic;
       


        public RelayCommand SaveRecord { get; private set; }

        public RelayCommand SearchRecord { get; private set; }

        public RelayCommand ExportToPDF { get; private set; }

        List<string> Headers;
        List<SqliteDataLayer.LetterRecord> letterRecords;
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

        public ObservableCollection<Source> Sources
        {
            get { return _source; }
            set { _source = value; RaisePropertyChanged("Sources"); }

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
       

        public void LoadData()
        {
            try
            {
                _logger.Info("Inside Records viewer view model Load data");
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

                var SourceList = SourceRepo.GetSourcesAsync().Result.ToList();
                ObservableCollection<Source> SourceData = new ObservableCollection<Source>();
                foreach (var item in SourceList)
                    SourceData.Add(item);
                Sources = SourceData;

                var SubjectList = SubRepo.GetSubectsAsync().Result.ToList();
                ObservableCollection<Subject> SubjectData = new ObservableCollection<Subject>();
                foreach (var item in SubjectList)
                    SubjectData.Add(item);
                Subjects = SubjectData;
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
            List<long> idPO, idPS, idTA;
            idPO = new List<long>();
            idPS = new List<long>();
            idTA = new List<long>();
            
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
            CreatePDFDataGrid create = new CreatePDFDataGrid("First.pdf");
            //create.AddHeader(Headers);
            create.AddFilters("Police Officer", SelectedPoliceOfficer.Name);
            create.AddFilters("Police Station", SelectedPoliceStation.Name);
            create.AddFilters("Topic Or Area", SelectedTopic.Name);
            create.AddRecords(_reportRecords,_policeOfficers,_PoliceStations,_topicsAndAreas);
            create.SaveAndClose();
            
           


        }
    }
}
