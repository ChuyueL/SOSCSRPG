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
        private GameSession _gameSession;
        public MainWindow()
        {
            InitializeComponent();

            _gameSession = new GameSession();

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
    }
}
