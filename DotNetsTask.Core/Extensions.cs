using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;

namespace DotNetsTasks.Core
{
    public static class Extensions
    {

        public static void AddOrReplace(this IDictionary<string, object> DICT, string key, object value)
        {
            if (DICT.ContainsKey(key))
                DICT[key] = value;
            else
                DICT.Add(key, value);
        }

        public static DateTime? ToDateTime(this string source, string format)
        {
            if (!string.IsNullOrWhiteSpace(source))
            {
                DateTime convertedDateTime;
                if (DateTime.TryParseExact(source, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out convertedDateTime))
                {
                    return convertedDateTime;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
        public static string ToUnique(this string fileName)
        {
            return $"{DateTime.Now.Ticks}{Path.GetExtension(fileName.ToLower())}";
        }
        public static string SaveFile(this IFormFile formFile, string pathToSave)
        {
            var fileExtension = Path.GetExtension(formFile.FileName);
            var fileOriginalName = Path.GetFileNameWithoutExtension(formFile.FileName);

            var fileName = $"{fileOriginalName}_{DateTime.Now.Ticks}.{fileExtension}";

            try
            {
                if (!string.IsNullOrWhiteSpace(pathToSave))
                {
                    if (!Directory.Exists(pathToSave))
                    {
                        Directory.CreateDirectory(pathToSave);
                    }


                    using (FileStream filestream = new FileStream(pathToSave + fileName, FileMode.Create))
                    {
                        formFile.CopyToAsync(filestream);
                    }

                }
            }
            catch (Exception)
            {
                fileName = null;
            }

            return fileName;
        }


        public static string GetDescription<TEnum>(this TEnum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            if (fi != null)
            {
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }

            return value.ToString();
        }

        /// <summary>
        /// Splits a List<T> into multiple chunks
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list to be chunked</param>
        /// <param name="chunkSize">The size of each chunk</param>
        /// <returns>A list of chunks</returns>
        public static List<List<T>> SplitIntoChunks<T>(this List<T> list, int chunkSize)
        {
            if (chunkSize <= 0)
            {
                throw new ArgumentException("Chunk size must be greater than 0");
            }
            List<List<T>> retVal = new List<List<T>>();
            int index = 0;
            while (index < list.Count)
            {
                int count = list.Count - index > chunkSize ? chunkSize : list.Count - index;
                retVal.Add(list.GetRange(index, count));
                index += chunkSize;
            }
            return retVal;
        }



      


    }
}
