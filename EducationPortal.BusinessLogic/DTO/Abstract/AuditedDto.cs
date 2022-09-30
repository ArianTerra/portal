namespace EducationPortal.BusinessLogic.DTO.Abstract;

public abstract class AuditedDto : BaseDto
{
    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedById { get; set; }

    public string? CreatedByUserName { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedById { get; set; }

    public string? UpdatedByUserName { get; set; }
}