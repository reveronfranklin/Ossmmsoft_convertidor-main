using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Convertidor.Migrations.DestinoMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConceptosRetenciones",
                columns: table => new
                {
                    CODIGO_CONCEPTO = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptosRetenciones", x => new { x.CODIGO_CONCEPTO, x.Titulo });
                });

            migrationBuilder.CreateTable(
                name: "HistoricoRetenciones",
                columns: table => new
                {
                    CODIGO_HISTORICO_NOMINA = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    TIPO_NOMINA = table.Column<int>(type: "integer", nullable: false),
                    UNIDAD_EJECUTORA = table.Column<string>(type: "text", nullable: false),
                    DESCRIPCION_CARGO = table.Column<string>(type: "text", nullable: false),
                    NACIONALIDAD = table.Column<string>(type: "text", nullable: false),
                    CEDULA = table.Column<int>(type: "integer", nullable: false),
                    NOMBRE = table.Column<string>(type: "text", nullable: false),
                    APELLIDO = table.Column<string>(type: "text", nullable: false),
                    FECHA_NOMINA = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FECHA_INGRESO = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MONTO = table.Column<decimal>(type: "numeric", nullable: false),
                    SUELDO = table.Column<decimal>(type: "numeric", nullable: false),
                    FECHA_INS = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoRetenciones", x => x.CODIGO_HISTORICO_NOMINA);
                });

            migrationBuilder.CreateTable(
                name: "IndiceCategoriaPrograma",
                columns: table => new
                {
                    CODIGO_ICP = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    ANO = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    ESCENARIO = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    CODIGO_SECTOR = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false, defaultValueSql: "('')"),
                    CODIGO_PROGRAMA = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false, defaultValueSql: "('')"),
                    CODIGO_SUBPROGRAMA = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false, defaultValueSql: "('')"),
                    CODIGO_PROYECTO = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false, defaultValueSql: "('')"),
                    CODIGO_ACTIVIDAD = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false, defaultValueSql: "('')"),
                    DENOMINACION = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, defaultValueSql: "('')"),
                    UNIDAD_EJECUTORA = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, defaultValueSql: "('')"),
                    DESCRIPCION = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false, defaultValueSql: "('')"),
                    CODIGO_FUNCIONARIO = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    FECHA_INI = table.Column<DateTime>(type: "date", nullable: false),
                    FECHA_FIN = table.Column<DateTime>(type: "date", nullable: false),
                    EXTRA1 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    EXTRA2 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    EXTRA3 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    USUARIO_INS = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    FECHA_INS = table.Column<DateTime>(type: "date", nullable: false),
                    USUARIO_UPD = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    FECHA_UPD = table.Column<DateTime>(type: "date", nullable: false),
                    CODIGO_EMPRESA = table.Column<int>(type: "integer", nullable: false),
                    CODIGO_OFICINA = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false, defaultValueSql: "('')"),
                    CODIGO_PRESUPUESTO = table.Column<decimal>(type: "numeric(10,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndiceCategoriaPrograma", x => x.CODIGO_ICP);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoPersonalCargo",
                columns: table => new
                {
                    CODIGO_EMPRESA = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    CODIGO_PERSONA = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    CODIGO_TIPO_NOMINA = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    CODIGO_PERIODO = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    CEDULA = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    FOTO = table.Column<string>(type: "character varying(44)", maxLength: 44, nullable: false, defaultValueSql: "('')"),
                    NOMBRE = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    APELLIDO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    NACIONALIDAD = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false, defaultValueSql: "('')"),
                    DESCRIPCION_NACIONALIDAD = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false, defaultValueSql: "('')"),
                    SEXO = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false, defaultValueSql: "('')"),
                    STATUS = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false, defaultValueSql: "('')"),
                    DESCRIPCION_STATUS = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false, defaultValueSql: "('')"),
                    DESCRIPCION_SEXO = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false, defaultValueSql: "('')"),
                    CODIGO_RELACION_CARGO = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    CODIGO_CARGO = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    CARGO_CODIGO = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false, defaultValueSql: "('')"),
                    CODIGO_ICP = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    CODIGO_ICP_UBICACION = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    SUELDO = table.Column<decimal>(type: "numeric", nullable: false),
                    DESCRIPCION_CARGO = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    TIPO_NOMINA = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    FRECUENCIA_PAGO_ID = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    CODIGO_SECTOR = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false, defaultValueSql: "('')"),
                    CODIGO_PROGRAMA = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false, defaultValueSql: "('')"),
                    TIPO_CUENTA_ID = table.Column<decimal>(type: "numeric(5,0)", nullable: false),
                    DESCRIPCION_TIPO_CUENTA = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    BANCO_ID = table.Column<decimal>(type: "numeric(5,0)", nullable: false),
                    DESCRIPCION_BANCO = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    NO_CUENTA = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValueSql: "('')"),
                    EXTRA1 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    EXTRA2 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    EXTRA3 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    USUARIO_INS = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    FECHA_INS = table.Column<DateTime>(type: "date", nullable: false),
                    USUARIO_UPD = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    FECHA_UPD = table.Column<DateTime>(type: "date", nullable: false),
                    FECHA_NOMINA = table.Column<DateTime>(type: "date", nullable: false),
                    FECHA_INGRESO = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoPersonalCargo", x => new { x.CODIGO_EMPRESA, x.CODIGO_TIPO_NOMINA, x.CODIGO_PERIODO, x.CODIGO_PERSONA });
                    table.ForeignKey(
                        name: "FK_IndiceCategoriaPrograma_HistoricoPersonalCargo",
                        column: x => x.CODIGO_ICP,
                        principalTable: "IndiceCategoriaPrograma",
                        principalColumn: "CODIGO_ICP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoNomina",
                columns: table => new
                {
                    CODIGO_HISTORICO_NOMINA = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CODIGO_EMPRESA = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    CODIGO_PERIODO = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    CODIGO_TIPO_NOMINA = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    CODIGO_PERSONA = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    FECHA_NOMINA = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CODIGO_CONCEPTO = table.Column<long>(type: "bigint", nullable: false),
                    COMPLEMENTO_CONCEPTO = table.Column<string>(type: "text", nullable: false),
                    TIPO = table.Column<string>(type: "text", nullable: false),
                    FRECUENCIA_ID = table.Column<long>(type: "bigint", nullable: false),
                    MONTO = table.Column<decimal>(type: "numeric", nullable: false),
                    STATUS = table.Column<string>(type: "text", nullable: false),
                    EXTRA1 = table.Column<string>(type: "text", nullable: false),
                    EXTRA2 = table.Column<string>(type: "text", nullable: false),
                    EXTRA3 = table.Column<string>(type: "text", nullable: false),
                    USUARIO_INS = table.Column<long>(type: "bigint", nullable: false),
                    FECHA_INS = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    USUARIO_UPD = table.Column<long>(type: "bigint", nullable: false),
                    FECHA_UPD = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoNomina", x => x.CODIGO_HISTORICO_NOMINA);
                    table.ForeignKey(
                        name: "FK_HistoricoNomina_HistoricoPersonalCargo",
                        columns: x => new { x.CODIGO_EMPRESA, x.CODIGO_TIPO_NOMINA, x.CODIGO_PERIODO, x.CODIGO_PERSONA },
                        principalTable: "HistoricoPersonalCargo",
                        principalColumns: new[] { "CODIGO_EMPRESA", "CODIGO_TIPO_NOMINA", "CODIGO_PERIODO", "CODIGO_PERSONA" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IDX_CODIGO_HISTORICO_NOMINA",
                table: "HistoricoNomina",
                column: "CODIGO_HISTORICO_NOMINA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoNomina_CODIGO_EMPRESA_CODIGO_TIPO_NOMINA_CODIGO_PE~",
                table: "HistoricoNomina",
                columns: new[] { "CODIGO_EMPRESA", "CODIGO_TIPO_NOMINA", "CODIGO_PERIODO", "CODIGO_PERSONA" });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoPersonalCargo_CODIGO_ICP",
                table: "HistoricoPersonalCargo",
                column: "CODIGO_ICP");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConceptosRetenciones");

            migrationBuilder.DropTable(
                name: "HistoricoNomina");

            migrationBuilder.DropTable(
                name: "HistoricoRetenciones");

            migrationBuilder.DropTable(
                name: "HistoricoPersonalCargo");

            migrationBuilder.DropTable(
                name: "IndiceCategoriaPrograma");
        }
    }
}
