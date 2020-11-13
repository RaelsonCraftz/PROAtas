using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PROAtas.Controls
{
    public class CustomSearchHandler : SearchHandler
    {
        private CancellationTokenSource cancellationTokenSource;

        public int LoadDelay => 1000;

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (IsFocused || string.IsNullOrEmpty(newValue))
            {
                // Cancels the current execution
                try
                {
                    cancellationTokenSource?.Cancel();
                }
                catch (ObjectDisposedException) { Debug.WriteLine($"[MCL] CancellationTokenSource threw a ObjectDisposedException!"); }

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
                            if (SearchCommandParameter == null)
                                SearchCommand?.Execute(newValue);
                            else
                                SearchCommand?.Execute(SearchCommandParameter);
                        });
                    }
                    else
                        source.Dispose();

                }, cancellationTokenSource.Token);
            }
        }

        public ICommand SearchCommand
        {
            get { return (ICommand)GetValue(LoadCommandProperty); }
            set { SetValue(LoadCommandProperty, value); }
        }
        public static readonly BindableProperty LoadCommandProperty = BindableProperty.Create(nameof(SearchCommand), typeof(ICommand), typeof(CustomSearchHandler), default(ICommand));

        public object SearchCommandParameter
        {
            get { return (object)GetValue(LoadCommandParameterProperty); }
            set { SetValue(LoadCommandParameterProperty, value); }
        }
        public static readonly BindableProperty LoadCommandParameterProperty = BindableProperty.Create(nameof(SearchCommandParameter), typeof(object), typeof(CustomSearchHandler), default(object));

        public bool IsSearching
        {
            get { return (bool)GetValue(IsSearchingProperty); }
            set { SetValue(IsSearchingProperty, value); }
        }
        public static readonly BindableProperty IsSearchingProperty = BindableProperty.Create(nameof(IsSearching), typeof(bool), typeof(CustomSearchHandler), default(bool));
    }
}
