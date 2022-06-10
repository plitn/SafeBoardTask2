namespace SafeBoardTask2;

public class TaskClass
{
    private Task Task { get; set; }
    private int jsErrs;
    private int susErrs;
    private int rmErrs;
    private int filesChecked;
    private string directory;
    private int checkErrors;
    private DateTime workTimer;

    public TaskClass(string directory)
    {
        checkErrors = 0;
        filesChecked = 0;
        this.directory = directory;
        jsErrs = 0;
        susErrs = 0;
        rmErrs = 0;
        Task = new Task(() => SearchErrors(directory));
    }

    private void SearchErrors(string directory)
    {
        
        string[] files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            try
            {
                var text = File.ReadAllText(file);
                Console.WriteLine(text);
                string userProfString = Environment.ExpandEnvironmentVariables(@"%userprofile%\Documents");
                if (file.EndsWith(".js") && text.Contains("<script>evil_script()</script>"))
                {
                    jsErrs++;
                }
                else if (text.Contains("Rundll32 sus.dll SusEntry"))
                {
                    susErrs++;
                }
                else if (text.Contains(@"rm -rf %userprofile%\Documents") || text.Contains($"rm -rf {userProfString}"))
                {
                    rmErrs++;
                }
            }
            catch (IOException ex)
            {
                checkErrors++;
            }

            filesChecked++;
        }
    }

    public void Start()
    {
        Task = new Task(() => SearchErrors(directory));
        Task.Start();
        workTimer = DateTime.Now;
    }

    public bool IsTaskCompleted()
    {
        return Task.IsCompleted;
    }
    
    public int GetTaskId()
    {
        return Task.Id;
    }

    public string GetInfo()
    {
        var workTime = DateTime.Now - workTimer;
        return $"Directory: {directory}\nProcessed files: {filesChecked}\nJs detecs: {jsErrs}\n" +
               $"rm -rf detects: {rmErrs}\nRundll32 detects: {rmErrs}\nErrors: {checkErrors}\n" +
               $"Exection time: {DateTime.Now - workTime}";
    }
}