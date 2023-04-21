using Autofac;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Web.Areas.Admin.Models;

namespace StackOverflow.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class QAController : Controller
{
    private readonly ILogger<QAController> _logger;
    private readonly ILifetimeScope _scope;

    public QAController(ILogger<QAController> logger, ILifetimeScope scope)
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
            _logger.LogError(ex.Message);
        }

        return View(model);
    }
}
