using Autofac;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Areas.Admin.Models;

namespace StackOverflow.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class AnswerController : Controller
{
    private readonly ILogger<AnswerController> _logger;
    private readonly ILifetimeScope _scope;

    public AnswerController(ILogger<AnswerController> logger, ILifetimeScope scope)
    {
        _logger = logger;
        _scope = scope;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Reply()
    {
        _logger.LogInformation("You are in Admin/Anser/Reply\n");
        var model = _scope.Resolve<AnswerModel>();

        return View(model);
    }

    [HttpPost]
    public IActionResult Reply(AnswerModel model)
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
}
