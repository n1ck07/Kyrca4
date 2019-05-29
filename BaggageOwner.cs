using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baggage_FlightDB
{    
    class BaggageOwnerInfo
    {
        /*Переменные для Владельца багажа*/
        string name;
        string surName;
        string lastName;
        uint flightNumber;
        string baggageTicket;
        uint amountOfBaggage;
        int baggageWeight;


        public BaggageOwnerInfo()
        {
        }

        public BaggageOwnerInfo(string name, string surName, string lastName, uint flightNumber, string baggageTicket, uint amountOfBaggage, int baggageWeight)
        {
            /*Ключевое слово this служил для уточнения того, что мы должны использовать , в данному случае указывая на переменную которая
            *будет применять значения с переданного конструктора*/
            this.Name = name;
            this.SurName = surName;
            this.LastName = lastName;
            this.FlightNumber = flightNumber;
            this.BaggageTicket = baggageTicket;
            this.AmountOfBaggage = amountOfBaggage;
            this.BaggageWeight = baggageWeight;
        }

        /*Свойства которые принимают значения и записывают в переменные*/
        public string Name { get => name; set => name = value; }
        public string SurName { get => surName; set => surName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public uint FlightNumber { get => flightNumber; set => flightNumber = value; }
        public string BaggageTicket { get => baggageTicket; set => baggageTicket = value; }
        public uint AmountOfBaggage { get => amountOfBaggage; set => amountOfBaggage = value; }
        public int BaggageWeight { get => baggageWeight; set => baggageWeight = value; }

    }
}
