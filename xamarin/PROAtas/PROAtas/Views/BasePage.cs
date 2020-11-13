using Newtonsoft.Json;
using PROAtas.ViewModels;
using System;
using Xamarin.Forms;

namespace PROAtas.Views
{
    public class BasePage : ContentPage
    {
        protected virtual void OnStart()
        {

        }

        protected virtual void OnLeave()
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            OnStart();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            OnLeave();
        }
    }

    public class BasePage<TViewModel> : ContentPage where TViewModel : BaseViewModel
    {
        public BasePage()
        {
            Visual = VisualMarker.Material;
        }

        protected virtual void OnStart()
        {

        }

        protected virtual void OnLeave()
        {

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
            base.OnDisappearing();

            OnLeave();
            ViewModel?.Leave();
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

        protected virtual void OnLeave()
        {

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
            base.OnDisappearing();

            OnLeave();
            ViewModel?.Leave();
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
