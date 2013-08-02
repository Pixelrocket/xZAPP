// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Net;

namespace xZAPP.iOS
{
	public partial class ReactionViewController : UIViewController
	{
		public ReactionViewController (IntPtr handle) : base (handle)
		{
		}


        private void SendReaction()
        {
            bool valid = true;
            try
            {
                if (this.ReactionTitle.Text.Length <= 0)
                {
                    valid = false;
                    InvokeOnMainThread(() => 
                                       {
                        this.ReactionTitle.Layer.BorderColor = UIColor.Red.CGColor;
                        this.ReactionTitle.Layer.BorderWidth = 3;
                        this.ReactionTitle.Layer.CornerRadius = 5;
                    });
                };

                if (this.ReactionEmailAddress.Text.Length <= 0)
                {
                    valid = false;
                    InvokeOnMainThread(() => 
                                       {
                        this.ReactionEmailAddress.Layer.BorderColor = UIColor.Red.CGColor;
                        this.ReactionEmailAddress.Layer.BorderWidth = 3;
                        this.ReactionEmailAddress.Layer.CornerRadius = 5;
                    });
                };

                if (this.ReactionContent.Text.Length <= 0)
                {
                    valid = false;
                    InvokeOnMainThread(() => 
                                       {
                        this.ReactionContent.Layer.BorderColor = UIColor.Red.CGColor;
                        this.ReactionContent.Layer.BorderWidth = 3;
                        this.ReactionContent.Layer.CornerRadius = 5;
                    });
                };

                if (valid)
                {

                   
                  
                }
            }
            catch (WebException webEx)
            {
                switch (((HttpWebResponse)webEx.Response).StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        //this.lblMessage.Text = "Login gegevens onjuist of niet gevonden!";
                        break;
                    default:
                        break;
                }
            }
            catch
            {
            }
        }

	}
}
