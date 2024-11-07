using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;

namespace KAG.Utilities
{
    /// <summary>
    /// A base class that implements the INotifyPropertyChanged interface.
    /// </summary>
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Verifies that a property name exists in an object.
        /// This method is only called in DEBUG mode.
        /// </summary>
        /// <param name="propertyName">A string containing the name of a property to verify.</param>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        protected void VerifyPropertyName(string propertyName)
        {
            var type = GetType();

            if (type.GetProperty(propertyName) == null)
                throw new ArgumentException("Property not found", propertyName);
        }

        /// <summary>
        /// Helper method for the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);

            //handle race condition of removing an event handler after the null check
            //but before the event is raised.
            var propChanged = PropertyChanged;

            if (propChanged == null)
                return;

            propChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Helper method for the PropertyChanged event.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyExpression">An Expression that contains the property that changed.</param>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            RaisePropertyChanged(GetPropertyName(propertyExpression));
        }

        /// <summary>
        /// Gets the string that is the name of the property.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyExpression">An Expression that contains the property.</param>
        /// <returns>The property name.</returns>
        /// <exception cref="ArgumentNullException">If propertyExpression is null.</exception>
        /// <exception cref="ArgumentException">If the Expression cannot be cast to a MemberExpression.</exception>
        protected string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var mbody = propertyExpression.Body as MemberExpression;

            if (mbody == null)
            {
                var ubody = propertyExpression.Body as UnaryExpression;

                if (ubody != null)
                {
                    mbody = ubody.Operand as MemberExpression;
                }

                if (mbody == null)
                {
                    throw new ArgumentException("Expression is not a MemberExpression", "propertyExpression");
                }
            }

            return mbody.Member.Name;
        }
    }
}
