using System.Collections.Generic;

namespace Darwin.Diagnostics;

public sealed class ProblemReporter
{
    private readonly List<Problem> _problems = [];

    public void AddError(int lineNumber, string description)
    {
        _problems.Add(new Problem(lineNumber, description, false));
    }

    public IEnumerable<Problem> GetAll()
    {
        return _problems;
    }
}