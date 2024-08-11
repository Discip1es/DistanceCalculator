namespace DistanceCalculator.Models
{
    /// <summary>
    /// Класс, представляющий город.
    /// </summary>
    public class City
    {
        /// <summary>
        /// Название города.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Широта города.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Долгота города.
        /// </summary>
        public double Longitude { get; set; }
    }
}
