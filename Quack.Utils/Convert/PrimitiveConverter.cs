﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Quack.Utils.Exceptions;

namespace Quack.Utils.Convert
{
    /// <summary>
    /// Convert string Value to T.
    /// </summary>
    public static class StringConverter
    {
        /// <summary>
        /// Convert a string to <see cref="TOutput"/>
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="value"></param>
        /// <exception cref="NotConvertibleException"></exception>
        public static TOutput To<TOutput>(this string value) where TOutput : struct
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(TOutput));

            return converter.CanConvertFrom(typeof(string))
                   ? (TOutput)converter.ConvertFrom(value)
                   : throw new NotConvertibleException($"Unable to convert type {typeof(string)} with value {value } " +
                                                       $"to type {typeof(TOutput)}");
        }
    }
}