# Flor Indumentaria

**Tienda online de ropa** — Trabajo práctico individual de Programación IV (UNLZ).

---

## ¿Qué es?

Es una página web de una tienda de ropa. Las personas entran con su **cuenta de Google**, miran el catálogo y compran. La vendedora (el **administrador**) carga las categorías y los productos con sus fotos, y ve todas las ventas.

## ¿Qué se puede hacer?

**Como cliente:**
- Entrar con la cuenta de Google.
- Ver el catálogo y filtrar por categoría.
- Comprar con el botón verde **Comprar**.
- Ver lo que compró en **Mis compras**.

**Como administrador (vendedor):**
- Crear, editar y dar de baja **categorías** y **productos** (con foto).
- Ver todas las **ventas**, con un aviso cuando alguien compra.
- Dar o quitar el permiso de administrador a otras personas.

## ¿Con qué está hecho?

- **C#** con **ASP.NET Core MVC** (.NET 10)
- Base de datos **SQL Server Express**, manejada con **Entity Framework Core**
- Inicio de sesión con **Google**
- Diseño con **Bootstrap** y estilos propios

---

## Antes de empezar

Hay que tener instalado:

1. **[.NET 10](https://dotnet.microsoft.com/download)**
2. **SQL Server Express** (con el nombre de instancia `SQLEXPRESS`)
3. **Visual Studio** o la herramienta de Entity Framework para consola:

```bash
dotnet tool install --global dotnet-ef --version 10.0.12
```

---

## Cómo ponerlo en marcha

### Paso 1 — Descargar el proyecto

```bash
git clone https://github.com/Florencia-Escubilla/Trabajo-Practico.git
```

```bash
cd Trabajo-Practico
```

### Paso 2 — Crear la base de datos

Este comando crea la base de datos **TiendaRopa** con todas sus tablas y algunos productos de ejemplo:

```bash
dotnet ef database update --project TrabajoPracticoDatos --startup-project "Trabajo Practico"
```

> **Otra opción:** restaurar la copia de la base que está en `BaseDeDatos/TiendaRopa.bak` (ver más abajo).

### Paso 3 — Conectar con Google

Para que funcione el botón **Ingresar con Google**, la aplicación necesita dos códigos que da Google: el **ID de cliente** y el **Secreto del cliente**.

**Cómo conseguirlos:**

1. Entrar a [Google Cloud Console](https://console.cloud.google.com/) → **Google Auth Platform** → **Clientes** → **Crear cliente**.
2. Tipo de aplicación: **Aplicación web**.
3. En **URI de redireccionamiento autorizados**, agregar: `https://localhost:7129/signin-google`
4. Copiar el **ID de cliente** y el **Secreto del cliente**.
5. En **Público** → **Usuarios de prueba**, agregar los correos de Google que van a poder entrar a la tienda.

**Dónde pegarlos:** en Visual Studio, clic derecho en el proyecto **Trabajo Practico** → **Administrar secretos de usuario**, y pegar:

```json
{
  "GoogleKeys": {
    "ClientId": "ACÁ-EL-ID-DE-CLIENTE",
    "ClientSecret": "ACÁ-EL-SECRETO-DEL-CLIENTE"
  }
}
```

Ese archivo queda guardado solo en tu computadora y **no se sube a GitHub**.

### Paso 4 — Ejecutar

Abrir `Trabajo Practico.slnx` con Visual Studio y apretar ▶ **https**.

La tienda se abre en **https://localhost:7129**.

---

## Usuarios y permisos

Hay dos tipos de usuario (**roles**):

| Rol | Qué puede hacer |
|---|---|
| **Cliente** | Ver el catálogo, comprar y ver sus compras |
| **Administrador** | Manejar categorías, productos, usuarios y ver las ventas |

**¿Quién es administrador?**
- La cuenta **escubillaweb@gmail.com** entra siempre como **administrador**. Ese correo está configurado en `Trabajo Practico/appsettings.json`, en `AdministradorPrincipal`.
- **Cualquier otra cuenta** de Google, la primera vez que entra, queda registrada como **cliente**.


## Cómo dar o quitar el permiso de administrador

**Desde la página**

1. Entrar con una cuenta de administrador.
2. Ir a **Administración** → **Usuarios**.
3. Tocar **Hacer administrador** (para darlo) o **Quitar administrador** (para sacarlo).

**Importante:** después de cambiar el permiso, esa persona tiene que **cerrar sesión y volver a entrar** para que se note el cambio.

## Cómo está organizado el código

El proyecto está dividido en **dos partes**:

| Parte | Qué tiene |
|---|---|
| **Trabajo Practico** — la web (lo que se ve) | Las páginas, los controladores, los estilos, las fotos y la configuración |
| **TrabajoPracticoDatos** — los datos (lo que se guarda) | Las clases de la base de datos, las migraciones y los repositorios que leen y guardan la información |

## Copia de seguridad de la base de datos

En la carpeta `BaseDeDatos` está el archivo **`TiendaRopa.bak`**, una copia completa de la base de datos (hecha con SQL Server 2025 Express).

Para restaurarla: en SQL Server Management Studio, clic derecho en **Bases de datos** → **Restaurar base de datos** → **Dispositivo** → elegir el archivo `TiendaRopa.bak`.
