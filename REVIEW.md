# Revisión de Pares (Peer Review)

## Sistema de Gestión de Entradas de Inventario
**Asignatura:** Programación Aplicada I  
**Tecnología:** Blazor Server + .NET 10, SQL Server  
**Mecanismo de Seguridad:** ASP.NET Core Identity

---

## Información del Revisor
- **Nombre del Revisor:** [ROMEL MARTIN ORTEGA PICHARDO]
- **Fecha de Revisión:** [16/01/2026]

---

## Resumen Ejecutivo

He revisado el código fuente y probado la funcionalidad del Sistema de Gestión de Entradas de Inventario. A continuación, presento lo que eh encontrado siendo estos organizados por cada requerimiento de la asignación.

---

## 1. Requerimientos Técnicos

| Requerimiento | Estado | Observaciones |
|---------------|--------|---------------|
| Framework Blazor Server | ✅ Cumple | Implementado con AddInteractiveServerComponents() |
| SQL Server + EF Core (Code First) | ✅ Cumple | Migraciones presentes en carpeta /Migrations |
| ASP.NET Core Identity | ✅ Cumple | Login y Register funcionales |
| .gitignore adecuado | ✅ Cumple | Excluye bin, obj correctamente |

---

## 2. Módulo de Productos

| Funcionalidad | Estado | Observaciones |
|---------------|--------|---------------|
| Crear productos | ✅ Cumple | Formulario con validaciones |
| Editar productos | ✅ Cumple | Permite modificar todos los campos excepto Existencia |
| Eliminar productos | ✅ Cumple | Validación de entradas asociadas |
| Campo Existencia readonly | ✅ Cumple | Solo se modifica por Entradas |
| Validaciones (obligatorios, > 0) | ✅ Cumple | DataAnnotations implementadas |
| Filtros en Index | ✅ Cumple | Filtro por ProductoId y Descripción |

**Modelo de Productos verificado:**
```csharp
public class Productos
{
    public int ProductoId { get; set; }
    public string Descripcion { get; set; }
    public decimal Costo { get; set; }      // [Range(0.01, ...)]
    public decimal Precio { get; set; }     // [Range(0.01, ...)]
    public int Existencia { get; set; }     // Readonly en UI
}
```

---

## 3. Módulo de Entradas (Maestro-Detalle)

| Funcionalidad | Estado | Observaciones |
|---------------|--------|---------------|
| Entidad Maestra (Entrada) | ✅ Cumple | EntradaId, Fecha, Concepto, Total |
| Entidad Detalle (EntradaDetalle) | ✅ Cumple | Id, EntradaId, ProductoId, Cantidad, Costo |
| Costo readonly en detalle | ✅ Cumple | Se carga automáticamente del producto |
| Total calculado automáticamente | ✅ Cumple | Suma de (Cantidad × Costo) |
| Filtro por rango de fechas | ✅ Cumple | Campos FechaDesde y FechaHasta |
| Filtro por otros criterios | ✅ Cumple | EntradaId y Concepto |

---

## 4. Confirmación de Lógica de Inventario (CRÍTICO)

### 4.1 Al Crear (Guardar) una Entrada
- [x] **VERIFICADO:** Las cantidades ingresadas en el detalle se SUMAN a la Existencia de los productos correspondientes.

**Código verificado en EntradasService.cs línea 21:**
```csharp
await AfectarExistencia(entrada.EntradasDetalle.ToArray(), TipoOperacion.Suma);
```

### 4.2 Al Modificar (Editar) una Entrada
- [x] **VERIFICADO:** El sistema revierte las cantidades originales (resta) y aplica las nuevas (suma).
- [x] **VERIFICADO:** Si se elimina una fila del detalle durante la edición, esa cantidad se resta del inventario.
- [x] **VERIFICADO:** Si se agrega una fila nueva, se suma al inventario.

**Código verificado en EntradasService.cs líneas 34-36:**
```csharp
// Primero resta las cantidades anteriores
await AfectarExistencia(anterior.EntradasDetalle.ToArray(), TipoOperacion.Resta);
// Luego suma las nuevas cantidades
await AfectarExistencia(entrada.EntradasDetalle.ToArray(), TipoOperacion.Suma);
```

### 4.3 Al Eliminar una Entrada
- [x] **VERIFICADO:** Se reversa la operación completa. Las cantidades que entraron se restan del inventario de los productos.

**Código verificado en EntradasService.cs línea 93:**
```csharp
await AfectarExistencia(entrada.EntradasDetalle.ToArray(), TipoOperacion.Resta);
```

## 5. Estructura del Proyecto

```
EntradaDeInventario/
├── Components/
│   ├── Account/          
│   ├── Layout/           
│   └── Pages/
│       ├── ProductosPage/
│       │   ├── ProductosIndex.razor
│       │   ├── ProductosCreate.razor
│       │   └── ProductosEdit.razor
│       └── EntradasPage/
│           ├── EntradasIndex.razor
│           ├── EntradasCreate.razor
│           └── EntradasEdit.razor
├── DAL/
│   └── Contexto.cs      
├── Models/
│   ├── Productos.cs
│   ├── Entradas.cs
│   └── EntradasDetalle.cs
├── Services/
│   ├── ProductosService.cs
│   └── EntradasService.cs
└── Migrations/           
```

## 6. Seguridad

| Aspecto | Estado | Observaciones |
|---------|--------|---------------|
| Autenticación | ✅ Cumple | ASP.NET Core Identity |
| Protección de rutas | ✅ Cumple | [Authorize] en páginas protegidas |
| Navegación condicional | ✅ Cumple | <AuthorizeView> en NavMenu |

---

## 7. Conclusión

**El proyecto CUMPLE con todos los requerimientos establecidos en la asignación.**

La lógica de negocio crítica para el manejo de inventario está correctamente implementada:
- ✅ Crear entrada → Suma existencias
- ✅ Modificar entrada → Revierte y aplica nuevas cantidades
- ✅ Eliminar entrada → Resta existencias

---

## Firma del Revisor

Confirmo que he revisado el código fuente y probado la aplicación, verificando que la lógica, este funciona correctamente en los tres escenarios requeridos (Crear, Editar, Eliminar).

**Firma:** ROMEL MARTIN ORTEGA PICHARDO

**Fecha:** 16/01/2026
