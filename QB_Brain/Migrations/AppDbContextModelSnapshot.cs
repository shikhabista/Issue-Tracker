﻿// <auto-generated />
using System;
using IT_Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IT_Web.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Base.Entities.Branch", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<long>("Code")
                        .HasColumnType("bigint")
                        .HasColumnName("code");

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("contact_no");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_branch");

                    b.ToTable("branch", "Base");
                });

            modelBuilder.Entity("Base.Entities.Issue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("AssigneeId")
                        .HasColumnType("bigint")
                        .HasColumnName("assignee_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("IssueStatus")
                        .HasColumnType("integer")
                        .HasColumnName("issue_status");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated");

                    b.Property<long>("RepositoryId")
                        .HasColumnType("bigint")
                        .HasColumnName("repository_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_issue");

                    b.HasIndex("AssigneeId")
                        .HasDatabaseName("ix_issue_assignee_id");

                    b.HasIndex("RepositoryId")
                        .HasDatabaseName("ix_issue_repository_id");

                    b.ToTable("issue", "it");
                });

            modelBuilder.Entity("Base.Entities.IssueLabel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("IssueId")
                        .HasColumnType("bigint")
                        .HasColumnName("issue_id");

                    b.Property<long>("LabelId")
                        .HasColumnType("bigint")
                        .HasColumnName("label_id");

                    b.Property<long>("RecDate")
                        .HasColumnType("bigint")
                        .HasColumnName("rec_date");

                    b.HasKey("Id")
                        .HasName("pk_issue_label");

                    b.HasIndex("IssueId")
                        .HasDatabaseName("ix_issue_label_issue_id");

                    b.HasIndex("LabelId")
                        .HasDatabaseName("ix_issue_label_label_id");

                    b.ToTable("issue_label", "it");
                });

            modelBuilder.Entity("Base.Entities.Label", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("RecDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("rec_date");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_label");

                    b.ToTable("label", "it");
                });

            modelBuilder.Entity("Base.Entities.Organization", b =>
                {
                    b.Property<string>("ItemKey")
                        .HasColumnType("text")
                        .HasColumnName("item_key");

                    b.Property<string>("ItemValue")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("item_value");

                    b.HasKey("ItemKey")
                        .HasName("pk_organization_info");

                    b.ToTable("organization_info", "Base");
                });

            modelBuilder.Entity("Base.Entities.Repository", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<long>("RecById")
                        .HasColumnType("bigint")
                        .HasColumnName("rec_by_id");

                    b.Property<DateTime>("RecDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("rec_date");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("Visibility")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("visibility");

                    b.HasKey("Id")
                        .HasName("pk_repository");

                    b.HasIndex("RecById")
                        .HasDatabaseName("ix_repository_rec_by_id");

                    b.ToTable("repository", "it");
                });

            modelBuilder.Entity("Base.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<long>("BranchId")
                        .HasColumnType("bigint")
                        .HasColumnName("branch_id");

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("contact_no");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedEmail")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("SecurityStamp")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.HasIndex("BranchId")
                        .HasDatabaseName("ix_user_branch_id");

                    b.ToTable("user", "Base");
                });

            modelBuilder.Entity("Base.Entities.Issue", b =>
                {
                    b.HasOne("Base.Entities.User", "Assignee")
                        .WithMany()
                        .HasForeignKey("AssigneeId")
                        .HasConstraintName("fk_issue_user_assignee_id");

                    b.HasOne("Base.Entities.Repository", "Repository")
                        .WithMany()
                        .HasForeignKey("RepositoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_issue_repository_repository_id");

                    b.Navigation("Assignee");

                    b.Navigation("Repository");
                });

            modelBuilder.Entity("Base.Entities.IssueLabel", b =>
                {
                    b.HasOne("Base.Entities.Issue", "Issue")
                        .WithMany()
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_issue_label_issue_issue_id");

                    b.HasOne("Base.Entities.Label", "Label")
                        .WithMany()
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_issue_label_label_label_id");

                    b.Navigation("Issue");

                    b.Navigation("Label");
                });

            modelBuilder.Entity("Base.Entities.Repository", b =>
                {
                    b.HasOne("Base.Entities.User", "RecBy")
                        .WithMany()
                        .HasForeignKey("RecById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_repository_user_rec_by_id");

                    b.Navigation("RecBy");
                });

            modelBuilder.Entity("Base.Entities.User", b =>
                {
                    b.HasOne("Base.Entities.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_branch_branch_id");

                    b.Navigation("Branch");
                });
#pragma warning restore 612, 618
        }
    }
}
