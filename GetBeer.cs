using System;

namespace BrewdogBeerTests
{
    public class GetBeer
    {
        public string ParameterName;
        public string BeerName;
        public string YeastName;
        public string FoodName;
        public string BeverageName;
        public int Id;

        public GetBeer()
        {
        }

        public GetBeer(int id)
        {
            Id = id;
        }

        public GetBeer(string parameterName, string beverageName)
        {
            ParameterName = parameterName;
            BeverageName = beverageName;
        }

        public GetBeer(string parameterName, string beerName, string yeastName, string foodName, int id)
        {
            ParameterName = parameterName;
            BeerName = beerName;
            YeastName = yeastName;
            FoodName = foodName;
            Id = id;
        }

        public static string GetBeerById(string requestHeader, int id)
        {
            return string.Concat(requestHeader, "/", id);
        }

        public static string GetBeerByName(string beerName)
        {
            if (!string.IsNullOrWhiteSpace(beerName))
            {
                return beerName;
            }
            throw new Exception(string.Format("The beer name seems to be null or whitespace {0}", beerName));
        }

        public static string GetBeerByYeastName(string yeastName)
        {
            if (!string.IsNullOrWhiteSpace(yeastName))
            {
                return yeastName;
            }
            throw new Exception(string.Format("The beer name seems to be null or whitespace {0}", yeastName));
        }

        public static string GetBeerByMatchingFood(string foodName)
        {
            if (!string.IsNullOrWhiteSpace(foodName))
            {
                return foodName;
            }
            throw new Exception(string.Format("The food name seems to be null or whitespace {0}", foodName));
        }
    }
}
