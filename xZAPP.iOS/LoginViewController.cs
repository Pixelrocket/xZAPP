// This file has been autogenerated from a class added in the UI designer.

using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using xZAPP.Core;
using System.Net;

namespace xZAPP.iOS
{
	public partial class LoginViewController : UIViewController
	{
		public LoginViewController (IntPtr handle) : base (handle)
		{
            Title = NSBundle.MainBundle.LocalizedString("Login", "Login");
		}

        //public ClientListViewController TableViewController
        //{
        //    get;
        //    set;
       // }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.NavigationItem.HidesBackButton = true;
        }   

        public override void ViewWillAppear(bool animated)
        {
            Title = NSBundle.MainBundle.LocalizedString("Login", "Login");
        }

        partial void btnInloggen_TouchUpInside(UIButton sender)
        {
            bool valid = true;
            this.lblMessage.Text = "";

            try {

                if ( this.txtUsername.Text.Length <= 0 ) {
                    valid = false;
                    InvokeOnMainThread ( () => {
                        this.txtUsername.Layer.BorderColor = UIColor.Red.CGColor;
                        this.txtUsername.Layer.BorderWidth = 3;
                        this.txtUsername.Layer.CornerRadius = 5;
                    });
                };
                if ( this.txtPassword.Text.Length <= 0 ) {
                    valid = false;
                    InvokeOnMainThread ( () => {
                        this.txtPassword.Layer.BorderColor = UIColor.Red.CGColor;
                        this.txtPassword.Layer.BorderWidth = 3;
                        this.txtPassword.Layer.CornerRadius = 5;
                    });
                };

                if(valid)
                {
                    // CheckCredentials and show error or list with clients for logged in user.
                    Credentials cr = new Credentials();
                    Credentials myCreds = cr.CheckCredentials(this.txtUsername.Text, this.txtPassword.Text);

                    if(myCreds != null)
                    {
                        ApplicationData.GetInstance.Token = myCreds.token;
                        this.PerformSegue("showClients", this);
                    }
                }
               
            } catch (WebException webEx) {
                switch (((HttpWebResponse)webEx.Response).StatusCode) {
                    case HttpStatusCode.Unauthorized:
                        this.lblMessage.Text = "Login gegevens onjuist of niet gevonden!";
                        break;
                    default:
                    break;
                }
            }
            catch {
            }
           
        }


        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "showClients")
            {              
                ((ClientListViewController)segue.DestinationViewController).SetToken(ApplicationData.GetInstance.Token);
            }
        }


         
	}
}
