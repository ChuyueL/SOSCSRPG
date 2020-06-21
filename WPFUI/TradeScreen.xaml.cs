using Engine.Models;
using Engine.ViewModels;
using System;
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
using System.Windows.Shapes;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for TradeScreen.xaml
    /// </summary>
    public partial class TradeScreen : Window
    {
        //Wraps data context as a GameSession object. 
        //Gives us a way for us to treat the data context as a GameSession object. Means we can use all the
        //properties and don't have to cast the data context as a GameSession object in multiple places.
        public GameSession Session => DataContext as GameSession;

        public TradeScreen()
        {
            InitializeComponent();
        }

        private void OnClick_Sell(object sender, RoutedEventArgs e)
        {
            //Figure out what row the button was on. 
            //sender=what sent the event (the button). The button is part of a row so get its data context
            //and convert it to a GameItem (as each row should be a GameItem).
            GameItem item = ((FrameworkElement)sender).DataContext as GameItem;

            //If we were able to convert the raiser of the event into a GameItem
            if (item != null)
            {
                Session.CurrentPlayer.Gold += item.Price;
                Session.CurrentTrader.AddItemToInventory(item);
                Session.CurrentPlayer.RemoveItemFromInventory(item);
            }
        }

        private void OnClick_Buy(object sender, RoutedEventArgs e)
        {
            GameItem item = ((FrameworkElement)sender).DataContext as GameItem;
            
            if (item != null)
            {
                if (Session.CurrentPlayer.Gold >= item.Price)
                {
                    Session.CurrentPlayer.Gold -= item.Price;
                    Session.CurrentTrader.RemoveItemFromInventory(item);
                    Session.CurrentPlayer.AddItemToInventory(item);
                }
                else
                {
                    MessageBox.Show("You do not have enough gold!");
                }
            }
        }

        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
