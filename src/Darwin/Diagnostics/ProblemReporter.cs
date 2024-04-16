using System.Collections.Generic;

namespace Darwin.Diagnostics;

public static class ProblemReporter
{
    private static readonly List<Problem> _problems = [];

    public static void AddError(int lineNumber, string description)
    {
        _problems.Add(new Problem(lineNumber, description, false));
    }

    public static IEnumerable<Problem> GetAll()
    {
        return _problems;
    }
}