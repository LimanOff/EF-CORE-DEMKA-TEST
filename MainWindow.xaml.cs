using EFCore.Model.DatabaseModel.Entities;
using EFCore.Model.Helpers;
using EFCore.Windows;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace EFCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Product> _productsObservableCollection;

        public MainWindow()
        {
            InitializeComponent();

            _productsObservableCollection = new ObservableCollection<Product>(App.DatabaseContext.Products.Include(p => p.Vendor));

            UpdateListBox();
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (UtilityHelper.AskQuestion("Вы хотите удалить этот продукт?"))
            {
                Button senderButton = (Button)sender;

                int id = (int)senderButton.Tag;

                Product productToDelete = _productsObservableCollection.FirstOrDefault(product => product.Id == id);

                if (productToDelete != null)
                {
                    _productsObservableCollection.Remove(productToDelete);

                    App.DatabaseContext.Products.Remove(productToDelete);
                    App.DatabaseContext.SaveChanges();
                }
                
                UtilityHelper.ShowInformationMessage("Продукт был удалён");
            }
        }

        private void OrderProductsByPrice_Click(object sender, RoutedEventArgs e)
        {
            List<Product> products = _productsObservableCollection.OrderBy(product => product.Price).ToList();
            _productsObservableCollection = new ObservableCollection<Product>(products);

            UpdateListBox();
        }

        private void CreateNewProduct_Click(object sender, RoutedEventArgs e)
        {
            using (ProductAddWindow productAddWindow = new ProductAddWindow())
            {
                productAddWindow.ShowDialog();
            }

            _productsObservableCollection = new ObservableCollection<Product>(App.DatabaseContext.Products);

            UpdateListBox();
        }

        private void UpdateListBox()
        {
            ProductsListBox.ItemsSource = null;

            UpdateProductImagePaths();

            ProductsListBox.ItemsSource = _productsObservableCollection;
        }

        private void UpdateProductImagePaths()
        {
            foreach (Product product in _productsObservableCollection)
            {
                if (!product.ImageName.Contains("Model/Images"))
                {
                    product.ImageName = $"{App.RootFolder}/Model/Images/Products/{product.ImageName}";
                }
            }
        }
    }
}