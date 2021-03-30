using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PROAtas.Controls
{
    public class LoadSearchBar : SearchBar
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
                {
                    cancellationTokenSource?.Cancel();
                }
                catch (ObjectDisposedException) { Debug.WriteLine($"[{AppInfo.Name}] CancellationTokenSource threw a ObjectDisposedException!"); }

                // Creates a new instance for the cancellation token
                cancellationTokenSource = new CancellationTokenSource();

                // Executes a background operation
                Task.Run(async () =>
                {
                    // Takes the token reference that will be cancelled after the next character inserted
                    var source = cancellationTokenSource;
                    // Await a certain time before trying another search
                    await Task.Delay(LoadDelay);

                    // If the token wasn't cancelled (when another character is inserted), do the search
                    if (!source.Token.IsCancellationRequested)
                    {
                        source.Dispose();

                        Dispatcher.BeginInvokeOnMainThread(() =>
                        {
                            if (LoadCommandParameter == null)
                                SearchCommand?.Execute(newValue);
                            else
                                SearchCommand?.Execute(LoadCommandParameter);
                        });
                    }
                    else
                        source.Dispose();

                }, cancellationTokenSource.Token);
            }
        }

        public Command LoadCommand
        {
            get { return (Command)GetValue(LoadCommandProperty); }
            set { SetValue(LoadCommandProperty, value); }
        }
        public static readonly BindableProperty LoadCommandProperty = BindableProperty.Create(nameof(SearchCommand), typeof(Command), typeof(LoadSearchBar), default(Command));

        public object LoadCommandParameter
        {
            get { return (object)GetValue(LoadCommandParameterProperty); }
            set { SetValue(LoadCommandParameterProperty, value); }
        }
        public static readonly BindableProperty LoadCommandParameterProperty = BindableProperty.Create(nameof(LoadCommandParameter), typeof(object), typeof(LoadSearchBar), default(object));

        public bool IsSearching
        {
            get { return (bool)GetValue(IsSearchingProperty); }
            set { SetValue(IsSearchingProperty, value); }
        }
        public static readonly BindableProperty IsSearchingProperty = BindableProperty.Create(nameof(IsSearching), typeof(bool), typeof(LoadSearchBar), default(bool));
    }
}
