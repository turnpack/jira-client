﻿using System;
using System.Reflection;
using Autofac;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Gtk;
using JiraAssistant.Logic.Services;
using JiraAssistant.Logic.Settings;

namespace JiraAssistant.Mono
{
	class Bootstrap
	{
		public static void Main(string[] args)
		{
			AppDomain.CurrentDomain.DomainUnload += (sender, e) =>
			{
				IocContainer.Dispose();
			};

			DispatcherHelper.Initialize();
			InitializeIoc();

			Application.Init();
			var win = IocContainer.Resolve<MainWindow>();

			win.Show();
			Application.Run();
		}

		public static IContainer IocContainer { get; private set; }

		private static void InitializeIoc()
		{
			if (IocContainer != null)
				return;

			var builder = new ContainerBuilder();

			var logicAssembly = Assembly.Load("JiraAssistant.Logic");

			builder.RegisterAssemblyTypes(logicAssembly)
			   .InNamespace("JiraAssistant.Logic.ViewModels")
			   .AsSelf()
			   .AsImplementedInterfaces();

			builder.RegisterAssemblyTypes(logicAssembly)
			   .InNamespace("JiraAssistant.Logic.ContextlessViewModels")
			   .AsSelf()
			   .AsImplementedInterfaces()
			   .SingleInstance();

			builder.RegisterAssemblyTypes(logicAssembly)
			   .InNamespace("JiraAssistant.Logic.Settings")
			   .Except<SettingsBase>()
			   .AsSelf()
			   .SingleInstance();

			builder.RegisterAssemblyTypes(logicAssembly)
			   .InNamespace("JiraAssistant.Logic.Services")
			   .AsImplementedInterfaces()
			   .AsSelf()
			   .SingleInstance();

			builder.RegisterAssemblyTypes(logicAssembly)
			   .InNamespace("JiraAssistant.Logic.Services.Daemons")
			   .AsImplementedInterfaces()
			   .AsSelf()
			   .SingleInstance()
			   .AutoActivate();

			builder.RegisterType<JiraApi>()
			   .SingleInstance()
			   .AsImplementedInterfaces()
			   .AutoActivate();

			builder.RegisterType<NavigationService>()
			   .SingleInstance()
			   .AutoActivate();

			builder.RegisterType<DesktopNotificationsService>()
			   .SingleInstance()
			   .AutoActivate();

			builder.RegisterType<Messenger>()
			   .As<IMessenger>()
			   .SingleInstance();

			builder.RegisterType<MainWindow>()
			   .AsSelf()
			   .SingleInstance();

			IocContainer = builder.Build();
		}
	}
}