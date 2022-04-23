using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChatApi.EF.Models;

namespace ChatApi.EF
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<GroupMessage> GroupMessages { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly set primary key
            modelBuilder.Entity<PrivateMessage>()
                .HasKey(e => e.PrivateMessageId);

            // Configure two foreign keys to one entity - User
            modelBuilder.Entity<PrivateMessage>()
                .HasOne(e => e.Sender)
                .WithMany(e => e.PrivateMessages)
                .HasForeignKey(e => e.SenderId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PrivateMessage>()
                .HasOne(e => e.Receiver)
                .WithMany(e => e.ResPrivateMessages)
                .HasForeignKey(e => e.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure self-reflecting one-to-many
            // Could not set foreign key to set null on delete
            // in case of self-reflecting table
            modelBuilder.Entity<PrivateMessage>()
                .HasOne(e => e.ResposeTo)
                .WithMany(e => e.Responses)
                .HasForeignKey(e => e.ResponseToId);



            // Explicitly set primary key
            modelBuilder.Entity<GroupMessage>()
                .HasKey(e => e.GroupMessageId);

            // Configure self-reflecting one-to-many
            // Could not set foreign key to set null on delete
            // in case of self-reflecting table
            modelBuilder.Entity<GroupMessage>()
                .HasOne(e => e.ResposeTo)
                .WithMany(e => e.Responses)
                .HasForeignKey(e => e.ResponseToId);

            // Soft deleted entities would not be found in any query
            modelBuilder.Entity<GroupMessage>()
                .HasQueryFilter(e => !e.IsSoftDeleted);
        }
    }
}
