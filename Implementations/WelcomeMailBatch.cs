using EmailSenderProgram.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace EmailSenderProgram.Implementations
{
    class WelcomeMailBatch : IMailBatch
    {
        public string MailType
        {
            get
            {
                return "Welcome mail";
            }
        }

        public List<Customer> GetReceivingCustomers()
        {
            return DataLayer.ListCustomers().Where(customer => customer.CreatedDateTime > DateTime.Now.AddDays(-1)).ToList();
        }

        public MailMessage GetCustomerPersonalizedMessage(Customer customer)
        {
            MailMessage message = new MailMessage()
            {
                From = new MailAddress("info@consulence.com"),
                Subject = "Welcome as a new customer at Consulence!",
                Body = "Hi " + customer.Email +
                    "<br>We would like to welcome you as customer on our site!<br><br>Best Regards,<br>Consulence Team"
            };
            message.To.Add(customer.Email);

            return message;
        }

    }
}
