﻿using Domain.Modules.Role;

namespace Domain.Modules.User
{
    /// <summary>
    /// User data with more details
    /// </summary>
    public class UserDetailedReadDto
    {
        /// <summary>
        /// User Id
        /// </summary>
        /// <example>00000000-0000-0000-0000-000000000001</example>
        public Guid Id { get; set; }

        /// <summary>
        /// User username
        /// </summary>
        /// <example>johndoe123</example>
        public string UserName { get; set; }

        /// <summary>
        /// User first name
        /// </summary>
        /// <example>John</example>
        public string FirstName { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        /// <example>Doe</example>
        public string LastName { get; set; }

        /// <summary>
        /// User age
        /// </summary>
        /// <example>30</example>
        public int Age { get; set; }

        /// <summary>
        /// User date of birth
        /// </summary>
        /// <example>22-02-1992</example>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// User assigned roles
        /// </summary>
        public List<RoleReadDto> Roles { get; set; }
    }
}