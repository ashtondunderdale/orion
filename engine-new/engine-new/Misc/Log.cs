using System.IO;

namespace engine_new.Misc;

public class Logger
{
    public static void CreateLogFile(string projectName) 
    {
        if (!File.Exists(Utils.ProjectPath + $"\\{projectName}" + "\\Log.txt"))
        {
            using var sw = new StreamWriter(Utils.ProjectPath + $"\\{projectName}" + "\\Log.txt", true);
        }
    }

    public static void Log(string info, string projectName) 
    {
        if (File.Exists(Utils.ProjectPath + "\\projectName" + "\\Log.txt"))
        {
            using var sw = new StreamWriter(Utils.ProjectPath + "\\projectName" + "\\Log.txt", true);
            {
                sw.WriteLine(info);
            }
        }
    }
}
