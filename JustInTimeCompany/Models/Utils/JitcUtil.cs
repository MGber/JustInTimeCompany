namespace JustInTimeCompany.Models.Utils
{
    public class JitcUtil
    {
        public static double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            if (lat1 == lat2 && lon1 == lon2)
                return 0;
            else
            {
                var theta = lon1 - lon2;
                var dist = Math.Sin(Deg2Rads(lat1)) * Math.Sin(Deg2Rads(lat2)) +
                           Math.Cos(Deg2Rads(lat1)) * Math.Cos(Deg2Rads(lat2)) * Math.Cos(Deg2Rads(theta));
                dist = Math.Acos(dist);
                dist = Rads2Deg(dist);
                dist = dist * 60 * 1.1515;
                return Math.Truncate(10 * (dist * 1.609344)) / 10;
            }
        }

        private static double Deg2Rads(double degrees)
        {
            return (Math.PI / 180) * degrees;
        }

        private static double Rads2Deg(double radians)
        {
            return (180 / Math.PI) * radians;
        }
    }
}
