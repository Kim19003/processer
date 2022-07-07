namespace Kimbrary.Types
{
    public class ValueHolder<T>
    {
        public T? Value { get; set; }

        public ValueHolder()
        {

        }

        public ValueHolder(T value)
        {
            Value = value;
        }
    }

    public class Time
    {
        public int Hour { get; set; }
        public int Minute { get; set; }

        private int dayDifference = 0;

        public Time(int hour, int minute)
        {
            if (hour < 24 && hour > -1)
            {
                Hour = hour;
            }
            else
            {
                Hour = 0;
            }
            if (minute < 60 && minute > -1)
            {
                Minute = minute;
            }
            else
            {
                Minute = 0;
            }
        }

        public void AddTime(Time time)
        {
            AddHours(time.Hour);
            AddMinutes(time.Minute);
        }

        public void RemoveTime(Time time)
        {
            RemoveHours(time.Hour);
            RemoveMinutes(time.Minute);
        }

        public void AddHours(int hours)
        {
            int addedHours = Hour + hours;

            if (addedHours > 23)
            {
                dayDifference++;
                Hour = addedHours - 24;
            }
            else
            {
                Hour = addedHours;
            }
        }

        public void RemoveHours(int hours)
        {
            int removedHours = Hour - hours;

            if (removedHours < 0)
            {
                dayDifference--;
                Hour = 24 - Math.Abs(removedHours);
            }
            else
            {
                Hour = removedHours;
            }
        }

        public void AddMinutes(int minutes)
        {
            int addedMinutes = Minute + minutes;

            if (addedMinutes > 59)
            {
                AddHours(1);
                Minute = addedMinutes - 60;
            }
            else
            {
                Minute = addedMinutes;
            }
        }

        public void RemoveMinutes(int minutes)
        {
            int removedMinutes = Minute - minutes;

            if (removedMinutes < 0)
            {
                RemoveHours(1);
                Minute = 60 - Math.Abs(removedMinutes);
            }
            else
            {
                Minute = removedMinutes;
            }
        }

        public void Tick()
        {
            AddMinutes(1);
        }

        public int GetDayDifference()
        {
            return dayDifference;
        }

        public override string ToString()
        {
            string stringFormat = $"{Hour}:";

            if (Minute < 10)
            {
                stringFormat += "0";
            }

            return stringFormat + $"{Minute}";
        }
    }
}