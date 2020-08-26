// <copyright file="GameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.BusinessLogic
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using Bomberman.BusinessLogic.Enum;
    using Bomberman.BusinessLogic.LogicClasses;
    using Bomberman.Data;
    using Bomberman.Model;
    using Bomberman.Repository;

    /// <summary>
    /// This is a gamelogic class which doing every work of the players
    /// </summary>
    public class GameLogic
    {
        /// <summary>
        /// Sets to true if game is finished
        /// </summary>
        private bool gameFinished = false;

        /// <summary>
        /// Players and map got from the gamemodel
        /// </summary>
        public GameModel gM;

        /// <summary>
        /// SoundSystem field
        /// </summary>
        public SoundSystem SoundSystem;

        private static Random random;

        /// <summary>
        /// insert got from DataBaseLogic
        /// </summary>
        private readonly DataBaseLogic roundrepository;

        /// <summary>
        /// From the begining of the game this stopwatch starts, and at the end of the game, stops and it will be on the screen
        /// </summary>
        private readonly Stopwatch gametime;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        public GameLogic()
        {
            this.SoundSystem = new SoundSystem();
            this.gametime = new Stopwatch();
            this.gM = new GameModel();
            random = new Random();
            this.roundrepository = new DataBaseLogic(new Repository());
            this.gametime.Start();
            this.SoundSystem.PlayTADASound();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// This constructor is for testing.
        /// </summary>
        /// <param name="gM">Gamemodel mock</param>
        /// <param name="repo">Repository mock</param>
        public GameLogic(GameModel gM, DataBaseLogic repo)
        {
            this.SoundSystem = new SoundSystem();
            this.gametime = new Stopwatch();
            this.gM = gM;
            random = new Random();
            this.roundrepository = repo;
            this.gametime.Start();
        }

        /// <summary>
        /// This event helps refreshes the screen after every statechange
        /// </summary>
        public event EventHandler GameStateChanged;

        /// <summary>
        /// This event helps ending the game
        /// </summary>
        public event EventHandler GameFinished;

        /// <summary>
        /// Gets returns map from GameModel
        /// </summary>
        public MapObject[,] Map
        {
            get { return this.gM.Map; }
        }

        /// <summary>
        /// Gets all the players from GameModel
        /// </summary>
        public Player[] Players
        {
            get { return this.gM.Players; }
        }

        /// <summary>
        /// This method builds up the gamingscreen ( map, players) from the default txt-s
        /// </summary>
        /// <param name="filePath">This is a filepath of the default map(s)</param>
        /// <param name="p1_name">This will be a player1 s name</param>
        /// <param name="p2_name">This will be a player2 s name</param>
        public void SetupGameLogic(string filePath, string p1_name, string p2_name)
        {
            this.gM.GetMap(filePath);
            this.Players[0].Name = p1_name;
            this.Players[1].Name = p2_name;
        }

        /// <summary>
        /// This method calls the gamemodel s LoadGame method
        /// </summary>
        /// <param name="filePath">This is a filepath of the saved maps</param>
        public void LoadGameLogic(string filePath)
        {
            this.gM.LoadGame(filePath);
        }

        /// <summary>
        /// This method calls the gamemodel s SaveGame method
        /// </summary>
        public void SaveGame()
        {
            this.gM.SaveGame();
        }

        /// <summary>
        /// This method decides the action of the player who does them
        /// </summary>
        /// <param name="movement">This is the action that the player does</param>
        /// <param name="p"> This is a player who does the action</param>
        public void Control(Movement movement, Player p)
        {
            switch (movement)
            {
                case Movement.Up:
                    this.StepWithCheck(p, 0, -1);
                    break;
                case Movement.Down:
                    this.StepWithCheck(p, 0, 1);
                    break;
                case Movement.Left:
                    this.StepWithCheck(p, -1, 0);
                    break;
                case Movement.Right:
                    this.StepWithCheck(p, 1, 0);
                    break;
                default:
                    this.DropBomb(p);
                    break;
            }
        }

        /// <summary>
        /// This method regenerates a bomb after the dropped bomb exploded
        /// </summary>
        /// <param name="player">This is a player who gets a bomb</param>
        public void RegenerateBomb(Player player)
        {
            player.Bombs.Add(new Bomb(player.BombRange));
        }

        /// <summary>
        /// This method decides what kind of powerup was picked up by the player
        /// </summary>
        /// <param name="player">This is a player who picks up the powerup</param>
        /// <param name="powerup">This is a powerup which was picked up</param>
        public void PickUpPowerUp(Player player, PowerUp powerup)
        {
            player.PowerUps.Add(powerup);
            switch (powerup)
            {
                case PowerUp.PlusBomb:
                    this.GetPoints(player, PointTypes.PickUpPowerUps);
                    this.RegenerateBomb(player);
                    break;
            }
        }

        /// <summary>
        /// This method is making the bombs explode
        /// </summary>
        /// <param name="bomba">This is the bomb which will explode</param>
        public void DestroyBomb(Bomb bomba)
        {
            Thread.Sleep(3000);
            this.ExplodeBombs(bomba);
            this.GameStateChanged?.Invoke(this, null);
            Thread.Sleep(1000);
            this.ClearExplosions(bomba);
        }

        /// <summary>
        /// With this method, the player can drop a bomb under itself
        /// </summary>
        /// <param name="player">This is a player who drops a bomb down</param>
        public void DropBomb(Player player)
        {
            if (player.Bombs.Count > 0)
            {
                this.gM.Map[player.PosY, player.PosX] = player.Bombs[0];

                Bomb bomba = new Bomb(player.BombRange, player.PosX, player.PosY, player);
                this.GameStateChanged?.Invoke(this, null);
                player.Bombs.RemoveAt(0);
                Task explode = Task.Run(
                    () =>
                {
                    this.DestroyBomb(bomba);
                    this.RegenerateBomb(player);
                    this.GameStateChanged?.Invoke(this, null);
                });
            }
        }

        /// <summary>
        /// After Explosion this method will put powerup or floor on the destroyed barrels place
        /// </summary>
        /// <param name="y">This is an y coordinate of the destroyed barrel</param>
        /// <param name="x">This is an x coordinate of the destroyed barrel</param>
        private void ShowPowerUp(int y, int x)
        {
            this.gM.Map[y, x] = this.PowerUpSelector();
        }

        /// <summary>
        /// This method will decide whether floor or powerup will be on the destroyed barrels place
        /// </summary>
        /// <returns>Return a plusbombpowerup or floor</returns>
        private MapObject PowerUpSelector()
        {
            if (random.Next(0, 2) == 0)
            {
                return new PlusBombPowerUp();
            }
            else
            {
                return new Floor();
            }
        }

        /// <summary>
        /// This method will decide how much points will the player get
        /// </summary>
        /// <param name="player">The player who gets the point</param>
        /// <param name="point_types"> The type of point</param>
        private void GetPoints(Player player, PointTypes point_types)
        {
            switch (point_types)
            {
                case PointTypes.DestroyWall:
                    player.Score += 100;
                    break;
                case PointTypes.PickUpPowerUps:
                    player.Score += 200;
                    break;
                default:
                case PointTypes.KillOtherPlayer:
                    player.Score += 2000;
                    break;
            }
        }

        /// <summary>
        /// This method makes the bomb explode
        /// </summary>
        /// <param name="bomba">This is the bomb which will explode</param>
        private void ExplodeBombs(Bomb bomba)
        {
            this.SoundSystem.PlayExplodeBombSound();
            for (int i = -bomba.Range; i < bomba.Range + 1; i++)
            {
                this.CheckCoordinatesBeforeExplosion(bomba.PosY, bomba.PosX + i, bomba);
                this.CheckCoordinatesBeforeExplosion(bomba.PosY + i, bomba.PosX, bomba);
            }

            if (this.Players[0].GotHit || this.Players[1].GotHit)
            {
                this.gametime.Stop();

                if (this.Players[1].GotHit)
                {
                    this.OnGameFinished(this.Players[0], this.Players[1], this.gametime.Elapsed.Seconds);
                }
                else
                {
                    this.OnGameFinished(this.Players[1], this.Players[0], this.gametime.Elapsed.Seconds);
                }
            }
        }

        /// <summary>
        /// This method can expend the players bomb range
        /// </summary>
        /// <param name="player">The player who gets the expended bombrange</param>
        private void ExpendPlayersBombsRange(Player player)
        {
            foreach (var item in player.Bombs)
            {
                item.Range += 1;
            }

            player.BombRange += 1;
        }

        /// <summary>
        /// This method will check the players step before he/she makes it
        /// </summary>
        /// <param name="player"> The player whose step will be checked</param>
        /// <param name="dx">Step right or left</param>
        /// <param name="dy"> Step Up or down</param>
        private void StepWithCheck(Player player, int dx, int dy)
        {
            int new_x = player.PosX + dx;
            int new_y = player.PosY + dy;

            if (this.gM.Map[new_y, new_x].GetType() != typeof(Wall) && this.gM.Map[new_y, new_x].GetType() != typeof(Bomb))
            {
                if (this.gM.Map[new_y, new_x].GetType() == typeof(Explode))
                {
                    this.GameStateChanged?.Invoke(this, null);
                    player.GotHit = true;
                    if (player == this.Players[0])
                    {
                        this.OnGameFinished(this.Players[1], player, this.gametime.Elapsed.Seconds);
                    }
                    else
                    {
                        this.OnGameFinished(this.Players[0], player, this.gametime.Elapsed.Seconds);
                    }
                }

                if (this.gM.Map[player.PosY, player.PosX].GetType() != typeof(Bomb))
                {
                    this.gM.Map[player.PosY, player.PosX] = new Floor();
                }

                if (this.gM.Map[new_y, new_x].GetType() == typeof(PlusBombPowerUp))
                {
                    this.PickUpPowerUp(player, PowerUp.PlusBomb);
                }

                player.PosX = new_x;
                player.PosY = new_y;

                this.gM.Map[new_y, new_x] = player;

                this.GameStateChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// This method will check the maps coordinates before the explosion
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y"> y coordinate</param>
        /// <param name="bomba">the bomb which will explode</param>
        private void CheckCoordinatesBeforeExplosion(int x, int y, Bomb bomba)
        {
            if (this.Players[0].PosX == y && this.Players[0].PosY == x)
            {
                this.Players[0].GotHit = true;
            }
            else if (this.Players[1].PosX == y && this.Players[1].PosY == x)
            {
                this.Players[1].GotHit = true;
            }
            else if (this.Map[x, y].GetType() != typeof(Wall))
            {
                this.Map[x, y] = new Explode();
            }
            else
            {
                if ((this.Map[x, y] as Wall).Destroyable)
                {
                    this.ShowPowerUp(x, y);
                    this.GetPoints(bomba.Owner, PointTypes.DestroyWall);
                }
            }
        }

        /// <summary>
        /// This method will replace all the explosion with floor
        /// </summary>
        /// <param name="bomba"> the bombs range and coordinates are necessary for the loop</param>
        private void ClearExplosions(Bomb bomba)
        {
            for (int i = -bomba.Range; i < bomba.Range + 1; i++)
            {
                if (this.Map[bomba.PosY, bomba.PosX + i].GetType() == typeof(Explode))
                {
                    this.Map[bomba.PosY, bomba.PosX + i] = new Floor();
                }

                if (this.Map[bomba.PosY + i, bomba.PosX].GetType() == typeof(Explode))
                {
                    this.Map[bomba.PosY + i, bomba.PosX] = new Floor();
                }
            }
        }

        /// <summary>
        /// This method is doing the after death logic
        /// </summary>
        /// <param name="p1">player 1</param>
        /// <param name="p2"> player 2</param>
        /// <param name="gametime"> current gametime </param>
        private void OnGameFinished(Player p1, Player p2, int gametime)
        {
            if (!this.gameFinished)
            {
                this.SoundSystem.PlayEndGameSound();
                this.gameFinished = true;
                this.GetPoints(p1, PointTypes.KillOtherPlayer);
                this.roundrepository.Insert(new Rounds()
                {
                    Loser = p2.Name,
                    Loser_Score = p2.Score,
                    Winner = p1.Name,
                    Winner_Score = p1.Score
                });

                this.GameFinished?.Invoke(this, new GameFinishedEventArgs(p1, p2, gametime));
            }
        }
    }
}
