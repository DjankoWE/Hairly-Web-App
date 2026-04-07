using Hairly.Data.Models;
using Hairly.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hairly.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Client>()
                .HasOne(c => c.Hairdresser)
                .WithMany()
                .HasForeignKey(c => c.HairdresserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Client>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Service>()
                .HasOne(s => s.Hairdresser)
                .WithMany()
                .HasForeignKey(s => s.HairdresserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Appointment>()
                .HasOne(a => a.Hairdresser)
                .WithMany()
                .HasForeignKey(a => a.HairdresserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Appointment>()
                .HasOne(a => a.Client)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Appointment>()
                .HasOne(a => a.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Review>()
                .HasOne(r => r.Client)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Review>()
                .HasOne(r => r.Appointment)
                .WithMany(a => a.Reviews)
                .HasForeignKey(r => r.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Client>()
                .HasQueryFilter(c => !c.IsDeleted);

            builder.Entity<Service>()
                .HasQueryFilter(s => !s.IsDeleted);

            builder.Entity<Appointment>()
                .HasQueryFilter(a => !a.IsDeleted);

            builder.Entity<Review>()
                .HasQueryFilter(r => !r.IsDeleted);


            builder.Entity<Service>().HasData(
                new Service
                {
                    Id = 1,
                    Name = "Подстригване - мъже",
                    Description = "Класическо мъжко подстригване с машинка и ножица",
                    Price = 25.00m,
                    DurationInMinutes = 30,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    IsDeleted = false
                },
                new Service
                {
                    Id = 2,
                    Name = "Подстригване - жени (къса коса)",
                    Description = "Подстригване и оформяне на къса коса",
                    Price = 30.00m,
                    DurationInMinutes = 40,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    IsDeleted = false
                },
                new Service
                {
                    Id = 3,
                    Name = "Подстригване - жени (дълга коса)",
                    Description = "Подстригване и оформяне на дълга коса",
                    Price = 40.00m,
                    DurationInMinutes = 60,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    IsDeleted = false
                },
                new Service
                {
                    Id = 4,
                    Name = "Боядисване - цяла глава",
                    Description = "Пълно боядисване с професионална боя",
                    Price = 80.00m,
                    DurationInMinutes = 120,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    IsDeleted = false
                },
                new Service
                {
                    Id = 5,
                    Name = "Кичури (фолио)",
                    Description = "Частично освежаване с кичури - фолио техника",
                    Price = 60.00m,
                    DurationInMinutes = 90,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    IsDeleted = false
                },
                new Service
                {
                    Id = 6,
                    Name = "Балеаж",
                    Description = "Балеаж техника за естествен ефект",
                    Price = 100.00m,
                    DurationInMinutes = 150,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    IsDeleted = false
                },
                new Service
                {
                    Id = 7,
                    Name = "Сешоар",
                    Description = "Измиване, подсушаване и оформяне със сешоар",
                    Price = 20.00m,
                    DurationInMinutes = 30,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    IsDeleted = false
                },
                new Service
                {
                    Id = 8,
                    Name = "Преса",
                    Description = "Изправяне с преса за гладка коса",
                    Price = 25.00m,
                    DurationInMinutes = 45,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    IsDeleted = false
                },
                new Service
                {
                    Id = 9,
                    Name = "Къдрене",
                    Description = "Къдрене с маша или ролки",
                    Price = 30.00m,
                    DurationInMinutes = 50,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    IsDeleted = false
                },
                new Service
                {
                    Id = 10,
                    Name = "Детско подстригване",
                    Description = "Подстригване за деца до 12 години",
                    Price = 15.00m,
                    DurationInMinutes = 20,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    IsDeleted = false
                },
                new Service
                {
                    Id = 11,
                    Name = "Оформяне на брада",
                    Description = "Подстригване и оформяне на брада и мустаци",
                    Price = 15.00m,
                    DurationInMinutes = 20,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    IsDeleted = false
                },
                new Service
                {
                    Id = 12,
                    Name = "Кератинова терапия",
                    Description = "Кератиново изправяне за дълготрайна гладкост",
                    Price = 150.00m,
                    DurationInMinutes = 180,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    IsDeleted = false
                }
            );

            builder.Entity<Client>().HasData(
                new Client
                {
                    Id = 1,
                    FirstName = "Иван",
                    LastName = "Петров",
                    PhoneNumber = "+359888123456",
                    Email = "ivan.petrov@example.com",
                    Note = "Предпочита късо подстригване, алергия към силни парфюми",
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    CreatedOn = new DateTime(2024, 10, 5),
                    IsDeleted = false
                },
                new Client
                {
                    Id = 2,
                    FirstName = "Мария",
                    LastName = "Георгиева",
                    PhoneNumber = "0877654321",
                    Email = "maria.georgieva@example.com",
                    Note = "Алергия към амоняк - използвай безамонячна боя",
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    CreatedOn = new DateTime(2024, 11, 12),
                    IsDeleted = false
                },
                new Client
                {
                    Id = 3,
                    FirstName = "Георги",
                    LastName = "Иванов",
                    PhoneNumber = "0898765432",
                    Email = "georgi.ivanov@gmail.com",
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    CreatedOn = new DateTime(2024, 12, 20),
                    IsDeleted = false
                },
                new Client
                {
                    Id = 4,
                    FirstName = "Елена",
                    LastName = "Димитрова",
                    PhoneNumber = "+359887111222",
                    Email = "elena.dimitrova@abv.bg",
                    Note = "Предпочита топли тонове при боядисване",
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    CreatedOn = new DateTime(2025, 1, 8),
                    IsDeleted = false
                },
                new Client
                {
                    Id = 5,
                    FirstName = "Петър",
                    LastName = "Стоянов",
                    PhoneNumber = "0878333444",
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    CreatedOn = new DateTime(2025, 1, 15),
                    IsDeleted = false
                },
                new Client
                {
                    Id = 6,
                    FirstName = "Анна",
                    LastName = "Тодорова",
                    PhoneNumber = "+359899555666",
                    Email = "anna.todorova@mail.bg",
                    Note = "Много чувствителен скалп - внимавай с температурата",
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    CreatedOn = new DateTime(2025, 2, 1),
                    IsDeleted = false
                },
                new Client
                {
                    Id = 7,
                    FirstName = "Димитър",
                    LastName = "Василев",
                    PhoneNumber = "0877777888",
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    CreatedOn = new DateTime(2025, 2, 10),
                    IsDeleted = false
                },
                new Client
                {
                    Id = 8,
                    FirstName = "София",
                    LastName = "Николова",
                    PhoneNumber = "+359888999000",
                    Email = "sofia.nikolova@yahoo.com",
                    Note = "Винаги иска съвет за нови прически",
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    CreatedOn = new DateTime(2026, 1, 20),
                    IsDeleted = false
                },
                new Client
                {
                    Id = 9,
                    FirstName = "Стефан",
                    LastName = "Йорданов",
                    PhoneNumber = "0898111222",
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    CreatedOn = new DateTime(2026, 2, 5),
                    IsDeleted = false
                },
                new Client
                {
                    Id = 10,
                    FirstName = "Виктория",
                    LastName = "Христова",
                    PhoneNumber = "+359877333555",
                    Email = "viki.hristova@gmail.com",
                    Note = "Дълга коса, иска да запази дължината",
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    CreatedOn = new DateTime(2026, 2, 8),
                    IsDeleted = false
                }
            );

            builder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 1,
                    ClientId = 1,
                    ServiceId = 1,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    AppointmentDate = new DateTime(2025, 12, 10, 10, 0, 0),
                    Status = AppointmentStatus.Completed,
                    Note = "Първо посещение",
                    CreatedOn = new DateTime(2025, 12, 1),
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = 2,
                    ClientId = 2,
                    ServiceId = 4,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    AppointmentDate = new DateTime(2025, 12, 15, 14, 0, 0),
                    Status = AppointmentStatus.Completed,
                    CreatedOn = new DateTime(2025, 12, 5),
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = 3,
                    ClientId = 3,
                    ServiceId = 1,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    AppointmentDate = new DateTime(2026, 1, 5, 11, 30, 0),
                    Status = AppointmentStatus.Completed,
                    CreatedOn = new DateTime(2025, 12, 28),
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = 4,
                    ClientId = 4,
                    ServiceId = 5,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    AppointmentDate = new DateTime(2026, 1, 20, 15, 0, 0),
                    Status = AppointmentStatus.Completed,
                    Note = "Клиентката е много доволна от резултата",
                    CreatedOn = new DateTime(2026, 1, 10),
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = 5,
                    ClientId = 1,
                    ServiceId = 1,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    AppointmentDate = new DateTime(2026, 2, 1, 10, 0, 0),
                    Status = AppointmentStatus.Completed,
                    CreatedOn = new DateTime(2026, 1, 25),
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = 6,
                    ClientId = 5,
                    ServiceId = 11,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    AppointmentDate = new DateTime(2026, 1, 25, 12, 0, 0),
                    Status = AppointmentStatus.Canceled,
                    Note = "Клиентът отмени заради болест",
                    CreatedOn = new DateTime(2026, 1, 20),
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = 7,
                    ClientId = 7,
                    ServiceId = 1,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    AppointmentDate = new DateTime(2026, 2, 8, 9, 0, 0),
                    Status = AppointmentStatus.DidNotShowed,
                    Note = "Не се е появил и не е обадил",
                    CreatedOn = new DateTime(2026, 2, 1),
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = 8,
                    ClientId = 2,
                    ServiceId = 7,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    AppointmentDate = new DateTime(2026, 2, 18, 10, 0, 0),
                    Status = AppointmentStatus.Scheduled,
                    CreatedOn = new DateTime(2026, 2, 10),
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = 9,
                    ClientId = 6,
                    ServiceId = 3,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    AppointmentDate = new DateTime(2026, 2, 20, 14, 30, 0),
                    Status = AppointmentStatus.Scheduled,
                    Note = "Иска да свали 5см и да оформи бретон",
                    CreatedOn = new DateTime(2026, 2, 12),
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = 10,
                    ClientId = 8,
                    ServiceId = 6,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    AppointmentDate = new DateTime(2026, 2, 22, 11, 0, 0),
                    Status = AppointmentStatus.Scheduled,
                    Note = "Първи път балеаж - покажи примери",
                    CreatedOn = new DateTime(2026, 2, 13),
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = 11,
                    ClientId = 4,
                    ServiceId = 8,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    AppointmentDate = new DateTime(2026, 2, 25, 16, 0, 0),
                    Status = AppointmentStatus.Scheduled,
                    CreatedOn = new DateTime(2026, 2, 13),
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = 12,
                    ClientId = 10,
                    ServiceId = 3,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    AppointmentDate = new DateTime(2026, 2, 28, 10, 30, 0),
                    Status = AppointmentStatus.Scheduled,
                    Note = "Само почистване на краищата",
                    CreatedOn = new DateTime(2026, 2, 13),
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = 13,
                    ClientId = 3,
                    ServiceId = 11,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    AppointmentDate = new DateTime(2026, 3, 5, 9, 30, 0),
                    Status = AppointmentStatus.Scheduled,
                    CreatedOn = new DateTime(2026, 2, 13),
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = 14,
                    ClientId = 9,
                    ServiceId = 1,
                    HairdresserId = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                    AppointmentDate = new DateTime(2026, 3, 8, 12, 0, 0),
                    Status = AppointmentStatus.Scheduled,
                    CreatedOn = new DateTime(2026, 2, 13),
                    IsDeleted = false
                }
            );

            builder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    ClientId = 1,
                    AppointmentId = 1,
                    Rating = 5,
                    Comment = "Много съм доволен! Бързо и качествено обслужване.",
                    CreatedOn = new DateTime(2025, 12, 10),
                    IsDeleted = false
                },
                new Review
                {
                    Id = 2,
                    ClientId = 2,
                    AppointmentId = 2,
                    Rating = 4,
                    Comment = "Цветът стана страхотен, но отне малко повече време.",
                    CreatedOn = new DateTime(2025, 12, 15),
                    IsDeleted = false
                },
                new Review
                {
                    Id = 3,
                    ClientId = 3,
                    AppointmentId = 3,
                    Rating = 5,
                    Comment = "Перфектно подстригване, точно както го исках.",
                    CreatedOn = new DateTime(2026, 1, 5),
                    IsDeleted = false
                },
                new Review
                {
                    Id = 4,
                    ClientId = 4,
                    AppointmentId = 4,
                    Rating = 5,
                    Comment = "Най-добрият балеаж, който съм имала!",
                    CreatedOn = new DateTime(2026, 1, 20),
                    IsDeleted = false
                },
                new Review
                {
                    Id = 5,
                    ClientId = 1,
                    AppointmentId = 5,
                    Rating = 4,
                    Comment = "Отново съм доволен, ще посетя пак.",
                    CreatedOn = new DateTime(2026, 2, 1),
                    IsDeleted = false
                }
            );

            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "L'Oreal Serie Expert Absolut Repair Shampoo",
                    Price = 29.90m,
                    ImageUrl = "/images/products/loreal-absolut-repair-shampoo.jpg",
                    Description = "Професионален шампоан за увредена коса с възстановяващо действие и незабавен ефект на заглаждане.",
                    QuantityInStock = 50,
                    CreatedOn = new DateTime(2025, 11, 3),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 2,
                    Name = "Kerastase Resistance Bain Force Architecte Shampoo",
                    Price = 39.90m,
                    ImageUrl = "/images/products/kerastase-shampoo.jpg",
                    Description = "Подсилващ шампоан за слаба и увредена коса, който възстановява структурата и здравината.",
                    QuantityInStock = 40,
                    CreatedOn = new DateTime(2025, 11, 7),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 3,
                    Name = "Wella Invigo Nutri-Enrich Shampoo",
                    Price = 24.50m,
                    ImageUrl = "/images/products/wella-invigo-shampoo.jpg",
                    Description = "Подхранващ шампоан за суха и изтощена коса с дълбоко хидратиращ ефект.",
                    QuantityInStock = 60,
                    CreatedOn = new DateTime(2025, 11, 21),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 4,
                    Name = "Schwarzkopf BC Bonacure Repair Rescue Shampoo",
                    Price = 22.90m,
                    ImageUrl = "/images/products/schwarzkopf-bcbonacure-shampoo.jpg",
                    Description = "Възстановяващ шампоан с веган кератин за силно увредена коса.",
                    QuantityInStock = 55,
                    CreatedOn = new DateTime(2025, 12, 1),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 5,
                    Name = "Kerastase Masque Therapiste",
                    Price = 54.90m,
                    ImageUrl = "/images/products/kerastase-masque.jpg",
                    Description = "Дълбоко възстановяваща маска за силно увредена коса, която възвръща еластичността и блясъка.",
                    QuantityInStock = 30,
                    CreatedOn = new DateTime(2025, 12, 1),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 6,
                    Name = "L'Oreal Absolut Repair Golden Mask",
                    Price = 34.90m,
                    ImageUrl = "/images/products/loreal-absolut-repair-mask.jpg",
                    Description = "Професионална маска за интензивно възстановяване и подхранване на косата.",
                    QuantityInStock = 45,
                    CreatedOn = new DateTime(2025, 12, 5),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 7,
                    Name = "Wella Fusion Intense Repair Mask",
                    Price = 29.90m,
                    ImageUrl = "/images/products/wella-fusion-mask.jpg",
                    Description = "Интензивна маска за възстановяване на косата и защита от накъсване.",
                    QuantityInStock = 35,
                    CreatedOn = new DateTime(2025, 12, 10),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 8,
                    Name = "American Crew Fiber",
                    Price = 21.90m,
                    ImageUrl = "/images/products/american-crew.jpg",
                    Description = "Силен фиксиращ продукт с матов ефект за оформяне на модерни прически.",
                    QuantityInStock = 50,
                    CreatedOn = new DateTime(2025, 12, 18),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 9,
                    Name = "L'Oreal Tecni Art Fix Max Gel",
                    Price = 19.90m,
                    ImageUrl = "/images/products/loreal-tecni-art.jpg",
                    Description = "Гел със силна фиксация за структурирани и дълготрайни прически.",
                    QuantityInStock = 40,
                    CreatedOn = new DateTime(2026, 1, 20),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 10,
                    Name = "Schwarzkopf Osis+ Dust It",
                    Price = 18.50m,
                    ImageUrl = "/images/products/schwarzkopf-osis-dust-it.jpg",
                    Description = "Матираща пудра за придаване на обем и текстура на косата.",
                    QuantityInStock = 60,
                    CreatedOn = new DateTime(2026, 2, 10),
                    IsDeleted = false
                },
                new Product
                {
                    Id = 11,
                    Name = "Wella EIMI Super Set Spray",
                    Price = 17.90m,
                    ImageUrl = "/images/products/wella-eimi-spray.jpg",
                    Description = "Лак за коса със силна фиксация за дълготраен контрол и завършен стил.",
                    QuantityInStock = 70,
                    CreatedOn = new DateTime(2026, 2, 25),
                    IsDeleted = false
                }
            );
        }
    }
}
