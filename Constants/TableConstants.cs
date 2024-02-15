
namespace SurveyApp.Constants;

public class TableConstant
{
    public string GetTableAliasName(string tableName)
    {
        switch(tableName)
        {
            case "GroupMembers" : return "gm";
            case "Groups" : return "groups";
            case "OptionCategories" : return "oc";
            case "QuestionSuggestions" : return "qs";
            case "Questions" : return "questions";
            case "Responses" : return "responses";
            case "SurveyActivityLogs" : return "sal";
            case "SurveyBackgroundImages" : return "sbi";
            case "SurveyCategories" : return "sc";
            case "SurveyResponses" : return "sr";
            case "Surveys" : return "surveys";
            case "Users" : return "users";
            default: throw new ArgumentOutOfRangeException();
        }
    }
}
