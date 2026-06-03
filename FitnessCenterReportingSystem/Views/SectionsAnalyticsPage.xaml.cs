using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using FitnessCenterReportingSystem.ViewModels;
using Microsoft.Win32;

namespace FitnessCenterReportingSystem.Views;

public partial class SectionsAnalyticsPage : Page
{
    private AttendanceReportViewModel ViewModel =>
        (AttendanceReportViewModel)DataContext;
    
    public SectionsAnalyticsPage()
    {
        InitializeComponent();
        DataContext = new SectionsAnalyticsViewModel();
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
            var viewModel =
                (SectionsAnalyticsViewModel)DataContext;

            var dialog = new SaveFileDialog
            {
                Filter = "CSV файлы (*.csv)|*.csv",
                FileName =
                    $"SectionsAnalytics_{DateTime.Now:yyyyMMdd}.csv"
            };

            if (dialog.ShowDialog() != true)
                return;

            using var writer =
                new StreamWriter(
                    dialog.FileName,
                    false,
                    Encoding.UTF8);

            writer.WriteLine(
                "Секция;Количество посетителей");

            foreach (var section in viewModel.Sections)
            {
                writer.WriteLine(
                    $"{section.SectionName};" +
                    $"{section.TotalVisitors}");
            }

            MessageBox.Show(
                "Отчет успешно экспортирован.",
                "Экспорт CSV",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Ошибка экспорта",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}