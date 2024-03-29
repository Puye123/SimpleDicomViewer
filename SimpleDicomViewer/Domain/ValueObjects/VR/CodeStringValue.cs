﻿using SimpleDicomViewer.Domain.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace SimpleDicomViewer.Domain.ValueObjects.VR
{
    /// <summary>
    /// CS(Code String: コード列) : string
    /// </summary>
    /// <remarks>
    /// 文字列であり，意味のない先頭または末尾のスペ－ス（20H）をもつことがある。 
    /// </remarks>
    public class CodeStringValue : ValueElement
    {
        public CodeStringValue(Tag tag, byte[] value) : base("CS", tag, value, length: 16*100, isFixedValue: false, valueType: typeof(string))
        {
        }

        protected override bool IsValidValue(byte[] value)
        {
            if (value.Length >= Length) { throw new InvalidDICOMFormatException($"設定可能な値は{Length}バイト以下です。"); }

            try
            {
                string valueString = System.Text.Encoding.ASCII.GetString(value);
                var result = Regex.IsMatch(valueString, @"^[A-Z0-9 _\\]*$");
                if (result)
                {
                    return true;
                }
                else
                {
                    throw new InvalidDICOMFormatException($"設定可能な値は^[A-Z0-9 _]*$です。設定しようとした値は{valueString}です。");
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}
