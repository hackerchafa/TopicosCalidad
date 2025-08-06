# =========================================
# COMANDOS ACTUALIZADOS - TESTMO
# =========================================

Write-Host "=== CONFIGURACIÓN INICIAL ===" -ForegroundColor Green
Write-Host "Primero, configura el token (ejecuta esto UNA VEZ):" -ForegroundColor Yellow
Write-Host '$env:TESTMO_TOKEN = "testmo_api_eyJpdiI6IjlCVnFXUDFGd0I5OGUrd2d2MzFBNXc9PSIsInZhbHVlIjoiMklpNGlCQTl3U2xSUGMyOEdwNVQ2UnZPUEFzOEtMUjJJQ0F2b241QnRDYz0iLCJtYWMiOiI4MTc0MWU0YzU5YmFhMDM3OTI1MThiYzYwZTg0ZGY0NGI0MGQ3MzlkNzIxMDIwYjlmMWRiMzA4YjdlYjQwMmRjIiwidGFnIjoiIn0="' -ForegroundColor Cyan
Write-Host ""

Write-Host "=== CASOS DE PRUEBA INDIVIDUALES ===" -ForegroundColor Green
Write-Host ""

Write-Host "🔵 CASO 1: Auth y Comentarios" -ForegroundColor Blue
Write-Host "cd `"C:\Users\Lenovo\EscolarWebApi\TopicosCalidad\CasoPrueba-Auth-Comentarios`"" -ForegroundColor White
Write-Host "dotnet test --logger `"trx;LogFileName=auth_comentarios.trx`"" -ForegroundColor White
Write-Host "testmo automation:run:submit --instance `"https://hector-hackerchafa.testmo.net`" --project-id 1 --name `"Auth-Comentarios`" --source `"PlataformaEscolar`" --results `"TestResults\auth_comentarios.trx`"" -ForegroundColor White
Write-Host ""

Write-Host "🟢 CASO 2: Calificaciones y Alumnos" -ForegroundColor Green
Write-Host "cd `"C:\Users\Lenovo\EscolarWebApi\TopicosCalidad\CasoPrueba-Calificaciones-Alumnos`"" -ForegroundColor White
Write-Host "dotnet test --logger `"trx;LogFileName=calificaciones_alumnos.trx`"" -ForegroundColor White
Write-Host "testmo automation:run:submit --instance `"https://hector-hackerchafa.testmo.net`" --project-id 1 --name `"Calificaciones-Alumnos`" --source `"PlataformaEscolar`" --results `"TestResults\calificaciones_alumnos.trx`"" -ForegroundColor White
Write-Host ""

Write-Host "🟡 CASO 3: Grados, Grupos, Clases y Horarios" -ForegroundColor Yellow
Write-Host "cd `"C:\Users\Lenovo\EscolarWebApi\TopicosCalidad\CasoPrueba-GradosGrupos-ClasesHorarios`"" -ForegroundColor White
Write-Host "dotnet test --logger `"trx;LogFileName=grados_clases.trx`"" -ForegroundColor White
Write-Host "testmo automation:run:submit --instance `"https://hector-hackerchafa.testmo.net`" --project-id 1 --name `"Grados-Grupos-Clases-Horarios`" --source `"PlataformaEscolar`" --results `"TestResults\grados_clases.trx`"" -ForegroundColor White
Write-Host ""

Write-Host "🟣 CASO 4: Profesores y Administradores" -ForegroundColor Magenta
Write-Host "cd `"C:\Users\Lenovo\EscolarWebApi\TopicosCalidad\CasoPrueba-Profesores-Administradores`"" -ForegroundColor White
Write-Host "dotnet test --logger `"trx;LogFileName=profesores_admin.trx`"" -ForegroundColor White
Write-Host "testmo automation:run:submit --instance `"https://hector-hackerchafa.testmo.net`" --project-id 1 --name `"Profesores-Administradores`" --source `"PlataformaEscolar`" --results `"TestResults\profesores_admin.trx`"" -ForegroundColor White
Write-Host ""

Write-Host "=== EJECUCIÓN AUTOMÁTICA DE TODOS ===" -ForegroundColor Cyan
Write-Host "Para ejecutar todos los casos automáticamente, usa este script:" -ForegroundColor Yellow
