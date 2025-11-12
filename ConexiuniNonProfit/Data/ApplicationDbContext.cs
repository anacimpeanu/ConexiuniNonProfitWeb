using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ConexiuniNonProfit.Models;
using ConexiuniNonProfit.Models;
using System.Text.RegularExpressions;

namespace ConexiuniNonProfit.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<ApplicationUser> Users { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Profile> Profiles { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Actiuni> Actiuni { get; set; }
		public DbSet<Friend> Friends { get; set; }
		public DbSet<Category> Categories { get; set; }

        public DbSet<PuzzlePiece> PuzzlePieces { get; set; }
        public DbSet<QuizResult> QuizResults { get; set; }
        //public DbSet<UserBadge> UserBadges { get; set; }
        public DbSet<UserParticipation> UserParticipations { get; set; }

       // public DbSet<LeaderboardEntry> LeaderboardEntries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Comment>()
				.HasOne(c => c.User)
				.WithMany(u => u.Comments)
				.HasForeignKey(c => c.UserId);

			modelBuilder.Entity<Comment>()
				.HasOne(c => c.Post)
				.WithMany(p => p.Comments)
				.HasForeignKey(c => c.PostId);

			modelBuilder.Entity<Post>()
				.HasOne(p => p.User)
				.WithMany(u => u.Posts)
				.HasForeignKey(p => p.UserId);


			modelBuilder.Entity<Post>()
				.HasOne(p => p.Category)
				.WithMany(c => c.Posts)
				.HasForeignKey(p => p.CategoryId);


			modelBuilder.Entity<Friend>()
				.HasOne(s => s.User1)
				.WithMany()
				.HasForeignKey(s => s.User1_Id)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Friend>()
				.HasOne(s => s.User2)
				.WithMany()
				.HasForeignKey(s => s.User2_Id)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Friend>()
				.HasOne(s => s.Actiuni)
				.WithMany()
				.HasForeignKey(s => s.ActiuniId);

			modelBuilder.Entity<Actiuni>().HasData(

				new Actiuni { ActiuniId = 1, ActiuniName = "Donare de sânge", ActiuniAbbreviation = "DS", ActiuniDescription = "Actiune de donare de sânge" },
				new Actiuni { ActiuniId = 2, ActiuniName = "Donare de organe", ActiuniAbbreviation = "DO", ActiuniDescription = "Actiune de donare de organe" },
				new Actiuni { ActiuniId = 3, ActiuniName = "Donare de haine", ActiuniAbbreviation = "DH", ActiuniDescription = "Actiune de donare de haine" },
				new Actiuni { ActiuniId = 4, ActiuniName = "Donare de alimente", ActiuniAbbreviation = "DA", ActiuniDescription = "Actiune de donare de alimente" },
				new Actiuni { ActiuniId = 5, ActiuniName = "Donare de bani", ActiuniAbbreviation = "DB", ActiuniDescription = "Actiune de donare de bani" },
				new Actiuni { ActiuniId = 6, ActiuniName = "Voluntariat educațional", ActiuniAbbreviation = "VE", ActiuniDescription = "Actiune de voluntariat în domeniul educației" },
				new Actiuni { ActiuniId = 7, ActiuniName = "Voluntariat de mediu", ActiuniAbbreviation = "VM", ActiuniDescription = "Actiune de voluntariat pentru protecția mediului" },
				new Actiuni { ActiuniId = 8, ActiuniName = "Voluntariat social", ActiuniAbbreviation = "VS", ActiuniDescription = "Actiune de voluntariat pentru ajutorarea persoanelor defavorizate" },
				new Actiuni { ActiuniId = 9, ActiuniName = "Voluntariat cultural", ActiuniAbbreviation = "VC", ActiuniDescription = "Actiune de voluntariat în domeniul culturii" },
				new Actiuni { ActiuniId = 10, ActiuniName = "Maraton caritabil", ActiuniAbbreviation = "MC", ActiuniDescription = "Organizare de maratoane pentru strângere de fonduri" },
				new Actiuni { ActiuniId = 11, ActiuniName = "Concert caritabil", ActiuniAbbreviation = "CC", ActiuniDescription = "Organizare de concerte pentru strângere de fonduri" },
				new Actiuni { ActiuniId = 12, ActiuniName = "Târg caritabil", ActiuniAbbreviation = "TC", ActiuniDescription = "Organizare de târguri pentru strângere de fonduri" },
				new Actiuni { ActiuniId = 13, ActiuniName = "Conferință caritabilă", ActiuniAbbreviation = "CFC", ActiuniDescription = "Organizare de conferințe pentru strângere de fonduri" },
				new Actiuni { ActiuniId = 14, ActiuniName = "Campanie de conștientizare leucemie", ActiuniAbbreviation = "CCL", ActiuniDescription = "Campanie pentru conștientizarea leucemiei" },
				new Actiuni { ActiuniId = 15, ActiuniName = "Sprijin pentru pacienți cu leucemie", ActiuniAbbreviation = "SPL", ActiuniDescription = "Activități de sprijin pentru pacienții cu leucemie și familiile acestora" },
				new Actiuni { ActiuniId = 16, ActiuniName = "Strângere de fonduri pentru leucemie", ActiuniAbbreviation = "SFL", ActiuniDescription = "Evenimente de strângere de fonduri pentru leucemie" },
				new Actiuni { ActiuniId = 17, ActiuniName = "Activități de reabilitare", ActiuniAbbreviation = "AR", ActiuniDescription = "Activități de reabilitare pentru persoane cu dizabilități" },
				new Actiuni { ActiuniId = 18, ActiuniName = "Educație pentru sănătate", ActiuniAbbreviation = "ES", ActiuniDescription = "Programe de educație pentru sănătate" },
				new Actiuni { ActiuniId = 19, ActiuniName = "Proiecte pentru drepturile omului", ActiuniAbbreviation = "PDO", ActiuniDescription = "Proiecte pentru promovarea și apărarea drepturilor omului" },
				new Actiuni { ActiuniId = 20, ActiuniName = "Ajutor pentru refugiați", ActiuniAbbreviation = "ARF", ActiuniDescription = "Activități de ajutorare a refugiaților" },
				new Actiuni { ActiuniId = 21, ActiuniName = "Asistență medicală gratuită", ActiuniAbbreviation = "AMG", ActiuniDescription = "Programe de asistență medicală gratuită" },
				new Actiuni { ActiuniId = 22, ActiuniName = "Educație financiară", ActiuniAbbreviation = "EF", ActiuniDescription = "Programe de educație financiară" },
				new Actiuni { ActiuniId = 23, ActiuniName = "Sprijin pentru persoane vârstnice", ActiuniAbbreviation = "SPV", ActiuniDescription = "Activități de sprijin pentru persoanele vârstnice" },
				new Actiuni { ActiuniId = 24, ActiuniName = "Sprijin pentru copii", ActiuniAbbreviation = "SC", ActiuniDescription = "Activități de sprijin pentru copii" },
				new Actiuni { ActiuniId = 25, ActiuniName = "Sprijin pentru femei", ActiuniAbbreviation = "SF", ActiuniDescription = "Programe de sprijin pentru femei" },
				new Actiuni { ActiuniId = 26, ActiuniName = "Proiecte de dezvoltare comunitară", ActiuniAbbreviation = "PDC", ActiuniDescription = "Proiecte pentru dezvoltarea comunităților" },
				new Actiuni { ActiuniId = 27, ActiuniName = "Asistență juridică gratuită", ActiuniAbbreviation = "AJG", ActiuniDescription = "Programe de asistență juridică gratuită" },
				new Actiuni { ActiuniId = 28, ActiuniName = "Sprijin pentru persoane fără adăpost", ActiuniAbbreviation = "SFA", ActiuniDescription = "Activități de sprijin pentru persoanele fără adăpost" },
				new Actiuni { ActiuniId = 29, ActiuniName = "Sprijin pentru dependenți", ActiuniAbbreviation = "SD", ActiuniDescription = "Programe de sprijin pentru persoanele dependente de diverse substanțe" },
				new Actiuni { ActiuniId = 30, ActiuniName = "Educație IT", ActiuniAbbreviation = "EIT", ActiuniDescription = "Programe de educație în domeniul tehnologiei informației" }
			);

