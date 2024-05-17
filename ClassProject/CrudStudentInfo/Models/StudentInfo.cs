using System.ComponentModel.DataAnnotations;

namespace CrudStudentInfo.Models
{
    public class StudentInfo
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Department { get; set; } = string.Empty;
        [MaxLength(150)]
        public string Address { get; set; }
    }
}
