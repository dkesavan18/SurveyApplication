
using System.Threading.Tasks;
using SurveyApp.Models;
using SurveyApp.Services;
using SurveyApp.Schemas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SurveyApp.Controllers;

[ApiController]
[Route("surveybackgroundimages")]
public class SurveyBackgroundImagessController : Controller
{
    private readonly ISurveyBackgroundImagesService _surveybackgroundimagesService;
    
    public SurveyBackgroundImagessController(ISurveyBackgroundImagesService surveybackgroundimagesService)
    {
        _surveybackgroundimagesService = surveybackgroundimagesService;
    }

    [HttpPost]
    [Route("list")]
    [Authorize]
    public async Task<SurveyBackgroundImagesListResponse> Get([FromBody] RequestFilter request)
    {
        var surveybackgroundimagesList =  await _surveybackgroundimagesService.GetSurveyBackgroundImagesList(request.Index,request.Limit,request);
        var count = await _surveybackgroundimagesService.GetSurveyBackgroundImagesCount(request);

        SurveyBackgroundImagesListResponse respObj = new SurveyBackgroundImagesListResponse();   
        
        respObj.count = count;            
            
        
        List<SurveyBackgroundImages> listData = new List<SurveyBackgroundImages>();
        foreach(var surveybackgroundimages in surveybackgroundimagesList)
        {
            SurveyBackgroundImages surveybackgroundimagesObj = new SurveyBackgroundImages();
     
                surveybackgroundimagesObj.SurveyBackgroundId = surveybackgroundimages.surveybackgroundid ?? null; 
                surveybackgroundimagesObj.BackgroundImage = surveybackgroundimages.backgroundimage;
    
            listData.Add(surveybackgroundimagesObj);
        }
        respObj.SurveyBackgroundImages = new List<SurveyBackgroundImages>();
        respObj.SurveyBackgroundImages = listData;
        return respObj;
    }
    
    [HttpGet("{id:long}")]
    [Authorize]
    public async Task<SurveyBackgroundImages> GetSurveyBackgroundImages(long id)
    {
        var surveybackgroundimages =  await _surveybackgroundimagesService.GetSurveyBackgroundImages(id);
        SurveyBackgroundImages surveybackgroundimagesObj = new SurveyBackgroundImages();
     
                surveybackgroundimagesObj.SurveyBackgroundId = surveybackgroundimages.surveybackgroundid ?? null; 
                surveybackgroundimagesObj.BackgroundImage = surveybackgroundimages.backgroundimage;
    
        return surveybackgroundimagesObj;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddSurveyBackgroundImages([FromBody]SurveyBackgroundImages surveybackgroundimages)
    {
        var result =  await _surveybackgroundimagesService.CreateSurveyBackgroundImages(surveybackgroundimages);

        return Ok(result);
    }
    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateSurveyBackgroundImages([FromBody]SurveyBackgroundImages surveybackgroundimages)
    {
        var result =  await _surveybackgroundimagesService.UpdateSurveyBackgroundImages(surveybackgroundimages);

        return Ok(result);
    }
    
    [HttpDelete("{id:long}")]
    [Authorize]
    public async Task<IActionResult> DeleteSurveyBackgroundImages(long id)
    {
        var result =  await _surveybackgroundimagesService.DeleteSurveyBackgroundImages(id);

        return Ok(result);
    }
}
