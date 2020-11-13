using PROAtas.Services;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PROAtas.Controls
{
    public class DownloadEntry : Entry
    {
        #region Service Container

        private readonly IDownloadService downloadService = App.Current.downloadService;

        #endregion

        private CancellationTokenSource cancellationTokenSource;
        public bool IsSavingEnabled;

        protected override void OnTextChanged(string oldTextValue, string newTextValue)
        {
            base.OnTextChanged(oldTextValue, newTextValue);

            if (IsSavingEnabled)
            {
                // Cancels the current execution
                if (cancellationTokenSource != null) cancellationTokenSource.Cancel();

                // Creates a new instance for the cancellation token
                IsSaving = true;
                cancellationTokenSource = new CancellationTokenSource();

                // Executes a background operation
                Task.Run(async () =>
                {
                    // Takes the token reference reference that will be cancelled after the next character inserted
                    var token = cancellationTokenSource.Token;

                    // Getting the bytes from the url that was inserted on the text
                    var bytes = await downloadService.DownloadImageUrl(newTextValue);

                    // If the token wasn't cancelled (when another character is inserted), send it to the ViewModel
                    if (!token.IsCancellationRequested)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            SaveCommand?.Execute(bytes);

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
        public static readonly BindableProperty SaveCommandProperty = BindableProperty.Create(nameof(SaveCommand), typeof(ICommand), typeof(DownloadEntry), default(ICommand));

        public bool IsSaving
        {
            get { return (bool)GetValue(IsSavingProperty); }
            set { SetValue(IsSavingProperty, value); }
        }
        public static readonly BindableProperty IsSavingProperty = BindableProperty.Create(nameof(IsSaving), typeof(bool), typeof(DownloadEntry), false);
    }
}
