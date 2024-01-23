namespace Order.API.ViewModels
{
    public class CreateOrderVM
    {
        public Guid BuyerID { get; set; }
        public List<CreateOrderItemVM> OrderItems { get; set; }
    }
}
