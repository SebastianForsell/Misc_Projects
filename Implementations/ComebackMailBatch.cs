using EmailSenderProgram.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace EmailSenderProgram.Implementations
{
    class ComebackMailBatch : IMailBatch
    {
        private string voucher;

        public ComebackMailBatch(string voucher)
        {
            this.voucher = voucher;
        }

        public string MailType
        {
            get
            {
                return "Comeback mail";
            }
        }

        public List<Customer> GetReceivingCustomers()
        {
            return DataLayer.ListCustomers().Where(customer => !DataLayer.ListOrders().Any(order => customer.Email == order.CustomerEmail)).ToList();
        }

        public MailMessage GetCustomerPersonalizedMessage(Customer customer)
        {
            MailMessage message = new MailMessage()
            {
                From = new MailAddress("info@consulence.com"),
                Subject = "We miss you as a customer",
                Body = "Hi " + customer.Email +
                     "<br>We miss you as a customer. Our shop is filled with nice products. Here is a voucher that gives you 50 kr to shop for." +
                     "<br>Voucher: " + voucher +
                     "<br><br>Best Regards,<br>Consulence Team",
            };
            message.To.Add(customer.Email);

            return message;
        }


    }
}
