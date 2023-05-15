using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.Models
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }
        public string? Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Title { get; set; }

        public string? Address { get; set; }

        public string City { get; set; }

        public string? Region { get; set; }

        public string? PostalCode { get; set; }

        public string Country { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }
        
        public DateTime DateOfBirth { get; set; }

        //Can add phonetic pronounciation if needed for both first and last name

        //Can add nickname / name that reminds you specifically of the person
        //Can also add an image, other links to social media, their websites
        //Related person and relationship mapping
        
        public string? Middle_Name { get; set; }

        public string? Nick_Name { get; set; }

        public string? Work_Phone { get; set; }

        public string? Work_Email { get; set; }

        public string? Companies { get; set; } //Worked for and currently working in( can add department / experience and field ) currently working is only mapped now




    }
}

