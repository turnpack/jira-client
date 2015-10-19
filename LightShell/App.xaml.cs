﻿using GalaSoft.MvvmLight.Threading;
using LightShell.Properties;
using System.Windows;

namespace LightShell
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application
   {
      static App()
      {
         DispatcherHelper.Initialize();
         if (Settings.Default.SettingsUpgradePending)
         {
            Settings.Default.Upgrade();
            Settings.Default.SettingsUpgradePending = false;
            Settings.Default.Save();
         }
      }
   }
}