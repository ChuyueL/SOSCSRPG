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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Engine.EventArgs;
using Engine.ViewModels;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //readonly because only want to set value when declaring variable or in the constructor.
        private readonly GameSession _gameSession = new GameSession();
        public MainWindow()
        {
            InitializeComponent();

            //OnGameMessageRaised is a function to handle the event 
            _gameSession.OnMessageRaised += OnGameMessageRaised;

            DataContext = _gameSession; //DataContext is built in property for xaml window

        }

        //only used by main window
        //click event sends these 2 parameters - we won't use them but we need to accept them
        private void OnClick_MoveNorth(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveNorth();
        }
        private void OnClick_MoveWest(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveWest();
        }
        private void OnClick_MoveEast(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveEast();
        }
        private void OnClick_MoveSouth(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveSouth();
        }

        private void OnClick_AttackMonster(object sender, RoutedEventArgs e)
        {
            _gameSession.AttackCurrentMonster();
        }
        private void OnGameMessageRaised(object sender, GameMessageEventArgs e)
        {
            //Add a new block, which is a paragraph that contains a run (string of text)
            //with the message to the rich text box.
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));

            //Scroll to end of rich text box as we want player to always see the last messages.
            GameMessages.ScrollToEnd();
        }

        private void OnClick_DisplayTradeScreen(object sender, RoutedEventArgs e)
        {
            TradeScreen tradeScreen = new TradeScreen(); //instantiates a new TradeScreen window
            tradeScreen.Owner = this; //allows us to center the trade screen on the main window
            //Doesn't actually pass the _gameSession object, passes a reference to the object. The actual object
            //only exists in one place and any other place that uses it points to that same place in memory. 
            //Any changes we make to _gameSession in TradeScreen happen to this one _gameSession object.
            tradeScreen.DataContext = _gameSession; 
            //Could use Show() but it displays it in a nonmodal way (user would still be able to click on buttons
            //on MainWindow. ShowDialog() means that TradeScreen is modal - locks everything else in UI out so user
            //can't click on MainWindow.
            tradeScreen.ShowDialog();
        }
    }
}
