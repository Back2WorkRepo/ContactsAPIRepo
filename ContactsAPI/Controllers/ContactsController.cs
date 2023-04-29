using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ExceptionServices;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(ILogger<ContactsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetContacts")]
        public IEnumerable<Contact> Get()
        {
            List<Contact> contacts= new List<Contact>();
            Contact contact= new Contact { First_Name = "First", Last_Name = "user"};
            contacts.Add(contact);
            //DateTime dob = new DateTime(1998, 10, 23);

            //Console.WriteLine(dob.ToString());
            //1998-10-23 00:00:00.0000000
            return contacts;
        }
        //[HttpGet(Name = "Getcontact")]
        //public Contact GetOne()
        //{
        //    //partial match support and incorrect match support as well
        //    Contact contact = new Contact { First_Name = "First", Last_Name = "user" };
           

           
        //    return contact;
        //}
    }
}
