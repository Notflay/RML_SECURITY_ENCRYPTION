# Proyecto de Cifrado y Descifrado

Este proyecto permite cifrar y descifrar valores utilizando el algoritmo AES. Para su correcto funcionamiento, es necesario proporcionar un archivo de configuración llamado `_encryption.xml` que contendrá las claves necesarias para el proceso de encriptación y desencriptación.

## Requisitos

- Crear un archivo `_encryption.xml` en el directorio raíz del proyecto.
- El archivo debe contener las siguientes claves:
  - `aesSecret`: Se utiliza para `secretKeyBytes`.
  - `aesIV`: Se utiliza para `ivBytes`.

## Formato del Archivo `_encryption.xml`

El archivo debe seguir la siguiente estructura:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<SBO>
    <add key="aesSecret" value="vzc-exqt-pwd-77" />
    <add key="aesIV" value="tgczza-iv-ioz-91" />
</SBO>
```

## Instalación y Uso

1. Clona el repositorio:
   ```sh
   git clone https://github.com/Notflay/RML_SECURITY_ENCRYPTION.git
   ```
2. Asegúrate de tener el archivo `_encryption.xml` correctamente configurado.
3. Ejecuta el proyecto según las instrucciones específicas del entorno.

## Notas

- Asegúrate de mantener seguro el archivo `_encryption.xml` y no incluirlo en el control de versiones.
- Puedes agregar `_encryption.xml` al archivo `.gitignore` para evitar su subida accidental al repositorio.

## Contribuciones

Si deseas contribuir a este proyecto, por favor abre un *pull request* o reporta problemas en la sección de *issues*.

## Licencia

Este proyecto está bajo la licencia [MIT](LICENSE).

