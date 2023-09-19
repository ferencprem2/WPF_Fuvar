using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Fuvar
{
    internal class Fuvar
    {
        private int taxiId;
        private DateTime startingTime;
        private int fullTimeOfTheRiseInSec;
        private double fullTripDistance;
        private double price;
        private double tip;
        private string paymentType;

        public Fuvar(int taxiId, DateTime startingTime, int fullTimeOfTheRiseInSec, double fullTripDistance, double price, double tip, string paymentType)
        {
            this.taxiId = taxiId;
            this.startingTime = startingTime;
            this.fullTimeOfTheRiseInSec = fullTimeOfTheRiseInSec;
            this.fullTripDistance = fullTripDistance;
            this.price = price;
            this.tip = tip;
            this.paymentType = paymentType;
        }

        public int TaxiId { get => taxiId; }
        public DateTime StartingTime { get => startingTime;} 
        public int FullTimeOfTheRiseInSec { get => fullTimeOfTheRiseInSec; }
        public double FullTripDistance { get => fullTripDistance;}
        public double Price { get => price;}
        public double Tip { get => tip;}
        public string PaymentType { get => paymentType;}    
    }
}
