﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicHub.Data;

namespace MusicHub.Migrations
{
    [DbContext(typeof(MusicHubDbContext))]
    [Migration("20191123083831_InitialDb")]
    partial class InitialDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MusicHub.Data.Models.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<int?>("ProducerId");

                    b.Property<DateTime>("ReleaseDate");

                    b.HasKey("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("MusicHub.Data.Models.Performer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<decimal>("NetWorth");

                    b.HasKey("Id");

                    b.ToTable("Performers");
                });

            modelBuilder.Entity("MusicHub.Data.Models.Producer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Pseudonym");

                    b.HasKey("Id");

                    b.ToTable("Producers");
                });

            modelBuilder.Entity("MusicHub.Data.Models.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AlbumId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<TimeSpan>("Duration");

                    b.Property<int>("Genre");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<decimal>("Price");

                    b.Property<int>("WriterId");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("WriterId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("MusicHub.Data.Models.SongPerformer", b =>
                {
                    b.Property<int>("SongId");

                    b.Property<int>("PerformerId");

                    b.HasKey("SongId", "PerformerId");

                    b.HasIndex("PerformerId");

                    b.ToTable("SongsPerformers");
                });

            modelBuilder.Entity("MusicHub.Data.Models.Writer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Pseudonym");

                    b.HasKey("Id");

                    b.ToTable("Writers");
                });

            modelBuilder.Entity("MusicHub.Data.Models.Album", b =>
                {
                    b.HasOne("MusicHub.Data.Models.Producer", "Producer")
                        .WithMany("Albums")
                        .HasForeignKey("ProducerId");
                });

            modelBuilder.Entity("MusicHub.Data.Models.Song", b =>
                {
                    b.HasOne("MusicHub.Data.Models.Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId");

                    b.HasOne("MusicHub.Data.Models.Writer", "Writer")
                        .WithMany("Songs")
                        .HasForeignKey("WriterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MusicHub.Data.Models.SongPerformer", b =>
                {
                    b.HasOne("MusicHub.Data.Models.Performer", "Performer")
                        .WithMany("PerformerSongs")
                        .HasForeignKey("PerformerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MusicHub.Data.Models.Song", "Song")
                        .WithMany("SongPerformers")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
