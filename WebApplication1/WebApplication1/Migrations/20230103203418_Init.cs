using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectRecommendEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ozet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnahtarKelimeler = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Yil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SimilarityOnayDurumu = table.Column<bool>(type: "bit", nullable: true),
                    OgretmenOnayDurumu = table.Column<bool>(type: "bit", nullable: true),
                    SimilarityScore = table.Column<int>(type: "int", nullable: false),
                    OgrenciId = table.Column<int>(type: "int", nullable: true),
                    OgrenciName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherComment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRecommendEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ogrenciler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    StudentName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProjectRecommendId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ogrenciler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ogrenciler_ProjectRecommendEntity_ProjectRecommendId",
                        column: x => x.ProjectRecommendId,
                        principalTable: "ProjectRecommendEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Ogrenciler",
                columns: new[] { "Id", "CreatedAt", "FullName", "Password", "ProjectRecommendId", "Role", "StudentName" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 3, 23, 34, 17, 931, DateTimeKind.Local).AddTicks(6596), "Hasan", "11111111", null, "admin", "hasan" },
                    { 2, new DateTime(2023, 1, 3, 23, 34, 17, 931, DateTimeKind.Local).AddTicks(6599), "mevlut", "11111111", null, "user", "mevlut" },
                    { 3, new DateTime(2023, 1, 3, 23, 34, 17, 931, DateTimeKind.Local).AddTicks(6602), "ibrahim", "11111111", null, "user", "ibrahim" }
                });

            migrationBuilder.InsertData(
                table: "ProjectRecommendEntity",
                columns: new[] { "Id", "AnahtarKelimeler", "Baslik", "OgrenciId", "OgrenciName", "OgretmenOnayDurumu", "Ozet", "SimilarityOnayDurumu", "SimilarityScore", "TeacherComment", "Yil" },
                values: new object[,]
                {
                    { 1, "Nanokristal teknolojisi, Suda çözünür ilaçlar, Oral yol, Hazırlama yöntemleri", "İLAÇLARIN ETKEN MADDESİNİN KONTROLLÜ ELDESİ VE İYİLEŞTİRİCİ ETKİLERİNİN ARTTIRILMASI ÜZERİNE YENİ BİR YÖNTEM", 2, "mevlut", true, "Nanokristal teknolojisi partikül boyutu 1000 nanometre (nm)’nin altında,herhangi bir taşıyıcı sisteme ihtiyaç duymadan katı ilaç partikülerinin üretilmesini sağlar. Sudaki çözünürlüğü düşük ilaçların partiküllerinin boyutunun küçültülmesi ile, yüzey alanlarının artması ve difüzyon tabakasının kalınlığının azaltılması k çözünürlük hızının da artışına yol açar. Buna bağlı olarak, absorpsiyon bölgesinde artan konsantrasyon gradienti bağırsak lümeni v kan arasındaki pasif difüzyon yoluyla permeasyonu ve emilimi teşvik etmektedir. Dolayısıyla Biyofarmasötik Sınıflandırma Sistemi (BCS) Sınıf II ve IV’e ait ilaç molekülleri için nanokristal teknolojisi yaklaşımını kullanarak biyoyararlanımlarını geliştirmek ve/veya arttırmak oldukça önemlidir. Nanometren boyutunda ilaç partikülü elde edebilmek için yukarıdan aşağıya (top-down) ve aşağıdan yukarıya (bottom-up) yöntemlerinden yararlanılmaktadır. İlaç endüstrisinde uygulama kolaylığı, tekrar edilebilirliği ve ölçeklendirilebilmesinedeniyle bilyeli değirmende yaş öğütme (BWM) ve yüksek basınçlı homojenizasyon (HPH) olarak alt bölümlere ayrılan yukarıdan aşağıya yöntemleri tercih edilmektedir. Nanokristal teknolojisi ile ilaç endüstrisinde hâlihazırda tedavide onaylanmış olan ilaç moleküllerinin daha az yan etki, daha düşük dozlar ve daha hızlı etki başlangıcı sağlayarak yeni dozaj formlarının geliştirilmesi ve yeni ilaç moleküllerinin daha iyi bir biyoyararlanımla formüle edilebilmesi amaçlanmaktadır.", true, 45, "", "2022" },
                    { 2, "Bakteriyel selüloz, Acetobacter, Ekzopolisakkarit, Fermantasyon", "Bakteriyel Selülozların Üretimi ve Özellikleri ile Gıda ve Gıda Dışı Uygulamalarda Kullanımı", 3, "ibrahim", true, "Selüloz D-glukopiranoz birimlerinin β-1,4 glikozidik bağlarla bağlanmasıyla oluşan lineer ve dünyada en yaygın olarak bulunan polimerdir. Selüloz, bitkilerin yanında bazı bakteriler tarafından da üretilmektedir. Bakteriyel selüloz olarakadlandırılan bu tip selülozlar gıda, ilaç, biyoteknoloji, biyomedikal, kozmetik, kağıt ve elektronik alanlarında kullanımı giderek artmaktadır. Saf olarak elde edilmesi, elastik, ağsı yapıda, yüksek kristalizasyon derecesi, yüzey alanı, su tutma kapasitesine ve gerilme direncine, daha ince ve gözenekli bir yapıya sahip olması gibi bitkisel selüloza kıyasla pek çok üstün özellikleri bulunmaktadır. Bu derleme bakteriyel selülozun üretimini, üretiminde kullanılan yöntemleri, nüretilen polimerin özelliklerini ve gıda ve gıda dışı uygulamalarda kullanımını kapsamaktadır.", true, 50, "", "2022" },
                    { 3, "Tavuk, kanatlı kırmızı akarı, sentetik akarisitler, bitkisel ürünler", "Yumurta Tavukçuluğunda Kanatlı Kırmızı Akarı (Dermanyssus gallinae) Problemi ve Mücadele", 2, "mevlut", true, "Kanatlı kırmızı akarı (Dermanyssus gallinae) dünyanın birçok ülkesinde yumurta tavukçuluğu endüstrisine büyük ekonomik zararlar veren bir dış parazittir. Kan ile beslenen bu parazit tavuğun sağlığını, refahını ve verim performansını olumsuz etkiler. Kanatlı kırmızı akarı ile mücadelede en popüler metot çeşitli sentetik akarisitlerin kullanılmasıdır. Ancak, kimyasal uygulamalar, akar direnci, aktif bileşenlerin etkisizliği ve ürünlerde ve çevrede zararlı artıklar gibi çok sayıda soruna yol açabilmektedir. Bu nedenle, kanatlı kırmızı akarı ile mücadelede çevreye ve insan sağlığına daha az zarar verebilecek yeni alternatif kaynakların belirlenmesi giderek önem kazanmaktadır. Organik gıda üretiminde kullanılan bitkisel ürünler kimyasal akarisitlere alternatif olabilir. Bu derlemede, kanatlı kırmızı akarların özellikleri, sentetik akarisitlerin neden olduğu sorunlar ve bitkisel ürünlerin akarisitler olarak kullanım potansiyelleri irdelenmiştir.", true, 41, "", "2022" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ogrenciler_ProjectRecommendId",
                table: "Ogrenciler",
                column: "ProjectRecommendId",
                unique: true,
                filter: "[ProjectRecommendId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ogrenciler");

            migrationBuilder.DropTable(
                name: "ProjectRecommendEntity");
        }
    }
}
