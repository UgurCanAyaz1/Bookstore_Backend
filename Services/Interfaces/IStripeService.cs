using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe;

namespace Bookstore_Backend.Services.Interfaces
{
    public interface IStripeService
    {
        public Task<PaymentIntent> ProcessPaymentAsync(string paymentMethodId, long amount, string stripeAccountId = null);
        
    }
}