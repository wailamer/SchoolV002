namespace SchoolEnterprise.Services;

public class MarkCalculationService
{
    public (decimal total, string grade, decimal percentage, string result) Calculate(decimal activities, decimal oral, decimal homework, decimal exam)
    {
        var total = activities + oral + homework + exam;
        var percentage = total;
        var grade = total >= 90 ? "ممتاز" : total >= 75 ? "جيد جداً" : total >= 60 ? "جيد" : total >= 50 ? "مقبول" : "ضعيف";
        var result = total >= 50 ? "ناجح" : "راسب";
        return (total, grade, percentage, result);
    }
}
