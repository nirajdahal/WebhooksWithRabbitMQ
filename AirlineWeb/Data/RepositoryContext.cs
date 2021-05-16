using AirlineWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineWeb.Data
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) { }

        public DbSet<FlightDetail> FlightDetailS{get; set;}
        public DbSet<WebhookSubscription> WebhookSubscriptions { get; set; }
    }
}
