﻿using EducationPortal.DataAccess.DomainModels.JoinEntities;

namespace EducationPortal.DataAccess.DomainModels.Materials;

public class BookMaterial : Material
{
    public ICollection<BookAuthorBookMaterial> BookAuthorBookMaterial { get; set; }

    public int Pages { get; set; }

    public int Year { get; set; } //TODO change it to DateTime

    public string Format { get; set; } //TODO change to enum or another table like BookAuthor
}