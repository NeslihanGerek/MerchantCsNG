using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MerchantCsNG
{
    class Program
    {
        static CurrencyContainer currencyContainer = new CurrencyContainer();

        static void Main(string[] args)
        {
            try
            {
                using (StreamReader sr = new StreamReader("test.txt"))
                {
                    String line = sr.ReadLine();
                    while(null != line)
                    {
                        line = line.Remove(line.Length - 1); // remove the question mark
                        string[] inputStr = Regex.Split(line, " ");
                        List<string> IGUnits = new List<string>();
                        int inx = Array.FindIndex(inputStr, x => x == "is") + 1;
                        if(inx == 0)
                        {
                            Console.WriteLine("I have no idea what you are talking about");
                        }

                        else if(null != inputStr.FirstOrDefault(x => x == "Credits")) // looking for some calculations
                        {
                            string metal = inputStr[inputStr.Count() - 1];
                            
                            while (inx < inputStr.Count() - 1)
                            {
                                IGUnits.Add(inputStr[inx]);
                                inx++;
                            }
                            Double result = currencyContainer.CalculateCredit(IGUnits, metal);
                            inx = line.IndexOf(" is ") + 4;

                            Console.WriteLine(line.Substring(inx, line.Length - inx) + " is " + result + " Credits");
                        }
                        else
                        {
                            while (inx < inputStr.Count())
                            {
                                IGUnits.Add(inputStr[inx]);
                                inx++;
                            }
                            Double result = currencyContainer.GetDecimalValue(IGUnits);
                            inx = line.IndexOf(" is ") + 4;
                            if(result == 0)
                            {
                                Console.WriteLine("I have no idea what you are talking about");
                            }
                            Console.WriteLine(line.Substring(inx, line.Length - inx) + " is " + result);
                        }
                        line = sr.ReadLine();                    
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Please press Enter to finish the operation");
            Console.ReadLine();
        }
    }
}
