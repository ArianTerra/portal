﻿using EducationPortal.BusinessLogic.DTO.Abstract;

namespace EducationPortal.BusinessLogic.DTO;

public class BookMaterialDto : AuditedDto
{
    public string Name { get; set; }

    public int Pages { get; set; }

    public int Year { get; set; }

    public BookFormatDto BookFormat { get; set; }

    public IEnumerable<BookAuthorDto> Authors { get; set; }
}