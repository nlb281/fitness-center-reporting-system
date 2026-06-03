using System.Windows.Controls;
using FitnessCenterReportingSystem.ViewModels;

namespace FitnessCenterReportingSystem.Views;

public partial class SectionsAnalyticsPage : Page
{
    public SectionsAnalyticsPage()
    {
        InitializeComponent();
        DataContext = new SectionsAnalyticsViewModel();
    }
}