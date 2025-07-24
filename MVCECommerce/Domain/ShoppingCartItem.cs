namespace MVCECommerce.Domain;

public class ShoppingCartItem // Sepetteki ürünler sınıfı
{
    public Guid Id { get; set; } // Benzersiz Id
    public Guid UserId { get; set; } // hangi kullanıcıya ait bir sepet
    public Guid ProductId { get; set; } // Ürün Id (Foreign Key)
    public int Quantity { get; set; } // Sepetteki ürün adedi
    public User? User { get; set; } // hangi kullanıcıya ait olduğunu gösteren navigation property
    public Product? Product { get; set; } // Sepetteki ürünnü tutatn navigation property

}
