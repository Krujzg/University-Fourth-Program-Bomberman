// <copyright file="InstructionsWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.UI
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for InstructionsWindow.xaml
    /// </summary>
    public partial class InstructionsWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstructionsWindow"/> class.
        /// </summary>
        public InstructionsWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Close
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
