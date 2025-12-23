namespace AppTask.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime PrevisionDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime Update { get; set; }
        public List<SubTaskModel> SubTasks { get; set; } = new List<SubTaskModel>();
    }

}
