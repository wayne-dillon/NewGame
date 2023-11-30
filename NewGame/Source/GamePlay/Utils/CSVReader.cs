using System.Collections.Generic;
using System.IO;

public class CSVReader
{
    public static List<string[]> ReadFile(string PATH)
    {
        List<string[]> ret = new();

        using (StreamReader reader = new(PATH))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');
                ret.Add(values);
            }
        }

        return ret;
    }
}