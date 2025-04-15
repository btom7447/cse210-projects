using System;

abstract class Activity
{
    private DateTime _date;
    private int _lengthMinutes;

    public Activity(DateTime date, int lengthMinutes)
    {
        _date = date;
        _lengthMinutes = lengthMinutes;
    }

    public DateTime Date => _date;
    public int LengthMinutes => _lengthMinutes;

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public virtual string GetSummary()
    {
        return $"{Date:dd MMM yyyy} {GetType().Name} ({LengthMinutes} min): " +
               $"Distance {GetDistance():0.0} km, Speed {GetSpeed():0.0} kph, Pace: {GetPace():0.0} min per km";
    }
}

// Running class
class Running : Activity
{
    private double _distanceKm;

    public Running(DateTime date, int lengthMinutes, double distanceKm)
        : base(date, lengthMinutes)
    {
        _distanceKm = distanceKm;
    }

    public override double GetDistance() => _distanceKm;
    public override double GetSpeed() => (_distanceKm / LengthMinutes) * 60;
    public override double GetPace() => LengthMinutes / _distanceKm;
}

// Cycling class
class Cycling : Activity
{
    private double _speedKph;

    public Cycling(DateTime date, int lengthMinutes, double speedKph)
        : base(date, lengthMinutes)
    {
        _speedKph = speedKph;
    }

    public override double GetDistance() => _speedKph * LengthMinutes / 60;
    public override double GetSpeed() => _speedKph;
    public override double GetPace() => 60 / _speedKph;
}

// Swimming class
class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int lengthMinutes, int laps)
        : base(date, lengthMinutes)
    {
        _laps = laps;
    }

    public override double GetDistance() => (_laps * 50) / 1000.0;
    public override double GetSpeed() => (GetDistance() / LengthMinutes) * 60;
    public override double GetPace() => LengthMinutes / GetDistance();
}

// Program class with Main method
class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 4.8),
            new Cycling(new DateTime(2022, 11, 3), 45, 20.0),
            new Swimming(new DateTime(2022, 11, 3), 30, 40)
        };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}