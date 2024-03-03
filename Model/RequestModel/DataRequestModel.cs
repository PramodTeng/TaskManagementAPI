namespace Task_Management_API.Model.RequestModel
{
    public class DataRequestModel
    {
        public int pageSize { get; set; } = 10;
        public int pageNumber { get; set; } = 1;
        public string status { get; set; } = null;
        public DateTime? dueDate { get; set; } = null;
        public DateTime? startDate { get; set; } = null;
        public string sortBy { get; set; } = "title";
        public bool isAscending { get; set; } = true;
    }
}
