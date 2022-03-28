﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PAS.API.Infrastructure;

#nullable disable

namespace PAS.API.Migrations
{
    [DbContext(typeof(PASDbContext))]
    [Migration("20220328150458_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PAS.API.Infrastructure.Entities.CodeListEntity", b =>
                {
                    b.Property<Guid>("CodeListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("code_list_id");

                    b.Property<string>("CodeListDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("code_list_description");

                    b.Property<string>("CodeListReference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("code_list_reference");

                    b.Property<string>("CodeListTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("code_list_title");

                    b.Property<int>("CodeListVersion")
                        .HasColumnType("int")
                        .HasColumnName("code_list_version");

                    b.Property<string>("EnumerationCodeList")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("enumeration_code_list");

                    b.HasKey("CodeListId");

                    b.ToTable("code_list", "policy_administration_system");
                });
#pragma warning restore 612, 618
        }
    }
}
