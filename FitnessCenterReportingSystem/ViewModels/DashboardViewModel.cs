using FitnessCenterReportingSystem.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FitnessCenterReportingSystem.ViewModels;

public class DashboardViewModel : INotifyPropertyChanged
{
    private int _totalVisitors;
    private int _totalSessions;
    private double _attendancePercent;
    private int _sectionsCount;
    private string _topSection = "-";

    public int TotalVisitors
    {
        get => _totalVisitors;
        set
        {
            _totalVisitors = value;
            OnPropertyChanged();
        }
    }

    public int TotalSessions
    {
        get => _totalSessions;
        set
        {
            _totalSessions = value;
            OnPropertyChanged();
        }
    }

    public double AttendancePercent
    {
        get => _attendancePercent;
        set
        {
            _attendancePercent = value;
            OnPropertyChanged();
        }
    }

    public int SectionsCount
    {
        get => _sectionsCount;
        set
        {
            _sectionsCount = value;
            OnPropertyChanged();
        }
    }

    public string TopSection
    {
        get => _topSection;
        set
        {
            _topSection = value;
            OnPropertyChanged();
        }
    }

    public ISeries[] VisitorsSeries { get; set; } = [];

    public Axis[] XAxes { get; set; } = [];

    public DashboardViewModel()
    {
        LoadData();
    }

    public void LoadData()
    {
        using var db = new FokReportingContext();

        var visits = db.Visits
            .Include(v => v.Section)
            .ToList();

        TotalVisitors = visits.Sum(v => v.AttendedVisitors);

        TotalSessions = visits.Count;

        SectionsCount = db.Sections.Count();

        var totalRegistered = visits.Sum(v => v.RegisteredVisitors);

        AttendancePercent = totalRegistered == 0
            ? 0
            : Math.Round(
                (double)TotalVisitors /
                totalRegistered * 100,
                1);

        var sectionStats = visits
            .GroupBy(v => v.Section.Name)
            .Select(g => new
            {
                SectionName = g.Key,
                Visitors = g.Sum(v => v.AttendedVisitors)
            })
            .OrderByDescending(x => x.Visitors)
            .ToList();

        TopSection =
            sectionStats.FirstOrDefault()?.SectionName ?? "-";

        VisitorsSeries =
        [
            new ColumnSeries<int>
            {
                Values = sectionStats
                    .Select(x => x.Visitors)
                    .ToArray()
            }
        ];

        XAxes =
        [
            new Axis
            {
                Labels = sectionStats
                    .Select(x => x.SectionName)
                    .ToArray(),

                IsVisible = false
            }
        ];

        OnPropertyChanged(nameof(VisitorsSeries));
        OnPropertyChanged(nameof(XAxes));
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