using System.IO;

namespace engine_new.Misc;

public class Logger
{
    public static void CreateLogFile(string projectName) 
    {
        if (!File.Exists(Utils.ProjectPath + $"\\{projectName}" + "\\Log.txt"))
        {
            using var sw = new StreamWriter(Utils.ProjectPath + $"\\{projectName}" + "\\Log.txt", true);
            Utils.ProjectLogFilePath = Utils.ProjectPath + $"\\{projectName}" + "\\Log.txt";
        }
    }

    public static void Log(string info) 
    {
        if (File.Exists(Utils.ProjectLogFilePath))
        {
            using var sw = new StreamWriter(Utils.ProjectLogFilePath);
            {
                sw.WriteLine(info);
            }
        }
    }
}
