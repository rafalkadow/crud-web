﻿namespace Domain.Modules.Role
{
    /// <summary>
    /// Data about a role in the company
    /// </summary>
    public class RoleReadDto
    {
        /// <summary>
        /// Role Id
        /// </summary>
        /// <example>4</example>
        public Guid Id { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        /// <example>Seller</example>
        public string Name { get; set; }
    }
}