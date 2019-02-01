﻿using GalaSoft.MvvmLight;
using NLog;
using RecordTracker.Services;
using RecordTracker.SqliteDataLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTracker.ViewModel
{
    public class AddRecordViewModel : ViewModelBase
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        IPoliceOfficerRepository POrepo;
        ObservableCollection<PoliceOfficer> _policeOfficers;

        IPoliceStationRepository PSrepo;
        ObservableCollection<PoliceStation> _PoliceStations;

        ITopicAndAreaRepository TArepo;
        ObservableCollection<TopicsAndArea> _topicsAndAreas;

        ISubjectRepository SubRepo;
        ObservableCollection<Subject> _subjects;

        ISourceRepository SourceRepo;
        ObservableCollection<Source> _source;

        IRecordRepository AddRepo;
        public RelayCommand AddNewRecord{ get; private set; }
        public RelayCommand CancelRecord { get; private set; }
        public AddRecordViewModel()
        {
            try
            {
                if (!ViewModelBase.IsInDesignModeStatic)
                {
                    _logger.Info("Inside Add Record ViewModel construtor");
                    POrepo = new PoliceOfficerRepository();
                    PSrepo = new PoliceStationRepository();
                    TArepo = new TopicAndAreaRepository();
                    SourceRepo = new SourceRepository();
                    SubRepo = new SubjectRepository();
                    AddRepo = new RecordRepository();
                    AddNewRecord = new RelayCommand(OnAdd, canAdd);
                    CancelRecord = new RelayCommand(OnCancel, canCancel);
                }
            }
            catch (Exception e)
            {
                _logger.Error("Some error have occured in AddRecordViewModel" + e.StackTrace);
                _logger.Error("AddRecordViewModel error message is " + e.Message + " inner error is " + e.InnerException.Message);
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
        public void LoadData()
        {
            try
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
                _logger.Error("Some error have AddRecordViewModel Load data , stacktarce =" + e.StackTrace);
                _logger.Error("AddRecordViewModel error message is " + e.Message + " inner error is " + e.InnerException.Message);
            }
        }

        #region Properties
      

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

        private string _officeReceiptDate;
        public string OfficeReceiptDate
        {
            get { return _officeReceiptDate; }
            set
            {

                _officeReceiptDate = value;
                RaisePropertyChanged("OfficeReceiptDate");
                CancelRecord.RaiseCanExecuteChanged();
                AddNewRecord.RaiseCanExecuteChanged();
            }
        }

        private string _officeDispatchDate;
        public string OfficeDispatchDate
        {
            get { return _officeDispatchDate; }
            set
            {

                _officeDispatchDate = value;
                RaisePropertyChanged("OfficeDispatchDate");
                CancelRecord.RaiseCanExecuteChanged();
                AddNewRecord.RaiseCanExecuteChanged();
            }
        }

        private string _officeDispatchNumber;
        public string OfficeDispatchNumber
        {
            get { return _officeDispatchNumber; }
            set {
                if (value != "")
                    _officeDispatchNumber = value;
                else
                    _officeDispatchNumber = null;
                
                RaisePropertyChanged("OfficeDispatchNumber");
                AddNewRecord.RaiseCanExecuteChanged();
                CancelRecord.RaiseCanExecuteChanged();
            }
        }

        private string _psDispatchDate;
        public string PsDispatchDate
        {
            get { return _psDispatchDate; }
            set
            {
                _psDispatchDate = value;
                AddNewRecord.RaiseCanExecuteChanged();
                CancelRecord.RaiseCanExecuteChanged();
                RaisePropertyChanged("PsDispatchDate");

            }
        }

        private string _psDispatchNumber;
        public string PsDispatchNumber
        {
            get { return _psDispatchNumber; }
            set
            {
                if (value != "")
                    _psDispatchNumber = value;
                else
                    _psDispatchNumber = null;

                RaisePropertyChanged("PsDispatchNumber");
                AddNewRecord.RaiseCanExecuteChanged();
                CancelRecord.RaiseCanExecuteChanged();
            }
        }

        private string _sanhaDetail;
        public string SanhaDetail
        {
            get { return _sanhaDetail; }
            set
            {
                _sanhaDetail = value;
                RaisePropertyChanged("SanhaDetail");
                AddNewRecord.RaiseCanExecuteChanged();
                CancelRecord.RaiseCanExecuteChanged();
            }
        }

        private string _verificationDetail;
        public string VerificationDetail
        {
            get { return _verificationDetail; }
            set
            {
                _verificationDetail = value;
                RaisePropertyChanged("VerificationDetail");
                AddNewRecord.RaiseCanExecuteChanged();
                CancelRecord.RaiseCanExecuteChanged();
            }
        }

        private string _caseNumber;
        public string CaseNumber
        {
            get { return _caseNumber; }
            set
            {
                if (value != "")
                    _caseNumber = value;
                else
                    _caseNumber = null;
                RaisePropertyChanged("CaseNumber");
                AddNewRecord.RaiseCanExecuteChanged();
                CancelRecord.RaiseCanExecuteChanged();
            }
        }


        private string _organizationName;
        public string OrganizationName
        {
            get { return _organizationName; }
            set
            {
                _organizationName = value;
                RaisePropertyChanged("OrganizationName");
                AddNewRecord.RaiseCanExecuteChanged();
                CancelRecord.RaiseCanExecuteChanged();
            }
        }

        private string _remarks;
        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
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


        private Subject _selectedSubject;
        public Subject SelectedSubject
        {
            get { return _selectedSubject; }
            set
            {
                _selectedSubject = value;
                AddNewRecord.RaiseCanExecuteChanged();
                CancelRecord.RaiseCanExecuteChanged();
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
                AddNewRecord.RaiseCanExecuteChanged();
                CancelRecord.RaiseCanExecuteChanged();
                RaisePropertyChanged("SelectedSource");
            }
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
                && LetterNumber != null && OfficeDispatchNumber != null && OfficeDispatchDate != null && OfficeReceiptDate != null;
        }

        private void OnAdd()
        {
            SqliteDataLayer.LetterRecord record = new SqliteDataLayer.LetterRecord();

            record.LetterNumber = long.Parse(LetterNumber);
            record.OfficeDispatchNumber = long.Parse(OfficeDispatchNumber);
            record.SourceID = SelectedSource.Id;
            record.OfficeDispatchDate = FormatDate(OfficeDispatchDate);
            record.OfficeReceiptDate = FormatDate(OfficeReceiptDate);

            record.OrganizationName = OrganizationName;
            record.SanhaDetail = SanhaDetail;
            record.VerificationDetail = VerificationDetail;
            record.SubjectID = SelectedSubject.Id;

            record.PsDispatchNumber = long.Parse(PsDispatchNumber);
            record.PsDispatchDate = PsDispatchDate;
            record.TopicAreaID = SelectedTA.Id;
            record.PoliceOfficerID = SelectedPO.Id;
            record.PoliceStationID = SelectedPS.Id;
  
            record.StatusID = 1;
            record.Remarks = Remarks;
            record.CaseNumber = long.Parse(CaseNumber);
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
                || LetterNumber!=null || OfficeDispatchNumber != null|| OfficeDispatchDate != null || OfficeReceiptDate != null;
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
            OfficeDispatchNumber = null;
            OfficeDispatchDate = null;
            OfficeReceiptDate = null;
        }

        private string FormatDate(string dateTime)
        {
            var dty = DateTime.Parse(dateTime);
            return dty.ToString("yyyy-MM-dd");
        }
    }
}
