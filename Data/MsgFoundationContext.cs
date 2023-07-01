using Microsoft.EntityFrameworkCore;
using MsgFoundation.Models;

namespace MsgFoundation.Data
{
    public class MsgFoundationContext : DbContext
    {
        public MsgFoundationContext(DbContextOptions<MsgFoundationContext> options) : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Payment> Payments => Set<Payment>();
    }   
}
