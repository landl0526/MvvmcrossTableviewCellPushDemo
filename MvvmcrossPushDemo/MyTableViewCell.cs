using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace MvvmcrossPushDemo
{
    public partial class MyTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("MyTableViewCell");
        public static readonly UINib Nib;

        static MyTableViewCell()
        {
            Nib = UINib.FromName("MyTableViewCell", NSBundle.MainBundle);
        }

        protected MyTableViewCell(IntPtr handle) : base(handle)
        {
            this.DelayBind(() =>
            {
                var bindings = this.CreateBindingSet<MyTableViewCell, Item>();

                bindings.Bind(MyBtn).For("Title").To(item => item.Name);
                bindings.Bind(MyBtn).To(item => item.ShowItemDetailsCommand).CommandParameter(DataContext);

                bindings.Apply();
            });
        }
    }
}
