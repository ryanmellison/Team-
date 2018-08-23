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
    public class Case
    {
        [DataMember]
        private int caseNumber;
        public int CaseNumber 
        {
            get { return caseNumber; }
            set { caseNumber = value; }
        }

        [DataMember]
        private double caseValue;

        public double CaseValue
        {
            get { return caseValue; }
            set { caseValue = value; }
        }

        [DataMember]
        private bool isOpened;

        public bool IsOpened
        {
            get { return isOpened; }
            set { isOpened = value; }
        }

    }
}
