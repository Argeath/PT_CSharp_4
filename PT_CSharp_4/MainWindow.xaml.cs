using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PT_CSharp_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CarsContext ctx;

        private string searchText;
        private string searchColumn;
        public MainWindow()
        {
            InitializeComponent();

            ctx = new CarsContext();
            RefreshData();

            Style rowStyle = new Style(typeof(DataGridRow));
            rowStyle.Setters.Add(new EventSetter(DataGridRow.MouseDoubleClickEvent,
                                     new MouseButtonEventHandler(Row_DoubleClick)));
            dataGrid.RowStyle = rowStyle;
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            EditWindow edit = new EditWindow(ctx, (Car)(row.Item));
            edit.Closing += (o, args) => RefreshData();
            edit.Show();
        }


        public void RefreshData()
        {
            List<Car> carsList = ctx.Cars.Include("Engine").ToList();
            IEnumerable<Car> search = carsList;
            if (searchColumn == "Model")
            {
                search = from car in carsList
                    where car.Model.Contains(searchText)
                    select car;
            } else if (searchColumn == "Year")
            {
                search = from car in carsList
                    where car.Year.Equals(int.Parse(searchText))
                    select car;
            }

            dataGrid.ItemsSource = search;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            searchText = searchInput.Text;
            searchColumn = columnSelect.Text;
            RefreshData();
        }

        private void AddNew_Click(object sender, RoutedEventArgs e)
        {
            EditWindow edit = new EditWindow(ctx);
            edit.Closing += (o, args) => RefreshData();
            edit.Show();
        }
    }
}
