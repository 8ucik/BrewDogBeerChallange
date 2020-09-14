namespace BrewdogBeerTests
{
    public class BeerScale
    {
        public string Abv_gt { get; set; }
        public string Abv_lt { get; set; }
        public string Ibu_gt { get; set; }
        public string Ibu_lt { get; set; }
        public string Ebc_gt { get; set; }
        public string Ebc_lt { get; set; }
        public readonly string ScaleType;
        public readonly double Value;

        public BeerScale(double value)
        {
            Abv_gt = "abv_gt";
            Abv_lt = "abv_lt";

            Ibu_gt = "ibu_gt";
            Ibu_lt = "ibu_lt";

            Ebc_gt = "ebc_gt";
            Ebc_lt = "ebc_lt";

            Value = value;
        }
        public BeerScale(double value, string scaleType)
        {
            ScaleType = scaleType;
            Value = value;
        }
    }
}
