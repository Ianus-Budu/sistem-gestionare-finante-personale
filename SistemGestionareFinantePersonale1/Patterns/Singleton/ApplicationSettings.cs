public class ApplicationSettings
{
    private static ApplicationSettings instance;
    private static readonly object lockObject = new object();

    private ApplicationSettings()
    {
    }

    public static ApplicationSettings Instance
    {
        get
        {
            lock (lockObject)
            {
                if (instance == null)
                {
                    instance = new ApplicationSettings();
                }

                return instance;
            }
        }
    }

    public string Currency { get; set; } = "MDL";
}