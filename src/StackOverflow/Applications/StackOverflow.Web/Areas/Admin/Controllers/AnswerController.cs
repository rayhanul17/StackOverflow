using Autofac;
using Microsoft.AspNetCore.Authorization;
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

        return Json(await model.GetAnswersAsync(dataTableModel));
    }

    public IActionResult Reply()
    {
        _logger.LogInformation("You are in Admin/Reply\n");
        var model = _scope.Resolve<AnswerModel>();

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

        return RedirectToAction("Index");
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
                Message = "Failed to delete answer",
                Type = ResponseTypes.Danger
            });
        }

        return RedirectToAction("Index");
    }

    public IActionResult Details()
    {
        return View();
    }

}
