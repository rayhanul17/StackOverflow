using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Areas.Admin.Models;
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
        var list = await model.GetQuestionsAsync(dataTableModel);
        return Json(list);
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
        //try
        //{
        //    var model = _scope.Resolve<DeleteCourseModel>();
        //    model.DeleteCourse(id);

        //    ViewResponse("Category successfully deleted.", ResponseTypes.Success);
        //}
        //catch (Exception ex)
        //{
        //    _logger.LogError(ex, ex.Message);
        //    ViewResponse(ex.Message, ResponseTypes.Error);
        //}
        return RedirectToAction("Index", "Question", new { Area = "Admin" });
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
