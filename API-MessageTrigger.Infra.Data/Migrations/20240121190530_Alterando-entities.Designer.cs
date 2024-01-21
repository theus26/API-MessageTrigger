﻿// <auto-generated />
using API_MessageTrigger.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APIMessageTrigger.Infra.Data.Migrations
{
    [DbContext(typeof(MessageTriggerContext))]
    [Migration("20240121190530_Alterando-entities")]
    partial class Alterandoentities
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("API_MessageTrigger.Domain.Entities.MessageTrigger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("InstanceName")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("NameInstance");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("PhoneNumber");

                    b.Property<bool>("Qrcode")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("Qrcode");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Token");

                    b.HasKey("Id");

                    b.ToTable("MessageTrigger", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
