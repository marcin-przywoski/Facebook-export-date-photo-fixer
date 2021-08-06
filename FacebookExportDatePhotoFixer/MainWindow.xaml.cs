﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ookii.Dialogs.Wpf;
using System.ComponentModel;
using FacebookExportDatePhotoFixer.Data;

namespace FacebookExportDatePhotoFixer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker _backgroundWorker = new BackgroundWorker();
        private BackgroundWorker _listUpdateWorker = new BackgroundWorker();
        FacebookExport facebookExport = new FacebookExport();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SourceLocation_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog chooseFolder = new VistaFolderBrowserDialog();
            bool? isBrowserDialogOpened = chooseFolder.ShowDialog();
            if (isBrowserDialogOpened == true)
            {
                facebookExport.Location = chooseFolder.SelectedPath + "/";
                SourceLocationLabel.Content = SourceLocationLabel.Content + chooseFolder.SelectedPath + "/";
                SourceLocationLabel.ToolTip = SourceLocationLabel.Content;
            }
        }

        private void DestinationLocation_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog chooseFolder = new VistaFolderBrowserDialog();
            bool? isBrowserDialogOpened = chooseFolder.ShowDialog();
            if (isBrowserDialogOpened == true)
            {
                facebookExport.Destination = chooseFolder.SelectedPath + "/";
                DestinationLocationLabel.Content = DestinationLocationLabel.Content + chooseFolder.SelectedPath + "/";
                DestinationLocationLabel.ToolTip = DestinationLocationLabel.Content;
            }
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(facebookExport.Destination) || String.IsNullOrEmpty(facebookExport.Location))
            {
                MessageBox.Show("One of the paths have not been selected!");
            }
            else
            {
                _backgroundWorker.DoWork += _backgroundWorker_DoWork;
                _backgroundWorker.RunWorkerAsync();
                _listUpdateWorker.DoWork += _listUpdateWorker_DoWork;
                _listUpdateWorker.RunWorkerAsync();

            }


        }

        private void _listUpdateWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //for(int i = 0; i<100; i++) {
            //    System.Threading.Thread.Sleep(500);
            //    Dispatcher.Invoke(() =>
            //    {
            //        OutputLog.Items.Add(i);
            //        OutputLog.SelectedIndex = OutputLog.Items.Count - 1;
            //    });
            //}
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            facebookExport.GetLanguage();
            facebookExport.GetHtmlFiles(Progress, OutputLog);
            facebookExport.GetMessagesFromHtmlFiles(Progress, OutputLog);
            facebookExport.ProcessHtmlFiles(Progress, OutputLog);


            //for(int i=0;i<100;i++)
            //{
            //    System.Threading.Thread.Sleep(1000);
            //    Dispatcher.Invoke(() =>
            //        {
            //        Progress.Value = i;

            //        });

            //}
        }
    }
}
