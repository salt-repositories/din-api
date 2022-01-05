﻿// <auto-generated />
using System;
using Din.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Din.Infrastructure.DataAccess.Migrations
{
    [DbContext(typeof(DinContext))]
    [Migration("20220105110444_split_content_rating_foreign_key")]
    partial class split_content_rating_foreign_key
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Din.Domain.Models.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("active");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("hash");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("account_role");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("account", (string)null);
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.AccountAuthorizationCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("active");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("code");

                    b.Property<DateTime>("Generated")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("generated");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("AccountId");

                    b.ToTable("account_authorization_code", (string)null);
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.AccountImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)")
                        .HasColumnName("account_id");

                    b.Property<Guid?>("AccountId1")
                        .HasColumnType("char(36)");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("longblob")
                        .HasColumnName("data");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("AccountId1");

                    b.ToTable("account_image", (string)null);
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.AddedContent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("char(36)")
                        .HasColumnName("account_id");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("date_added");

                    b.Property<int>("ForeignId")
                        .HasColumnType("int")
                        .HasColumnName("foreign_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("status");

                    b.Property<int>("SystemId")
                        .HasColumnType("int")
                        .HasColumnName("system_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("title");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("AccountId");

                    b.ToTable("added_content", (string)null);
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.ContentPollStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ContentId")
                        .HasColumnType("char(36)")
                        .HasColumnName("content_id");

                    b.Property<DateTime>("PlexUrlPolled")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("plex_url_polled");

                    b.Property<DateTime>("PosterUrlPolled")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("poster_url_polled");

                    b.HasKey("Id")
                        .HasName("id");

                    b.ToTable("content_poll_status", (string)null);
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.ContentRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("MovieId")
                        .HasColumnType("char(36)")
                        .HasColumnName("movie_id");

                    b.Property<Guid?>("TvShowId")
                        .HasColumnType("char(36)")
                        .HasColumnName("tvshow_id");

                    b.Property<double>("Value")
                        .HasColumnType("double")
                        .HasColumnName("value");

                    b.Property<int>("Votes")
                        .HasColumnType("int")
                        .HasColumnName("votes");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("MovieId")
                        .IsUnique();

                    b.HasIndex("TvShowId")
                        .IsUnique();

                    b.ToTable("content_rating", (string)null);
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.Episode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("AirDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("air_date");

                    b.Property<int>("EpisodeNumber")
                        .HasColumnType("int")
                        .HasColumnName("episode_number");

                    b.Property<bool>("HasFile")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("has_file");

                    b.Property<bool>("Monitored")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("monitored");

                    b.Property<string>("Overview")
                        .HasColumnType("longtext")
                        .HasColumnName("overview");

                    b.Property<int>("SeasonNumber")
                        .HasColumnType("int")
                        .HasColumnName("season_number");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("title");

                    b.Property<Guid>("TvShowId")
                        .HasColumnType("char(36)")
                        .HasColumnName("tvshow_id");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("TvShowId");

                    b.ToTable("episode", (string)null);
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("genre", (string)null);
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.LoginAttempt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Browser")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("browser");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("date");

                    b.Property<string>("Device")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("device");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("char(36)")
                        .HasColumnName("location_id");

                    b.Property<string>("Os")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("os");

                    b.Property<string>("PublicIp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("public_ip");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("status");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("LocationId");

                    b.ToTable("login_attempt", (string)null);
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.LoginLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("City")
                        .HasColumnType("longtext")
                        .HasColumnName("city");

                    b.Property<string>("ContinentCode")
                        .HasColumnType("longtext")
                        .HasColumnName("continent_code");

                    b.Property<string>("ContinentName")
                        .HasColumnType("longtext")
                        .HasColumnName("continent_name");

                    b.Property<string>("CountryCode")
                        .HasColumnType("longtext")
                        .HasColumnName("country_code");

                    b.Property<string>("CountryName")
                        .HasColumnType("longtext")
                        .HasColumnName("country_name");

                    b.Property<string>("Latitude")
                        .HasColumnType("longtext")
                        .HasColumnName("latitude");

                    b.Property<string>("Longitude")
                        .HasColumnType("longtext")
                        .HasColumnName("longitude");

                    b.Property<string>("RegionCode")
                        .HasColumnType("longtext")
                        .HasColumnName("region_code");

                    b.Property<string>("RegionName")
                        .HasColumnType("longtext")
                        .HasColumnName("region_name");

                    b.Property<string>("ZipCode")
                        .HasColumnType("longtext")
                        .HasColumnName("zip_code");

                    b.HasKey("Id")
                        .HasName("id");

                    b.ToTable("login_location", (string)null);
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Added")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("added");

                    b.Property<bool>("Downloaded")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("downloaded");

                    b.Property<bool>("HasFile")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("has_file");

                    b.Property<string>("ImdbId")
                        .HasColumnType("longtext")
                        .HasColumnName("imdb_id");

                    b.Property<DateTime>("InCinemas")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("in_cinemas");

                    b.Property<string>("Overview")
                        .HasColumnType("longtext")
                        .HasColumnName("overview");

                    b.Property<DateTime>("PhysicalRelease")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("physical_release");

                    b.Property<string>("PlexUrl")
                        .HasColumnType("longtext")
                        .HasColumnName("plex_url");

                    b.Property<string>("PosterPath")
                        .HasColumnType("longtext")
                        .HasColumnName("poster_path");

                    b.Property<string>("Status")
                        .HasColumnType("longtext")
                        .HasColumnName("status");

                    b.Property<string>("Studio")
                        .HasColumnType("longtext")
                        .HasColumnName("studio");

                    b.Property<int>("SystemId")
                        .HasColumnType("int")
                        .HasColumnName("system_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("title");

                    b.Property<int>("TmdbId")
                        .HasColumnType("int")
                        .HasColumnName("tmdb_id");

                    b.Property<string>("Year")
                        .HasColumnType("longtext")
                        .HasColumnName("year");

                    b.Property<string>("YoutubeTrailerId")
                        .HasColumnType("longtext")
                        .HasColumnName("youtube_trailer_id");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("SystemId")
                        .IsUnique();

                    b.ToTable("movie", (string)null);
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AccountIdentity")
                        .HasColumnType("char(36)")
                        .HasColumnName("account_id");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("creation_date");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("token");

                    b.HasKey("Id")
                        .HasName("id");

                    b.ToTable("refresh_token", (string)null);
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.Season", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("EpisodeCount")
                        .HasColumnType("int")
                        .HasColumnName("episode_count");

                    b.Property<int>("SeasonsNumber")
                        .HasColumnType("int")
                        .HasColumnName("season_number");

                    b.Property<int>("TotalEpisodeCount")
                        .HasColumnType("int")
                        .HasColumnName("total_episode_count");

                    b.Property<Guid>("TvShowId")
                        .HasColumnType("char(36)")
                        .HasColumnName("tvshow_id");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("TvShowId");

                    b.ToTable("season", (string)null);
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.TvShow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Added")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("added");

                    b.Property<string>("AirTime")
                        .HasColumnType("longtext")
                        .HasColumnName("air_time");

                    b.Property<bool>("Downloaded")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("downloaded");

                    b.Property<int>("EpisodeCount")
                        .HasColumnType("int")
                        .HasColumnName("episode_count");

                    b.Property<DateTime>("FirstAired")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("first_aired");

                    b.Property<bool>("HasFile")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("has_file");

                    b.Property<string>("ImdbId")
                        .HasColumnType("longtext")
                        .HasColumnName("imdb_id");

                    b.Property<string>("Network")
                        .HasColumnType("longtext")
                        .HasColumnName("network");

                    b.Property<string>("Overview")
                        .HasColumnType("longtext")
                        .HasColumnName("overview");

                    b.Property<string>("PlexUrl")
                        .HasColumnType("longtext")
                        .HasColumnName("plex_url");

                    b.Property<string>("PosterPath")
                        .HasColumnType("longtext")
                        .HasColumnName("poster_path");

                    b.Property<int>("SeasonCount")
                        .HasColumnType("int")
                        .HasColumnName("season_count");

                    b.Property<string>("Status")
                        .HasColumnType("longtext")
                        .HasColumnName("status");

                    b.Property<int>("SystemId")
                        .HasColumnType("int")
                        .HasColumnName("system_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("title");

                    b.Property<int>("TotalEpisodeCount")
                        .HasColumnType("int")
                        .HasColumnName("total_episode_count");

                    b.Property<int>("TvdbId")
                        .HasColumnType("int")
                        .HasColumnName("tvdb_id");

                    b.Property<string>("Year")
                        .HasColumnType("longtext")
                        .HasColumnName("year");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("SystemId")
                        .IsUnique();

                    b.ToTable("tv_show", (string)null);
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.TvShowGenre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("char(36)")
                        .HasColumnName("genre_id");

                    b.Property<Guid>("TvshowId")
                        .HasColumnType("char(36)")
                        .HasColumnName("tvshow_id");

                    b.HasKey("Id")
                        .HasName("id");

                    b.HasIndex("GenreId");

                    b.HasIndex("TvshowId");

                    b.ToTable("tv_show_genre", (string)null);
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.AccountAuthorizationCode", b =>
                {
                    b.HasOne("Din.Domain.Models.Entities.Account", "Account")
                        .WithMany("Codes")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.AccountImage", b =>
                {
                    b.HasOne("Din.Domain.Models.Entities.Account", null)
                        .WithOne("Image")
                        .HasForeignKey("Din.Domain.Models.Entities.AccountImage", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Din.Domain.Models.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId1");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.AddedContent", b =>
                {
                    b.HasOne("Din.Domain.Models.Entities.Account", "Account")
                        .WithMany("AddedContent")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.ContentRating", b =>
                {
                    b.HasOne("Din.Domain.Models.Entities.Movie", null)
                        .WithOne("Ratings")
                        .HasForeignKey("Din.Domain.Models.Entities.ContentRating", "MovieId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Din.Domain.Models.Entities.TvShow", null)
                        .WithOne("Ratings")
                        .HasForeignKey("Din.Domain.Models.Entities.ContentRating", "TvShowId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.Episode", b =>
                {
                    b.HasOne("Din.Domain.Models.Entities.TvShow", "TvShow")
                        .WithMany("Episodes")
                        .HasForeignKey("TvShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TvShow");
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.LoginAttempt", b =>
                {
                    b.HasOne("Din.Domain.Models.Entities.LoginLocation", "Location")
                        .WithMany("LoginAttempts")
                        .HasForeignKey("LocationId");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.Season", b =>
                {
                    b.HasOne("Din.Domain.Models.Entities.TvShow", null)
                        .WithMany("Seasons")
                        .HasForeignKey("TvShowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.TvShowGenre", b =>
                {
                    b.HasOne("Din.Domain.Models.Entities.Genre", "Genre")
                        .WithMany("TvShowGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Din.Domain.Models.Entities.TvShow", "TvShow")
                        .WithMany("Genres")
                        .HasForeignKey("TvshowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("TvShow");
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.Account", b =>
                {
                    b.Navigation("AddedContent");

                    b.Navigation("Codes");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.Genre", b =>
                {
                    b.Navigation("TvShowGenres");
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.LoginLocation", b =>
                {
                    b.Navigation("LoginAttempts");
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.Movie", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("Din.Domain.Models.Entities.TvShow", b =>
                {
                    b.Navigation("Episodes");

                    b.Navigation("Genres");

                    b.Navigation("Ratings");

                    b.Navigation("Seasons");
                });
#pragma warning restore 612, 618
        }
    }
}
