
using RentalCar.DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Microsoft.Owin.Security.DataProtection;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;
using System.Threading;
using Nest;
using ServiceLayer;
using System.Linq;

namespace RentalCar
{
    public class EUserCustomTokenProvider : IUserTwoFactorTokenProvider<EUser>
    {
        private readonly Dictionary<string, IUserTwoFactorTokenProvider<EUser>> _tokenProviders =
     new Dictionary<string, IUserTwoFactorTokenProvider<EUser>>();


        public EUserCustomTokenProvider()
        {
            

        }

        /// <summary>
        ///     IDataProtector for the token
        /// </summary>

        /// <summary>
        ///     Lifespan after which the token is considered expired
        /// </summary>
       //public IDataProtector Protector { get; private set; }
        public TimeSpan TokenLifespan { get; set; }
        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<EUser> manager, EUser user)
        {
            return CanGenerateTwoFactorTokenAsync(manager, user);
        }
      
        private void ThrowIfDisposed()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<string> GenerateAsync(string purpose, UserManager<EUser> manager, EUser user)
        {
            var dataProtectionProvider = DataProtectionProvider.Create("StayfsA3456789032#@#@#@#");
            var Protector = dataProtectionProvider.CreateProtector("RentalCarTechS@$yu7890");
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var ms = new MemoryStream();
            var userId = await manager.GetUserIdAsync(user);
            //using (var writer = ms.CreateWriter())
            //{
            //    writer.Write(DateTimeOffset.UtcNow);
            //    writer.Write(userId);
            //    writer.Write(purpose ?? "");
            //    string stamp = null;
            //    if (manager.SupportsUserSecurityStamp)
            //    {
            //        stamp = await manager.GetSecurityStampAsync(user);
            //    }
            //    writer.Write(stamp ?? "");
            //}
            var protectedBytes = Protector.Protect(ms.ToArray());
            return Tools.Encryptbyte(ms.ToArray());
        }


        
        public virtual async Task<bool> ValidateAsync(string purpose, string token, UserManager<EUser> manager, EUser user)
        {
            try
            {

                var dataProtectionProvider = DataProtectionProvider.Create("StayfsA3456789032#@#@#@#");
                var Protector = dataProtectionProvider.CreateProtector("RentalCarTechS@$yu7890");
                TokenLifespan = TimeSpan.FromDays(1);
                var n = Convert.FromBase64String(token);

				//var unprotectedData = Protector.Unprotect(n);
                string d= Tools.GetDecryptedQueryString(token);
                var dec=Tools.GetDecryptedQueryByte(token);

                var ms = new MemoryStream(dec);
                //using (var reader = ms.CreateReader())
                //{
                //    var creationTime = reader.ReadDateTimeOffset();
                //    var expirationTime = creationTime + TokenLifespan;
                //    if (expirationTime < DateTimeOffset.UtcNow)
                //    {
                //        return false;
                //    }

                //    var userId = reader.ReadString();
                //    var actualUserId = await manager.GetUserIdAsync(user);
                //    if (userId != actualUserId)
                //    {
                //        return false;
                //    }
                //    var purp = reader.ReadString();
                //    if (!string.Equals(purp, purpose))
                //    {
                //        return false;
                //    }
                //    var stamp = reader.ReadString();
                //    if (reader.PeekChar() != -1)
                //    {
                //        return false;
                //    }

                //    if (manager.SupportsUserSecurityStamp)
                //    {
                //        return stamp == await manager.GetSecurityStampAsync(user);
                //    }
                //    return stamp == "";
                //}
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
                // Do not leak exception
            }
            return false;
        }
        
        // Implement any other necessary methods from the IUserTwoFactorTokenProvider<EUser> interface
        // For example, you might want to implement NotifyAsync and GetValidTwoFactorProvidersAsync if needed.
    }
 
}