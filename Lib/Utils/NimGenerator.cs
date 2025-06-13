namespace Lib.Utils
{
    public static class NimGenerator
    {
        private static Dictionary<string, int> dailyCounters = new();

        public static string GenerateNim()
        {
            var today = DateTime.Today;
            string datePart = today.ToString("yyyyMMdd");

            if (!dailyCounters.ContainsKey(datePart))
                dailyCounters[datePart] = 1;
            else
                dailyCounters[datePart]++;

            string counterPart = dailyCounters[datePart].ToString("D3");

            return datePart + counterPart;
        }
    }
}
