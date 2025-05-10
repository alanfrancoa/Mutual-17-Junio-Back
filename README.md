# 📍 README Backend - Mutual 17 de Junio

## 📌 Descripción del Proyecto

Sistema integral de gestión desarrollado para **Mutual 17 de Junio** que moderniza y automatiza sus procesos operativos, reemplazando un sistema legacy obsoleto. Centraliza la administración de asociados, préstamos, inventario y más, con especial énfasis en:

- 🚀 **Automatización** de procesos manuales  
- 🔐 **Seguridad de datos** (Ley 25.326)  
- 📈 **Generación de reportes** financieros y de gestión  
- ⚖️ **Cumplimiento normativo** INAES (Ley 20.321)  

---

## ✨ Funcionalidades Principales  

| Módulo          | Características Clave |  
|----------------|-----------------------|  
| **Asociados**   | CRUD con árbol familiar, historial crediticio |  
| **Préstamos**   | Cálculo automático de intereses INAES, generación de pagarés PDF |  
| **Cobranzas**   | Alertas automáticas (Cron jobs), integración con descuentos |  
| **Inventario**  | Movimientos de stock vinculados a compras |  
| **Proveedores** | Flujo completo: órdenes → facturas → pagos |  
| **Reportes**    | Generación XLSX/PDF compatibles con INAES |  
| **Usuarios**    | RBAC con JWT y auditoría de acceso |  

---

## 🛠️ Stack Tecnológico  
- **Backend**: 
  - .NET Core 6.0 + Entity Framework Core 7
  - SQL Server 2022 (Always Encrypted)
  - Redis (caché de consultas)
- **Seguridad**: 
  - Encriptación AES-256
- **Compatibilidad**: 
  - Windows 7+ (Modo Compatibility)
  - Mínimo 4GB RAM / 50GB HDD

---

## ⚙️ Configuración Local  
1. Requisitos:
```bash
- .NET SDK 6.0.403
- SQL Server 2022 Developer Edition
- Windows 11
