using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TankBrowser.MVVM.Model;



namespace TankBrowser.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy EditContentView.xaml
    /// </summary>
    public partial class EditContentView : UserControl
    {
        string FileName = "";
        string ImageMainSource = Directory.GetCurrentDirectory();
        public List<Tank> TankList;
        public EditContentView()
        {
            InitializeComponent();
            TankList = SQLiteAccess.ReadAllTanks();
            for (int i = 0; i < TankList.Count; i++)
                EditListViewTanks.Items.Add(TankList[i].Name);
            if(TankList.Count> 0)
                EditListViewTanks.SelectedIndex = 0;
            ReloadContentView();
        }

        public void ChangeTitleName()
        {
            int index = EditListViewTanks.SelectedIndex;
            if(index<0)
                return;
            EditListViewTanks.Text = TankList[index].Name;
        }
        public void ChangeDescription()
        {
            int index = EditListViewTanks.SelectedIndex;
            if(index<0)
                return;
            EditDescribeTank.Text = TankList[index].Description;
        }
        public void ChangeImage()
        {
            int index = EditListViewTanks.SelectedIndex;
            if (index < 0)
                return;
            string PhotoPath = $@"{ImageMainSource}\..\..\Debug\images\{TankList[index].ImageName.ToLower()}";
            EditedImage.Source = new BitmapImage(new Uri(PhotoPath));
        }
        public void ReloadContentView()
        {
            TankList = SQLiteAccess.ReadAllTanks();
            for (int i = 0; i < TankList.Count; i++)
                if (!EditListViewTanks.Items.Contains(TankList[i].Name))
                    EditListViewTanks.Items.Add(TankList[i].Name);
            ChangeTitleName();
            ChangeDescription();
            ChangeImage();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter =
                "Image Files (*.png; *.jpg)|*.png; *.jpg";

            if ((bool)dialog.ShowDialog())
            {
                var FileSource = dialog.FileName.Split('\\').ToList();
                FileName = FileSource[FileSource.Count-1];
                var bitmap = new BitmapImage(new Uri(dialog.FileName));
                var image = new System.Windows.Controls.Image { Source = bitmap };
                if (!File.Exists(@$"{ImageMainSource}\..\..\Debug\images\{FileName}"))
                    File.Copy(dialog.FileName, @$"{ImageMainSource}\..\..\Debug\images\{FileName}");
                EditedImage.Source = bitmap;

            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (FileName == "")
                FileName = TankList[EditListViewTanks.SelectedIndex].ImageName;
            var editedTank = new Tank(TankList[EditListViewTanks.SelectedIndex].Name,EditDescribeTank.Text, FileName);
            try
            {
                SQLiteAccess.UpdateTankData(editedTank);
                Logger.logMessage("Updated Tank Data");
                EditListViewTanks.IsEnabled = false;
                EditDescribeTank.Text = "Poprawnie zedytowano czołg. Wejdź do panelu review by zobaczyć zmiany";
                EditDescribeTank.BorderBrush = new SolidColorBrush(Color.FromRgb(124, 252, 0));
            }
            catch (Exception ex)
            {
                Logger.logMessage("Update Tank Data Error",ex);
            }
            
                
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var tank2Delete = TankList[EditListViewTanks.SelectedIndex];
            try
            {
                SQLiteAccess.DeleteTank(tank2Delete);
                EditListViewTanks.SelectedIndex = 0;
                Logger.logMessage("Correctly Deleted Tank");
                EditListViewTanks.IsEnabled = false;
                EditDescribeTank.Text = "Poprawnie usunięto czołg. Wejdź do panelu review by zobaczyć zmiany";
                EditDescribeTank.BorderBrush = new SolidColorBrush(Color.FromRgb(124, 252, 0));
                EditedImage.Source = null;

            }
            catch 
            {
                Logger.logMessage("Delete Tank Error");
            }

        }

        private void EditListViewTanks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReloadContentView();
        }
    }
}
