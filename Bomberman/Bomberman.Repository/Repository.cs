// <copyright file="Repository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.SqlClient;
    using System.Linq;
    using Bomberman.Data;

    /// <summary>
    /// Database repository
    /// </summary>
    public class Repository : IRepository
    {
        private readonly BomberManDbEntities dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        public Repository()
        {
            this.dbContext = new BomberManDbEntities();
            System.Diagnostics.Debugger.NotifyOfCrossThreadDependency();
        }

        /// <summary>
        /// Insert a new element into an entity.
        /// </summary>
        /// <param name="entity">Type of entity</param>
        /// <returns>True if operation was successful, false otherwise.</returns>
        public virtual bool Insert(Rounds entity)
        {
            this.dbContext.Rounds.Add(entity);
            this.dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// Returns all items from an entity.
        /// </summary>
        /// <param name="entity">Type of entity</param>
        /// <returns>Every item in entity</returns>
        public IEnumerable<Rounds> Select(Rounds entity)
        {
            IEnumerable<Rounds> querry = from i in this.dbContext.Rounds
                                         orderby i.Winner_Score descending
                                         select i;
            return querry;
        }
    }
}
