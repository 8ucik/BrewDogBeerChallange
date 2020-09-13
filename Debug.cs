using RestSharp;

namespace BrewdogBeerTests
{
    public class Debug
    {
        public static void DebugPars(RestRequest request)
        {
            foreach (var par in request.Parameters)
            {
                if (par != null)
                {
                    System.Diagnostics.Debug.WriteLine(par);
                }
                else
                {
                    // This is a joke from my previous job. Made on purpose.
                    System.Diagnostics.Debug.WriteLine("Something is no yes.");
                }
            }
        }
    }
}
