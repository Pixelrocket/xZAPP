using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using xZAPP.Core;
using System.Threading.Tasks;

namespace xZAPP.iOS
{
    public partial class ClientListViewController : UITableViewController
    {
        DataSource dataSource;

        public ClientListViewController(IntPtr handle) : base (handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Cliënten", "Cliënten");			
            // Custom initialization
        }

        public ReportListViewController ViewController{ get; set;}
        public string Token{ get; set;}      

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
            //NavigationItem.LeftBarButtonItem = EditButtonItem;

            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Stop, Logout);
            NavigationItem.RightBarButtonItem = addButton;

            //this.NavigationController.NavigationBar.Items[0].Title = "Logout"; //new UIBarButtonItem("Logout", UIBarButtonItemStyle.Plain, null);
            this.NavigationItem.HidesBackButton = true;

            Client cl = new Client();

            // Call GetClientsAsync and set result as datasource, TaskScheduler must be used to update UI
            cl.GetClientsAsync(Token).ContinueWith(t => {
                TableView.Source = dataSource = new DataSource(t.Result);
                TableView.ReloadData();
            },TaskScheduler.FromCurrentSynchronizationContext ());
        }

        public void SetToken(string token)
        {
            Token = token;
        }   

        class DataSource : UITableViewSource
        {
            static readonly NSString CellIdentifier = new NSString("DataSourceCell");
            List<Client> clients = new List<Client>();

            public DataSource(List<Client> cls)
            {
                clients = cls;
            }

            public IList<Client> Clients
            {
                get 
                {
                    return clients;
                }
            }
            // Customize the number of sections in the table view.
            public override int NumberOfSections(UITableView tableView)
            {
                return 1;
            }

            public override int RowsInSection(UITableView tableview, int section)
            {               
                return clients == null ? 0 : clients.Count;
            }

            // Customize the appearance of table view cells.
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = (UITableViewCell)tableView.DequeueReusableCell(CellIdentifier, indexPath);
                cell.TextLabel.Text = clients[indexPath.Row].clientNameFormal;
                return cell;
            }

            public override bool CanEditRow(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
            {
                // Return false if you do not want the specified item to be editable.
                return false;
            }
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "showReports")
            {
                var indexPath = TableView.IndexPathForSelectedRow;
                var item = dataSource.Clients[indexPath.Row];

                ((ReportListViewController)segue.DestinationViewController).SetClient(item);
            }
        }

        private void Logout(object sender, EventArgs args)
        {       
            this.PerformSegue("logoutFromClients", this);
        }
    }
}

