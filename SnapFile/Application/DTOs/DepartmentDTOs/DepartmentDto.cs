namespace SnapFile.Application.DTOs.DepartmentDTOs
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? HeadId { get; set; }
        public string HeadName { get; set; } // если хотим показывать имя пользователя
    }
}
