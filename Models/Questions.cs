
namespace SurveyApp.Models;

public class Questions
{
   public long? QuestionId { get; set; }
   public long? SurveyId { get; set; }
   public string? Question { get; set; }
   public string? QuestionCategory { get; set; }
   public long? OptionCategoryId { get; set; }
   public object? OptionText { get; set; }
   public string? QuestionTextFormatting { get; set; }

   public virtual Surveys Surveys { get; set; }
   public virtual OptionCategories OptionCategories { get; set; }
}

public class QuestionsListResponse
{
   public List<Questions> Questions { get; set; }
   public long count { get; set; }
}
