namespace BrewdogBeerTests
{
    public class BeerScale
    {
        public string Abv_gt;
        public string Abv_lt;
        public string Ibu_gt;
        public string Ibu_lt;
        public string Ebc_gt;
        public string Ebc_lt;
        public string ScaleType;
        public double Value;

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
