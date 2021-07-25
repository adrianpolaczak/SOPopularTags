﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SOPopularTags.Infrastructure;

namespace SOPopularTags.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210725195242_UpdateTables")]
    partial class UpdateTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SOPopularTags.Domain.Models.SOTagRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("SOTagRequests");
                });

            modelBuilder.Entity("SOPopularTags.Domain.Models.SOTagRequestItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<bool>("HasSynonyms")
                        .HasColumnType("bit");

                    b.Property<bool>("IsModeratorOnly")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SOTagRequestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SOTagRequestId");

                    b.ToTable("SOTagRequestItems");
                });

            modelBuilder.Entity("SOPopularTags.Domain.Models.SOTagRequestItem", b =>
                {
                    b.HasOne("SOPopularTags.Domain.Models.SOTagRequest", "SOTagRequest")
                        .WithMany("Items")
                        .HasForeignKey("SOTagRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SOTagRequest");
                });

            modelBuilder.Entity("SOPopularTags.Domain.Models.SOTagRequest", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
