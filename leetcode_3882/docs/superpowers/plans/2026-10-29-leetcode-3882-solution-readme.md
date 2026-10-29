# LeetCode 3882 Solution and README Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Add a runnable sample harness for both existing LeetCode 3882 algorithms and document the problem and solutions in Traditional Chinese.

**Architecture:** Keep the current dynamic-programming and memoized-DFS methods and their signatures. Run both against the three official examples, a single-cell case, and a ten-bit boundary case from `Main`; document the state definitions, transitions, walkthroughs, and verified commands in the root README.

**Tech Stack:** C# / .NET 10 console app; Markdown.

**Spec:** Approved user plan in the 2026-10-29 conversation.

## Global Constraints

- Preserve the existing bilingual problem statement verbatim.
- Keep both solution method signatures and do not add a test project.
- Use four-space C# indentation and Traditional Chinese for README prose.
- Problem constraints: `1 <= m, n <= 1000`, `m * n <= 1000`, `0 <= grid[i][j] <= 1023`.

## Review Focus

- A one-cell grid must include its value exactly once; cover `[[7]]` with expected result `7`.
- Multiple paths must be compared instead of returning the first path; cover official Example 1 with expected result `6`.
- A zero XOR is the smallest possible result and DFS may stop safely; cover official Example 3 with expected result `0`.
- One-row traversal must not require a downward move; cover official Example 3 (`[[2,7,5]]`).
- A maximum ten-bit value must remain an addressable state; cover `[[0, 1023]]` with expected result `1023` in both DP and DFS.

---

### Task 1: Add method documentation and runnable examples

**Files:**
- Modify: `leetcode_3882/Program.cs`

**Interfaces:**
- Preserve `Main(string[] args)`, `MinCost(int[][] grid)`, and `MinCostDfs(int[][] grid)`.
- `Main` runs each method against the three official examples, `[[7]]`, and `[[0, 1023]]`, reports expected/actual results, and fails if either method returns a mismatch.

- [x] Move the existing bilingual problem statement XML comment to `Program` without changing its text; add method-specific XML summaries and parameter/return documentation to the three methods.
- [x] Add concise comments for the DP reachable-XOR transition, DFS memo state, and zero-result pruning.
- [x] Run the console project and confirm all ten method/case comparisons pass.

### Task 2: Create the Traditional-Chinese README

**Files:**
- Create: `README.md`

**Interfaces:**
- Document the verified `dotnet build` and `dotnet run` commands from the repository root.
- Explain both existing methods and use official Example 1 for each step-by-step walkthrough.

- [x] Describe the problem, official constraints, XOR insight, each method's design and complexity, walkthroughs, and run instructions.
- [x] Ensure all command paths and expected sample results match the implementation.

### Task 3: Verify the delivered changes

**Files:**
- Verify: `leetcode_3882/Program.cs`, `README.md`

- [x] Run `dotnet build leetcode_3882/leetcode_3882.csproj` and require a successful build.
- [x] Run `dotnet run --project leetcode_3882/leetcode_3882.csproj` and require all sample checks to pass.
- [x] Run `git diff --check` and require no whitespace errors.
