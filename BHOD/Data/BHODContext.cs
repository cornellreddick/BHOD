using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BHOD.Models;
using Microsoft.EntityFrameworkCore;

namespace BHOD.Data
{
    public class BHODContext : DbContext
    {
        public BHODContext(DbContextOptions options) : base(options) { }
       
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<Hairstylist> Hairstylists { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PreBookedAppointments> PreBookedAppointmentses { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ShopHours> ShopHourses { get; set; }
        public DbSet<ShopPersonal> ShopPersonals { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<AppointmentHistory> AppointmentHistories { get; set; }
        public DbSet<Appointment> Appointmentses { get; set; }
        
    }
}
