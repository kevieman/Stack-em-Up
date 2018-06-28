using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Stack_m_up
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        readonly Game1 _game;
        int x = 0;

        public GamePage()
        {
            this.InitializeComponent();

            // Create the game.
            var launchArguments = string.Empty;
            _game = MonoGame.Framework.XamlGame<Game1>.Create(launchArguments, Window.Current.CoreWindow, swapChainPanel);

            Player_Amount_Slider.Value = 2;
            Player_Amount_Slider.Minimum = 2;
            Main_Volume_Slider.Value = 100;
            Music_Volume_Slider.Value = 100;
            SFX_Volume_Slider.Value = 100;
        }

        // Disables and enables all the UI elements for when Start Game button is clicked.
        private void Start_Game_Click(object sender, RoutedEventArgs e) 
        {
            StartGame_Button.Visibility = Visibility.Collapsed;
            Settings_Button.Visibility = Visibility.Collapsed;
            Credits_Button.Visibility = Visibility.Collapsed;
            Quit_Button.Visibility = Visibility.Collapsed;

            Back_Button.Visibility = Visibility.Visible;

            Start_Survival_Button.Visibility = Visibility.Visible;
            Player_Amount_Text.Visibility = Visibility.Visible;
            Player_Amount_Slider.Visibility = Visibility.Visible;
            Object_Set_Text.Visibility = Visibility.Visible;
            Object_Set_Slider.Visibility = Visibility.Visible;

            _game.Click();
        }

        // Disables and enables all the UI elements for when Stettings button is clicked.
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            StartGame_Button.Visibility = Visibility.Collapsed;
            Settings_Button.Visibility = Visibility.Collapsed;
            Credits_Button.Visibility = Visibility.Collapsed;
            Quit_Button.Visibility = Visibility.Collapsed;

            Back_Button.Visibility = Visibility.Visible;
            Main_Volume_Slider.Visibility = Visibility.Visible;
            Music_Volume_Slider.Visibility = Visibility.Visible;
            SFX_Volume_Slider.Visibility = Visibility.Visible;
            Main_Volume_Text.Visibility = Visibility.Visible;
            Music_Volume_Text.Visibility = Visibility.Visible;
            SFX_Volume_Text.Visibility = Visibility.Visible;

            _game.Click();
        }

        private void Credits_Click(object sender, RoutedEventArgs e)
        {

        }

        // Disables and enables all the UI elements for when Back button is clicked.
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            StartGame_Button.Visibility = Visibility.Visible;
            Settings_Button.Visibility = Visibility.Visible;
            Credits_Button.Visibility = Visibility.Visible;
            Quit_Button.Visibility = Visibility.Visible;

            Back_Button.Visibility = Visibility.Collapsed;
            Back_Button2.Visibility = Visibility.Collapsed;

            Main_Volume_Slider.Visibility = Visibility.Collapsed;
            Music_Volume_Slider.Visibility = Visibility.Collapsed;
            SFX_Volume_Slider.Visibility = Visibility.Collapsed;
            Main_Volume_Text.Visibility = Visibility.Collapsed;
            Music_Volume_Text.Visibility = Visibility.Collapsed;
            SFX_Volume_Text.Visibility = Visibility.Collapsed;

            Start_Survival_Button.Visibility = Visibility.Collapsed;
            Player_Amount_Text.Visibility = Visibility.Collapsed;
            Player_Amount_Slider.Visibility = Visibility.Collapsed;
            Object_Set_Text.Visibility = Visibility.Collapsed;
            Object_Set_Slider.Visibility = Visibility.Collapsed;

            Background.Visibility = Visibility.Visible;
            Logo.Visibility = Visibility.Visible;

            _game.Click();
        }

        // Disables and enables all the UI elements for when Start_Survival_Click button is clicked.
        private void Start_Survival_Click(object sender, RoutedEventArgs e)
        {
            StartGame_Button.Visibility = Visibility.Collapsed;
            Settings_Button.Visibility = Visibility.Collapsed;
            Credits_Button.Visibility = Visibility.Collapsed;
            Quit_Button.Visibility = Visibility.Collapsed;

            Back_Button.Visibility = Visibility.Collapsed;
            Back_Button2.Visibility = Visibility.Visible;

            Main_Volume_Slider.Visibility = Visibility.Collapsed;
            Music_Volume_Slider.Visibility = Visibility.Collapsed;
            SFX_Volume_Slider.Visibility = Visibility.Collapsed;
            Main_Volume_Text.Visibility = Visibility.Collapsed;
            Music_Volume_Text.Visibility = Visibility.Collapsed;
            SFX_Volume_Text.Visibility = Visibility.Collapsed;

            Start_Survival_Button.Visibility = Visibility.Collapsed;
            Player_Amount_Text.Visibility = Visibility.Collapsed;
            Player_Amount_Slider.Visibility = Visibility.Collapsed;
            Object_Set_Text.Visibility = Visibility.Collapsed;
            Object_Set_Slider.Visibility = Visibility.Collapsed;

            Background.Visibility = Visibility.Collapsed;
            Logo.Visibility = Visibility.Collapsed;
            
            _game.Click();
            _game.startGame( Convert.ToInt32(Player_Amount_Slider.Value) ); // Start game
        }

        // Disables and enables all the UI elements for when Quit button is clicked.
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
            _game.Click();
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {
            
        }

        // Changes the master volume when the slider has been used.
        private void changeMasterVolume(object sender, RoutedEventArgs e)
        {
            float percentage = (float)(Main_Volume_Slider.Value / 100);
            _game.updateMasterVolume(percentage);   // Update the master Volume
            
            if(x == 2)
            {
                _game.sliderClick(percentage);
                x = 0;
            } else
            {
                x++;
            }
        }

        // Changes the SFX volume when the slider has been used.
        private void changeSfxVolume(object sender, RoutedEventArgs e)
        {
            float percentage = (float)(SFX_Volume_Slider.Value / 100);
            _game.updateSfxVolume(percentage);   // Update the SFX Volume

            if (x == 2)
            {
                _game.sliderClick(percentage);
                x = 0;
            }
            else
            {
                x++;
            }
        }

        // Changes the music volume when the slider has been used.
        private void changeMusicVolume(object sender, RoutedEventArgs e)
        {
            float percentage = (float)(Music_Volume_Slider.Value / 100);
            _game.updateMusicVolume(percentage);   // Update the music Volume

            if (x == 2)
            {
                _game.sliderClickMusic(percentage);
                x = 0;
            }
            else
            {
                x++;
            }
        }
    }
}
