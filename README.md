# Yardım Masası Uygulaması (Backend)

Bu proje, bir yardım masası (destek sistemi) uygulamasının backend kısmını içermektedir. Kullanıcıların destek taleplerini oluşturmasına, görüntülemesine ve destek ekiplerinin bu talepleri yönetmesine olanak sağlar.

## Teknolojiler

- ASP.NET Core Web API
- PostgreSQL
- Entity Framework Core
- Katmanlı Mimari
- Stored Procedure kullanımı
- JWT Authentication

## Kurulum

1. **Gereksinimler:**
   - .NET 6 veya üzeri
   - PostgreSQL
   - Visual Studio 2022 veya VS Code

2. **Projeyi Klonlayın:**

   ```bash
   git clone https://github.com/didaryilmaz/YardimMasasi_Backend.git
   cd YardimMasasi_Backend
   ```

3. **Veritabanı Ayarları:**

   `appsettings.json` dosyasında `ConnectionStrings` kısmını kendi PostgreSQL bilgilerinizle güncelleyin:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=yardim_masasi;Username=postgres;Password=sifre"
   }
   ```

4. **Veritabanını Oluşturun:**

   - Migration varsa:
     ```bash
     dotnet ef database update
     ```
   - Manuel kurulum gerekiyorsa, `Scripts/` klasöründeki SQL dosyalarını çalıştırın.

5. **Projeyi Çalıştırın:**

   ```bash
   dotnet run
   ```

   API, varsayılan olarak `http://localhost:5000` adresinden erişilebilir olacaktır.

## Temel Özellikler

- Ticket oluşturma, görüntüleme, filtreleme
- Kategori ve öncelik bazlı raporlama
- Admin paneli ile kategori/öncelik yönetimi
- Destek personeline otomatik ticket atama
- JWT ile kullanıcı yetkilendirme (user, destek, admin rolleri)

## Örnek API Endpoint'leri

| Yöntem | URL | Açıklama |
|--------|-----|----------|
| GET | `/api/tickets` | Tüm ticket'ları getir |
| POST | `/api/tickets` | Yeni ticket oluştur |
| GET | `/api/reports/category-frequency` | Kategori sıklık raporu |
| GET | `/api/tickets/user/{userId}` | Belirli kullanıcıya ait ticket'lar |
| GET | `/api/tickets/support/{supportId}` | Destek personeline atanmış ticket'lar |

## Lisans

Bu proje [MIT Lisansı](LICENSE) ile lisanslanmıştır.

---

> Yardım Masası uygulaması, kurumsal destek süreçlerini dijitalleştirmek ve yönetilebilir hale getirmek için geliştirilmiştir.
