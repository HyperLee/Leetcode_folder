# LeetCode 1887 — Reduction Operations to Make the Array Elements Equal

> 使陣列元素相等的減少操作次數｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/reduction-operations-to-make-the-array-elements-equal/)
- [中文題目](https://leetcode.cn/problems/reduction-operations-to-make-the-array-elements-equal/)

## 題目說明

給定整數陣列 `nums`。每次操作選取目前最大值中索引最小的元素，將它降低為嚴格小於
最大值的次大相異值。回傳讓所有元素相等所需的操作次數。

題目限制：

- `1 <= nums.length <= 50,000`
- `1 <= nums[i] <= 50,000`

## 解法：複製、排序與相異值層數

公開 API：

```csharp
public static int ReductionOperations(int[] nums)
```

方法先複製輸入再排序，因此不會改動呼叫者的陣列。由小到大掃描排序結果時，每跨入一個
新的相異值，就代表後續元素比最小值多一層需要降低。將目前層數加到每個元素的操作數，
即可得到總答案。

核心不變量是：

> 掃描到某個元素時，`distinctLevelCount` 等於它與最小值之間的相異值層數，也就是該
> 元素最終需要執行的降低次數。

操作次數取決於相異值層數，而不是數值差距。例如 `[1, 100, 100]` 只有兩層值，每個
`100` 都只需降低一次，所以答案是 `2`，不是 `198`。

### 逐步範例

以 `[5, 1, 3]` 為例，複製後排序成 `[1, 3, 5]`：

| 目前值 | 是否進入新層 | 層數 | 累計操作數 |
| ---: | --- | ---: | ---: |
| `1` | 起點 | 0 | 0 |
| `3` | 是 | 1 | 1 |
| `5` | 是 | 2 | 3 |

因此總共需要 `3` 次操作。

### 複雜度

| 項目 | 複雜度 | 說明 |
| --- | --- | --- |
| 時間 | `O(n log n)` | 複製後排序，接著線性掃描 |
| 結果空間 | `O(1)` | 只回傳一個整數 |
| 輔助空間 | `O(n)` | 使用輸入副本以保持公開方法純度 |

## Acceptance Harness

`Main` 執行九個確定性案例。每案同時驗證答案與原始輸入未被修改；任何失敗都會把
process exit code 設為 `1`。

| # | 輸入 | 預期 | 驗證目的 |
| ---: | --- | ---: | --- |
| 1 | `[5, 1, 3]` | 3 | 官方範例 1 |
| 2 | `[1, 1, 1]` | 0 | 官方範例 2；所有值已相等 |
| 3 | `[1, 1, 2, 2, 3]` | 4 | 官方範例 3；多個重複值群組 |
| 4 | `[1]` | 0 | 最小有效輸入 |
| 5 | `[2, 1]` | 1 | 兩元素、單一降低操作 |
| 6 | `[1, 100, 100]` | 2 | 數值差距不影響層數 |
| 7 | `[4, 1, 2, 2, 4]` | 6 | 未排序且有多個重複群組 |
| 8 | 50,000 個 `7` | 0 | 最大長度、全部相等 |
| 9 | `50,000..1` | 1,249,975,000 | 最大長度、最大相異層數與 `int` 上界 spot check |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_1887/leetcode_1887/leetcode_1887.csproj --nologo
dotnet run --no-build --project leetcode_1887/leetcode_1887/leetcode_1887.csproj
```

若直接開啟題目根目錄 `leetcode_1887/`，使用：

```bash
dotnet build leetcode_1887/leetcode_1887.csproj --nologo
dotnet run --no-build --project leetcode_1887/leetcode_1887.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: 1 - Official example 1
Input: [5, 1, 3]
Expected: 3
Actual: 3
Input preserved: True
Result: PASS

Case: 2 - Official example 2
Input: [1, 1, 1]
Expected: 0
Actual: 0
Input preserved: True
Result: PASS

Case: 3 - Official example 3
Input: [1, 1, 2, 2, 3]
Expected: 4
Actual: 4
Input preserved: True
Result: PASS

Case: 4 - Minimum input
Input: [1]
Expected: 0
Actual: 0
Input preserved: True
Result: PASS

Case: 5 - Two elements
Input: [2, 1]
Expected: 1
Actual: 1
Input preserved: True
Result: PASS

Case: 6 - Value gap does not change level count
Input: [1, 100, 100]
Expected: 2
Actual: 2
Input preserved: True
Result: PASS

Case: 7 - Unsorted duplicate groups
Input: [4, 1, 2, 2, 4]
Expected: 6
Actual: 6
Input preserved: True
Result: PASS

Case: 8 - Maximum length all equal
Input: [7 x 50000]
Expected: 0
Actual: 0
Input preserved: True
Result: PASS

Case: 9 - Maximum length all distinct
Input: [50000..1]
Expected: 1249975000
Actual: 1249975000
Input preserved: True
Result: PASS

Summary: 9/9 checks passed.
```

## 專案結構

```plaintext
leetcode_1887/
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
└── leetcode_1887/
    ├── Program.cs
    └── leetcode_1887.csproj
```
