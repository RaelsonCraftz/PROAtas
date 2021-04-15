using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using PROAtas.Mobile.Core;
using PROAtas.Mobile.Services.Shared;
using PROAtas.Mobile.Views;
using Xamarin.Craftz.Services;
using Xamarin.Forms;

namespace PROAtas
{
    public partial class App : Application
    {
        public App()
        {
            // Register Syncfusion community license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDI0OTU4QDMxMzkyZTMxMmUzMGdVc3hFemFHS051VkdHWkwxRHQyOXFMeGllNGR1Q216dkxDRDhSM1BXQzA9");

            InitializeComponent();

            // Shared Services
            DependencyService.Register<PermissionService>();
            DependencyService.Register<DataService>();
            DependencyService.Register<LogService>();

            //No longer used
            //DependencyService.Register<MockMinuteStorage>();

            // Shell navigation Routes
            Routing.RegisterRoute(Routes.Minute, typeof(MinutePage));

            // App Center
#if DEBUG
            AppCenter.Start("ios=57c152a7-d262-477b-b1bd-66423e9ae043;android=826adf62-6f61-4385-a18e-2c923465f6f4", typeof(Analytics), typeof(Crashes));
#else
            AppCenter.Start("ios=851675c4-638e-476a-a5df-23a124edf212;android=8abe7872-57be-4123-a058-1095f557c264", typeof(Analytics), typeof(Crashes));
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

            var version = App.Current.Properties[AppConsts.Version]?.ToString();
            if (version != "v14.1")
            {
                Current.Properties[AppConsts.Version] = "v14.1";
                Current.SavePropertiesAsync();

                Application.Current.MainPage.DisplayAlert($"Versão 14.1", "Olá!\r\n\r\nEsta é a versão 14.1 do PRO Atas, que agora se chama Cosmo Atas. As principais mudanças são:\r\n\r\n- Visual redesenhado e mais moderno;\r\n- Experiência de usuário melhorada;\r\n- Implementação de serviços que permitem rastrear possíveis erros que estejam ocorrendo no aplicativo;\r\n- Alguns bugs foram corrigidos;\r\n- Hotfix 1: a cor das entradas de texto foi corrigida;\r\n- Hotfix 2: em algumas situações (principalmente PDF), o aplicativo não conseguia gerar o documento. Isto foi corrigido\r\n- Problema conhecido: para alguns usuários é possível que, logo após iniciar, o app fique com uma tela preta. Caso isso aconteça, favor enviar um e-mail para o desenvolvedor relatando o ocorrido;\r\n\r\nEste aplicativo é desenvolvido por Raelson por iniciativa própria. Caso precise entrar em contato, enviar e-mail para raelsoncraftz@gmail.com", "Legal!");
            }
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
