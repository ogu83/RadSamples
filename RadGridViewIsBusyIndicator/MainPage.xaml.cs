using KAG.Utilities;
using RadGridViewIsBusyIndicator;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace KAG.OCScreen.Utility.StyleSelectors
{
    public class CommodityCellStyleSelector : StyleSelector
    {
        public Style SplashBlendStyle { get; set; }

        public Style DeletedStyle { get; set; }

        public Style UndeletedStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            FreightLineModel freightLine = item as FreightLineModel;

            if (freightLine == null || freightLine.ModelState.HasFlag(ModelState.Deleted))
            {
                return DeletedStyle;
            }

            FreightParentLineModel parent = freightLine as FreightParentLineModel;

            if (parent != null && parent.Children.Count > 1)
            {
                return SplashBlendStyle;
            }

            return UndeletedStyle;
        }
    }
}

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

    public class FreightChildLineModel : FreightLineModel
    {
        private FreightParentLineModel _parent;
        public FreightParentLineModel Parent
        {
            get { return _parent; }
            set
            {
                if (_parent == value)
                {
                    return;
                }

                if (_parent != null)
                {
                    _parent.Children.Remove(this);
                }

                if (value != null)
                {
                    value.Children.Add(this);
                }

                _parent = value;
                RaisePropertyChanged(() => Parent);
            }
        }
    }

    public abstract class FreightLineModel : EditableModel
    {
        double _fgtQuantity;
        public double FgtQuantity { get { return _fgtQuantity; } set { if (_fgtQuantity == value) return; _fgtQuantity = value; RaisePropertyChanged(nameof(FgtQuantity)); } }

        double _fgtVolume2;
        public double FgtVolume2 { get { return _fgtVolume2; } set { if (_fgtVolume2 == value) return; _fgtVolume2 = value; RaisePropertyChanged(nameof(FgtVolume2)); } }

    }

    public class FreightParentLineModel : FreightLineModel
    {
        public FreightParentLineModel()
        {
            Children = new ObservableCollection<FreightChildLineModel>();
        }

        public ObservableCollection<FreightChildLineModel> Children { get; private set; }
    }

    public partial class MainPage : Page
    {
        public ObservableCollection<FreightParentLineModel> ParentLines { get; private set; }

        public MainPage()
        {
            DataContext = this;
            ParentLines = new ObservableCollection<FreightParentLineModel> {
                new FreightParentLineModel { FgtQuantity = 10, FgtVolume2= 100 },
                new FreightParentLineModel { FgtQuantity = 20, FgtVolume2 = 200 },
            };
            InitializeComponent();
        }
    }
}