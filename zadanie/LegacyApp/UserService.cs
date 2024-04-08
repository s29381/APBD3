using System;
using System.Diagnostics;

namespace LegacyApp
{
    public interface ICreditLimitService
    {
        int GetCreditLimit(string lastName, DateTime birthdate);
    }
    
    public interface IClientRepository
    {
        Client GetById(int idClient);
    }
    
    public class UserService
    {
        public IClientRepository _clientRepository { get; set; }
        public ICreditLimitService _creditService { get; set; }

        public UserService(IClientRepository clientRepository,
            ICreditLimitService creditService)
        {
            _clientRepository = clientRepository;
            _creditService = creditService;
        }

        [Obsolete]
        public UserService()
        {
            _clientRepository = (IClientRepository)new ClientRepository();
            _creditService = (ICreditLimitService)new UserCreditService();
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            Validate validate = new Validate(firstName,lastName,email,dateOfBirth);
            if (validate.EmptyName())
            {
                return false;
            }

            if (validate.IncorrectEmail())
            {
                return false;
            }
            
            if (validate.ImproperAge())
            {
                return false;
            }
            
            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            validate.UserCreditLimit(user,client,this);

            if (validate.IsOverLimit(user))
            {
                return false;
            }

            SaveUser(user);
            return true;
        }
        
        private void SaveUser(User user)
        {
            UserDataAccess.AddUser(user);
        }
    }

    class Validate
    {
        private string FirstName;
        private string LastName;
        private string Email;
        private DateTime DateOfBirth;
        public Validate(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = lastName;
            DateOfBirth = dateOfBirth;
        }

        public bool EmptyName()
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
            {
                return true;
            }

            return false;
        }
        
        public bool IncorrectEmail()
        {
            if (!Email.Contains("@") && !Email.Contains("."))
            {
                return true;
            }

            return false;
        }
        
        public bool ImproperAge()
        {
            var now = DateTime.Now;
            int age = now.Year - DateOfBirth.Year;
            if (now.Month < DateOfBirth.Month || (now.Month == DateOfBirth.Month && now.Day < DateOfBirth.Day)) age--;

            if (age < 21)
            {
                return true;
            }

            return false;
        }

        public void UserCreditLimit(User user, Client client, UserService us)
        {
            ICreditLimitService _creditService = us._creditService;
            
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                int creditLimit = _creditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
            }
            else
            {
                user.HasCreditLimit = true;
                int creditLimit = _creditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }
        }

        public bool IsOverLimit(User user)
        {
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return true;
            }

            return false;
        }
    }
}
