using Infrastructure.Shared.Models;

namespace Domain.Modules.Role
{
    /// <summary>
    /// Role Enum
    /// </summary>
    public enum RolesEnum
    {
        /// <summary>
        /// Admin
        /// </summary>
        Administrator = 1, //AppConstants.Roles.Admin.Id,
        /// <summary>
        /// manager
        /// </summary>
        Manager = 2, //AppConstants.Roles.Manager.Id,
        /// <summary>
        /// stock
        /// </summary>
        Stock = 3, //AppConstants.Roles.Stock.Id,
        /// <summary>
        /// seller
        /// </summary>
        Seller = 4, //AppConstants.Roles.Seller.Id,
    }
}