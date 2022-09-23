using EducationPortal.BusinessLogic.DTO.Abstract;

namespace EducationPortal.BusinessLogic.DTO;

public class UserRegisterDto : BaseDto
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}