using System.Data.Entity;

namespace PhoneBook.Model
{
    public class PersonContext : DbContext
    {
        public PersonContext() : base("DbConnection")
        { }

        public virtual DbSet<Person> Persons { get; set; }
    }
}