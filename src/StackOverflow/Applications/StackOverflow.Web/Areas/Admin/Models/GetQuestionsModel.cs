using Autofac;
using StackOverflow.Services.Services;
using StackOverflow.Web.Models;

namespace StackOverflow.Web.Areas.Admin.Models
{
    public class GetQuestionsModel : AdminBaseModel
    {
        public string Title { get; set; }
        public int VoteCount { get; set; }
        public DateTime TimeStamp { get; set; }

        private IQuestionService? _questionService;

        public GetQuestionsModel(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<object> GetQuestionsAsync(DataTablesAjaxRequestModel model)
        {
            var data = await _questionService.GetQuestionsAsync(model.PageIndex,
                model.PageSize,
                model.SearchText,
                model.GetSortText(new string[] {"Title", "VoteCount", "TimeStamp" }));

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
