namespace SchoolEnterprise.Models.Domain;

public class AttendanceEntry
{
    public int AttendanceId { get; set; }
    public int StudentRecordId { get; set; }
    public DateTime Date { get; set; }
    public bool IsPresent { get; set; }
    public string Notes { get; set; } = string.Empty;
}
