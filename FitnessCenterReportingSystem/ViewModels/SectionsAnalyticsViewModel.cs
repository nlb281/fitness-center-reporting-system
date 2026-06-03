using FitnessCenterReportingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FitnessCenterReportingSystem.ViewModels;

public class SectionsAnalyticsViewModel : INotifyPropertyChanged
{
    public ObservableCollection<SectionStatistics> Sections { get; set; } = [];

    private string _mostPopularSection = "";

    public string MostPopularSection
    {
        get => _mostPopularSection;
        set
        {
            _mostPopularSection = value;
            OnPropertyChanged();
        }
    }

    private int _totalVisitors;

    public int TotalVisitors
    {
        get => _totalVisitors;
        set
        {
            _totalVisitors = value;
            OnPropertyChanged();
        }
    }

    private int _sectionsCount;

    public int SectionsCount
    {
        get => _sectionsCount;
        set
        {
            _sectionsCount = value;
            OnPropertyChanged();
        }
    }

    public SectionsAnalyticsViewModel()
    {
        LoadData();
    }

    public void LoadData()
    {
        using var db = new FokReportingContext();

        var statistics = db.Visits
            .Include(v => v.Section)
            .AsEnumerable()
            .GroupBy(v => v.Section.Name)
            .Select(g => new SectionStatistics
            {
                SectionName = g.Key,
                TotalVisitors = g.Sum(x => x.AttendedVisitors)
            })
            .OrderByDescending(x => x.TotalVisitors)
            .ToList();

        Sections.Clear();

        foreach (var item in statistics)
        {
            Sections.Add(item);
        }

        SectionsCount = statistics.Count;

        TotalVisitors = statistics.Sum(x => x.TotalVisitors);

        MostPopularSection =
            statistics.FirstOrDefault()?.SectionName ?? "-";
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}