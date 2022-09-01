using System;
using System.Collections.Generic;

namespace nGantt.PeriodSplitter;

public class PeriodYearSplitter : PeriodSplitter
{
    public PeriodYearSplitter(DateTime min, DateTime max)
        : base(min, max)
    { }

    public override List<Period> Split()
    {
        DateTime precedingBreak = new DateTime(min.Year, 1, 1);
        return Split(precedingBreak);
    }

    protected override DateTime Increase(DateTime date, int value) => date.AddYears(value);
}