# dotnet-async-concurrency-lab

## Overview
This repository is a focused laboratory for exploring **asynchronous programming, concurrency, and synchronization primitives in .NET**.

The goal is to build a **deep, first-principles understanding** of how async/await, Tasks, threading, and synchronization mechanisms behave at runtime, independent of any framework.  
This understanding directly supports writing **correct, scalable, and deadlock-free ASP.NET Core APIs**.

This is not a tutorial repository.  
All examples are **scenario-driven** and intentionally designed to expose real-world pitfalls and edge cases.

---

## Motivation
Many issues in modern .NET backends are caused not by incorrect framework usage, but by a shallow understanding of concurrency fundamentals.

This repository exists to answer questions such as:
- Why does blocking on async code lead to deadlocks?
- What actually happens when `await` yields execution?
- How does the ThreadPool schedule work under load?
- When are synchronization primitives required, and when are they harmful?
- Why does `SynchronizationContext` still matter?

Understanding these concepts in isolation leads to safer and more predictable systems.

---

## Repository Structure
Each top-level directory represents an **independent console application** focused on a specific conceptual area.
```text
dotnet-async-concurrency-lab
│
├── 01-Process-vs-Thread
├── 02-Thread-State
├── 03-Synchronization-Primitives
├── 04-Task-and-ThreadPool
├── 05-Async-vs-Parallel
├── 06-Cancellation
├── 07-Task-Advanced
├── 08-SynchronizationContext
│
└── README.md
