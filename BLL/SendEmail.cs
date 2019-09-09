using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SendEmail
    {
        //פונקציית שליחת מייל
        public static MailMessage SendEmail1(string text, string subject, string mailToSend)
        {
            try
            {
                string mailSend = "easytofind187@gmail.com";
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential("easytofind187@gmail.com", "sm035780129");// Enter senders User name and password
                smtp.EnableSsl = true;

                MailMessage mail = new MailMessage(mailSend, mailToSend, subject, text);
                //if (filePath != null)
                //{
                //    //http://localhost:62699/UploadFile/אפרת/ANTIQUE2.JPG
                //    var path = HttpContext.Current.Server.MapPath("~");


                //    path = path.Replace(@"\", @"/");
                //    path += filePath.Substring(23);

                //    //mail.Attachments.Add(new Attachment(path));
                //    mail.Attachments.Add(new Attachment(path));
                //}
                mail.BodyEncoding = UTF8Encoding.UTF8;
                //mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                Console.WriteLine(x);
            }
            return new MailMessage();
        }
    
    }
}
