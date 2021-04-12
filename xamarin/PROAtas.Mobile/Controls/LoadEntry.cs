using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PROAtas.Mobile.Controls
{
    public class LoadEntry : Entry
    {
        private CancellationTokenSource cancellationTokenSource;

        public int LoadDelay => 1000;

        protected override void OnTextChanged(string oldValue, string newValue)
        {
            base.OnTextChanged(oldValue, newValue);

            if (IsFocused || string.IsNullOrEmpty(newValue))
            {
                // Cancels the current execution
                try 
                { cancellationTokenSource?.Cancel(); }
                catch (ObjectDisposedException) 
                { Debug.WriteLine($"[{AppInfo.Name}] an error occurred while canceling the CancellationTokenSource of an LoadEditor. This is expected to occur occasionally"); }

                // Creates a new instance for the cancellation token
                cancellationTokenSource = new CancellationTokenSource();

                // Executes a background operation
                IsBusy = true;
                Task.Run(async () =>
                {
                    // Takes the token reference that will be cancelled after the next character inserted
                    var source = cancellationTokenSource;
                    // Await a certain time before executing the search
                    await Task.Delay(LoadDelay);

                    // If the token wasn't cancelled (when another character is inserted), do the search
                    if (!source.Token.IsCancellationRequested)
                    {
                        source.Dispose();

                        Dispatcher.BeginInvokeOnMainThread(() =>
                        {
                            if (LoadCommandParameter == null)
                                LoadCommand?.Execute(newValue);
                            else
                                LoadCommand?.Execute(LoadCommandParameter);

                            IsBusy = false;
                        });
                    }
                    else
                        source.Dispose();

                }, cancellationTokenSource.Token);
            }
        }

        public ICommand LoadCommand
        {
            get { return (Command)GetValue(LoadCommandProperty); }
            set { SetValue(LoadCommandProperty, value); }
        }
        public static readonly BindableProperty LoadCommandProperty = BindableProperty.Create(nameof(LoadCommand), typeof(ICommand), typeof(LoadEntry), default(ICommand));

        public object LoadCommandParameter
        {
            get { return (object)GetValue(LoadCommandParameterProperty); }
            set { SetValue(LoadCommandParameterProperty, value); }
        }
        public static readonly BindableProperty LoadCommandParameterProperty = BindableProperty.Create(nameof(LoadCommandParameter), typeof(object), typeof(LoadEntry), default(object));

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }
        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(nameof(IsBusy), typeof(bool), typeof(LoadEntry), default(bool));
    }
}
