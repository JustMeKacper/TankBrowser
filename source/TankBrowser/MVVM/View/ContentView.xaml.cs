using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using TankBrowser.MVVM.Model;


namespace TankBrowser.MVVM.View
{
    /// <summary>
    /// Logika interakcji dla klasy Content.xaml
    /// </summary>
    public partial class ContentView : UserControl
    {
        string ImageMainSource = Directory.GetCurrentDirectory();

        public List<Tank> TankList;
        public ContentView()
        {
            InitializeComponent();
            TankList = SQLiteAccess.ReadAllTanks();
            for (int i = 0; i < TankList.Count; i++)
                ListViewTanks.Items.Add(TankList[i].Name);
            ListViewTanks.SelectedIndex = 0;
            ReloadContentView();
        }
        public void ChangeTitleName()
        {
            int index = ListViewTanks.SelectedIndex;
            if (index < 0)
                return;
            ListViewTanks.Text = TankList[index].Name;
        }
        public void ChangeDescription()
        {
            int index = ListViewTanks.SelectedIndex;
            if (index < 0)
                return;
            DescribeTank.Text = TankList[index].Description;
        }
        public void ChangeImage()
        {
            int index = ListViewTanks.SelectedIndex;
            if (index < 0)
                return;
            string PhotoPath = $@"{ImageMainSource}\..\..\Debug\images\{TankList[index].ImageName.ToLower()}";
            tankImage.Source = new BitmapImage(new Uri(PhotoPath));
        }
        public void ReloadContentView()
        {
            TankList = SQLiteAccess.ReadAllTanks();
            for (int i = 0; i < TankList.Count; i++)
                if (!ListViewTanks.Items.Contains(TankList[i].Name))
                    ListViewTanks.Items.Add(TankList[i].Name);
            ChangeTitleName();
            ChangeDescription();
            ChangeImage();
        }

        private void ListViewTanks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReloadContentView();
        }
    }
}
