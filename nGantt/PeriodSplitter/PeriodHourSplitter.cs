using System;
using System.Collections.Generic;

namespace nGantt.PeriodSplitter;

public class PeriodHourSplitter : PeriodSplitter
{
    public PeriodHourSplitter(DateTime min, DateTime max)
        : base(min, max)
    { }

    public override List<Period> Split()
    {
        DateTime precedingBreak = new DateTime(min.Year, min.Month, min.Day, min.Hour, 0, 0);
        return Split(precedingBreak);
    }

    protected override DateTime Increase(DateTime date, int value) => date.AddHours(value);
}