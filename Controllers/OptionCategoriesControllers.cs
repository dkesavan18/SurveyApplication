
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Services;
using SurveyApp.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SurveyApp.Controllers;

[ApiController]
[Route("optioncategories")]
public class OptionCategoriessController : Controller
{
    private readonly IOptionCategoriesService _optioncategoriesService;
    
    public OptionCategoriessController(IOptionCategoriesService optioncategoriesService)
    {
        _optioncategoriesService = optioncategoriesService;
    }

    [HttpPost]
    [Route("list")]
    [Authorize]
    public async Task<OptionCategoriesListResponse> Get([FromBody] RequestFilter request)
    {
        var optioncategoriesList =  await _optioncategoriesService.GetOptionCategoriesList(request.Index,request.Limit,request);
        var count = await _optioncategoriesService.GetOptionCategoriesCount(request);

        OptionCategoriesListResponse respObj = new OptionCategoriesListResponse();   
        
        respObj.count = count;            
            
        
        List<OptionCategories> listData = new List<OptionCategories>();
        foreach(var optioncategories in optioncategoriesList)
        {
            OptionCategories optioncategoriesObj = new OptionCategories();
     
                optioncategoriesObj.OptionCategoryId = optioncategories.optioncategoryid ?? null; 
                optioncategoriesObj.OptionCategory = optioncategories.optioncategory; 
                optioncategoriesObj.DefaultChoices = optioncategories.defaultchoices;
    
            listData.Add(optioncategoriesObj);
        }
        respObj.OptionCategories = new List<OptionCategories>();
        respObj.OptionCategories = listData;
        return respObj;
    }
    
    [HttpGet("{id:long}")]
    [Authorize]
    public async Task<OptionCategories> GetOptionCategories(long id)
    {
        var optioncategories =  await _optioncategoriesService.GetOptionCategories(id);
        OptionCategories optioncategoriesObj = new OptionCategories();
     
                optioncategoriesObj.OptionCategoryId = optioncategories.optioncategoryid ?? null; 
                optioncategoriesObj.OptionCategory = optioncategories.optioncategory; 
                optioncategoriesObj.DefaultChoices = optioncategories.defaultchoices;
    
        return optioncategoriesObj;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddOptionCategories([FromBody]OptionCategories optioncategories)
    {
        var result =  await _optioncategoriesService.CreateOptionCategories(optioncategories);

        return Ok(result);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateOptionCategories([FromBody]OptionCategories optioncategories)
    {
        var result =  await _optioncategoriesService.UpdateOptionCategories(optioncategories);

        return Ok(result);
    }
    
    [HttpDelete("{id:long}")]
    [Authorize]
    public async Task<IActionResult> DeleteOptionCategories(long id)
    {
        var result =  await _optioncategoriesService.DeleteOptionCategories(id);

        return Ok(result);
    }
}
