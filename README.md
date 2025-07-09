# SumatoVisionCore

**SumatoVisionCore** es una librería en C# orientada al procesamiento de video en tiempo real utilizando OpenCvSharp. Esta librería permite capturar frames desde cámaras, procesarlos con múltiples hilos y realizar operaciones comunes en visión por computadora.

## 🚀 Funcionalidades principales

- Captura de video en tiempo real desde cámara o archivo.
- Procesamiento asíncrono y concurrente de frames.
- Estructura extensible para aplicar lógica personalizada de procesamiento de imágenes.
- Gestión segura de hilos y recursos (`IDisposable`, `BlockingCollection`, etc.).
- Preparado para integrarse fácilmente con interfaces gráficas (WinForms, WPF).

## 🧱 Estructura de clases

- `CameraFrameSource`: captura frames desde la cámara usando `OpenCvSharp.VideoCapture`.
- `FrameReader`: lee frames de un stream de video y los envía a una cola.
- `FrameQueue`: implementa una cola segura para compartir frames entre hilos.
- `FrameProcessor`: procesa frames en un hilo dedicado usando una acción personalizada.
- `MatFrame`: adaptación de `Mat` para implementar `IFrame`.
- `IFrame`: interfaz que permite tratar distintos tipos de frames de forma polimórfica.

## 🖼️ Ejemplo de uso básico

```csharp
var queue = new FrameQueue();
var reader = new FrameReader(queue, "");
var processor = new FrameProcessor(queue, frame =>
{
    // Procesamiento de cada frame
    Console.WriteLine($"Frame recibido con tamaño: {frame.RawMat.Width}x{frame.RawMat.Height}");
});

reader.Start();
processor.Start();
```

## 🧵 Hilos y control de flujo

- `FrameReader` y `FrameProcessor` corren en hilos separados para evitar bloquear el hilo principal.
- Se utiliza `BlockingCollection` para la comunicación entre hilos (productor-consumidor).
- Puedes detener y reanudar el procesamiento agregando métodos `Pause` y `Resume` en `FrameProcessor`.

## 💡 Requisitos

- [.NET 6.0 o superior](https://dotnet.microsoft.com/)
- [OpenCvSharp4](https://www.nuget.org/packages/OpenCvSharp4.Windows/)
- WinForms (si se usa en una app con GUI)

Instalación de paquetes con NuGet:

```bash
dotnet add package OpenCvSharp4.Windows
```

## 🧪 Ejecución

Puedes compilar este proyecto como librería o incluirlo dentro de una aplicación WinForms o consola.

Ejemplo de ejecución en consola (con cámara por defecto):
```bash
dotnet run --project SumatoVisionCore.ConsoleApp
```

## 📦 Integración con UI

- Puede integrarse con un `PictureBox` en WinForms para mostrar los frames en tiempo real.
- Incluye manejos para evitar excepciones al acceder a controles desde hilos no-UI (`Invoke`, `IsHandleCreated`).

## 🔧 Próximas mejoras

- Soporte para múltiples cámaras simultáneamente.
- Detección de reconexión automática si se pierde el feed de la cámara.
- Aplicación de filtros o redes de IA para análisis en tiempo real.
- Agregar logging estructurado.


# SumatoVisionViewer

**SumatoVisionViewer** es una aplicación en C# (WinForms/WPF) diseñada para mostrar en tiempo real los resultados procesados por **SumatoVisionCore**. Permite visualizar video desde cámaras o archivos.

---

## 🎯 Objetivos principales

- Visualización fluida de video en tiempo real.
- Interfaz intuitiva con controles para iniciar, pausar y detener la reproducción.
- Conexión directa con pipelines de procesamiento (SumatoVisionCore).

---

## 🚦 Requisitos

- [.NET 6.0 o superior](https://dotnet.microsoft.com/)
- [OpenCvSharp4](https://www.nuget.org/packages/OpenCvSharp4.Windows/)
- (Opcional) [SumatoVisionCore](../SumatoVisionCore): para integrar la captura y procesamiento de frames

Instalación:

```bash
dotnet add package OpenCvSharp4.Windows
```

