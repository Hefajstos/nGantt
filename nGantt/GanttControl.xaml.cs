using System;
using nGantt.GanttChart;
using nGantt.PeriodSplitter;
using System.Collections.ObjectModel;

namespace nGantt;

public partial class GanttControl
{
    private readonly GanttChartData _ganttChartData = new();
    public ObservableCollection<TimeLine> GridLineTimeLine { get; } = new();
    public GanttChartData GanttData => _ganttChartData;

    public GanttControl()
    {
        InitializeComponent();
        DataContext = this;
    }

    public void Initialize(int days)
    {
        _ganttChartData.MinDate = DateTime.Today;
        _ganttChartData.MaxDate = DateTime.Today.AddDays(days);
    }

    public void AddGanttTask(GanttRow row, GanttTask task)
    {
        if (task.Start < _ganttChartData.MaxDate && task.End > _ganttChartData.MinDate)
            row.Tasks.Add(task);
    }

    public void CreateTimeLine(int days)
    {
        PeriodSplitter.PeriodSplitter splitter = new PeriodDaySplitter(DateTime.Today, DateTime.Today.AddDays(days));
        if (splitter.MaxDate != GanttData.MaxDate || splitter.MinDate != GanttData.MinDate)
            throw new ArgumentException("The timeline must have the same max and min -date as the chart");

        var timeLineParts = splitter.Split();

        var timeline = new TimeLine();
        int i = 0;
        foreach (var p in timeLineParts)
        {
            i++;
            timeline.Items.Add(new TimeLineItem { Name = i.ToString(), Start = p.Start, End = p.End.AddSeconds(-1) });
        }

        _ganttChartData.TimeLines.Add(timeline);
    }

    public GanttRowGroup CreateGanttRowGroup()
    {
        var rowGroup = new GanttRowGroup() { };
        _ganttChartData.RowGroups.Add(rowGroup);
        return rowGroup;
    }

    public GanttRow CreateGanttRow(GanttRowGroup rowGroup, string name)
    {
        var row = new GanttRow() { Header = name, Tasks = new ObservableCollection<GanttTask>() };
        rowGroup.Rows.Add(row);
        return row;
    }
}