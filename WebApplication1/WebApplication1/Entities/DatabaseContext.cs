using Microsoft.EntityFrameworkCore;
using WebApplication1.Configuration;

namespace WebApplication1.Entities
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<ProjectRecommendEntity> ProjectRecommends { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Ogrenci>(new OgrenciConfiguration());
            modelBuilder.ApplyConfiguration<ProjectRecommendEntity>(new ProjectRecommendConfiguration());


            modelBuilder.Entity<Ogrenci>().HasData(

                new Ogrenci()
                {
                    Id = 1,
                    FullName = "Hasan",
                    StudentName = "hasan",
                    Password = "11111111",
                    CreatedAt = DateTime.Now,
                    Role = "admin",
                    ProjectRecommendId = null
                },
                new Ogrenci()
                {
                    Id = 2,
                    FullName = "mevlut",
                    StudentName = "mevlut",
                    Password = "11111111",
                    CreatedAt = DateTime.Now,
                    Role = "user",
                    ProjectRecommendId = null
                },
                new Ogrenci()
                {
                    Id = 3,
                    FullName = "ibrahim",
                    StudentName = "ibrahim",
                    Password = "11111111",
                    CreatedAt = DateTime.Now,
                    Role = "user",
                    ProjectRecommendId = null
                }
            );

            modelBuilder.Entity<ProjectRecommendEntity>().HasData(
                new ProjectRecommendEntity()
                {
                    Id =1,
                    Baslik = "İLAÇLARIN ETKEN MADDESİNİN KONTROLLÜ ELDESİ VE İYİLEŞTİRİCİ ETKİLERİNİN ARTTIRILMASI ÜZERİNE YENİ BİR YÖNTEM",
                    Ozet = "Nanokristal teknolojisi partikül boyutu 1000 nanometre (nm)’nin altında,herhangi bir taşıyıcı sisteme ihtiyaç duymadan katı ilaç partikülerinin üretilmesini sağlar. Sudaki çözünürlüğü düşük ilaçların partiküllerinin boyutunun küçültülmesi ile, yüzey alanlarının artması ve difüzyon tabakasının kalınlığının azaltılması k çözünürlük hızının da artışına yol açar. Buna bağlı olarak, absorpsiyon bölgesinde artan konsantrasyon gradienti bağırsak lümeni v kan arasındaki pasif difüzyon yoluyla permeasyonu ve emilimi teşvik etmektedir. Dolayısıyla Biyofarmasötik Sınıflandırma Sistemi (BCS) Sınıf II ve IV’e ait ilaç molekülleri için nanokristal teknolojisi yaklaşımını kullanarak biyoyararlanımlarını geliştirmek ve/veya arttırmak oldukça önemlidir. Nanometren boyutunda ilaç partikülü elde edebilmek için yukarıdan aşağıya (top-down) ve aşağıdan yukarıya (bottom-up) yöntemlerinden yararlanılmaktadır. İlaç endüstrisinde uygulama kolaylığı, tekrar edilebilirliği ve ölçeklendirilebilmesinedeniyle bilyeli değirmende yaş öğütme (BWM) ve yüksek basınçlı homojenizasyon (HPH) olarak alt bölümlere ayrılan yukarıdan aşağıya yöntemleri tercih edilmektedir. Nanokristal teknolojisi ile ilaç endüstrisinde hâlihazırda tedavide onaylanmış olan ilaç moleküllerinin daha az yan etki, daha düşük dozlar ve daha hızlı etki başlangıcı sağlayarak yeni dozaj formlarının geliştirilmesi ve yeni ilaç moleküllerinin daha iyi bir biyoyararlanımla formüle edilebilmesi amaçlanmaktadır.",
                    AnahtarKelimeler = "Nanokristal teknolojisi, Suda çözünür ilaçlar, Oral yol, Hazırlama yöntemleri",
                    Yil = "2022",
                    SimilarityOnayDurumu = true,
                    SimilarityScore = 45,
                    OgrenciId = 2,
                    OgrenciName = "mevlut",
                    OgretmenOnayDurumu = true,
                    TeacherComment = ""
                },
                new ProjectRecommendEntity()
                {
                    Id= 2,
                    Baslik = "Bakteriyel Selülozların Üretimi ve Özellikleri ile Gıda ve Gıda Dışı Uygulamalarda Kullanımı",
                    Ozet = "Selüloz D-glukopiranoz birimlerinin β-1,4 glikozidik bağlarla bağlanmasıyla oluşan lineer ve dünyada en yaygın olarak bulunan polimerdir. Selüloz, bitkilerin yanında bazı bakteriler tarafından da üretilmektedir. Bakteriyel selüloz olarakadlandırılan bu tip selülozlar gıda, ilaç, biyoteknoloji, biyomedikal, kozmetik, kağıt ve elektronik alanlarında kullanımı giderek artmaktadır. Saf olarak elde edilmesi, elastik, ağsı yapıda, yüksek kristalizasyon derecesi, yüzey alanı, su tutma kapasitesine ve gerilme direncine, daha ince ve gözenekli bir yapıya sahip olması gibi bitkisel selüloza kıyasla pek çok üstün özellikleri bulunmaktadır. Bu derleme bakteriyel selülozun üretimini, üretiminde kullanılan yöntemleri, nüretilen polimerin özelliklerini ve gıda ve gıda dışı uygulamalarda kullanımını kapsamaktadır.",
                    AnahtarKelimeler = "Bakteriyel selüloz, Acetobacter, Ekzopolisakkarit, Fermantasyon",
                    Yil = "2022",
                    SimilarityOnayDurumu = true,
                    SimilarityScore = 50,
                    OgrenciId = 3,
                    OgrenciName = "ibrahim",
                    OgretmenOnayDurumu = true,
                    TeacherComment = ""
                },
                new ProjectRecommendEntity()
                {
                    Id= 3,
                    Baslik = "Yumurta Tavukçuluğunda Kanatlı Kırmızı Akarı (Dermanyssus gallinae) Problemi ve Mücadele",
                    Ozet = "Kanatlı kırmızı akarı (Dermanyssus gallinae) dünyanın birçok ülkesinde yumurta tavukçuluğu endüstrisine büyük ekonomik zararlar veren bir dış parazittir. Kan ile beslenen bu parazit tavuğun sağlığını, refahını ve verim performansını olumsuz etkiler. Kanatlı kırmızı akarı ile mücadelede en popüler metot çeşitli sentetik akarisitlerin kullanılmasıdır. Ancak, kimyasal uygulamalar, akar direnci, aktif bileşenlerin etkisizliği ve ürünlerde ve çevrede zararlı artıklar gibi çok sayıda soruna yol açabilmektedir. Bu nedenle, kanatlı kırmızı akarı ile mücadelede çevreye ve insan sağlığına daha az zarar verebilecek yeni alternatif kaynakların belirlenmesi giderek önem kazanmaktadır. Organik gıda üretiminde kullanılan bitkisel ürünler kimyasal akarisitlere alternatif olabilir. Bu derlemede, kanatlı kırmızı akarların özellikleri, sentetik akarisitlerin neden olduğu sorunlar ve bitkisel ürünlerin akarisitler olarak kullanım potansiyelleri irdelenmiştir.",
                    AnahtarKelimeler = "Tavuk, kanatlı kırmızı akarı, sentetik akarisitler, bitkisel ürünler",
                    Yil = "2022",
                    SimilarityOnayDurumu = true,
                    SimilarityScore = 41,
                    OgrenciId = 2,
                    OgrenciName = "mevlut",
                    OgretmenOnayDurumu = true,
                    TeacherComment = ""
                }
            );

        }
    }
}
