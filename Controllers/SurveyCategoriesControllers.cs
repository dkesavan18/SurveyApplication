
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Services;
using SurveyApp.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SurveyApp.Controllers;

[ApiController]
[Route("surveycategories")]
public class SurveyCategoriessController : Controller
{
    private readonly ISurveyCategoriesService _surveycategoriesService;

    public SurveyCategoriessController(ISurveyCategoriesService surveycategoriesService)
    {
        _surveycategoriesService = surveycategoriesService;
    }

    [HttpPost]
    [Route("list")]
    [Authorize]
    public async Task<SurveyCategoriesListResponse> Get([FromBody] RequestFilter request)
    {
        var surveycategoriesList = await _surveycategoriesService.GetSurveyCategoriesList(request.Index, request.Limit, request);
        var count = await _surveycategoriesService.GetSurveyCategoriesCount(request);

        SurveyCategoriesListResponse respObj = new SurveyCategoriesListResponse();

        respObj.count = count;


        List<SurveyCategories> listData = new List<SurveyCategories>();
        foreach (var surveycategories in surveycategoriesList)
        {
            SurveyCategories surveycategoriesObj = new SurveyCategories();

            surveycategoriesObj.SurveyCategoryId = surveycategories.surveycategoryid ?? null;
            surveycategoriesObj.SurveyCategory = surveycategories.surveycategory;

            listData.Add(surveycategoriesObj);
        }
        respObj.SurveyCategories = new List<SurveyCategories>();
        respObj.SurveyCategories = listData;
        return respObj;
    }

    [HttpGet("{id:long}")]
    [Authorize]
    public async Task<SurveyCategories> GetSurveyCategories(long id)
    {
        var surveycategories = await _surveycategoriesService.GetSurveyCategories(id);
        SurveyCategories surveycategoriesObj = new SurveyCategories();

        surveycategoriesObj.SurveyCategoryId = surveycategories.surveycategoryid ?? null;
        surveycategoriesObj.SurveyCategory = surveycategories.surveycategory;

        return surveycategoriesObj;
    }

    [HttpPost]
    // [Authorize]
    public async Task<IActionResult> AddSurveyCategories([FromBody] SurveyCategories surveycategories)
    {
        var result = await _surveycategoriesService.CreateSurveyCategories(surveycategories);

        return Ok(result);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateSurveyCategories([FromBody] SurveyCategories surveycategories)
    {
        var result = await _surveycategoriesService.UpdateSurveyCategories(surveycategories);

        return Ok(result);
    }

    [HttpDelete("{id:long}")]
    [Authorize]
    public async Task<IActionResult> DeleteSurveyCategories(long id)
    {
        var result = await _surveycategoriesService.DeleteSurveyCategories(id);

        return Ok(result);
    }
}
