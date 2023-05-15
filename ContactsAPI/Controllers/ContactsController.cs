using Azure.Messaging.ServiceBus;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Runtime.ExceptionServices;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly ContactsContext _context;

        public ContactsController(ILogger<ContactsController> logger,ContactsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetContacts")]
        public IActionResult GetAll()
        {
            var response = _context.contacts_table.ToList<Contact>();
            //DateTime dob = new DateTime(1998, 10, 23);
            //Console.WriteLine(dob.ToString());
            //1998-10-23 00:00:00.0000000
            return Ok(response);
        }
        //[HttpGet(Name = "Getcontact")]
        //public Contact GetOne()
        //{
        //    //partial match support and incorrect match support as well
        //    Contact contact = new Contact { First_Name = "First", Last_Name = "user" };



        //    return contact;
        //}
        [HttpPut]
        public IActionResult Put(Contact contact)
        {
            Contact foundresult= _context.contacts_table.FirstOrDefault(x=>x.Id== contact.Id);
            if (foundresult==null)
                return NotFound();
            Contact findresult = foundresult;
            findresult.PhoneNumber = (contact.PhoneNumber!="string")?foundresult.PhoneNumber:"string";
            findresult.Email = (contact.Email != "string") ? foundresult.Email : "string";
            findresult.Address = (contact.Address != "string") ? foundresult.Address : "string";
            findresult.City = (contact.City != "string") ? foundresult.City : "string";
            findresult.Region = (contact.Region != "string") ? foundresult.Region : "string";
            findresult.PostalCode = (contact.PostalCode != "string") ? foundresult.PostalCode : "string";
            findresult.Country = (contact.Country != "string") ? foundresult.Country : "string";
            findresult.Work_Email = (contact.Work_Email != "string") ? foundresult.Work_Email : "string";
            findresult.Work_Phone = (contact.Work_Phone != "string") ? foundresult.Work_Phone : "string";
            //findresult.DateOfBirth = (contact.DateOfBirth. != "string") ? foundresult.DateOfBirth : "string";
            findresult.Companies = (contact.Companies != "string") ? foundresult.Companies : "string";
            findresult.Title = contact.Title;
            findresult.Nick_Name = contact.Nick_Name;
            findresult.Last_Name = contact.Last_Name;
            findresult.First_Name= contact.First_Name;
            findresult.Middle_Name= contact.Middle_Name;
            _context.contacts_table.Remove(foundresult);
            _context.SaveChanges();
            _context.contacts_table.Add(findresult);
            _context.SaveChanges();
            // make changes in findresult from contact and return Updated or so
            return Ok();
        }
       
        [HttpPost]

        public IActionResult Create(Contact contact)
        {
            //validation
            contact.Id = Guid.NewGuid();
            _context.contacts_table.Add(contact);
            _context.SaveChanges();
            if(contact.Middle_Name != "string") 
                SendMessage(contact.First_Name+" "+contact.Middle_Name+" "+contact.Last_Name+" "+"Created"+" "+"Birthday"+contact.DateOfBirth);
            else
                SendMessage(contact.First_Name + " " + contact.Last_Name + " " + "Created" + " " + "Birthday" + contact.DateOfBirth);
            return Ok(contact);      //to change to Created()
        }
        //result = _context.contacts.FirstOrDefault(x => x.Phone == id);
        //    if(result == null)
        //        result = _context.contacts.FirstOrDefault(x => x.Email == id);
        //    if(result==null)
        //        result = _context.contacts.FirstOrDefault(x=>x.FirstName==id);
        //    if (result == null)
        //        result = _context.contacts.FirstOrDefault(x=>x.LastName==id);
        //    if (result == null)
        //        result = _context.contacts.FirstOrDefault(x=>x.Companies==id);
        //    if (result == null)
        //        result = new Contact();
        [ApiExplorerSettings(IgnoreApi = true)]
        public async void SendMessage(string message)
        {
            // the client that owns the connection and can be used to create senders and receivers
            ServiceBusClient client;

            // the sender used to publish messages to the queue
            ServiceBusSender sender;

            // number of messages to be sent to the queue
            const int numOfMessages = 1;

            // The Service Bus client types are safe to cache and use as a singleton for the lifetime
            // of the application, which is best practice when messages are being published or read
            // regularly.
            //
            // set the transport type to AmqpWebSockets so that the ServiceBusClient uses the port 443. 
            // If you use the default AmqpTcp, you will need to make sure that the ports 5671 and 5672 are open

            // TODO: Replace the <NAMESPACE-CONNECTION-STRING> and <QUEUE-NAME> placeholders
            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            client = new ServiceBusClient("Endpoint=sb://eventcreator.servicebus.windows.net/;SharedAccessKeyName=Everything;SharedAccessKey=zq6hEr83khFKhl4UU9BF93V+5qJjyKNur+ASbAdg92Y=;EntityPath=createevent", clientOptions);
            sender = client.CreateSender("createevent");

            // create a batch 
            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            for (int i = 1; i <= numOfMessages; i++)
            {
                // try adding a message to the batch
                if (!messageBatch.TryAddMessage(new ServiceBusMessage(message)))
                {
                    // if it is too large for the batch
                    throw new Exception($"The message {i} is too large to fit in the batch.");
                }
            }

            try
            {
                // Use the producer client to send the batch of messages to the Service Bus queue
                await sender.SendMessagesAsync(messageBatch);
                Console.WriteLine($"A batch of {numOfMessages} messages has been published to the queue.");
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }

    }

}

