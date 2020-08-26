// <copyright file="Bindable.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.UI.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Bindable class
    /// </summary>
    public class Bindable : INotifyPropertyChanged
    {
        /// <summary>
        /// PropertyChanged Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// OnPropchange
        /// </summary>
        /// <param name="s"> s param</param>
        protected void OnPropertyChange([CallerMemberName] string s = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));
        }
    }
}
