using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Foundation;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using UIKit;

namespace MvvmcrossPushDemo
{
    public class FirstViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public FirstViewModel(IMvxNavigationService navigationService)
        {

            _navigationService = navigationService;

            ItemList = new List<Item>();
            for (int i = 0; i < 10; i++)
            {
                ItemList.Add(new Item { Name = "Item" + i });
            }
        }

        private List<Item> itemList;
        public List<Item> ItemList
        {
            set
            {
                itemList = value;
                RaisePropertyChanged(() => ItemList);
            }
            get
            {
                return itemList;
            }
        }
    }

    public class Item
    {
        private readonly Lazy<IMvxNavigationService> _navigationService = new Lazy<IMvxNavigationService>(Mvx.Resolve<IMvxNavigationService>);

        public string Name { set; get; }

        private ICommand showItemDetailsCommand;
        public ICommand ShowItemDetailsCommand
        {
            get
            {
                return showItemDetailsCommand ?? (showItemDetailsCommand = new MvxCommand<Item>(ShowItemDetails));
            }
        }
        async void ShowItemDetails(Item item)
        {
            await _navigationService.Value.Navigate<SecondViewModel, Item>(item);
        }
    }
}