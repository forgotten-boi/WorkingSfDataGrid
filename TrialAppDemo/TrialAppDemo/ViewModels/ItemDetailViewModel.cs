using System;

using TrialAppDemo.Models;

namespace TrialAppDemo.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }

    public class OrderDetailViewModel : BaseViewModel
    {
        public OrderInfo Item { get; set; }
        public OrderDetailViewModel(OrderInfo item = null)
        {
            Title = item?.CustomerName;
            Item = item;
        }
    }
}
