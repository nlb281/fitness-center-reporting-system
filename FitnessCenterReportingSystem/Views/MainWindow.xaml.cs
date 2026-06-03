using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FitnessCenterReportingSystem.Views;

namespace FitnessCenterReportingSystem;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        MainFrame.Navigate(new AttendanceReportPage());
    }
    
    private void Attendance_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new AttendanceReportPage());
    }

    private void Sections_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new SectionsAnalyticsPage());
    }
    private void Dashboard_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new DashboardPage());
    }
}