using KAG.Utilities;

namespace RadSamples
{
    public partial class RadWindow1
    {
        public class ScrollOrdersViewModel : NotifyPropertyAndErrorBase
        {
            private ScrollOrdersModelRequest _request;
            public ScrollOrdersModelRequest Request
            {
                get { return _request; }
                set
                {
                    if (_request == value)
                    {
                        return;
                    }

                    _request = value;
                    RaisePropertyChanged(() => Request);
                }
            }
        }
    }
}