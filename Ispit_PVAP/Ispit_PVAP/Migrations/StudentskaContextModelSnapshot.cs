﻿// <auto-generated />
using System;
using Ispit_PVAP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ispit_PVAP.Migrations
{
    [DbContext(typeof(StudentskaContext))]
    partial class StudentskaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Serbian_Latin_100_CI_AI")
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ispit_PVAP.Models.Ispit", b =>
                {
                    b.Property<int>("IdIspita")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_ISPITA");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdIspita"));

                    b.Property<DateOnly>("Datum")
                        .HasColumnType("date")
                        .HasColumnName("DATUM");

                    b.Property<short>("IdPredmeta")
                        .HasColumnType("smallint")
                        .HasColumnName("ID_PREDMETA");

                    b.Property<int>("IdRoka")
                        .HasColumnType("int")
                        .HasColumnName("ID_ROKA");

                    b.HasKey("IdIspita")
                        .HasName("PK__ispit__29C6AD7FDD02363B");

                    b.HasIndex("IdRoka");

                    b.ToTable("ispit", (string)null);
                });

            modelBuilder.Entity("Ispit_PVAP.Models.IspitniRok", b =>
                {
                    b.Property<int>("IdRoka")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_ROKA");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRoka"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("NAZIV");

                    b.Property<string>("SkolskaGod")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)")
                        .HasColumnName("SKOLSKA_GOD");

                    b.Property<string>("StatusRoka")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("STATUS_ROKA")
                        .IsFixedLength();

                    b.HasKey("IdRoka")
                        .HasName("PK__ispitni___C7D0FE72BF36341A");

                    b.ToTable("ispitni_rok", (string)null);
                });

            modelBuilder.Entity("Ispit_PVAP.Models.Predmet", b =>
                {
                    b.Property<short>("IdPredmeta")
                        .HasColumnType("smallint")
                        .HasColumnName("ID_PREDMETA");

                    b.Property<short>("Espb")
                        .HasColumnType("smallint")
                        .HasColumnName("ESPB");

                    b.Property<short>("IdProfesora")
                        .HasColumnType("smallint")
                        .HasColumnName("ID_PROFESORA");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("NAZIV");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("STATUS")
                        .IsFixedLength();

                    b.HasKey("IdPredmeta")
                        .HasName("PK__predmet__2C40E4E28279966B");

                    b.HasIndex("IdProfesora");

                    b.ToTable("predmet", (string)null);
                });

            modelBuilder.Entity("Ispit_PVAP.Models.Profesor", b =>
                {
                    b.Property<short>("IdProfesora")
                        .HasColumnType("smallint")
                        .HasColumnName("ID_PROFESORA");

                    b.Property<DateOnly>("DatumZap")
                        .HasColumnType("date")
                        .HasColumnName("DATUM_ZAP");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("IME");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("PREZIME");

                    b.Property<string>("Zvanje")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("ZVANJE")
                        .IsFixedLength();

                    b.HasKey("IdProfesora")
                        .HasName("PK__profesor__63597FD7DB522E88");

                    b.ToTable("profesor", (string)null);
                });

            modelBuilder.Entity("Ispit_PVAP.Models.Student", b =>
                {
                    b.Property<int>("IdStudenta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_STUDENTA");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdStudenta"));

                    b.Property<short>("Broj")
                        .HasColumnType("smallint")
                        .HasColumnName("BROJ");

                    b.Property<string>("GodinaUpisa")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)")
                        .HasColumnName("GODINA_UPISA");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("IME");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)")
                        .HasColumnName("PREZIME");

                    b.Property<string>("Smer")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)")
                        .HasColumnName("SMER");

                    b.HasKey("IdStudenta")
                        .HasName("PK__student__0FD2897897FB3C85");

                    b.ToTable("student", (string)null);
                });

            modelBuilder.Entity("Ispit_PVAP.Models.StudentPredmet", b =>
                {
                    b.Property<int>("IdStudenta")
                        .HasColumnType("int")
                        .HasColumnName("ID_STUDENTA");

                    b.Property<short>("IdPredmeta")
                        .HasColumnType("smallint")
                        .HasColumnName("ID_PREDMETA");

                    b.Property<string>("SkolskaGodina")
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)")
                        .HasColumnName("SKOLSKA_GODINA");

                    b.HasKey("IdStudenta", "IdPredmeta", "SkolskaGodina")
                        .HasName("PK__student___BB07D41166382CA2");

                    b.HasIndex("IdPredmeta");

                    b.ToTable("student_predmet", (string)null);
                });

            modelBuilder.Entity("Ispit_PVAP.Models.Zapisnik", b =>
                {
                    b.Property<int>("IdStudenta")
                        .HasColumnType("int")
                        .HasColumnName("ID_STUDENTA");

                    b.Property<int>("IdIspita")
                        .HasColumnType("int")
                        .HasColumnName("ID_ISPITA");

                    b.Property<string>("Bodovi")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)")
                        .HasColumnName("BODOVI");

                    b.Property<float>("Ocena")
                        .HasColumnType("real")
                        .HasColumnName("OCENA");

                    b.HasKey("IdStudenta", "IdIspita")
                        .HasName("PK__zapisnik__ED4EE3AFE84A496D");

                    b.HasIndex("IdIspita");

                    b.ToTable("zapisnik", (string)null);
                });

            modelBuilder.Entity("Ispit_PVAP.Models.Ispit", b =>
                {
                    b.HasOne("Ispit_PVAP.Models.IspitniRok", "IdRokaNavigation")
                        .WithMany("Ispits")
                        .HasForeignKey("IdRoka")
                        .IsRequired()
                        .HasConstraintName("FK_ispit_ispitni_rok");

                    b.Navigation("IdRokaNavigation");
                });

            modelBuilder.Entity("Ispit_PVAP.Models.Predmet", b =>
                {
                    b.HasOne("Ispit_PVAP.Models.Profesor", "IdProfesoraNavigation")
                        .WithMany("Predmets")
                        .HasForeignKey("IdProfesora")
                        .IsRequired()
                        .HasConstraintName("FK_predmet_profesor");

                    b.Navigation("IdProfesoraNavigation");
                });

            modelBuilder.Entity("Ispit_PVAP.Models.StudentPredmet", b =>
                {
                    b.HasOne("Ispit_PVAP.Models.Predmet", "IdPredmetaNavigation")
                        .WithMany("StudentPredmets")
                        .HasForeignKey("IdPredmeta")
                        .IsRequired()
                        .HasConstraintName("FK_student_predmet_predmet");

                    b.HasOne("Ispit_PVAP.Models.Student", "IdStudentaNavigation")
                        .WithMany("StudentPredmets")
                        .HasForeignKey("IdStudenta")
                        .IsRequired()
                        .HasConstraintName("FK_student_predmet_student");

                    b.Navigation("IdPredmetaNavigation");

                    b.Navigation("IdStudentaNavigation");
                });

            modelBuilder.Entity("Ispit_PVAP.Models.Zapisnik", b =>
                {
                    b.HasOne("Ispit_PVAP.Models.Ispit", "IdIspitaNavigation")
                        .WithMany("Zapisniks")
                        .HasForeignKey("IdIspita")
                        .IsRequired()
                        .HasConstraintName("FK_zapisnik_ispit");

                    b.HasOne("Ispit_PVAP.Models.Student", "IdStudentaNavigation")
                        .WithMany("Zapisniks")
                        .HasForeignKey("IdStudenta")
                        .IsRequired()
                        .HasConstraintName("FK_zapisnik_student");

                    b.Navigation("IdIspitaNavigation");

                    b.Navigation("IdStudentaNavigation");
                });

            modelBuilder.Entity("Ispit_PVAP.Models.Ispit", b =>
                {
                    b.Navigation("Zapisniks");
                });

            modelBuilder.Entity("Ispit_PVAP.Models.IspitniRok", b =>
                {
                    b.Navigation("Ispits");
                });

            modelBuilder.Entity("Ispit_PVAP.Models.Predmet", b =>
                {
                    b.Navigation("StudentPredmets");
                });

            modelBuilder.Entity("Ispit_PVAP.Models.Profesor", b =>
                {
                    b.Navigation("Predmets");
                });

            modelBuilder.Entity("Ispit_PVAP.Models.Student", b =>
                {
                    b.Navigation("StudentPredmets");

                    b.Navigation("Zapisniks");
                });
#pragma warning restore 612, 618
        }
    }
}
