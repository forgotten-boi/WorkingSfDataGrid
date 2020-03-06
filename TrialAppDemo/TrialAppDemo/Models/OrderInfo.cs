using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TrialAppDemo.Models
{
    public class OrderInfo : INotifyPropertyChanged, IEditableObject
    {


        private int orderId;
        private int customerId;
        private string customerName;
        private string shipCity;
        private string shipCountry;
        public int OrderId
        {
            get { return orderId; }
            set
            {
                orderId = value;
                RaisePropertyChanged("OrderId");
            }
        }
        public int CustomerID
        {
            get { return customerId; }
            set
            {
                customerId = value;
                RaisePropertyChanged("CustomerID");
            }
        }

        public string CustomerName
        {
            get { return customerName; }
            set
            {
                customerName = value;
                RaisePropertyChanged("CustomerName");
            }
        }

        public string ShipCity
        {
            get { return shipCity; }
            set
            {
                shipCity = value;
                RaisePropertyChanged("ShipCity");
            }
        }


        public string ShipCountry
        {
            get { return shipCountry; }
            set
            {
                shipCountry = value;
                RaisePropertyChanged("ShipCountry");
            }
        }

        public void RaisePropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        private bool _isClosed;
        public bool IsClosed
        {
            get { return _isClosed; }
            set
            {
                this._isClosed = value;
                RaisePropertyChanged("IsClosed");
            }
        }

        protected Dictionary<string, object> BackUp()
        {
            var dictionary = new Dictionary<string, object>();
            var itemProperties = this.GetType().GetTypeInfo().DeclaredProperties;
            foreach (var pDescriptor in itemProperties)
            {
                if (pDescriptor.CanWrite)
                    dictionary.Add(pDescriptor.Name, pDescriptor.GetValue(this));
            }
            return dictionary;
        }


        private Dictionary<string, object> storedValues;
        public void BeginEdit()
        {
            this.storedValues = this.BackUp();
        }

        public void CancelEdit()
        {
            if (this.storedValues == null)
                return;

            foreach (var item in this.storedValues)
            {
                var itemProperties = this.GetType().GetTypeInfo().DeclaredProperties;
                var pDesc = itemProperties.FirstOrDefault(p => p.Name == item.Key);
                if (pDesc != null)
                    pDesc.SetValue(this, item.Value);
            }
        }

        public void EndEdit()
        {
            if (this.storedValues != null)
            {
                this.storedValues.Clear();
                this.storedValues = null;
            }
        
        }
        public OrderInfo()
        {

        }
        public OrderInfo(int orderId, int customerId, string customerName, string shipCity, string shipCountry)
        {
            OrderId = orderId;
            CustomerID = customerId;
            CustomerName = customerName;
            ShipCity = shipCity;
            ShipCountry = shipCountry;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
