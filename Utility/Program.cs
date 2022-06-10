namespace Utility;

public class Program
{
    public static void Main(string[] args)
    {
        ScanerUtility sc = new ScanerUtility();
        string cmd = Console.ReadLine();
        while (cmd != "exit")
        {
            if (cmd.StartsWith("status"))
            {
                int id = 0;
                if (!int.TryParse(cmd.Split(" ")[1], out id))
                {
                    Console.WriteLine("parse error");
                }
                else
                {
                    sc.GetStatus(id);
                }
            }
            else if (cmd.StartsWith("scan"))
            {
                string directory = cmd.Split(" ")[1];
                sc.ScanDirectory(directory);
            }
            cmd = Console.ReadLine();
        }
    }
    
}