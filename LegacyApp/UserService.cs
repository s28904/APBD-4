using System;
using LegacyApp.Enum;
using LegacyApp.Interfaces;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICreditService _creditService;
        private readonly IAddUserAdapter _addUserAdapter;

        
        public UserService(IClientRepository clientRepository, ICreditService creditService, IAddUserAdapter addUserAdapter)
        {
            _clientRepository = clientRepository;
            _creditService = creditService;
            _addUserAdapter = addUserAdapter;
        }

        [Obsolete]
        public UserService()
        {
            _clientRepository = new ClientRepository();
            _creditService = new UserCreditService();
            _addUserAdapter = new UserDataAccessAdapter();
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!IsFirstnameCorrect(firstName) || !IsLastnameCorrect(lastName))
            {
                return false;
            }

            if (!IsEmailCorrect(email))
            {
                return false;
            }

            if (!IsAgeCorrect(dateOfBirth)) return false;
            
            

           var client = GetClientById(clientId, _clientRepository);
           var user = CreateUser(client,firstName, lastName, email, dateOfBirth);
           user.SetClientCreditLimit(_creditService);

            if (!DoesUserQualify(user)) return false;
            
            _addUserAdapter.AddUser(user);
            
            return true;
        }

        private static Client GetClientById(int clientId, IClientRepository clientRepository)
        {
            var client = clientRepository.GetById(clientId);
            return client;
        }

        private static User CreateUser(Client client,string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            var user = new User(client, dateOfBirth, email, firstName, lastName);
            return user;
        }

        private static bool DoesUserQualify(User user)
        {
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            return true;
        }
        

        private static bool IsAgeCorrect(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }

            return true;
        }

        private static bool IsEmailCorrect(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        private static bool IsLastnameCorrect(string lastName)
        {
            return !string.IsNullOrEmpty(lastName);
        }

        private static bool IsFirstnameCorrect(string firstName)
        {
            return !string.IsNullOrEmpty(firstName);
        }
    }
}
