<p>**Challenge para la empresa Tekton**</p>
<p>Este challenge debe contener lo siguiente:
1.1. Crear un rest API en .net Core (última versión).
1.2. Documentar la API usando swagger.
1.3. Usar patrones (ejemplo: mediator pattern, repository pattern, cqrs, etc).
1.4. Aplicar principios SOLID y Clean Code.
1.5. Implementar la solución haciendo uso de TDD.
1.6. Usar buenos patrones para las validaciones del Request, y además,
considerar los HTTP Status Codes en cada petición realizada.
1.7. Estructurar el proyecto en N-capas.
1.8. Agregar un archivo readme (README.md) en dónde se haga una breve
descripción de los patrones o arquitectura usada en el proyecto. Además,
poner los pasos para levantar el proyecto localmente.También, se puede
agregar alguna información que se considere útil.
1.9. Subir el proyecto a github de manera pública.
</p>
**Pasos para levantar el proyecto localmente**
1. En el Package manager console, ejecutar la migración d ela base de datos
   add-migration MyFirstMigration -o migrations
2. Despues de la migración hacer el update para que tome los cambios
   update-database

**Descipcion de los patrones de aruitectura usados**
**1. CQRS**
  El patrón de arquitectura CQRS (Command Query Responsibility Segregation) es una forma de organizar y estructurar el código en sistemas informáticos. 
  Para entenderlo sin un trasfondo técnico, podemos usar una analogía con una biblioteca.

  **Descripción Analógica:**
  Imagina una biblioteca donde los usuarios pueden leer libros (operación de consulta) y los bibliotecarios pueden agregar o quitar libros (operación de comando). 
  En un sistema tradicional, todos se dirigen al mismo mostrador para realizar ambas operaciones. Esto puede generar congestión y dificultades de gestión.

  CQRS propone separar estas operaciones. Habría un mostrador exclusivo para consultas, donde los usuarios obtienen información, y otro para comandos, 
  donde los bibliotecarios realizan cambios en la colección. Esto mejora la eficiencia y simplifica la gestión de la biblioteca.

  **Aplicación en Sistemas:**
  En términos generales, CQRS sugiere separar las operaciones de lectura y escritura en un sistema. Las consultas (lecturas) se manejan 
  de manera diferente a los comandos (escrituras). Esto permite optimizar cada parte del sistema según sus necesidades específicas, 
  mejorando el rendimiento y la flexibilidad.

  En resumen, CQRS es como tener mostradores especializados en una biblioteca, uno para obtener información y otro para realizar cambios,
  mejorando la eficiencia y la gestión en sistemas informáticos.
  
**2. Mediator**
  El Patrón de Arquitectura Mediator es como tener un "coordinador" central en un grupo de personas. Imagina que tienes un proyecto en el que varias 
  personas trabajan juntas, pero en lugar de que cada persona hable directamente con todas las demás, todas se comunican a través de un coordinador.

  En un equipo sin coordinador, cada persona tendría que hablar con todas las demás, lo que podría volverse caótico. Con el Mediator, todos se conectan 
  al coordinador, y el coordinador se encarga de distribuir la información adecuada a las personas correctas.

  En términos más técnicos, este patrón ayuda a reducir las dependencias caóticas entre objetos en un sistema informático. Cada objeto no necesita 
  conocer todos los demás, sino solo el Mediator, que gestiona las interacciones.

  En resumen, el Mediator actúa como un intermediario central, facilitando la comunicación ordenada y reduciendo la complejidad en proyectos donde 
  múltiples partes deben colaborar.

**3. Repository**
  El Patrón de Arquitectura Repository es como tener una "caja fuerte" para almacenar y gestionar todos los datos de una aplicación. 
  Imagina que estás organizando tu casa y decides guardar todas tus pertenencias valiosas en una caja fuerte centralizada en lugar de 
  tenerlas dispersas por toda la casa.

  En términos más técnicos, el Repository actúa como un intermediario entre la aplicación y la fuente de datos (como una base de datos). 
  En lugar de que cada parte de la aplicación acceda directamente a la base de datos, utilizan el Repository para almacenar y recuperar datos 
  de manera organizada y segura.

  Este enfoque tiene beneficios, como centralizar la gestión de datos, facilitar las actualizaciones y proporcionar un punto 
  único de acceso para toda la aplicación.

  En resumen, el Repository es como una caja fuerte central que almacena y gestiona todos los datos de la aplicación de manera organizada y segura.
