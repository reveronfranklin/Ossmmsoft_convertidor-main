# Correccion de OP y preservacion de comprobantes

Se mantienen los endpoints POST `/api/AdmOrdenPago/Retornar`,
`/api/AdmOrdenPago/Update` y `/api/AdmOrdenPago/Aprobar`.
Retornar y Aprobar reciben `{"codigoOrdenPago": 123}` y conservan sus validaciones.

`Update` conserva su cuerpo actual; `numeroComprobante`, `numeroComprobante2`,
`numeroComprobante3` y `numeroComprobante4` son opcionales para la correccion.
El formulario omite esos campos. Si un cliente anterior los envia vacios, cero
u otro numero, el backend conserva todo numero positivo ya persistido.
Si el campo no tiene numero, se mantiene la asignacion inicial existente.
La fecha original se conserva cuando ya existe el comprobante principal.
Una fecha omitida se acepta si ya existe fecha guardada.

Respuesta: `ResultDto<AdmOrdenPagoResponseDto>`, con `isValid`, `message` y `data`
que incluye los numeros conservados. No hay paginacion en estas operaciones.
Ejemplo parcial de respuesta de una correccion:

```json
{
  "isValid": true,
  "message": "",
  "data": {
    "codigoOrdenPago": 123,
    "numeroComprobante": 20260400000123,
    "numeroComprobante2": 20260400000124,
    "numeroComprobante3": 20260400000125,
    "numeroComprobante4": 20260400000126
  }
}
```

Reaprobar reutiliza el IVA existente. Los reportes mantienen su flujo de visor.
Anular sigue siendo una operacion diferente; este contrato no renumera ni
recupera comprobantes historicos.
