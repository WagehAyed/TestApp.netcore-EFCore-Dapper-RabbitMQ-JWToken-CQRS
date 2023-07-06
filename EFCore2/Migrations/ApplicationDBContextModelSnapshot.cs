﻿// <auto-generated />
using EFCore2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCore2.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence<int>("Seq_OrderNo");

            modelBuilder.Entity("EFCore2.ApplicationDBContext+Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<long>("OrderNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("NEXT VALUE FOR Seq_OrderNo");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EFCore2.ApplicationDBContext+Order_test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<long>("OrderNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("NEXT VALUE FOR Seq_OrderNo");

                    b.HasKey("Id");

                    b.ToTable("orderTests");
                });
#pragma warning restore 612, 618
        }
    }
}
