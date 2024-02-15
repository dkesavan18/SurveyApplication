
namespace SurveyApp.Models;

public class SurveyCategories
{
   public long? SurveyCategoryId {get;set;}
   public string? SurveyCategory {get;set;}

}

public class SurveyCategoriesListResponse 
{
   public List<SurveyCategories> SurveyCategories {get;set;}
   public long count {get;set;}
}
