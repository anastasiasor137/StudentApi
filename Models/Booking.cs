using System;

namespace TaskManagementSystem.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string ResourceType { get; set; } // Тип ресурса (например, "Отель", "Авиабилет", "Ресторан")
        public string ResourceName { get; set; } // Имя или описание ресурса
        public DateTime StartDateTime { get; set; } // Дата и время начала бронирования
        public DateTime EndDateTime { get; set; } // Дата и время окончания бронирования
        public bool IsActive { get; set; } = true; // Активно ли бронирование
    }
}