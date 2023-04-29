using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI
{
    public class ContactsContext : DbContext
    {
        public ContactsContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=IN05N000R5\\SQLEXPRESS;Initial Catalog=ContactsDB;Integrated Security=true;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        DbSet<Contact> contacts_table { get; set; }



    }
}
