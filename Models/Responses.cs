
namespace SurveyApp.Models;

public class Responses
{
   public long? ResponseId {get;set;}
   public long? RespondentId {get;set;}
   public long? SurveyId {get;set;}
   public int? TotalScore {get;set;}

   public virtual Users  Users {get;set;}
   public virtual Surveys  Surveys {get;set;}
}

public class ResponsesListResponse 
{
   public List<Responses> Responses {get;set;}
   public long count {get;set;}
}
