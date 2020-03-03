﻿// <auto-generated />
using System;
using DistSysACW.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DistSysACW.Migrations
{
    [DbContext(typeof(UserContext))]
    partial class UserContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DistSysACW.Models.Log", b =>
                {
                    b.Property<int>("LogID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LogDateTime");

                    b.Property<string>("Log_string");

                    b.HasKey("LogID");

                    b.ToTable("logs");
                });

            modelBuilder.Entity("DistSysACW.Models.User", b =>
                {
                    b.Property<int>("api_key")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("log_dataLogID");

                    b.Property<string>("role");

                    b.Property<string>("user_name");

                    b.HasKey("api_key");

                    b.HasIndex("log_dataLogID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DistSysACW.Models.User", b =>
                {
                    b.HasOne("DistSysACW.Models.Log", "log_data")
                        .WithMany()
                        .HasForeignKey("log_dataLogID");
                });
#pragma warning restore 612, 618
        }
    }
}
