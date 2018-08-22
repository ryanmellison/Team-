using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealOrNoDeal.Models
{
    [ProtoContract(SkipConstructor = true)]
    public class GameObject
    {
        [ProtoMember(1)]
        private Dictionary<double, double> currentCases;

        public Dictionary<double, double> CurrentCases
        {
            get { return currentCases; }
            set { currentCases = value; }
        }
        //public string GetCurrentCases(double key)
        //{
        //    if (currentCases.TryGetValue(key, out double value))
        //    {
        //        return value.ToString();
        //    }
        //    else
        //    {
        //        return "Does Not Exist";
        //    }
        //}

        //public void SetCurrentCases(double key, double value)
        //{
        //    currentCases.Add(key, value);
        //}
        [ProtoMember(2)]
        private Dictionary<double, double> allCases;

        public Dictionary<double, double> AllCases
        { 
            get { return allCases; }
            set { allCases = value; }
        }
        [ProtoMember(3)]
        private Case userCase;

        public Case UserCase
        {
            get { return userCase; }
            set { userCase = value; }
        }
        [ProtoMember(4)]
        private int turnCycle;

        public int TurnCycle
        {
            get { return turnCycle; }
            set { turnCycle = value; }
        }


    }
}
