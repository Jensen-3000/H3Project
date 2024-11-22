﻿namespace H3Project.Data.Models;

public class Genre
{
    public int GenreId { get; set; }
    public string Name { get; set; }
    public ICollection<Movie> Movies { get; set; }
}