namespace BrewdogBeerTests
{
    public class BrewTime
    {
        public string Brewed_before { get; private set; }

        public string Brewed_after { get; private set; }
        public string When;

        public BrewTime()
        {
            Brewed_before = "brewed_before";
            Brewed_after = "brewed_after";
        }

        public BrewTime(string when)
        {
            Brewed_before = "brewed_before";
            Brewed_after = "brewed_after";
            When = when;
        }
    }
}
