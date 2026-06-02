namespace Utils
{
    /// <summary>
    /// Utility class used to format string in specific formats
    /// </summary>
    public static class StringFormat
    {
        public static string FormatTimer(float currentTimer)
        {
            int seconds = (int)currentTimer;
            int milliseconds = (int)((currentTimer - seconds) * 100);
            return $"{seconds:D2}:{milliseconds:D2}";
        }
    }
}