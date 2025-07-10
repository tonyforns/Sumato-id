# Sumato-id - Evaluación Técnica Backend (Dificultad Media)

Este proyecto implementa los 3 desafíos propuestos en la evaluación técnica para Sumato-Id, utilizando .NET 8, OpenCVSharp, multithreading y arquitectura de servicios. Está dividido en módulos reutilizables que permiten trabajar con diferentes fuentes de video y realizar procesamiento distribuido de frames.

## 🧩 Tecnologías utilizadas

- .NET 8
- C#
- OpenCvSharp
- WinForms
- Threads y ConcurrentQueue
- WebSockets
- RabbitMQ (para la comunicación entre servicios - No implementado)
- Design Patterns (Factory, Strategy, Command, Delegate)

---

## Desafío 1 - App Console

### ✅ Objetivo
Conectarse a una webcam o video local desde una aplicación de consola y procesar los frames utilizando múltiples hilos.

### 🧪 Funcionalidades implementadas

- Conexión a webcam o archivo de video (usando `VideoCapture` de OpenCVSharp).
- Redimensionamiento de cada frame a 640x480.
- Almacenamiento de los frames en una `BlockingCollection<IFrame>` (Queue thread-safe).
- Procesamiento de frames con diseño desacoplado usando interfaces.
- Separación de lógica: cada componente implementado con responsabilidades claras.

### 🧵 Threads

- `Main`: Inicializa y gestiona el ciclo de vida de los componentes.
- `ReaderThread`: Extrae frames y los introduce en la cola (`PushQueue`).
- `ProcessorThread`: Consume los frames desde la cola y aplica el procesamiento (`PullQueue`).

### 📁 Clases Clave

- `IFrame`: Interfaz base para distintos tipos de frame (por ejemplo, `MatFrame`, `BitmapFrame`).
- `FrameQueue`: Implementación de la cola thread-safe.
- `FrameReader`: Lee desde cámara o archivo y hace push de frames.
- `FrameProcessor`: Consume frames de la cola y aplica redimensionamiento.

---

## Desafío 2 - Windows Forms

### ✅ Objetivo
Visualizar los frames procesados en una interfaz WinForms utilizando el proyecto del Desafío 1 como base.

### 🔧 Funcionalidades

- Selección entre cámara o archivo de video.
- Visualización de video redimensionado.
- Interfaz WinForms que consume la DLL compartida de `SumatoVisionCore`.
- Actualización segura del `PictureBox` mediante `Invoke`.

---

## Desafío 3 - Arquitectura distribuida

### ✅ Objetivo
Separar la lógica en 3 servicios: `Capture`, `Queue`, `Processing`, permitiendo escalabilidad horizontal.

### 🧱 Arquitectura implementada 

```
[Service Capture] ---> [Service Queue] ---> [Service Processing]
       (WS)                  (Queue)                 (WS)
```

- **Service Capture**: Lee los frames desde webcam/video y los envía vía WebSocket.
- **Service Queue**: Recibe frames desde `Capture` y los redistribuye a `Processing`.
- **Service Processing**: Consume los frames y aplica redimensionamiento.

> Comunicación entre servicios implementada usando **WebSockets** (para simplicidad local) y **RabbitMQ** como propuesta para ambientes distribuidos.
> Tambien se puede implementar para que Service Processing pueda enviarle mensajes a Service Capture gracias a la flexibilidad de WS

### 💡 Recomendación Técnica

Para ambientes distribuidos, se recomienda usar **RabbitMQ** por las siguientes razones:

- Permite comunicación asíncrona y desacoplada.
- Tolerancia a fallos y balanceo de carga.
- Fácil de escalar y monitorizar.
- Amplio soporte en .NET y otras plataformas.

---

## 🔨 Instrucciones de ejecución

### Requisitos

- .NET 8 SDK
- OpenCvSharp4.Windows
- RabbitMQ (opcional para pruebas distribuidas)

### Ejecución (Modo Local)

1. Clonar el repositorio:
   ```bash
   git clone https://github.com/tonyforns/Sumato-id.git
   cd Sumato-id
   ```

2.1 Ejecutar consola camera:
   ```bash
   cd Challange 1
   dotnet run
   ```

2.2 Ejecutar consola video file:
   ```bash
   cd Challange 1
   dotnet run {VideoPath}
   ```

3. Ejecutar WinForms:
   ```bash
   cd SumatoVisionViewer
   dotnet run
   ```

4. Ejecutar servicios distribuidos (opcional):
   Ejecutar los proyectos `QueueService`, `CaptureService`,  y `ProcessingService` en ventanas separadas .

---

## 🧠 Diseño Extensible

- Uso de interfaces (`IFrame`) para soportar nuevos tipos de frame.
- Uso de interfaces(`IFrameSource`) para soportar nuevos tipos frame producer.
- Diseño desacoplado y basado en componentes.
- Preparado para integración con herramientas de análisis de video.

---

## 📫 Contacto

Desarrollado por [Antonio Forns] (https://github.com/tonyforns)  
