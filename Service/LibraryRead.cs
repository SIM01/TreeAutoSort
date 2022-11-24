using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using TreeAutoSort.Models;

namespace TreeAutoSort.Service;
public class LibraryRead
{
     public static List<DirtyData> LibraryReading(string path)
     {
         List<DirtyData> datas;
         string text = "Id;Parentid;Text;\n";     //заголовок
         string filetext = File.ReadAllText(path);
         File.WriteAllText(path, (filetext.IndexOf(text) == -1 ? text : "") + filetext );

         using (var streamReader = new StreamReader(path))
         {
             var config = new CsvConfiguration(CultureInfo.InvariantCulture)
             {
                 Delimiter = ";"
             };
             using (var csvReader = new CsvReader(streamReader, config))
             {
                 var records = csvReader.GetRecords<DirtyData>();
                 datas = records.ToList();
             }
         }

         return datas;
     }
 }