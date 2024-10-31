## Proyecto de Aplicación de Chat

Integrantes: Juan Bianchini - Franco Moreno

### 1. Introducción y Objetivos
Este proyecto tiene como objetivo desarrollar una **aplicación de chat** en C# usando Visual Studio como parte de un proyecto universitario. La aplicación permite la comunicación entre usuarios a través de protocolos TCP y UDP.Para mejorar la funcionalidad y la experiencia del usuario, se ha desarrollado una interfaz gráfica atractiva y un sistema de historial de chats que se almacena en una base de datos MongoDB.

Los objetivos principales son:

. Implementar una comunicación confiable (TCP) y una comunicación rápida (UDP) entre cliente y servidor.

. Crear una interfaz gráfica intuitiva y atractiva para el usuario.

. Mantener un historial de chat almacenado en MongoDB, asegurando la persistencia de los mensajes.

. [futura implementación] Desarrollar un sistema de registro y login de usuarios, permitiendo acceso seguro y controlado.

### 2. Descripción del Funcionamiento de la Aplicación
La aplicación se divide en dos proyectos principales que manejan las comunicaciones cliente-servidor. A continuación, se explica la estructura y funcionamiento de cada componente:

**Proyecto Servidor**

Este proyecto se encarga de recibir, procesar, y enviar mensajes tanto por protocolo TCP como por UDP. Cuenta con dos formularios:
- Servidor TCP: Establece una conexión confiable para enviar y recibir mensajes entre cliente y servidor.
- Servidor UDP: Permite la comunicación mediante paquetes, enviando de vuelta los mensajes recibidos (eco) y mejorando la velocidad de la comunicación.
  
**Proyecto Cliente**

Este proyecto permite a los usuarios enviar mensajes al servidor y recibir respuestas. También cuenta con dos formularios:
- Cliente TCP: Utiliza una conexión confiable para enviar y recibir mensajes.
- Cliente UDP: Permite el envío y recepción de mensajes en paquetes rápidos, no garantizando la entrega, pero mejorando la rapidez de transmisión.

**Almacenamiento del Historial de Chats**

El historial de chats se guarda en una base de datos MongoDB, permitiendo que todos los mensajes enviados y recibidos sean persistentes. Este almacenamiento proporciona a los usuarios un registro continuo de sus conversaciones, incluso si se desconectan temporalmente.

**Sistema de Registro y Login** (en desarrollo)

Aunque esta funcionalidad está en desarrollo, se planea implementar un sistema de autenticación de usuarios, que almacena en MongoDB los datos de usuario y permite:
- Registro de nuevos usuarios: Almacenando datos básicos de usuario para futuras sesiones.
- Login: Autentica los datos almacenados para asegurar un acceso controlado.

### 3. Conclusión
Este proyecto de chat en C# combina características de comunicación cliente-servidor (TCP y UDP) y persistencia de datos en MongoDB, con una interfaz amigable. El sistema cumple con los requisitos de un proyecto de mensajería básico. A futuro, se buscará mejorar la estabilidad del sistema de registro y login para garantizar un acceso seguro y funcional, entre otras implementaciones.

