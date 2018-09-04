using System.Net;
using System.Net.Mail;
using System.Text;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Concrete
{

    public class EmailSettings
    {
        public string MailToAddress = "2714803874@qq.com";
        public string MailFromAddress = "1980933521@qq.com";
        public bool UseSsl = true;
        public string Username = "1980933521";
        public string Password = "fucefarotesubfid";
        public string ServerName = "smtp.qq.com";
        public int ServerPort = 25;
        public bool WriteAsFile = true;
        //public string FileLocation = @"c:\sports_store_emails";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username,
                          emailSettings.Password);
                //if (emailSettings.WriteAsFile)
                //{
                //    smtpClient.DeliveryMethod
                //        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                //    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                //    smtpClient.EnableSsl = false;
                //}
                StringBuilder body = new StringBuilder()
                    .AppendLine("您的订单已提交")
                    .AppendLine("---")
                    .AppendLine("物品:");
                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (小计: {2:c})",line.Product.Name ,line.Quantity,subtotal);
                    body.AppendLine("\n");
                }

                body.AppendFormat("总计: {0:c}", cart.ComputeTotalValue()).Append("\n")
                    .AppendLine("---")
                    .AppendLine("地址为:")
                    .Append(shippingInfo.Country)
                    .Append(shippingInfo.City)
                    .Append(shippingInfo.State ?? "")
                    .Append(shippingInfo.Zip)
                    .Append(shippingInfo.Line1)
                    .Append(shippingInfo.Line2 ?? "")
                    .AppendLine("收件人：")
                    .Append(shippingInfo.Name)
                    .AppendLine("联系方式：")
                    .Append(shippingInfo.Line3 ?? "")
                    .AppendLine("---")
                    .AppendFormat("是否包邮: {0}",
                        shippingInfo.GiftWrap ? "包邮" : "不包邮")
                    .AppendLine("感谢您的支持！欢迎您下次光临！");

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,   // From
                                       emailSettings.MailToAddress,    // To
                                       "您的订单已成功提交!",        //Subject
                                       body.ToString());             // Body

                //if (emailSettings.WriteAsFile)
                //{
                //    mailMessage.BodyEncoding = Encoding.ASCII;
                //}
                smtpClient.Send(mailMessage);
            }
        }
    }
}
