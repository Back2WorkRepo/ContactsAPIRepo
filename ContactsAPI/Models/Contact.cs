﻿using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.Models
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }
        
        public DateTime DateOfBirth { get; set; }

    }
    }

