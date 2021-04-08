using Craftz.ViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Craftz.Views
{
    public class BaseDialog : PopupPage
    {
        public Action OnResult { get; set; }

        public BaseDialog()
        {

        }

        public BaseDialog(Action onResult)
        {
            OnResult = onResult;
        }

        protected virtual void OnStart()
        {

        }

        protected virtual void OnLeave()
        {

        }

        protected virtual bool CanLeave()
        {
            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            OnStart();
        }

        protected override void OnDisappearing()
        {
            if (CanLeave())
            {
                OnLeave();
                base.OnDisappearing();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (CanLeave())
                return base.OnBackButtonPressed();
            else
                return true;
        }
    }

    public class BaseDialog<TViewModel> : PopupPage where TViewModel : BaseViewModel
    {
        public Action OnResult { get; set; }

        public BaseDialog()
        {
            
        }

        public BaseDialog(Action onResult)
        {
            OnResult = onResult;
        }

        protected virtual void OnStart()
        {

        }

        protected virtual void OnLeave()
        {

        }

        protected virtual bool CanLeave()
        {
            return true;
        }

        public TViewModel ViewModel
        {
            get { return (TViewModel)BindingContext; }
            set { BindingContext = value; }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            OnStart();
            ViewModel?.Initialize();
        }

        protected override void OnDisappearing()
        {
            if (CanLeave() && (ViewModel?.CanLeave() ?? true))
            {
                OnLeave();
                ViewModel?.Leave();
                base.OnDisappearing();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (CanLeave() && (ViewModel?.CanLeave() ?? true))
                return base.OnBackButtonPressed();
            else
                return true;
        }
    }

    public class BaseDialog<TViewModel, TModel> : PopupPage
        where TViewModel : BaseViewModel<TModel>
        where TModel : class
    {
        public Action<TModel> OnResult { get; set; }

        public BaseDialog()
        {
            Visual = VisualMarker.Material;
        }

        public BaseDialog(TModel model) : this()
        {
            Model = model;
        }

        public BaseDialog(TModel model, Action<TModel> onResult) : this(model)
        {
            OnResult = onResult;
        }

        protected virtual void OnStart()
        {

        }

        protected virtual void OnLeave()
        {

        }

        protected virtual bool CanLeave()
        {
            return true;
        }

        public TViewModel ViewModel
        {
            get { return (TViewModel)BindingContext; }
            set { BindingContext = value; }
        }

        public TModel Model
        {
            get => _model;
            set { _model = value; }
        }
        private TModel _model;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            OnStart();
            ViewModel?.Initialize(Model);
        }

        protected override void OnDisappearing()
        {
            if (CanLeave() && (ViewModel?.CanLeave() ?? true))
            {
                OnLeave();
                ViewModel?.Leave();
                base.OnDisappearing();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (CanLeave() && (ViewModel?.CanLeave() ?? true))
                return base.OnBackButtonPressed();
            else
                return true;
        }
    }
}
