# Script para ejecutar casos de prueba individuales y subirlos a Testmo
# Asegurate de tener tu API token configurado: $env:TESTMO_TOKEN = "tu_token_aqui"

Write-Host "=== Ejecutando Casos de Prueba Individuales ===" -ForegroundColor Green

# Variables de configuración
$TESTMO_INSTANCE = "https://hector-hackerchafa.testmo.net"
$PROJECT_ID = "1"
$BASE_DIR = "C:\Users\Lenovo\EscolarWebApi\TopicosCalidad"

# Función para ejecutar un caso de prueba específico
function Ejecutar-CasoPrueba {
    param(
        [string]$CarpetaCaso,
        [string]$NombreCaso,
        [string]$ArchivoResultados
    )
    
    Write-Host "`n--- Ejecutando: $NombreCaso ---" -ForegroundColor Yellow
    
    # Cambiar al directorio del caso de prueba
    Set-Location "$BASE_DIR\$CarpetaCaso"
    
    # Restaurar paquetes si es necesario
    Write-Host "Restaurando paquetes..." -ForegroundColor Cyan
    dotnet restore
    
    # Ejecutar las pruebas
    Write-Host "Ejecutando pruebas..." -ForegroundColor Cyan
    dotnet test --logger "trx;LogFileName=$ArchivoResultados"
    
    # Verificar si el archivo de resultados existe
    $rutaResultados = "TestResults\$ArchivoResultados"
    if (Test-Path $rutaResultados) {
        Write-Host "Archivo de resultados encontrado: $rutaResultados" -ForegroundColor Green
        
        # Subir resultados a Testmo
        Write-Host "Subiendo resultados a Testmo..." -ForegroundColor Cyan
        testmo automation:run:submit `
            --instance $TESTMO_INSTANCE `
            --project-id $PROJECT_ID `
            --name $NombreCaso `
            --source "PlataformaEscolar" `
            --results $rutaResultados
    } else {
        Write-Host "ERROR: No se encontró el archivo de resultados en $rutaResultados" -ForegroundColor Red
    }
}

# ====== EJECUCIÓN DE CASOS DE PRUEBA ======

# Caso 1: Auth y Comentarios
Ejecutar-CasoPrueba -CarpetaCaso "CasoPrueba-Auth-Comentarios" -NombreCaso "Auth-Comentarios" -ArchivoResultados "auth_comentarios.trx"

# Caso 2: Calificaciones y Alumnos
Ejecutar-CasoPrueba -CarpetaCaso "CasoPrueba-Calificaciones-Alumnos" -NombreCaso "Calificaciones-Alumnos" -ArchivoResultados "calificaciones_alumnos.trx"

# Caso 3: Grados, Grupos, Clases y Horarios
Ejecutar-CasoPrueba -CarpetaCaso "CasoPrueba-GradosGrupos-ClasesHorarios" -NombreCaso "Grados-Grupos-Clases-Horarios" -ArchivoResultados "grados_clases.trx"

# Caso 4: Profesores y Administradores
Ejecutar-CasoPrueba -CarpetaCaso "CasoPrueba-Profesores-Administradores" -NombreCaso "Profesores-Administradores" -ArchivoResultados "profesores_admin.trx"

# Volver al directorio base
Set-Location $BASE_DIR

Write-Host "`n=== Ejecución Completada ===" -ForegroundColor Green
