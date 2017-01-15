using System;
using System.Collections.Generic;
using System.Linq;

namespace TM_Impronet_Interface.Classes
{
    public class RosterDate : ICloneable
    {
        public string Code { get; set; }

        public DateTime StartDate { get; set; }

        public string Shifts { get; set; }

        public char DeductHours { get; set; }
        
        public char DeductNt { get; set; }

        public char DeductOt1 { get; set; }

        public char DeductOt2 { get; set; }

        public char DeductOt3 { get; set; }

        public char DeductOt4 { get; set; }

        public int TargetNt { get; set; }

        public int TargetOt1 { get; set; }

        public int TargetOt2 { get; set; }

        public int TargetOt3 { get; set; }

        public int TargetOt4 { get; set; }

        public char BalancingDirection { get; set; }

        public int MaxOt0 { get; set; }
        public int MaxOt1 { get; set; }

        public int MaxOt2 { get; set; }

        public int MaxOt3 { get; set; }

        public int MaxOt4 { get; set; }

        public override string ToString()
        {            
            return StartDate.ToString("dd-MMM-yyyy") + $" ({Shifts.Split(',').Count(i => i!="")})";
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        private IEnumerable<string> GetFirstWeekSchedule()
        {
            //Get start date day of the week            
            var dow = (int)StartDate.DayOfWeek;
            var fullSchedule = new List<string>(Shifts.Split(','));

            //Start from the first day of a full week
            if (dow != 0)
            {
                var removeCount = (6 - dow) + 1;
                if (removeCount > fullSchedule.Count)
                {
                    throw new Exception("The roster importer needs at least one full week as a reference for import");                    
                }
                fullSchedule.RemoveRange(0, removeCount);
            }

            //Get the first full week from the reference roster
            if (fullSchedule.Count < 7)
            {
                throw new Exception("The roster importer needs at least one full week as a reference for import");
            }
            return fullSchedule.GetRange(0, 7);
        }

        public RosterDate CreateMonthSchedule(DateTime startDate)
        {
            var weekSchedule = (List<string>) GetFirstWeekSchedule();
            var schedule = new List<string>();

            var startDow = (int)startDate.DayOfWeek;
            var daysInTheMonth = (DateTime.DaysInMonth(startDate.Year, startDate.Month) - startDate.Day) + 1;

            var addCount = (6 - startDow) + 1;
            daysInTheMonth -= addCount;
            schedule.AddRange(weekSchedule.GetRange(startDow, addCount));

            while (daysInTheMonth >= 7)
            {
                schedule.AddRange(weekSchedule);
                daysInTheMonth -= 7;
            }

            //Add remaining days
            schedule.AddRange(weekSchedule.GetRange(0, daysInTheMonth));

            var rosterDate = (RosterDate)Clone();
            rosterDate.StartDate = startDate;
            rosterDate.Shifts = schedule.Aggregate(string.Empty, (current, s) => current + (s + ",")).TrimEnd(',');
            return rosterDate;
        }

        public RosterDate CreateWeekSchedule(DateTime startDate, short numberOfWeeks)
        {
            var weekSchedule = (List<string>)GetFirstWeekSchedule();
            var schedule = new List<string>();

            for (var i = 0; i < numberOfWeeks; i++)
            {
                schedule.AddRange(weekSchedule);
            }
            //var scheduleCount = schedule.Count;
            //for (var i = 0; i < 32 - scheduleCount; i++)
            //{
            //    schedule.Add("");
            //}

            var rosterDate = (RosterDate)Clone();
            rosterDate.StartDate = startDate;
            rosterDate.Shifts =
                schedule.Aggregate(string.Empty, (current, s) => current + (s + ","));
            //rosterDate.Shifts = rosterDate.Shifts.Remove(rosterDate.Shifts.Length - 1, 1);
            return rosterDate;
        }

        public bool CheckIfOverlap(RosterDate checkRosterDate)
        {
            //Reference Range
            var daysInSchedule = new List<string>(checkRosterDate.Shifts.Split(',').Where(i=>i!="")).Count;
            var refStart = checkRosterDate.StartDate;
            var refEnd = checkRosterDate.StartDate.AddDays(daysInSchedule);

            //Class Range
            var cdaysInSchedule = new List<string>(Shifts.Split(',').Where(i => i != "")).Count;
            var cStart = StartDate;
            var cEnd = StartDate.AddDays(cdaysInSchedule);

            return refStart < cEnd && cStart < refEnd;
        }
    }
}
