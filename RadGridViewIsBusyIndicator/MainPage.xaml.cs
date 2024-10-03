using KAG.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace RadGridViewIsBusyIndicator
{
    [Flags]
    public enum ModelState
    {
        Unmodified = 0x0,
        Modified = 0x1,
        New = 0x2,
        Deleted = 0x4
    }

    public interface IEditableModel : INotifyPropertyChanged, INotifyDataErrorInfoEx
    {
        ModelState ModelState { get; set; }
    }

    public abstract class EditableModel : NotifyPropertyAndErrorBase, IEditableModel
    {
        protected EditableModel()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != GetPropertyName(() => ModelState))
            {
                ModelState |= ModelState.Modified;
            }
        }
#if OPENSILVER
        ModelState _modelState;
        public ModelState ModelState { get { return _modelState; } set { if (_modelState == value) return; _modelState = value; RaisePropertyChanged(nameof(ModelState)); } }
#else
        public ModelState ModelState { get; set; }
#endif
    }

    public class StopModel : EditableModel
    {

        DateTime? _stpArrivalDate;
        public DateTime? StpArrivalDate
        {
            get { return _stpArrivalDate; }
            set
            {
                if (_stpArrivalDate == value)
                    return;
                _stpArrivalDate = value;
                RaisePropertyChanged(nameof(StpArrivalDate));
            }
        }

    }

    public class MyModel: EditableModel
    {
       public ObservableCollection<StopModel> ItemsView { get; set; }
    }

    public partial class MainPage : Page
    {
        ObservableCollection<StopModel> ItemsView { get; set; }

        public MainPage()
        {
            var elements = new List<StopModel> {
                new StopModel() { StpArrivalDate = DateTime.Today.AddDays(-1)},
                new StopModel() { StpArrivalDate = DateTime.Today }
            };

            var myModel = new MyModel() { ItemsView = new ObservableCollection<StopModel>(elements) };

            DataContext = myModel;

            InitializeComponent();
        }
    }
}