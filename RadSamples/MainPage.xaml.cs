using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
using Telerik.Windows.Controls.TransitionControl;
using Telerik.Windows.Controls;
//using Telerik.Windows.Controls;

namespace RadSamples
{
    public static class ChildrenOfTypeExtensions
    {
        //
        // Summary:
        //     Gets all child elements recursively from the visual tree by given type.
        public static IEnumerable<T> ChildrenOfType<T>(this DependencyObject element) where T : DependencyObject
        {
            return element.GetChildrenRecursive().OfType<T>();
        }

        //
        // Summary:
        //     Finds child element of the specified type. Uses breadth-first search.
        //
        // Parameters:
        //   element:
        //     The target System.Windows.DependencyObject which children will be traversed.
        //
        //
        // Type parameters:
        //   T:
        //     The type of the child that will be searched in the object hierarchy. The type
        //     should be System.Windows.DependencyObject.
        //
        // Returns:
        //     The first child element that is of the specified type.
        public static T FindChildByType<T>(this DependencyObject element) where T : DependencyObject
        {
            return element.ChildrenOfType<T>().FirstOrDefault();
        }

        internal static IEnumerable<T> FindChildrenByType<T>(this DependencyObject element) where T : DependencyObject
        {
            return element.ChildrenOfType<T>();
        }

        internal static FrameworkElement GetChildByName(this FrameworkElement element, string name)
        {
            return ((FrameworkElement)element.FindName(name)) ?? element.ChildrenOfType<FrameworkElement>().FirstOrDefault((FrameworkElement c) => c.Name == name);
        }

        //
        // Summary:
        //     Does a deep search of the element tree, trying to find a descendant of the given
        //     type (including the element itself).
        //
        // Returns:
        //     True if the target is one of the elements.
        internal static T GetFirstDescendantOfType<T>(this DependencyObject target) where T : DependencyObject
        {
            return (target as T) ?? target.ChildrenOfType<T>().FirstOrDefault();
        }

        internal static IEnumerable<T> GetChildren<T>(this DependencyObject parent) where T : FrameworkElement
        {
            return parent.GetChildrenRecursive().OfType<T>();
        }

        //
        // Summary:
        //     Enumerates through element's children in the visual tree.
        private static IEnumerable<DependencyObject> GetChildrenRecursive(this DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(element, i);
                if (child == null)
                {
                    continue;
                }

                yield return child;
                foreach (DependencyObject item in child.GetChildrenRecursive())
                {
                    yield return item;
                }
            }
        }

        internal static IEnumerable<T> ChildrenOfType<T>(this DependencyObject element, Type typeWhichChildrenShouldBeSkipped)
        {
            return element.GetChildrenOfType(typeWhichChildrenShouldBeSkipped).OfType<T>();
        }

        private static IEnumerable<DependencyObject> GetChildrenOfType(this DependencyObject element, Type typeWhichChildrenShouldBeSkipped)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(element, i);
                yield return child;
                if (typeWhichChildrenShouldBeSkipped.IsAssignableFrom(child.GetType()))
                {
                    continue;
                }

                foreach (DependencyObject item in child.GetChildrenOfType(typeWhichChildrenShouldBeSkipped))
                {
                    yield return item;
                }
            }
        }
    }

    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

        }

        private void mainRTV_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //Dispatcher.InvokeAsync(new Action(async () =>
            //           {
            //await Task.Delay(1000);
            var uc1 = mainRTV.FindChildByType<UserControl1>();
            if (uc1 != null)
            {
                Console.WriteLine("FindChildByType Success");
            }
            else
            {
                Console.WriteLine("FindChildByType Failed");
            }
            //}), System.Windows.Threading.DispatcherPriority.Background);
        }

    }
}