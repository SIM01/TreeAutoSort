using TreeAutoSort.Service;
using TreeAutoSort.Models;

namespace TreeAutoSort
{
    class Program
    {
        static void Main()
        {
            string currDir = Environment.CurrentDirectory;
            Console.WriteLine("Введите имя файла:");
            var filename = Console.ReadLine();
            if (string.IsNullOrEmpty(filename))
            {
                throw new TreeAutoSort.Exceptions.FileNotFoundException("Ошибка ввода!");
            }

            string path = currDir + "\\" + filename;
            if (!File.Exists(path))
            {
                throw new TreeAutoSort.Exceptions.FileNotFoundException("Такого файла не существует!");
            }

            Console.WriteLine("Введите способ чтения файла(0 - ручной; 1 - CsvHelper):");
            var readingtype = Console.ReadLine();
            if (string.IsNullOrEmpty(readingtype))
            {
                throw new TreeAutoSort.Exceptions.FileNotFoundException("Ошибка ввода!");
            }

            Console.WriteLine("Введите способ вывода результата (0 - на экран; 1 - в файл):");
            var writeingtype = Console.ReadLine();
            if (string.IsNullOrEmpty(writeingtype))
            {
                throw new TreeAutoSort.Exceptions.FileNotFoundException("Ошибка ввода!");
            }

            List<DirtyData> datas;
            if (readingtype == "0")
            {
                datas = ManualRead.ManualReading(path);
            }
            else
            {
                datas = LibraryRead.LibraryReading(path);
            }

            var sortdata = SortRows.SortingRows(datas);
            string outputtext = String.Empty;
            foreach (var row in sortdata)
            {
                if (writeingtype == "0")
                {
                    Console.WriteLine((row.Countspace == 0 ? "" : " ".PadLeft(row.Countspace))
                                      +
                                      $"{row.Id};{row.Parentid};{row.Text};");
                }
                else
                {
                    outputtext = outputtext + (row.Countspace == 0 ? "" : " ".PadLeft(row.Countspace)) +
                                 $"{row.Id};{row.Parentid};{row.Text};" + "\n";
                }
            }

            if (writeingtype != "0")
            {
                using (FileStream file = File.Create(path.Replace(filename, "tree_" + filename)))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(outputtext);
                    file.Write(array, 0, array.Length);
                    Console.WriteLine("Готово!");
                }
            }
        }
    }
}