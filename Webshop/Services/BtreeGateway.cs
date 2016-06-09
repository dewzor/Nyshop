using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Braintree;
using Webshop.ViewModels;

namespace Webshop.Services
{
    public class BtreeGateway : IGateway
    {
        private readonly BraintreeGateway _gateway = new BraintreeGateway
        {
            Environment = Braintree.Environment.SANDBOX,
            MerchantId = "yf7rxf7r5qsbpf53",
            PublicKey = "w9kxfc59hgkf4k7m",
            PrivateKey = "b258e44303b1c25926ae1b4ef42517bf"
        };

        public PaymentResult ProcessPayment(CheckoutViewModel model)
        { 
            var request = new TransactionRequest()
            {
                Amount = model.Total,
                CreditCard = new TransactionCreditCardRequest()
                {
                    Number = model.CardNumber,
                    CVV = model.Cvv,
                    ExpirationMonth = model.Month,
                    ExpirationYear = model.Year
                },
                Options = new TransactionOptionsRequest()
                {
                    SubmitForSettlement = true
                }
            };

            var result = _gateway.Transaction.Sale(request);
            if (result.IsSuccess())
            {
                return new PaymentResult(result.Target.Id, true, null);
            }

            return new PaymentResult(null, false, result.Message);
        }
    }
}