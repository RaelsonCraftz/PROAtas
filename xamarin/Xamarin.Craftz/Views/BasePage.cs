using Craftz.Core;
using Craftz.ViewModel;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;

namespace Craftz.Pages
{
    public class BasePage : ContentPage
    {
        protected virtual void OnStart()
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
                base.OnDisappearing();
        }
    }

    public class BasePage<TViewModel> : ContentPage where TViewModel : BaseViewModel
    {
        public BasePage()
        {

        }

        protected virtual void OnStart()
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
                base.OnDisappearing();
        }
    }

    [QueryProperty(nameof(model), "model")]
    public class BasePage<TViewModel, TModel> : ContentPage
        where TViewModel : BaseViewModel<TModel>
        where TModel : class
    {
        public BasePage()
        {
            Visual = VisualMarker.Material;
        }

        protected virtual void OnStart()
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
                base.OnDisappearing();
        }

        #region Navigation Properties

        public string model
        {
            set
            {
                string jsonStr = Uri.UnescapeDataString(value);
                var model = JsonConvert.DeserializeObject<TModel>(jsonStr);

                Model = model;
            }
        }

        #endregion
    }
}