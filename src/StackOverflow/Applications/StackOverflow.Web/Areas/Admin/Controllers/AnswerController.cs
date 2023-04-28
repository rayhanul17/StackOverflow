using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Services.Exceptions;
using StackOverflow.Web.Areas.Admin.Models;
using StackOverflow.Web.Extensions;
using StackOverflow.Web.Models;

namespace StackOverflow.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize]
public class AnswerController : Controller
{
    private readonly ILogger<AnswerController> _logger;
    private readonly ILifetimeScope _scope;

    public AnswerController(ILogger<AnswerController> logger, ILifetimeScope scope)
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
    public async Task<JsonResult> GetAnswers()
    {
        var dataTableModel = new DataTablesAjaxRequestModel(Request);
        var model = _scope.Resolve<GetAnswersModel>();

        var id = HttpContext.Request.Headers.Referer.ToString().Split('/').Last();
        var qid = new Guid(id);

        return Json(await model.GetAnswersAsyncByQuestion(qid, dataTableModel));
    }

    public IActionResult Reply()
    {
        var qid = Request.Path.ToString().Split('/').Last();

        _logger.LogInformation("You are in Admin/Reply\n");
        var model = _scope.Resolve<AnswerModel>();
        model.QuestionId = Guid.Parse(qid);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Reply(AnswerModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                model.ResolveDependency(_scope);
                await model.Add();

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Successfully replied.",
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

        return RedirectToAction("Index", "Question", new{Area = "Admin"});
    }

    public IActionResult Edit(Guid id)
    {
        var model = _scope.Resolve<AnswerEditModel>();

        try
        {
            model.GetAnswer(id);
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
    public async Task<IActionResult> Edit(AnswerEditModel model)
    {
        if (ModelState.IsValid)
        {
            model.ResolveDependency(_scope);

            try
            {
                await model.UpdateAnswerAsync();

                TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
                {
                    Message = "Successfully updated answer.",
                    Type = ResponseTypes.Success
                });

                return RedirectToAction("Index", "Question", new {Area = "Admin"});
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
                    Message = "There was a problem in updating answer.",
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
            var model = _scope.Resolve<AnswerModel>();
            await model.DeleteAsync(id);

            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Successfully deleted answer.",
                Type = ResponseTypes.Success
            });

            return RedirectToAction("Index", "Question", new { Area = "Admin" });
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
                Message = "Failed to delete answer",
                Type = ResponseTypes.Danger
            });
        }

        return RedirectToAction("Index", "Question", new {Area = "Admin"});
    }

    public IActionResult Details()
    {
        return View();
    }

    public async Task<IActionResult> VoteUp(Guid id)
    {

        var qid = new Guid(HttpContext.Request.Headers.Referer.ToString().Split('/').Last());

        try
        {
            var model = _scope.Resolve<AnswerModel>();
            await model.VoteUpAsync(id);

            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Successfully vote up this question.",
                Type = ResponseTypes.Success
            });
            RouteValueDictionary rv = new RouteValueDictionary()
            {
                {
                    "id", qid
                }
            };
            return RedirectToAction("Index", "Question", new { Area = "Admin" });
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
                Message = "Failed to vote this question",
                Type = ResponseTypes.Danger
            });
        }

        return RedirectToAction("Index", "Question", new { Area = "Admin"});
    }

    public async Task<IActionResult> VoteDown(Guid id)
    {
        try
        {
            var model = _scope.Resolve<AnswerModel>();
            await model.VoteDownAsync(id);

            TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            {
                Message = "Successfully vote down this question.",
                Type = ResponseTypes.Success
            });

            return RedirectToAction("Index", "Question", new {Area = "Admin"});
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
                Message = "Failed to vote this question",
                Type = ResponseTypes.Danger
            });
        }

        return RedirectToAction("Index");
    }

}
