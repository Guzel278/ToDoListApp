
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Models;

namespace ToDoList.DataBase;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ToDoItemModel> ToDoItems { get; set; }
    public DbSet<PriorityModel> Priorities { get; set; }
    public DbSet<UserModel> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies(); // for lazy loading
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ToDoItemModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd();
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.IsCompleted).IsRequired();
            entity.Property(e => e.DueDate).IsRequired();

            entity.HasOne(e => e.Priority)
                  .WithMany(p => p.ToDoItems)
                  .HasForeignKey(e => e.PriorityId);

            entity.HasOne(e => e.User)
                  .WithMany(u => u.ToDoItems)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade); // to automatically delete tasks when you delete a user
        });

        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired();
                  

            entity.HasMany(e => e.ToDoItems)
                  .WithOne(e => e.User)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<PriorityModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                  .ValueGeneratedOnAdd();
            entity.Property(e => e.Level).IsRequired();

            entity.HasMany(e => e.ToDoItems)
                  .WithOne(e => e.Priority)
                  .HasForeignKey(e => e.PriorityId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

