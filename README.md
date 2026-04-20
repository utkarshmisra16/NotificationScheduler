# 🚀 Notification Scheduler

A **production-style, scalable .NET backend system** for scheduling and processing notifications using a clean layered architecture and background job processing.

---

## 📊 CI/CD Status

[![CI](https://github.com/<USERNAME>/NotificationScheduler/actions/workflows/dotnet-ci.yml/badge.svg)](https://github.com/<USERNAME>/NotificationScheduler/actions/workflows/dotnet-ci.yml)

---

## 🧠 System Overview

Notification Scheduler is a backend system designed to reliably schedule and deliver notifications (initially email-based) at a future time using background processing.

It demonstrates **real-world backend engineering principles** including:

* Clean Architecture (Layered Design)
* Background Job Processing
* Separation of Concerns
* Scalable Data Access using Dapper
* CI automation using GitHub Actions

---

## 🏗️ Architecture (High-Level Design)

```
Client (UI / API Consumer)
        ↓
Controller Layer (Presentation)
        ↓
Service Layer (Business Logic)
        ↓
Repository Layer (Dapper Data Access)
        ↓
Database (SQL Server)

+ Background Processing Layer (Hangfire)
```

---

## 📁 Project Structure

```
Controllers/              → API / MVC endpoints
Services/                 → Business logic layer
  Interfaces/
  Implementations/
Repositories/             → Data access (Dapper)
  Interfaces/
  Implementations/
Models/                   → Database entities
DTOs/                     → Request/Response contracts
Helpers/                  → Utilities (DB, Email, etc.)
.github/workflows/       → CI/CD pipelines
```

---

## ⚙️ Tech Stack

* ASP.NET Core MVC / Web API
* C# (.NET 8+)
* Dapper (Lightweight ORM)
* SQL Server
* Hangfire (Background Job Scheduler)
* GitHub Actions (CI Pipeline)

---

## 🚀 Core Features

### 📅 Scheduling Engine

* Schedule notifications for future execution
* Persistent job tracking via database

### 📧 Notification System

* Email-based notification delivery (extensible)
* Abstracted sender service for future providers

### 🔁 Background Processing

* Reliable job execution using Hangfire
* Automatic retry support (configurable)

### 🧠 Clean Architecture

* Strict separation of Controller, Service, and Repository layers
* Testable and maintainable design

### 📊 Job Tracking

* Status lifecycle: Pending → Sent → Failed
* Database-backed audit trail

---

## 🔄 CI/CD Pipeline (GitHub Actions)

On every push / pull request to `main`:

1. Restore dependencies
2. Build solution
3. Run unit tests (if available)
4. Validate compilation integrity

This ensures **continuous integration safety** before merging code.

---

## 🧪 How to Run Locally

```bash
git clone https://github.com/<USERNAME>/NotificationScheduler.git
cd NotificationScheduler/EmailSchedulerApp

dotnet restore
dotnet build
dotnet run
```

---

## 🗄️ Database Design (Core Table)

**ScheduledEmails**

* Id (PK)
* ToEmail
* Subject
* Body
* ScheduledTime
* Status (Pending / Sent / Failed)
* CreatedAt

---

## 🔐 Design Decisions

### Why Dapper?

* Lightweight and high-performance
* Full SQL control for optimized queries
* Better suited for simple data access layer

### Why Layered Architecture?

* Separation of concerns
* Easier testing and maintenance
* Scalable for future microservices migration

### Why Hangfire?

* Persistent background job processing
* Built-in retries and dashboard
* Production-proven scheduling engine

---

## 📈 Future Enhancements

* 📩 Multi-channel notifications (Email + SMS + WhatsApp)
* 📊 Admin dashboard for job monitoring
* 🔔 Real-time push notifications
* 🧾 Template-based email system
* 🐳 Docker containerization
* ☁️ Azure deployment with CI/CD pipeline
* 📡 Distributed queue system (RabbitMQ / Azure Service Bus)

---

## 🧑‍💻 Author

* GitHub: [https://github.com/](https://github.com/)<USERNAME>

---

## ⭐ Why this project stands out

This project demonstrates:

* System design thinking (scalable backend architecture)
* Real-world background job processing
* Production-grade CI/CD setup
* Clean separation of responsibilities
* Extensibility for enterprise-grade features

---

🔥 Built with a focus on **scalability, maintainability, and production readiness**.
