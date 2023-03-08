using System;
using System.Windows.Media;
using nGantt.GanttChart;
using nGantt.PeriodSplitter;
using System.Collections.ObjectModel;

namespace nGantt;

public partial class GanttControl
{
    private GanttChartData ganttChartData = new();
    private ObservableCollection<TimeLine> gridLineTimeLines = new();

    public delegate string PeriodNameFormatter(Period period);
    public delegate Brush BackgroundFormatter(TimeLineItem timeLineItem);

    public ObservableCollection<TimeLine> GridLineTimeLine { get { return gridLineTimeLines; } }
   
    public GanttChartData GanttData { get { return ganttChartData; } }
    public ObservableCollection<TimeLine> TimeLines { get; private set; }

    public GanttControl()
    {
        InitializeComponent();
        DataContext = this;
    }

    public void Initialize(DateTime minDate, DateTime maxDate)
    {
        ganttChartData.MinDate = minDate;
        ganttChartData.MaxDate = maxDate;
    }

    public void AddGanttTask(GanttRow row, GanttTask task)
    {
        if (task.Start < ganttChartData.MaxDate && task.End > ganttChartData.MinDate)
            row.Tasks.Add(task);
    }

    public void CreateTimeLine(PeriodSplitter.PeriodSplitter splitter, PeriodNameFormatter PeriodNameFormatter)
    {
        if (splitter.MaxDate != GanttData.MaxDate || splitter.MinDate != GanttData.MinDate)
            throw new ArgumentException("The timeline must have the same max and min -date as the chart");

        var timeLineParts = splitter.Split();

        var timeline = new TimeLine();
        foreach (var p in timeLineParts)
        {
            timeline.Items.Add(new TimeLineItem() { Name = PeriodNameFormatter(p), Start = p.Start, End = p.End.AddSeconds(-1) });
        }

        ganttChartData.TimeLines.Add(timeline);
    }

    public GanttRowGroup CreateGanttRowGroup()
    {
        var rowGroup = new GanttRowGroup() { };
        ganttChartData.RowGroups.Add(rowGroup);
        return rowGroup;
    }

    public HeaderedGanttRowGroup CreateGanttRowGroup(string name)
    {
        var rowGroup = new HeaderedGanttRowGroup() { Name = name };
        ganttChartData.RowGroups.Add(rowGroup);
        return rowGroup;
    }

    public GanttRow CreateGanttRow(GanttRowGroup rowGroup, string name)
    {
        var rowHeader = new GanttRowHeader() { Name = name };
        var row = new GanttRow() { RowHeader = rowHeader, Tasks = new ObservableCollection<GanttTask>() };
        rowGroup.Rows.Add(row);
        return row;
    }

    public void SetGridLinesTimeline(TimeLine timeline)
    {
        if (!ganttChartData.TimeLines.Contains(timeline))
            throw new Exception("Invalid timeline");
    }

    public void SetGridLinesTimeline(TimeLine timeline, BackgroundFormatter backgroundFormatter)
    {
        if (!ganttChartData.TimeLines.Contains(timeline))
            throw new Exception("Invalid timeline");

        foreach (var item in timeline.Items)
            item.BackgroundColor = backgroundFormatter(item);

        gridLineTimeLines.Clear();
        gridLineTimeLines.Add(timeline);
        //gridLineTimeLine = timeline;
    }
}