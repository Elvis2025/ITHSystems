# 🧩 ITHSystems

**ITHSystems** es una aplicación desarrollada en **.NET MAUI** que implementa el patrón **MVVM (Model-View-ViewModel)** y una arquitectura modular orientada a la escalabilidad y reutilización del código.  
El proyecto integra manejo de vistas, lógica de negocio, servicios, repositorios y recursos compartidos para gestionar operaciones logísticas, como entregas, beneficiarios y sincronización de envíos.

---

## 📁 Estructura del Proyecto

### 🔹 Core del Sistema
| Carpeta | Descripción |
|----------|--------------|
| **Attributes/** | Contiene atributos personalizados que extienden el comportamiento de clases o propiedades. |
| **AutoMapper/** | Define los perfiles de mapeo entre entidades, DTOs y ViewModels. Facilita la conversión de datos entre capas. |
| **Behaviors/** | Incluye comportamientos reutilizables para controles de UI (validaciones, eventos, etc.). |
| **Constants/** | Define constantes globales del sistema como claves, mensajes o configuraciones estáticas. |
| **Controls/** | Componentes personalizados de interfaz de usuario reutilizables en distintas páginas. |
| **DTOs/** | Objetos de transferencia de datos utilizados entre las capas de aplicación y presentación. |
| **Enums/** | Enumeraciones que describen estados, tipos o valores definidos usados en la lógica de negocio. |
| **Extensions/** | Métodos de extensión para simplificar operaciones comunes en tipos de datos o servicios. |
| **Model/** | Contiene las entidades de dominio y modelos de negocio principales. |
| **Repositories/** | Implementa el acceso a datos (principalmente SQLite) mediante clases de persistencia y repositorios genéricos. |
| **Resources/** | Recursos visuales y de configuración (colores, estilos, íconos, etc.). |
| **Resx/** | Archivos de recursos para internacionalización y soporte multilenguaje. |
| **Services/** | Servicios que encapsulan la lógica de negocio o comunicación con APIs, bases de datos y almacenamiento local. |
| **UsesCases/** | Casos de uso que definen flujos de negocio específicos y operaciones reutilizables. |

---

## 🖼️ Estructura de Vistas (Views)

### **Deliveries/**
Módulo encargado de la gestión de entregas, sincronización y seguimiento:
- **DeliveredShipmentsNotSynchronized** → Muestra los envíos no sincronizados.  
- **DeliveriesPostponed** → Registra y visualiza entregas aplazadas.  
- **PendingDeliveries** → Lista las entregas pendientes, subdivididas por beneficiarios.  
- **Beneficiary** → Gestiona causas de no entrega y beneficiarios asociados.  
- **DeliverBeneficiary** → Permite realizar entregas a beneficiarios (primera y segunda persona).  
- **DeliveriesPage.xaml / DeliveriesViewModel.cs** → Página principal del módulo de entregas.

### **Home/**
Página principal tras el inicio de sesión.  
- `HomePage.xaml` y `HomePageViewModel.cs` definen el punto de entrada del usuario.  
- `BuildHomeModules.cs` gestiona la carga de módulos dinámicamente.

### **Login/**
Manejo de autenticación y acceso al sistema:
- `LoginPage.xaml` y `LoginPageViewModel.cs` implementan la vista y lógica de autenticación.

### **ModalsMessage/**
Contiene componentes modales reutilizables:
- `AlertMessageModal`, `ErrorMessagePopup`, `SuccessMessagePopup`, `WarningMessagePopup`.

### **PickupService/**
Módulo que administra servicios de recogida de paquetes.
- `PickupServicePage.xaml` y `PickupServiceViewModel.cs`.

---

## ⚙️ Configuración y Entrada

- **App.xaml** → Define estilos globales y configuración base de la aplicación.
- **AppShell.xaml** → Estructura de navegación mediante **.NET MAUI Shell**.
- **MauiProgram.cs** → Punto de inicio del proyecto, registra servicios e inyección de dependencias.
- **ConfigurationExtensions.cs / ServicesExtensions.cs** → Métodos auxiliares para inicializar configuraciones y servicios del contenedor de dependencias.
- **SQLiteConfiguration.cs** → Configura la base de datos local SQLite.

---

## 🧠 Patrón de Arquitectura

El proyecto sigue el patrón **MVVM (Model-View-ViewModel)** junto con principios de **Clean Architecture**, organizando el código en capas:
- **View** → Interfaz gráfica y bindings.
- **ViewModel** → Lógica de presentación y comandos.
- **Model / Domain** → Entidades de negocio.
- **Services / Repositories** → Acceso a datos y lógica de aplicación.

---

## 🧾 Tecnologías Utilizadas

- **.NET MAUI (.NET 9)**  
- **C# 12**  
- **SQLite local database**  
- **MVVM Community Toolkit**  
- **AutoMapper**  
- **Dependency Injection (IServiceCollection)**  
- **Resx localization**

---

## 🚀 Próximos pasos

- Implementar capa de sincronización en segundo plano.  
- Integrar autenticación persistente con almacenamiento seguro.  
- Extender la internacionalización (multi-idioma).  
- Crear documentación XML de servicios y ViewModels.

---

## 👤 Autor

**ITH Systems Development Team**  
Desarrollado por *Elvis Hernández*  
📧 Contacto: *(Telefono: 849-869-8664 Email: inelvis16031124@gmail.com)*  


