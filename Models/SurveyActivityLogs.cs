
namespace SurveyApp.Models;

public class SurveyActivityLogs
{
   public long? LogId {get;set;}
   public DateTime? StartTime {get;set;}
   public DateTime? EndTime {get;set;}
   public long? SurveyId {get;set;}
   public string? UserAction {get;set;}
   public DateTime? LastModified {get;set;}

   public virtual Surveys  Surveys {get;set;}
}

public class SurveyActivityLogsListResponse 
{
   public List<SurveyActivityLogs> SurveyActivityLogs {get;set;}
   public long count {get;set;}
}
