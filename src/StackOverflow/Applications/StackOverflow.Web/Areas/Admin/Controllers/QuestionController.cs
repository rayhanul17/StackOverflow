using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Services.Exceptions;
using StackOverflow.Web.Areas.Admin.Models;
using StackOverflow.Web.Extensions;
using StackOverflow.Web.Models;

namespace StackOverflow.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize]
public class QuestionController : Controller
{
    private readonly ILogger<QuestionController> _logger;
    private readonly ILifetimeScope _scope;

    public QuestionController(ILogger<QuestionController> logger, ILifetimeScope scope)
    {
        _logger = logger;
        _scope = scope;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet, AllowAnonymous]
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
    public async Task<IActionResult> Ask(QuestionModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                model.ResolveDependency(_scope);
                await model.Ask();

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Successfully added a new questions.",
                    Type = ResponseTypes.Success
                });
            }
            catch (CustomException ioe)
            {
                _logger.LogError(ioe, ioe.Message);
                ModelState.AddModelError("", ioe.Message);
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = ioe.Message,
                    Type = ResponseTypes.Warning
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "There was a problem in creating course.",
                    Type = ResponseTypes.Danger
                });
            }
        }
        else
        {
            string messageText = string.Empty;
            foreach (var message in ModelState.Values)
            {
                for (int i = 0; i < message.Errors.Count; i++)
                {
                    messageText += message.Errors[i].ErrorMessage;
                }
            }
            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = messageText,
                Type = ResponseTypes.Danger
            });
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var model = _scope.Resolve<QuestionEditModel>();

        try
        {
            await model.GetQuestion(id);
        }
        catch(CustomException  ex)
        {
            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = ex.Message,
                Type = ResponseTypes.Warning
            });
        }

        return View(model);
    }

    [ValidateAntiForgeryToken, HttpPost]
    public async Task<IActionResult> Edit(QuestionEditModel model)
    {
        if (ModelState.IsValid)
        {
            model.ResolveDependency(_scope);

            try
            {
                await model.UpdateQuestionAsync();

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Successfully updated question.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction("Index");
            }
            catch (CustomException ioe)
            {
                _logger.LogError(ioe, ioe.Message);
                ModelState.AddModelError("", ioe.Message);
                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = ioe.Message,
                    Type = ResponseTypes.Warning
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "There was a problem in updating question.",
                    Type = ResponseTypes.Danger
                });
            }
        }

        return View(model);
    }
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var model = _scope.Resolve<QuestionModel>();
            await model.DeleteAsync(id);

            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Successfully deleted question.",
                Type = ResponseTypes.Success
            });

            return RedirectToAction("Index");
        }

        catch (CustomException ioe)
        {
            _logger.LogError(ioe, ioe.Message);
            ModelState.AddModelError("", ioe.Message);
            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = ioe.Message,
                Type = ResponseTypes.Warning
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Failed to Delete Question",
                Type = ResponseTypes.Danger
            });
        }

        return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid id)
    {
        //var s = Request.Path;        
        
        var model = _scope.Resolve<QuestionEditModel>();

        try
        {
            await model.GetQuestion(id);
        }
        catch (CustomException ex)
        {
            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = ex.Message,
                Type = ResponseTypes.Warning
            });
        }

        return View(model);
    }

    public async Task<IActionResult> RefDetails(Guid id)
    {
        var qid = new Guid(HttpContext.Request.Headers.Referer.ToString().Split('/').Last());

        var model = _scope.Resolve<QuestionEditModel>();

        try
        {
            await model.GetQuestion(qid);
        }
        catch (CustomException ex)
        {
            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = ex.Message,
                Type = ResponseTypes.Warning
            });
        }

        return View(model);
    }

    public async Task<IActionResult> VoteUp(Guid id)
    {
        try
        {
            var model = _scope.Resolve<QuestionModel>();
            await model.VoteUpAsync(id);

            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Successfully vote up to question.",
                Type = ResponseTypes.Success
            });

            return RedirectToAction("Index");
        }

        catch (CustomException ioe)
        {
            _logger.LogError(ioe, ioe.Message);
            ModelState.AddModelError("", ioe.Message);
            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = ioe.Message,
                Type = ResponseTypes.Warning
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Failed to vote to question",
                Type = ResponseTypes.Danger
            });
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> VoteDown(Guid id)
    {
        try
        {
            var model = _scope.Resolve<QuestionModel>();
            await model.VoteDownAsync(id);

            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Successfully vote down to question.",
                Type = ResponseTypes.Success
            });

            return RedirectToAction("Index");
        }

        catch (CustomException ioe)
        {
            _logger.LogError(ioe, ioe.Message);
            ModelState.AddModelError("", ioe.Message);
            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = ioe.Message,
                Type = ResponseTypes.Warning
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Failed to vote to question",
                Type = ResponseTypes.Danger
            });
        }

        return RedirectToAction("Index");
    }
}
