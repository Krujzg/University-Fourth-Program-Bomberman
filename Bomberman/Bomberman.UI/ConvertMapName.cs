// <copyright file="ConvertMapName.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Bomberman.UI
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;

    /// <summary>
    /// This class can chop of the .txt or .sav after the maps name
    /// </summary>
    public class ConvertMapName : IValueConverter
    {
        /// <summary>
        /// convert method
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="targetType">targetType</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns>Returns chopped of strings</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string word = (string)value;
            return word.Substring(0, word.Length - 4);
        }

        /// <summary>
        /// ConvertBack method
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="targetType">targetType</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns>Returns absolutely nothing</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
