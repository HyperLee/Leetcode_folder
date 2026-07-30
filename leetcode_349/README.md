# LeetCode 349：兩個陣列的交集

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/C%23-Console-239120)
![LeetCode](https://img.shields.io/badge/LeetCode-349-FFA116)

這是一個使用 C# 與 .NET 10 實作的教學型主控台專案。專案保留原本的
Dictionary + HashSet 解法，並加入 `HashSet.IntersectWith` 解法，從手動查找與去重，
逐步走向直接使用集合交集運算。

- [LeetCode English](https://leetcode.com/problems/intersection-of-two-arrays/)
- [LeetCode 中文](https://leetcode.cn/problems/intersection-of-two-arrays/)

## 題目說明

給定兩個整數陣列 `nums1` 與 `nums2`，回傳它們的交集。

輸出必須符合兩項條件：

1. 每個共同元素只能出現一次。
2. 回傳元素的順序不限。

例如：

| 輸入 | 輸出 | 說明 |
| --- | --- | --- |
| `nums1 = [1, 2, 2, 1]`、`nums2 = [2, 2]` | `[2]` | 雖然 `2` 重複出現，交集只保留一份。 |
| `nums1 = [4, 9, 5]`、`nums2 = [9, 4, 9, 8, 4]` | `[4, 9]` | `[9, 4]` 也同樣正確，因為題目不限制順序。 |

## 限制條件

- `1 <= nums1.Length, nums2.Length <= 1000`
- `0 <= nums1[i], nums2[i] <= 1000`
- 公開方法預期接收符合題目限制的非 `null` 陣列，不另外定義無效輸入行為。
- `Intersection` 與 `Intersection2` 都不修改輸入陣列，也不直接輸出主控台內容。
- 回傳陣列中的元素皆唯一，但原始列舉順序不保證。

## 解題概念與出發點

最直接的暴力做法，是讓 `nums1` 的每個元素都逐一和 `nums2` 的每個元素比較。若兩個陣列
長度分別為 `n` 與 `m`，最壞情況需要 `n * m` 次比較，時間複雜度為 `O(nm)`。而且即使
找到相同元素，還必須額外處理重複值。

本題真正需要的操作只有兩種：

1. 快速判斷某個值是否存在於另一個陣列。
2. 確保同一個共同元素只輸出一次。

Dictionary 與 HashSet 的平均查找、插入成本都是 `O(1)`，因此可以把重複的線性搜尋改成
雜湊查找。兩種實作都先用集合型資料結構整理 `nums1`，再以不同方式求出交集，平均時間可
降為 `O(n + m)`。

令：

- `n` 為 `nums1` 長度。
- `m` 為 `nums2` 長度。
- `u1` 為 `nums1` 中的唯一元素數量。
- `k` 為最後交集中的唯一元素數量。

## 解法比較

| 解法 | 核心做法 | 平均時間 | 輔助空間 | 結果空間 | 修改輸入 |
| --- | --- | --- | --- | --- | --- |
| `Intersection` | Dictionary 查找、HashSet 收集結果 | `O(n + m)` | `O(u1 + k)` | `O(k)` | 否 |
| `Intersection2` | HashSet 建立集合、`IntersectWith` 縮成交集 | `O(n + m)` | `O(u1)` | `O(k)` | 否 |

兩種解法最後都呼叫 `ToArray()` 建立回傳值，所以結果空間為 `O(k)`。表格中的輔助空間
不包含最後回傳陣列。

## 解法一：Dictionary 查找 + HashSet 去重

### 設計說明

`Intersection` 將「查找」與「建立唯一結果」分成兩個資料結構：

1. 掃描 `nums1`，將尚未出現的值加入 `Dictionary<int, int>`。
2. 掃描 `nums2`，使用 Dictionary 平均 `O(1)` 的查找判斷目前值是否也存在於 `nums1`。
3. 若為共同元素，就加入結果 HashSet。
4. HashSet 會自動忽略重複加入的值，因此結果中的每個元素都唯一。
5. 將結果集合轉成陣列後回傳。

Dictionary 的 value 在這個解法中只作為存在標記；真正需要的是 key 是否存在。刻意保留這個
版本，可以清楚展示「先建立查找表，再用另一個集合去重」的兩階段思路。

### 官方範例二演示

輸入：

- `nums1 = [4, 9, 5]`
- `nums2 = [9, 4, 9, 8, 4]`

先建立 Dictionary：

| 讀入 `nums1` 的值 | Dictionary keys |
| ---: | --- |
| 4 | `{4}` |
| 9 | `{4, 9}` |
| 5 | `{4, 9, 5}` |

接著掃描 `nums2`：

| 讀入值 | Dictionary 是否包含 | 動作 | 結果 HashSet |
| ---: | --- | --- | --- |
| 9 | 是 | 加入 9 | `{9}` |
| 4 | 是 | 加入 4 | `{9, 4}` |
| 9 | 是 | 再次加入但被 HashSet 去重 | `{9, 4}` |
| 8 | 否 | 忽略 | `{9, 4}` |
| 4 | 是 | 再次加入但被 HashSet 去重 | `{9, 4}` |

最後的集合是 `{9, 4}`，轉為陣列後可回傳 `[9, 4]` 或 `[4, 9]`。acceptance harness
在顯示前會排序副本，所以主控台穩定顯示 `[4, 9]`，但不改變公開 API 的順序不限契約。

## 解法二：HashSet.IntersectWith

### 設計說明

`Intersection2` 直接使用集合交集：

1. 由 `nums1` 建立本地 HashSet；建構過程已移除 `nums1` 內的重複值。
2. 呼叫 `intersection.IntersectWith(nums2)`。
3. `IntersectWith` 會移除集合中沒有出現在 `nums2` 的元素。
4. 剩餘內容自然就是唯一交集，再轉成陣列回傳。

`IntersectWith` 修改的是方法內新建立的 HashSet，不是呼叫端傳入的陣列，因此兩個輸入仍
保持原樣。相較於解法一，這個版本不需要分開維護查找表與結果集合，程式更直接地表達
「保留兩個集合的共同部分」。

### 官方範例二演示

同樣使用：

- `nums1 = [4, 9, 5]`
- `nums2 = [9, 4, 9, 8, 4]`

| 步驟 | 本地 HashSet 狀態 | 說明 |
| --- | --- | --- |
| 由 `nums1` 建立集合 | `{4, 9, 5}` | `nums1` 的每個唯一值各保留一次。 |
| 檢查 `nums2` 是否包含 4 | `{4, 9, 5}` | 4 存在，因此保留。 |
| 檢查 `nums2` 是否包含 9 | `{4, 9, 5}` | 9 存在，因此保留。 |
| 檢查 `nums2` 是否包含 5 | `{4, 9}` | 5 不存在，因此移除。 |
| 轉為陣列 | `[4, 9]` 或 `[9, 4]` | 順序不限，元素保持唯一。 |

## Acceptance Harness

專案目前沒有 xUnit、NUnit 或 MSTest 專案。`Main` 是可重複執行的 acceptance harness，
使用相同案例檢查兩個公開方法。

每個案例會為每個方法建立各自的 `nums1` 與 `nums2` 副本，並驗證：

1. 實際結果與預期結果集合相等，不受回傳順序影響。
2. 實際結果陣列沒有重複元素。
3. 兩個方法執行後，四個輸入副本都與原始案例逐元素相同。

案例只有在上述條件全部成立時才顯示 PASS。任一案例失敗都會把 process exit code 設為 1。
顯示結果時只排序新副本，不會修改公開方法的回傳值或原始輸入。

| # | 案例 | `nums1` | `nums2` | 預期 |
| ---: | --- | --- | --- | --- |
| 1 | 官方範例一 | `[1, 2, 2, 1]` | `[2, 2]` | `[2]` |
| 2 | 官方範例二 | `[4, 9, 5]` | `[9, 4, 9, 8, 4]` | `[4, 9]` |
| 3 | 無交集 | `[1, 2, 3]` | `[4, 5, 6]` | `[]` |
| 4 | 兩側都有重複共同值 | `[1, 1, 2, 2]` | `[2, 2, 2]` | `[2]` |
| 5 | 最小長度與元素下界 | `[0]` | `[0]` | `[0]` |
| 6 | 不同順序的完整交集 | `[0, 500, 1000]` | `[1000, 500, 0]` | `[0, 500, 1000]` |
| 7 | 部分交集 | `[1, 2, 3, 4]` | `[2, 4, 6, 8]` | `[2, 4]` |
| 8 | 長度與元素上界 | 1000 個 `1000` | 999 個 `0` 加上一個 `1000` | `[1000]` |

## 建置、測試與執行

需求：

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

從此題目的 repository root 執行：

```bash
dotnet restore leetcode_349/leetcode_349.csproj
dotnet build leetcode_349/leetcode_349.csproj --nologo --no-restore
dotnet test leetcode_349.sln --nologo --no-restore
dotnet run --no-build --project leetcode_349/leetcode_349.csproj
git diff --check
```

目前沒有正式測試專案，因此 `dotnet test` 是 solution-level smoke check；實際行為驗證由
`Main` 中的八個案例完成。

### 實際執行輸出

以下內容來自完成建置後的 fresh run：

```text
Case: Official example 1
Nums1: [1, 2, 2, 1]
Nums2: [2, 2]
Expected: [2]
Intersection: [2]
Intersection2: [2]
Inputs preserved: True
Result: PASS

Case: Official example 2
Nums1: [4, 9, 5]
Nums2: [9, 4, 9, 8, 4]
Expected: [4, 9]
Intersection: [4, 9]
Intersection2: [4, 9]
Inputs preserved: True
Result: PASS

Case: No intersection
Nums1: [1, 2, 3]
Nums2: [4, 5, 6]
Expected: []
Intersection: []
Intersection2: []
Inputs preserved: True
Result: PASS

Case: Duplicates in both arrays
Nums1: [1, 1, 2, 2]
Nums2: [2, 2, 2]
Expected: [2]
Intersection: [2]
Intersection2: [2]
Inputs preserved: True
Result: PASS

Case: Minimum lengths and value
Nums1: [0]
Nums2: [0]
Expected: [0]
Intersection: [0]
Intersection2: [0]
Inputs preserved: True
Result: PASS

Case: Complete intersection in different order
Nums1: [0, 500, 1000]
Nums2: [1000, 500, 0]
Expected: [0, 500, 1000]
Intersection: [0, 500, 1000]
Intersection2: [0, 500, 1000]
Inputs preserved: True
Result: PASS

Case: Partial intersection
Nums1: [1, 2, 3, 4]
Nums2: [2, 4, 6, 8]
Expected: [2, 4]
Intersection: [2, 4]
Intersection2: [2, 4]
Inputs preserved: True
Result: PASS

Case: Maximum lengths and value
Nums1: [length 1000; all values are 1000]
Nums2: [length 1000; 999 zeros followed by 1000]
Expected: [1000]
Intersection: [1000]
Intersection2: [1000]
Inputs preserved: True
Result: PASS

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
.
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
├── leetcode_349.sln
└── leetcode_349/
    ├── Program.cs
    └── leetcode_349.csproj
```
