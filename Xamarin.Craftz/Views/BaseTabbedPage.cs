using Craftz.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Craftz.Views
{
    public class BaseTabbedPage : TabbedPage
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

    public class BaseTabbedPage<TViewModel> : TabbedPage where TViewModel : BaseViewModel
    {
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
    public class BaseTabbedPage<TViewModel, TModel> : TabbedPage
        where TViewModel : BaseViewModel<TModel>
        where TModel : class
    {
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
