namespace TaskManagementSystem.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } // Статусы: "Новая", "В работе", "Выполнена"

        public TaskItem()
        {
            Status = "Новая"; // Статус по умолчанию
        }
    }
}