using Core.Mail;
using Core.Services;
using DAL;
using DAL.Infrastructure;
using DAL.Repositories;
using DAL.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Core.Mail
{
    public class MailJob
    {
        private ISendEntity<Product> sendProduct;

        public MailJob(ISendEntity<Product> sendProduct)
        {
            this.sendProduct = sendProduct;
        }

        public static void Start()
        {
            while (true)
            {
                SendMail();

                System.Threading.Thread.Sleep(5 * 60 * 1000);
            }
        }

        public static void SendMail()
        {
            MailJob mj = new MailJob(new SendProduct(new ProductService(new ProductRepository(new UnitOfWork(new DbFactory())))));
            mj.SendMailNonStatic();
        }

        public void SendMailNonStatic()
        {
            IEnumerable<Product> newProducts = sendProduct.GetProductForSend();

            foreach (Product product in newProducts)
            {
                string tagsList = GetNameTagList(product.Tags);
                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress("username@gmail.com")); //replace with valid value
                message.Subject = "Details of the created or edited product";
                string body = "<p>Product Details:<p> Name : {0}</p> <p> Quantity : {1}</p>  <p> Category : {2}</p> <p> Tags : {3}</p> Price : {4} EUR</p>";
                message.Body = string.Format(body, product.Name, product.Quantity.ToString(), product.CategoryId, tagsList, product.Price.ToString());
                message.IsBodyHtml = true;

                try { 
                using (var smtp = new SmtpClient())
                {
                    if (product.Image != null && product.Image.Count() > 0)
                    {
                        using (MemoryStream ms = new System.IO.MemoryStream(product.Image))
                        {
                            ms.Write(product.Image, 0, product.Image.Length);
                            ms.Position = 0;
                            Attachment attachment = new Attachment(ms, "image.jpg", "image/jpeg");
                            message.Attachments.Add(attachment);
                            message.Attachments[0].ContentId = "image.jpg";
                            message.Attachments[0].ContentDisposition.Inline = true;
                            message.Attachments[0].ContentDisposition.FileName = "image.jpg";
                            message.Body += string.Format("Show Image in List of Products: {0}", YesNoShowInList(product.ShowInList));
                            message.Body += "<img src='cid:image'>" + Environment.NewLine;
                            smtp.Send(message);
                        }
                    }
                    else
                        smtp.Send(message);
                }
                product.IsSendByEmail = true;
                sendProduct.UpdateProduct(product);
                }
                catch(Exception ex)
                {
                    ExtensionLogger.Error("Error at sending  mail", ex);
                }
            }
        }

        private static string GetNameTagList(ICollection<Tag> Tags)
        {
            StringBuilder tagsString = new StringBuilder();
            foreach (Tag tag in Tags)
            {
                tagsString.Append(tag.Name);
                tagsString.Append(", ");
            }
            return tagsString.ToString().TrimEnd(' ', ',');
        }

        private static string YesNoShowInList(bool? ShowInList)
        {
            if (ShowInList == null) return "This value wasn't set";
            if (ShowInList == true) return "Yes";
            else return "No";
        }
    }
}
