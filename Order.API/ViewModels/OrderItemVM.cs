namespace Order.API.ViewModels
{
    public class OrderItemVM
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
