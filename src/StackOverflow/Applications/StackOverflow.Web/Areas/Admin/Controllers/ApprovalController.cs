using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Services.Exceptions;
using StackOverflow.Services.Services.Membership;
using StackOverflow.Web.Areas.Admin.Models;
using StackOverflow.Web.Extensions;
using StackOverflow.Web.Models;

namespace StackOverflow.Web.Areas.Admin.Controllers;

[Area("Admin"), Authorize]
public class ApprovalController : Controller
{
    private readonly ILogger<ApprovalController> _logger;
    private readonly ILifetimeScope _scope;
    private readonly IAccountService _accountService;

    public ApprovalController(ILogger<ApprovalController> logger, ILifetimeScope scope, IAccountService accountService)
    {
        _logger = logger;
        _scope = scope;
        _accountService = accountService;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet, AllowAnonymous]
    public async Task<JsonResult> GetQuestions()
    {
        var userId = Guid.Parse(_accountService.GetUserId());

        var dataTableModel = new DataTablesAjaxRequestModel(Request);
        var model = _scope.Resolve<GetQuestionsModel>();

        return Json(await model.GetQuestionsByUserIdAsync(userId, dataTableModel));
    }

    public IActionResult Answer()
    {
        return View();
    }
    public async Task<JsonResult> GetAnswers()
    {
        var id = HttpContext.Request.Headers.Referer.ToString().Split('/').Last();
        var qid = new Guid(id);

        var dataTableModel = new DataTablesAjaxRequestModel(Request);
        var model = _scope.Resolve<GetAnswersModel>();

        return Json(await model.GetPendingAnswersAsync(qid, dataTableModel));
    }
    public async Task<IActionResult> Approve(Guid id)
    {
        try
        {
            var model = _scope.Resolve<AnswerModel>();
            await model.ApproveAsync(id);

            //TempData.Put<ResponseModel>("ResponseMessage", new ResponseModel
            //{
            //    Message = "Successfully deleted question.",
            //    Type = ResponseTypes.Success
            //});

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
                Message = "Failed to approve answer",
                Type = ResponseTypes.Danger
            });
        }

        return RedirectToAction("Index");
    }

}
