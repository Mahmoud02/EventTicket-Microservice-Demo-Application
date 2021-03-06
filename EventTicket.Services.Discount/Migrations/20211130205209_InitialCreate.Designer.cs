// <auto-generated />
using System;
using EventTicket.Services.Discount.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EventTicket.Services.Discount.Migrations
{
    [DbContext(typeof(DiscountDbContext))]
    [Migration("20211130205209_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EventTicket.Services.Discount.Entities.Coupon", b =>
                {
                    b.Property<Guid>("CouponId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("AlreadyUsed")
                        .HasColumnType("bit");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CouponId");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            CouponId = new Guid("d4677b66-8084-42f1-96b3-9036fa9e8f98"),
                            AlreadyUsed = false,
                            Amount = 10,
                            Code = "BeNice"
                        },
                        new
                        {
                            CouponId = new Guid("8dec7bb4-c21d-4bae-9a50-c0291de19cf8"),
                            AlreadyUsed = false,
                            Amount = 20,
                            Code = "Awesome"
                        },
                        new
                        {
                            CouponId = new Guid("aab55637-18c3-4d8a-93a1-583c23de0d26"),
                            AlreadyUsed = false,
                            Amount = 100,
                            Code = "AlmostFree"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
