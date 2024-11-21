namespace ComarchCwiczeniaTesty.Invoicing;

public class InvoiceService : IInvoiceService
{
    private readonly ITaxService taxService;
    private readonly IDiscountService discountService;

    public InvoiceService(ITaxService taxService, IDiscountService discountService)
    {
        this.taxService = taxService;
        this.discountService = discountService;
    }


    public decimal CalculateTotal(decimal amount, string customerType)
    {
        var discount = discountService.CalculateDiscount(amount, customerType);
        var taxableAmount = amount - discount;
        var tax = taxService.GetTax(taxableAmount);
        return taxableAmount + tax;
    }
}