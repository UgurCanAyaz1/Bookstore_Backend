using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_Backend.Models
{
    public class PaymentProcessRequest
{
    public string PaymentMethodId { get; set; }
    public long Amount { get; set; }  
    }
}

