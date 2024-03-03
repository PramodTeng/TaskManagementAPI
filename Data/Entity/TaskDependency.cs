namespace Task_Management_API.Data.Entity
{
    public class TaskDependency
    {
        public int DependencyId { get; set; }
        public int TaskItemId { get; set; }
        public int DependentTaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }
        public TaskItem? DependentTaskItem { get; set; }
    }
}
