// <copyright file="Tests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.BusinessLogic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Bomberman.BusinessLogic.Enum;
    using Bomberman.Data;
    using Bomberman.Model;
    using Bomberman.Repository;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Business logic test fixture.
    /// </summary>
    [TestFixture]
    public class Tests
    {
        private GameLogic gL;
        private DataBaseLogic dBl;
        private List<Rounds> database;
        private Mock<GameModel> gMMock;
        private Mock<Repository> mockedRepository;

        /// <summary>
        /// Sets up test fields.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.gMMock = new Mock<GameModel>();
            this.mockedRepository = new Mock<Repository>();
            this.dBl = new DataBaseLogic(this.mockedRepository.Object);
            this.database = new List<Rounds>();
            MapObject[,] map = this.SetupMap(5);
            Player[] players = new Player[2];
            players[0] = new Player(1, 1, "t1", "red");
            players[1] = new Player(map.GetLength(0) - 2, map.GetLength(1) - 2, "t2", "blue");
            map[players[0].PosX, players[0].PosY] = players[0];
            map[players[1].PosX, players[1].PosY] = players[1];

            this.gMMock.Setup(x => x.Map).Returns(map);
            this.gMMock.Setup(x => x.Players).Returns(players);
            this.mockedRepository.Setup(x => x.Insert(It.IsAny<Rounds>())).Callback(() =>
            {
                this.database.Add(new Rounds());
            });

            this.gL = new GameLogic(this.gMMock.Object, this.dBl);
        }

        /// <summary>
        /// Creates a simple map for easier testing
        /// </summary>
        /// <param name="size">Map side length</param>
        /// <returns>Mapobject matrix (game map)</returns>
        public MapObject[,] SetupMap(int size)
        {
            MapObject[,] result = new MapObject[size, size];

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    if (i == 0 || i == result.GetLength(0) - 1 ||
                        j == 0 || j == result.GetLength(1) - 1)
                    {
                        result[i, j] = new Wall(false);
                    }
                    else
                    {
                        result[i, j] = new Floor();
                    }
                }
            }

            result[(result.GetLength(0) - 1) / 2, (result.GetLength(1) - 1) / 2] = new Wall(true);

            return result;
        }

        /// <summary>
        /// Tests if player can step on floor.
        /// </summary>
        [Test]
        public void TestPlayerStepOnFloor()
        {
            Assert.That(this.gL.Map[2, 1].GetType() == typeof(Floor), Is.True);
            this.gL.Control(Movement.Down, this.gL.Players[0]);
            Assert.That(this.gL.Map[2, 1], Is.EqualTo(this.gL.Players[0]));
        }

        /// <summary>
        /// Tests if player can step on wall.
        /// </summary>
        [Test]
        public void IfPlayerTriesToStepOnWall_ItCant()
        {
            Assert.That(this.gL.Map[0, 1].GetType() == typeof(Wall), Is.True);
            this.gL.Control(Movement.Up, this.gL.Players[0]);
            Assert.That(this.gL.Map[1, 1], Is.EqualTo(this.gL.Players[0]));
        }

        /// <summary>
        /// Tests if player can step on barrel.
        /// </summary>
        [Test]
        public void IfPlayerTriesToStepOnDestructibleWall_ItCant()
        {
            var size = this.gL.Map.GetLength(0);
            Assert.That(this.gL.Map[(size - 1) / 2, (size - 1) / 2].GetType() == typeof(Wall));

            this.gL.Control(Movement.Down, this.gL.Players[0]);
            this.gL.Control(Movement.Right, this.gL.Players[0]);

            Assert.That(this.gL.Map[2, 1], Is.EqualTo(this.gL.Players[0]));
        }

        /// <summary>
        /// Tests if player can step on other players.
        /// </summary>
        [Test]
        public void IfPlayerTriesToStepOnPlayer_ItCan()
        {
            this.gL.Map[1, 2] = new Player(2, 1, "t3", "test");
            this.gL.Control(Movement.Right, this.gL.Players[0]);
            Assert.That(this.gL.Map[1, 2], Is.EqualTo(this.gL.Players[0]));
        }

        /// <summary>
        /// Tests if player can step on bomb.
        /// </summary>
        [Test]
        public void IfPlayerTriesToStepOnBomb_ItCant()
        {
            this.gL.Map[1, 2] = new Bomb(1);
            this.gL.Control(Movement.Right, this.gL.Players[0]);
            Assert.That(this.gL.Map[1, 1], Is.EqualTo(this.gL.Players[0]));
        }

        /// <summary>
        /// Tests RegenerateBomb method.
        /// </summary>
        [Test]
        public void IfRegenerateBombIsInvoked_PlayerGetsAnAdditionalBomb()
        {
            Assert.That(this.gL.Players[0].Bombs.Count, Is.EqualTo(1));
            this.gL.RegenerateBomb(this.gL.Players[0]);
            Assert.That(this.gL.Players[0].Bombs.Count, Is.GreaterThan(1));
        }

        /// <summary>
        /// Tests PickUpPowerUp method.
        /// </summary>
        [Test]
        public void ISPowerUpPickedUp_BombsCountplus1()
        {
            this.gL.PickUpPowerUp(this.gL.Players[0], PowerUp.PlusBomb);
            Assert.That(this.gL.Players[0].Bombs.Count, Is.EqualTo(2));
        }

        /// <summary>
        /// Tests whether PickUpPowerUp is invoked, when player steps on powerup.
        /// </summary>
        [Test]
        public void IfPlayerStepsOnPowerup_PickupPowerupIsInvoked()
        {
            this.gL.Map[1, 2] = new PlusBombPowerUp();
            this.gL.Control(Movement.Right, this.gL.Players[0]);
            Assert.That(this.gL.Players[0].Bombs.Count, Is.EqualTo(2));
        }

        /// <summary>
        /// Tests whether player gets hit, if it walks into an explosion.
        /// </summary>
        [Test]
        public void IfPlayerStepsOnExplosion_PlayerGotHitIsTrue()
        {
            this.gL.Map[1, 2] = new Explode();
            this.gL.Control(Movement.Right, this.gL.Players[0]);
            Assert.That(this.gL.Players[0].GotHit, Is.True);
        }

        /// <summary>
        /// Tests whether player gets given amount of score for destroying a barrel.
        /// </summary>
        [Test]
        public void IfPlayerGetsPoints_PlayerGetsPointForDestroyingBarrel()
        {
            this.gL.Map[3, 2] = new Wall(true);
            Bomb bomba = new Bomb(1) { Owner = this.gL.Players[1], PosX = 1, PosY = 3 };
            this.gL.Map[3, 1] = bomba;
            this.gL.DestroyBomb(bomba);
            Assert.That(this.gL.Players[1].Score, Is.EqualTo(100));
        }

        /// <summary>
        /// Tests whether player gets points from walking over a powerup.
        /// </summary>
        [Test]
        public void IfPlayerGetsPoints_PlayerGetsPointForPickingUpPowerUp()
        {
            this.gL.Map[1, 2] = new PlusBombPowerUp();
            this.gL.Control(Movement.Right, this.gL.Players[0]);
            Assert.That(this.gL.Players[0].Score, Is.EqualTo(200));
        }

        /// <summary>
        /// Tests if player gets hit, when blown up by bomb.
        /// </summary>
        [Test]
        public void IfPlayerStaysOnBomb_ItGetsHit()
        {
            Bomb bomba = new Bomb(1) { Owner = this.gL.Players[1], PosX = 2, PosY = 1 };
            this.gL.Map[1, 2] = bomba;
            this.gL.DestroyBomb(bomba);
            Assert.That(this.gL.Players[0].GotHit, Is.True);
        }

        /// <summary>
        /// Tests if high score is updated once the game finished.
        /// </summary>
        [Test]
        public void IfPlayerGetsHit_RepositoryInsertsNewItem()
        {
            Bomb bomba = new Bomb(1) { Owner = this.gL.Players[1], PosX = 2, PosY = 1 };
            this.gL.Map[1, 2] = bomba;
            this.gL.DestroyBomb(bomba);
            Assert.That(this.database.Count, Is.EqualTo(1));
        }
    }
}
