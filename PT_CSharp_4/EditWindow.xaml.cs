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
using System.Windows.Shapes;

namespace PT_CSharp_4
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {


        private bool isEditing = false;
        private Car car;
        private CarsContext ctx;
        public EditWindow(CarsContext ctx)
        {
            InitializeComponent();

            this.ctx = ctx;
            car = new Car {Engine = new Engine()};
        }

        public EditWindow(CarsContext ctx, Car car)
        {
            InitializeComponent();

            this.ctx = ctx;
            this.car = car;
            isEditing = true;

            modelInput.Text = car.Model;
            yearInput.Text = car.Year+"";
            engineModelInput.Text = car.Engine.Model;
            engineDisplacementInput.Text = car.Engine.Displacement + "";
            engineHorsePowerInput.Text = car.Engine.HorsePower + "";
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                car.Model = modelInput.Text;
                car.Year = int.Parse(yearInput.Text);

                car.Engine.Model = engineModelInput.Text;
                car.Engine.Displacement = double.Parse(engineDisplacementInput.Text);
                car.Engine.HorsePower = double.Parse(engineHorsePowerInput.Text);

                if (!isEditing)
                {
                    ctx.Cars.Add(car);
                }
                ctx.SaveChanges();

                Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Nieprawidlowy format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
