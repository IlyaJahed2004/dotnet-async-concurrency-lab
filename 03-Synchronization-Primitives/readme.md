# Synchronization Primitives – Mutual Exclusion and Thread Blocking

## Overview

This module demonstrates a fundamental concurrency problem: **multiple threads accessing a shared file simultaneously**.

When more than one thread performs file I/O on the same path without synchronization, the program becomes vulnerable to race conditions and runtime failures.

This example focuses on understanding **why synchronization is required** and **how thread blocking works internally** when using mutual exclusion.

---

## The Core Problem

Two threads attempt to write to the same file concurrently:

- The main thread
- A worker thread

Both threads execute the same file-writing logic. Since the file is a **shared resource**, concurrent access without coordination is unsafe.

File operations such as opening, writing, and closing are **not atomic**. This means that thread interleaving can occur at any point during these operations.

---

## Race Condition Scenario

A possible execution timeline:

1. Thread A opens the file for writing
2. Before the write operation completes, its CPU time slice expires
3. The scheduler switches execution to Thread B
4. Thread B attempts to open the same file
5. The file is already in use
6. An I/O exception may be thrown, or behavior becomes undefined

This situation is a classic **race condition on a shared I/O resource**.

---

## Important Note on Scheduling

The root cause is **not time slicing itself**.

Even on:
- A single-core CPU
- Non-parallel execution

The problem still exists because:
- There is no mutual exclusion
- The shared resource is accessed concurrently
- The operations are not protected as a single critical section

Concurrency alone is sufficient to trigger the issue.

---

## Mutual Exclusion as the Correct Solution

To prevent concurrent access, the file-writing logic must be protected by a synchronization mechanism that enforces **exclusive access**.

In .NET, this is achieved using `lock`, which is implemented internally via `Monitor`.

Mutual exclusion ensures that:
- Only one thread can enter the critical section at a time
- All other threads are prevented from accessing the shared resource until it is safe

---

## What Happens When a Thread Cannot Acquire the Lock

Consider this sequence:

1. Thread A acquires the lock and enters the critical section
2. Thread A opens the file
3. Thread A’s time slice expires
4. The scheduler switches to Thread B
5. Thread B reaches the same lock

At this point:

- Thread B **cannot enter** the critical section
- Thread B is transitioned to a **blocked state**
- Thread B is removed from the runnable queue
- Thread B **does not consume CPU time**
- Thread B does **not spin or busy-wait**

The scheduler will not allocate CPU time to Thread B until the lock becomes available.

---

## Lock Release and Resumption

When Thread A finishes its file operation and exits the critical section:

- The lock is released
- One of the blocked threads is unblocked
- Thread B becomes runnable again
- Thread B acquires the lock and proceeds safely

This guarantees serialized and deterministic access to the shared file.

---

## Key Properties of `lock` / `Monitor`

- Provides mutual exclusion
- Prevents race conditions on shared resources
- Blocks threads instead of wasting CPU cycles
- Relies on OS-level synchronization primitives
- Ensures memory visibility and ordering guarantees

---

## Key Takeaways

- File access from multiple threads must be synchronized
- The issue is a race condition, not a scheduling bug
- Threads waiting for a lock are **blocked**, not spinning
- No CPU time is wasted while waiting
- Mutual exclusion guarantees correctness and safety

---

## Summary

> A thread that cannot acquire a lock does not run — it blocks.

This behavior is essential for writing correct and safe concurrent programs.
