using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TrialAppDemo.Models;

namespace TrialAppDemo.ViewModels
{
    public partial class OrderViewModel : BaseViewModel // : INotifyPropertyChanged
    {
        // ViewModel.cs

        #region Filtering

        #region Fields

        private string filterText = "";
        private string selectedColumn = "All Columns";
        private string selectedCondition = "Equals";
        internal delegate void FilterChanged();
        internal FilterChanged filterTextChanged;



        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string name) 
            =>this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        
        #region Property

        public string FilterText
        {
            get { return filterText; }
            set
            {
                filterText = value;
                OnFilterTextChanged();
                RaisePropertyChanged("FilterText");
            }
        }

        public string SelectedCondition
        {
            get { return selectedCondition; }
            set { selectedCondition = value; }
        }

        public string SelectedColumn
        {
            get { return selectedColumn; }
            set { selectedColumn = value; }
        }

        #endregion

        #region Private Methods

        private void OnFilterTextChanged()
        {
            filterTextChanged();
        }

        private bool MakeStringFilter(OrderInfo o, string option, string condition)
        {
            var value = o.GetType().GetProperty(option);
            var exactValue = value.GetValue(o, null);
            exactValue = exactValue.ToString().ToLower();
            string text = FilterText.ToLower();
            var methods = typeof(string).GetMethods();
            if (methods.Count() != 0)
            {
                if (condition == "Contains")
                {
                    var methodInfo = methods.FirstOrDefault(method => method.Name == condition);
                    bool result1 = (bool)methodInfo.Invoke(exactValue, new object[] { text });
                    return result1;
                }
                else if (exactValue.ToString() == text.ToString())
                {
                    bool result1 = String.Equals(exactValue.ToString(), text.ToString());
                    if (condition == "Equals")
                        return result1;
                    else if (condition == "NotEquals")
                        return false;
                }
                else if (condition == "NotEquals")
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        private bool MakeNumericFilter(OrderInfo o, string option, string condition)
        {
            var value = o.GetType().GetProperty(option);
            var exactValue = value.GetValue(o, null);
            double res;
            bool checkNumeric = double.TryParse(exactValue.ToString(), out res);
            if (checkNumeric)
            {
                switch (condition)
                {
                    case "Equals":
                        try
                        {
                            if (exactValue.ToString() == FilterText)
                            {
                                if (Convert.ToDouble(exactValue) == (Convert.ToDouble(FilterText)))
                                    return true;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case "NotEquals":
                        try
                        {
                            if (Convert.ToDouble(FilterText) != Convert.ToDouble(exactValue))
                                return true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            return true;
                        }
                        break;
                }
            }
            return false;
        }

        #endregion

        #region Public Methods

        public bool FilerRecords(object o)
        {
            double res;
            bool checkNumeric = double.TryParse(FilterText, out res);
            var item = o as OrderInfo;
            if (item != null && FilterText.Equals(""))
            {
                return true;
            }
            else
            {
                if (item != null)
                {
                    if (checkNumeric && !SelectedColumn.Equals("All Columns"))
                    {
                        bool result = MakeNumericFilter(item, SelectedColumn, SelectedCondition);
                        return result;
                    }
                    else if (SelectedColumn.Equals("All Columns"))
                    {
                        //if (item.CustomerID.ToString().ToLower().Contains(FilterText.ToLower()) ||
                        //    item.ShipCountry.ToLower().Contains(FilterText.ToLower()) ||
                        //    item.ShipCity.ToString().ToLower().Contains(FilterText.ToLower()) ||
                        //    item.CustomerName.ToString().ToLower().Contains(FilterText.ToLower()) ||
                        //    item.OrderId.ToString().ToLower().Contains(FilterText.ToLower()))
                        if(item.CustomerName.ToString().ToLower().Contains(FilterText.ToLower()))
                            return true;
                        return false;
                    }
                    else
                    {
                        bool result = MakeStringFilter(item, SelectedColumn, SelectedCondition);
                        return result;
                    }
                }
            }
            return false;
        }

        #endregion

        #endregion
    }
}
