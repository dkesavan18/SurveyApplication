
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public interface IQuestionsService
{
    Task<ResponseObject> CreateQuestions(Questions questions);
    Task<dynamic> GetQuestions(long id);
    Task<long> GetQuestionsCount(RequestFilter filter);
    Task<dynamic> GetQuestionsList(int index,int limit,RequestFilter filter);
    Task<bool> UpdateQuestions(Questions questions);
    Task<bool> DeleteQuestions(long key);
}
