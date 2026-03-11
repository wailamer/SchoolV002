namespace SchoolEnterprise.Services;

public class MarkCalculationService
{
    public (decimal total, string grade, decimal percentage, string result) Calculate(decimal activities, decimal oral, decimal homework, decimal exam)
    {
        var total = activities + oral + homework + exam;
        var percentage = total;
        var grade = percentage switch
        {
            >= 90 => "ممتاز",
            >= 80 => "جيد جداً",
            >= 70 => "جيد",
            >= 60 => "مقبول",
            _ => "ضعيف"
        };
        var result = percentage >= 50 ? "ناجح" : "راسب";
        return (total, grade, percentage, result);
    }
}
