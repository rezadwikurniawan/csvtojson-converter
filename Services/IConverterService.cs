using CsvToJsonConverter.Model;

namespace CsvToJsonConverter.Services
{
    public interface IConverterService
    {
        string ConvertCsvToJson(RequestModel content);        
    }
}