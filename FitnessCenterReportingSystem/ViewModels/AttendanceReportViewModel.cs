using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FitnessCenterReportingSystem.Models;

namespace FitnessCenterReportingSystem.ViewModels;

public class AttendanceReportViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Visit> Visits { get; set; } = [];

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

    private string _attendancePercent = "";

    public string AttendancePercent
    {
        get => _attendancePercent;
        set
        {
            _attendancePercent = value;
            OnPropertyChanged();
        }
    }

    public AttendanceReportViewModel()
    {
        LoadData();
    }

    public void LoadData()
    {
        using var db = new FokReportingContext();

        var visits = db.Visits
            .Include(v => v.Section)
            .Include(v => v.Coach)
            .ToList();

        Visits.Clear();

        foreach (var visit in visits)
        {
            Visits.Add(visit);
        }

        var registered = visits.Sum(v => v.RegisteredVisitors);
        var attended = visits.Sum(v => v.AttendedVisitors);

        TotalVisitors = attended;

        AttendancePercent =
            registered == 0
                ? "0%"
                : $"{(double)attended / registered:P0}";
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}