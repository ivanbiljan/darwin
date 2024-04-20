using System.Collections.Generic;
using Darwin.LexicalAnalysis;

namespace Darwin.Diagnostics;

public static class DiagnosticBag
{
    private static readonly List<Diagnostic> _problems = [];

    public static void AddError(SourceLocation location, string description)
    {
        _problems.Add(
            new Diagnostic
            {
                Location = location,
                Message = description,
                Severity = DiagnosticSeverity.Error
            }
        );
    }

    public static IEnumerable<Diagnostic> GetAll()
    {
        return _problems;
    }
}