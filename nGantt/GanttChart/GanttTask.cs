using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace nGantt.GanttChart;

public class GanttTask : DependencyObject
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Name { get; set; }
    public Visibility TaskProgressVisibility { get; set; }
    private double percentageCompleted;

    public GanttTask()
    {
        IsEnabled = true;
        TaskProgressVisibility = Visibility.Visible;
        BackgroundColor = Colors.Blue;
    }

    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(GanttTask), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public static readonly DependencyProperty IsEnabledProperty =
        DependencyProperty.Register(nameof(IsEnabled), typeof(bool), typeof(GanttTask), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    public SolidColorBrush Background { get; set; }

    public Color BackgroundColor
    {
        get => Background.Color;
        set => Background = new SolidColorBrush(value);
    }

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    public double PercentageCompleted
    {
        get => 1 - percentageCompleted;
        set => percentageCompleted = value;
    }
    public string PercentageCompletedText => string.Format("{0}%", Math.Round(percentageCompleted * 100, 0));

}