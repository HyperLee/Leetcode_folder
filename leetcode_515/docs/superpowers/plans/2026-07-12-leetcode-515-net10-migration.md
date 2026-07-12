# LeetCode 515 .NET 10 Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use `superpowers:subagent-driven-development` or `superpowers:executing-plans` to implement this plan task-by-task.

**Goal:** 將 `leetcode_515/` 依 `LEETCODE_NET10_MIGRATION_SPEC.md` 升級至 .NET 10，保留 DFS 並完成發布。

**Architecture:** 使用 problem-root workspace 與巢狀 SDK-style `net10.0` console project。`LargestValues` 與 DFS helper 保持純資料處理，所有輸出集中在 `Main`。

## Execution checklist

- [ ] Convert the legacy project and remove `.sln`, `App.config`, and `AssemblyInfo.cs`.
- [ ] Implement the nullable-safe DFS API and ten deterministic acceptance checks.
- [ ] Add shared settings, VS Code configuration, README, AGENTS.md, and Superpowers design artifacts.
- [ ] Run the complete local gate and independent read-only review.
- [ ] Create the single migration commit, publish the PR, merge it, update Issue #2, and verify `main`.
