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

        private static string Days(TimeSpan timeSpan)
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

        private static string Hours(TimeSpan timeSpan)
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
    }
}