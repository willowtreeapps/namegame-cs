using MvvmCross.WindowsUWP.Views;
using System;
using System.Threading.Tasks;
using WillowTree.NameGame.Core.Models;
using WillowTree.NameGame.Core.Services;
using WillowTree.NameGame.Core.ViewModels;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace WillowTree.NameGame.Win
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : MvxWindowsPage
    {       
        public new MainViewModel ViewModel
        {
            get { return base.ViewModel as MainViewModel; }
            set { base.ViewModel = value; }
        }

        public MainView()
        {
            this.InitializeComponent();
        }

        private void image1_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            IsCorrect(image1, redXImage1, greenCheckImage1, txtImage1);
        }

        private void image2_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            IsCorrect(image2, redXImage2, greenCheckImage2, txtImage2);
        }

        private void image3_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            IsCorrect(image3, redXImage3, greenCheckImage3, txtImage3);
        }

        private void image4_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            IsCorrect(image4, redXImage4, greenCheckImage4, txtImage4);
        }

        private void image5_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            IsCorrect(image5, redXImage5, greenCheckImage5, txtImage5);           
        }

        private void image6_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            IsCorrect(image6, redXImage6, greenCheckImage6, txtImage6);            
        }

        /// <summary>
        /// Checks if the guess is correct.
        ///     If correct, plus 1 for Correct total
        ///     If incorrect, plus 1 for Correct total
        /// </summary>
        private void setScore(bool isCorrectGuess, Image redX, Image greenCheck)
        {
            if (isCorrectGuess 
                && redX.Visibility == Visibility.Collapsed 
                && greenCheck.Visibility == Visibility.Collapsed)    
            {
                txtCorrect.Text = (Convert.ToInt32(txtCorrect.Text) + 1).ToString();
            }
            else
            {
                txtIncorrect.Text = (Convert.ToInt32(txtIncorrect.Text) + 1).ToString();
            }
        }

        /// <summary>
        /// Business logic for handling answers. 
        ///     If correct, set the 'correct' score, make the 'greenCheck' Image visible, wait three seconds
        ///         then reset the players displayed
        ///     If incorrect, set the 'incorrect' score, make the 'redX' Image visible
        /// </summary>
        private async void IsCorrect(Image guess, Image redX, Image greenCheck, TextBlock imageName)
        {       
            bool isCorrect = false;
            imageName.Visibility = Visibility.Visible;

            if (txtWhoIs.Text == guess.Tag.ToString())
            {
                isCorrect = true;

                setScore(isCorrect, redX, greenCheck);
                greenCheck.Visibility = Visibility.Visible;
                await Task.Delay(3000);
                ViewModel.AddPeople();
                resetTiles();
            }
            else
            {
                setScore(isCorrect, redX, greenCheck);
                redX.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Hides the images: player name text, redX, & greenCheck images
        /// </summary>
        private void resetTiles()
        {
            txtImage1.Visibility = Visibility.Collapsed;
            txtImage2.Visibility = Visibility.Collapsed;
            txtImage3.Visibility = Visibility.Collapsed;
            txtImage4.Visibility = Visibility.Collapsed;
            txtImage5.Visibility = Visibility.Collapsed;
            txtImage6.Visibility = Visibility.Collapsed;
            redXImage1.Visibility = Visibility.Collapsed;
            redXImage2.Visibility = Visibility.Collapsed;
            redXImage3.Visibility = Visibility.Collapsed;
            redXImage4.Visibility = Visibility.Collapsed;
            redXImage5.Visibility = Visibility.Collapsed;
            redXImage6.Visibility = Visibility.Collapsed;
            greenCheckImage1.Visibility = Visibility.Collapsed;
            greenCheckImage2.Visibility = Visibility.Collapsed;
            greenCheckImage3.Visibility = Visibility.Collapsed;
            greenCheckImage4.Visibility = Visibility.Collapsed;
            greenCheckImage5.Visibility = Visibility.Collapsed;
            greenCheckImage6.Visibility = Visibility.Collapsed;
        }
    }
}

