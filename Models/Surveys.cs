
namespace SurveyApp.Models;

public class Surveys
{
   public long? SurveyId {get;set;}
   public string? SurveyName {get;set;}
   public int? SurveyCategoryId {get;set;}
   public int? BackroundImageId {get;set;}
   public long? UserId {get;set;}
   public DateTime? CreatedAt {get;set;}
   public string? SurveyStatus {get;set;}

   public virtual SurveyCategories  SurveyCategories {get;set;}
   public virtual SurveyBackgroundImages  SurveyBackgroundImages {get;set;}
   public virtual Users  Users {get;set;}
}

public class SurveysListResponse 
{
   public List<Surveys> Surveys {get;set;}
   public long count {get;set;}
}
