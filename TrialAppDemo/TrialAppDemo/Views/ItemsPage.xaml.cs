using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TrialAppDemo.Models;
using TrialAppDemo.Views;
using TrialAppDemo.ViewModels;
using Syncfusion.SfDataGrid.XForms;
using System.Diagnostics;

namespace TrialAppDemo.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        OrderViewModel viewModel;
        StackLayout contextMenu;
        Button sortButton;
        Button clearSortButton;
        private bool isContextMenuDisplayed = false;
        private string currentColumnName;

        public ItemsPage()
        {
            InitializeComponent();

            CreateContextMenu();
       
            dataGrid.GridTapped += DataGrid_GridTapped;
            dataGrid.AllowSorting = true;
            BindingContext = viewModel = new OrderViewModel();
            viewModel.filterTextChanged = OnFilterChanged;

        }

        public void CreateContextMenu()
        {
            contextMenu = new StackLayout();

            sortButton = new Button();
            sortButton.Text = "Sort";
            sortButton.BackgroundColor = Color.Black;
            sortButton.TextColor = Color.White;
            sortButton.Clicked += SortButton_Clicked;

            clearSortButton = new Button();
            clearSortButton.Text = "Clear sort";
            clearSortButton.BackgroundColor = Color.Black;
            clearSortButton.TextColor = Color.White;
            clearSortButton.Clicked += ClearSortButton_Clicked;

            // A custom view hosting two buttons are now created
            contextMenu.Children.Add(sortButton);
            contextMenu.Children.Add(clearSortButton);
        }

        private void DataGrid_GridTapped(object sender, GridTappedEventsArgs e)
        {
            // Hides the context menu when SfDataGrid is tapped anywhere outside the context menu view
            relativeLayout.Children.Remove(contextMenu);
            isContextMenuDisplayed = false;
        }

        // Removes the sorting applied to the SfDataGrid
        private void ClearSortButton_Clicked(object sender, EventArgs e)
        {
            relativeLayout.Children.Remove(contextMenu);
            isContextMenuDisplayed = false;
            dataGrid.SortColumnDescriptions.Clear();
        }

        // Sorts the SfDataGrid data based on the column selected in the context menu
        private void SortButton_Clicked(object sender, EventArgs e)
        {
            relativeLayout.Children.Remove(contextMenu);
            isContextMenuDisplayed = false;
            dataGrid.SortColumnDescriptions.Clear();
            dataGrid.SortColumnDescriptions.Add(new SortColumnDescription()
            {
                ColumnName = currentColumnName
            });
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            dataGrid.SelectionMode = Syncfusion.SfDataGrid.XForms.SelectionMode.Single;
            dataGrid.EditTapAction = TapAction.OnTap;
            //if (viewModel.Items.Count == 0)
            //    viewModel.LoadItemsCommand.Execute(null);
        }

        private void dataGrid_GridLongPressed(object sender, Syncfusion.SfDataGrid.XForms.GridLongPressedEventArgs e)
        {
            if (!isContextMenuDisplayed)
            {
                currentColumnName = dataGrid.Columns[e.RowColumnIndex.ColumnIndex].MappingName;
                var point = dataGrid.RowColumnIndexToPoint(e.RowColumnIndex);
                // Display the ContextMenu when the SfDataGrid is long pressed
                relativeLayout.Children.Add(contextMenu, Constraint.Constant(point.X), Constraint.Constant(point.Y));
                isContextMenuDisplayed = true;
            }
            else
            {
                // Hides the context menu when SfDataGrid is long pressed when the context menu is already visible in screen
                relativeLayout.Children.Remove(contextMenu);
                isContextMenuDisplayed = false;
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            EnableEdit();
            DisplayAlert("Edit Enabled", "Edit Enabled", "Ok");
        }

       

        public void EnableEdit()
        {
           
            dataGrid.AllowEditing = true;
            dataGrid.NavigationMode = NavigationMode.Cell;
          
            this.dataGrid.EditorSelectionBehavior = EditorSelectionBehavior.MoveLast;
        }

        public void DisableEdit()
        {

            dataGrid.AllowDraggingRow = true;
            dataGrid.AllowEditing = false;
            dataGrid.NavigationMode = NavigationMode.Row;
            dataGrid.EditTapAction = TapAction.OnTap;
            this.dataGrid.EditorSelectionBehavior = EditorSelectionBehavior.MoveLast;
        }

        #region Filtering
        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null)
                viewModel.FilterText = "";
            else
                viewModel.FilterText = e.NewTextValue;
         
        }

        private void OnFilterChanged()
        {
            if (dataGrid.View != null)
            {
                this.dataGrid.View.Filter = viewModel.FilerRecords;
                this.dataGrid.View.RefreshFilter();
            }
        }

        #endregion

        private void dataGrid_CurrentCellEndEdit(object sender, GridCurrentCellEndEditEventArgs e)
        {
            var obj = sender as Syncfusion.SfDataGrid.XForms.SfDataGrid;
            var row = Grid.GetRow(this.dataGrid.Children[this.dataGrid.SelectedIndex]);
            var column = Grid.GetColumn(this.dataGrid.Children[this.dataGrid.SelectedIndex]);

            edittedIndex = this.dataGrid.SelectedIndex;

            var parseOrderInfo = (OrderInfo)dataGrid.SelectedItem;
            viewModel.OrdersInfo.Where(p => p.OrderId == parseOrderInfo.OrderId).FirstOrDefault().IsClosed = true;

            IsEditted = true;
            //obj.BackgroundColor = Color.Red;
            Debug.WriteLine("End Edit Called");
            //if (await DisplayAlert("Edit Enabled", "Data Updated, Do you like to edit anotherOne", "Yes", "No"))
               
        }

        private void dataGrid_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            //e.Style.BackgroundColor = Color.Black;
            //e.Style.ForegroundColor = Color.White;
            //e.Handled = true;
        }
        public bool IsEditted = false;
        public int edittedIndex { get; set; }
        private void dataGrid_QueryRowStyle(object sender, QueryRowStyleEventArgs e)
        {
         
            var grid = sender as SfDataGrid;
            //if ((e.RowData == grid.SelectedItem || e.RowIndex == grid.SelectedIndex) && IsEditted)
            //{
            //if (e.RowIndex == edittedIndex || viewModel.OrdersInfo[e.RowIndex].IsClosed)
            var parseOrderInfo = (OrderInfo)e.RowData;
            if(parseOrderInfo != null && parseOrderInfo.IsClosed)
            {
                e.Style.BackgroundColor = Color.DarkRed; 
                e.Style.ForegroundColor = Color.DarkSeaGreen; 
                //viewModel.OrdersInfo[e.RowIndex].IsClosed = true; 
                IsEditted = false;
                DisableEdit();
            }
            e.Handled = true;
        }



        private void ThemeChange_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine(this.dataGrid.GridStyle.GetType());
            var defaultTheme = new Syncfusion.SfDataGrid.XForms.DefaultStyle();

            if (this.dataGrid.GridStyle.GetType() == typeof(Syncfusion.SfDataGrid.XForms.DefaultStyle))
            {
                this.dataGrid.GridStyle = new Dark();
                this.Theme.Text = "Dark";
            }
            else
            {
                this.dataGrid.GridStyle = new Syncfusion.SfDataGrid.XForms.DefaultStyle();
                this.Theme.Text = "Light";
            }
        }
    }

    [Serializable]
    public class MyException : Exception
    {
        public MyException()
        {

        }
        public MyException(string message) : base(message)
        {
            Debug.WriteLine(message);

        }
        public MyException(string message, Exception inner) : base(message, inner)
        {
            Debug.WriteLine(message);
        }
        protected MyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
            Debug.WriteLine(info);
        }
    }
}