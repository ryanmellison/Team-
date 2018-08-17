using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealOrNoDeal.Models
{
    public class Case
    {
        private int caseNumber;

        public int CaseNumber 
        {
            get { return caseNumber; }
            set { caseNumber = value; }
        }

        private double caseValue;

        public double CaseValue
        {
            get { return caseValue; }
            set { caseValue = value; }
        }


    }
}
