# README Project Audit Index Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Replace the root README project index with the complete audit fields and create one GitHub Issue that tracks every project-root file gap.

**Architecture:** Parse the 612-row `.csproj` Markdown table in `PROJECT_DOTNET_VERSION_AUDIT.md` once, validate its schema and aggregate counts, then select the 608 main-project rows to derive both the one-row-per-problem README table and GitHub Issue body. Use a temporary Node.js generator so repository changes remain limited to documentation, then independently validate the generated artifacts before creating the external Issue.

**Tech Stack:** Markdown, Node.js standard library, Git, GitHub CLI.

## Global Constraints

- `PROJECT_DOTNET_VERSION_AUDIT.md` is the only source of project metadata.
- Do not modify any `leetcode_*` project, `.csproj`, or missing project-root file.
- Do not modify `PROJECT_DOTNET_VERSION_AUDIT.md`.
- The README index columns must be `題號`, `資料夾`, `專案檔`, `TargetFramework`, `SDK-style`, `.editorconfig`, `.gitignore`, `README.md`.
- Existing files use relative Markdown links; missing files display `**缺少**`.
- The GitHub Issue contains one checkbox per affected project and lists every missing file for that project.
- Expected audit invariants are 612 `.csproj` files: 608 main projects and 4 test projects; total framework counts are `net8.0` 295, `net9.0` 6, `net10.0` 311; README main-project framework counts are `net8.0` 294, `net9.0` 6, `net10.0` 308; `.editorconfig` missing 191, `.gitignore` missing 175, `README.md` missing 164; 196 project folders are missing at least one file; all projects SDK-style.

---

### Task 1: Generate and validate the README audit index

**Files:**
- Create: `/private/tmp/update_leetcode_audit_index.mjs`
- Modify: `README.md:85`
- Create: `/private/tmp/leetcode-project-file-gaps-issue.md`

**Interfaces:**
- Consumes: the Markdown table between `## 全部專案明細` and `## 缺少 \`.editorconfig\`` in `PROJECT_DOTNET_VERSION_AUDIT.md`.
- Produces: the root README section beginning at `## 完整題目索引` and an Issue body with one checkbox per affected project.

- [ ] **Step 1: Verify the current README does not yet satisfy the new schema**

Run:

```bash
node -e 'const fs=require("fs");const s=fs.readFileSync("README.md","utf8");const h=s.match(/^\\| 題號 .*$/m)?.[0]??"";if(h.includes("SDK-style")&&h.includes(".editorconfig")&&h.includes(".gitignore"))process.exit(1);console.log("RED: README audit columns are missing");'
```

Expected: prints `RED: README audit columns are missing` with exit code 0.

- [ ] **Step 2: Create the temporary generator**

Create `/private/tmp/update_leetcode_audit_index.mjs` with logic that:

1. Reads `PROJECT_DOTNET_VERSION_AUDIT.md`, extracts all 612 main-table rows, and selects the 608 `主專案` rows for README and Issue generation.
2. Splits each row into the eight audit columns and strips only Markdown emphasis/backticks.
3. Rejects rows whose type is not `主專案`, whose framework is empty, whose SDK-style value is not `是`, or whose file status is not `有`/`缺少`.
4. Asserts the global invariants listed above.
5. Generates the README header, explanation, eight-column table, and relative links.
6. Replaces `README.md` from `## 完整題目索引` through end of file.
7. Generates `/private/tmp/leetcode-project-file-gaps-issue.md` with:
   - a link to `PROJECT_DOTNET_VERSION_AUDIT.md`;
   - the three expected missing counts;
   - one unchecked item per project with at least one missing file;
   - completion conditions from the approved design.

- [ ] **Step 3: Run the generator**

Run:

```bash
node /private/tmp/update_leetcode_audit_index.mjs
```

Expected: reports 608 README rows and 196 affected projects, and writes only `README.md` plus the temporary Issue body.

- [ ] **Step 4: Validate README structure independently**

Run a read-only Node.js validator that parses the generated README table and asserts:

```text
row count = 608
column count per row = 8
net8.0 = 294
net9.0 = 6
net10.0 = 308
SDK-style 是 = 608
.editorconfig 缺少 = 191
.gitignore 缺少 = 175
README.md 缺少 = 164
```

Expected: exit code 0 and a summary containing all eight confirmed values.

- [ ] **Step 5: Validate links and working-tree scope**

Run:

```bash
git diff --check
git status --short
git diff -- README.md
```

Then use a read-only script to resolve every non-missing README table link against the repository root and assert that each target exists.

Expected: no whitespace errors, no unrelated changes, and all generated links resolve.

### Task 2: Preview, create, and read back the GitHub Issue

**Files:**
- Read: `/private/tmp/leetcode-project-file-gaps-issue.md`
- External create: one Issue in `HyperLee/Leetcode_folder`

**Interfaces:**
- Consumes: the validated Issue body generated by Task 1.
- Produces: one GitHub Issue titled `補齊題目專案的 .editorconfig、.gitignore 與 README.md`.

- [ ] **Step 1: Validate the Issue body independently**

Run a read-only validator that asserts:

```text
checkbox count = 196
each affected project appears exactly once
the per-file checkbox annotations aggregate to 191, 175, and 164
no project with all three files present appears
```

Expected: exit code 0 with all counts printed, including 196 unique affected projects.

- [ ] **Step 2: Check for an existing open duplicate**

Run:

```bash
gh issue list --repo HyperLee/Leetcode_folder --state open --limit 100 --json number,title,url
```

Expected: no open Issue with the exact planned title or equivalent scope. If one exists, stop and update nothing externally.

- [ ] **Step 3: Show the exact write target and create the Issue**

Restate repository `HyperLee/Leetcode_folder`, the exact title, all three counts, and the 196-project checklist size before the write.

Run:

```bash
gh issue create \
  --repo HyperLee/Leetcode_folder \
  --title "補齊題目專案的 .editorconfig、.gitignore 與 README.md" \
  --body-file /private/tmp/leetcode-project-file-gaps-issue.md
```

Expected: one new Issue URL.

- [ ] **Step 4: Read back and verify the Issue**

Run:

```bash
gh issue view ISSUE_NUMBER \
  --repo HyperLee/Leetcode_folder \
  --json number,title,state,body,url
```

Assert the title, open state, three aggregate counts, 196 checkboxes, and per-project annotations match the local preview.

Expected: all fields and counts match.

### Task 3: Final repository verification and documentation commit

**Files:**
- Modify: `README.md`
- Modify: `docs/superpowers/specs/2026-07-27-readme-project-audit-index-design.md`
- Add: `docs/superpowers/plans/2026-07-27-readme-project-audit-index.md`

**Interfaces:**
- Consumes: validated README and verified GitHub Issue URL.
- Produces: one local commit containing the corrected design counts, implementation plan, and README update.

- [ ] **Step 1: Run final verification**

Run:

```bash
git diff --check
git status --short
git diff --stat
```

Repeat the independent README validator from Task 1 after the Issue readback.

Expected: only the corrected design, plan, and README are uncommitted; every invariant still passes.

- [ ] **Step 2: Commit the repository changes**

Run:

```bash
git add README.md \
  docs/superpowers/specs/2026-07-27-readme-project-audit-index-design.md \
  docs/superpowers/plans/2026-07-27-readme-project-audit-index.md
git commit -m "Expand README project audit index"
```

Expected: one commit containing only the approved README implementation, corrected design counts, and its execution plan.

- [ ] **Step 3: Confirm clean completion state**

Run:

```bash
git status --short
git show --stat --oneline HEAD
```

Expected: clean working tree and a commit summary listing only the three intended files.
