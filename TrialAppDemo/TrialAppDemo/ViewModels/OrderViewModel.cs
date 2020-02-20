using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TrialAppDemo.Models;

namespace TrialAppDemo.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            this.OrdersInfo = new ObservableCollection<OrderInfo>();
            GenerateOrders();
        }
        public void GenerateOrders()
        {
            for (int i = 0; i < 25; i++)
            {
                OrdersInfo.Add(new OrderInfo(i+1000, i+2379,
                                             String.Concat((char)(65+i),(char)(65 + i + 1),(char)(65 + i + 2),(char)(65 + i + 3)),
                                             String.Concat((char)(91 - i), (char)(65 - 3 + i), (char)(65 - 6 + i), (char)(65 - 9 + i )),
                                             i%2==0?"Nepal":"NetherLand"
                                             ));
            }
            var abc = (char)(67 + 12);
        }
        public ObservableCollection<OrderInfo> OrdersInfo{ get; set; }
    }
}
