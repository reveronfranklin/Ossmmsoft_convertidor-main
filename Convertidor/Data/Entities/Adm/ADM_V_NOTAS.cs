using System;
using System.ComponentModel.DataAnnotations; // Para [Keyless] si usas EF Core
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore; // Para [Table]

#nullable enable

namespace Convertidor.Data.Entities.Adm;

/// <summary>
/// Representa la vista ADM_V_NOTAS de Oracle.
/// Se usa [Keyless] porque las vistas no suelen tener una clave primaria en el modelo EF.
/// Se usa [Table] para mapear explícitamente a la vista de la base de datos.
/// </summary>
[Table("ADM_V_NOTAS")]
[Keyless] // Requiere 'using Microsoft.EntityFrameworkCore;'
public class ADM_V_NOTAS
{
    /// <summary>
    /// Código del lote de pago.
    /// Tipo de dato: NUMBER(X,0) en Oracle, mapeado a int en C#.
    /// </summary>
    public int CODIGO_LOTE_PAGO { get; set; }

    /// <summary>
    /// Código del pago/cheque.
    /// Tipo de dato: NUMBER(X,0) en Oracle, mapeado a int en C#.
    /// </summary>
    public int CODIGO_PAGO { get; set; }

    /// <summary>
    /// Número del pago/cheque.
    /// Tipo de dato: NUMBER(X,0) en Oracle, mapeado a int en C#.
    /// </summary>
    public int NUMERO_PAGO { get; set; }

    /// <summary>
    /// Fecha del pago.
    /// Tipo de dato: DATE en Oracle, mapeado a DateTime en C#.
    /// </summary>
    public DateTime FECHA_PAGO { get; set; }

    /// <summary>
    /// Nombre asociado al pago (ej. nombre del banco).
    /// Tipo de dato: VARCHAR2 en Oracle, mapeado a string? en C#.
    /// </summary>
    public string? NOMBRE { get; set; }

    /// <summary>
    /// Número de cuenta.
    /// Tipo de dato: VARCHAR2 en Oracle, mapeado a string? en C#.
    /// </summary>
    public string? NO_CUENTA { get; set; }

    /// <summary>
    /// Nombre de la entidad a la que se debe pagar (combinación de nombre/apellido o nombre de proveedor).
    /// Tipo de dato: VARCHAR2 en Oracle (resultado de NVL y concatenación), mapeado a string? en C#.
    /// </summary>
    public string? PAGAR_A_LA_ORDEN_DE { get; set; }

    /// <summary>
    /// Motivo del pago.
    /// Tipo de dato: VARCHAR2 en Oracle, mapeado a string? en C#.
    /// </summary>
    public string? MOTIVO { get; set; }

    /// <summary>
    /// Monto del pago.
    /// Tipo de dato: NUMBER(X,Y) en Oracle, mapeado a decimal en C#.
    /// </summary>
    public decimal MONTO { get; set; }

    /// <summary>
    /// Indicador de endoso (ej. 'NO ENDOSABLE' o NULL).
    /// Tipo de dato: VARCHAR2 en Oracle (resultado de DECODE), mapeado a string? en C#.
    /// </summary>
    public string? ENDOSO { get; set; }

    /// <summary>
    /// Usuario que insertó el registro.
    /// Tipo de dato: VARCHAR2 en Oracle (resultado de NVL), mapeado a string? en C#.
    /// </summary>
    public string? USUARIO_INS { get; set; }

    /// <summary>
    /// Código del proveedor.
    /// Tipo de dato: NUMBER(X,0) en Oracle, mapeado a int en C#.
    /// </summary>
    public int CODIGO_PROVEEDOR { get; set; }

    /// <summary>
    /// Detalle de la operación ICP/PUC (resultado de función).
    /// Tipo de dato: VARCHAR2 en Oracle, mapeado a string? en C#.
    /// </summary>
    public string? DETALLE_OP_ICP_PUC { get; set; }

    /// <summary>
    /// Monto de la operación ICP/PUC (resultado de to_number de función).
    /// Tipo de dato: NUMBER en Oracle, mapeado a decimal? en C#.
    /// </summary>
    public decimal? MONTO_OP_ICP_PUC { get; set; }

    /// <summary>
    /// Detalle de impuestos retenidos (resultado de función).
    /// Tipo de dato: VARCHAR2 en Oracle, mapeado a string? en C#.
    /// </summary>
    public string? DETALLE_IMP_RET { get; set; }

    /// <summary>
    /// Monto de impuestos retenidos (resultado de to_number de función).
    /// Tipo de dato: NUMBER en Oracle, mapeado a decimal? en C#.
    /// </summary>
    public decimal? MONTO_IMP_RET { get; set; }

    /// <summary>
    /// Campo extra/etiqueta 2.
    /// Tipo de dato: VARCHAR2 en Oracle, mapeado a string? en C#.
    /// </summary>
    public string? ETIQUETA2 { get; set; }

    /// <summary>
    /// Identificador del tipo de pago/cheque.
    /// Tipo de dato: NUMBER(10,0) en Oracle, mapeado a decimal? en C# para una conversión robusta.
    /// </summary>
    public decimal? TIPO_PAGO_ID { get; set; }

    /// <summary>
    /// Título del reporte.
    /// Tipo de dato: VARCHAR2 en Oracle, mapeado a string? en C#.
    /// </summary>
    public string? TITULO_REPORTE { get; set; }
}