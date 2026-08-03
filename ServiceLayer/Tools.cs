using CorePush.Apple;
using RentalCar.DomainLayer.Model;
using RentalCar.ServiceLayer.Interface;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Http;
using Nest;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using RentalCar.DomainLayer.LookUpObjects;

namespace ServiceLayer
{
    public static class Tools
    {
        public enum LoggerActions { Read, Update, Delete, Create };
        private static Random rng = new Random();


        private static string Key = "Es$nad1%23Tak54#";
        private static byte[] GetByte(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }
        public static List<string> EverythingBetween(this string source, string start, string end)
        {
            var results = new List<string>();

            string pattern = string.Format(
                "{0}({1}){2}",
                Regex.Escape(start),
                ".+?",
                 Regex.Escape(end));

            foreach (Match m in Regex.Matches(source, pattern))
            {
                results.Add(m.Groups[1].Value);
            }

            return results;
        }
        public static byte[] EncryptString(string data)
        {
            byte[] byteData = GetByte(data);
            Aes algo = Aes.Create();
            algo.Key = GetByte(Key);
            algo.GenerateIV();

            MemoryStream mStream = new MemoryStream();
            mStream.Write(algo.IV, 0, algo.IV.Length);

            CryptoStream myCrypto = new CryptoStream(mStream, algo.CreateEncryptor(), CryptoStreamMode.Write);
            myCrypto.Write(byteData, 0, byteData.Length);
            myCrypto.FlushFinalBlock();

            return mStream.ToArray();
        }
        
        public static string Encryptbyte(byte[] byteData)
        {
            Aes algo = Aes.Create();
            algo.Key = GetByte(Key);
            algo.GenerateIV();

            MemoryStream mStream = new MemoryStream();
            mStream.Write(algo.IV, 0, algo.IV.Length);

            CryptoStream myCrypto = new CryptoStream(mStream, algo.CreateEncryptor(), CryptoStreamMode.Write);
            myCrypto.Write(byteData, 0, byteData.Length);
            myCrypto.FlushFinalBlock();

            return Convert.ToBase64String(mStream.ToArray());
        }


		public static byte[] DecryptByte(byte[] data)
		{
			Aes algo = Aes.Create();
			algo.Key = GetByte(Key);
			MemoryStream mStream = new MemoryStream();

			byte[] byteData = new byte[algo.IV.Length];
			Array.Copy(data, byteData, byteData.Length);
			algo.IV = byteData;
			int readFrom = 0;
			readFrom += algo.IV.Length;

			CryptoStream myCrypto = new CryptoStream(mStream, algo.CreateDecryptor(), CryptoStreamMode.Write);
			myCrypto.Write(data, readFrom, data.Length - readFrom);
			myCrypto.FlushFinalBlock();

            return mStream.ToArray();
		}
		public static string DecryptString(byte[] data)
        {
            Aes algo = Aes.Create();
            algo.Key = GetByte(Key);
            MemoryStream mStream = new MemoryStream();

            byte[] byteData = new byte[algo.IV.Length];
            Array.Copy(data, byteData, byteData.Length);
            algo.IV = byteData;
            int readFrom = 0;
            readFrom += algo.IV.Length;

            CryptoStream myCrypto = new CryptoStream(mStream, algo.CreateDecryptor(), CryptoStreamMode.Write);
            myCrypto.Write(data, readFrom, data.Length - readFrom);
            myCrypto.FlushFinalBlock();

            return Encoding.UTF8.GetString(mStream.ToArray());
        }

        public static string GetEncryptedQueryString(string data)
        {
            return Convert.ToBase64String(EncryptString(data));
        }

