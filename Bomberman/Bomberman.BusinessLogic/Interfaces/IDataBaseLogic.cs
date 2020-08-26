// <copyright file="IDataBaseLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Bomberman.Data;
    using Bomberman.Repository;

    /// <summary>
    /// This is a DatabaseLogic's interface which calling the two crud methods from the repository
    /// </summary>
    public interface IDataBaseLogic
    {
        /// <summary>
        /// The HighScoreWindow calls this method, and this method calls the repository's select method
        /// </summary>
        /// <param name="entity"> This entity is a Rounds type item </param>
        /// <returns> It returns a Rounds Collection from the db</returns>
        IEnumerable<Rounds> Select(Rounds entity);

        /// <summary>
        /// The Gamelogic method calls this method, and this method calls the repository's insert method
        /// </summary>
        /// <param name="entity">This entity is a Rounds type item</param>
        /// <returns>Return true if the called method from the repository is successful, else it returns falses</returns>
        bool Insert(Rounds entity);
    }
}
