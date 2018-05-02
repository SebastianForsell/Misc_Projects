using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;


namespace EmailSenderProgram.Interfaces
{
    interface IMailBatch
    {
        string MailType { get; }
        List<Customer> GetReceivingCustomers();
        MailMessage GetCustomerPersonalizedMessage(Customer customer);
    }
}