			modelBuilder.Entity<Category>().HasData(
					new Category { CategoryId = 1, CategoryName = "Leucemie" },
					new Category { CategoryId = 2, CategoryName = "Cancer" },
					new Category { CategoryId = 3, CategoryName = "Donare de sânge" },
					new Category { CategoryId = 4, CategoryName = "Donare de organe" },
					new Category { CategoryId = 5, CategoryName = "Voluntariat" },
					new Category { CategoryId = 6, CategoryName = "Educație" },
					new Category { CategoryId = 7, CategoryName = "Mediu" },
					new Category { CategoryId = 8, CategoryName = "Cultură" },
					new Category { CategoryId = 9, CategoryName = "Drepturile omului" },
					new Category { CategoryId = 10, CategoryName = "Ajutor umanitar" },
					new Category { CategoryId = 11, CategoryName = "Sănătate" },
					new Category { CategoryId = 12, CategoryName = "Animale" },
					new Category { CategoryId = 13, CategoryName = "Sport" },
					new Category { CategoryId = 14, CategoryName = "Tineret" },
					new Category { CategoryId = 15, CategoryName = "Femei" },
					new Category { CategoryId = 16, CategoryName = "Copii" },
					new Category { CategoryId = 17, CategoryName = "Vârstnici" },
					new Category { CategoryId = 18, CategoryName = "Persoane cu dizabilități" },
					new Category { CategoryId = 19, CategoryName = "Comunități defavorizate" },
					new Category { CategoryId = 20, CategoryName = "Refugiați" },
					new Category { CategoryId = 21, CategoryName = "Educație financiară" },
					new Category { CategoryId = 22, CategoryName = "Asistență juridică" },
					new Category { CategoryId = 23, CategoryName = "Proiecte de dezvoltare" },
					new Category { CategoryId = 24, CategoryName = "Sprijin psihologic" },
					new Category { CategoryId = 25, CategoryName = "Antreprenoriat social" },
					new Category { CategoryId = 26, CategoryName = "Proiecte culturale" },
					new Category { CategoryId = 27, CategoryName = "Protecția mediului" },
					new Category { CategoryId = 28, CategoryName = "Ajutor pentru persoane fără adăpost" },
					new Category { CategoryId = 29, CategoryName = "Lupta împotriva sărăciei" },
					new Category { CategoryId = 30, CategoryName = "Sprijin pentru dependenți" }
				);
		}

	}
}