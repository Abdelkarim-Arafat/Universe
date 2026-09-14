<div align="center">

<img src="https://img.shields.io/badge/Universe-University%20Management%20System-4A90D9?style=for-the-badge&logo=graduation-cap&logoColor=white" alt="Universe"/>

# 🎓 Universe

### Enterprise University Management System

A comprehensive university management platform designed to digitalize and streamline academic and administrative operations within higher education institutions.

[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-Latest-239120?style=flat-square&logo=csharp)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927?style=flat-square&logo=microsoftsqlserver)](https://www.microsoft.com/en-us/sql-server)
[![Redis](https://img.shields.io/badge/Redis-Cache-DC382D?style=flat-square&logo=redis)](https://redis.io/)
[![SignalR](https://img.shields.io/badge/SignalR-Real--Time-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/en-us/apps/aspnet/signalr)
[xUnit](https://xunit.net/) ([image](https://img.shields.io/badge/xUnit-Unit%20Testing-512BD4?style=flat-square&logo=xunit))
[Docker](https://www.docker.com/) ([image](https://img.shields.io/badge/Docker-Containerized-2496ED?style=flat-square&logo=docker))
[![License](https://img.shields.io/badge/License-Academic-blue?style=flat-square)](#-license)

</div>

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Core Features](#-core-features)
- [Architecture](#-architecture)
- [Tech Stack](#-tech-stack)
- [Getting Started](#-getting-started)
- [API Documentation](#-api-documentation)
- [User Roles](#-user-roles)
- [Security](#-security)
- [Performance](#-performance)

---

## 🚀 Overview

Managing university operations typically requires multiple disconnected systems and significant manual effort. **Universe** unifies every part of the academic lifecycle into a single, cohesive platform — from student enrollment to graduation.

```
Student Affairs  ──►  Manages academic structure, staff, courses & results
Staff Members    ──►  Advise students, enter grades, communicate
Students         ──►  Access records, schedules, services & grades
```

Built as a **Graduation Project**, Universe demonstrates enterprise-grade software engineering practices including Clean Architecture, CQRS, distributed caching, real-time communication, and cloud integrations.

---

## ✨ Core Features

### 🏛️ Student Affairs Management

<strong>Academic Structure</strong>

- Create and manage colleges and academic programs
- Define academic levels, credit hours, and workload rules
- Create and manage courses
- Configure per-semester course offerings
- Open and manage academic years and semesters


<strong>Campus Management</strong>

- Manage university buildings and classrooms
- Organize and track academic facilities


<strong>Student Management</strong>

- Register new students and manage full profiles
- Manage personal, contact, family, military, and qualification data
- Transfer students between academic programs


<strong>Staff & Advising</strong>

- Add and manage staff members
- Assign staff as academic advisors with dedicated student groups
- View and manage advisor–student relationships


<strong>Registration & Scheduling</strong>

- Register students in courses each semester
- Auto-generate course schedules
- Define and publish examination schedules


<strong>Assessment Control</strong>

- Enable or disable grade entry per semester
- Record and update student grades
- Control result visibility and publish final semester results


---

### 👨‍🎓 Student Portal

| Feature | Description |
|---|---|
| 📄 Personal Info | View and track personal academic profile |
| 📊 Academic Records | Full history of registered courses and grades |
| 🏆 Semester Results | View grades and GPA per semester |
| 📝 Detailed Grades | Breakdown of marks per course component |
| 🗓️ Course Schedule | View weekly timetable |
| 📅 Exam Schedule | View examination dates for registered courses |

---

### 💳 Student Services Marketplace

A built-in digital services marketplace connecting students with university administrative services.

```
Student Affairs  ──►  Publish services with fees
Students         ──►  Browse, request, and pay online
Payment Gateway  ──►  PayPal integration for secure transactions
```

---

### 💬 Real-Time Messaging

Built-in messaging powered by **SignalR** for instant communication.

- ✅ Staff ↔ Staff messaging
- ✅ Staff ↔ Student messaging
- ✅ Threaded conversations & replies
- ✅ Read / Unread tracking
- ✅ Soft deletion support
- ✅ Real-time delivery

---

### 🔔 Real-Time Notifications

- Instant push notifications via **SignalR**
- Unread notification counters
- Full notification history
- Mark notifications as seen

---

### ☁️ Media Management

Profile images are stored in the cloud via **Cloudinary** — no local file storage, faster delivery, and better scalability.

---

## 🏗️ Architecture

Universe follows **Clean Architecture** principles, ensuring strict separation of concerns and long-term maintainability.

```
┌─────────────────────────────────────┐
│         Presentation Layer          │  ASP.NET Core Web API
│           (Controllers)             │
└───────────────────┬─────────────────┘
                    │
┌───────────────────▼─────────────────┐
│         Application Layer           │  CQRS + MediatR + FluentValidation
│    (Commands, Queries, Handlers)    │
└───────────────────┬─────────────────┘
                    │
┌───────────────────▼─────────────────┐
│           Domain Layer              │  Entities, interfaces, Dtos,
│      (Core Business Logic)          │  Enums & Abstractions
└───────────────────┬─────────────────┘
                    │
┌───────────────────▼─────────────────┐
│       Infrastructure Layer          │  EF Core, Repositories, SignalR,
│   (External Concerns & Services)    │  Cloudinary, PayPal, Background Jobs
└─────────────────────────────────────┘
```

### 🧩 Design Patterns

| Pattern | Usage |
|---|---|
| **Clean Architecture** | Layer separation and dependency inversion |
| **CQRS** | Separate read and write models via MediatR |
| **Repository Pattern** | Abstracted data access per aggregate |
| **Unit of Work** | Atomic transactions across repositories |
| **Dependency Injection** | Built-in .NET DI container |

---

## ⚙️ Tech Stack

### Backend
| Technology | Purpose |
|---|---|
| ASP.NET Core | Web API framework |
| C# | Primary language |
| Entity Framework Core | ORM and database migrations |
| MediatR | CQRS pipeline and request handling |
| FluentValidation | Request validation |
| SignalR | Real-time WebSocket communication |
| Background Jobs | OTP delivery, email processing, scheduled tasks |

### Data & Caching
| Technology | Purpose |
|---|---|
| SQL Server | Primary relational database |
| Redis Cloud | Distributed cache |
| Hybrid Cache | Multi-tier caching strategy (in-memory + Redis) |

### Cloud & Integrations
| Service | Purpose |
|---|---|
| Cloudinary | Cloud media storage and delivery |
| PayPal | Online payment processing |

### Security
| Technology | Purpose |
|---|---|
| JWT Authentication | Stateless token-based auth |
| ASP.NET Identity | User management and password hashing |
| Role-Based Authorization | Access control per user role |
| Rate Limiting | API throttling and abuse prevention |

---

## ⚡ Performance & Scalability

- **CQRS** separates read and write operations for optimized query performance
- **Hybrid Caching** (in-memory + Redis) reduces database load
- **Cache Invalidation** keeps data consistent across cache layers
- **Redis Cloud** provides distributed, scalable caching
- **Database Indexing** on frequently queried columns
- **Pagination** on all list endpoints
- **Async/Await** throughout the codebase for non-blocking I/O
- **Rate Limiting** protects against traffic spikes and abuse

---

## 👥 User Roles

| Role | Responsibilities |
|---|---|
| 🏛️ **Student Affairs** | Academic administration, scheduling, grading control, services |
| 👨‍🏫 **Staff** | Academic advising, grade entry, student communication |
| 👨‍🎓 **Student** | Academic access, result viewing, service requests |

---

## 🔒 Security

- JWT-based stateless authentication
- Role-based endpoint authorization
- Secure payment processing via PayPal
- Rate limiting on all public endpoints
- Cloud-secure media storage via Cloudinary
- Background job isolation for sensitive operations (OTP, email)

---

## 📖 API Documentation

Interactive API documentation is available via **Scalar API Reference** after running the application:

```
https://localhost:{port}/scalar
```

Scalar provides a modern, interactive API documentation experience with built-in endpoint exploration, authentication support, and OpenAPI integration.

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Redis](https://redis.io/) (or a Redis Cloud instance)

### Installation

```bash
# 1. Clone the repository
git clone https://github.com/Abdelkarim-Arafat/Universe.git
cd Universe
```

```bash
# 2. Configure settings
# Update appsettings.json with your:
# - SQL Server connection string
# - Redis connection string
# - Cloudinary credentials
# - PayPal credentials
# - JWT secret key
```

```bash
# 3. Apply database migrations
dotnet ef database update
```

```bash
# 4. Run the application
dotnet run
```

```bash
# 5. Open Scalar
# Navigate to https://localhost:{port}/scalar
```

---

## 🎯 Project Goals

- ✅ Digital transformation of university operations
- ✅ Unified platform for academic workflow management
- ✅ Real-time communication between all user types
- ✅ Cloud-native media and payment integrations
- ✅ Enterprise-grade architecture with maintainability in mind
- ✅ Demonstration of modern backend engineering practices

---

## 👨‍💻 About

**Universe** was developed as a **Graduation Project** to demonstrate the practical application of:

- Enterprise software architecture (Clean Architecture)
- CQRS and the Mediator pattern
- Distributed caching strategies
- Real-time communication with SignalR
- Cloud service integrations
- Secure API design

---

## 📄 License

This project is intended for **educational and academic purposes**.

---

<div align="center">
  <sub>Built with ❤️ using ASP.NET Core, Clean Architecture & CQRS</sub>
</div>
