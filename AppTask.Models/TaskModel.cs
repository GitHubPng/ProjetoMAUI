using System.ComponentModel;

namespace AppTask.Models
{
    public class TaskModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime PrevisionDate { get; set; }
        private bool _isCompleted;
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                //Chamar a notificação
                _isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }
        public DateTime Created { get; set; }
        public DateTime Update { get; set; }
        public List<SubTaskModel> SubTasks { get; set; } = new List<SubTaskModel>();

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }

}
