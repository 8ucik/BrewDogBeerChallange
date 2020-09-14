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
                    System.Diagnostics.Debug.WriteLine("Something is no yes."); //this is a joke from previous work.
                }
            }
        }
        public static void ShowOutput(string output)
        {
            System.Diagnostics.Debug.WriteLine(output);
        } 
    }
}
