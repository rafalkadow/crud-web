﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Modules.Base.Models
{
    public class Audit
    {
        [Key]
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? Type { get; set; }
        public string? TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string? AffectedColumns { get; set; }
        public string? PrimaryKey { get; set; }
    }
}
