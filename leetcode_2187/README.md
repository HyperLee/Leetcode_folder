# LeetCode 2187 — Minimum Time to Complete Trips

> 完成旅途的最少時間｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/minimum-time-to-complete-trips/)
- [中文題目](https://leetcode.cn/problems/minimum-time-to-complete-trips/)

## 題目說明

給定整數陣列 `time`，其中 `time[i]` 是第 `i` 輛巴士完成一趟旅途所需時間；所有巴士可平行運行。
請回傳讓它們合計至少完成 `totalTrips` 趟旅途的最少整數時間。

題目限制：

- `1 <= time.length <= 100,000`
- `1 <= time[i], totalTrips <= 10,000,000`

## 單調可行性與搜尋邊界

對一個候選總時間 `t`，每輛巴士可完成 `t / time[i]` 趟。令

`completed(t) = Σ floor(t / time[i])`

若 `completed(t) >= totalTrips`，則 `t` 可行。時間增加不會讓任何巴士完成的趟數減少，因此
「可行」是單調 predicate：從某個時間開始會永遠可行，適合以二分搜尋找第一個可行時間。

左界是 `1`；右界使用 `(long)time.Max() * totalTrips`。即使只有最慢巴士運行，在此時間也剛好能
完成 `totalTrips` 趟，因此右界必定可行。乘法先轉為 `long`，避免 `int` 溢位。

## 解法一：`left < right` 的第一個可行值

公開 API：

```csharp
public static long MinimumTime(int[] time, int totalTrips)
```
維持答案位於閉區間 `[left, right]`：

- 若 `middle` 可行，最小答案可能正是 `middle`，令 `right = middle`。
- 若 `middle` 不可行，所有不大於它的時間都不可行，令 `left = middle + 1`。
- 當 `left == right` 時，兩者就是第一個可行時間。

方法只讀取輸入陣列，不輸出主控台。時間複雜度為
`O(n log(max(time) * totalTrips))`，輔助空間為 `O(1)`，結果空間為 `O(1)`。

## 解法二：`left <= right` 與候選答案

公開 API：

```csharp
public static long MinimumTime2(int[] time, int totalTrips)
```

此模板以最初可行的右界初始化 `candidate`：

- 若 `middle` 可行，先記為候選，再令 `right = middle - 1` 尋找更小可行值。
- 若 `middle` 不可行，令 `left = middle + 1`。
- 迴圈結束時，`candidate` 是最後保留的最小可行時間。

它同樣只讀取輸入，時間複雜度為 `O(n log(max(time) * totalTrips))`，輔助空間為 `O(1)`，
結果空間為 `O(1)`。
兩個公開方法共用同一個可行性 helper，避免兩份計數邏輯漂移。

## 大量累計與防禦性早停

可行性 helper 會在累計趟數達到 `totalTrips` 時立刻回傳 `true`，不再掃描其餘巴士。這能避免
可行候選時間在已達標後仍做不必要的大量累加，作為處理大計數的防禦性早停。第 8 個 harness case 使用
99,999 輛每趟 1 的巴士加上一輛每趟 10,000,000 的巴士，並要求 10,000,000 趟；答案 `101`。
它保留了驅動早停設計的大量累計輸入形狀，並驗證這個邊界的正確答案；是否提早返回則由 helper 的
明確控制流程保證，而非由此案例單獨證明溢位行為。

以 `time=[2, 3]`、`totalTrips=5` 為例：

```plaintext
t = 5：floor(5 / 2) + floor(5 / 3) = 2 + 1 = 3，不可行
t = 6：floor(6 / 2) + floor(6 / 3) = 3 + 2 = 5，可行
```

所以第一個可行時間是 `6`。

## Acceptance Harness

`Main` 執行 9 個確定性案例。每案建立兩份獨立輸入副本，分別檢查兩個 API 的文字結果與輸入陣列保存，
共 36 個檢查。任一失敗都會把 process exit code 設為 `1`；大型陣列只輸出摘要，避免產生冗長輸出。

| # | 輸入摘要 | 預期 | 驗證目的 |
| ---: | --- | ---: | --- |
| 1 | `[1,2,3]`，5 趟 | 3 | 官方範例 |
| 2 | `[2]`，1 趟 | 2 | 單一巴士 |
| 3 | `[1]`，1 趟 | 1 | 最小有效輸入 |
| 4 | `[5,10,10]`，9 趟 | 25 | 最慢單趟建立的右界 |
| 5 | `[5,1,3]`，5 趟 | 4 | 未排序時間 |
| 6 | `[2,3]`，5 趟 | 6 | 合併容量剛好達標 |
| 7 | `[10,000,000]`，10,000,000 趟 | 100,000,000,000,000 | 最大答案 |
| 8 | 99,999 個 1 與一個 10,000,000，10,000,000 趟 | 101 | 大量累計的防禦性早停輸入 |
| 9 | 100,000 個 10,000,000，10,000,000 趟 | 1,000,000,000 | 最大巴士數 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_2187/leetcode_2187/leetcode_2187.csproj --nologo
dotnet run --no-build --project leetcode_2187/leetcode_2187/leetcode_2187.csproj
```

若直接開啟題目根目錄 `leetcode_2187/`，使用：

```bash
dotnet build leetcode_2187/leetcode_2187.csproj --nologo
dotnet run --no-build --project leetcode_2187/leetcode_2187.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: 1 - Official example
Input: time=[1, 2, 3], totalTrips=5
PASS MinimumTime result | Expected: 3 | Actual: 3
PASS MinimumTime input preserved | Expected: True | Actual: True
PASS MinimumTime2 result | Expected: 3 | Actual: 3
PASS MinimumTime2 input preserved | Expected: True | Actual: True

Case: 2 - Single bus
Input: time=[2], totalTrips=1
PASS MinimumTime result | Expected: 2 | Actual: 2
PASS MinimumTime input preserved | Expected: True | Actual: True
PASS MinimumTime2 result | Expected: 2 | Actual: 2
PASS MinimumTime2 input preserved | Expected: True | Actual: True

Case: 3 - Minimum valid input
Input: time=[1], totalTrips=1
PASS MinimumTime result | Expected: 1 | Actual: 1
PASS MinimumTime input preserved | Expected: True | Actual: True
PASS MinimumTime2 result | Expected: 1 | Actual: 1
PASS MinimumTime2 input preserved | Expected: True | Actual: True

Case: 4 - Shared slowest upper bound
Input: time=[5, 10, 10], totalTrips=9
PASS MinimumTime result | Expected: 25 | Actual: 25
PASS MinimumTime input preserved | Expected: True | Actual: True
PASS MinimumTime2 result | Expected: 25 | Actual: 25
PASS MinimumTime2 input preserved | Expected: True | Actual: True

Case: 5 - Unsorted durations
Input: time=[5, 1, 3], totalTrips=5
PASS MinimumTime result | Expected: 4 | Actual: 4
PASS MinimumTime input preserved | Expected: True | Actual: True
PASS MinimumTime2 result | Expected: 4 | Actual: 4
PASS MinimumTime2 input preserved | Expected: True | Actual: True

Case: 6 - Exact combined capacity
Input: time=[2, 3], totalTrips=5
PASS MinimumTime result | Expected: 6 | Actual: 6
PASS MinimumTime input preserved | Expected: True | Actual: True
PASS MinimumTime2 result | Expected: 6 | Actual: 6
PASS MinimumTime2 input preserved | Expected: True | Actual: True

Case: 7 - Maximum answer
Input: time=[10_000_000], totalTrips=10_000_000
PASS MinimumTime result | Expected: 100000000000000 | Actual: 100000000000000
PASS MinimumTime input preserved | Expected: True | Actual: True
PASS MinimumTime2 result | Expected: 100000000000000 | Actual: 100000000000000
PASS MinimumTime2 input preserved | Expected: True | Actual: True

Case: 8 - Large-accumulation early-stop guard
Input: time=[1 x 99,999, 10,000,000], totalTrips=10,000,000
PASS MinimumTime result | Expected: 101 | Actual: 101
PASS MinimumTime input preserved | Expected: True | Actual: True
PASS MinimumTime2 result | Expected: 101 | Actual: 101
PASS MinimumTime2 input preserved | Expected: True | Actual: True

Case: 9 - Maximum bus count
Input: time=[10,000,000 x 100,000], totalTrips=10,000,000
PASS MinimumTime result | Expected: 1000000000 | Actual: 1000000000
PASS MinimumTime input preserved | Expected: True | Actual: True
PASS MinimumTime2 result | Expected: 1000000000 | Actual: 1000000000
PASS MinimumTime2 input preserved | Expected: True | Actual: True

Summary: 36/36 checks passed.
```

## 專案結構

```plaintext
leetcode_2187/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_2187/
    ├── Program.cs
    └── leetcode_2187.csproj
```
