using Pets.DomainModel;

namespace Pets.DAL
{
    /// <summary>
    /// Provide access to Pet Owner data.
    /// </summary>
    public interface IOwnerRepository
    {
        /// <summary>
        /// Provide access to all owners.
        /// </summary>
        /// <returns>A queryable list of owners.</returns>
        Owners GetAllOwners();
    }
}
