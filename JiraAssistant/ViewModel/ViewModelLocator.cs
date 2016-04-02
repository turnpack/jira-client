using Autofac;
using JiraAssistant.Services;
using JiraAssistant.Services.Resources;
using JiraAssistant.Services.Settings;

namespace JiraAssistant.ViewModel
{
   public class ViewModelLocator
   {
      private AgileBoardSelectViewModel _agileBoardSelect;
      private MainMenuViewModel _mainMenu;

      public ViewModelLocator()
      {
         BuildContainer();
      }

      public static IContainer IocContainer { get; set; }

      public MainViewModel Main
      {
         get { return IocContainer.Resolve<MainViewModel>(); }
      }

      public LoginPageViewModel Login
      {
         get { return IocContainer.Resolve<LoginPageViewModel>(); }
      }

      public JiraSessionViewModel JiraSession
      {
         get { return IocContainer.Resolve<JiraSessionViewModel>(); }
      }

      public MainMenuViewModel MainMenu
      {
         get
         {
            _mainMenu = _mainMenu ?? new MainMenuViewModel(IocContainer);
            return _mainMenu;
         }
      }

      public AgileBoardSelectViewModel AgileBoardSelect
      {
         get
         {
            _agileBoardSelect = _agileBoardSelect ?? new AgileBoardSelectViewModel(IocContainer);
            return _agileBoardSelect;
         }
      }

      public GraveyardSettings GraveyardSettings
      {
         get { return IocContainer.Resolve<GraveyardSettings>(); }
      }

      public UpdateSettings UpdateSettings
      {
         get { return IocContainer.Resolve<UpdateSettings>(); }
      }

      public IssuesSettings IssuesSettings
      {
         get { return IocContainer.Resolve<IssuesSettings>(); }
      }

      public AnalysisSettings AnalysisSettings
      {
         get { return IocContainer.Resolve<AnalysisSettings>(); }
      }

      public static void Cleanup()
      {
         IocContainer.Dispose();
      }

      private void BuildContainer()
      {
         if (IocContainer != null)
            return;

         var builder = new ContainerBuilder();

         builder.RegisterAssemblyTypes(GetType().Assembly)
            .InNamespaceOf<ViewModelLocator>()
            .Except<ViewModelLocator>()
            .AsSelf()
            .AsImplementedInterfaces()
            .SingleInstance();

         builder.RegisterAssemblyTypes(GetType().Assembly)
            .InNamespaceOf<BaseRestService>()
            .Except<BaseRestService>()
            .AsSelf()
            .AsImplementedInterfaces()
            .SingleInstance();

         builder.RegisterAssemblyTypes(GetType().Assembly)
            .InNamespaceOf<SettingsBase>()
            .Except<SettingsBase>()
            .AsSelf()
            .SingleInstance();

         builder.RegisterAssemblyTypes(GetType().Assembly)
            .InNamespace("JiraAssistant.Services")
            .AsImplementedInterfaces()
            .AsSelf()
            .SingleInstance();

         builder.RegisterType<UpdateService>().AutoActivate();

         IocContainer = builder.Build();
      }
   }
}