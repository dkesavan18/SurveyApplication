
namespace SurveyApp.Models;

public class SurveyBackgroundImages
{
   public long? SurveyBackgroundId {get;set;}
   public string? BackgroundImage {get;set;}

}

public class SurveyBackgroundImagesListResponse 
{
   public List<SurveyBackgroundImages> SurveyBackgroundImages {get;set;}
   public long count {get;set;}
}
