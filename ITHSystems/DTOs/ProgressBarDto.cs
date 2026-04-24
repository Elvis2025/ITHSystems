namespace ITHSystems.DTOs;

public sealed record class ProgressBarDto
{
    public int DownloadedCount { get; set; }
    public int InsertedCount { get; set; }
    public string Message { get; set; } = string.Empty;
    public double Progress { get; set; }
    public int PageSize => 100;
}
