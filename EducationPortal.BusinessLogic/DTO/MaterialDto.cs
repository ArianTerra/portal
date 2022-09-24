using EducationPortal.BusinessLogic.DTO.Abstract;

namespace EducationPortal.BusinessLogic.DTO;

public class MaterialDto : AuditedDto
{
    public string Name { get; set; }

    public string Discriminator { get; set; }

    public override string ToString()
    {
        return Name;
    }
}