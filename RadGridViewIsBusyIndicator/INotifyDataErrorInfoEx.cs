using System.ComponentModel;

namespace KAG.Utilities
{
#if SILVERLIGHT
    /// <summary>
    /// Notify Data Error Info Ex interface
    /// </summary>
    public interface INotifyDataErrorInfoEx : INotifyDataErrorInfo
#else
    public interface INotifyDataErrorInfoEx : IDataErrorInfo
#endif
    {
        /// <summary>
        /// Removes all errors from the object.
        /// </summary>
        void ClearErrors();

        /// <summary>
        /// Removes the error with the code provided.
        /// </summary>
        /// <param name="propertyName">The property the error is on.</param>
        /// <param name="code">The code of the error.</param>
        void RemoveError(string propertyName, int code);

        /// <summary>
        /// Removes the error with the code provided.
        /// </summary>
        /// <param name="propertyName">The property the error is on.</param>
        /// <param name="code">The code of the error.</param>
        void RemoveError(string propertyName, string code);

        /// <summary>
        /// Adds an error to a property.
        /// If an error with this code exists, it's description will be changed.
        /// </summary>
        /// <param name="propertyName">The propery the error is on.</param>
        /// <param name="code">The code of the error.</param>
        /// <param name="description">The description of the error.</param>
        void AddError(string propertyName, int code, string description);

        /// <summary>
        /// Adds an error to a property.
        /// If an error with this code exists, it's description will be changed.
        /// </summary>
        /// <param name="propertyName">The propery the error is on.</param>
        /// <param name="code">The code of the error.</param>
        /// <param name="description">The description of the error.</param>
        void AddError(string propertyName, string code, string description);
    }
}