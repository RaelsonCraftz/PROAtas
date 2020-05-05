using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PROAtas.Controls
{
    public class CustomEntry : Entry
    {
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public int SaveDelay { get; set; } = 1500;

        protected override void OnTextChanged(string oldTextValue, string newTextValue)
        {
            base.OnTextChanged(oldTextValue, newTextValue);

            if (IsSavingEnabled && !IsSaving)
            {
                IsSaving = true;

                // Executes a background operation
                Task.Run(async () =>
                {
                    // Cancels the current execution
                    if (cancellationTokenSource != null) cancellationTokenSource.Cancel();

                    // Creates a new instance for the cancellation token
                    cancellationTokenSource = new CancellationTokenSource();
                    
                    // Takes the token reference reference that will be cancelled after the next character inserted
                    var tokenSource = cancellationTokenSource;
                    // Await a certain time before trying another search
                    await Task.Delay(SaveDelay);

                    // If the token wasn't cancelled (when another character is inserted), do the search
                    if (!tokenSource.Token.IsCancellationRequested)
                    {
                        tokenSource.Dispose();
                        
                        Dispatcher.BeginInvokeOnMainThread(() =>
                        {
                            if (SaveCommandParameter == null)
                                SaveCommand?.Execute(newTextValue);
                            else
                                SaveCommand?.Execute(SaveCommandParameter);

                            IsSaving = false;
                        });
                    }

                }, cancellationTokenSource.Token);
            }
        }

        public ICommand SaveCommand
        {
            get { return (ICommand)GetValue(SaveCommandProperty); }
            set { SetValue(SaveCommandProperty, value); }
        }
        public static readonly BindableProperty SaveCommandProperty = BindableProperty.Create(nameof(SaveCommand), typeof(ICommand), typeof(CustomEntry), default(ICommand));

        public object SaveCommandParameter
        {
            get { return (object)GetValue(SaveCommandParameterProperty); }
            set { SetValue(SaveCommandParameterProperty, value); }
        }
        public static readonly BindableProperty SaveCommandParameterProperty = BindableProperty.Create(nameof(SaveCommandParameter), typeof(object), typeof(CustomEntry), default(object));

        public bool IsSaving
        {
            get { return (bool)GetValue(IsSavingProperty); }
            set { SetValue(IsSavingProperty, value); }
        }
        public static readonly BindableProperty IsSavingProperty = BindableProperty.Create(nameof(IsSaving), typeof(bool), typeof(CustomEntry), false);

        public bool IsSavingEnabled
        {
            get { return (bool)GetValue(IsSavingEnabledProperty); }
            set { SetValue(IsSavingEnabledProperty, value); }
        }
        public static readonly BindableProperty IsSavingEnabledProperty = BindableProperty.Create(nameof(IsSavingEnabled), typeof(bool), typeof(CustomEntry), true);
    }
}
