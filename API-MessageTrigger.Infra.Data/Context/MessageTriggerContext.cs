using API_MessageTrigger.Domain.Entities;
using API_MessageTrigger.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace API_MessageTrigger.Infra.Data.Context
{
    public class MessageTriggerContext : DbContext
    {
        public MessageTriggerContext(DbContextOptions<MessageTriggerContext> options) : base(options)
        {

        }
        public DbSet<MessageTrigger> MessageTrigger { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MessageTrigger>(new MessageTriggerMap().Configure);
        }

    }


}
