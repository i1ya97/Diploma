﻿// <auto-generated />
using Diploma.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Diploma.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Diploma.Models.Attribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttributeTypeId")
                        .HasColumnType("int");

                    b.Property<int>("ClusterId")
                        .HasColumnType("int");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PrecedentId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PrimaryKey_AttributeId");

                    b.HasIndex("AttributeTypeId");

                    b.HasIndex("ClusterId");

                    b.HasIndex("PrecedentId");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("Diploma.Models.AttributeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PrimaryKey_AttributeTypeId");

                    b.ToTable("AttributeTypes");
                });

            modelBuilder.Entity("Diploma.Models.Cluster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttributeTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Num")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PrimaryKey_ClusterId");

                    b.HasIndex("AttributeTypeId");

                    b.ToTable("Clusters");
                });

            modelBuilder.Entity("Diploma.Models.Precedent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id")
                        .HasName("PrimaryKey_PrecedentId");

                    b.ToTable("Precedents");
                });

            modelBuilder.Entity("Diploma.Models.Attribute", b =>
                {
                    b.HasOne("Diploma.Models.AttributeType", "AttributeType")
                        .WithMany()
                        .HasForeignKey("AttributeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diploma.Models.Cluster", "Cluster")
                        .WithMany("Attributes")
                        .HasForeignKey("ClusterId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Diploma.Models.Precedent", "Precedent")
                        .WithMany("Attributes")
                        .HasForeignKey("PrecedentId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("AttributeType");

                    b.Navigation("Cluster");

                    b.Navigation("Precedent");
                });

            modelBuilder.Entity("Diploma.Models.Cluster", b =>
                {
                    b.HasOne("Diploma.Models.AttributeType", "AttributeType")
                        .WithMany()
                        .HasForeignKey("AttributeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AttributeType");
                });

            modelBuilder.Entity("Diploma.Models.Cluster", b =>
                {
                    b.Navigation("Attributes");
                });

            modelBuilder.Entity("Diploma.Models.Precedent", b =>
                {
                    b.Navigation("Attributes");
                });
#pragma warning restore 612, 618
        }
    }
}
