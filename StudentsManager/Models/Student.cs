using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManager.Models
{
    [Table("student")]
    public class Student
    {
        public long Id { get; set; }
        public string StudentNumber { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        // Propiedad de navegación para las tareas
        public ICollection<Homework> Homeworks { get; set; }
    }
}