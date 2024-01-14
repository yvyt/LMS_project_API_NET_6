namespace ExamService.Model
{
    public class ExamDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Class { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string NumberQuestion {  get; set; }
        public string IsMutipleChoice { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public string CreateAt { get; set; }
        public string UpdateAt { get; set; }
    }
}
