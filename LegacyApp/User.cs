using System;
using LegacyApp.Enum;
using LegacyApp.Interfaces;

namespace LegacyApp
{
    public class User
    {
        public Client Client { get; internal set; }
        public DateTime DateOfBirth { get; internal set; }
        public string EmailAddress { get; internal set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public bool HasCreditLimit { get; internal set; }
        public int CreditLimit { get; internal set; }

        public User(Client client, DateTime dateOfBirth, string emailAddress, string firstName, string lastName)
        {
            Client = client;
            DateOfBirth = dateOfBirth;
            EmailAddress = emailAddress;
            FirstName = firstName;
            LastName = lastName;
        }


        public void SetClientCreditLimit(ICreditService creditService)
        {
            if (Client.ClientType == ClientType.VeryImportantClient)
            {
                HasCreditLimit = false;
            }
            else if (Client.ClientType == ClientType.ImportantClient)
            {
                using (var userCreditService = creditService)
                {
                    int creditLimit = userCreditService.GetCreditLimit(LastName, DateOfBirth);
                    creditLimit = creditLimit * 2;
                    CreditLimit = creditLimit;
                }
            }
            else
            {
                HasCreditLimit = true;
                using (var userCreditService = creditService)
                {
                    int creditLimit = userCreditService.GetCreditLimit(LastName, DateOfBirth);
                    CreditLimit = creditLimit;
                }
            }   
        }
    }
    
}