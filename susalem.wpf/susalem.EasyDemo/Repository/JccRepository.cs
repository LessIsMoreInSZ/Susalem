﻿using Microsoft.EntityFrameworkCore;
using susalem.EasyDemo.Entities;
using susalem.EasyDemo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace susalem.EasyDemo.Repository
{
    internal class JccRepository : DbContext
    {
        private string strDb = $"Data Source = {Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQLite\\sqlite.db")}";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrWhiteSpace(strDb))
            {
                optionsBuilder.UseSqlite(strDb);
            }
        }

        /// <summary>
        /// 报警
        /// </summary>
        //public DbSet<AlarmModel>? Alarms { get; set; }


        //public DbSet<AlarmModel>? Alarms { get; set; }

        public DbSet<UserModel>? Users { get; set; }

        public DbSet<RoleModel>? Roles { get; set; }

        public DbSet<ChemicalParaModel>? ChemicalParas { get; set; }

        public DbSet<CabinetInfoModel>? CabinetInfos { get; set; }

        public DbSet<HistoryModel>? Historys { get; set; }
    }
}
