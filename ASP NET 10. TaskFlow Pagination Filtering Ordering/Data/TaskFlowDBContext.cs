using ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_10._TaskFlow_Pagination_Filtering_Ordering.Data;

public class TaskFlowDBContext : DbContext
{
    public TaskFlowDBContext(DbContextOptions options) 
        : base(options)
    { }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TaskItem> TaskItems => Set<TaskItem>();

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
            }
            );


        // TaskItem
        modelBuilder.Entity<TaskItem>(
            entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t=> t.Title)
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
    }
}
