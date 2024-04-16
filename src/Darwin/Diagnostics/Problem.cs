namespace Darwin.Diagnostics;

public sealed record Problem(int LineNumber, string Description, bool IsWarning);