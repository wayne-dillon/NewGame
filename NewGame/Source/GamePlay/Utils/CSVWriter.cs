using System.Collections.Generic;
using System.IO;
using System.Text;

public class CSVWriter
{
    public static void WriteFile(string PATH, List<string[]> STRINGS)
    {
        StringBuilder txt = new();
        foreach (string[] line in STRINGS)
        {
            txt.Append(line[0]);
            for (int i = 1; i < line.Length; i++)
            {
                txt.Append($",{line[i]}");
            }
            txt.Append('\n');
        }
        File.WriteAllText(PATH, txt.ToString());
    }
}