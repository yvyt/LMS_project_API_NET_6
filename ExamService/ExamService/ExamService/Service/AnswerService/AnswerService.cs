using ExamService.Data;

namespace ExamService.Service.AnswerService
{
    public class AnswerService:IAnswerService
    {
        private ExamsContext _context { get; set; }
        public AnswerService(ExamsContext context)
        {
            _context = context;
        }
    }
}
