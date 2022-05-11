using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TankBrowser.MVVM.Model;
using TankBrowser.MVVM;


namespace TankBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public int Index = 0;
        public string Source = System.IO.Path.GetFullPath(Environment.CurrentDirectory);
        public Tank Maus= new Tank("Maus", "Pojazd był projektowany od czerwca 1942 do lipca 1944 roku. Z łączną masą sięgającą 188 ton był to najcięższy i największy czołg, jaki kiedykolwiek skonstruowano. Stworzono tylko 2 prototypy i zaledwie jeden z nich dysponował uzbrojoną wieżą.", "maus");
        Tank Mvy = new Tank("M-V-Y", "W czerwcu 1953 roku przedsiębiorstwo H.L. Yoh Company Inc. przedstawiło siedem prototypów dobrze zapowiadającego się czołgu. Jeden z nich nazwano M-V-Y. Propozycja dotyczyła stworzenia pojazdu z niezwykłym przedziałem bojowym: dowódca i celowniczy znajdowali się w bardzo wąskiej, oscylacyjnej wieży, a ładowniczy – w kadłubie. Pociski były przenoszone z kadłuba do wieży przez wąski kanał u podstawy wieży. Zawieszenie miało mieć poziome amortyzatory i rezerwową gąsienicę wewnętrzną, która miała pozwalać na opuszczenie pola bitwy przez pojazd w przypadku uszkodzenia głównej gąsienicy. Czołg wyposażono w działo główne i pięć karabinów maszynowych. Nigdy nie został zbudowany.", "m-v-y");
        Tank Pl50tppr = new Tank("50TP PR", "Szkic czołgu ciężkiego opracowanego przez kadeta Tadeusza Tyszkiewicza z Wojskowej Akademii Technicznej w Warszawie na początku lat 50. Nowy pojazd miał ważyć do 50 ton. Istniał tylko w planach.", "50tp_pr");
        public List<Tank> TankList = new List<Tank> { };

        

        public void reload(int index)
        {
            string PhotoPath = $@"C:/Users/repca/Desktop/Studia/Prototypy Czołgow/TanksPrototypes/source/TanksPrototypes/bin/Debug/images/{TankList[index].ImageName.ToLower()}.png";
            //tankImage.Source = new BitmapImage(new Uri(PhotoPath, UriKind.Relative));
            //tankImage.Source = new BitmapImage(new Uri($"C:/Users/repca/Desktop/Studia/Prototypy Czołgow/TanksPrototypes/source/TanksPrototypes/bin/Debug/images/{TankList[index].ImageName.ToLower()}.png"));
            //titleName.Text = TankList[index].Name.Replace("_", " ");
            
            //DescribeTank.Text = TankList[index].Description;
        }
        public MainWindow()
        {
            InitializeComponent();
            TankList.Add(Maus);
            TankList.Add(Mvy);
            TankList.Add(Pl50tppr);
            reload(Index);
            
        }
        private void titleLoad(object sender, RoutedEventArgs e)
        {
            //tankImage.Source = @"C:\Users\repca\Desktop\Studia\Prototypy Czołgow\TanksPrototypes\source\images\50tp_pr";
            reload(this.Index);
        }
        private void Decrement_Btn_click(object sender, RoutedEventArgs e)
        {
            if (Index - 1 < 0)
                Index = TankList.Count - 1;
            else
                Index--;
            reload(this.Index);
        }

        private void Increment_Btn_click(object sender, RoutedEventArgs e)
        {
            if (Index + 1 > TankList.Count - 1)
                Index = 0;
            else
                Index++;
            reload(this.Index);
        }
        private void ExitButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

    }
}
