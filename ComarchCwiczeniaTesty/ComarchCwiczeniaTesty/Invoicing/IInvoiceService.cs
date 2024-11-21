using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchCwiczeniaTesty.Invoicing;

public interface IInvoiceService
{
    decimal CalculateTotal(decimal amount, string customerType);
}