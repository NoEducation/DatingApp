﻿// <auto-generated />
using Dating.DAT;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dating.Migrations
{
    [DbContext(typeof(DatingContext))]
    [Migration("20191102115618_InitalMigration")]
    partial class InitalMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("Dating.Models.Value", b =>
                {
                    b.Property<int>("ValueId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ValueId");

                    b.ToTable("Values");
                });
#pragma warning restore 612, 618
        }
    }
}