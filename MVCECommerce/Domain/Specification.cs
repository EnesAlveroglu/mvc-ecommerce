namespace MVCECommerce.Domain
{
    public class Specification : _EntityBase // özellik sınıfı, EntityBase sınıfından miras alır.
    {
        // Scalar Properties = Direk veritabanına yazılacak özellikler
        public  string? NameTr { get; set; } // Türkçe isim
        public  string? NameEn { get; set; } // English name
        public Guid CategoryId { get; set; } // Kategori Id(Foreign Key)

        // Navigation Properties = bu kısmı Entity Framework kullanır. Scalar Properties veritabanına kaydederken Category? kaydedilmesin diye sonuna ? soru işareti koyduk bu kısım veritabanında ki ilişkiyi yönetir. 
        public Category? Category { get; set; } 
    }
}
