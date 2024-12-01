﻿namespace H3Project.Data.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string UserType { get; set; }

    public ICollection<Ticket> Tickets { get; set; }
}
