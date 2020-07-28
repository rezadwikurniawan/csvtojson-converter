using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using CsvToJsonConverter.Model;
using CsvToJsonConverter.Services;
using CsvToJsonConverter.Utils;
using Newtonsoft.Json;

namespace CsvToJsonConverter.Service
{
    public class ConverterService : IConverterService
    {
        public string ConvertCsvToJson(RequestModel request)
        {                        
            return _ConvertCsv(request);
        }

        private string _ConvertCsv(RequestModel request)
        {
            if (request.delimiter == string.Empty)
            {
                request.delimiter = Constant.DEFAULT_DELIMITER;
            }
            string fileContent = DecodeBase64String(request.content);
            string[] fileContentArray = SplitCsvRowToArray(fileContent);
            List<KeyValuePair<string, object>> fileContentAsKeyVal = CreateKeyValPair(fileContentArray, request.delimiter);
            List<dynamic> convertedContent = CreateReponse(fileContentAsKeyVal, fileContentArray, request.delimiter);

            return JsonConvert.SerializeObject(convertedContent);
        }

        public string DecodeBase64String(string base64String)
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(base64String);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string[] SplitCsvRowToArray(string csvFileContent)
        {
            return csvFileContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }

        public int GetColumnCount(string[] content, string delimiter)
        {
            return content[0].Split(delimiter).Length;
        }

        public List<KeyValuePair<string, object>> CreateKeyValPair(string[] content, string delimiter)
        {
            List<KeyValuePair<string, object>> fileContentAsKeyVal = new List<KeyValuePair<string, object>>();
            
            content.ToList().Skip(1).ToList().ForEach(row =>
            {
                int columnIndex = 0;
                row.Split(delimiter).ToList().ForEach(column =>
                {
                    string[] splittedheaders = content.First().Split(delimiter);
                    string[] splittedContent = row.Split(delimiter);
                    fileContentAsKeyVal.Add(
                        new KeyValuePair<string, object>(splittedheaders[columnIndex], splittedContent[columnIndex]));
                    columnIndex++;
                });
            });

            return fileContentAsKeyVal;
        }

        public List<dynamic> CreateReponse(List<KeyValuePair<string, object>> contentList, string[] content, string delimiter)
        {
            int columnCount = GetColumnCount(content, delimiter);
            List<dynamic> convertedContent = new List<dynamic>();
            
            for (int i = 0; i < contentList.Count; i += columnCount)
            {
                IDictionary<string, object> dynamicObject = new ExpandoObject() as IDictionary<string, object>;
                for (int j = i; j < i + columnCount; j++)
                {
                    dynamicObject.Add(contentList[j].Key.Trim(), contentList[j].Value.ToString().Trim());
                }
                convertedContent.Add(dynamicObject);
            }
            return convertedContent;                
        }
    }
}