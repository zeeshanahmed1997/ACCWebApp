﻿using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Users
{
    public int UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Usercategory { get; set; }
}