namespace SchoolEnterprise.Models.Domain;

public class StudentRegistration
{
    public int StudentId { get; set; }
    public int GeneralRegisterNumber { get; set; }
    public string MinistryCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public string GrandFatherName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MotherName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhotoPath { get; set; } = string.Empty;
}
