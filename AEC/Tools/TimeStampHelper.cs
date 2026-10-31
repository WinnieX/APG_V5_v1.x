using System;
using System.Collections.Generic;
using System.Text;

namespace AEC.Tools
{
    public class TimeStampHelper
    {
        private static readonly DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static uint DateTimeToUint32TimeStamp(DateTime dt)
        {
            //DateTime today = DateTime.Now;
            // Get the current UTC time

            DateTime currentTime = dt.ToUniversalTime();

            TimeSpan elapsedTime = currentTime - start;

            // Calculate the Unix timestamp using TimeSpan
            uint unixTimestamp = (uint)elapsedTime.TotalSeconds;

            return unixTimestamp;
        }

        public static DateTime Uint32TimeStampToDateTime(uint timeStamp)
        {
            TimeSpan ts = TimeSpan.FromSeconds(timeStamp);
            DateTime dt = start + ts;

            return dt.ToLocalTime();
        }

        public static uint GetTimeStampWithoutSecond(DateTime time)
        {
            DateTime timeStamp = new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);
            return DateTimeToUint32TimeStamp(timeStamp);
        }
    }
}
