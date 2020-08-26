// <copyright file="IRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Bomberman.Data;

    /// <summary>
    /// Interface for the Database repository.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Returns all items from an entity.
        /// </summary>
        /// <param name="entity">Type of entity</param>
        /// <returns>Every item in entity</returns>
        IEnumerable<Rounds> Select(Rounds entity);

        /// <summary>
        /// Insert a new element into an entity.
        /// </summary>
        /// <param name="entity">Type of entity</param>
        /// <returns>True if operation was successful, false otherwise.</returns>
        bool Insert(Rounds entity);
    }
}
