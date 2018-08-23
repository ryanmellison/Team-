using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DealOrNoDeal.Models
{
    [DataContract]
    public class GameObject
    {
        
        [DataMember]
        private Case[] cases;

        public Case[] Cases
        {
            get { return cases; }
            set { cases = value; }
        }

       
        [DataMember]
        private Case userCase;

        public Case UserCase
        {
            get { return userCase; }
            set { userCase = value; }
        }

        [DataMember]
        private int turnCycle;

        public int TurnCycle
        {
            get { return turnCycle; }
            set { turnCycle = value; }
        }


    }
}
