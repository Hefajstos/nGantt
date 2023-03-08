using System;
using System.Collections.Generic;

namespace nGantt.PeriodSplitter;

public abstract class PeriodSplitter
{
    private readonly List<Period> _result = new();
    protected DateTime Min;
    private readonly DateTime _max;

    protected PeriodSplitter(DateTime min, DateTime max)
    {
        Min = min;
        _max = max;
    }

    public DateTime MinDate => Min;
    public DateTime MaxDate => _max;

    public abstract List<Period> Split();

    protected abstract DateTime Increase(DateTime date, int value);

    protected List<Period> Split(DateTime offsetDate)
    {
        Period firstPeriod = new Period { Start = Min, End = Increase(offsetDate, 1) };
        _result.Add(firstPeriod);

        if (firstPeriod.End >= _max)
        {
            firstPeriod.End = _max;
            return _result;
        }

        int i = 1;
        while (Increase(offsetDate, i) < _max)
        {
            Period period = new Period { Start = Increase(offsetDate, i), End = Increase(offsetDate, i + 1) };
            if (period.End >= _max)
                period.End = _max;

            _result.Add(period);
            i++;
        }

        return _result;
    }
}