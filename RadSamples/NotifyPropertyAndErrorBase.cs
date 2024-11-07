// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBeProtected.Global

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace KAG.Utilities
{
#if SILVERLIGHT
    /// <summary>
    /// A base class that implements the INotifyPropertyChanged and INotifyDataErrorInfo interfaces.
    /// </summary>
#else
    /// <summary>
    /// A base class that implements the INotifyPropertyChanged and IDataErrorInfo interfaces.
    /// </summary>
#endif
    public abstract class NotifyPropertyAndErrorBase : NotifyPropertyChangedBase, INotifyDataErrorInfoEx
    {
        /// <summary>
        /// Initializes a new instance of the NotifyPropertyAndErrorBase class.
        /// </summary>
        protected NotifyPropertyAndErrorBase()
        {
            Errors = new ObservableCollection<ValidationError>();
        }

#if SILVERLIGHT
        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire object.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
#endif

        /// <summary>
        /// Gets the collection of errors on the object.
        /// </summary>
        [Display(AutoGenerateField = false)]
        protected ObservableCollection<ValidationError> Errors { get; set; }

        /// <summary>
        /// Gets a value indicating whether the object has validation errors.
        /// </summary>
        [Display(AutoGenerateField = false)]
        public bool HasErrors
        {
            get { return Errors.Any(); }
        }

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire object.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property to retrieve validation errors for, or null or System.String.Empty
        /// to retrieve errors for the entire object.
        /// </param>
        /// <returns>The validation errors for the property or object.</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            IEnumerable<string> errors;

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                errors = Errors.Select(e => e.Description);
                
            }
            else
            {
                errors = Errors.Where(e => e.PropertyName == propertyName)
                    .Select(e => e.Description);
            }

            return errors;
        }

        /// <summary>
        /// Removes all errors from the object.
        /// </summary>
        public void ClearErrors()
        {
#if SILVERLIGHT
            IEnumerable<string> properties = Errors.Select(e => e.PropertyName)
                .Distinct()
                .ToList();
#endif
            Errors.Clear();

#if SILVERLIGHT
            foreach (var property in properties)
            {
                RaiseErrorsChanged(property);
            }
#endif
        }

        protected void RemoveError<T>(Expression<Func<T>> propertyExpression, int code)
        {
            RemoveError(GetPropertyName(propertyExpression), code);
        }

        protected void RemoveError<T>(Expression<Func<T>> propertyExpression, string code)
        {
            RemoveError(GetPropertyName(propertyExpression), code);
        }

        /// <summary>
        /// Removes the error with the code provided.
        /// </summary>
        /// <param name="propertyName">The property the error is on.</param>
        /// <param name="code">The code of the error.</param>
        public void RemoveError(string propertyName, int code)
        {
            RemoveError(propertyName, code.ToString());
        }

        /// <summary>
        /// Removes the error with the code provided.
        /// </summary>
        /// <param name="propertyName">The property the error is on.</param>
        /// <param name="code">The code of the error.</param>
        public void RemoveError(string propertyName, string code)
        {
            ValidationError error = Errors.SingleOrDefault(e => e.PropertyName == propertyName
                                                                && e.Code == code);
            if (error == null)
            {
                return;
            }

            Errors.Remove(error);

#if SILVERLIGHT
            RaiseErrorsChanged(propertyName);
#endif
        }

        protected void AddError<T>(Expression<Func<T>> propertyExpression, int code, string description)
        {
            AddError(GetPropertyName(propertyExpression), code, description);
        }

        protected void AddError<T>(Expression<Func<T>> propertyExpression, string code, string description)
        {
            AddError(GetPropertyName(propertyExpression), code, description);
        }

        /// <summary>
        /// Adds an error to a property.
        /// If an error with this code exists, it's description will be changed.
        /// </summary>
        /// <param name="propertyName">The propery the error is on.</param>
        /// <param name="code">The code of the error.</param>
        /// <param name="description">The description of the error.</param>
        public void AddError(string propertyName, int code, string description)
        {
            AddError(propertyName, code.ToString(), description);
        }

        /// <summary>
        /// Adds an error to a property.
        /// If an error with this code exists, it's description will be changed.
        /// </summary>
        /// <param name="propertyName">The propery the error is on.</param>
        /// <param name="code">The code of the error.</param>
        /// <param name="description">The description of the error.</param>
        public void AddError(string propertyName, string code, string description)
        {
            RemoveError(propertyName, code);

            var current = Errors.SingleOrDefault(e => e.PropertyName == propertyName && e.Code == code);

            if (current == null)
            {
                Errors.Add(new ValidationError {PropertyName = propertyName, Code = code, Description = description});
            }
            else
            {
                current.Description = description;
            }

#if SILVERLIGHT
            RaiseErrorsChanged(propertyName);
#endif
        }

#if SILVERLIGHT
        /// <summary>
        /// Raises the ErrorChanged event.
        /// </summary>
        /// <param name="propertyName">The property for which the errors have changed.</param>
        protected void RaiseErrorsChanged(string propertyName)
        {
            EventHandler<DataErrorsChangedEventArgs> errorsChanged = ErrorsChanged;
            if (errorsChanged == null)
            {
                return;
            }

            errorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
#endif

#if !SILVERLIGHT
        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <returns>The error message for the property. The default is an empty string ("").</returns>
        public string this[string columnName]
        {
            get
            {
                IEnumerable<string> errors = GetErrors(columnName)
                    .Cast<string>()
                    .ToList();

                return errors.Any() 
                    ? string.Join("\n", errors) 
                    : null;
            }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        public string Error
        {
            get 
            {
                return Errors.Any() 
                    ? string.Join("\n", Errors.Select(e => e.Description)) 
                    : null;
            }
        }
#endif
    }
}

// ReSharper restore MemberCanBeProtected.Global
// ReSharper restore UnusedMember.Global
// ReSharper restore MemberCanBePrivate.Global