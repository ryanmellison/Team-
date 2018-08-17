using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealOrNoDeal.Models
{
    public class GameObject
    {
        private Dictionary<double, double> currentCases;

        public Dictionary<double, double> CurrentCases
        {
            get { return currentCases; }
            set { currentCases = value; }
        }

        private Dictionary<double, double> allCases;

        public Dictionary<double, double> AllCases
        { 
            get { return allCases; }
            set { allCases = value; }
        }

        private double[] userCase;

        public double[] UserCase
        {
            get { return userCase; }
            set { userCase = value; }
        }

        private int turnCycle;

        public int TurnCycle
        {
            get { return turnCycle; }
            set { turnCycle = value; }
        }


    }
}
