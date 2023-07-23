using Aspose.Email;
using Aspose.Email.Clients;
using Aspose.Email.Clients.Smtp;
using System.Diagnostics;

namespace HardwareStoreMng.Helper
{
    public static class Helper
    {

       public static bool SendVerificationEmailToEmployee(string email, string code)
        {
            bool respons = false;
            MailMessage message = new MailMessage();

            
            message.Subject = "Hardwar Store Mng Email Verficiation Code";
            message.Body = "Use this Following Code  \n " + code + "\nto Confirm Your Opertaion Kindly Remindrer it's valid for 10 minutes since now";
            message.To.Add(new MailAddress(email, "Dear Employee", false));
            message.From = new MailAddress("Hardware Store Mng ", "Seccurity Team", false); 
            SmtpClient client = new SmtpClient();

            client.Host = "smtp.office365.com";
            client.Username = "stegen_1990@outlook.com";
            client.Password = "kaisarmajali1990";
            client.Port = 587;
            client.SecurityOptions = SecurityOptions.SSLExplicit;
            try
            {
                
                client.Send(message);
                respons=true;
                return respons; 
            }
            catch (Exception ex)
            {
                respons = false;
                Trace.WriteLine(ex.ToString());
                return respons;

            }



        }
    }
}
