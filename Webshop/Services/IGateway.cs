using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webshop.ViewModels;

namespace Webshop.Services
{
    public interface IGateway
    {
        PaymentResult ProcessPayment(CheckoutViewModel model);
    }
}