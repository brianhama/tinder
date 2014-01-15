using System;

namespace Minimod.PrettyDateAndTime
{
    /// <summary>
    /// Minimod.PrettyDateAndTimeMinimod, Version 1.1.0
    /// <para>Formats DateTime, DateTimeOffset and TimeSpan in a very readable manner.</para>
    /// </summary>
    /// <remarks>
    /// Licensed under the Apache License, Version 2.0; you may not use this file except in compliance with the License.
    /// http://www.apache.org/licenses/LICENSE-2.0
    /// </remarks>
    internal static class PrettyDateAndTimeMinimod
    {
        public static string GetPrettyString(this DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue) return "<DateTime.MinValue>";
            if (dateTime == DateTime.MaxValue) return "<DateTime.MaxValue>";

            string kind = "";
            if (dateTime.Kind == DateTimeKind.Utc)
                kind = " (UTC)";
            else if (dateTime.Kind == DateTimeKind.Local)
                kind = dateTime.ToString(" (K)");

            if (dateTime.TimeOfDay == TimeSpan.Zero) return dateTime.ToString("yyyy-MM-dd") + kind;
            if (dateTime.Second + dateTime.Millisecond == 0) return dateTime.ToString("yyyy-MM-dd HH:mm") + kind;
            if (dateTime.Millisecond == 0) return dateTime.ToString("yyyy-MM-dd HH:mm:ss") + kind;

            return dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + kind;
        }

        public static string GetPrettyString(this DateTimeOffset dateTime)
        {
            return "<DateTimeOffset> { "
                   + GetPrettyString(dateTime.LocalDateTime) + ", "
                   + GetPrettyString(dateTime.UtcDateTime)
                   + " }";
        }

        public static string GetPrettyString(this TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero)
            {
                return "<TimeSpan.Zero>";
            }

            if (timeSpan == TimeSpan.MinValue)
            {
                return "<TimeSpan.MinValue>";
            }

            if (timeSpan == TimeSpan.MaxValue)
            {
                return "<TimeSpan.MaxValue>";
            }

            if (timeSpan < TimeSpan.FromSeconds(1))
            {
                return milliseconds(timeSpan);
            }
            if (timeSpan < TimeSpan.FromMinutes(1))
            {
                return seconds(timeSpan);
            }
            if (timeSpan < TimeSpan.FromHours(1))
            {
                return minutes(timeSpan);
            }
            if (timeSpan < TimeSpan.FromHours(24))
            {
                return hours(timeSpan);
            }

            return days(timeSpan);
        }

        private static string days(TimeSpan timeSpan)
        {
            if (timeSpan.TotalDays % 1 == 0)
            {
                return (int)timeSpan.TotalDays + " d";
            }

            return String.Format("{0}.{1:D2}" +
                                 ((timeSpan.TotalHours % 1 == 0)
                                      ? ""
                                      : ":{2:D2}" + ((timeSpan.TotalMinutes % 1 == 0)
                                                         ? ""
                                                         : ":{3:D2}" + (timeSpan.TotalSeconds % 1 == 0
                                                                            ? ""
                                                                            : ".{4:D3}"))) + " d", timeSpan.Days,
                                 timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }

        private static string hours(TimeSpan timeSpan)
        {
            if (timeSpan.TotalHours % 1 == 0)
            {
                return (int)timeSpan.TotalHours + " h";
            }

            return String.Format("{0}:{1:D2}" + ((timeSpan.TotalMinutes % 1 == 0)
                                                     ? ""
                                                     : ":{2:D2}" + (timeSpan.TotalSeconds % 1 == 0
                                                                        ? ""
                                                                        : ".{3:D3}")) + " h", timeSpan.Hours,
                                 timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }

        private static string milliseconds(TimeSpan timeSpan)
        {
            if (timeSpan.TotalMilliseconds % 1 == 0)
            {
                return (int)timeSpan.TotalMilliseconds + " ms";
            }

            return timeSpan.TotalMilliseconds.ToString() + " ms";
        }

        private static string minutes(TimeSpan timeSpan)
        {
            if (timeSpan.TotalMinutes % 1 == 0)
            {
                return (int)timeSpan.TotalMinutes + " min";
            }

            return String.Format("{0}:{1:D2}" + (timeSpan.Milliseconds == 0 ? "" : ".{2:D3}") + " min",
                                 timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }

        private static string seconds(TimeSpan timeSpan)
        {
            if (timeSpan.TotalSeconds % 1 == 0)
            {
                return (int)timeSpan.TotalSeconds + " s";
            }

            return String.Format("{0}.{1:D3} s", timeSpan.Seconds, timeSpan.Milliseconds);
        }
    }
}