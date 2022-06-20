﻿// <auto-generated />
using LibraryProject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LibraryProject.API.Migrations
{
    [DbContext(typeof(LibraryProjectContext))]
    partial class LibraryProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LibraryProject.API.Database.Entities.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("bookId")
                        .HasColumnType("int");

                    b.Property<string>("loaned_At")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("return_date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Loan");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            bookId = 2,
                            loaned_At = "06/05/22",
                            return_date = "13/05/22",
                            userID = 2
                        },
                        new
                        {
                            Id = 3,
                            bookId = 5,
                            loaned_At = "27/06/22",
                            return_date = "27/07/22",
                            userID = 4
                        });
                });

            modelBuilder.Entity("LibraryProject.API.Database.Entities.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(62)");

                    b.HasKey("Id");

                    b.ToTable("Publisher");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Gyldendal"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Rosinante"
                        });
                });

            modelBuilder.Entity("LibraryProject.API.Database.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("bookId")
                        .HasColumnType("int");

                    b.Property<string>("reserved_At")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("reserved_To")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Reservation");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            bookId = 1,
                            reserved_At = "06/05/22",
                            reserved_To = "13/05/22",
                            userId = 1
                        },
                        new
                        {
                            Id = 2,
                            bookId = 2,
                            reserved_At = "14/05/22",
                            reserved_To = "21/05/22",
                            userId = 2
                        });
                });

            modelBuilder.Entity("LibraryProject.Database.Entities.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Author");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Astrid",
                            LastName = " Lindgrens",
                            MiddleName = ""
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Helle",
                            LastName = "Helle",
                            MiddleName = ""
                        });
                });

            modelBuilder.Entity("LibraryProject.Database.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(32)");

                    b.Property<short>("PublishYear")
                        .HasColumnType("smallint");

                    b.Property<int>("PublisherId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Book");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            CategoryId = 1,
                            Description = "Bog for børn",
                            Image = "Book1.png",
                            Language = "Danish",
                            PublishYear = (short)1945,
                            PublisherId = 1,
                            Title = "Pippi Langstrømper"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 2,
                            CategoryId = 2,
                            Description = "Romaner for voksen2",
                            Image = "Book2.png",
                            Language = "Danish",
                            PublishYear = (short)2005,
                            PublisherId = 2,
                            Title = "Rødby-Puttgarden"
                        });
                });

            modelBuilder.Entity("LibraryProject.Database.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "KidsBook"
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Roman"
                        });
                });

            modelBuilder.Entity("LibraryProject.Database.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "peter@abc.com",
                            FirstName = "Peter",
                            LastName = "Aksten",
                            MiddleName = "Per.",
                            Password = "password",
                            Role = 0
                        },
                        new
                        {
                            Id = 2,
                            Email = "riz@abc.com",
                            FirstName = "Rizwanah",
                            LastName = "Mustafa",
                            MiddleName = "R.R",
                            Password = "password",
                            Role = 1
                        });
                });

            modelBuilder.Entity("LibraryProject.Database.Entities.Book", b =>
                {
                    b.HasOne("LibraryProject.Database.Entities.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryProject.Database.Entities.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryProject.API.Database.Entities.Publisher", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Category");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("LibraryProject.Database.Entities.Category", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
