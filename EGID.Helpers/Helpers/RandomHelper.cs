namespace EGIDTask.Helpers.Helpers
{
    public static class RandomHelper
    {
        private static readonly Random random = new Random();

        public static decimal RandomNumberBetween(decimal minValue, decimal maxValue)
        {
            var next = (decimal) random.NextDouble();

            return minValue + next * (maxValue - minValue);
        }
    }
}