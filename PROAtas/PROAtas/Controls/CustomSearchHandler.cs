using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PROAtas.Controls
{
    public class CustomSearchHandler : SearchHandler
    {
        private CancellationTokenSource cancelToken;

        public int SearchDelay { get; set; } = 1500;

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            // Cancels the token whose source is being executed
            cancelToken?.Cancel();

            // Creates new token instance
            IsSearching = true;
            cancelToken = new CancellationTokenSource();

            // Executes a background operation
            Task.Run(async () =>
            {
                // Takes the token reference that will be cancelled after the next character inserted
                var token = cancelToken.Token;
                // Await a certain time before trying another search
                await Task.Delay(SearchDelay);

                // If the token wasn't cancelled (when another character is inserted), do the search
                if (!token.IsCancellationRequested)
                {
                    Dispatcher.BeginInvokeOnMainThread(() =>
                    { 
                        if (SearchCommandParameter == null)
                            SearchCommand?.Execute(newValue);
                        else
                            SearchCommand?.Execute(SearchCommandParameter);

                        IsSearching = false;
                    });
                }
            }, cancelToken.Token);
        }

        public ICommand SearchCommand
        {
            get { return (ICommand)GetValue(SearchCommandProperty); }
            set { SetValue(SearchCommandProperty, value); }
        }
        public static readonly BindableProperty SearchCommandProperty = BindableProperty.Create(nameof(SearchCommand), typeof(ICommand), typeof(CustomSearchHandler), default(ICommand));

        public ICommand SearchCommandParameter
        {
            get { return (ICommand)GetValue(SearchCommandParameterProperty); }
            set { SetValue(SearchCommandParameterProperty, value); }
        }
        public static readonly BindableProperty SearchCommandParameterProperty = BindableProperty.Create(nameof(SearchCommandParameter), typeof(ICommand), typeof(CustomSearchHandler), default(ICommand));

        public bool IsSearching
        {
            get { return (bool)GetValue(IsSearchingProperty); }
            set { SetValue(IsSearchingProperty, value); }
        }
        public static readonly BindableProperty IsSearchingProperty = BindableProperty.Create(nameof(IsSearching), typeof(bool), typeof(CustomSearchHandler), default(bool));
    }
}