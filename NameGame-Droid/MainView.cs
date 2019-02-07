using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WillowTree.NameGame.Core.ViewModels;

namespace WillowTree.NameGame.Droid
{
    [Activity(Label = "NameGame", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainView : Activity
    {
        private MainViewModel ViewModel { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewModel = new MainViewModel();

            SetContentView(Resource.Layout.view_main);
        }
    }
}