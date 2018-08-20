using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealOrNoDeal.Models
{
    [ProtoContract]
    public class Case
    {
        private int caseNumber;
        [ProtoMember(1)]
        public int CaseNumber 
        {
            get { return caseNumber; }
            set { caseNumber = value; }
        }

        private double caseValue;
        [ProtoMember(2)]

        public double CaseValue
        {
            get { return caseValue; }
            set { caseValue = value; }
        }


    }
}
