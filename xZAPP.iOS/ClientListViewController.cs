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
			
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                ContentSizeForViewInPopover = new SizeF(320f, 600f);
                ClearsSelectionOnViewWillAppear = false;
            }
			
            // Custom initialization
        }

        public ClientDetailViewController DetailViewController
        {
            get;
            set;
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
            //NavigationItem.LeftBarButtonItem = EditButtonItem;

            //var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, AddNewItem);
            //NavigationItem.RightBarButtonItem = addButton;

            Client cl = new Client();

            // Call GetClientsAsync and set result as datasource, TaskScheduler must be used to update UI
            cl.GetClientsAsync().ContinueWith(t => {
                TableView.Source = dataSource = new DataSource(t.Result);
                TableView.ReloadData();
            },TaskScheduler.FromCurrentSynchronizationContext ());


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
                return clients.Count;
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
            if (segue.Identifier == "showDetail")
            {
                var indexPath = TableView.IndexPathForSelectedRow;
                var item = dataSource.Clients[indexPath.Row];

                ((ClientDetailViewController)segue.DestinationViewController).SetClient(item);
            }
        }
    }
}

