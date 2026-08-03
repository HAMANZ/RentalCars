using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;
using  Microsoft.Owin.Security.DataProtection;
using System.Configuration.Provider;
using RentalCar.DomainLayer.Models;
using System.Resources;

namespace RentalCar
{

    public class EUserManager : UserManager<EUser>

    {
        private readonly Dictionary<string, IUserTwoFactorTokenProvider<EUser>> _tokenProviders =
     new Dictionary<string, IUserTwoFactorTokenProvider<EUser>>();
        EUserCustomTokenProvider EUserCustomTokenProvider;

        public EUserManager(EUserCustomTokenProvider _EUserCustomTokenProvider, IUserStore<EUser> store, IOptions<IdentityOptions> optionsAccessor,
               IPasswordHasher<EUser> passwordHasher, IEnumerable<IUserValidator<EUser>> userValidators,
               IEnumerable<IPasswordValidator<EUser>> passwordValidators, ILookupNormalizer keyNormalizer,
               IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<EUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

            this.EUserCustomTokenProvider = _EUserCustomTokenProvider;

            RegisterTokenProvidern(TokenOptions.DefaultProvider, new AuthenticatorTokenProvider<EUser>());
        }

        public virtual Task<string> GeneratePasswordResetTokenAsyncN(EUser user)
        {
            ThrowIfDisposed();
            return GenerateUserTokenAsyncs(user, Options.Tokens.PasswordResetTokenProvider, ResetPasswordTokenPurpose);
        }
        public virtual Task<string> CustomChangePhoneNumberTokenNAsync(EUser user, string phoneNumber)
        {
            ThrowIfDisposed();
            return GenerateUserTokenNAsync(user, TokenOptions.DefaultProvider, ChangePhoneNumberTokenPurpose + ":" + phoneNumber);
        }
        public virtual Task<string> GenerateEmailConfirmationTokenNAsync(EUser user, string phoneNumber)
        {
            ThrowIfDisposed();
            return GenerateUserTokenNAsync(user, Options.Tokens.EmailConfirmationTokenProvider, ConfirmEmailTokenPurpose);
        }
 
        public virtual async Task<IdentityResult> ConfirmEmailNAsync(EUser user, string token)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
          //  await VerifyUserTokenNAsync(user, Options.Tokens.EmailConfirmationTokenProvider, ConfirmEmailTokenPurpose, token);
            if (!await VerifyUserTokenNAsync(user, Options.Tokens.EmailConfirmationTokenProvider, ConfirmEmailTokenPurpose, token))
            {
                return IdentityResult.Failed(ErrorDescriber.InvalidToken());
            }
            user.EmailConfirmed = true;
            return await UpdateUserAsync(user);
        }

        public virtual Task<string> GenerateUserTokenAsyncs(EUser user, string tokenProvider, string purpose)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (tokenProvider == null)
            {
                throw new ArgumentNullException(nameof(tokenProvider));
            }
            if (!_tokenProviders.ContainsKey(tokenProvider))
            {
                //throw new NotSupportedException(Resources.FormatNoTokenProvider(nameof(ServiceProvidern), tokenProvider));
            }

            return EUserCustomTokenProvider.GenerateAsync(purpose, this, user);
        }
        public virtual void RegisterTokenProvidern(string providerName, IUserTwoFactorTokenProvider<EUser> provider)
        {
            ThrowIfDisposed();
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            _tokenProviders[providerName] = provider;
        }
        public virtual async Task<IdentityResult> ResetPasswordNAsync(EUser user, string token, string newPassword)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Make sure the token is valid and the stamp matches
            //if (!await VerifyUserTokenNAsync(user, TokenOptions.DefaultProvider, TokenOptions.DefaultProvider, token))
            //{
            //    return IdentityResult.Failed(ErrorDescriber.InvalidToken());
            //}
            var result = await UpdatePasswordHash(user, newPassword, validatePassword: true);
            if (!result.Succeeded)
            {
                return result;
            }
            return await UpdateUserAsync(user);
        }

        public virtual async Task<bool> VerifyUserTokenNAsync(EUser user, string tokenProvider, string purpose, string token)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (tokenProvider == null)
            {
                throw new ArgumentNullException(nameof(tokenProvider));
            }

            if (!_tokenProviders.ContainsKey(tokenProvider))
            {
                //Resources.FormatNoTokenProvider(nameof(EUser), tokenProvider)
                throw new NotSupportedException();
            }
            // Make sure the token is valid
            var result = await EUserCustomTokenProvider.ValidateAsync(purpose, token, this, user);

            if (!result)
            {
                Logger.LogWarning(9, "VerifyUserTokenAsync() failed with purpose: {purpose} for user.", purpose);
            }
            return result;
        }
     
        public virtual Task<string> GenerateUserTokenNAsync(EUser user, string tokenProvider, string purpose)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (tokenProvider == null)
            {
                throw new ArgumentNullException(nameof(tokenProvider));
            }
            if (!_tokenProviders.ContainsKey(tokenProvider))
            {
                //throw new NotSupportedException(Resources.FormatNoTokenProvider(nameof(EUser), tokenProvider));
                throw new NotSupportedException();
            }

            return EUserCustomTokenProvider.GenerateAsync(purpose, this, user);
        }



    }
}