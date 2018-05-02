using EmailSenderProgram.Implementations;
using EmailSenderProgram.Interfaces;
using System;
using System.Net.Mail;

namespace EmailSenderProgram
{
    internal class Program
    {
        /// <summary>
        /// This application is run everyday
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            Console.WriteLine("Sending welcome mails.");
            bool success = SendMailBatch(new WelcomeMailBatch());
#if DEBUG
            //Debug mode, always send Comeback mail
            Console.WriteLine("Sending comeback mails.");
            success = success && SendMailBatch(new ComebackMailBatch(voucher: "ConsulenceComebackToUs"));
#else
			//Every Sunday send Comeback mail
			if (DateTime.Now.DayOfWeek.Equals(DayOfWeek.Sunday))
			{
				Console.WriteLine("Sending comeback mails.");
				success = success && SendMailBatch(new ComebackMailBatch(voucher: "ConsulenceComebackToUs"));
			}
#endif
            if (success)
            {
                Console.WriteLine("All mails sent successfully.");
            }
            else
            {
                Console.WriteLine("There were errors sending the mails.");
            }
                
            Console.ReadKey(true);
        }

        /// <summary>
        /// Send mail batch of the specified injected type.
        /// </summary>
        /// <returns>
        /// Returns true if all the mails were sent successfully.
        /// </returns>
        public static bool SendMailBatch(IMailBatch mailBatch)
        {
            var smtp = new SmtpClient("yoursmtphost");
            try
            {
                var receivingCustomers = mailBatch.GetReceivingCustomers();
                foreach (var customer in receivingCustomers)
                {
                    MailMessage message = mailBatch.GetCustomerPersonalizedMessage(customer);
#if DEBUG
                    //Debug mode, always send Comeback mail
                    Console.WriteLine($"Mail sent to customer with mail: {customer.Email}");
#else
			        smtp.Send(message);
#endif
                }
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Failed at sending mail batch of type '{mailBatch.MailType}', exception: {exception.Message}");
                return false;
            }
            finally
            {
                smtp.Dispose();
            }
        }
    }
}