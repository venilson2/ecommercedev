namespace Ecommercedev.Source.Core.Entites
{
    public class AddressEntity
    {
        public AddressEntity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Street { get; set; }
        public Guid ClientId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
