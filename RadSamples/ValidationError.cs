namespace KAG.Utilities
{
    /// <summary>
    /// Represents a validation error.
    /// </summary>
    public class ValidationError : NotifyPropertyChangedBase
    {
        /// <summary>
        /// The property the error belongs to.
        /// </summary>
        private string _propertyName;

        /// <summary>
        /// The code of the error.
        /// </summary>
        private string _code;

        /// <summary>
        /// The description of the error.
        /// </summary>
        private string _description;

        /// <summary>
        /// Gets or sets the property the error belongs to.
        /// </summary>
        public string PropertyName
        {
            get { return _propertyName; }
            set
            {
                if (_propertyName == value)
                    return;

                _propertyName = value;
                RaisePropertyChanged("PropertyName");
            }
        }
        
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public string Code
        {
            get { return _code; }
            set
            {
                if (_code == value)
                    return;

                _code = value;
                RaisePropertyChanged("Code");
            }
        }

        /// <summary>
        /// Gets or sets the description of the error.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                    return;

                _description = value;
                RaisePropertyChanged("Description");
            }
        }
    }
}
