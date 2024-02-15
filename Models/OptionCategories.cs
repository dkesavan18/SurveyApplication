
namespace SurveyApp.Models;

public class OptionCategories
{
   public long? OptionCategoryId {get;set;}
   public string? OptionCategory {get;set;}
   public object? DefaultChoices {get;set;}

}

public class OptionCategoriesListResponse 
{
   public List<OptionCategories> OptionCategories {get;set;}
   public long count {get;set;}
}
