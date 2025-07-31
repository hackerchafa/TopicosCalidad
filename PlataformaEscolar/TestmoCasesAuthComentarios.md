# Casos de prueba para Testmo

## Auth

### Registro de usuario exitoso
- Endpoint: POST /api/Auth/register
- Precondición: Usuario no existe
- Body:
```json
{
  "UserName": "JuanAlumno",
  "Email": "juan@correo.com",
  "Password": "Abc123!",
  "Rol": "Alumno"
}
```
- Resultado esperado: 200 OK, "Usuario registrado correctamente"

### Login exitoso
- Endpoint: POST /api/Auth/login
- Precondición: Usuario existe
- Body:
```json
{
  "UserName": "JuanAlumno",
  "Password": "Abc123!"
}
```
- Resultado esperado: 200 OK, token JWT válido

## Comentarios

### Comentar a profesor
- Endpoint: POST /api/Comentario/a-profesor
- Precondición: Usuario autenticado como Alumno
- Body:
```json
{
  "Texto": "Buen profesor",
  "ProfesorId": 1,
  "AlumnoId": 2
}
```
- Resultado esperado: 200 OK, confirmación del comentario

### Comentar a alumno
- Endpoint: POST /api/Comentario/a-alumno
- Precondición: Usuario autenticado como Profesor
- Body:
```json
{
  "Texto": "Buen trabajo",
  "ProfesorId": 1,
  "AlumnoId": 2
}
```
- Resultado esperado: 200 OK, confirmación del comentario
