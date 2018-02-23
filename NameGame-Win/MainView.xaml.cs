using MvvmCross.WindowsUWP.Views;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using WillowTree.NameGame.Core.Models;
using WillowTree.NameGame.Core.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Threading;
using Windows.UI;

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
			HideGuessControls();
			// load the first image 
			Window.Current.Activated += ((Win, Args) => GenerateTheNextImage());

		}


		public string FeaturedUser { get; private set; }
		public Profile CurrentProfile { get; private set; }

		private bool guessedCorrectly = false;

		// hide controls 
		private void HideGuessControls()
		{
			btnHelpMe.Visibility = Visibility.Collapsed;
			txtUserGuess.Visibility = Visibility.Collapsed;
			btnCheck.Visibility = Visibility.Collapsed;
			grdMultipleChoice.Children.Clear();
		}

		private void ShowGuessControls()
		{
			btnHelpMe.Visibility = Visibility.Visible;
			txtUserGuess.Visibility = Visibility.Visible;
			btnCheck.Visibility = Visibility.Visible;
		}

		private void ShowLoadingBar()
		{
			loading.Visibility = Visibility.Visible;
		}

		private void HideLoadingBar()
		{
			loading.Visibility = Visibility.Collapsed;
		}

		public void GenerateTheNextImage()
		{
			guessedCorrectly = false;
			HideGuessControls();
			Uri uri = null;
			Profile profile = null;

			do
			{
				profile = ViewModel.GetRandomProfile();
				CurrentProfile = profile;
			}
			while (!Uri.TryCreate("http:" + CurrentProfile.Headshot.Url, UriKind.Absolute, out uri));

			FeaturedUser = profile.FirstName + " " + profile.LastName;
			Image currentImage = new Image();
			BitmapImage bitmapSource = new BitmapImage();

			ShowLoadingBar();
			foreach (var imageChild in imageGrid.Children.OfType<Image>().Where(child => !child.Equals(currentImage)).ToArray())
				imageGrid.Children.Remove(imageChild);
			txtWhoAmI.Visibility = Visibility.Visible;
		
			txtWhoAmI.Text = "Loading...";

			try
			{
				bitmapSource.ImageOpened += (bitmapImage, args) =>
				{
					currentImage.SetValue(Grid.RowProperty, 0);
					currentImage.SetValue(Grid.ColumnProperty, 0);
					currentImage.SetValue(Grid.ColumnSpanProperty, 3);

					currentImage.HorizontalAlignment = HorizontalAlignment.Center;
					currentImage.VerticalAlignment = VerticalAlignment.Bottom;
					currentImage.Margin = new Thickness(10);

					txtWhoAmI.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Colors.Black);
					txtWhoAmI.Text = "Guess My Name!";
					ShowGuessControls();
					HideLoadingBar();
				};

				bitmapSource.ImageFailed += (bitmapImage, args) =>
				{
					ShowLoadingBar();
				};

				bitmapSource.UriSource = uri;
				currentImage.Source = bitmapSource;
			 	currentImage.Height = 400;

				imageGrid.Children.Add(currentImage);

			}
			catch (Exception ex)
			{
				txtWhoAmI.Text = $"Error: {ex.Message}";
				ShowLoadingBar();
			}
		}
		private bool IsGuessCorrect(string guess)
		{
			return string.Equals(guess.Trim(), FeaturedUser, StringComparison.CurrentCultureIgnoreCase);
		}
		/**
		 * handle the guess,  populate the label with a suitable message
		 * if correct wait then load next image
		 **/

		private void HandleGuess(string guess)
		{
			if (string.IsNullOrWhiteSpace(guess))
				return;

			if (guessedCorrectly)
			{
				txtWhoAmI.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Colors.Red);
				txtWhoAmI.Text = $"You already got it right.  It's {FeaturedUser}!";
			
				return;
			}

			if (!IsGuessCorrect(guess))
			{
				txtWhoAmI.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Colors.Red);
				txtWhoAmI.Text = "Oh No, Try again!";
				return;
			}
			txtWhoAmI.Foreground = new Windows.UI.Xaml.Media.SolidColorBrush(Colors.ForestGreen);
			txtWhoAmI.Text = String.Format($"Correct! Hi, my Name is : {guess}!");
		

			var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
			timer.Start();

			//delay loading next page for 2 seconds
			timer.Tick += (sender, args) =>
			{
				timer.Stop();
				txtUserGuess.Text = "";

				if (btnHelpMe.Visibility == Visibility.Collapsed)
				{
					grdMultipleChoice.Children.Clear();
				}

				btnHelpMe.Visibility = Visibility.Collapsed;
				guessedCorrectly = true;
				GenerateTheNextImage();
			};
		}
		/**
		* guess could either be a radio button selection or simply input
		* text
		**/
		private void ValidateGuess(object sender, RoutedEventArgs e)
		{

			string guess = string.Empty;

			if (sender is RadioButton)
				guess = (sender as RadioButton).Content.ToString();
			else
				guess = txtUserGuess.Text;

			HandleGuess(guess);
		}

		private void Multiple_Choice(object sender, RoutedEventArgs e)
		{
	       //get random profiles for  multiple choice 
			Profile[] randomProfiles = ViewModel.Get5RandomeProfiles(CurrentProfile);

			var row = 0;
			var col = 0;

			var random = new Random(Environment.TickCount);
			//shuffle random profiles 
			var profileOptions =
				randomProfiles
					.Select(profile => profile.FirstName + " " + profile.LastName)
					.Concat(new[] { CurrentProfile.FirstName + " " + CurrentProfile.LastName })
					.OrderBy(_ => random.Next(100))
					.ToArray();
			// loop through 4 random profiles
			foreach (var p in profileOptions)
			{
				RadioButton rb = new RadioButton()
				{
					Margin = new Thickness(10)
				};
				// when a radio button is select by the user, validate the guess
				
				rb.Click += new RoutedEventHandler(ValidateGuess);
				rb.Content = p;
				// add radio button
				grdMultipleChoice.Children.Add(rb);

				rb.SetValue(Grid.RowProperty, row);
				rb.SetValue(Grid.ColumnProperty, col);

				col++;
				// set up the radio buttons positions
				if (col >= grdMultipleChoice.ColumnDefinitions.Count)
				{
					col = 0;
					row++;
				}
			}

			btnHelpMe.Visibility = Visibility.Collapsed;
		}

		private void ValidateGuessRadioButton(object sender, RoutedEventArgs e)
		{
			var guessedName = (sender as RadioButton).Content.ToString();
			if (guessedName.ToLower() != FeaturedUser.ToLower())
			{

				txtWhoAmI.Text = "Oh No, Try again!";
				return;
			}
			txtWhoAmI.Text = String.Format($"Correct! Hi, my Name is : { guessedName}!");

			txtUserGuess.Text = "";
			grdMultipleChoice.Children.Clear();
			btnHelpMe.Visibility = Visibility.Collapsed;
		}
	}
}
