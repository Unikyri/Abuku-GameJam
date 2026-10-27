# Colaboración

## Equipo

- **Juan:** programación, Git e integración técnica.
- **Adrián:** escena de la biblioteca, objetos y escenario.
- **Laura:** interfaz, audio, efectos y transiciones.
- **Eli:** arte 2D, sprites y animación.

El diseño, las pruebas y la entrega son responsabilidad de todos.

## Cómo trabajamos

1. Actualiza `main` y crea una rama corta para tu tarea, por ejemplo `feature/ui-selection`.
2. Haz cambios pequeños y pruébalos en Unity. Guarda cada recurso junto con su archivo `.meta`.
3. Haz un commit claro, sube la rama y abre un pull request a `main` para que otra persona lo revise. Después de integrarlo, actualiza tu copia.

Adrián es responsable de `Library`. Avisemos antes de editarla y evitemos trabajar en la escena al mismo tiempo. Laura puede preparar la interfaz en prefabs; Eli, el arte y las animaciones; y Juan, los scripts. Adrián integra los cambios en la escena.

## Primer commit y prueba

Revisa los cambios preparados y pendientes antes del primer commit. Incluye `Assets` con sus `.meta` y `.gitkeep`, `Packages`, `ProjectSettings`, `.gitignore` y la documentación. Revisa si los archivos de `.vscode` le sirven al equipo. No incluyas `/Library`, `/Temp`, `/Logs`, `/UserSettings` ni builds generadas.

Después del primer push, otra persona debe clonar el repositorio en una carpeta nueva, abrirlo con Unity 6000.3.27f1, esperar la importación y probar `Library` en Play sin errores en la consola. Los cuatro deben comprobar que pueden abrirlo antes de empezar.

Por ahora no usamos Git LFS. Si aparecen archivos grandes de arte o audio, acordaremos qué tipos guardar con LFS y lo instalaremos todos antes de agregarlos.
