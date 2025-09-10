DROP DATABASE IF EXISTS empresa_db;

CREATE DATABASE empresa_db;
USE empresa_db;

CREATE TABLE clientes (
    cliente_id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    correo_contacto VARCHAR(100) UNIQUE NOT NULL,
    direccion VARCHAR(200),
    zona INT
);

CREATE TABLE usuarios (
    usuario_id INT AUTO_INCREMENT PRIMARY KEY,
    cliente_id INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    correo VARCHAR(100) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    FOREIGN KEY (cliente_id) REFERENCES clientes(cliente_id)
);

CREATE TABLE tipos_producto (
    tipo_id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL
);

CREATE TABLE productos (
    producto_id INT AUTO_INCREMENT PRIMARY KEY,
    tipo_id INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    precio DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (tipo_id) REFERENCES tipos_producto(tipo_id)
);

CREATE TABLE ventas (
    venta_id INT AUTO_INCREMENT PRIMARY KEY,
    cliente_id INT NOT NULL,
    usuario_id INT NOT NULL,
    producto_id INT NOT NULL,
    fecha DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    monto DECIMAL(10,2) NOT NULL,
    metodo_pago TINYINT NOT NULL, -- 1=Efectivo, 2=Tarjeta, 3=Transferencia
    FOREIGN KEY (cliente_id) REFERENCES clientes(cliente_id),
    FOREIGN KEY (usuario_id) REFERENCES usuarios(usuario_id),
    FOREIGN KEY (producto_id) REFERENCES productos(producto_id)
);

CREATE TABLE reportes (
    reporte_id INT AUTO_INCREMENT PRIMARY KEY,
    tipo VARCHAR(50) NOT NULL,
    fecha_generacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    descripcion TEXT
);

CREATE TABLE proveedores (
    proveedor_id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    contacto VARCHAR(100),
    telefono VARCHAR(50),
    direccion VARCHAR(200)
);

CREATE TABLE categorias_insumo (
    categoria_id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL
);

CREATE TABLE insumos (
    insumo_id INT AUTO_INCREMENT PRIMARY KEY,
    categoria_id INT NOT NULL,
    proveedor_id INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    costo DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (categoria_id) REFERENCES categorias_insumo(categoria_id),
    FOREIGN KEY (proveedor_id) REFERENCES proveedores(proveedor_id)
);

CREATE TABLE productos_insumos (
    producto_id INT NOT NULL,
    insumo_id INT NOT NULL,
    cantidad DECIMAL(10,2),
    PRIMARY KEY (producto_id, insumo_id),
    FOREIGN KEY (producto_id) REFERENCES productos(producto_id),
    FOREIGN KEY (insumo_id) REFERENCES insumos(insumo_id)
);
