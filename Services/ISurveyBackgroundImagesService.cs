
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public interface ISurveyBackgroundImagesService
{
    Task<ResponseObject> CreateSurveyBackgroundImages(SurveyBackgroundImages surveybackgroundimages);
    Task<dynamic> GetSurveyBackgroundImages(long id);
    Task<long> GetSurveyBackgroundImagesCount(RequestFilter filter);
    Task<dynamic> GetSurveyBackgroundImagesList(int index,int limit,RequestFilter filter);
    Task<bool> UpdateSurveyBackgroundImages(SurveyBackgroundImages surveybackgroundimages);
    Task<bool> DeleteSurveyBackgroundImages(long key);
}
