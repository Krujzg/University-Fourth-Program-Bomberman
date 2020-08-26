// <copyright file="GameDisplay.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.UI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Bomberman.BusinessLogic;
    using Bomberman.Model;

    /// <summary>
    /// This is the gameDisplay class that can draw pictures on the screen
    /// </summary>
    public class GameDisplay : FrameworkElement
    {
        /// <summary>
        /// GamelogicPorperty
        /// </summary>
        public static readonly DependencyProperty GameLogicProperty =
            DependencyProperty.Register("Logic", typeof(GameLogic), typeof(GameDisplay), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets gamelogic
        /// </summary>
        public GameLogic GL
        {
            get
            {
                return (GameLogic)this.GetValue(GameLogicProperty);
            }

            set
            {
                this.SetValue(GameLogicProperty, value);
                this.GL.GameStateChanged += this.GL_GameStateChanged;
            }
        }

        /// <summary>
        /// Gets the image from file
        /// </summary>
        /// <param name="filePath">file path of the picture</param>
        /// <returns>An ImageBrush</returns>
        public ImageBrush GetBrush(string filePath)
        {
            return new ImageBrush(
                new BitmapImage(new Uri(filePath, UriKind.RelativeOrAbsolute)));
        }

        /// <summary>
        /// Decides which picture has to be drawn
        /// </summary>
        /// <param name="drawingContext">drawingcontext</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.GL.Map != null)
            {
                double tile_width = this.ActualWidth / this.GL.Map.GetLength(1);
                double tile_height = this.ActualHeight / this.GL.Map.GetLength(0);
                string currentDir = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();

                for (int i = 0; i < this.GL.Map.GetLength(0); i++)
                {
                    for (int j = 0; j < this.GL.Map.GetLength(1); j++)
                    {
                        MapObject item = this.GL.Map[i, j];

                        if (j == this.GL.Players[0].PosX && i == this.GL.Players[0].PosY)
                        {
                            this.DrawItem(
                                drawingContext,
                                currentDir + "\\images\\bomberman_p1.png",
                                tile_width,
                                tile_height,
                                i,
                                j);
                        }
                        else if (j == this.GL.Players[1].PosX && i == this.GL.Players[1].PosY)
                        {
                            this.DrawItem(
                                drawingContext,
                                currentDir + "\\images\\bomberman_p2.png",
                                tile_width,
                                tile_height,
                                i,
                                j);
                        }
                        else if (item is Wall)
                        {
                            if ((item as Wall).Destroyable)
                            {
                                this.DrawItem(
                                    drawingContext,
                                    currentDir + "\\images\\barrel.png",
                                    tile_width,
                                    tile_height,
                                    i,
                                    j);
                            }
                            else
                            {
                                this.DrawItem(
                                    drawingContext,
                                    currentDir + "\\images\\wall.png",
                                    tile_width,
                                    tile_height,
                                    i,
                                    j);
                            }
                        }
                        else if (item is Floor)
                        {
                            this.DrawFloor(drawingContext, tile_width, tile_height, i, j);
                        }
                        else if (item is Bomb)
                        {
                            this.DrawItem(
                                drawingContext,
                                currentDir + "\\images\\bomb.png",
                                tile_width,
                                tile_height,
                                i,
                                j);
                        }
                        else if (item is Explode)
                        {
                            this.DrawItem(
                                drawingContext,
                                currentDir + "\\images\\explode.png",
                                tile_width,
                                tile_height,
                                i,
                                j);
                        }
                        else if (item is PlusBombPowerUp)
                        {
                            this.DrawItem(
                                drawingContext,
                                currentDir + "\\images\\powerup.png",
                                tile_width,
                                tile_height,
                                i,
                                j);
                        }
                        else if (item is BrokenBarel)
                        {
                            this.DrawItem(
                                drawingContext,
                                currentDir + "\\images\\barel_broken.png",
                                tile_width,
                                tile_height,
                                i,
                                j);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Invokes InvalidateVisualMethod
        /// </summary>
        /// <param name="sender"> sender </param>
        /// <param name="e"> e </param>
        private void GL_GameStateChanged(object sender, EventArgs e)
        {
            Application.Current.Dispatcher?.Invoke(() => this.InvalidateVisual());
        }

        /// <summary>
        /// Simple version of drawRectangle
        /// </summary>
        /// <param name="drawingContext">drawingContext</param>
        /// <param name="filename">filename</param>
        /// <param name="tile_width">tile_width</param>
        /// <param name="tile_height">tile_height</param>
        /// <param name="i">i</param>
        /// <param name="j">j</param>
        private void DrawItem(DrawingContext drawingContext, string filename, double tile_width, double tile_height, int i, int j)
        {
            drawingContext.DrawRectangle(
                                    this.GetBrush(filename),
                                    null,
                                    new Rect(j * tile_width, i * tile_height, tile_width, tile_height));
        }

        /// <summary>
        /// Simple version of drawRectangle
        /// </summary>
        /// <param name="drawingContext">drawingContext</param>
        /// <param name="tile_width">tile_width</param>
        /// <param name="tile_height">tile_height</param>
        /// <param name="i">i</param>
        /// <param name="j">j</param>
        private void DrawFloor(DrawingContext drawingContext, double tile_width, double tile_height, int i, int j)
        {
            drawingContext.DrawRectangle(
                                    Brushes.Black,
                                    new Pen(Brushes.Black, 1),
                                    new Rect(j * tile_width, i * tile_height, tile_width, tile_height));
        }
    }
}
