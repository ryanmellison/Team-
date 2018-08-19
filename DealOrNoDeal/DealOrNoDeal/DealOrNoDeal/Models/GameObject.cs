using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealOrNoDeal.Models
{
    [ProtoContract]
    public class GameObject
    {
        private Dictionary<double, double> currentCases;
        [ProtoMember(1)]

        public Dictionary<double, double> CurrentCases
        {
            get { return currentCases; }
            set { currentCases = value; }
        }

        private Dictionary<double, double> allCases;
        [ProtoMember(2)]

        public Dictionary<double, double> AllCases
        { 
            get { return allCases; }
            set { allCases = value; }
        }

        private Case userCase;
        [ProtoMember(3)]

        public Case UserCase
        {
            get { return userCase; }
            set { userCase = value; }
        }

        private int turnCycle;
        [ProtoMember(4)]

        public int TurnCycle
        {
            get { return turnCycle; }
            set { turnCycle = value; }
        }


    }
}
