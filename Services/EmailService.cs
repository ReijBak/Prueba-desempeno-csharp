using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Prueba_desempeno_csharp.Services
{
    public class EmailService
    {
        // These fields could be loaded from appsettings.json for better security
        private readonly string _fromEmail = "stevencardona2001@gmail.com";
        private readonly string _fromName = "Saint Vicent Hospital System";
        private readonly string _appPassword = "abkscsacvkswbvny";

        /// <summary>
        /// Sends a confirmation email to a patient when an appointment is created.
        /// </summary>
        public void SendAppointmentEmail(string toEmail, string patientName, DateTime appointmentDate, string medicName)
        {
            // Create the email message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_fromName, _fromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = "Appointment Confirmation";

            // Email body (plain text)
            message.Body = new TextPart("plain")
            {
                Text = $"Hello {patientName},\n\n" +
                       $"Your medical appointment has been successfully scheduled for {appointmentDate:dddd, MMMM dd, yyyy 'at' HH:mm}.\n" +
                       $"Assigned doctor: Dr. {medicName}.\n\n" +
                       $"Thank you for choosing Saint Vicent Hospital."
            };

            // Send the message using Gmail SMTP
            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate(_fromEmail, _appPassword);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}