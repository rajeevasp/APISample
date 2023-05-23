using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading;
using System.Configuration;
using System.Net;

namespace API.Utilities.Email
{
    public class EmailHelper
    {
        #region Events
        /// <summary>
        ///Occurs after an e-mail has been sent. The sender is the MailMessage object.
        /// </summary>
        public static event EventHandler<EventArgs> EmailFailed;

        /// <summary>
        ///Occurs after an e-mail has been sent. The sender is the MailMessage object.
        /// </summary>
        public static event EventHandler<EventArgs> EmailSent;

        /// <summary>
        ///Occurs when a message will be logged. The sender is a string containing the log message.
        /// </summary>
        public static event EventHandler<EventArgs> OnLog;
        #endregion

        /// <summary>
        /// Sends a MailMessage object using the SMTP settings.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// Error message, if any.
        /// </returns>
        public static string SendMailMessage(MailMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            StringBuilder errorMsg = new StringBuilder();
            try
            {
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;
                var smtp = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"]);
                // don't send credentials if a server doesn't require it,
                // linux smtp servers don't like that 
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SmtpUserName"]))
                {
                    smtp.Credentials = new NetworkCredential(
                    ConfigurationManager.AppSettings["SmtpUserName"], ConfigurationManager.AppSettings["SmtpPassword"]);
                }
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                smtp.Send(message);
                OnEmailSent(message);
            }
            catch (Exception ex)
            {
                OnEmailFailed(message);
                errorMsg.Append("Error sending email in SendMailMessage: ");
                Exception current = ex;
                while (current != null)
                {
                    if (errorMsg.Length > 0) { errorMsg.Append(" "); }
                    errorMsg.Append(current.Message);
                    current = current.InnerException;
                }
            }
            finally
            {
                // Remove the pointer to the message object so the GC can close the thread.
                message.Dispose();
            }
            return errorMsg.ToString();
        }

        /// <summary>
        /// The on email failed.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        private static void OnEmailFailed(MailMessage message)
        {
            if (EmailFailed != null)
            {
                EmailFailed(message, new EventArgs());
            }
        }

        /// <summary>
        /// Sends the mail message asynchronously in another thread.
        /// </summary>
        /// <param name="message">
        /// The message to send.
        /// </param>
        public static void SendMailMessageAsync(MailMessage message)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                SendMailMessage(message);
            });
        }

        /// <summary>
        /// The on email sent.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        private static void OnEmailSent(MailMessage message)
        {
            if (EmailSent != null)
            {
                EmailSent(message, new EventArgs());
            }
        }
    }
}
