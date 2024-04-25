namespace MEDIQUICK.BL
{
    public class User
    {
        string firstName;
        string lastName;
        string email;
        string password;
        string phoneNumber;
        bool isAdmin;
        bool isActive;

        public User(string firstName, string lastName, string email, string password, string phoneNumber, bool isAdmin, bool isActive)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            IsAdmin = isAdmin;
            IsActive = isActive;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
    }
}
