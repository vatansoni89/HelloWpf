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

namespace HelloWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SelectedOperator selectedOperator;
        double lastNumber;
        public MainWindow()
        {
            InitializeComponent();
            resultLabel.Content = "0";
            btnMultNegative.Click += BtnMultNegative_Click;
            btnAC.Click += BtnAC_Click;
        }

        private void BtnAC_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
        }

        private void BtnMultNegative_Click(object sender, RoutedEventArgs e)
        {
            double.TryParse(resultLabel.Content.ToString(), out double num);
            resultLabel.Content = (num * (-1)).ToString();
        }

        private void BtnNumber_Click(object sender, RoutedEventArgs e)
        {
            var number = sender as Button;

            var num = number.Content.ToString();
            if (resultLabel.Content.ToString() != "0")
            {
                resultLabel.Content = $"{resultLabel.Content}{num}";
            }
            else
            {
                resultLabel.Content = num;
            }
        }

        private void OperationBtn_Click(object sender, RoutedEventArgs e)
        {
            double.TryParse(resultLabel.Content.ToString(), out lastNumber);

            resultLabel.Content = 0;

            if (sender == BtnAdd)
            {
                selectedOperator = SelectedOperator.Add;
            }
        }

        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out double newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Add:
                        resultLabel.Content = SimpleMath.Add(newNumber, lastNumber);
                        break;
                    case SelectedOperator.Subsn:
                        break;
                    case SelectedOperator.Mult:
                        break;
                    case SelectedOperator.Divide:
                        break;
                    default:
                        break;
                }

            }
        }
    }

    public class SimpleMath
    { 
        public static double Add(double n1, double n2)
        { 
            return n1+n2;
        }
    }

    public enum SelectedOperator
    {
        Add,
        Subsn,
        Mult,
        Divide
    }
}
