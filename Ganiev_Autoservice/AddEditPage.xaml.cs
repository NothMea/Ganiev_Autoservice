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

namespace Ganiev_Autoservice
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Service _currentService = new Service();

        public AddEditPage(Service SelectedService)
        {
            InitializeComponent();

            if (SelectedService != null)
            {
                _currentService = SelectedService;
            }

            DataContext = _currentService;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentService.Title))
                errors.AppendLine("укажите название услуги");

            if (_currentService.Cost <= 0)
                errors.AppendLine("Правильно укажите стоимость услуги");

            if (_currentService.DiscountInt < 0 || _currentService.Discount == null)
                errors.AppendLine("Укажите скидку правильно");

            if (_currentService.DurationInSeconds == 0)
                errors.AppendLine("Укажите длительность");

            if (_currentService.DurationInSeconds > 240 || _currentService.DurationInSeconds < 0)
                errors.AppendLine("Длительность не может быть больше 240 минут");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var allServices = Ganiev_autoserviceEntities.GetContext().Service.ToList();
            allServices = allServices.Where(p => p.Title == _currentService.Title).ToList();
            if (allServices.Count == 0)
            {
                if (_currentService.ID == 0)
                {
                    Ganiev_autoserviceEntities.GetContext().Service.Add(_currentService);
                }
                try
                {
                    Ganiev_autoserviceEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена");
                    Manager.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Уже существует такая услуга");
            }

            if (_currentService.ID == 0)
                Ganiev_autoserviceEntities.GetContext().Service.Add(_currentService);

            try
            {
                Ganiev_autoserviceEntities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
