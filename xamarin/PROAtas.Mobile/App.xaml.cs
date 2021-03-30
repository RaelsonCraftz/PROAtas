using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using PROAtas.Core;
using PROAtas.Services;
using PROAtas.Views;
using Xamarin.Craftz.Services;
using Xamarin.Forms;

namespace PROAtas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Shared Services
            DependencyService.Register<MockMinuteStorage>();
            DependencyService.Register<DataService>();
            DependencyService.Register<LogService>();

            // Platform specific Services
            Routing.RegisterRoute("minute", typeof(MinutePage));

            // App Center
#if DEBUG
            AppCenter.Start("ios=57c152a7-d262-477b-b1bd-66423e9ae043;android=826adf62-6f61-4385-a18e-2c923465f6f4", typeof(Analytics), typeof(Crashes));
#else
            AppCenter.Start("ios=8abe7872-57be-4123-a058-1095f557c264;android=3498baf2-e916-450a-affb-01f49bb2071a", typeof(Analytics), typeof(Crashes));
#endif

            // Saving default preferences
            if (!Current.Properties.ContainsKey(AppConsts.UserName)) Current.Properties[AppConsts.UserName] = "Novo Usuário";
            if (!Current.Properties.ContainsKey(AppConsts.OrganizationName)) Current.Properties[AppConsts.OrganizationName] = "Nova Organização";
            if (!Current.Properties.ContainsKey(AppConsts.FontSize)) Current.Properties[AppConsts.FontSize] = "14";
            if (!Current.Properties.ContainsKey(AppConsts.FontFamily)) Current.Properties[AppConsts.FontFamily] = "Times New Roman";
            if (!Current.Properties.ContainsKey(AppConsts.MarginLeft)) Current.Properties[AppConsts.MarginLeft] = "3";
            if (!Current.Properties.ContainsKey(AppConsts.MarginTop)) Current.Properties[AppConsts.MarginTop] = "2";
            if (!Current.Properties.ContainsKey(AppConsts.MarginRight)) Current.Properties[AppConsts.MarginRight] = "3";
            if (!Current.Properties.ContainsKey(AppConsts.MarginBottom)) Current.Properties[AppConsts.MarginBottom] = "2";
            if (!Current.Properties.ContainsKey(AppConsts.Version)) Current.Properties[AppConsts.Version] = "0";
            if (!Current.Properties.ContainsKey(AppConsts.SelectedMinuteImage)) Current.Properties[AppConsts.SelectedMinuteImage] = "0";
            Current.SavePropertiesAsync();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
