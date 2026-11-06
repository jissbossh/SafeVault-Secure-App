# SafeVault - Implementación Avanzada de Seguridad con Minimal APIs

Este repositorio contiene la segunda iteración del proyecto **SafeVault**, una API RESTful desarrollada en C# que implementa arquitecturas modernas de seguridad para mitigar vulnerabilidades críticas. En esta versión, se ha optado por un enfoque de **Minimal APIs** y el uso de **consultas ADO.NET estrictamente parametrizadas** para el acceso a datos.

## Resumen de Vulnerabilidades Mitigadas y Uso de Microsoft Copilot

### 1. Inyección SQL (SQLi)
**Vulnerabilidad:** Las consultas dinámicas basadas en concatenación permitían la inyección de comandos destructivos.
**Corrección:** En lugar de depender de un ORM, se implementó el uso de `SqlCommand` con parámetros explícitos (`@Username`). Copilot sugirió la sintaxis exacta para la clase `SqlParameter`, garantizando que las entradas del usuario se traten como datos literales y no como código ejecutable.

### 2. Cross-Site Scripting (XSS)
**Vulnerabilidad:** Riesgo de que se reflejaran scripts maliciosos en las respuestas del servidor.
**Corrección:** Copilot ayudó a generar un método de saneamiento en `SecurityService.cs` utilizando expresiones regulares (`Regex.Replace`) para eliminar etiquetas HTML (`<.*?>`) de las entradas y salidas. Esto asegura que cualquier intento de inyectar scripts sea neutralizado antes de procesar la solicitud.

### 3. Control de Acceso y Validación
**Corrección:** Se utilizaron Data Annotations en el modelo `User` (como `[RegularExpression]`) para validar entradas a nivel de modelo. Además, se implementó un mecanismo de Control de Acceso Basado en Roles (RBAC) validando las cabeceras de la petición para asegurar que solo usuarios con el rol adecuado accedan a los recursos protegidos.

## Pruebas
El proyecto incluye pruebas unitarias que simulan inyecciones de código malicioso (XSS) para validar la efectividad del servicio de saneamiento. Para ejecutar las pruebas:
`dotnet test`