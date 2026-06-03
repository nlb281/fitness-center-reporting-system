using System.Windows.Controls;
using FitnessCenterReportingSystem.ViewModels;

namespace FitnessCenterReportingSystem.Views;

public partial class DashboardPage : Page
{
    public DashboardPage()
    {
        InitializeComponent();
        
        DataContext = new DashboardViewModel();
    }
}