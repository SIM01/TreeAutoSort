using TreeAutoSort.Models;

namespace TreeAutoSort.Service;

public class SortRows
{
    static List<SortedData> _sortdata = new List<SortedData>();

    public static List<SortedData> SortingRows(List<DirtyData> originaldatas)
    {
        List<DirtyData> rootrows;
        rootrows = originaldatas.GetRange(0, originaldatas.Count).OrderBy(p => p.Text).ToList();

        foreach (var row in originaldatas)
        {
            var childrows = rootrows.Where(p => p.Parentid.Equals(row.Id) && p.Id != row.Id).OrderBy(p => p.Text);
            rootrows = rootrows.Except(childrows.ToList()).ToList();
        }

        foreach (var row in rootrows)
        {
            int count = 0;
            _sortdata.Add(new SortedData()
            {
                Id = row.Id,
                Parentid = row.Parentid,
                Text = row.Text,
                Countspace = count
            });
            RecursionSort(originaldatas, GetChildRows(originaldatas, row.Id), count + 1);
        }

        return _sortdata;
    }

    static void RecursionSort(List<DirtyData> originaldatas, List<DirtyData> datas, int count)
    {
        foreach (var row in datas)
        {
            _sortdata.Add(new SortedData()
            {
                Id = row.Id,
                Parentid = row.Parentid,
                Text = row.Text,
                Countspace = count
            });

            RecursionSort(originaldatas, GetChildRows(originaldatas, row.Id), count + 1);
        }
    }

    public static List<DirtyData> GetChildRows(List<DirtyData> datas, int id)
    {
        return datas.Where(p => p.Parentid.Equals(id) && p.Id != id).OrderBy(p => p.Text).ToList();
    }
}