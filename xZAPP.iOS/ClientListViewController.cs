using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using xZAPP.Core;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;

namespace xZAPP.iOS
{
    public partial class ClientListViewController : UITableViewController
    {
        DataSource dataSource;
        Client latestClient;
             

        public ClientListViewController(IntPtr handle) : base (handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Cliënten", "Cliënten");			
            // Custom initialization
        }

        public ReportListViewController ViewController{ get; set;}


        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {      
            base.ViewDidLoad();

            var logoutButton = new UIBarButtonItem(UIBarButtonSystemItem.Stop, Logout);
            NavigationItem.RightBarButtonItem = logoutButton;

            //this.NavigationController.NavigationBar.Items[0].Title = "Logout"; //new UIBarButtonItem("Logout", UIBarButtonItemStyle.Plain, null);
            this.NavigationItem.HidesBackButton = true;


            var cl = new Client();
            latestClient = cl.ReadLatestClient();
            GetClients();
        }    


        class DataSource : UITableViewSource
        {
            static readonly NSString CellIdentifier = new NSString("DataSourceCell");
            List<Client> clients = new List<Client>();
            ClientListViewController clientListView;

            public DataSource(List<Client> cls, ClientListViewController parent)
            {
                clients = cls;
                this.clientListView = parent;
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

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {                
                clientListView.PerformSegue("showReports", this);
                tableView.DeselectRow (indexPath, true); 
            }

            // Customize the appearance of table view cells.
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = (UITableViewCell)tableView.DequeueReusableCell(CellIdentifier, indexPath);

                cell.TextLabel.Text = clients[indexPath.Row].FormalName;
                cell.DetailTextLabel.Text = "[" + clients[indexPath.Row].NumberOfDailyReports.ToString() + "]";

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
                if (indexPath != null)
                {
                    latestClient = dataSource.Clients[indexPath.Row];
                }

                //Write clientid to file ...
                latestClient.WriteLatestClient(latestClient);
                
                ((ReportListViewController)segue.DestinationViewController).SetClient(latestClient);
            }
        }



        private void Logout(object sender, EventArgs args)
        {       
            //this.PerformSegue("logoutFromClients", this);
            this.NavigationController.PopToRootViewController(true);
        }

        private void GetClients()
        {           
            try
            {
                Client cl = new Client();   

                cl.GetClientsAsync(ApplicationData.GetInstance.Token).ContinueWith(t => {
                       
                        List<Client> clients = t.Result;
                        ClientList.Source = dataSource = new DataSource(clients, this);
                        ClientList.ReloadData();

                        if(ClientList.NumberOfRowsInSection(0) == 0)
                        {
                            UIAlertView alert = new UIAlertView("zAPP Melding", "Er zijn voor u geen cliënten beschikbaar.", null, "Ok", null);
                            alert.Show();
                        }

                        // select latest client
                        Client lcl = cl.ReadLatestClient();
                        if(lcl != null)
                        {
                            var index = clients.FindIndex(c => c.ClientId == lcl.ClientId);
                            NSIndexPath path = NSIndexPath.FromRowSection(index, 0);
                            ClientList.SelectRow(path, false, UITableViewScrollPosition.None);
           
                            //dataSource.RowSelected(ClientList, path);
                            //this.PerformSegue("showReports", this);    

                        }

                },TaskScheduler.FromCurrentSynchronizationContext());
                     
            }

            catch (WebException webEx)
            {
                switch (((HttpWebResponse)webEx.Response).StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        this.PerformSegue("logoutFromClients", this);
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