		public static byte[] GetDecryptedQueryByte(string data)
		{
			byte[] byteData = Convert.FromBase64String(data.Replace(" ", "+"));
			return DecryptByte(byteData);
		}
		public static string GetDecryptedQueryString(string data)
        {
            byte[] byteData = Convert.FromBase64String(data.Replace(" ", "+"));
            return DecryptString(byteData);
        }
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static string UploadImage(IFormFile file)
        {
            string pah = "";
            string filename = "";
            try
            {
                if (file != null)
                {

                    var extension = new StringBuilder(".").Append(file.FileName.Split(".")[file.FileName.Split(".").Length - 1]);
                    filename = new StringBuilder(Guid.NewGuid().ToString()).Append(extension).ToString();
                    pah = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images" , filename);
                    using (var s = new FileStream(pah, FileMode.Create))
                    {
                        file.CopyTo(s);
                    }

                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return filename;
        }



        public static string UploadImage(IFormFile file, string ImagePath)
		{
			string pah = "";
			string filename = "";
			try
			{
				if (file != null)
				{

					var extension = new StringBuilder(".").Append(file.FileName.Split(".")[file.FileName.Split(".").Length - 1]);
					filename = new StringBuilder(Guid.NewGuid().ToString()).Append(extension).ToString();
					pah = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\" + ImagePath, filename);
					using (var s = new FileStream(pah, FileMode.Create))
					{
						file.CopyTo(s);
					}

				}
			}
			catch (Exception ex)
			{
				return ex.Message;
			}

			return filename;
		}

		//public static string Smsnew(string phone)
		//{

		//	try
		//	{

		//		string customerId = "E078159E-B916-4C3C-8B3A-84AEBF2E4DUaKeltmdUBiYiMy3qDBmJKYcoiimCFRaOu3c1lw92N9kqrLa9AAniWEFwDqqg3LYFllahTjuv6xlMsPHkV5Imw==BF";
		//		string apiKey = "UaKeltmdUBiYiMy3qDBmJKYcoiimCFRaOu3c1lw92N9kqrLa9AAniWEFwDqqg3LYFllahTjuv6xlMsPHkV5Imw==";
		//		string result = phone;
		//		string phoneNumber = result;
		//		Random random = new Random();
		//		int num = random.Next(100000);
		//		string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

		//		string verifyCode = GenerateRandomOTP(6, saAllowedCharacters);
		//		//  string verifyCode = GenerateRandomOTP().ToString();

		//		string message = "Your verification code is : " + verifyCode + "";
		//		string messageType = "ARN";


		//		MessagingClient messagingClient = new MessagingClient(customerId, apiKey);
		//		RestClient.TelesignResponse telesignResponse = messagingClient.Message(phoneNumber, message, messageType);

		//		return verifyCode;


		//	}
		//	catch (Exception e)
		//	{
		//		throw;
		//	}
		//}

		public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        static public void WriteToFile(string Message, string path = "")
        {

            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Logs"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Logs");
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "Logs\\ResponseLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {

                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }


        static public string BeautifyLogs(string email, string appId, string status, string errorMessage = "")
        {
            try
            {
                string result = "";
                if (email == "Email")
                {
                    result = String.Format("{0,-60} | {1,-5} | {2,-10} | {3,-25} | {4,-150}", email, appId, status, "Date", errorMessage);

                }
                else
                {
                    result = String.Format("{0,-60} | {1,-5} | {2,-10} | {3,-25} | {4,-150}", email, appId, status, DateTime.Now, errorMessage);

                }
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static void sendEmail(string emailTo, string subject, string body, AppSettings appSettings)
        {
            try
            {
               
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(appSettings.Email,"RentalCar");
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("RentalCarquraninst@gmail.com", "thtyjzwtbfauboof");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        
        public static async Task sendEmail(string emailTo, string subject, string body)
        {
            try
            {
                using (var mail = new MailMessage())
                {

                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress("RentalCarquraninst@gmail.com", "RentalCar");
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("RentalCarquraninst@gmail.com", "thtyjzwtbfauboof");
                        smtp.EnableSsl = true;
                        smtp.SendAsync(mail,null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }


        public static  Task<string> sendNotification(string Title,string Body,string registrationToken)
        {

            Task<string> response = null;

            try
            {
               var message = new Message()
                {
                    //Data = new Dictionary<string, string>()
                    //{
                    //{ "myData", "1337" },
                    //},
                    Token = registrationToken,
                    //Topic = "all",
                    Notification = new FirebaseAdmin.Messaging.Notification()
                    {
                        Title = Title,
                        Body = Body
                    }
                };


                // Send a message to the device corresponding to the provided
                // registration token.
                response = FirebaseMessaging.DefaultInstance.SendAsync(message);
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception when calling DefaultApi.Notification: " + e.Message);
                Console.WriteLine("Status Code: " + e.Message);
                Console.WriteLine(e.StackTrace);
                return response;
            }
        }





        static public List<LookUpAttributes> GetAttributes(string ClassName, string XMLPath)
        {
            List<LookUpAttributes> attributes = new List<LookUpAttributes>();
            LookUpAttributes attr = new LookUpAttributes();
            XmlDocument doc = new XmlDocument();


            try
            {
                doc.Load(XMLPath);
                XmlNode Lookups = doc.ChildNodes[1];

                foreach (XmlNode child in Lookups.ChildNodes)
                {

                    if (child.Attributes["Name"].Value == ClassName)
                    {
                        foreach (XmlNode node in child)
                        {
                            attr = new LookUpAttributes();
                            attr.Name = node.Attributes["Name"].Value;
                            attr.Code = node.Attributes["Code"].Value;
                            if (node.Attributes["isLangNull"].Value == "False")
                                attr.isLangNull = false;
                            else
                                attr.isLangNull = true;

                            if (node.Attributes["isMedia"].Value == "False")
                                attr.isMedia = false;
                            else
                                attr.isMedia = true;

                            if (node.Attributes["isMain"].Value == "False")
                                attr.isMain = false;
                            else
                                attr.isMain = true;

                            if (node.Attributes["isList"].Value == "False")
                                attr.isList = false;
                            else
                                attr.isList = true;

                            if (node.Attributes["isVideo"].Value == "False")
                                attr.isVideo = false;
                            else
                                attr.isVideo = true;


                            attributes.Add(attr);
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return attributes;
        }

        static public String getUUID()
        {
            return System.Guid.NewGuid().ToString();
        }

        static public String getUTCDateTime()
        {
            DateTime time = DateTime.Now.ToUniversalTime();
            return time.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
        }
    }
}
