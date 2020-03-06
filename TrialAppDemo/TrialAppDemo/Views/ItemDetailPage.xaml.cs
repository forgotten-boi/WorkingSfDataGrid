using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TrialAppDemo.Models;
using TrialAppDemo.ViewModels;

namespace TrialAppDemo.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        OrderDetailViewModel viewModel;

        public ItemDetailPage(OrderDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage(OrderInfo orderInfo)
        {
            InitializeComponent();
            var random = new Random();
            var item = new OrderInfo()
            {
                CustomerName = orderInfo.CustomerName,
                ShipCountry = $"Order: {orderInfo.OrderId}, Locations:{orderInfo.ShipCountry}, {orderInfo.ShipCity}",
                OrderId = orderInfo.OrderId// orderInfo.ShipCountry,
            };

            viewModel = new OrderDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}