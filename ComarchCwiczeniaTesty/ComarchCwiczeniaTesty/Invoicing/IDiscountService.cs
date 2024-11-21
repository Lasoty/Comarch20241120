namespace ComarchCwiczeniaTesty.Invoicing;

public interface IDiscountService
{
    decimal CalculateDiscount(decimal amount, string customerType);
}