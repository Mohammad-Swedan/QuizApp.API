using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QuizForAndroid.DAL.Entities;

namespace QuizForAndroid.DAL.Contexts;

public partial class QuizAppDbContext : DbContext
{
    public QuizAppDbContext()
    {
    }

    public QuizAppDbContext(DbContextOptions<QuizAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Choice> Choices { get; set; }

    public virtual DbSet<College> Colleges { get; set; }

    public virtual DbSet<Draft> Drafts { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<PendingWriterApplication> PendingWriterApplications { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionTypeInfo> QuestionTypeInfos { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<QuizLikesDislike> QuizLikesDislikes { get; set; }

    public virtual DbSet<QuizPopularity> QuizPopularities { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WriterApplication> WriterApplications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        optionsBuilder.UseSqlServer("Server=DESKTOP-OINSVUL\\SQLSERVER;Database=QuizForAndroid;User Id=sa;Password=123456;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Choice>(entity =>
        {
            entity.HasKey(e => e.ChoiceId).HasName("PK__Choices__76F51686BD01D378");

            entity.HasOne(d => d.Question).WithMany(p => p.Choices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Choices__Questio__5EBF139D");
        });

        modelBuilder.Entity<College>(entity =>
        {
            entity.HasKey(e => e.CollegeId).HasName("PK__Colleges__294095192E4C90E1");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.University).WithMany(p => p.Colleges)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Colleges__Univer__3E52440B");
        });

        modelBuilder.Entity<Draft>(entity =>
        {
            entity.HasKey(e => e.DraftId).HasName("PK__Drafts__3E93D63BE72B0711");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Drafts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Drafts__QuizID__656C112C");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__Material__C506131773962DC6");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.College).WithMany(p => p.Materials)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Materials__Colle__4D94879B");
        });

        modelBuilder.Entity<PendingWriterApplication>(entity =>
        {
            entity.ToView("PendingWriterApplications");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06F8C543A4A48");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Questions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Questions__QuizI__59FA5E80");
        });

        modelBuilder.Entity<QuestionTypeInfo>(entity =>
        {
            entity.HasKey(e => e.QuestionTypeId).HasName("PK__Question__7EDFF911433D551E");

            entity.Property(e => e.QuestionTypeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.QuizId).HasName("PK__Quizzes__8B42AE6EDA3B402B");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Material).WithMany(p => p.Quizzes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Quizzes__Materia__5441852A");

            entity.HasOne(d => d.Writer).WithMany(p => p.Quizzes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Quizzes__WriterI__534D60F1");
        });

        modelBuilder.Entity<QuizLikesDislike>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.QuizId }).HasName("PK__QuizLike__EF3CE64A82C55CFC");

            entity.HasOne(d => d.Quiz).WithMany(p => p.QuizLikesDislikes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuizLikes__QuizI__628FA481");

            entity.HasOne(d => d.User).WithMany(p => p.QuizLikesDislikes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuizLikes__UserI__619B8048");
        });

        modelBuilder.Entity<QuizPopularity>(entity =>
        {
            entity.ToView("QuizPopularity");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A7393B002");
        });

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.HasKey(e => e.SpecializationId).HasName("PK__Speciali__5809D84F9753427C");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.College).WithMany(p => p.Specializations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Specializ__Colle__412EB0B6");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.UniversityId).HasName("PK__Universi__9F19E19C4193A149");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACEEB9558A");

            entity.Property(e => e.RoleId).HasDefaultValue(1);

            entity.HasOne(d => d.College).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__CollegeID__48CFD27E");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleID__4AB81AF0");

            entity.HasOne(d => d.Specialization).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__Specializ__49C3F6B7");

            entity.HasOne(d => d.University).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__Universit__47DBAE45");
        });

        modelBuilder.Entity<WriterApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__WriterAp__C93A4F79EF163886");

            entity.Property(e => e.ApplicationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).HasDefaultValue("Pending");

            entity.HasOne(d => d.ReviewedByNavigation).WithMany(p => p.WriterApplicationReviewedByNavigations).HasConstraintName("FK__WriterApp__Revie__787EE5A0");

            entity.HasOne(d => d.User).WithMany(p => p.WriterApplicationUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WriterApp__UserI__778AC167");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
