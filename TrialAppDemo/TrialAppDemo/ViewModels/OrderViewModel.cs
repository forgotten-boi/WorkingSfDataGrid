using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TrialAppDemo.Models;

namespace TrialAppDemo.ViewModels
{
    public partial class OrderViewModel //: BaseViewModel
    {

        public OrderViewModel()
        {
            this.OrdersInfo = new ObservableCollection<OrderInfo>();
            GenerateOrders();
        }
        public void GenerateOrders()
        {
            for(int j= 0; j< 10; j++)
            for (int i = j; i < 25; i++)
            {
                OrdersInfo.Add(new OrderInfo(i+1000, i+2379,
                                             String.Concat((char)(65+i),(char)(65 + i + 1),(char)(65 + i + 2),(char)(65 + i + 3)),
                                             String.Concat((char)(91 - i), (char)(91 - 3 + i), (char)(91 - 6 + i), (char)(91 - 9 + i )),
                                             i%2==0?"Nepal":"NetherLand"
                                             ));
            }
        
        }


   

        public ObservableCollection<OrderInfo> OrdersInfo{ get; set; }
    }
}
