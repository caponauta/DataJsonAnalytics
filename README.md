# DataJsonAnalytics

El objeto del proyecto es recibir una estructura Json para que sea guardado en una base de datos PostgreSQL definida, toda la data que sea contenida en el mismo. 

## Lenguaje utilizado
- La implementación se realizó con .NET core 5.0.
- Se utiliza un paquete de compatibilidad para la conexión hacia PostgreSQL. (https://www.npgsql.org/)
- De manera local, el servicio queda con la URL a la api:
  https://localhost:44334/api/main/save-data
- Se cambia al puerto 5024, quedando la URL para consumir el servicio de la siguiente manera: 
  http://localhost:5024/api/main/save-data



## PostgreSQL
La estructura de datos definida para la base de datos es:

![Diagrama BD](/Images/DiagramaBD.png "Diagrama de diseño propuesto para guardar la información del Json")


## Docker

En la configuración, se cambia el puerto para que sea usado el 5024 y se ejecutan los comandos para la creación de la imagen y puesta en marcha: 

docker build -t apijson .

docker run -d -p 5024:5024 --name JsonAnalytics apijson

