using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Binding.BindingContext;

namespace MvvmcrossPushDemo
{

    [Register("FirstView")]
    public class FirstView : MvxViewController
    {
        UITableView TableView = new UITableView();

        public FirstView()
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
            View.BackgroundColor = UIColor.White;

            base.ViewDidLoad();
            // Perform any additional setup after loading the view

            View.AddSubview(TableView);
            TableView.Frame = View.Bounds;
            var source = new TableSource(TableView);

            TableView.Source = source;
            TableView.RowHeight = 120f;
            TableView.ReloadData();

            var set = this.CreateBindingSet<FirstView, FirstViewModel>();

            set.Bind(source).For(s => s.ItemsSource).To(vm => vm.ItemList);

            set.Apply();
        }
    }

    public class TableSource : MvxTableViewSource
    {
        private static readonly NSString CellIdentifier = new NSString("MyTableViewCell");

        public TableSource(UITableView tableView)
                : base(tableView)
        {
            tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            tableView.RegisterNibForCellReuse(UINib.FromName("MyTableViewCell", NSBundle.MainBundle),
                                              CellIdentifier);
        }


        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            return TableView.DequeueReusableCell(CellIdentifier, indexPath);
        }
    }
}