# 1464. Maximum Product of Two Elements in an Array／陣列中兩個元素的最大乘積

給定一個正整數陣列，選出兩個不同位置的元素，使兩者各減一後的乘積最大。本專案保留三種
教學解法，分別展示「單趟維護前兩大值」、「複製後排序」與「枚舉右端、維護左側最大值」的
思考方式。

- [LeetCode English](https://leetcode.com/problems/maximum-product-of-two-elements-in-an-array/)
- [LeetCode 中文](https://leetcode.cn/problems/maximum-product-of-two-elements-in-an-array/)

## 題目說明

給定整數陣列 `nums`，選擇兩個不同索引 `i` 與 `j`，回傳
`(nums[i] - 1) * (nums[j] - 1)` 的最大值。

因為所有元素都是正整數，若 `a >= b`，則 `a - 1 >= b - 1 >= 0`。因此，原始數值最大的
兩個元素也會使各減一後的乘積最大。三種解法的差異不在於要找什麼，而在於如何找出或逐步
利用這兩個最大值。

## 限制條件

- `2 <= nums.Length <= 500`
- `1 <= nums[i] <= 1000`
- 公開 API 僅處理 LeetCode 定義的有效輸入，不另外定義無效輸入行為
- `MaxProduct`、`MaxProduct2` 與 `MaxProduct3` 都不輸出資料，也不修改 `nums`

## 解法比較

| 解法 | 核心做法 | 時間複雜度 | 輔助空間 | 是否修改輸入 |
| --- | --- | --- | --- | --- |
| `MaxProduct` | 單趟維護最大值與次大值 | `O(n)` | `O(1)` | 否 |
| `MaxProduct2` | 複製陣列、排序後取末兩項 | `O(n log n)` | `O(n)` | 否 |
| `MaxProduct3` | 枚舉右端並維護左側最大值與最佳乘積 | `O(n)` | `O(1)` | 否 |

三種方法都只回傳一個整數，因此結果空間皆為 `O(1)`。解法二的 `O(n)` 輔助空間來自保護
原始輸入所建立的完整陣列副本。

## 解法一：單趟維護前兩大值

### 設計概念

`MaxProduct` 在掃描過程中維護兩個不變量：

- `largest` 是目前已掃描元素中的最大值。
- `secondLargest` 是目前已掃描元素中的次大值。

當新值大於 `largest` 時，原本的最大值不能直接丟棄，因為它會成為新的次大值，所以先把
`largest` 下移到 `secondLargest`，再更新 `largest`。若新值只大於 `secondLargest`，則只
更新次大值。使用嚴格的大於比較仍能處理重複最大值：第二個相同最大值會進入
`secondLargest`。

### 範例演示

以 `nums = [10, 2, 5, 2]` 為例：

| 讀入值 | 更新前 `(largest, secondLargest)` | 判斷 | 更新後 |
| ---: | --- | --- | --- |
| 10 | `(0, 0)` | 10 大於最大值 | `(10, 0)` |
| 2 | `(10, 0)` | 2 只大於次大值 | `(10, 2)` |
| 5 | `(10, 2)` | 5 只大於次大值 | `(10, 5)` |
| 2 | `(10, 5)` | 不需更新 | `(10, 5)` |

最後使用前兩大值計算：

```plaintext
(10 - 1) * (5 - 1) = 9 * 4 = 36
```

## 解法二：複製後排序

### 設計概念

`MaxProduct2` 是最直觀的做法：

1. 複製 `nums`，避免排序改動呼叫端資料。
2. 將副本由小到大排序。
3. 取排序後最後兩個元素，也就是最大值與次大值。
4. 將兩值各減一後相乘。

這個方法容易閱讀與驗證，但完整排序做了比「只找前兩大值」更多的工作，因此時間複雜度高於
另外兩種線性解法。

### 範例演示

同樣使用 `nums = [10, 2, 5, 2]`：

```plaintext
原始輸入：       [10, 2, 5, 2]
建立排序副本：   [10, 2, 5, 2]
副本排序後：     [2, 2, 5, 10]
取最後兩項：     5 與 10
計算：           (10 - 1) * (5 - 1) = 36
原始輸入仍為：   [10, 2, 5, 2]
```

## 解法三：枚舉右端並維護左側最大值

### 設計概念

暴力解法會枚舉所有 `i < j` 的索引組合。`MaxProduct3` 保留「依序枚舉右端 `j`」的觀點，
但不再重新掃描整段左側：

- `largestOnLeft` 保存目前位置左側曾出現的最大值。
- `answer` 保存目前找到的最佳乘積。
- 讀到目前值時，先用它和 `largestOnLeft` 更新答案，再把目前值納入
  `largestOnLeft`。這個順序確保候選值來自兩個不同索引。

初始的 `largestOnLeft` 與 `answer` 都是 0。由於有效輸入至少包含兩個正整數，第一個元素
產生的非正候選值不會改變答案；從第二個元素開始，`largestOnLeft` 就是實際存在於左側的
最大值。

### 範例演示

以 `nums = [10, 2, 5, 2]` 為例：

| 目前值 | 計算前左側最大值 | 候選乘積 | 更新後答案 | 納入目前值後的左側最大值 |
| ---: | ---: | ---: | ---: | ---: |
| 10 | 0 | `(0 - 1) * (10 - 1) = -9` | 0 | 10 |
| 2 | 10 | `(10 - 1) * (2 - 1) = 9` | 9 | 10 |
| 5 | 10 | `(10 - 1) * (5 - 1) = 36` | 36 | 10 |
| 2 | 10 | `(10 - 1) * (2 - 1) = 9` | 36 | 10 |

最終答案為 `36`。與解法一不同，這個方法不直接保存次大值，而是在每個右端位置立即評估
「目前右值 × 左側最佳選擇」，再保留全域最佳答案。

## Acceptance Harness

專案沒有正式測試專案；`Main` 是可重複執行的 acceptance harness，包含八個確定性案例。
每個案例會建立三份獨立輸入並分別呼叫三個公開方法。只有下列條件全部成立時，該案例才會
顯示 PASS：

1. 三個方法都回傳案例定義的預期值。
2. 三份輸入在方法執行後都與原始案例逐元素相同。

任何案例失敗都會讓 process exit code 成為 1。

| # | 案例 | 輸入 | 預期 |
| --- | --- | --- | ---: |
| 1 | 官網範例 1 | `[3, 4, 5, 2]` | 12 |
| 2 | 官網範例 2／重複最大值 | `[1, 5, 4, 5]` | 16 |
| 3 | 官網範例 3／最小長度 | `[3, 7]` | 12 |
| 4 | 最小元素值 | `[1, 1]` | 0 |
| 5 | 最大元素值 | `[1000, 1000]` | 998001 |
| 6 | 最大值先出現／次大值回歸 | `[10, 2, 5, 2]` | 36 |
| 7 | 一般未排序回歸 | `[4, 9, 2, 8, 3]` | 56 |
| 8 | 最大長度 | `1..498, 1000, 999`，共 500 個元素 | 997002 |

## 建置與執行

從 repository root `/Users/qiuzili/Leetcode/Leetcode_folder` 執行：

```bash
dotnet build leetcode_1464/leetcode_1464/leetcode_1464.csproj --nologo
dotnet run --no-build --project leetcode_1464/leetcode_1464/leetcode_1464.csproj
```

若直接開啟 `leetcode_1464/` 作為 VS Code workspace，則從題目根目錄執行：

```bash
dotnet build leetcode_1464/leetcode_1464.csproj --nologo
dotnet run --no-build --project leetcode_1464/leetcode_1464.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: Official example 1
Input: [3, 4, 5, 2]
Expected: 12
MaxProduct: 12
MaxProduct2: 12
MaxProduct3: 12
Input preserved: True
Result: PASS

Case: Official example 2 / duplicate maximum
Input: [1, 5, 4, 5]
Expected: 16
MaxProduct: 16
MaxProduct2: 16
MaxProduct3: 16
Input preserved: True
Result: PASS

Case: Official example 3 / minimum length
Input: [3, 7]
Expected: 12
MaxProduct: 12
MaxProduct2: 12
MaxProduct3: 12
Input preserved: True
Result: PASS

Case: Minimum values
Input: [1, 1]
Expected: 0
MaxProduct: 0
MaxProduct2: 0
MaxProduct3: 0
Input preserved: True
Result: PASS

Case: Maximum values
Input: [1000, 1000]
Expected: 998001
MaxProduct: 998001
MaxProduct2: 998001
MaxProduct3: 998001
Input preserved: True
Result: PASS

Case: Largest arrives first / second-largest regression
Input: [10, 2, 5, 2]
Expected: 36
MaxProduct: 36
MaxProduct2: 36
MaxProduct3: 36
Input preserved: True
Result: PASS

Case: Unsorted general regression
Input: [4, 9, 2, 8, 3]
Expected: 56
MaxProduct: 56
MaxProduct2: 56
MaxProduct3: 56
Input preserved: True
Result: PASS

Case: Maximum-length case
Input: [length 500; values 1..498, 1000, 999]
Expected: 997002
MaxProduct: 997002
MaxProduct2: 997002
MaxProduct3: 997002
Input preserved: True
Result: PASS

Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_1464/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   ├── readme-template.md
│   └── superpowers/
│       ├── plans/2026-07-18-leetcode-1464-net10-migration.md
│       └── specs/2026-07-18-leetcode-1464-net10-migration-design.md
└── leetcode_1464/
    ├── Program.cs
    └── leetcode_1464.csproj
```
