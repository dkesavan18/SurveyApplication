
namespace SurveyApp.Schemas;

public class Condition
{
    public string TableName {get;set;}
    public string FieldName {get;set;}
    public string Operator {get;set;}
    public dynamic FieldValue {get;set;}
}

public class RequestFilter
{
    public int Index {get;set;}
    public int Limit {get;set;}
    public string? OrderByTable {get;set;}
    public string? OrderByField {get;set;}
    public string? Sort {get;set;}
    public string? Aggregator {get;set;}
    public List<Condition>? Conditions {get;set;}
}
