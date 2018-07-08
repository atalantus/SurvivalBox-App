using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataParser
{
    /// <summary>
    /// GPSData-Object containing Position, Time, Velocity and KnotVelocity from GPS-Data using an NMEA formatted message.
    /// </summary>
    public class GPSData
    {
        private GPSPosition position;
        private DateTime time;
        private float trueVelocity;
        private float knotVelocity;

        public string TimeString => GetTime() + " (UTC)";
        public string PositionString => $"Coordinates: {position.latitude.value}, {position.longitude.value}";
        public string TrueVelocityString => $"Velocity: {trueVelocity.ToString()}";

        /// <summary>
        /// Creates a new GPSData object.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="time"></param>
        /// <param name="trueVelocity"></param>
        /// <param name="knotVelocity"></param>
        public GPSData(GPSPosition position, DateTime time, float trueVelocity, float knotVelocity)
        {
            this.position = position;
            this.time = time;
            this.trueVelocity = trueVelocity;
            this.knotVelocity = knotVelocity;
        }

        public GPSData()
        {
        }

        public string GetTime()
        {
            return time.ToUniversalTime().ToString("g");
        }


        /// <summary>
        /// Updates this GPSData-Object with the new values of the input.
        /// </summary>
        /// <param name="message">The input data string starting with $GPRMC</param>
        public void ParseGPRMC(string message)
        {
            string[] data = message.Split(',');
            this.position = new GPSPosition(
                new Coordinate(float.Parse(data[3]), Coordinate.ParseDirection(data[4])),
                new Coordinate(float.Parse(data[5]), Coordinate.ParseDirection(data[6])));

            // Format DateTime
            StringBuilder timeBuilder = new StringBuilder();
            for (int i = 0; i < data[1].Length; i++)
            {
                if (i % 2 == 0)
                    timeBuilder.Append(':');
                timeBuilder.Append(data[1][i]);
            }
            string timeFormatted = timeBuilder.ToString();

            StringBuilder dateBuilder = new StringBuilder();
            for (int i = 0; i < data[9].Length; i++)
            {
                if (i % 2 == 0)
                    dateBuilder.Append('.');
                dateBuilder.Append(data[9][i]);
            }
            string dateFormatted = dateBuilder.ToString();
            dateFormatted = dateFormatted.Remove(0, 1);
            timeFormatted = timeFormatted.Remove(0, 1);

            this.time = DateTime.ParseExact(timeFormatted + " " + dateFormatted, "HH:mm:ss dd.MM.yy", CultureInfo.InvariantCulture);

            this.trueVelocity = float.Parse(data[8]);
            this.knotVelocity = float.Parse(data[9]);
        }

        /// <summary>
        /// Returns a new GPSData-Object with the values of the input.
        /// </summary>
        /// <param name="message">The input data string starting with $GPRMC</param>
        /// <returns>New GPSData-Object.</returns>
        public static GPSData GetGPRMCObject(string message)
        {
            string[] data = message.Split(',');
            GPSPosition position = new GPSPosition(
                new Coordinate(float.Parse(data[3]), Coordinate.ParseDirection(data[4])),
                new Coordinate(float.Parse(data[5]), Coordinate.ParseDirection(data[6])));

            // Format DateTime
            StringBuilder timeBuilder = new StringBuilder();
            for (int i = 0; i < data[1].Length; i++)
            {
                if (i % 2 == 0)
                    timeBuilder.Append(':');
                timeBuilder.Append(data[1][i]);
            }
            string timeFormatted = timeBuilder.ToString();

            StringBuilder dateBuilder = new StringBuilder();
            for (int i = 0; i < data[9].Length; i++)
            {
                if (i % 2 == 0)
                    dateBuilder.Append('.');
                dateBuilder.Append(data[9][i]);
            }
            string dateFormatted = dateBuilder.ToString();
            dateFormatted = dateFormatted.Remove(0, 1);
            timeFormatted = timeFormatted.Remove(0, 1);

            DateTime time = DateTime.ParseExact(timeFormatted + " " + dateFormatted, "HH:mm:ss dd.MM.yy", CultureInfo.InvariantCulture);

            float trueVelocity = float.Parse(data[8]);
            float knotVelocity = float.Parse(data[9]);

            return new GPSData(position, time, trueVelocity, knotVelocity);
        }
    }

    public struct GPSPosition
    {
        public Coordinate latitude;
        public Coordinate longitude;

        public GPSPosition(Coordinate lat, Coordinate lon)
        {
            this.latitude = lat;
            this.longitude = lon;
        }
    }

    public enum CoordinateDirection
    {
        N, E, S, W
    }

    public struct Coordinate
    {
        public float value;
        public CoordinateDirection direction;

        public Coordinate(float v, CoordinateDirection d)
        {
            this.value = v;
            this.direction = d;
        }

        public static CoordinateDirection ParseDirection(string m)
        {
            switch (m)
            {
                case "N":
                    return CoordinateDirection.N;
                    break;
                case "E":
                    return CoordinateDirection.E;
                    break;
                case "S":
                    return CoordinateDirection.S;
                    break;
                case "W":
                    return CoordinateDirection.W;
                    break;
            }
            throw new Exception($"Invalid CoordinateDirection '{m}'");
        }

    }
}
