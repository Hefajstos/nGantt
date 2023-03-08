using System;
using System.Windows;
using System.Windows.Media;

namespace nGantt.GanttChart;

public class GanttTask : DependencyObject
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Name { get; set; }

    public GanttTask()
    {
        BackgroundColor = Colors.Blue;
    }

    public SolidColorBrush Background { get; set; }

    public Color BackgroundColor
    {
        get => Background.Color;
        set => Background = new SolidColorBrush(value);
    }
}