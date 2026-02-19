using ASP_NET_20._TaskFlow_FIle_attachment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_20._TaskFlow_FIle_attachment.Data;

public class TaskFlowDBContext : IdentityDbContext<ApplicationUser>
{
    public TaskFlowDBContext(DbContextOptions options)
        : base(options)
    { }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<ProjectMember> ProjectMembers => Set<ProjectMember>();
    public DbSet<TaskAttachment> TaskAttachments => Set<TaskAttachment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Project
        modelBuilder.Entity<Project>(
            entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(p => p.Description)
                    .IsRequired()
                    .HasMaxLength(1000);
                entity.Property(p => p.CreatedAt)
                    .IsRequired();
                entity.Property(p => p.OwnerId)
                     .IsRequired()
                     .HasMaxLength(450);
                entity.HasOne(p => p.Owner)
                      .WithMany()
                      .HasForeignKey(p => p.OwnerId)
                      .OnDelete(DeleteBehavior.Restrict);
            }
            );


        // TaskItem
        modelBuilder.Entity<TaskItem>(
            entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Title)
                     .IsRequired()
                     .HasMaxLength(200);
                entity.Property(t => t.Description)
                    .IsRequired()
                    .HasMaxLength(1000);
                entity.Property(t => t.CreatedAt)
                    .IsRequired();
                entity.Property(t => t.Status)
                    .IsRequired();
                entity.Property(t => t.Priority)
                    .IsRequired();

                entity.HasOne(t => t.Project)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(e => e.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
            );

        // RefreshToken
        modelBuilder.Entity<RefreshToken>(
            entity =>
            {
                entity
                    .HasKey(e => e.Id);

                entity
                    .HasIndex(e => e.JwtId)
                    .IsUnique();

                entity
                    .Property(e => e.JwtId)
                    .IsRequired()
                    .HasMaxLength(64);

                entity
                    .Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);
            }
            );

        // ProjectMember
        modelBuilder.Entity<ProjectMember>(
            entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.UserId });
                entity.HasOne(e => e.Project)
                    .WithMany(p => p.Members)
                    .HasForeignKey(e => e.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.ProjectMemberships)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.UserId).HasMaxLength(450);
            }
            );

        // TaskAttachment
        modelBuilder.Entity<TaskAttachment>(
            entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.OriginalFileName)
                      .IsRequired()
                      .HasMaxLength(500);

                entity.Property(e => e.StoredFileName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.ContentType)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.UploadedByUserId)
                      .IsRequired()
                      .HasMaxLength(450);

                entity.HasOne(e => e.TaskItem)
                      .WithMany(t => t.Attachments)
                      .HasForeignKey(e => e.TaskItemId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.UploadedByUser)
                      .WithMany()
                      .HasForeignKey(e => e.UploadedByUserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
    }
}
