# LeetCode 442：陣列中所有重複的資料

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/C%23-Console-239120)
![LeetCode](https://img.shields.io/badge/LeetCode-442-FFA116)

這是一個使用 C# 與 .NET 10 實作的教學型主控台專案。專案保留原本的排序與相鄰比較
解法，並加入符合題目線性時間、常數額外空間要求的正負號標記解法。`Main` 內含八組
可重複執行的案例，可同時驗證兩種解法。

- [LeetCode English](https://leetcode.com/problems/find-all-duplicates-in-an-array/)
- [LeetCode 中文](https://leetcode.cn/problems/find-all-duplicates-in-an-array/)

## 題目說明

給定長度為 `n` 的整數陣列 `nums`，其中每個整數都介於 `1` 到 `n`，且每個有出現的
整數只會出現一次或兩次。請回傳所有出現兩次的整數，結果順序不限。

例如：

| 輸入 | 輸出 | 說明 |
| --- | --- | --- |
| `[4, 3, 2, 7, 8, 2, 3, 1]` | `[2, 3]` | `2` 與 `3` 各出現兩次。 |
| `[1, 1, 2]` | `[1]` | 只有 `1` 出現兩次。 |
| `[1]` | `[]` | 沒有任何重複值。 |

題目進一步要求演算法達到 `O(n)` 時間，並且只使用結果集合以外的常數額外空間。

## 限制條件

- `n == nums.Length`
- `1 <= n <= 100000`
- `1 <= nums[i] <= n`
- 每個有出現的元素只會出現一次或兩次。
- 公開方法預期接收符合上述限制的非 `null` 陣列，不另外定義無效輸入行為。
- 回傳結果的順序不限。

> [!IMPORTANT]
> 兩種解法都會原地修改輸入。`FindDuplicates` 會重新排列元素；
> `FindDuplicates2` 會將部分元素改成負數。呼叫端若仍需原始內容，應先建立副本。

## 解題概念與出發點

最直接的作法是對每個元素掃描整個陣列，確認它是否再次出現，但最壞情況需要 `O(n²)`
時間。另一種常見方式是使用 `HashSet<int>` 記錄看過的值，可將時間降為 `O(n)`，卻需要
`O(n)` 額外空間，無法符合題目的常數額外空間要求。

本專案示範兩條不同思路：

1. **先建立順序**：排序後相同值必然相鄰，只要掃描並比較前後元素。
2. **利用輸入值域當索引**：因為每個值都介於 `1` 到 `n`，數值 `value` 可以映射到
   `value - 1`；利用該位置的正負號記錄是否看過此值，不需要額外的查找集合。

令：

- `n` 為輸入陣列長度。
- `k` 為重複值的數量，也就是回傳集合長度。
- 輔助空間不包含最後必須回傳的 `O(k)` 結果集合。

## 解法比較

| 解法 | 核心做法 | 時間複雜度 | 輔助空間 | 結果空間 | 輸入副作用 |
| --- | --- | --- | --- | --- | --- |
| `FindDuplicates` | 原地排序後比較相鄰元素 | `O(n log n)` | `O(log n)` | `O(k)` | 永久改變排列順序 |
| `FindDuplicates2` | 值映射索引，以正負號標記是否看過 | `O(n)` | `O(1)` | `O(k)` | 部分元素變成負數 |

排序法容易理解，但不符合題目要求的 `O(n)` 時間；正負號標記法則同時達到線性時間與
常數額外空間。兩者都選擇直接利用輸入陣列，以避免建立長度為 `n` 的完整副本。

## 解法一：排序與相鄰比較

### 設計說明

`FindDuplicates` 先使用 `Array.Sort(nums)` 原地排序。排序後，所有相同值都會集中在一起，
因此只需從索引 `1` 開始，逐一比較 `nums[i - 1]` 與 `nums[i]`：

1. 兩者不同，表示目前值尚未形成重複，繼續掃描。
2. 兩者相同，表示該值出現兩次，將它加入結果。
3. 題目保證每個值最多出現兩次，因此不需要處理連續三個以上相同值的情況。

排序需要 `O(n log n)` 時間；.NET 的陣列排序在最壞情況使用 `O(log n)` 呼叫堆疊空間。
完成後再線性掃描一次，不會改變整體時間複雜度。此解法的結果自然依數值遞增排列。

### 範例演示

輸入：

```text
[4, 3, 2, 7, 8, 2, 3, 1]
```

原地排序後：

```text
[1, 2, 2, 3, 3, 4, 7, 8]
```

相鄰比較流程：

| 前一個值 | 目前值 | 判斷 | 結果 |
| ---: | ---: | --- | --- |
| 1 | 2 | 不同，略過 | `[]` |
| 2 | 2 | 相同，加入 `2` | `[2]` |
| 2 | 3 | 不同，略過 | `[2]` |
| 3 | 3 | 相同，加入 `3` | `[2, 3]` |
| 3 | 4 | 不同，略過 | `[2, 3]` |
| 4 | 7 | 不同，略過 | `[2, 3]` |
| 7 | 8 | 不同，略過 | `[2, 3]` |

最後回傳 `[2, 3]`，而呼叫端的輸入也已變成排序後的陣列。

## 解法二：正負號標記

### 設計說明

`FindDuplicates2` 利用題目保證的值域 `1 <= nums[i] <= n`。每個數值都能安全映射到陣列
內的一個位置：

```text
mappedIndex = Math.Abs(nums[i]) - 1
```

必須先取絕對值，因為目前讀到的位置可能已被之前的步驟改成負數。取得映射索引後：

1. 若 `nums[mappedIndex]` 為正數，表示第一次看到這個值；將該位置乘上 `-1` 留下標記。
2. 若 `nums[mappedIndex]` 已是負數，表示之前看過同一個值；將
   `mappedIndex + 1` 加入結果。
3. 題目保證每個值最多出現兩次，所以同一值只會加入一次。

每個元素只處理一次，時間為 `O(n)`；所有狀態都儲存在原陣列的正負號中，因此結果集合
之外只需要 `mappedIndex` 等固定數量變數，輔助空間為 `O(1)`。

### 範例演示

以 `[4, 3, 2, 7, 8, 2, 3, 1]` 為例：

| 讀到的值 | 映射索引 | 映射位置狀態 | 動作 | 結果 |
| ---: | ---: | --- | --- | --- |
| 4 | 3 | `7` 為正 | 將索引 3 改成 `-7` | `[]` |
| 3 | 2 | `2` 為正 | 將索引 2 改成 `-2` | `[]` |
| -2 | 1 | `3` 為正 | 取絕對值 2，將索引 1 改成 `-3` | `[]` |
| -7 | 6 | `3` 為正 | 取絕對值 7，將索引 6 改成 `-3` | `[]` |
| 8 | 7 | `1` 為正 | 將索引 7 改成 `-1` | `[]` |
| 2 | 1 | `-3` 為負 | `2` 已看過，加入結果 | `[2]` |
| -3 | 2 | `-2` 為負 | `3` 已看過，加入結果 | `[2, 3]` |
| -1 | 0 | `4` 為正 | 取絕對值 1，將索引 0 改成 `-4` | `[2, 3]` |

掃描結束後回傳 `[2, 3]`。此時輸入已變成：

```text
[-4, -3, -2, -7, 8, 2, -3, -1]
```

這些負號是演算法的狀態標記，不代表原始資料包含負數。

## Acceptance Harness

專案目前沒有 xUnit、NUnit 或 MSTest 專案。`Main` 是可重複執行的 acceptance harness，
每個案例會：

1. 為兩種解法建立各自的輸入副本，避免第一個解法的原地修改影響第二個解法。
2. 分別呼叫 `FindDuplicates` 與 `FindDuplicates2`。
3. 排序實際與預期結果後比較，讓驗證不依賴回傳順序。
4. 只有兩個解法都符合預期時，該案例才顯示 `PASS`。
5. 任一案例失敗時，將 process exit code 設為 1。

| # | 案例 | 驗證重點 |
| ---: | --- | --- |
| 1 | 官方範例一 | 兩個分散的重複值 |
| 2 | 官方範例二 | 單一重複值 |
| 3 | 最小長度 | 長度 1 且沒有重複 |
| 4 | 反向排列且無重複 | 排序前順序不影響判斷 |
| 5 | 所有已出現值都成對 | 多組相鄰重複 |
| 6 | 數值上下界都重複 | 值 1 與值 `n` 的索引映射 |
| 7 | 分散的兩組重複 | 多組重複且第二次出現順序不同 |
| 8 | 長度上限 100000 | 大型輸入與接近數值上界的重複 |

## 建置、測試與執行

需求：

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

從此題目的 repository root 執行：

```bash
dotnet restore leetcode_442/leetcode_442.csproj
dotnet format leetcode_442/leetcode_442.csproj --no-restore --verify-no-changes
dotnet build leetcode_442/leetcode_442.csproj --nologo --no-restore
dotnet run --no-build --project leetcode_442/leetcode_442.csproj
git diff --check
```

目前沒有正式測試專案，因此不另外執行 `dotnet test`；行為驗證由 `Main` 中的八組案例完成。

### 實際執行輸出

以下內容來自完成建置後的 fresh run：

```text
Case: Official example 1
Input: [4, 3, 2, 7, 8, 2, 3, 1]
Expected: [2, 3]
FindDuplicates: [2, 3]
FindDuplicates2: [2, 3]
Result: PASS

Case: Official example 2
Input: [1, 1, 2]
Expected: [1]
FindDuplicates: [1]
FindDuplicates2: [1]
Result: PASS

Case: Minimum length
Input: [1]
Expected: []
FindDuplicates: []
FindDuplicates2: []
Result: PASS

Case: No duplicates in reverse order
Input: [5, 4, 3, 2, 1]
Expected: []
FindDuplicates: []
FindDuplicates2: []
Result: PASS

Case: Every present value is paired
Input: [1, 1, 2, 2]
Expected: [1, 2]
FindDuplicates: [1, 2]
FindDuplicates2: [1, 2]
Result: PASS

Case: Duplicate values at both bounds
Input: [1, 6, 3, 4, 6, 1]
Expected: [1, 6]
FindDuplicates: [1, 6]
FindDuplicates2: [1, 6]
Result: PASS

Case: Separated duplicate pairs
Input: [2, 4, 1, 2, 3, 4]
Expected: [2, 4]
FindDuplicates: [2, 4]
FindDuplicates2: [2, 4]
Result: PASS

Case: Maximum length
Input: [length 100000; values 1..99999 followed by 99999]
Expected: [99999]
FindDuplicates: [99999]
FindDuplicates2: [99999]
Result: PASS

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
.
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_442.sln
└── leetcode_442/
    ├── Program.cs
    └── leetcode_442.csproj
```

- `Program.cs`：包含兩種尋找重複值的演算法與八組可執行案例。
- `leetcode_442.csproj`：目標框架為 `net10.0` 的主控台專案。
- `docs/readme-template.md`：建立本 README 時遵循的文件指引。
