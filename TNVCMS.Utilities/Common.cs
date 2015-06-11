﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TNVCMS.Utilities
{
    public static class Common
    {
        public static string GetUniqueString()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        public static string ToUrlSlug(string value)
        {
            value = RemoveUnicode(value);

            //First to lower case 
            value = value.ToLowerInvariant();

            //Remove all accents
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);

            value = Encoding.ASCII.GetString(bytes);

            //Replace spaces 
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);

            //Remove invalid chars 
            value = Regex.Replace(value, @"[^\w\s\p{Pd}]", "", RegexOptions.Compiled);

            //Trim dashes from end 
            value = value.Trim('-', '_');

            //Replace double occurences of - or \_ 
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return value;
        }
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",  
                                            "đ",  
                                            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",  
                                            "í","ì","ỉ","ĩ","ị",  
                                            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",  
                                            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",  
                                            "ý","ỳ","ỷ","ỹ","ỵ",};
                                                    string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",  
                                            "d",  
                                            "e","e","e","e","e","e","e","e","e","e","e",  
                                            "i","i","i","i","i",  
                                            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",  
                                            "u","u","u","u","u","u","u","u","u","u","u",  
                                            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
    }
}
