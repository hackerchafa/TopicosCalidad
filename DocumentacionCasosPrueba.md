# Documentación de Casos de Prueba - Plataforma Escolar WebAPI

## Resumen General
Esta documentación describe los 4 casos de prueba automatizados para validar la funcionalidad completa de la WebAPI de la Plataforma Escolar.

## Casos de Prueba Implementados

### 1. Caso de Prueba: Auth-Comentarios
**Objetivo:** Validar la autenticación de usuarios y el sistema de comentarios entre alumnos y profesores.

**Funcionalidades Probadas:**
- Registro de usuarios (Alumno y Profesor)
- Login de usuarios
- Creación de comentarios de alumno a profesor
- Creación de comentarios de profesor a alumno

**Tests Incluidos:**
- `Register_Alumno_ReturnsOk()`
- `Login_Alumno_ReturnsToken()`
- `ComentarAProfesor_ReturnsOk()`
- `ComentarAAlumno_ReturnsOk()`

### 2. Caso de Prueba: GradosGrupos-ClasesHorarios
**Objetivo:** Validar la gestión de grados, grupos y horarios de clases.

**Funcionalidades Probadas:**
- Creación de grados y grupos
- Consulta de grados y grupos existentes
- Creación de horarios de clases
- Consulta de horarios de clases

**Tests Incluidos:**
- `CrearGradoGrupo_ReturnsOk()`
- `ObtenerGradosGrupos_ReturnsOk()`
- `CrearClaseHorario_ReturnsOk()`
- `ObtenerClasesHorarios_ReturnsOk()`

### 3. Caso de Prueba: Calificaciones-Alumnos
**Objetivo:** Validar el sistema de calificaciones y gestión de información de alumnos.

**Funcionalidades Probadas:**
- Asignación de calificaciones por profesores
- Consulta de calificaciones por alumno
- Obtención de lista de alumnos
- Consulta de perfil de alumno

**Tests Incluidos:**
- `AsignarCalificacion_ReturnsOk()`
- `ObtenerCalificacionesAlumno_ReturnsOk()`
- `ObtenerListaAlumnos_ReturnsOk()`
- `ObtenerPerfilAlumno_ReturnsOk()`

### 4. Caso de Prueba: Profesores-Administradores
**Objetivo:** Validar la gestión de profesores y administradores del sistema.

**Funcionalidades Probadas:**
- Consulta de lista de profesores
- Consulta de perfil de profesor
- Consulta de lista de administradores
- Consulta de perfil de administrador

**Tests Incluidos:**
- `ObtenerListaProfesores_ReturnsOk()`
- `ObtenerPerfilProfesor_ReturnsOk()`
- `ObtenerListaAdministradores_ReturnsOk()`
- `ObtenerPerfilAdministrador_ReturnsOk()`

## Estructura de Archivos

```
TopicosCalidad/
├── CasoPrueba-Auth-Comentarios/
│   ├── AuthComentariosTests.csproj
│   ├── AuthControllerTests.cs
│   └── ComentarioControllerTests.cs
├── CasoPrueba-GradosGrupos-ClasesHorarios/
│   ├── GradosGruposClasesHorariosTests.csproj
│   └── GradosGruposClasesHorariosTests.cs
├── CasoPrueba-Calificaciones-Alumnos/
│   ├── CalificacionesAlumnosTests.csproj
│   └── CalificacionesAlumnosTests.cs
└── CasoPrueba-Profesores-Administradores/
    ├── ProfesoresAdministradoresTests.csproj
    └── ProfesoresAdministradoresTests.cs
```

## Comandos para Ejecutar y Subir a Testmo

### 1. Ejecutar todos los casos de prueba:
```bash
# Caso 1: Auth-Comentarios
dotnet test CasoPrueba-Auth-Comentarios/AuthComentariosTests.csproj --logger:"trx;LogFileName=auth_comentarios.trx"

# Caso 2: GradosGrupos-ClasesHorarios
dotnet test CasoPrueba-GradosGrupos-ClasesHorarios/GradosGruposClasesHorariosTests.csproj --logger:"trx;LogFileName=grados_clases.trx"

# Caso 3: Calificaciones-Alumnos
dotnet test CasoPrueba-Calificaciones-Alumnos/CalificacionesAlumnosTests.csproj --logger:"trx;LogFileName=calificaciones_alumnos.trx"

# Caso 4: Profesores-Administradores
dotnet test CasoPrueba-Profesores-Administradores/ProfesoresAdministradoresTests.csproj --logger:"trx;LogFileName=profesores_admin.trx"
```

### 2. Subir resultados a Testmo:
```bash
# Configurar token (ejecutar una vez)
$env:TESTMO_TOKEN="testmo_api_eyJpdiI6Ik1KaitzM3JBakJXVnRWc1Qzc2RFSGc9PSIsInZhbHVlIjoiTzRiZTlock11eUtBajIrdkZSN1QvbFVYbTlaVjZsbldMek5VdnFJZDNoOD0iLCJtYWMiOiIyMTQ4ZjMzYWJhYTFhMGY2N2RlOTI1NDM3NmI1OTljMDQ5ZWNiNTAxZjkwNDhmYWMyNzNmNTgzZGU0ZDg4ZDQwIiwidGFnIjoiIn0="

# Subir cada caso de prueba
testmo automation:run:submit --instance https://hector-hackerchafa.testmo.net --project-id 1 --name "Caso 1: Auth-Comentarios" --source dotnet --results CasoPrueba-Auth-Comentarios/TestResults/auth_comentarios.trx

testmo automation:run:submit --instance https://hector-hackerchafa.testmo.net --project-id 1 --name "Caso 2: GradosGrupos-ClasesHorarios" --source dotnet --results CasoPrueba-GradosGrupos-ClasesHorarios/TestResults/grados_clases.trx

testmo automation:run:submit --instance https://hector-hackerchafa.testmo.net --project-id 1 --name "Caso 3: Calificaciones-Alumnos" --source dotnet --results CasoPrueba-Calificaciones-Alumnos/TestResults/calificaciones_alumnos.trx

testmo automation:run:submit --instance https://hector-hackerchafa.testmo.net --project-id 1 --name "Caso 4: Profesores-Administradores" --source dotnet --results CasoPrueba-Profesores-Administradores/TestResults/profesores_admin.trx
```

## Tecnologías Utilizadas
- **.NET 8.0** - Framework de desarrollo
- **xUnit** - Framework de pruebas unitarias
- **RestSharp** - Cliente HTTP para pruebas de API
- **Testmo** - Plataforma de gestión de pruebas
- **System.Text.Json** - Deserialización de respuestas JSON

## Notas para la Exposición
1. Cada caso de prueba valida una funcionalidad específica de la WebAPI
2. Los tests incluyen autenticación JWT cuando es requerida
3. Se crean usuarios únicos para evitar conflictos entre pruebas
4. Los resultados se exportan en formato TRX compatible con Testmo
5. La documentación facilita la comprensión y mantenimiento de las pruebas
