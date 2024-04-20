using Darwin.LexicalAnalysis;

namespace Darwin.Diagnostics;

public sealed record Diagnostic
{
    public required SourceLocation Location { get; init; }
    
    public required string Message { get; init; }
    
    public required DiagnosticSeverity Severity { get; init; }
}