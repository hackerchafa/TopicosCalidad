# =========================================
# SCRIPT AUTOMÁTICO - EJECUTAR TODOS LOS CASOS
# =========================================

# Configurar token (ejecutar una vez por sesión)
$env:TESTMO_TOKEN = "testmo_api_eyJpdiI6IjlCVnFXUDFGd0I5OGUrd2d2MzFBNXc9PSIsInZhbHVlIjoiMklpNGlCQTl3U2xSUGMyOEdwNVQ2UnZPUEFzOEtMUjJJQ0F2b241QnRDYz0iLCJtYWMiOiI4MTc0MWU0YzU5YmFhMDM3OTI1MThiYzYwZTg0ZGY0NGI0MGQ3MzlkNzIxMDIwYjlmMWRiMzA4YjdlYjQwMmRjIiwidGFnIjoiIn0="

$BASE_PATH = "C:\Users\Lenovo\EscolarWebApi\TopicosCalidad"

Write-Host "🚀 Ejecutando todos los casos de prueba..." -ForegroundColor Green
Write-Host ""

# Función para ejecutar un caso de prueba
function Ejecutar-Caso {
    param(
        [string]$Nombre,
        [string]$Directorio,
        [string]$ArchivoResultado,
        [string]$NombreTestmo
    )
    
    Write-Host "📋 Ejecutando: $Nombre" -ForegroundColor Yellow
    
    # Cambiar directorio
    Set-Location "$BASE_PATH\$Directorio"
    
    # Ejecutar pruebas
    Write-Host "   🔄 Ejecutando pruebas..." -ForegroundColor Cyan
    dotnet test --logger "trx;LogFileName=$ArchivoResultado"
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "   ✅ Pruebas completadas" -ForegroundColor Green
        
        # Subir a Testmo
        Write-Host "   ⬆️ Subiendo a Testmo..." -ForegroundColor Cyan
        testmo automation:run:submit --instance "https://hector-hackerchafa.testmo.net" --project-id 1 --name $NombreTestmo --source "PlataformaEscolar" --results "TestResults\$ArchivoResultado"
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host "   ✅ Subido a Testmo correctamente" -ForegroundColor Green
        } else {
            Write-Host "   ❌ Error subiendo a Testmo" -ForegroundColor Red
        }
    } else {
        Write-Host "   ❌ Error en las pruebas" -ForegroundColor Red
    }
    
    Write-Host ""
}

# Ejecutar todos los casos
Ejecutar-Caso "Auth y Comentarios" "CasoPrueba-Auth-Comentarios" "auth_comentarios.trx" "Auth-Comentarios"

Ejecutar-Caso "Calificaciones y Alumnos" "CasoPrueba-Calificaciones-Alumnos" "calificaciones_alumnos.trx" "Calificaciones-Alumnos"

Ejecutar-Caso "Grados, Grupos, Clases y Horarios" "CasoPrueba-GradosGrupos-ClasesHorarios" "grados_clases.trx" "Grados-Grupos-Clases-Horarios"

Ejecutar-Caso "Profesores y Administradores" "CasoPrueba-Profesores-Administradores" "profesores_admin.trx" "Profesores-Administradores"

# Volver al directorio base
Set-Location $BASE_PATH

Write-Host "🎉 ¡Todos los casos completados!" -ForegroundColor Green
Write-Host "🔍 Revisa los resultados en: https://hector-hackerchafa.testmo.net" -ForegroundColor Cyan
