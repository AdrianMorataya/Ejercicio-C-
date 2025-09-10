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

USE empresa_db;

INSERT INTO clientes (nombre, correo_contacto, direccion, zona) VALUES
('Juan Pérez', 'juan@example.com', 'Zona 1, Ciudad', 1),
('María López', 'maria@example.com', 'Zona 2, Ciudad', 2),
('Carlos Gómez', 'carlos@example.com', 'Zona 3, Ciudad', 1),
('Ana Martínez', 'ana@example.com', 'Zona 4, Ciudad', 3),
('Luis Fernández', 'luis@example.com', 'Zona 5, Ciudad', 2);

INSERT INTO usuarios (cliente_id, nombre, apellido, correo, password_hash) VALUES
(1, 'Pedro', 'Sánchez', 'pedro@example.com', 'hash1'),
(2, 'Lucía', 'Ramírez', 'lucia@example.com', 'hash2'),
(3, 'Miguel', 'Torres', 'miguel@example.com', 'hash3'),
(4, 'Sofía', 'Hernández', 'sofia@example.com', 'hash4'),
(5, 'Jorge', 'Vargas', 'jorge@example.com', 'hash5');

INSERT INTO tipos_producto (nombre) VALUES
('Bebida'),
('Snack'),
('Electrónica');

INSERT INTO productos (tipo_id, nombre, precio) VALUES
(1, 'Coca-Cola', 1.50),
(1, 'Agua Mineral', 1.00),
(2, 'Papas Fritas', 2.00),
(2, 'Chocolate', 1.80),
(3, 'Auriculares', 25.00);

INSERT INTO proveedores (nombre, contacto, telefono, direccion) VALUES
('Proveedor A', 'Carlos', '555-1234', 'Calle 1'),
('Proveedor B', 'Ana', '555-5678', 'Calle 2'),
('Proveedor C', 'Luis', '555-9012', 'Calle 3');

INSERT INTO categorias_insumo (nombre) VALUES
('Bebidas'),
('Snacks'),
('Electrónica');

INSERT INTO insumos (categoria_id, proveedor_id, nombre, costo) VALUES
(1, 1, 'Lata de Coca-Cola', 0.80),
(1, 1, 'Botella de Agua', 0.50),
(2, 2, 'Bolsa de Papas', 1.00),
(2, 2, 'Barra de Chocolate', 0.90),
(3, 3, 'Auriculares Inalámbricos', 15.00);

INSERT INTO productos_insumos (producto_id, insumo_id, cantidad) VALUES
(1, 1, 1),
(2, 2, 1),
(3, 3, 1),
(4, 4, 1),
(5, 5, 1);

INSERT INTO ventas (cliente_id, usuario_id, producto_id, monto, metodo_pago) VALUES
(1, 1, 1, 1.50, 1),
(2, 2, 2, 1.00, 2),
(3, 3, 3, 2.00, 3),
(4, 4, 4, 1.80, 1),
(5, 5, 5, 25.00, 2);

INSERT INTO reportes (tipo, descripcion) VALUES
('Inventario', 'Reporte de stock inicial'),
('Ventas', 'Reporte de ventas semanales'),
('Proveedores', 'Reporte de proveedores activos');

SELECT * FROM clientes;

