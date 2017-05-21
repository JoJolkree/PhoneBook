using System.Reflection;

namespace PhoneBook.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Person() { }

        public Person(string lastName = "", string firstName = "", string phoneNumber = "", string email = "")
        {
            LastName = lastName;
            FirstName = firstName;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public void CopyInfoFromOtherPerson(Person other)
        {
            //LastName = other.LastName;
            //FirstName = other.FirstName;
            //PhoneNumber = other.PhoneNumber;
            //Email = other.Email;

            foreach (var property in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.Name == "Id") continue;
                property.SetValue(this, property.GetValue(other));
            }
        }
    }
}
