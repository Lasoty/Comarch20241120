namespace ComarchCwiczeniaTesty.Invoicing;

public interface ITaxService
{
    decimal GetTax(decimal amount);
}