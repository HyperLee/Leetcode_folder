# LeetCode 350：兩個陣列的交集 II

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/C%23-Console-239120)
![LeetCode](https://img.shields.io/badge/LeetCode-350-FFA116)

這是一個使用 C# 與 .NET 10 實作的教學型主控台專案。專案保留原本的 Dictionary
次數表解法，並新增排序副本搭配雙指標的解法，示範如何在保留重複次數的前提下求出兩個
整數陣列的交集。

- [LeetCode English](https://leetcode.com/problems/intersection-of-two-arrays-ii/)
- [LeetCode 中文](https://leetcode.cn/problems/intersection-of-two-arrays-ii/)

## 題目說明

給定兩個整數陣列 `nums1` 與 `nums2`，回傳兩者的交集。某個元素在結果中出現的次數，
必須等於它在兩個輸入陣列中出現次數的較小值；結果的排列順序不限。

例如：

| 輸入 | 輸出 | 說明 |
| --- | --- | --- |
| `nums1 = [1, 2, 2, 1]`、`nums2 = [2, 2]` | `[2, 2]` | 兩側都至少有兩個 `2`，所以結果保留兩個。 |
| `nums1 = [4, 9, 5]`、`nums2 = [9, 4, 9, 8, 4]` | `[4, 9]` | `[9, 4]` 也正確，因為題目不限制順序。 |

這與 LeetCode 349 的「唯一交集」不同：本題不能使用單純的集合去重，必須追蹤每個值還有
多少次可以加入結果。

## 限制條件

- `1 <= nums1.Length, nums2.Length <= 1000`
- `0 <= nums1[i], nums2[i] <= 1000`
- 公開方法預期接收符合題目限制的非 `null` 陣列，不另外定義無效輸入行為。
- `Intersect` 與 `Intersect2` 都不修改輸入陣列，也不直接輸出主控台內容。
- 回傳順序不保證；驗證時必須比較元素及其重複次數，不能只比較集合。

官方延伸問題還包含：

1. 如果兩個陣列已經排序，如何最佳化？
2. 如果其中一個陣列遠小於另一個陣列，應選擇哪種方法？
3. 如果較大陣列存放在磁碟且記憶體不足以一次載入，應如何處理？

## 解題概念與出發點

暴力法會讓 `nums1` 的每個元素逐一和 `nums2` 比較。若兩個陣列長度分別是 `n` 與 `m`，
最壞情況需要 `O(nm)` 次比較；為避免同一位置被重複使用，還要額外維護配對狀態。

本專案改用兩種不同思路：

1. **以空間換時間**：Dictionary 記錄較短陣列中每個元素的剩餘次數，再以平均 `O(1)`
   的雜湊查找掃描較長陣列。
2. **先建立順序再線性合併**：複製並排序兩個陣列，利用雙指標只向前走訪一次。

令：

- `n` 為 `nums1` 長度。
- `m` 為 `nums2` 長度。
- `k` 為交集結果的元素數量，包含重複值。

## 解法比較

| 解法 | 核心做法 | 時間複雜度 | 輔助空間 | 結果空間 | 修改輸入 |
| --- | --- | --- | --- | --- | --- |
| `Intersect` | 較短陣列建立 Dictionary 次數表 | 平均 `O(n + m)` | `O(min(n, m))` | `O(k)` | 否 |
| `Intersect2` | 排序兩個副本後使用雙指標 | `O(n log n + m log m)` | `O(n + m)` | `O(k)` | 否 |

輔助空間不包含最後回傳的結果陣列。`Intersect2` 的 `O(n + m)` 來自兩個排序副本；
這項選擇讓公開方法保有不修改輸入的契約。

## 解法一：Dictionary 次數表

### 設計說明

`Intersect` 先比較兩個陣列長度，將較短陣列傳給 `GetIntersection` 建立次數表。選擇較短
陣列可把 Dictionary 的大小控制在 `O(min(n, m))`。

`GetIntersection` 的流程如下：

1. 掃描較短陣列，以 Dictionary 的 key 保存元素、value 保存尚未配對的次數。
2. 依原始順序掃描較長陣列。
3. 如果目前元素不在次數表中，代表沒有可用配對，直接略過。
4. 如果元素存在，將它加入結果並把剩餘次數減一。
5. 次數降為零時移除該 key，後續相同值就不會超量加入。

這個做法也適合「一個陣列明顯較小」的情境。若較大陣列位於磁碟，還能先把較小陣列的
次數表保留在記憶體，再以串流方式分批掃描較大陣列，不必同時載入全部資料。

### 範例演示

使用官方範例二：

- 較短陣列：`[4, 9, 5]`
- 較長陣列：`[9, 4, 9, 8, 4]`

先建立次數表：

| 讀入較短陣列的值 | Dictionary |
| ---: | --- |
| 4 | `{4: 1}` |
| 9 | `{4: 1, 9: 1}` |
| 5 | `{4: 1, 9: 1, 5: 1}` |

再掃描較長陣列：

| 讀入值 | 判斷與動作 | Dictionary 剩餘內容 | 結果 |
| ---: | --- | --- | --- |
| 9 | 存在，加入結果；次數歸零後移除 | `{4: 1, 5: 1}` | `[9]` |
| 4 | 存在，加入結果；次數歸零後移除 | `{5: 1}` | `[9, 4]` |
| 9 | 已無可用次數，略過 | `{5: 1}` | `[9, 4]` |
| 8 | 不存在，略過 | `{5: 1}` | `[9, 4]` |
| 4 | 已無可用次數，略過 | `{5: 1}` | `[9, 4]` |

公開方法可以回傳 `[9, 4]`，因為題目不限制順序。Acceptance harness 顯示前會排序副本，
所以 transcript 穩定顯示 `[4, 9]`，但不會改變方法實際回傳值或輸入陣列。

## 解法二：排序副本與雙指標

### 設計說明

`Intersect2` 不直接排序輸入，而是先建立兩個完整副本。排序後使用 `nums1Index` 與
`nums2Index` 分別指向兩個陣列尚未處理的最小值：

1. 兩個值相等：找到一組配對，加入結果，兩個指標都前進。
2. `nums1` 的值較小：它不可能和 `nums2` 目前或後續更大的值配對，只前進
   `nums1Index`。
3. `nums2` 的值較小：同理，只前進 `nums2Index`。
4. 任一指標抵達陣列末端就結束，因為另一側剩餘元素已不可能再配對。

相等時兩個指標只各前進一次，因此自然保留正確的重複次數。例如一側有三個 `1`、另一側
只有兩個 `1`，最多只會成功配對兩次。

如果呼叫端已能保證輸入排序完成，且允許直接使用該順序，雙指標掃描本身只需要
`O(n + m)` 時間與結果之外的 `O(1)` 額外空間。本專案仍複製並排序，藉此維持統一的
「不修改輸入」公開契約。

### 範例演示

使用重複次數不對稱案例：

- `nums1 = [1, 1, 1, 2]`
- `nums2 = [1, 1, 2, 2]`

兩者本來已排序，雙指標過程如下：

| `nums1Index` 的值 | `nums2Index` 的值 | 動作 | 結果 |
| ---: | ---: | --- | --- |
| 1 | 1 | 相等，加入 1；兩側前進 | `[1]` |
| 1 | 1 | 相等，加入 1；兩側前進 | `[1, 1]` |
| 1 | 2 | 左側較小，只前進左指標 | `[1, 1]` |
| 2 | 2 | 相等，加入 2；兩側前進 | `[1, 1, 2]` |

此時 `nums1` 已走完，演算法結束。雖然 `nums2` 還剩一個 `2`，另一側已沒有可配對的值，
所以正確結果為 `[1, 1, 2]`。

## Acceptance Harness

專案目前沒有 xUnit、NUnit 或 MSTest 專案。`Main` 是可重複執行的 acceptance harness，
每組案例都會：

1. 為 `Intersect` 與 `Intersect2` 建立各自的輸入副本。
2. 排序 Expected 與 Actual 的副本，以多重集合語意比較元素和重複次數。
3. 驗證兩個解法執行後，四個輸入副本都與原始測試資料逐元素相同。
4. 只有兩個結果與輸入保存檢查全部成立，該案例才顯示 `PASS`。
5. 任一案例失敗時，將 process exit code 設為 1。

| # | 案例 | 驗證重點 |
| ---: | --- | --- |
| 1 | 官方範例一 | 相同元素重複兩次 |
| 2 | 官方範例二 | 多個共同元素與順序無關 |
| 3 | 無交集 | 正確回傳空陣列 |
| 4 | 重複次數不對稱 | 使用兩側出現次數的較小值 |
| 5 | 最短長度與元素下界 | 長度 1、值 0 |
| 6 | 不同順序的完整交集 | 所有元素相交但排列相反 |
| 7 | 第二個陣列較短 | `Intersect` 交換短、長陣列角色 |
| 8 | 長度與元素上界 | 兩側長度 1000、共同值 1000 |

## 建置、測試與執行

需求：

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

從此題目的 repository root 執行：

```bash
dotnet restore leetcode_350/leetcode_350.csproj
dotnet build leetcode_350/leetcode_350.csproj --nologo --no-restore
dotnet run --no-build --project leetcode_350/leetcode_350.csproj
git diff --check
```

目前沒有正式測試專案，因此不另外執行 `dotnet test`；行為驗證由 `Main` 中的八組案例完成。

### 實際執行輸出

以下內容來自完成建置後的 fresh run：

```text
Case: Official example 1
Nums1: [1, 2, 2, 1]
Nums2: [2, 2]
Expected: [2, 2]
Intersect: [2, 2]
Intersect2: [2, 2]
Inputs preserved: True
Result: PASS

Case: Official example 2
Nums1: [4, 9, 5]
Nums2: [9, 4, 9, 8, 4]
Expected: [4, 9]
Intersect: [4, 9]
Intersect2: [4, 9]
Inputs preserved: True
Result: PASS

Case: No intersection
Nums1: [1, 2, 3]
Nums2: [4, 5, 6]
Expected: []
Intersect: []
Intersect2: []
Inputs preserved: True
Result: PASS

Case: Asymmetric duplicate counts
Nums1: [1, 1, 1, 2]
Nums2: [1, 1, 2, 2]
Expected: [1, 1, 2]
Intersect: [1, 1, 2]
Intersect2: [1, 1, 2]
Inputs preserved: True
Result: PASS

Case: Minimum lengths and value
Nums1: [0]
Nums2: [0]
Expected: [0]
Intersect: [0]
Intersect2: [0]
Inputs preserved: True
Result: PASS

Case: Complete intersection in different order
Nums1: [0, 500, 1000]
Nums2: [1000, 500, 0]
Expected: [0, 500, 1000]
Intersect: [0, 500, 1000]
Intersect2: [0, 500, 1000]
Inputs preserved: True
Result: PASS

Case: Second array is shorter
Nums1: [1, 2, 2, 3, 3]
Nums2: [2, 3]
Expected: [2, 3]
Intersect: [2, 3]
Intersect2: [2, 3]
Inputs preserved: True
Result: PASS

Case: Maximum lengths and value
Nums1: [length 1000; all values are 1000]
Nums2: [length 1000; 999 zeros followed by 1000]
Expected: [1000]
Intersect: [1000]
Intersect2: [1000]
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
├── leetcode_350.sln
└── leetcode_350/
    ├── Program.cs
    └── leetcode_350.csproj
```

- `Program.cs`：包含兩種交集演算法與八組可執行驗證案例。
- `leetcode_350.csproj`：目標框架為 `net10.0` 的主控台專案。
- `docs/readme-template.md`：本 README 遵循的初始文件指引。
- `.vscode/`：提供預設建置工作與直接啟動 `leetcode_350` 的偵錯設定。
