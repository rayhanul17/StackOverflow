using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Areas.Admin.Models;
using StackOverflow.Web.Extensions;
using StackOverflow.Web.Models;

namespace StackOverflow.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class QuestionController : Controller
{
    private readonly ILogger<QuestionController> _logger;
    private readonly ILifetimeScope _scope;

    public QuestionController(ILogger<QuestionController> logger, ILifetimeScope scope)
    {
        _logger = logger;
        _scope = scope;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<JsonResult> GetQuestions()
    {
        var dataTableModel = new DataTablesAjaxRequestModel(Request);
        var model = _scope.Resolve<GetQuestionsModel>();

        return Json(await model.GetQuestionsAsync(dataTableModel));
    }

    public IActionResult Ask()
    {
        _logger.LogInformation("You are in Admin/Ask\n");
        var model = _scope.Resolve<QuestionModel>();

        return View(model);
    }

    [HttpPost]
    public IActionResult Ask(QuestionModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                model.Ask();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception Message: {ex.Message}\nException: {ex}\n\n");
        }

        return View(model);
    }

    public IActionResult Edit(Guid id)
    {
        //var model = _scope.Resolve<EditCourseModel>();
        //model.GetCourse(id);
        return View();
    }
    public IActionResult Delete(Guid id)
    {
        try
        {
            var model = _scope.Resolve<QuestionModel>();
            model.Delete(id);

            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Successfully Deleted Course",
                Type = ResponseTypes.Success
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Failed to Delete Course",
                Type = ResponseTypes.Danger
            });
        }

        return RedirectToAction("Index");
    }

    //[HttpPost]
    //[ValidateAntiForgeryToken]

    //public IActionResult Delete(Guid id)
    //{
    //    try
    //    {
    //        var model = _scope.Resolve<DeleteCourseModel>();
    //        model.DeleteCourse(id);

    //        ViewResponse("Category successfully deleted.", ResponseTypes.Success);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, ex.Message);
    //        ViewResponse(ex.Message, ResponseTypes.Danger);
    //    }
    //    return RedirectToAction("Index", "Course", new { Area = "Admin" });
    //}
}
