using Microsoft.EntityFrameworkCore;
using SnapFile.Domain.Entities;
using System.Reflection.Emit;

namespace SnapFile.Infrastructure.Data
{
    

    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmailCode> EmailCodes { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<TemplateVariable> TemplateVariables { get; set; }
        public DbSet<TemplateApprover> TemplateApprovers { get; set; }

        public DbSet<RequestType> RequestTypes { get; set; }

        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestValue> RequestValues { get; set; }
        public DbSet<RequestApprover> RequestApprovers { get; set; }

        public DbSet<Formulation> Formulations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasIndex(u => u.DepartmentId);

            modelBuilder.Entity<Request>()
                .HasIndex(r => r.CreatedByUserId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Phone)
                .IsUnique();

            modelBuilder.Entity<Department>()
                .HasOne(d => d.Head)
                .WithMany()
                .HasForeignKey(d => d.HeadId)
                .OnDelete(DeleteBehavior.SetNull);

            //Связи User
            modelBuilder.Entity<User>()
                .HasOne(u => u.Position)
                .WithMany(p => p.Users)
                .HasForeignKey(u => u.PositionId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            // Связи Template
            modelBuilder.Entity<Template>()
                .HasOne(t => t.RequestType)
                .WithMany(rt => rt.Templates)
                .HasForeignKey(t => t.RequestTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Template>()
                .HasOne(t => t.Formulation)
                .WithMany()
                .HasForeignKey(t => t.FormulationId)
                .OnDelete(DeleteBehavior.SetNull);

            // Связи TemplateVariable и TemplateApprover
            modelBuilder.Entity<TemplateVariable>()
                .HasOne(tv => tv.Template)
                .WithMany(t => t.Variables)
                .HasForeignKey(tv => tv.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TemplateApprover>()
                .HasOne(ta => ta.Template)
                .WithMany(t => t.Approvers)
                .HasForeignKey(ta => ta.TemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TemplateApprover>()
                .HasOne(ta => ta.User)
                .WithMany()
                .HasForeignKey(ta => ta.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связи Request
            modelBuilder.Entity<Request>()
                .HasOne(r => r.Template)
                .WithMany()
                .HasForeignKey(r => r.TemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.CreatedByUser)
                .WithMany()
                .HasForeignKey(r => r.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.RecipientUser)
                .WithMany()
                .HasForeignKey(r => r.RecipientUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.RecipientDepartment)
                .WithMany()
                .HasForeignKey(r => r.RecipientDepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Formulation)
                .WithMany()
                .HasForeignKey(r => r.FormulationId)
                .OnDelete(DeleteBehavior.SetNull);

            // Связи RequestValue
            modelBuilder.Entity<RequestValue>()
                .HasOne(rv => rv.Request)
                .WithMany(r => r.Values)
                .HasForeignKey(rv => rv.RequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RequestValue>()
                .HasOne(rv => rv.TemplateVariable)
                .WithMany()
                .HasForeignKey(rv => rv.TemplateVariableId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связи RequestApprover
            modelBuilder.Entity<RequestApprover>()
                .HasOne(ra => ra.Request)
                .WithMany(r => r.Approvers)
                .HasForeignKey(ra => ra.RequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RequestApprover>()
                .HasOne(ra => ra.User)
                .WithMany()
                .HasForeignKey(ra => ra.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связи RequestType
            modelBuilder.Entity<RequestType>()
                .HasMany(rt => rt.Templates)
                .WithOne(t => t.RequestType)
                .HasForeignKey(t => t.RequestTypeId);

            // Связи Formulation
            modelBuilder.Entity<Formulation>()
                .Property(f => f.Text)
                .IsRequired();

            // Установка первичного ключа для Formulation
            modelBuilder.Entity<Formulation>()
                .HasKey(f => f.Id);

        }
    }
}
