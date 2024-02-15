
using SurveyApp.Models;
using SurveyApp.Constants;
using SurveyApp.Schemas;
using System.Runtime.Remoting;
using System.Text.Json;
using System.Text.RegularExpressions;
namespace SurveyApp.Helpers;

public class Util
{
    public string PascalCaseToSnakeCase(string str) {
        return Regex.Replace(str, "[A-Z0-9][0-9]*", "_$0").ToLower().Trim('_');
    }

    public async Task<string> FormWhereQuery(RequestFilter filter)
    {
        TableConstant tblconst = new TableConstant();
        string whereQuery = "";       
        if(filter == null) 
        {
            return whereQuery;
        }   
        if(filter.Conditions == null)
        {
            return whereQuery;
        }
        foreach(Condition condition in filter.Conditions)
        {
            var value = condition.FieldValue;
           
            string oprtor = "";
            if(condition.Operator.Equals("equal"))
            {
                oprtor = "=";
            }
            if(condition.Operator.Equals("notequal"))
            {
                oprtor = "!=";
            }
            if(condition.Operator.Equals("like"))
            {
                oprtor = "like";
            }
            //Console.WriteLine(isNumber);
            if(string.IsNullOrEmpty(whereQuery))
            {
                string tableAlias = tblconst.GetTableAliasName(condition.TableName);
                string field = PascalCaseToSnakeCase(condition.FieldName);
                
                if(value.ValueKind == JsonValueKind.Number || value.ValueKind == JsonValueKind.True || value.ValueKind == JsonValueKind.False)
                {
                    whereQuery = tableAlias+"."+field+ " "+oprtor+" " +condition.FieldValue;
                }
                
                if(value.ValueKind == JsonValueKind.String)
                {
                    if(oprtor == "like")
                    {
                        whereQuery = tableAlias+"."+field+ " "+oprtor+" '"+ condition.FieldValue+"%'";
                    }
                    else
                    {
                        whereQuery = tableAlias+"."+field+ " "+oprtor+" '"+ condition.FieldValue+"'";
                    }
                    
                }
               
            }
            else
            {
               string tableAlias = tblconst.GetTableAliasName(condition.TableName);
                string field = PascalCaseToSnakeCase(condition.FieldName);
               
                if(value.ValueKind == JsonValueKind.Number || value.ValueKind == JsonValueKind.True || value.ValueKind == JsonValueKind.False)
                {
                    whereQuery = whereQuery+" "+filter.Aggregator+" "+ tableAlias+"."+field+ " "+oprtor+" " +condition.FieldValue;
                }
                
                if(value.ValueKind == JsonValueKind.String)
                {
                    if(oprtor == "like")
                    {
                        whereQuery = whereQuery+" "+filter.Aggregator+" "+ tableAlias+"."+field+ " "+oprtor+" '"+ condition.FieldValue+"%'";
                    }
                    else
                    {
                        whereQuery = whereQuery+" "+filter.Aggregator+" "+tableAlias+"."+field+ " "+oprtor+" '"+ condition.FieldValue+"'";
                    }
                    
                }
                

            }
            
        }
        return whereQuery;
    }

    public async Task<string> FormOrderQuery(RequestFilter filter)
    {
        TableConstant tblconst = new TableConstant();
        string orderQuery = "";
        if(!String.IsNullOrEmpty(filter.OrderByTable))
        {
            string tableAlias = tblconst.GetTableAliasName(filter.OrderByTable);
            string snakeCaseField = PascalCaseToSnakeCase(filter.OrderByField);
            orderQuery = "ORDER BY "+tableAlias+"."+snakeCaseField+" "+filter.Sort;
        }
        return orderQuery;
    }
}
