﻿using System;

namespace InterpolatedColorConsole
{
    /// <summary>
    /// RawString is just a wrapper around string (with implicit conversion to/from string) which allows us to have overloads for receiving IFormattable (interpolated strings) 
    /// and other overloads that accept plain strings, while prioritizing the overloads for IFormattable when the input is an interpolated string.<br />
    /// If you use interpolated strings (which allow to use a wide range of action delegates) you'll end up using the methods overloads which accept IFormattable. <br />
    /// If you just pass a regular string it will be converted to RawString. 
    /// (In other words, if we had overloads taking plain strings then all interpolated strings would be converted to strings and we would break all the magic)
    /// Based on https://www.damirscorner.com/blog/posts/20180921-FormattableStringAsMethodParameter.html
    /// </summary>
    public class RawString
    {
        private string Value { get; }

        private RawString(string str)
        {
            Value = str;
        }

        /// <summary>
        /// Implicit conversion
        /// </summary>
        public static implicit operator RawString(string str) => new RawString(str);

        /// <summary>
        /// Implicit conversion
        /// </summary>
        public static implicit operator RawString(FormattableString formattable) => new RawString(formattable.ToString());

        /// <summary>
        /// Implicit conversion
        /// </summary>
        public static implicit operator string(RawString raw) => raw.Value;
    }
}
