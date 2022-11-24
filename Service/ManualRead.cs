using TreeAutoSort.Models;
namespace TreeAutoSort.Service;

public class ManualRead
{
    public static List<DirtyData> ManualReading(string path)
    {
        var datas = new List<DirtyData>();
        string text = "Id;Parentid;Text;\n";     //заголовок
        string filetext = File.ReadAllText(path);
        
        File.WriteAllText(path,   filetext.Replace(text,"") );
        
        IEnumerable<string> lines = File.ReadLines(path);
        foreach (var line in lines)
        {
            string[] slist = line.Split(';');

            datas.Add(new DirtyData()
            {
                Id = Convert.ToInt32(slist[0]),
                Parentid = Convert.ToInt32(slist[1]),
                Text = slist[2]
            });
        }

        return datas;
    }
}