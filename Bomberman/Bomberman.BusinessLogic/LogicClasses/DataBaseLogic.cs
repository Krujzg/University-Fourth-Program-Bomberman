// <copyright file="DataBaseLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.BusinessLogic
{
    using System.Collections.Generic;
    using Bomberman.Data;
    using Bomberman.Repository;

    /// <summary>
    /// This is a DatabaseLogic which calling the two crud methods from the repository
    /// </summary>
    public class DataBaseLogic : IDataBaseLogic
    {
        private readonly IRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseLogic"/> class.
        /// </summary>
        /// <param name="repository"> It gets an IRepository type ,which will transfer the functions to the Repository</param>
        public DataBaseLogic(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// The Gamelogic method calls this method, and this method calls the repository's insert method
        /// </summary>
        /// <param name="entity">This entity is a Rounds type item</param>
        /// <returns>Return true if the called method from the repository is successful, else it returns falses</returns>
        public bool Insert(Rounds entity)
        {
            return this.repository.Insert(entity);
        }

        /// <summary>
        /// The HighScoreWindow calls this method, and this method calls the repository's select method
        /// </summary>
        /// <param name="entity"> This entity is a Rounds type item </param>
        /// <returns> It returns a Rounds Collection from the db</returns>
        public IEnumerable<Rounds> Select(Rounds entity)
        {
            return this.repository.Select(entity);
        }
    }
}
