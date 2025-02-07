using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.DAL.Context
{
    public class MyPortfolioContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public MyPortfolioContext(DbContextOptions<MyPortfolioContext> options) : base(options)
        {
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<Image> Images { get; set; }

        // Sifrelerin hash'lenerek tutulabilmesi icin bu yapi olusturuluyor
        public string CreatePasswordHash(AppUser user, string password)
        {
            var passwordHasher = new PasswordHasher<AppUser>();
            return passwordHasher.HashPassword(user, password);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // User seed data
            var admin = new AppUser
            {
                Id = Guid.Parse("612430B6-0B01-40E7-9957-BC699E3050CB"),
                Name = "Kübra",
                Surname = "Yalçın",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "kubrayalcin.272@gmail.com",
                NormalizedEmail = "KUBRAYALCİN.272@GMAİL.COM",
                PhoneNumber = "(546) 111 9999",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            admin.PasswordHash = CreatePasswordHash(admin, "987654");
            modelBuilder.Entity<AppUser>().HasData(admin);
            // Role seed data
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole
                {
                    Id = Guid.Parse("3B09E0C6-C435-43EB-BF31-28683852EE31"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                });
            // About seed data
            modelBuilder.Entity<About>().HasData(
                new About
                {
                    AboutId = 1,
                    Title = "Ben Kimim",
                    SubDescription = "Hakkımda Daha Fazlası",
                    Details = "Osmaniye Korkut Ata Üniversitesi Bilgisayar Mühendisliği 3. sınıf öğrencisiyim. Araştırmayı, yeni bilgiler öğrenmeyi " +
                            "ve her geçen gün kendimi geliştirmeyi seviyorum. Azimli, hırslı, ve sorumluluk sahibi biri olmanın yanı sıra takım çalışmasına da yatkınım. " +
                            "Şu an aktif olarak ASP.NET Core MVC, SignalR, Web Api, " +
                            "gibi web alanlarında yazılım geliştiriyor, aynı zamanda yeni şeyler " +
                            "öğreniyorum. "
                });
            // Feature seed data
            modelBuilder.Entity<Feature>().HasData(
                new Feature
                {
                    FeatureId = 1,
                    Title = "Merhaba",
                    Description = "Ben Kübra. Osmaniye Korkut Ata Üniversitesi Bilgisayar Mühendisliği 3. sınıf öğrencisiyim."
                });
            // Contact seed data
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    ContactId = 1,
                    Title = "Bana Ulaşın",
                    Description = "Merak ettiğiniz her konu hakkında bana mesaj atabilirsiniz.",
                    Phone = "(546) 111 9999",
                    Email = "kubrayalcin.272@gmail.com",
                    Address = "Gaziantep, Türkiye"
                });
        }
    }

}
