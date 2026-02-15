# Process vs Thread – Concurrency Without Blocking

## Purpose
This console application demonstrates **basic concurrency in C#** using multiple threads inside a single process.  
The goal is to build an intuitive understanding of how threads behave when no explicit synchronization (such as `Thread.Join`) is applied—a foundational concept for later understanding `async/await` and ASP.NET request handling.

---

## Conceptual Model

- The program runs inside **one process**.
- Two **threads** exist within that process:
  - The **main thread**
  - A **secondary (worker) thread**
- Both threads perform **CPU-bound work** (console output).
- No I/O-bound operations are involved.

Because both threads are active at the same time, the program exhibits **concurrent execution**.

---

## Execution Behavior (Without `Thread.Join`)

In this implementation, the main thread **does not wait** for the worker thread to complete.

Key characteristics:

- The main thread is **not blocked**
- Both threads are eligible to run simultaneously
- The operating system scheduler alternates execution between threads
- Execution is typically **time-sliced**, similar to a round-robin strategy

As a result:
- Output from both threads is **interleaved**
- The exact order of execution is **non-deterministic**
- Both threads make progress independently

This is an example of **concurrent CPU-bound execution**.

---

## Blocking vs Non-Blocking

### Current Behavior
- No blocking is introduced
- The main thread continues execution immediately after starting the worker thread
- The program is **non-blocking at the application level**

### If `Thread.Join` Were Used
If the main thread explicitly waited for the worker thread:

- The main thread would **block**
- Execution would become **synchronous**
- The worker thread would fully complete before the main thread continues
- Concurrency would effectively be lost from the program’s perspective

This distinction is critical when reasoning about performance and responsiveness.

---

## Concurrency vs Async (Important Distinction)

Although the program is concurrent, it is **not asynchronous in the `async/await` sense**.

- No `async` keyword is used
- No `await` is involved
- No non-blocking I/O is present

Concurrency here is achieved purely through **multiple threads**, not through asynchronous programming.

| Aspect | Present |
|------|--------|
| Single process | ✅ |
| Multiple threads | ✅ |
| CPU-bound work | ✅ |
| I/O-bound work | ❌ |
| Blocking | ❌ |
| async/await | ❌ |
| Concurrency | ✅ |

---

## Why This Example Matters

Understanding this example is essential before moving on to:
- `Task` vs `Thread`
- ThreadPool behavior
- `async` / `await`
- ASP.NET request concurrency
- Deadlocks caused by blocking calls

Many real-world ASP.NET issues stem from misunderstanding **blocking vs concurrency** at this fundamental level.

---

## Summary

- A single process can run multiple threads concurrently
- Not calling `Thread.Join` prevents the main thread from blocking
- CPU-bound concurrency is fundamentally different from async I/O
- This example builds the mental model required for correct async API design in ASP.NET
