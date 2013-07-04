// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using xZAPP.Core;

namespace xZAPP.iOS
{
	public partial class ReportDetailViewController : UIViewController
	{
        DailyReport report;
        public string Token{ get; set;}   


		public ReportDetailViewController (IntPtr handle) : base (handle)
		{
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
            var logoutButton = new UIBarButtonItem(UIBarButtonSystemItem.Stop, Logout);
            NavigationItem.RightBarButtonItem = logoutButton;

            // Add a button for response form ...

            // Fill data in view
            this.ReportSubject.Text = report.Subject;
            this.ReportBy.Text = (string.IsNullOrEmpty(report.EmployeeFirstname) ? "" : report.EmployeeFirstname) + 
                (string.IsNullOrEmpty(report.EmployeeInfix) ? " " : " " +report.EmployeeInfix + " ") + 
                (string.IsNullOrEmpty(report.EmployeeLastname) ? "" : report.EmployeeLastname);
            this.ReportDate.Text = report.Date + " - " + report.Time;
            this.ReportContent.Text = report.Content;
        }

        public void SetDailyReport(DailyReport rep)
        {
            if (report != rep)
            {
                report = rep;

                // Update the view

            }
        }           




        private void Logout(object sender, EventArgs args)
        {       
            this.PerformSegue("logoutFromReportDetail", this);
        }
	}
}
