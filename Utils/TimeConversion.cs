namespace HospitalManagementSystem.Utils
{
    public class TimeConversion
    {
        // NETMF DateTime ticks origin is 1601/1/1
        const long epochTicks = 621355968000000000;
        internal const long TicksPerMillisecond = 10000;
        public static DateTime TimestampToDateTime(long timestamp)
        {
            if (timestamp > (DateTime.MaxValue.Ticks - epochTicks) / TicksPerMillisecond)
            {
                return DateTime.MaxValue;
            }
            else if (timestamp < (DateTime.MinValue.Ticks - epochTicks) / TicksPerMillisecond)
            {
                return DateTime.MinValue;
            }

            return new DateTime(epochTicks + timestamp * TicksPerMillisecond, DateTimeKind.Utc);
        }
        public static long DateTimeToTimestamp(DateTime dateTime)
        {
            return (long)((dateTime.ToUniversalTime().Ticks - epochTicks) / TicksPerMillisecond);
        }
    }
}
