using GalaSoft.MvvmLight;
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
    public class PoliceOfficerListViewModel : ViewModelBase
    {
        IPoliceOfficerRepository repo;
        ObservableCollection<PoliceOfficer> _policeOfficers;
        public RelayCommand AddPoliceOfficer { get; private set; }
        public RelayCommand DeletePoliceOfficer { get; private set; }
        public RelayCommand SavePoliceOfficer { get; private set; }
        private string _poName = "";
        private PoliceOfficer _selectedPoliceOfficer;

        //constructor
        public PoliceOfficerListViewModel()
        {
            if (!ViewModelBase.IsInDesignModeStatic)
            {
                //Code that throws the exception
                repo = new PoliceOfficerRepository();
                AddPoliceOfficer = new RelayCommand(OnAdd);
                DeletePoliceOfficer = new RelayCommand(OnDelete, CanDelete);
                SavePoliceOfficer = new RelayCommand(OnSave, CanSave);
            }
        }

        #region All Properties
        public ObservableCollection<PoliceOfficer> PoliceOfficers
        {
            get { return _policeOfficers; }
            set { _policeOfficers = value; RaisePropertyChanged("PoliceOfficers"); }

        }

        public PoliceOfficer SelectedPoliceOfficer
        {
            get
            {
                return _selectedPoliceOfficer;
            }
            set
            {
                _selectedPoliceOfficer = value;
                DeletePoliceOfficer.RaiseCanExecuteChanged();
                SavePoliceOfficer.RaiseCanExecuteChanged();
            }
        }
        public string NewPOname
        {
            get { return _poName; }
            set
            {
                _poName = value;
                RaisePropertyChanged("NewPOname");
                //AddPoliceStation.RaiseCanExecuteChanged(); // this code will work with CanAdd function.
            }
        }
        #endregion

        #region All methods
        public void LoadData()
        {
            var data = repo.GetPoliceOfficersAsync().Result.ToList();
            ObservableCollection<PoliceOfficer> PSData = new ObservableCollection<PoliceOfficer>();
            foreach (var item in data)
                PSData.Add(item);
            PoliceOfficers = PSData;
        }

        private void OnAdd()
        {
            PoliceOfficer PO = new PoliceOfficer() { Name = NewPOname };
            NewPOname = "";
            repo.AddPoliceOfficerAsync(PO);
            PoliceOfficers.Add(PO);
        }

        private void OnDelete()
        {

            repo.DeletePoliceOfficerAsync(SelectedPoliceOfficer.Id);
            PoliceOfficers.Remove(SelectedPoliceOfficer);
        }

        private bool CanDelete()
        {
            return SelectedPoliceOfficer != null;
        }

        private void OnSave()
        {
            repo.UpdatePoliceOfficerAsync(SelectedPoliceOfficer);
            SelectedPoliceOfficer = null;
        }

        private bool CanSave()
        {
            return SelectedPoliceOfficer != null;
        }
        #endregion
    }
}
