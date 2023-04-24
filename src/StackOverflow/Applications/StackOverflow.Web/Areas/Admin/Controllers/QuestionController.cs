using Autofac;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Areas.Admin.Models;

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
}
