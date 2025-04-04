using Microsoft.EntityFrameworkCore;

namespace DataLayer.Models
{
    public class HotelDbContextModel : DbContext
    {
        public DbSet<FacilityModel> Facilities { get; set; }
        public DbSet<PaymentMethodModel> PaymentMethods { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<HotelModel> Hotels { get; set; }
        public DbSet<ReservationModel> Reservations { get; set; }
        public DbSet<PaymentModel> Payments { get; set; }
        public HotelDbContextModel(DbContextOptions<HotelDbContextModel> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфигуриране на FacilityModel
            modelBuilder.Entity<FacilityModel>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<FacilityModel>()
                .Property(f => f.Type)
                .HasConversion<string>();  // Запазване на Enum като string

            // Конфигуриране на PaymentMethodModel
            modelBuilder.Entity<PaymentMethodModel>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<PaymentMethodModel>()
                .Property(p => p.Type)
                .HasConversion<string>();

            // Конфигуриране на UserModel
            modelBuilder.Entity<UserModel>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<UserModel>()
            .HasIndex(u => u.UserName)
            .IsUnique();

            modelBuilder.Entity<UserModel>()
                .Property(p => p.Type)
                .HasConversion<string>();

            // Връзка с хотела (Добавяне на връзка с таблицата Hotels)
            modelBuilder.Entity<UserModel>()
                .HasOne<HotelModel>()
                .WithMany()
                .HasForeignKey(u => u.HotelId)
                .OnDelete(DeleteBehavior.SetNull); // Потребителите могат да нямат свързан хотел

            // Конфигуриране на HotelModel
            modelBuilder.Entity<HotelModel>()
                .HasKey(p => p.Id);

            // Конфигуриране на ReservationModel
            modelBuilder.Entity<ReservationModel>()
                .HasKey(p => p.Id);

            // Конфигуриране на PaymentModel
            modelBuilder.Entity<PaymentModel>()
                .HasKey(p => p.Id);

            // Вмъкване на системни платежни методи
            modelBuilder.Entity<PaymentMethodModel>().HasData(
                new PaymentMethodModel { Id = 1, Name = PaymentMethodTypeEnum.CreditCard.ToString(), Type = PaymentMethodTypeEnum.CreditCard, IsSystemDefined = true },
                new PaymentMethodModel { Id = 2, Name = PaymentMethodTypeEnum.PayPal.ToString(), Type = PaymentMethodTypeEnum.PayPal, IsSystemDefined = true },
                new PaymentMethodModel { Id = 3, Name = PaymentMethodTypeEnum.BankTransfer.ToString(), Type = PaymentMethodTypeEnum.BankTransfer, IsSystemDefined = true }
            );
            // Вмъкване на системни хотелски удобства
            modelBuilder.Entity<FacilityModel>().HasData(
                new FacilityModel { Id = 1, Type = FacilityTypeEnum.Pool , Name = FacilityTypeEnum.Pool.ToString() },
                new FacilityModel { Id = 2, Type = FacilityTypeEnum.Gym, Name = FacilityTypeEnum.Gym.ToString() },
                new FacilityModel { Id = 3, Type = FacilityTypeEnum.Spa, Name = FacilityTypeEnum.Spa.ToString() }
            );

            // Вмъкване на системен потребител (администратор)
            modelBuilder.Entity<UserModel>().HasData(
                new UserModel
                {
                    Id = 1,
                    UserName = "admin",
                    Password = UserModel.HashPassword("admin123"), // Запазваме хеширана парола
                    Type = UserTypeEnum.Admin
                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string solutionFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string databaseFile = "Hotel.db";
            string databasePath = Path.Combine(solutionFolder, databaseFile);
            optionsBuilder.UseSqlite($"Data Source = {databasePath}");
        }
    }

}
