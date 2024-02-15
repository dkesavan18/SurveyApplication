
using System.Collections.Generic;
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Schemas;

namespace SurveyApp.Services;

public interface IQuestionSuggestionsService
{
    Task<ResponseObject> CreateQuestionSuggestions(QuestionSuggestions questionsuggestions);
    Task<dynamic> GetQuestionSuggestions(long id);
    Task<long> GetQuestionSuggestionsCount(RequestFilter filter);
    Task<dynamic> GetQuestionSuggestionsList(int index,int limit,RequestFilter filter);
    Task<bool> UpdateQuestionSuggestions(QuestionSuggestions questionsuggestions);
    Task<bool> DeleteQuestionSuggestions(long key);
}
