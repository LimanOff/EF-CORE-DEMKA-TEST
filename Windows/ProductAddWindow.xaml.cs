using EFCore.Model.DatabaseModel.Entities;
using EFCore.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EFCore.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProductAddWindow.xaml
    /// </summary>
    public partial class ProductAddWindow : Window, IDisposable
    {
        private string _newDirectoryToSaveImages = "Model/Images/Products";

        private string _imageName = "EMPTY";

        public ProductAddWindow()
        {
            InitializeComponent();

            ImageOfProduct.Source = new BitmapImage(new Uri($"{App.RootFolder}/Model/Images/Products/PlaceHolder.png"));
        }

        public void Dispose()
        {
            Close();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            // Тут ещё по хорошему сделать защиту от повторной отправки одинаковых данных

            if (_imageName != "EMPTY")
            {
                Product newProduct = new Product()
                {
                    VendorId = 1,
                    Price = 300,
                    ImageName = _imageName
                };

                App.DatabaseContext.Products.Add(newProduct);
                App.DatabaseContext.SaveChanges();

                UtilityHelper.ShowInformationMessage("Продукт был добавлен");
            }
            else
            {
                UtilityHelper.ShowErrorMessage("Не добавлено изображение для продукта");
            }
        }

        private void AddImageProduct_Click(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PNG изображения (*.png)|*.png| JPG изображение (*.jpg)|*.jpg";

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileURL = openFileDialog.FileName;

                    _imageName = openFileDialog.SafeFileName;

                    string newFileURL = $"{App.RootFolder}/{_newDirectoryToSaveImages}/{_imageName}";

                    if (!File.Exists(newFileURL))
                    {
                        File.Copy(fileURL, newFileURL);
                        ImageOfProduct.Source = new BitmapImage(new Uri($"{App.RootFolder}/Model/Images/Products/{_imageName}"));
                    }
                    else
                    {
                        UtilityHelper.ShowErrorMessage("Изображение с таким именем уже добавлено.\nПопробуйте переименовать файл");
                    }
                }
            }
        }
    }
}
