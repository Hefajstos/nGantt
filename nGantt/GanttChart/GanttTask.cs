using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace nGantt.GanttChart;

public class GanttTask : DependencyObject
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Name { get; set; }
    public int StartDay { get; set; }
    public int EndDay { get; set; }
    public int Duration { get; set; }
    
    public GanttTask(string name, int start, int days)
    {
        BackgroundColor = Colors.Blue;
        Name = name;
        Start = DateTime.Today.AddDays(start);
        End = Start.AddDays(days);
        StartDay = start + 1;
        EndDay = start + days;
        Duration = EndDay - StartDay + 1;
        LineVisibility = Visibility.Hidden;
    }

    public SolidColorBrush Background { get; set; }
    public SolidColorBrush Line { get; set; }
    public Visibility LineVisibility { get; set; }

    public Color BackgroundColor
    {
        get => Background.Color;
        set => Background = new SolidColorBrush(value);
    }

    public Color LineColor
    {
        get => Line.Color;
        set => Line = new SolidColorBrush(value);
    }
}