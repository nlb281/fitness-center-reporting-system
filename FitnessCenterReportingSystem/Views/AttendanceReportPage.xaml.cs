using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using FitnessCenterReportingSystem.ViewModels;
using Microsoft.Win32;

namespace FitnessCenterReportingSystem.Views;

public partial class AttendanceReportPage : Page
{
    private AttendanceReportViewModel ViewModel =>
        (AttendanceReportViewModel)DataContext;
    
    public AttendanceReportPage()
    {
        InitializeComponent();
        
        DataContext = new AttendanceReportViewModel();
    }
    
    private void ReloadButton_Click(object sender, RoutedEventArgs e)
    {
        ViewModel.LoadData();

        MessageBox.Show(
            "Данные успешно обновлены",
            "Обновление",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }
    
    private void ExportCsvButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var dialog = new SaveFileDialog
            {
                Filter = "CSV файлы (*.csv)|*.csv",
                FileName = $"AttendanceReport_{DateTime.Now:yyyyMMdd}.csv"
            };

            if (dialog.ShowDialog() != true)
                return;

            using var writer =
                new StreamWriter(dialog.FileName, false, Encoding.UTF8);

            writer.WriteLine("Дата;Секция;Тренер;Записано;Посетило");

            foreach (var visit in ViewModel.Visits)
            {
                writer.WriteLine(
                    $"{visit.Date:dd.MM.yyyy};" +
                    $"{visit.Section?.Name};" +
                    $"{visit.Coach?.Fio};" +
                    $"{visit.RegisteredVisitors};" +
                    $"{visit.AttendedVisitors}");
            }

            MessageBox.Show(
                "Отчет успешно экспортирован",
                "Экспорт",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Ошибка",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}