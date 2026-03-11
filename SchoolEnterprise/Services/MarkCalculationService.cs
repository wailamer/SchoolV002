namespace SchoolEnterprise.Services;

public class MarkCalculationService
{
    public (decimal total, string grade) Calculate(decimal activities, decimal oral, decimal homework, decimal exam)
    {
        var total = activities + oral + homework + exam;
        var grade = total >= 90 ? "ممتاز" : total >= 75 ? "جيد جدًا" : total >= 60 ? "جيد" : total >= 50 ? "مقبول" : "راسب";
        return (total, grade);
    }

    public decimal Percentage(IEnumerable<decimal> totals) => totals.Any() ? Math.Round(totals.Average(), 2) : 0;
}
