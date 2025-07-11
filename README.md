# Sumato-id - Evaluación Técnica Backend (Dificultad Media)

Este proyecto implementa los 3 desafíos propuestos en la evaluación técnica para Sumato-Id, utilizando .NET 8, OpenCVSharp, multithreading y arquitectura de servicios. Está dividido en módulos reutilizables que permiten trabajar con diferentes fuentes de video y realizar procesamiento distribuido de frames.

---

## 🧩 Tecnologías utilizadas

- .NET 8
- C#
- OpenCvSharp
- WinForms
- Threads y ConcurrentQueue
- WebSockets
- RabbitMQ *(propuesto, no implementado)*
- Design Patterns: Factory, Strategy, Command, Delegate

---

## 🚀 Desafío 1 - App Console

### ✅ Objetivo

Conectarse a una webcam o video local desde una aplicación de consola y procesar los frames utilizando múltiples hilos.

### 🧪 Funcionalidades implementadas

- Conexión a webcam o archivo de video (usando `VideoCapture` de OpenCVSharp).
- Redimensionamiento de cada frame a 640x480.
- Almacenamiento de los frames en una `BlockingCollection<IFrame>` (cola thread-safe).
- Procesamiento desacoplado usando interfaces y patrones de diseño.

### 🧵 Hilos utilizados

- `Main`: inicializa y gestiona el ciclo de vida.
- `ReaderThread`: lee frames (`PushQueue`).
- `ProcessorThread`: consume y procesa (`PullQueue`).

### 📁 Clases clave

- `IFrame`: interfaz base para `MatFrame`, `BitmapFrame`, etc.
- `FrameQueue`: implementación thread-safe de la cola.
- `FrameReader`: captura desde cámara o video.
- `FrameProcessor`: procesa los frames (resize, etc).

---

## 🖼️ Desafío 2 - Windows Forms

### ✅ Objetivo

Visualizar los frames procesados en una interfaz WinForms utilizando el proyecto del Desafío 1 como base.

### 🔧 Funcionalidades

- Selección entre cámara o archivo.
- Visualización en `PictureBox` con redimensionamiento.
- Uso del core como DLL reutilizable (`SumatoVisionCore`).
- Actualización de UI segura con `Invoke`.

---

## 🧱 Desafío 3 - Arquitectura distribuida

### ✅ Objetivo

Separar la lógica en 3 servicios: `Capture`, `Queue`, `Processing`, permitiendo escalabilidad horizontal.

```
[Service Capture] ---> [Service Queue] ---> [Service Processing]
       (WS)                  (Queue)                 (WS)
```

- **Capture**: captura y envía frames por WebSocket.
- **Queue**: recibe y redistribuye a los clientes `Processing`.
- **Processing**: recibe, convierte y procesa los frames.

> Implementación actual con WebSockets. Se propone RabbitMQ para ambientes reales distribuidos. También se podría usar WebSocket bidireccional para mensajes de retorno (`Processing → Capture`).

---

## 💡 Recomendación Técnica

Usar RabbitMQ en producción:

- Comunicación desacoplada y asíncrona.
- Escalabilidad y tolerancia a fallos.
- Integración sencilla con .NET.

---

## 🔨 Instrucciones de ejecución

### ✅ Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- OpenCvSharp4.Windows
- RabbitMQ *(opcional)*

### ▶️ Ejecución local

1. Clonar el repositorio:
   ```bash
   git clone https://github.com/tonyforns/Sumato-id.git
   cd Sumato-id
   ```

2. Ejecutar consola (modo cámara):
   ```bash
   cd Challange1
   dotnet run
   ```

3. Ejecutar consola (modo archivo de video):
   ```bash
   dotnet run {ruta_al_video}
   ```

4. Ejecutar WinForms:
   ```bash
   cd SumatoVisionViewer
   dotnet run
   ```

5. Ejecutar servicios distribuidos:
   Ejecutar `CaptureService`, `QueueService` y `ProcessingService` en consolas separadas, ejecutando primero `QueueService` para evitar errores.

---

## 🧠 Diseño extensible

- `IFrame` para nuevos formatos de imagen.
- `IFrameSource` para nuevas fuentes de entrada.
- Componentes desacoplados y testeables.
- Preparado para análisis de video futuro.

---

## ⚠️ Mejoras posibles

- Manejo de reconexión automática de WebSockets ante caídas de red.
- Agregado de logs estructurados con Serilog o similar.
- Reintentos automáticos de envío ante fallos transitorios.
- Persistencia temporal de frames en caso de desconexión.
- Implementar comunicación bidireccional real en los servicios.

---

## 📫 Contacto

Desarrollado por [Antonio Forns](https://github.com/tonyforns)