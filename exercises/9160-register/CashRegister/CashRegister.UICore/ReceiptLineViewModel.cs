namespace CashRegister.UICore
{
    public class ReceiptLineViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
