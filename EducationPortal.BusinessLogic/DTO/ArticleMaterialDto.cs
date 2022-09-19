namespace EducationPortal.BusinessLogic.DTO;

public class ArticleMaterialDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime PublishDate { get; set; }

    public string Source { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedById { get; set; }

    public string? CreatedByName { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedById { get; set; }

    public string? UpdatedByName { get; set; }
}