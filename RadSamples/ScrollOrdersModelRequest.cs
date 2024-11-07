using KAG.Utilities;
using System.ComponentModel.DataAnnotations;
using System;

namespace RadSamples
{
    public partial class RadWindow1
    {
        public class ScrollOrdersModelRequest : NotifyPropertyAndErrorBase
        {
            private DateTime? _startDateStart;

            public DateTime? StartDateStart
            {
                get { return _startDateStart; }
                set
                {
                    if (_startDateStart == value)
                        return;

                    _startDateStart = value;
                    RaisePropertyChanged(() => StartDateStart);
                }
            }

            private DateTime? _startDateEnd;

            public DateTime? StartDateEnd
            {
                get { return _startDateEnd; }
                set
                {
                    if (_startDateEnd == value)
                        return;

                    _startDateEnd = value;
                    RaisePropertyChanged(() => StartDateEnd);
                }
            }

            private DateTime? _completionDateStart;

            public DateTime? CompletionDateStart
            {
                get { return _completionDateStart; }
                set
                {
                    if (_completionDateStart == value)
                        return;

                    _completionDateStart = value;
                    RaisePropertyChanged(() => CompletionDateStart);
                }
            }

            private DateTime? _completionDateEnd;

            public DateTime? CompletionDateEnd
            {
                get { return _completionDateEnd; }
                set
                {
                    if (_completionDateEnd == value)
                        return;

                    _completionDateEnd = value;
                    RaisePropertyChanged(() => CompletionDateEnd);
                }
            }

            private DateTime? _bookDateStart;

            public DateTime? BookDateStart
            {
                get { return _bookDateStart; }
                set
                {
                    if (_bookDateStart == value)
                        return;

                    _bookDateStart = value;
                    RaisePropertyChanged(() => BookDateStart);
                }
            }

            private DateTime? _bookDateEnd;

            public DateTime? BookDateEnd
            {
                get { return _bookDateEnd; }
                set
                {
                    if (_bookDateEnd == value)
                        return;

                    _bookDateEnd = value;
                    RaisePropertyChanged(() => BookDateEnd);
                }
            }

            private string _orderSource;

            [StringLength(6)]
            public string OrderSource
            {
                get { return _orderSource; }
                set
                {
                    if (_orderSource == value)
                        return;

                    _orderSource = value;
                    RaisePropertyChanged(() => OrderSource);
                }
            }
        }
    }
}