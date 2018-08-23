using DealOrNoDeal.Models;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace DealOrNoDeal
{
    public class GameLogic
    {
        public static int[] Numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 };
        public static double[] Values = new Double[] { .01, 1, 5, 10, 25, 50, 75, 100, 200, 300, 400, 500, 750, 1000, 5000, 10000, 25000, 50000, 75000, 100000, 200000, 300000, 400000, 500000, 750000, 1000000 };
        public static Case[] cases = new Case[26];
        public static Case userCase = new Case();
        private StorageFile file;
        private string savePath;


        public static void ProduceCases() //Go through each case and give it a Random Value
        {
            Random rnd = new Random();
            
            double[] MyRandomArray = Values.OrderBy(x => rnd.Next()).ToArray(); //Randomizes the Values[] into a new array
            for (int i = 0; i < Values.Count(); i++)
            {
                cases[i] = new Case() { CaseNumber = Numbers[i], CaseValue = MyRandomArray[i], IsOpened = false }; //adds the Case number as the Key, and the Values as the value
            }
        }
        public static Case RevealCase(int caseNum) //returns the case value 
        {
            return cases[caseNum - 1];
        }
        public static void PickUserCase(int caseNum) //takes the number of the case and adds it to a new Dictionary so it can be stored/ then removes the case from the currentCase list
        {
            userCase = cases[caseNum - 1];
            OpenCase(caseNum);
        }
        public static void OpenCase(int caseNum) //get case that is selected to remove, 
        {
            Case c = cases[caseNum - 1];
            c.IsOpened = true;
        }
        public static double BankerOffer()
        {
            double retVal;
            double numOfCases = 0;
            double expectedValue = 0;
            foreach (Case c in cases)
            {
                if (c.IsOpened == false)
                {
                    expectedValue += c.CaseValue;
                    numOfCases++;
                }
            }
            expectedValue /= numOfCases;
            retVal = 12275.30 + (.748 * expectedValue) + (-2714.74 * numOfCases) + (.0000006986 * (expectedValue * expectedValue)) + (32.623 * (numOfCases * numOfCases));
            return retVal;
        }
    }
}

