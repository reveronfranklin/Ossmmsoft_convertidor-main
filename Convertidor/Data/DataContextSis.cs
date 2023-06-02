﻿


using CConvertidor.Data.Entities.Sis;
using Convertidor.Data.Entities.RentasMunicipales;
using Convertidor.Data.Entities.Sis;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data
{
    public class DataContextSis : DbContext
    {
        public DataContextSis(DbContextOptions<DataContextSis> options) : base(options)
        {

        }
        public DbSet<SIS_USUARIOS> SIS_USUARIOS { get; set; }
        public DbSet<SIS_V_SISTEMA_USUARIO_PROGRAMA> SIS_V_SISTEMA_USUARIO_PROGRAMA { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            base.OnModelCreating(modelBuilder);


            modelBuilder
            .Entity<SIS_V_SISTEMA_USUARIO_PROGRAMA>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("SIS_V_SISTEMA_USUARIO_PROGRAMA");
            })
          .Entity<SIS_USUARIOS>(builder =>
          {
              //builder.HasNoKey();
              builder.HasKey(table => new
              {
                  table.CODIGO_USUARIO,

              });
              builder.ToTable("SIS_USUARIOS");
          });


        }



    }
}
