# LeetCode 217：Contains Duplicate／存在重複元素

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4)
[![LeetCode 217](https://img.shields.io/badge/LeetCode-217-FFA116)](https://leetcode.com/problems/contains-duplicate/description/)

以 C# 實作「存在重複元素」的兩種解法，分別展示排序後相鄰比較，以及使用
`Dictionary<int, int>` 記錄已看過數值的思考方式。專案包含可直接執行的 acceptance
harness，會使用固定測資驗證兩種解法並輸出 PASS／FAIL。

## 題目說明

給定整數陣列 `nums`：

- 若任一數值在陣列中至少出現兩次，回傳 `true`。
- 若陣列中的每個數值都互不相同，回傳 `false`。

題目連結：

- [LeetCode 217 - Contains Duplicate](https://leetcode.com/problems/contains-duplicate/description/)
- [力扣 217 - 存在重複元素](https://leetcode.cn/problems/contains-duplicate/description/)

## 限制條件

- `1 <= nums.Length <= 100000`
- `-10^9 <= nums[i] <= 10^9`
- 公開方法假設 `nums` 不為 `null`，不另外定義 `null` 的處理方式。
- Acceptance harness 額外保留空陣列案例，確認兩種實作在此防禦性輸入下都回傳
  `false`；空陣列不是官方題目的有效輸入。

## 解題概念與出發點

題目只要求判斷「是否存在」重複值，不需要列出所有重複值、計算完整頻率，或回傳重複位置。
因此，一旦找到第二次出現的數值，就可以立刻回傳 `true`，不必繼續掃描。

兩種解法採用不同方式，讓原本可能需要兩兩比較的問題變得容易判斷：

1. **排序法**改變元素排列，使相同值集中在相鄰位置。
2. **Dictionary 法**保留已看過的值，使每個新值都能直接查詢是否曾經出現。

| 解法 | 核心資料結構 | 時間複雜度 | 輔助空間 | 結果空間 | 是否修改輸入 |
| --- | --- | --- | --- | --- | --- |
| `ContainsDuplicate` | 原陣列排序 | `O(n log n)` | `O(log n)` | `O(1)` | 是，會原地排序 |
| `ContainsDuplicate2` | `Dictionary<int, int>` | 平均 `O(n)` | `O(n)` | `O(1)` | 否 |

兩個方法都只回傳一個布林值，因此結果空間皆為 `O(1)`。排序法的輔助空間來自排序程序；
Dictionary 法最壞需要保存所有互不相同的元素。

## 解法一：排序後比較相鄰元素

### 設計說明

`ContainsDuplicate` 先呼叫 `Array.Sort(nums)` 原地排序。排序完成後，所有相同數值都會形成
連續區段；因此只要從左到右比較 `nums[i - 1]` 與 `nums[i]`，即可判斷是否存在重複值。

演算法流程：

1. 將輸入陣列由小到大排序。
2. 從索引 1 開始走訪，每次比較目前元素與前一個元素。
3. 若兩者相等，代表同一數值至少出現兩次，立即回傳 `true`。
4. 若完成所有相鄰比較仍未發現相等元素，回傳 `false`。

核心不變量是：進行相鄰比較時，陣列已經排序，所以任何重複值必定至少有一對相鄰元素。
這也是不需要枚舉所有索引組合的原因。

> [!IMPORTANT]
> `Array.Sort` 會直接改變傳入陣列的順序。Acceptance harness 會為此方法建立獨立副本，
> 避免排序結果影響另一種解法或原始案例顯示。

### 範例演示

輸入 `nums = [1, 2, 3, 1]`：

```text
原始輸入： [1, 2, 3, 1]
排序結果： [1, 1, 2, 3]
```

| 比較位置 | 左值 | 右值 | 判斷 |
| ---: | ---: | ---: | --- |
| 0 與 1 | 1 | 1 | 相等，立即回傳 `true` |

排序後第一對元素已相等，因此後續的 `1` 與 `2`、`2` 與 `3` 不必再檢查。

### 複雜度

- 時間：`O(n log n)`，主要成本為排序；排序後的相鄰掃描為 `O(n)`。
- 輔助空間：`O(log n)`，來自排序程序使用的堆疊空間。
- 結果空間：`O(1)`，只回傳一個布林值。

## 解法二：Dictionary 記錄已看過的數值

### 設計說明

`ContainsDuplicate2` 使用 `Dictionary<int, int>`，將每個已看過的數值當成鍵。題目只需要
知道某個值是否已出現，不需要累加完整頻率，因此字典中的值固定記為 `1`；判斷重點是鍵
是否存在。

演算法流程：

1. 建立空字典 `seenNumbers`。
2. 由左到右讀取每個 `num`。
3. 若 `seenNumbers.ContainsKey(num)` 為 `true`，代表目前是第二次遇到該值，立即回傳
   `true`。
4. 否則將 `num` 加入字典，繼續處理下一個元素。
5. 掃描結束仍未找到既有鍵時，回傳 `false`。

核心不變量是：處理目前元素前，字典恰好包含所有已走訪過的不同數值。因此，字典中存在
目前鍵，等價於目前值曾在較早位置出現。

### 範例演示

輸入 `nums = [1, 2, 3, 1]`：

| 目前值 | 檢查前的字典鍵 | 是否已存在 | 處理結果 |
| ---: | --- | --- | --- |
| 1 | `{}` | 否 | 加入 1，繼續 |
| 2 | `{1}` | 否 | 加入 2，繼續 |
| 3 | `{1, 2}` | 否 | 加入 3，繼續 |
| 1 | `{1, 2, 3}` | 是 | 立即回傳 `true` |

這個方法不需要改變輸入順序，而且通常能在遇到第一個重複值時提前結束。

### 複雜度

- 時間：平均 `O(n)`，每個元素進行一次字典查詢，未出現時再加入字典。
- 輔助空間：`O(n)`，所有元素互異時需要保存 `n` 個鍵。
- 結果空間：`O(1)`，只回傳一個布林值。

## 可執行驗證案例

專案沒有獨立測試專案；`Main` 是可重複執行的 acceptance harness。每個案例都會為兩種
解法建立獨立陣列副本，再分別比對實際結果與預期值，共執行 14 項檢查。

| # | 案例 | 輸入 | 預期 |
| ---: | --- | --- | --- |
| 1 | 官方案例 1：非相鄰重複值 | `[1, 2, 3, 1]` | `true` |
| 2 | 官方案例 2：所有元素皆不重複 | `[1, 2, 3, 4]` | `false` |
| 3 | 官方案例 3：多個數值重複出現 | `[1, 1, 1, 3, 3, 4, 3, 2, 4, 2]` | `true` |
| 4 | 防禦性案例：空陣列 | `[]` | `false` |
| 5 | 邊界案例：單一元素 | `[1]` | `false` |
| 6 | 邊界案例：包含負數重複值 | `[-1, 0, -1]` | `true` |
| 7 | 邊界案例：最小值與最大值 | `[-10^9, 10^9, -10^9]` | `true` |

任一方法的實際結果不符合預期時，該項會顯示 `FAIL`，且程式會設定非零 process exit
code，方便命令列或持續整合流程辨識失敗。

## 建置與執行

從 `leetcode_217` 題目根目錄執行：

```bash
dotnet restore leetcode_217/leetcode_217.csproj
dotnet build leetcode_217/leetcode_217.csproj --nologo --no-restore
dotnet run --no-build --project leetcode_217/leetcode_217.csproj
```

Fresh run 的完整輸出如下：

```text
LeetCode 217 - Contains Duplicate
==================================================
Case: 官方案例 1：非相鄰重複值
Input: [1, 2, 3, 1]
Expected: True
ContainsDuplicate: True (PASS)
ContainsDuplicate2: True (PASS)

Case: 官方案例 2：所有元素皆不重複
Input: [1, 2, 3, 4]
Expected: False
ContainsDuplicate: False (PASS)
ContainsDuplicate2: False (PASS)

Case: 官方案例 3：多個數值重複出現
Input: [1, 1, 1, 3, 3, 4, 3, 2, 4, 2]
Expected: True
ContainsDuplicate: True (PASS)
ContainsDuplicate2: True (PASS)

Case: 防禦性案例：空陣列
Input: []
Expected: False
ContainsDuplicate: False (PASS)
ContainsDuplicate2: False (PASS)

Case: 邊界案例：單一元素
Input: [1]
Expected: False
ContainsDuplicate: False (PASS)
ContainsDuplicate2: False (PASS)

Case: 邊界案例：包含負數重複值
Input: [-1, 0, -1]
Expected: True
ContainsDuplicate: True (PASS)
ContainsDuplicate2: True (PASS)

Case: 邊界案例：最小值與最大值
Input: [-1000000000, 1000000000, -1000000000]
Expected: True
ContainsDuplicate: True (PASS)
ContainsDuplicate2: True (PASS)

Summary: 14/14 checks passed.
```

## 專案結構

```text
.
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_217/
│   ├── Program.cs
│   └── leetcode_217.csproj
└── leetcode_217.sln
```

## 驗證

完成變更後，執行下列指令確認 C# 專案仍可還原、建置與執行：

```bash
dotnet restore leetcode_217/leetcode_217.csproj
dotnet build leetcode_217/leetcode_217.csproj --nologo --no-restore
dotnet run --no-build --project leetcode_217/leetcode_217.csproj
```

本專案目前沒有自動化測試專案，因此使用成功建置與上述 14 項 console acceptance checks
作為行為驗收。最後使用下列指令檢查 Git diff 的多餘空白：

```bash
git diff --check
```
