﻿using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.EntityMappers;
using Microsoft.EntityFrameworkCore;
namespace DAL.ApplicationDbContext
{
	public class AppDbContext:DbContext
	{
		public AppDbContext(DbContextOptions options) :base(options)
		{

		}
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserMap());
		}
	}
}
