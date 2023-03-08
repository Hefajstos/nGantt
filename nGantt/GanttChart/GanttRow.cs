using System.Collections.ObjectModel;

namespace nGantt.GanttChart;

public class GanttRow
{
    public string Header { get; set; }
    public ObservableCollection<GanttTask> Tasks { get; set; }
}