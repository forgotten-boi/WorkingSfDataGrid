using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TrialAppDemo.Models;
using TrialAppDemo.Services;
using TrialAppDemo.Views;
using Xamarin.Forms;

namespace TrialAppDemo.ViewModels
{
    public partial class OrderViewModel //: BaseViewModel
    {
        public IDataStore<OrderInfo> DataStore => DependencyService.Get<IDataStore<OrderInfo>>();
        public OrderViewModel()
        {
            this.OrdersInfo = new ObservableCollection<OrderInfo>();
            GenerateOrders();

            MessagingCenter.Subscribe<NewItemPage, OrderInfo>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as OrderInfo;
                OrdersInfo.Add(newItem);
                //await DataStore.AddItemAsync(newItem);
                
            });
        }
        public void GenerateOrders()
        {
            for(int j= 0; j< 10; j++)
            for (int i = j; i < 25; i++)
            {
                OrdersInfo.Add(new OrderInfo((j+1)*1000+ i, i +2379,
                                             String.Concat((char)(65+i),(char)(65 + i + 1),(char)(65 + i + 2),(char)(65 + i + 3)),
                                             String.Concat((char)(91 - i), (char)(91 - 3 + i), (char)(91 - 6 + i), (char)(91 - 9 + i )),
                                             i%2==0?"Nepal":"NetherLand"
                                             ));
            }
        
        }


   

        public ObservableCollection<OrderInfo> OrdersInfo{ get; set; }
    }
}
