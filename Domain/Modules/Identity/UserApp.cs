using Microsoft.AspNetCore.Identity;
using Shared.Enums;
using Shared.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Modules.Identity
{
    public class UserApp : IdentityUser<Guid>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        [ForeignKey("CreatedUserId")]
        public Guid? CreatedUserId { get; set; }

        [StringLength(200)]
        public string? CreatedUserName { get; set; }

        public DateTime? CreatedOnDateTimeUTC { get; set; }

        public Guid? ModifiedUserId { get; set; }

        public string? ModifiedUserName { get; set; }
        public DateTime? ModifiedOnDateTimeUTC { get; set; }

        [Required]
        [MaxLength(25)]
        public RecordStatusEnum RecordStatus { get; set; }

        [Required]
        public ulong OrderId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        public bool IsActive { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public List<RoleApp> Roles { get; set; } = new List<RoleApp>();
        public List<UserRoleApp> UserRoles { get; set; } = new List<UserRoleApp>();
    }
}