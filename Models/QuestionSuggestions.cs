
namespace SurveyApp.Models;

public class QuestionSuggestions
{
   public long? SuggestedQuestionId {get;set;}
   public string? SuggestedQuestion {get;set;}
   public long? SurveyCategoryId {get;set;}

   public virtual SurveyCategories  SurveyCategories {get;set;}
}

public class QuestionSuggestionsListResponse 
{
   public List<QuestionSuggestions> QuestionSuggestions {get;set;}
   public long count {get;set;}
}
