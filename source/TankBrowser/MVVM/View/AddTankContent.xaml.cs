using System;
using System.Collections.Generic;
using System.IO;
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
using TankBrowser.MVVM.Model;

namespace TankBrowser.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy AddTankContent.xaml
    /// </summary>
    public partial class AddTankContent : UserControl
    {
        string FileName = "";
        string ImageMainSource = Directory.GetCurrentDirectory();
        public List<Tank> TankList;
        public AddTankContent()
        {
            TankList = SQLiteAccess.ReadAllTanks();
            InitializeComponent();
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter =
                "Image Files (*.png; *.jpg)|*.png; *.jpg";

            if ((bool)dialog.ShowDialog())
            {
                var FileSource = dialog.FileName.Split('\\').ToList();
                FileName = FileSource[FileSource.Count - 1];
                var bitmap = new BitmapImage(new Uri(dialog.FileName));
                var image = new System.Windows.Controls.Image { Source = bitmap };
                if (!File.Exists(@$"{ImageMainSource}\..\images\{FileName}"))
                    File.Copy(dialog.FileName, @$"{ImageMainSource}\..\images\{FileName}");
                AddedImage.Source = bitmap;
            }
        }

        public void ChangeBackSuccessColors()
        {
            AddImageButton.BorderBrush = null;
            AddTankName.BorderBrush = null;
            AddDescribeTank.BorderBrush = null;
        }
        private void SaveAddButton_Click(object sender, RoutedEventArgs e)
        {
            bool isSthEmpty = AddDescribeTank.Text == "" || AddedImage.Source == null || AddTankName.Text == "";
            if (isSthEmpty)
            {
                if (AddDescribeTank.Text == "")
                    AddDescribeTank.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                else
                    AddDescribeTank.BorderBrush = null;
                if (AddTankName.Text == "")
                    AddTankName.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                else
                    AddTankName.BorderBrush = null;
                if (AddedImage.Source == null)
                    AddImageButton.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                else
                    AddImageButton.BorderBrush = null;
                return;
            }
            else
            {
               ChangeBackSuccessColors();

                Tank newTank = new Tank(AddTankName.Text, AddDescribeTank.Text, FileName);
                try
                {
                    SQLiteAccess.InsertTank(newTank);
                    Logger.logMessage("Correctly added to DB");
                    AddedImage.Source = null;
                    AddTankName.Text= "";
                    AddDescribeTank.Text = "";
                    AddImageButton.BorderBrush = new SolidColorBrush(Color.FromRgb(124, 252, 0));
                    AddTankName.BorderBrush = new SolidColorBrush(Color.FromRgb(124, 252, 0));
                    AddDescribeTank.BorderBrush = new SolidColorBrush(Color.FromRgb(124, 252, 0));
                }
                catch
                {
                    Logger.logMessage("Failed add to DB");
                }
            }
            
        }
    }
}
