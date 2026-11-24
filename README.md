# La Mesa de Partes de la MPT

Prototipo de consola en C# para registrar y consultar expedientes de la Mesa de Partes de la Municipalidad Provincial de Trujillo (MPT).

- Curso: Fundamentos de Programación (CIIN1205), UPN
- Docente: Ing. Jorge Ricardo Perez Vigil
- Grupo: 11
- Evaluación: Final (EF)

## Integrantes
- Patrick Gustavo Garcia Flores
- Jhonatan Josué Hurtado Cruz
- Luis Gustavo Culquitante Chuquirima
- Pedro Luis Contreras Correa

## Repositorio
https://github.com/PatriConCk2/MesaDePartesMPT

## Qué hace el prototipo
- Registra expedientes con código único (formato `EXP-001`), solicitante, tipo de trámite, asunto y fecha de recepción. El estado inicial es "Recibido".
- Valida los datos: campos obligatorios, espacios sobrantes, formato del código, código no repetido, tipo de trámite (1 a 4), fecha real en formato DD/MM/AAAA y rechazo del carácter `|` en los textos.
- Busca un expediente por su código con una función recursiva.
- Lista todos los expedientes registrados.
- Ordena los expedientes por código o por fecha (método de selección, sin funciones automáticas de ordenamiento).
- Modifica el estado o el asunto de un expediente y elimina un expediente, ambos con confirmación (S/N).
- Muestra un resumen por tipo de trámite y estado con un arreglo bidimensional.
- Guarda los expedientes en el archivo `expedientes.txt` al salir y después de modificar o eliminar, y los carga al iniciar.

## Requisitos
- SDK de .NET instalado (el proyecto usa net10.0) o Visual Studio / VS Code con C#.

## Instrucciones de ejecución
**Opción A: terminal**
1. Abrir una terminal en la carpeta del proyecto.
2. Ejecutar:
```
dotnet run
```
**Opción B: Visual Studio**
1. Abrir el proyecto (`MesaDePartesMPT.csproj`).
2. Presionar `Ctrl + F5` para ejecutar sin depurar.

## Cómo usarlo
Al iniciar aparece el menú:

```
1. Registrar expediente
2. Buscar expediente por codigo
3. Listar expedientes
4. Ordenar expedientes
5. Modificar expediente
6. Eliminar expediente
7. Resumen por tipo y estado
8. Salir
```

- Escribir el número de la opción y presionar Enter.
- Código: `EXP-` seguido de tres dígitos (por ejemplo, `EXP-001`).
- Tipo de trámite: elegir un número del 1 al 4.
- Fecha: formato `DD/MM/AAAA` (por ejemplo, `15/10/2026`).
- Estado: 1 Recibido, 2 En revision, 3 Atendido.
- Para guardar los registros nuevos, salir con la opción 8; si se cierra la ventana a la fuerza, no se guardan.

## Archivo de datos
`expedientes.txt` se crea en la carpeta desde la que se ejecuta el programa. Tiene una línea por expediente con los campos separados por `|`:

```
EXP-001|Carlos Mendoza Ruiz|Licencia de funcionamiento|Licencia para bodega|15/10/2026|Recibido
```

Las líneas con formato inválido se ignoran al cargar. Usar solo datos ficticios.

## Datos de prueba
Tres expedientes de ejemplo (ficticios):

| Código | Solicitante | Tipo | Asunto | Fecha |
|---|---|---|---|---|
| EXP-003 | Rosa Vega Salas | 3 | Recurso de apelacion | 20/09/2026 |
| EXP-001 | Carlos Mendoza Ruiz | 2 | Licencia para bodega | 15/10/2026 |
| EXP-002 | Luis Paredes Diaz | 1 | Solicitud de copia certificada | 03/02/2026 |

Los casos de prueba (CP-01 a CP-21) están en el informe técnico, sección 8.

## Estructura del proyecto
- `Program.cs`: código fuente del prototipo.
- `MesaDePartesMPT.csproj`: configuración del proyecto.
- `MPT_Mesa_de_Partes.psc`: diseño lógico en PSeInt.
- `README.md`: este archivo.

## Etapas de desarrollo (EF)
| Etapa | Módulo | Autor (cuenta de GitHub) |
|---|---|---|
| 2 | Trim en entradas, CAPACIDAD y limpieza | PatriConCk2 |
| 3 | Guardar y cargar expedientes en archivo | Dracoass |
| 4 | Búsqueda recursiva | PatriConCk2 |
| 5 | Modificar y eliminar con confirmación | 1JhonS1 |
| 6 | Resumen por tipo y estado (arreglo bidimensional) | pedroluiscontrerasco-hash |
