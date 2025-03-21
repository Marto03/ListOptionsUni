using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class PaymentMethodDTO
    {
        public string Name { get; set; }
        public bool IsSystemDefined { get; set; }

        public static readonly List<PaymentMethodDTO> DefaultPaymentMethods = new List<PaymentMethodDTO>
        {
            new PaymentMethodDTO { Name = "Visa", IsSystemDefined = true },
            new PaymentMethodDTO { Name = "PayPal", IsSystemDefined = true }
        };
    }
    //public class PaymentMethodDTO
    //{
    //    public string Name { get; set; }
    //    public bool IsSystemDefined { get; set; }

    //    public static readonly List<PaymentMethodDTO> DefaultPaymentMethods = new List<PaymentMethodDTO>
    //    {
    //        new PaymentMethodDTO { Name = "Visa", IsSystemDefined = true },
    //        new PaymentMethodDTO { Name = "PayPal", IsSystemDefined = true }
    //    };

    //    public static List<PaymentMethodDTO> GetDefaultPaymentMethods()
    //    {
    //        return DefaultPaymentMethods;
    //    }
    //}
}
