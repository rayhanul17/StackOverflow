using Autofac;
using StackOverflow.Services.Services;
using StackOverflow.Web.Models;

namespace StackOverflow.Web.Areas.Admin.Models
{
    public class GetAnswersModel : AdminBaseModel
    {
        private IAnswerService? _answerService;

        public GetAnswersModel(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        public async Task<object?> GetAnswersAsync(DataTablesAjaxRequestModel model)
        {
            var data = await _answerService?.GetAnswers(
                model.PageIndex,
                model.PageSize,
                model.SearchText,
                model.GetSortText(new string[] {"Description", "VoteCount", "TimeStamp"}));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Description,
                            record.VoteCount.ToString(),
                            record.TimeStamp.ToString(),
                            record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        public async Task<object?> GetAnswersAsyncByQuestion(Guid qid, DataTablesAjaxRequestModel model)
        {
            var data = await _answerService?.GetAnswersByQuestion(
                qid,
                model.PageIndex,
                model.PageSize,
                model.SearchText,
                model.GetSortText(new string[] { "Description", "VoteCount", "TimeStamp" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Description,
                            record.VoteCount.ToString(),
                            record.TimeStamp.ToString(),
                            record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
