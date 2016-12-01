using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TODO_APP1.Models;

namespace TODO_APP1.Migrations
{
    [DbContext(typeof(TODO_AppContext))]
    [Migration("20161130202409_30112016")]
    partial class _30112016
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TODO_APP1.Models.Todos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("TODOS");
                });

            modelBuilder.Entity("TODO_APP1.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("TodoId")
                        .HasColumnName("TODO_Id");

                    b.Property<string>("UserFullName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("TodoId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TODO_APP1.Models.Users", b =>
                {
                    b.HasOne("TODO_APP1.Models.Todos", "Todo")
                        .WithMany("Users")
                        .HasForeignKey("TodoId")
                        .HasConstraintName("fk_todoId");
                });
        }
    }
}
