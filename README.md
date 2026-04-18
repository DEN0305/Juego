# Juego de Plataformas

Este proyecto es un juego de plataformas desarrollado en Unity como parte de la materia "Desarrollo de Videojuegos y Multimedia".

## Descripción General
En este juego, el jugador controla un personaje que debe saltar entre plataformas para alcanzar una campana ubicada en la parte superior del nivel. El escenario y las plataformas se generan de manera programática, lo que permite una experiencia dinámica y desafiante en cada partida. El objetivo principal es llegar lo más alto posible sin caer, poniendo a prueba la habilidad y precisión del jugador.

## Mecánicas del Juego
- **Movimiento:** El personaje puede moverse lateralmente y saltar para desplazarse entre plataformas.
- **Plataformas:** Las plataformas se alternan entre posiciones y alturas, aumentando la dificultad progresivamente.
- **Campana:** Al llegar a la campana, el jugador completa el nivel y se muestra un mensaje de victoria.
- **Fondo Dinámico:** El fondo se genera automáticamente para ambientar el escenario.
- **Colisiones:** El personaje no puede atravesar las plataformas desde abajo, asegurando un reto justo.

## Características Técnicas
- Generación automática de plataformas y fondo.
- Control de personaje con físicas realistas usando Rigidbody2D.
- Detección de victoria y reinicio del nivel.
- Visuales personalizables mediante sprites.
- Código organizado en scripts independientes para fácil mantenimiento.

## Instalación y Ejecución
1. Clona o descarga este repositorio.
2. Abre la carpeta del proyecto en Unity (versión recomendada 2021 o superior).
3. Abre la escena principal desde la carpeta `Assets/Scenes`.
4. Ejecuta el juego desde el editor de Unity.

## Organización del Proyecto
- `Assets/Scripts`: Contiene todos los scripts C# del juego.
- `Assets/Prefabs`: Prefabricados reutilizables para plataformas y otros objetos.
- `Assets/Sprites`: Imágenes y sprites utilizados en el juego.
- `Assets/Scenes`: Escenas del juego.
- `ProjectSettings`: Configuración del proyecto Unity.

## Créditos y Autoría
## Capturas de Pantalla

A continuación se muestran algunas capturas del juego:

![Captura 1](capturas/Captura%20de%20pantalla%202026-04-17%20191657.png)
![Captura 2](capturas/Captura%20de%20pantalla%202026-04-17%20191733.png)
![Captura 3](capturas/Captura%20de%20pantalla%202026-04-17%20191756.png)

**Autor:** Diego Enrique Niño Rodriguez
**Materia:** Desarrollo de Videojuegos y Multimedia

Este proyecto fue realizado como parte de la formación en desarrollo de videojuegos, aplicando conceptos de programación, diseño y multimedia.

## Licencia
Este proyecto es de uso académico y puede ser modificado para fines educativos.

---

