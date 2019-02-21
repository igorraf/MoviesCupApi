using Microsoft.EntityFrameworkCore;
using MoviesCup.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoviesCupApiTest
{
    public static class DbContextFactory<TContext> where TContext : DbContext
    {

        public static TContext Memory()
        {
            var builder = new DbContextOptionsBuilder<TContext>();
            builder = builder.UseInMemoryDatabase("MemoryDb");

            return (TContext)Activator.CreateInstance(typeof(TContext), new object[] { builder.Options, "" });
        }
    }
}
