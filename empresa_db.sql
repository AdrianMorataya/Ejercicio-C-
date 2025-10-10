DROP DATABASE IF EXISTS empresa_db;

CREATE DATABASE empresa_db;
USE empresa_db;

CREATE TABLE usuarios (
    usuario_id INT AUTO_INCREMENT PRIMARY KEY,
    rol VARCHAR(20) NOT NULL DEFAULT 'Empleado',
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    correo VARCHAR(100) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL
);

CREATE TABLE tipos_producto (
    tipo_id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL
);

CREATE TABLE productos (
    producto_id INT AUTO_INCREMENT PRIMARY KEY UNIQUE,
    tipo_id INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    precio DECIMAL(10,2) NOT NULL,
    activo TINYINT(1) NOT NULL DEFAULT 1,
    stock INT NOT NULL,
    FOREIGN KEY (tipo_id) REFERENCES tipos_producto(tipo_id)
);

CREATE TABLE ventas (
    venta_id INT AUTO_INCREMENT PRIMARY KEY,
    usuario_id INT NOT NULL,
    producto_id INT NOT NULL,
    fecha DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    monto DECIMAL(10,2) NOT NULL,
    metodo_pago TINYINT NOT NULL,
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
    proveedor_id INT AUTO_INCREMENT PRIMARY KEY UNIQUE,
    nombre VARCHAR(100) NOT NULL,
    contacto VARCHAR(100),
    telefono VARCHAR(50),
    direccion VARCHAR(200),
    activo TINYINT(1) NOT NULL DEFAULT 1
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


INSERT INTO usuarios ( nombre, apellido, correo, password_hash) VALUES
( 'Pedro', 'Sánchez', 'pedro@example.com', 'hash1'),
( 'Lucía', 'Ramírez', 'lucia@example.com', 'hash2'),
( 'Miguel', 'Torres', 'miguel@example.com', 'hash3'),
( 'Sofía', 'Hernández', 'sofia@example.com', 'hash4'),
( 'Jorge', 'Vargas', 'jorge@example.com', 'hash5');

INSERT INTO tipos_producto (nombre) VALUES
('Bebida'),
('Snack'),
('Electrónica');

INSERT INTO productos (stock, tipo_id, nombre, precio, activo) VALUES
(10, 1, 'Coca-Cola', 1.50, 1),
(25, 1, 'Agua Mineral', 1.00, 1),
(12, 2, 'Papas Fritas', 2.00, 1),
(6, 2, 'Chocolate', 1.80, 1),
(53, 3, 'Auriculares', 25.00, 1);

INSERT INTO proveedores (nombre, contacto, telefono, direccion) VALUES
('Proveedor A', 'Carlos', '555-1234', 'Calle 1'),
('Proveedor B', 'Ana', '555-5678', 'Calle 2'),
('Proveedor C', 'Luis', '555-9012', 'Calle 3'),
('Proveedor Juan', 'Juan', '111-2222', 'Zona 2');

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

INSERT INTO ventas (usuario_id, producto_id, monto, metodo_pago) VALUES
( 1, 1, 1.50, 1),
( 2, 2, 1.00, 2),
( 3, 3, 2.00, 3),
( 4, 4, 1.80, 1),
( 5, 5, 25.00, 2);

INSERT INTO reportes (tipo, descripcion) VALUES
('Inventario', 'Reporte de stock inicial'),
('Ventas', 'Reporte de ventas semanales'),
('Proveedores', 'Reporte de proveedores activos');

SELECT * FROM usuarios;
SELECT * FROM productos;
SELECT * FROM tipos_producto;
SELECT * FROM categorias_insumo;
select * from insumos;
select * from proveedores;
select * from ventas;