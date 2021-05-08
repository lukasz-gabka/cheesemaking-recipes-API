﻿// <auto-generated />
using Cheesemaking_recipes_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cheesemaking_recipes_API.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20210508102340_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cheesemaking_recipes_API.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TemplateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Cheesemaking_recipes_API.Entities.Input", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LabelId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LabelId")
                        .IsUnique();

                    b.ToTable("Inputs");
                });

            modelBuilder.Entity("Cheesemaking_recipes_API.Entities.Label", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("Cheesemaking_recipes_API.Entities.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TemplateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId")
                        .IsUnique();

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("Cheesemaking_recipes_API.Entities.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("Cheesemaking_recipes_API.Entities.Category", b =>
                {
                    b.HasOne("Cheesemaking_recipes_API.Entities.Template", "Template")
                        .WithMany("Categories")
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Template");
                });

            modelBuilder.Entity("Cheesemaking_recipes_API.Entities.Input", b =>
                {
                    b.HasOne("Cheesemaking_recipes_API.Entities.Label", "Label")
                        .WithOne("Input")
                        .HasForeignKey("Cheesemaking_recipes_API.Entities.Input", "LabelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Label");
                });

            modelBuilder.Entity("Cheesemaking_recipes_API.Entities.Label", b =>
                {
                    b.HasOne("Cheesemaking_recipes_API.Entities.Category", "Category")
                        .WithMany("Labels")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Cheesemaking_recipes_API.Entities.Note", b =>
                {
                    b.HasOne("Cheesemaking_recipes_API.Entities.Template", "Template")
                        .WithOne("Note")
                        .HasForeignKey("Cheesemaking_recipes_API.Entities.Note", "TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Template");
                });

            modelBuilder.Entity("Cheesemaking_recipes_API.Entities.Category", b =>
                {
                    b.Navigation("Labels");
                });

            modelBuilder.Entity("Cheesemaking_recipes_API.Entities.Label", b =>
                {
                    b.Navigation("Input");
                });

            modelBuilder.Entity("Cheesemaking_recipes_API.Entities.Template", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Note");
                });
#pragma warning restore 612, 618
        }
    }
}
