namespace STD.PL.Records;

public sealed class Error
{
    public int Code { get; set; }
    public string? Reason { get; set; }
    public string? Detail { get; set; }
    public List<string>? Errors { get; set; }
    public string? StackTrace { get; set; }
}