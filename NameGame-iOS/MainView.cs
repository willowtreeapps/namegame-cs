
using System;
using System.Drawing;

using Foundation;
using UIKit;
using WillowTree.NameGame.Core.ViewModels;

namespace WillowTree.NameGame.iOS
{
    public partial class MainView : UIViewController
    {
        private MainViewModel ViewModel { get; set; }

        public MainView(IntPtr handle) : base(handle)
        {
            ViewModel = new MainViewModel();
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();


        }

        #region View lifecycle

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        #endregion
    }
}