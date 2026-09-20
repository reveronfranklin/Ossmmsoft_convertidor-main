# Compromisos pendientes para crear orden de pago

POST `/api/PreCompromisos/GetCompromisosPendientesByPresupuesto`
(el cliente usa `/PreCompromisos/GetCompromisosPendientesByPresupuesto` sobre su base API).

## Solicitud

```json
{
  "codigoPresupuesto": 2026,
  "pageNumber": 0,
  "pageSize": 5,
  "searchText": "cmp-00130"
}
```

`pageNumber` empieza en cero. Valores negativos se normalizan a cero y
`pageSize` no positivo usa 5. `searchText` vacio o nulo no filtra; se recortan
espacios y se buscan coincidencias parciales sin distinguir mayusculas en
numero de compromiso, nombre del proveedor o fecha visible `dd/MM/aaaa`.
Ejemplos: `cmp-00130`, `CORPOELEC`, `09/03/2026`, `03/2026`.

## Disponibilidad

Se ofrecen solamente compromisos aprobados (`AP`) con saldo por causar positivo en
`ADM_V_COMPROMISO_PENDIENTE` y sin asociacion en `ADM_COMPROMISO_OP` a una orden
de pago del mismo presupuesto cuyo estado sea distinto de anulado (`AN`).
La asociacion se compara por identificador y origen para evitar colisiones.
Una orden pendiente tambien reserva el compromiso. Una orden anulada permite
volver a ofrecerlo si sigue aprobado y con saldo disponible.

La consulta por compromiso utilizada por `AdmOrdenPago/Create` aplica la misma
exclusion; una seleccion que ya no esta disponible devuelve `isValid: false`
y el mensaje `COMPROMISO NO ESTA PENDIENTE`.

## Respuesta

Se conserva `ResultDto<List<PreCompromisosResponseDto>>`:

```json
{
  "data": [],
  "isValid": true,
  "message": "",
  "page": 0,
  "totalPage": 0,
  "cantidadRegistros": 0
}
```

Cada fila conserva `codigoCompromiso`, `numeroCompromiso`, `fechaCompromiso`,
`fechaCompromisoString`, `codigoProveedor`, `nombreProveedor`, `monto`,
`codigoPresupuesto`, `status` y los demas campos existentes del DTO.
`cantidadRegistros` cuenta resultados despues de filtrar y antes de paginar;
`totalPage` es el total de paginas. Orden: fecha descendente, luego codigo
descendente. Una busqueda sin coincidencias devuelve lista vacia valida.
Los errores conservan `isValid: false`, `data: null` y `message`.

El frontend reinicia la pagina al buscar, separa la cache por presupuesto,
consulta de nuevo al abrir el selector e invalida la lista al crear una orden.

## Verificacion funcional con datos

- Buscar numero completo/parcial, proveedor y fecha; limpiar debe recuperar la lista.
- Buscar desde otra pagina debe volver a la primera y actualizar el total.
- Crear una orden y reabrir el selector: el compromiso debe desaparecer.
- Intentar crear desde una seleccion antigua debe devolver compromiso no pendiente.
- Comprobar que una orden anulada no bloquea un compromiso aprobado con saldo.

Los compromisos `PE` no se ofrecen, aunque tengan saldo. `AdmOrdenPago/Create`
revalida el estado y rechaza un compromiso no aprobado incluso si se invoca
directamente o desde una seleccion anterior. Esta regla no depende de `status`
enviado por el frontend.
