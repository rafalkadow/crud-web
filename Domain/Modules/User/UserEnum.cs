using Infrastructure.Shared.Models;

namespace Domain.Modules.User
{
    /// <summary>
    /// Test user to authenticate to
    /// </summary>
    public enum UserEnum
    {
        /// <summary>
        ///     - Username: admin
        ///     - Password: admin
        /// </summary>
        Administrator = 1,// AppConstants.Users.Admin.Id,

        /// <summary>
        ///     - Username: manager
        ///     - Password: manager
        /// </summary>
        Manager = 2, // AppConstants.Users.Manager.Id,

        /// <summary>
        ///     - Username: stock
        ///     - Password: stock
        /// </summary>
        Stock = 3, //AppConstants.Users.Stock.Id,

        /// <summary>
        ///     - Username: seller
        ///     - Password: seller
        /// </summary>
        Seller = 4, // AppConstants.Users.Seller.Id,

        /// <summary>
        ///     - Username: public
        ///     - Password: public
        /// </summary>
        Public = 5,//AppConstants.Users.Public.Id
    }
}