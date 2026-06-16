# 📦 SpareWMS (Spare Part Warehouse Management System)

<div align="center">

[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/en-us/sql-server/)
[![Windows Forms](https://img.shields.io/badge/Windows%20Forms-0078D4?style=for-the-badge&logo=windows&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)

Sistem Manajemen Inventori Spare Part Berbasis Desktop

*Aplikasi desktop terintegrasi untuk mengelola spare part di warehouse dengan sistem tracking real-time, transaksi otomatis, dan role-based access control*

[Dokumentasi](#dokumentasi) • [Instalasi](#instalasi) • [Fitur](#fitur-utama) • [Struktur](#struktur-project)

</div>

---

## Daftar Isi

- [Tentang Project](#tentang-project)
- [Fitur Utama](#fitur-utama)
- [Teknologi yang Digunakan](#teknologi-yang-digunakan)
- [Prasyarat](#prasyarat)
- [Instalasi](#instalasi)
- [Konfigurasi Database](#konfigurasi-database)
- [Cara Menjalankan](#cara-menjalankan)
- [Struktur Project](#struktur-project)
- [Dokumentasi Database](#dokumentasi-database)
- [Workflow Aplikasi](#workflow-aplikasi)
- [Kontak](#kontak)

---

## Tentang Project

**SpareWMS** adalah aplikasi desktop yang dibangun untuk mengelola inventori spare part di warehouse dengan sistem yang terorganisir dan efisien. Aplikasi ini memudahkan pengguna dalam melakukan tracking spare part across different racks dan bins, mengelola transaksi masuk-keluar (IN/OUT), serta memelihara kontrol akses berbasis role.

### Latar Belakang

Project ini dikembangkan untuk mengatasi kebutuhan warehouse management yang lebih terstruktur dengan kemampuan tracking real-time, audit trail transaksi, dan sistem keamanan role-based. Dengan menggunakan teknologi .NET Framework dan SQL Server, aplikasi ini menyediakan solusi yang robust dan dapat diandalkan.

### Target Pengguna

| Supervisor | Operator |
|:---:|:---:|
| Kelola master data | View & manage inventory |
| Monitoring transaksi | Input transaksi IN/OUT |
| Generate laporan | Tracking spare parts |
| User management | Absensi kegiatan |

---

## Fitur Utama

### Sistem Keamanan & Autentikasi
- **Login berbasis Role** - Supervisor dan Operator
- **Role-based Access Control** - Kontrol akses berbasis peran
- **User Management** - Kelola pengguna dan role

### Manajemen Spare Part
- **CRUD Spare Part** - Tambah, edit, hapus spare part
- **Kategori Spare Part** - Organisasi berdasarkan kategori (Finished Goods, Raw Materials)
- **Master Data Management** - Kelola data dasar warehouse

### Warehouse Organization
- **Racks Management** - Kelola rak penyimpanan
- **Rows & Bins Configuration** - Setup baris dan bin per rack
- **Location Tracking** - Tracking lokasi exact spare part

### Transaction Management
- **Incoming Transactions (IN)** - Catat barang masuk ke warehouse
- **Outgoing Transactions (OUT)** - Catat barang keluar dari warehouse
- **Transaction Logs** - Audit trail lengkap semua transaksi
- **User Attribution** - Track siapa yang melakukan transaksi

### Inventory & Reporting
- **Real-time Inventory Tracking** - Monitoring stok real-time
- **Capacity Monitoring** - Track kapasitas bin dan rack
- **Transaction History** - Laporan lengkap semua transaksi
- **Activity Timeline** - Timeline lengkap aktivitas warehouse

---

## Teknologi yang Digunakan

<div align="center">

### Core Technologies

[![C Sharp](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![Windows Forms](https://img.shields.io/badge/Windows%20Forms-0078D4?style=for-the-badge&logo=windows&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/desktop/winforms/)

### Database & ORM

[![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/en-us/sql-server/)
[![LINQ to SQL](https://img.shields.io/badge/LINQ%20to%20SQL-512BD4?style=for-the-badge&logo=dotnet)](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql/linq/)

### Development Tools

[![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91?style=for-the-badge&logo=visual-studio&logoColor=white)](https://visualstudio.microsoft.com/)
[![SQL Server Management Studio](https://img.shields.io/badge/SSMS-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://docs.microsoft.com/en-us/sql/ssms/)

</div>

---

### Frontend
- **Windows Forms (WinForms)** - Desktop UI framework
- **Custom Controls** - UI components
- **Data Binding** - Real-time data updates

### Backend & Database
- **LINQ to SQL** - Object-relational mapping
- **SQL Server** - Enterprise database
- **Stored Procedures** - Complex data operations

#### Database Schema

![Database Diagram](wireframe/databaseDiagram.png)

**Entity Relationships:**
- Categories → Racks, SpareParts (1:N)
- Racks → RackRows (1:N)
- RackRows → RackBins (1:N)
- Users → InventoryLogs (1:N)
- SpareParts → Users (CreatedBy, ModifiedBy, DeletedBy)

**Key Tables:**
- **Users** - Authentication & role management
- **Categories** - Spare part categorization
- **Racks** - Warehouse rack organization
- **RackRows/RackBins** - Physical location mapping
- **SpareParts** - Inventory master data
- **InventoryLogs** - Transaction audit trail

---

## UI Wireframes

### Login Form
Autentikasi pengguna dan validasi kredensial

![Login Form](wireframe/loginForm.png)

### Operator Main Dashboard
Interface untuk operator melakukan transaksi inventory masuk dan keluar

![Operator Main Form](wireframe/operatorMainForm.png)

### Supervisor Dashboard
Dashboard komprehensif untuk monitoring dan manajemen master data

![Supervisor Main Form](wireframe/supervisorMainForm.png)

---

## Prasyarat

Sebelum memulai, pastikan Anda telah menginstall:

### 1. Visual Studio 2019 atau lebih baru
- Download dari [https://visualstudio.microsoft.com/](https://visualstudio.microsoft.com/)
- Pilih workload: **Desktop development with C#**

### 2. .NET Framework 4.8
- Biasanya sudah tersedia di Visual Studio
- Verifikasi di **Settings → .NET Frameworks**

### 3. SQL Server 2016 atau lebih baru
- Download dari [https://www.microsoft.com/en-us/sql-server/sql-server-downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Atau gunakan **SQL Server Express** (gratis)

### 4. SQL Server Management Studio (SSMS)
- Download dari [https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

---

## Instalasi

### 1. Clone Repository

```bash
git clone https://github.com/username/SpareWMS.git
cd SpareWMS
```

### 2. Open Project di Visual Studio

1. Buka **Visual Studio**
2. Pilih **File → Open → Project/Solution**
3. Buka file `SpareWMS.sln`
4. Tunggu Visual Studio memproses project

### 3. Restore Solution

```bash
# Right-click pada Solution → Restore NuGet Packages
# atau gunakan Package Manager Console:
Update-Package -Reinstall
```

---

## Konfigurasi Database

### 1. Setup Database Connection

Edit file `App.config` dan sesuaikan connection string SQL Server Anda:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <connectionStrings>
    <add name="SpareWMS.Properties.Settings.SpareWMSConnectionString" 
         connectionString="Data Source=YourDbNameHere;Initial Catalog=DbSpareWMS;Integrated Security=True;TrustServerCertificate=True" 
         providerName="System.Data.SqlClient" />
  </connectionStrings>
  
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.8" />
  </startup>
</configuration>
```

### 2. Configuring App.config

Update connection string di `App.config` agar sesuai dengan instance SQL Server Anda. Berikut panduan konfigurasi:

![App.config Configuration Guide](https://res.cloudinary.com/dxztacmkj/image/upload/v1781501974/AppConfig_pccqek.png)

**Parameter Connection String:**

| Parameter | Deskripsi | Contoh |
|-----------|-----------|---------|
| `Data Source` | Nama/lokasi SQL Server instance | `localhost` atau `DESKTOP-ABC\SQLEXPRESS` |
| `Initial Catalog` | Nama database | `SpareWMS` |
| `Integrated Security` | Gunakan Windows Authentication | `True` (recommended) |
| `User ID` | Username SQL Server (jika tidak menggunakan Windows Auth) | `sa` |
| `Password` | Password SQL Server | `YourPassword` |

**Connection String Examples:**

```xml
<!-- Windows Authentication (Recommended) -->
Data Source=localhost;Initial Catalog=SpareWMS;Integrated Security=True;TrustServerCertificate=True

<!-- SQL Server Authentication -->
Data Source=DESKTOP-ABC\SQLEXPRESS;Initial Catalog=SpareWMS;User ID=sa;Password=YourPassword;TrustServerCertificate=True

<!-- Remote Server -->
Data Source=192.168.1.100,1433;Initial Catalog=SpareWMS;User ID=sa;Password=YourPassword;TrustServerCertificate=True
```

### 3. Database Setup dengan Script

1. Buka **SQL Server Management Studio (SSMS)**
2. Buat koneksi ke SQL Server Anda
3. Buka file `DbSpareWMS.sql` (File → Open → Query File)
4. Jalankan script (tekan F5 atau Execute)

```sql
-- Script akan:
--  Membuat database 'SpareWMS' jika belum ada
--  Membuat semua table dan relationships
--  Insert seed data sampel
```

---

## Cara Menjalankan

### 1. Build Solution

```
Visual Studio:
1. Menu → Build → Build Solution
   atau tekan: Ctrl + Shift + B
```

### 2. Run Application

```
Visual Studio:
1. Tekan F5 atau Click "Start Debugging"
   atau Menu → Debug → Start Debugging

Shortcut: F5
```

### 3. Login ke Aplikasi

Gunakan akun sample berikut (dari seed data):

**Supervisor Account:**
- Username: `budi.sup`
- Password: `Pass123!`

**Operator Account:**
- Username: `andi.op`
- Password: `Op12345`

---

## Struktur Project

```
SpareWMS/
│
├── App.config                      # Konfigurasi aplikasi & connection string
├── Program.cs                      # Entry point aplikasi
├── DbSpareWMS.sql                  # Database initialization script
├── SpareWMS.csproj                 # Project file
├── SpareWMS.sln                    # Solution file
│
├── Properties/
│   ├── AssemblyInfo.cs            # Assembly metadata
│   ├── Resources.resx             # Resource files
│   ├── Resources.Designer.cs      # Auto-generated
│   ├── Settings.settings          # Application settings
│   └── Settings.Designer.cs       # Auto-generated
│
├── Forms/                          # Windows Forms UI
│   ├── Form1.cs                   # Login screen
│   ├── Form1.Designer.cs          # Form1 designer
│   ├── Form1.resx                 # Form1 resources
│   ├── Form2.cs                   # Main dashboard
│   ├── Form3.cs                   # Spare parts management
│   ├── Form4.cs                   # Warehouse setup
│   ├── Form5.cs                   # Transactions
│   ├── Form6.cs                   # Reports
│   └── ...Designer.cs / ...resx   # Designer files untuk setiap form
│
├── Data/
│   ├── DataClasses1.dbml          # LINQ to SQL mapping
│   ├── DataClasses1.designer.cs   # Auto-generated LINQ classes
│   └── DataClasses1.dbml.layout   # DBML layout
│
├── Models/
│   └── Class1.cs                  # Business logic classes
│
├── bin/
│   └── Debug/                     # Compiled executable
│       └── SpareWMS.exe
│
├── obj/
│   └── Debug/                     # Build artifacts
│
└── README.md                       # Dokumentasi project
```

### Penjelasan Folder Utama

| Folder | Fungsi |
|--------|--------|
| **Forms/** | Berisi semua UI screens (Form1 - Form6) |
| **Data/** | LINQ to SQL classes mapping dan DbML |
| **Models/** | Business logic dan helper classes |
| **Properties/** | Assembly info, resources, settings |
| **bin/** | Compiled executable files |

---

## Dokumentasi Database

### Database Schema Overview

```
SpareWMS Database
│
├── Users
│   ├── Id (PK)
│   ├── FullName
│   ├── Username (UNIQUE)
│   ├── Password
│   └── Role (Supervisor/Operator)
│
├── Categories
│   ├── Id (PK)
│   └── Name
│
├── Racks
│   ├── Id (PK)
│   ├── RackCode
│   ├── CategoryId (FK → Categories)
│   └── Description
│
├── RackRows
│   ├── Id (PK)
│   ├── RackId (FK → Racks)
│   ├── RowNumber
│   └── RowName
│
├── SpareParts
│   ├── Id (PK)
│   ├── PartNumber (UNIQUE)
│   ├── CategoryId (FK → Categories)
│   ├── Name
│   ├── IsActive
│   ├── MaxCapacityInRack
│   ├── CreatedAt / CreatedByUserId
│   ├── ModifiedAt / ModifiedByUserId
│   ├── DeletedAt / DeletedByUserId
│   └── [Relationships dengan Users]
│
├── RackBins
│   ├── Id (PK)
│   ├── RackRowId (FK → RackRows)
│   ├── BinNumber
│   ├── BinCode
│   ├── SparePartId (FK → SpareParts)
│   ├── Capacity
│   ├── EntryDate
│   └── ModifiedAt
│
└── InventoryLogs
    ├── Id (PK)
    ├── BinId (FK → RackBins)
    ├── SparePartId (FK → SpareParts)
    ├── TransactionType (IN/OUT)
    ├── Quantity
    ├── TransactionDate
    └── UserId (FK → Users)
```

### Entity Relationships

**One-to-Many Relationships:**
- `Categories` → `Racks` (1:N)
- `Categories` → `SpareParts` (1:N)
- `Racks` → `RackRows` (1:N)
- `RackRows` → `RackBins` (1:N)
- `Users` → `InventoryLogs` (1:N)

**Many-to-One Relationships:**
- `SpareParts` → `Users` (CreatedBy, ModifiedBy, DeletedBy)
- `RackBins` → `SpareParts` (1:1 optional)

---

## Workflow Aplikasi

### Login Flow
```
┌─────────────┐
│ Start App   │
└──────┬──────┘
       │
       ▼
┌──────────────────┐      ┌─────────────┐
│ Show Login Form  │──→───│ Valid User? │
└──────┬───────────┘      └──────┬──────┘
       │                         │
       │ Yes                     │ No
       ▼                         ▼
  ┌─────────────────┐    ┌────────────────┐
  │ Check Role      │    │ Show Error Msg │
  └────────┬────────┘    └──────┬─────────┘
           │                    │
      ┌────┴─────┬──────┐       │
      │           │      │      │
Supervisor  Operator Others    Back to
      │           │      │      Login
      ▼           ▼      ▼      ▼
  Dashboard  Dashboard Error
```

### Inventory Transaction Flow
```
Start Transaction
       │
       ▼
Select Spare Part
       │
       ▼
Choose Rack/Bin
       │
       ▼
Select Transaction Type (IN/OUT)
       │
       ▼
Enter Quantity
       │
       ▼
Validate Capacity
       │
   ┌───┴───┐
   │       │
 Valid   Invalid
   │       │
   ▼       ▼
Create  Show Error
Record
   │
   ▼
Create Inventory Log
   │
   ▼
Update Bin Capacity
   │
   ▼
Success
```

---

## Development Guide

### Building dari Command Line

```bash
# Navigate ke project folder
cd SpareWMS

# Build solution
msbuild SpareWMS.sln /p:Configuration=Debug

# Build release
msbuild SpareWMS.sln /p:Configuration=Release
```

### Database Troubleshooting

**Error: "Cannot connect to database"**
```
 Solution:
1. Buka SSMS
2. Verify koneksi ke SQL Server
3. Check App.config connection string
4. Pastikan database SpareWMS sudah ada
```

**Error: "Database does not exist"**
```
 Solution:
1. Buka SSMS
2. Jalankan DbSpareWMS.sql script
3. Verify database dibuat successfully
```

---

## Sample Data

Database sudah include sample data:

### Users (5 sample accounts)
```
Supervisor:
- budi.sup (Pass123!)
- siti.sup (Pass123!)

Operator:
- andi.op (Op12345)
- rina.op (Op12345)
- fajar.op (Op12345)
```

### Spare Parts (20 items)
- 10 Finished Goods items
- 10 Raw Materials items

### Warehouse Setup
- 5 Racks
- 15 RackRows (3 levels per rack)
- 25+ RackBins

---

## Known Issues & Limitations

| Issue | Status | Workaround |
|-------|--------|-----------|
| Report generation |  In Progress | Manual export to Excel |
| Real-time sync |  Planned | Refresh manual |
| Multi-user concurrent access |  Limited | Implement row-level locking |
| Mobile access |  Not Available | Use on desktop only |

---

## Changelog

<details>
<summary><b>Version 1.0.0 (2026) - Current Release </b></summary>

### Features Implemented
-  User authentication dengan role-based access
-  CRUD Spare Parts management
-  Warehouse structure setup (Racks, Rows, Bins)
-  Inventory transaction management (IN/OUT)
-  Transaction logs dan audit trail
-  Real-time inventory tracking
-  User & role management

### Technical Implementation
-  Windows Forms desktop application
-  LINQ to SQL data access layer
-  SQL Server database integration
-  .NET Framework 4.8 compatibility

</details>


---

## Kontak

<div align="center">

### Let's Connect!

**Abby Dahlan Havizh**

[![Email](https://img.shields.io/badge/Email-D14836?style=for-the-badge&logo=gmail&logoColor=white)](mailto:abby11dahlan@gmail.com)
[![Instagram](https://img.shields.io/badge/Instagram-E4405F?style=for-the-badge&logo=instagram&logoColor=white)](https://www.instagram.com/abayy_____________/)
[![GitHub](https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/abayDahln)

---

### Support This Project

<table>
<tr>
<td align="center">
 Star this repo
</td>
<td align="center">
 Share with others
</td>
<td align="center">
 Report issues
</td>
<td align="center">
 Suggest features
</td>
</tr>
</table>

---

<img src="https://img.icons8.com/fluency/48/000000/code.png"/> **Made with ❤ by Abby Dahlan Havizh** <img src="https://img.icons8.com/fluency/48/000000/code.png"/>

*Building digital solutions for warehouse management excellence*

 **If this project helps you, please give it a star!** 

</div>








