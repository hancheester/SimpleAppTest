﻿using System.ComponentModel.DataAnnotations;

namespace Portal.Models;

public class EditOrganizationViewModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }
}