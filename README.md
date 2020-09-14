## BrewDogBeerChallange
Repository made for the purpose of a job interview for a Test Automation position. I have to admit that the code is not best and it should look better, but maybe some time later I will give it a refreshed stage.

### Change log
- 12.09.2020 - All of this code - firstly has been created using Visual studio code run with c# and .netcore framework from an raspberry pi4 with 4GBs of RAM. Then I have moved to VS2019 and a normal PC.
- 13.09.2020 - Initial commit. Added most of the functionalities. Missing some of the Negative test cases. It is just too late for my eyes.
- 14.09.2020 - Removed some of not used comments. Added just one negative test. I know that there are many apporach to API testing and only sky is the limit but for me "time is of the essence".

### Tools
- The testing was conducted using a API client called RestSharp. The client is based on C# and .NET -> .NET CORE, thus the C# was used to create some methods to support this client.
- The following API was used for testing: https://punkapi.com/documentation/v2 and tests were created by following the requests from the job interview itself.
- Firstly test execution was based on a .NET Core console application, later I have moved to Unittests available from Visual Studio 2019.

### Issues
- There is an issue with the beer scale being which should be a double. In case of the restsharp all of the input is a string, but still the "tester" should input a double to the method. I had discovered
- an issue where the input is being add to the request but for some reason the dot (.) from type of double is switched to a comma (,) which in this case makes the request to the API not possible
- I have added a small walkaround which replaces the comma(,) with a dot(,) and thus this is possible to run.
- Also the Unittest from VS is not designed to continue on failure (my bad) and not prepared to report logs. I had to use the System.Diagnostics instead. 
- The whole response from the API is not best suited and for usage with Dicationaries and even foreaching through those and splitting the output makes the output very unreadable. I gave up on splitting and adding to the dictionary. The whole response.Content is a long string where I search for the Data which I have requested to have. Not the best apporach but for such a task possible to change in the nearby future. 

#### Documentation
- Test API: https://punkapi.com/documentation/v2
- Restsharp: https://restsharp.dev/
- Unit Tests Documentation: https://restsharp.dev/