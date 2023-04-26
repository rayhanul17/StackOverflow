using Autofac;
using StackOverflow.Services.Services;
using StackOverflow.Web.Models;

namespace StackOverflow.Web.Areas.Admin.Models
{
    public class GetQuestionsModel : AdminBaseModel
    {
        private IQuestionService? _questionService;

        public GetQuestionsModel(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<object?> GetQuestionsAsync(DataTablesAjaxRequestModel model)
        {
            var data = await _questionService?.GetQuestions(
                model.PageIndex,
                model.PageSize,
                model.SearchText,
                model.GetSortText(new string[] {"Title", "VoteCount", "TimeStamp"}));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Title,
                            record.VoteCount.ToString(),
                            record.TimeStamp.ToString(),
                            record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
