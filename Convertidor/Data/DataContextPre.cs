using Convertidor.Data.Entities;
using Convertidor.Data.Entities.Presupuesto;
using Microsoft.EntityFrameworkCore;

namespace Convertidor.Data
{
    public class DataContextPre : DbContext
    {
        public DataContextPre(DbContextOptions<DataContextPre> options) : base(options)
        {

        }
        public DbSet<PRE_INDICE_CAT_PRG> PRE_INDICE_CAT_PRG { get; set; }

        public DbSet<PRE_PRESUPUESTOS> PRE_PRESUPUESTOS { get; set; }
        public DbSet<PRE_V_SALDOS> PRE_V_SALDOS { get; set; }
        public DbSet<PRE_V_DENOMINACION_PUC> PRE_V_DENOMINACION_PUC { get; set; }
        public DbSet<PRE_V_DENOMINACION_ICP_PUC> PRE_V_DENOMINACION_ICP_PUC { get; set; }
        



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            base.OnModelCreating(modelBuilder);
          

            modelBuilder
            .Entity<PRE_V_SALDOS>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("PRE_V_SALDOS");
            })
            .Entity<PRE_V_DENOMINACION_PUC>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("PRE_V_DENOMINACION_PUC");
            })
            .Entity<PRE_V_DENOMINACION_ICP_PUC>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("PRE_V_DENOMINACION_ICP_PUC");
            })
             .Entity<PRE_INDICE_CAT_PRG>(builder =>
             {
                 builder.HasNoKey();
                 builder.ToTable("PRE_INDICE_CAT_PRG");
             })
            .Entity<PRE_PRESUPUESTOS>(builder =>
            {
                //builder.HasNoKey();
                builder.HasKey(table => new
                {
                    table.CODIGO_PRESUPUESTO,

                });
                builder.ToTable("PRE_PRESUPUESTOS");
            });

        }



    }
}
