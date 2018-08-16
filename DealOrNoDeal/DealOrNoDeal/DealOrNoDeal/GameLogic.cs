﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace DealOrNoDeal
{
    public class GameLogic
    {
        public static double[] Cases = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 };
        public static double[] Values = new Double[] { .01, 1, 5, 10, 25, 50, 75, 100, 200, 300, 400, 500, 750, 1000, 5000, 10000, 25000, 50000, 75000, 100000, 200000, 300000, 400000, 500000, 750000, 1000000 };
        public static Dictionary<double, double> CurrentCases = new Dictionary<double, double>();
        public static Dictionary<double, double> AllCases = new Dictionary<double, double>();
        static public Dictionary<double, string> Case = new Dictionary<double, string>();

        public static async Task SerializeAsync()
        {
            Case.Add(200, "200");
            Case.Add(15, "15");
            Case.Add(432, "432");
            Case.Add(230, "230");

            FileSavePicker picker = new FileSavePicker();
            picker.FileTypeChoices.Add("file style", new string[] { ".txt" });
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.SuggestedFileName = "test";
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await localFolder.CreateFileAsync(picker.SuggestedFileName, CreationCollisionOption.ReplaceExisting);

            using (Stream fs = await sampleFile.OpenStreamForWriteAsync())
            {
                Serializer.Serialize(fs, Case);
            }
        }


        public static void ProduceCases() //Go through each case and give it a Random Value
        {
            Random rnd = new Random();
            CurrentCases.Clear();
            AllCases.Clear();
            double[] MyRandomArray = Values.OrderBy(x => rnd.Next()).ToArray(); //Randomizes the Values[] into a new array
            for (int i = 0; i < Values.Count(); i++)
            {
                    CurrentCases.Add(Cases[i], MyRandomArray[i]); //adds the Case number as the Key, and the Values as the value
                    AllCases.Add(Cases[i], MyRandomArray[i]);
            }
        }
        public static double RevealCase(int caseNum) //returns the case value 
        {
            return AllCases[caseNum];
        }
        public static void PickUserCase(int caseNum) //takes the number of the case and adds it to a new Dictionary so it can be stored/ then removes the case from the currentCase list
        {
            Dictionary<double, double> userCase = new Dictionary<double, double>(); //makes a new Dictionary to hold the Case Number and the Value inside
            userCase.Add(caseNum, CurrentCases[caseNum]);
            RemoveOpenedCase(caseNum);
        }
        public static void RemoveOpenedCase(int caseNum) //get case that is selected to remove, 
        {
            CurrentCases.Remove(caseNum);
        }
        public static double BankerOffer()
        {
            double retVal;
            double numOfCases = CurrentCases.Count();
            double expectedValue = CurrentCases.Values.Average();
            retVal = 12275.30 + (.748 * expectedValue) + (-2714.74 * numOfCases) + (.0000006986 * (expectedValue * expectedValue)) + (32.623 * (numOfCases * numOfCases));
            return retVal;
        }
    }
}
