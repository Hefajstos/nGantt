using System;
using System.Collections.Generic;

namespace nGantt.PeriodSplitter;

public class PeriodDaySplitter : PeriodSplitter
{
    public PeriodDaySplitter(DateTime min, DateTime max)
        : base(min, max)
    { }

    public override List<Period> Split()
    {
        DateTime precedingBreak = min.Date;
        return Split(precedingBreak);
    }

    protected override DateTime Increase(DateTime date, int value) => date.AddDays(value);
}