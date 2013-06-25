using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using xZAPP.Core;

namespace xZAPP.iOS
{
    public partial class ClientDetailViewController : UIViewController
    {
        UIPopoverController masterPopoverController;
        Client client;

        public ClientDetailViewController(IntPtr handle) : base (handle)
        {
        }

        public void SetClient(object cl)
        {
            if (client != cl)
            {
                client = cl;
				
                // Update the view
                ConfigureView();
            }
			
            if (masterPopoverController != null)
                masterPopoverController.Dismiss(true);
        }

        void ConfigureView()
        {

            // Update the user interface for the detail item
            if (IsViewLoaded && client != null)
            {
                this.Title = client.clientNameFormal;
                detailDescriptionLabel.Text = client.clientNameInformal;

                // TODO Create a UITableView with the dagrapportage for selected client
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
            // Perform any additional setup after loading the view, typically from a nib.
            ConfigureView();
        }

        [Export ("splitViewController:willHideViewController:withBarButtonItem:forPopoverController:")]
        public void WillHideViewController(UISplitViewController splitController, UIViewController viewController, UIBarButtonItem barButtonItem, UIPopoverController popoverController)
        {
            barButtonItem.Title = NSBundle.MainBundle.LocalizedString("Master", "Master");
            NavigationItem.SetLeftBarButtonItem(barButtonItem, true);
            masterPopoverController = popoverController;
        }

        [Export ("splitViewController:willShowViewController:invalidatingBarButtonItem:")]
        public void WillShowViewController(UISplitViewController svc, UIViewController vc, UIBarButtonItem button)
        {
            // Called when the view is shown again in the split view, invalidating the button and popover controller.
            NavigationItem.SetLeftBarButtonItem(null, true);
            masterPopoverController = null;
        }
    }
}

