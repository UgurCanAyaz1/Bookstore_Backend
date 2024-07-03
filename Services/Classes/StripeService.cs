using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookstore_Backend.Models;
using Bookstore_Backend.Services.Interfaces;
using Microsoft.Extensions.Options;
using Stripe;

namespace Bookstore_Backend.Services.Classes
{
    public class StripeService : IStripeService
    {
        private readonly StripeSettings _stripeSettings;

        public StripeService(IOptions<StripeSettings> stripeOptions)
        {
            _stripeSettings = stripeOptions.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }

        public async Task<PaymentIntent> ProcessPaymentAsync(string paymentMethodId, long amount, string stripeAccountId = null)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = amount,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" },
                PaymentMethod = paymentMethodId,
                Confirm = true
            };
            var service = new PaymentIntentService();
            var requestOptions = new RequestOptions();

            if (!string.IsNullOrEmpty(stripeAccountId))
            {
                requestOptions.StripeAccount = stripeAccountId;
            }

            var paymentIntent = await service.CreateAsync(options, requestOptions);
            return paymentIntent;
        }
    }
}
