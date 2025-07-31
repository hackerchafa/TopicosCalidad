# Script para ejecutar todos los casos de prueba y subirlos a Testmo
# Ejecutar este script desde PowerShell en la carpeta TopicosCalidad

Write-Host "=== Ejecutando Casos de Prueba de Plataforma Escolar ===" -ForegroundColor Green

# Configurar token de Testmo
$env:TESTMO_TOKEN="testmo_api_eyJpdiI6Ik1KaitzM3JBakJXVnRWc1Qzc2RFSGc9PSIsInZhbHVlIjoiTzRiZTlock11eUtBajIrdkZSN1QvbFVYbTlaVjZsbldMek5VdnFJZDNoOD0iLCJtYWMiOiIyMTQ4ZjMzYWJhYTFhMGY2N2RlOTI1NDM3NmI1OTljMDQ5ZWNiNTAxZjkwNDhmYWMyNzNmNTgzZGU0ZDg4ZDQwIiwidGFnIjoiIn0="

Write-Host "`n1. Ejecutando Caso de Prueba: Auth-Comentarios..." -ForegroundColor Yellow
dotnet test CasoPrueba-Auth-Comentarios/PlataformaEscolar.TestsClean.csproj --logger:"trx;LogFileName=auth_comentarios.trx"

Write-Host "`n2. Ejecutando Caso de Prueba: GradosGrupos-ClasesHorarios..." -ForegroundColor Yellow
dotnet test CasoPrueba-GradosGrupos-ClasesHorarios/GradosGruposClasesHorariosTests.csproj --logger:"trx;LogFileName=grados_clases.trx"

Write-Host "`n3. Ejecutando Caso de Prueba: Calificaciones-Alumnos..." -ForegroundColor Yellow
dotnet test CasoPrueba-Calificaciones-Alumnos/CalificacionesAlumnosTests.csproj --logger:"trx;LogFileName=calificaciones_alumnos.trx"

Write-Host "`n4. Ejecutando Caso de Prueba: Profesores-Administradores..." -ForegroundColor Yellow
dotnet test CasoPrueba-Profesores-Administradores/ProfesoresAdministradoresTests.csproj --logger:"trx;LogFileName=profesores_admin.trx"

Write-Host "`n=== Subiendo Resultados a Testmo ===" -ForegroundColor Green

Write-Host "`n1. Subiendo Auth-Comentarios..." -ForegroundColor Cyan
testmo automation:run:submit --instance https://hector-hackerchafa.testmo.net --project-id 1 --name "Caso 1: Auth-Comentarios" --source dotnet --results CasoPrueba-Auth-Comentarios/TestResults/auth_comentarios.trx

Write-Host "`n2. Subiendo GradosGrupos-ClasesHorarios..." -ForegroundColor Cyan
testmo automation:run:submit --instance https://hector-hackerchafa.testmo.net --project-id 1 --name "Caso 2: GradosGrupos-ClasesHorarios" --source dotnet --results CasoPrueba-GradosGrupos-ClasesHorarios/TestResults/grados_clases.trx

Write-Host "`n3. Subiendo Calificaciones-Alumnos..." -ForegroundColor Cyan
testmo automation:run:submit --instance https://hector-hackerchafa.testmo.net --project-id 1 --name "Caso 3: Calificaciones-Alumnos" --source dotnet --results CasoPrueba-Calificaciones-Alumnos/TestResults/calificaciones_alumnos.trx

Write-Host "`n4. Subiendo Profesores-Administradores..." -ForegroundColor Cyan
testmo automation:run:submit --instance https://hector-hackerchafa.testmo.net --project-id 1 --name "Caso 4: Profesores-Administradores" --source dotnet --results CasoPrueba-Profesores-Administradores/TestResults/profesores_admin.trx

Write-Host "`n=== ¡Todos los casos de prueba completados! ===" -ForegroundColor Green
Write-Host "Revisa los resultados en: https://hector-hackerchafa.testmo.net/projects/view/1" -ForegroundColor Blue
