using KAG.Utilities;
using System;
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

    //    public class StopModel : EditableModel
    //    {

    //        DateTime? _stpArrivalDate;
    //        public DateTime? StpArrivalDate
    //        {
    //            get { return _stpArrivalDate; }
    //            set
    //            {
    //                if (_stpArrivalDate == value)
    //                    return;
    //                _stpArrivalDate = value;
    //                RaisePropertyChanged(nameof(StpArrivalDate));
    //            }
    //        }

    //    }

    //    public class MyModel: EditableModel
    //    {
    //       public ObservableCollection<StopModel> ItemsView { get; set; }
    //    }

    //public static class TextBoxExtensions
    //{
    //    public static readonly DependencyProperty BlockKeyInputProperty =
    //        DependencyProperty.RegisterAttached(
    //            "BlockKeyInput",
    //            typeof(bool),
    //            typeof(TextBoxExtensions),
    //            new PropertyMetadata(false, OnBlockKeyInputChanged));

    //    public static bool GetBlockKeyInput(DependencyObject obj)
    //    {
    //        return (bool)obj.GetValue(BlockKeyInputProperty);
    //    }

    //    public static void SetBlockKeyInput(DependencyObject obj, bool value)
    //    {
    //        obj.SetValue(BlockKeyInputProperty, value);
    //    }

    //    private static void OnBlockKeyInputChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //        if (d is TextBox textBox && (bool)e.NewValue)
    //        {
    //            textBox.KeyDown += TextBox_KeyDown;
    //            textBox.GotFocus += TextBox_GotFocus;  // Attach GotFocus event handler
    //            textBox.MouseEnter += TextBox_MouseEnter;
    //            textBox.MouseLeave += TextBox_MouseLeave;
    //            textBox.Focus();
    //            textBox.IsTabStop = false;             // Prevent tabbing
    //            textBox.ContextMenu = null;            // Disable the context menu
    //        }
    //        else if (d is TextBox textBox1 && !(bool)e.NewValue)
    //        {
    //            textBox1.MouseLeave -= TextBox_MouseLeave;
    //            textBox1.MouseEnter -= TextBox_MouseEnter;
    //            textBox1.KeyDown -= TextBox_KeyDown;
    //            textBox1.GotFocus -= TextBox_GotFocus;  // Detach GotFocus event handler
    //            textBox1.ClearValue(TextBox.ContextMenuProperty);  // Restore context menu
    //            textBox1.ClearValue(TextBox.IsTabStopProperty);     // Restore IsTabStop property
    //        }
    //    }

    //    private static void TextBox_MouseLeave(object sender, MouseEventArgs e)
    //    {
    //        //if (sender is TextBox textBox)
    //        //    textBox.IsHitTestVisible = true;
    //    }

    //    private static void TextBox_MouseEnter(object sender, MouseEventArgs e)
    //    {
    //        //if (sender is TextBox textBox)
    //        //    textBox.IsHitTestVisible = false;
    //    }

    //    // KeyDown event handler to block input and Ctrl+V for paste
    //    private static void TextBox_KeyDown(object sender, KeyEventArgs e)
    //    {
    //        if (sender is TextBox textBox && !textBox.IsTabStop)
    //        {
    //            // Block all key input
    //            e.Handled = true;
    //            textBox.IsHitTestVisible = false;
    //            //if (e.Key == Key.Ctrl)
    //            //{
    //            //    e.Cancellable = true;
    //            //    e.Handled = true;
    //            //}

    //            //// Check for Ctrl+V (paste)
    //            //if ((e.Key == Key.V) && ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control))
    //            //{
    //            //    e.Handled = true; // Block paste operation via Ctrl+V
    //            //}
    //        }
    //    }

    //    private static void TextBox_GotFocus(object sender, RoutedEventArgs e)
    //    {
    //        if (sender is TextBox textBox && !textBox.IsTabStop)
    //        {
    //            var parent = VisualTreeHelper.GetParent(textBox) as Control;
    //            if (parent != null)
    //                parent.Focus();
    //        }
    //    }
    //}

    public class FreightChildLineModel : FreightLineModel
    {
        private FreightParentLineModel _parent;

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
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
        /// <summary>
        /// Initializes a new instance of the <see cref="FreightParentLineModel"/> class.
        /// </summary>
        public FreightParentLineModel()
        {
            Children = new ObservableCollection<FreightChildLineModel>();
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        public ObservableCollection<FreightChildLineModel> Children { get; private set; }
    }

    public partial class MainPage : Page
    {
        //ObservableCollection<StopModel> ItemsView { get; set; }

        ObservableCollection<FreightParentLineModel> ParentLines { get; }

        public MainPage()
        {
            //var elements = new List<StopModel> {
            //    new StopModel() { StpArrivalDate = DateTime.Today.AddDays(-1)},
            //    new StopModel() { StpArrivalDate = DateTime.Today }
            //};

            //var myModel = new MyModel() { ItemsView = new ObservableCollection<StopModel>(elements) };

            //DataContext = myModel;
            DataContext = this;
            InitializeComponent();
        }
    }
}