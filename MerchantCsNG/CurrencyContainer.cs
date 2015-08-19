using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MerchantCsNG
{
    public class CurrencyContainer
    {
        public Dictionary<string, Double> currencyDict = new Dictionary<string, Double>();
        public Dictionary<string, Double> valueDict = new Dictionary<string, Double>();
        public Dictionary<string, Int32> conversionDict = new Dictionary<string, Int32>()
        {
            {"I", 1},
            {"V", 5},
            {"X", 10},
            {"L", 50},
            {"C", 100},
            {"D", 500},
            {"M", 1000}
        };

        public CurrencyContainer()
        {
            try
            {
                using (StreamReader sr = new StreamReader("input.txt"))
                {
                    String line = sr.ReadToEnd();
                    string[] currencies = Regex.Split(line, "\r\n");
                    foreach(string currency in currencies)
                    {
                        string[] inputStr = Regex.Split(currency, " ");
                        if(inputStr.Count() == 3) // definition of a currency
                        {
                            currencyDict.Add(inputStr[0], conversionDict.FirstOrDefault(x => x.Key == inputStr[2]).Value);
                        }
                        else // input for value of the metal
                        {
                            Double val = 0;
                            int counter = 0;
                            List<string> tempRomanConversion = new List<string>();
                            while (currencyDict.ContainsKey(inputStr[counter]))
                            {
                                tempRomanConversion.Add(inputStr[counter]);
                                counter++;
                            }

                            val = GetDecimalValue(tempRomanConversion);
                            // hit the metal
                            valueDict.Add(inputStr[counter], Convert.ToDouble(inputStr[counter + 2])/val);
                        }
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public Double GetDecimalValue(List<string> conversionInput)
        {
            Double val = 0;
            for (int i = 0; i < conversionInput.Count; i++)
            {
                Double tempVal = currencyDict.FirstOrDefault(x => x.Key == conversionInput[i]).Value;
                if (tempVal == 5 || tempVal == 50 || tempVal == 500)
                {
                    if (i + 1 == conversionInput.Count || (i + 1 < conversionInput.Count && tempVal != currencyDict.FirstOrDefault(x => x.Key == conversionInput[i + 1]).Value))
                    {
                        val += tempVal;
                    }
                    else
                    {
                        Console.WriteLine("Invalid combination");
                        break;
                    }
                }
                else if (i + 1 < conversionInput.Count && tempVal < currencyDict.FirstOrDefault(x => x.Key == conversionInput[i + 1]).Value)
                {
                    val -= tempVal;
                }
                else
                {
                    val += tempVal;
                }
            }

            return val;
        }

        public Double CalculateCredit(List<string> IGUnits, string metal)
        {
            Double val = GetDecimalValue(IGUnits);
            val = val * valueDict.FirstOrDefault(x => x.Key == metal).Value;
            return val;

        }

    }
}
