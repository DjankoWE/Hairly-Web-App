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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Client>()
                .HasOne(c => c.Hairdresser)
                .WithMany()
                .HasForeignKey(c => c.HairdresserId)
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

            // Global query filters for soft delete
            builder.Entity<Client>()
                .HasQueryFilter(c => !c.IsDeleted);

            builder.Entity<Service>()
                .HasQueryFilter(s => !s.IsDeleted);

            builder.Entity<Appointment>()
                .HasQueryFilter(a => !a.IsDeleted);


            var defaultHairdresser = new IdentityUser
            {
                Id = "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                UserName = "stylist@hairly.com",
                NormalizedUserName = "STYLIST@HAIRLY.COM",
                Email = "stylist@hairly.com",
                NormalizedEmail = "STYLIST@HAIRLY.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(
                        new IdentityUser { UserName = "stylist@hairly.com" }, 
                        "Hairly123!")
            };

            builder.Entity<IdentityUser>().HasData(defaultHairdresser);


            // Seed services
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


            // Seed clients
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

            // Seed appointments
            builder.Entity<Appointment>().HasData(
                // Минали часове (Completed)
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
                // Отменени
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
                // NoShow
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
                // Предстоящи часове (Scheduled) - ВАЖНО: Задай реални бъдещи дати
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
        }
    }
}
