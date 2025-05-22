using Microsoft.EntityFrameworkCore;
using YardimMasasi.Models;

public static class DataSeeder
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, CategoryName = "Yazılım Hatası" },
            new Category { CategoryId = 2, CategoryName = "Donanım Sorunu" },
            new Category { CategoryId = 3, CategoryName = "Kullanıcı Erişimi" },
            new Category { CategoryId = 4, CategoryName = "Ağ Problemleri" }
        );

        modelBuilder.Entity<Priority>().HasData(
            new Priority { PriorityId = 1, PriorityName = "Düşük" },
            new Priority { PriorityId = 2, PriorityName = "Orta" },
            new Priority { PriorityId = 3, PriorityName = "Yüksek" },
            new Priority { PriorityId = 4, PriorityName = "Acil" }
        );
    }
}
