# Thread State – Local vs Shared Data, Scheduling, and Priority

## Purpose
This module explores **thread state and execution behavior in C#**, with emphasis on:
- Local (thread-local) vs shared state
- Stack vs heap memory
- OS thread scheduling
- The effect and limitations of thread priority

These concepts form the foundation for understanding correctness and performance in multithreaded and ASP.NET applications.

---

## Thread-Local State (Stack-Based)

Each thread owns its **own private stack**.

As a result:
- Variables declared **inside the thread’s execution method**
- Parameters passed directly to the thread entry point

are stored on the thread’s stack and are therefore **thread-local**.

Properties:
- Isolated per thread
- Not visible to other threads
- No synchronization required
- Safe by default

This is why variables created inside a thread method are inherently safe from race conditions.

---

## Shared State (Heap-Based)

Variables that:
- Are declared outside the thread method
- Are captured from an outer scope
- Live in shared objects

are stored on the **heap** and can be accessed by multiple threads.

Properties:
- Shared between threads
- Requires synchronization
- Subject to race conditions
- Execution-order dependent

Without explicit coordination, reads and writes to shared state are **non-deterministic**.

---

## Stack vs Heap in Multithreading

| Memory Area | Ownership | Visibility | Safety |
|------------|----------|------------|--------|
| Stack | Per-thread | Private | ✅ Safe |
| Heap | Shared | Global | ❌ Unsafe without sync |

This distinction explains:
- Why local variables are safe
- Why shared flags and counters are dangerous
- Why locks and atomic operations are necessary

---

## Execution Model and Scheduling

Threads are scheduled by the **operating system**, not by the CLR.

Key characteristics:
- Preemptive scheduling
- Time-sliced execution
- Threads take turns running
- Behavior resembles a **round-robin–like model**

Important implications:
- Execution order is not guaranteed
- Output interleaving is expected
- Timing-sensitive bugs may appear or disappear

You must never rely on execution order unless you explicitly synchronize.

---

## Thread Priority

Threads can be assigned a **priority hint** (e.g., `Highest`, `Normal`, `Lowest`).

Important facts about thread priority:
- Priority is **only a hint** to the OS scheduler
- It does **not guarantee execution order**
- It does **not prevent context switching**
- Lower-priority threads are not blocked, only de-prioritized

Even with a higher priority:
- Threads may still interleave
- Other threads will still execute
- Behavior remains non-deterministic

Thread priority should be used **sparingly**, mainly for:
- Background vs foreground work
- Latency-sensitive operations (with caution)

It is **not** a synchronization mechanism.

---

## Concurrency Implications

Because:
- Each thread has its own stack
- Heap memory is shared
- Scheduling is preemptive and non-deterministic
- Priority is advisory, not absolute

The following rules apply:
- Local variables are safe
- Shared variables must be synchronized
- Execution order must never be assumed
- Priority cannot replace proper synchronization

---

## Why This Matters

Understanding these principles is required before moving to:
- Race conditions and atomicity
- `lock` vs `Monitor`
- Thread pool and `Task`
- `async` / `await`
- ASP.NET request concurrency
- `SynchronizationContext` and `ConfigureAwait`

Without correct reasoning about memory and scheduling, higher-level abstractions will fail in subtle ways.

---

## Summary

- Each thread has its own stack
- Local variables are thread-local and safe
- Shared variables live on the heap and require synchronization
- Threads are time-sliced by the OS
- Scheduling is non-deterministic
- Thread priority is a hint, not a guarantee
- Correct concurrency depends on memory ownership, not timing
