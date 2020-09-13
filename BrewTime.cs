namespace BrewdogBeerTests
{
    public class BrewTime
    {
        public string Brewed_before;
        public string Brewed_after;
        public string When;

        public BrewTime()
        {
        }
        public BrewTime(string when)
        {
            Brewed_before = "brewed_before";
            Brewed_after = "brewed_after";
            When = when;
        }
    }
}
