using SnapFile.Application.DTOs.UserDTOs;

namespace SnapFile.Application.DTOs.PositionDTOs
{
    public class PositionWithUsersDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ShortUserDto> Users { get; set; }
    }
}
