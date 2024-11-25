﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrumpVersApi.Contexts;

#nullable disable

namespace TrumpVersApi.Migrations
{
    [DbContext(typeof(TrumpThoughtsContext))]
    partial class TrumpThoughtsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("TrumpVersApi.Models.TrumpThoughts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Thought")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TrumpThoughts");
                });
#pragma warning restore 612, 618
        }
    }
}
