using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TODO_APP1.Models
{
    public partial class TODO_AppContext : DbContext
    {
        public virtual DbSet<Todos> Todos { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-MI5NR5S;Database=TODO_App;Trusted_Connection=True;");
        }

        /*public partial class TODOContext : DbContext//configuratia este facuta in Startup.cs
        {
            public TODOContext(DbContextOptions<TODOContext> options)
                :base(options)
            { }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todos>(entity =>
            {
                entity.ToTable("TODOS");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnType("varchar(max)");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Title).HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.TodoId).HasColumnName("TODO_Id");

                entity.Property(e => e.UserFullName)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.UserName).HasColumnType("varchar(100)");

                entity.HasOne(d => d.Todo)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.TodoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_todoId");
            });
        }
    }
}