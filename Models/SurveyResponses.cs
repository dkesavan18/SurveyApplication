
namespace SurveyApp.Models;

public class SurveyResponses
{
   public long? SurveyResponseId {get;set;}
   public long? ResponseId {get;set;}
   public long? QuestionId {get;set;}
   public string? Option {get;set;}

   public virtual Questions  Questions {get;set;}
}

public class SurveyResponsesListResponse 
{
   public List<SurveyResponses> SurveyResponses {get;set;}
   public long count {get;set;}
}
