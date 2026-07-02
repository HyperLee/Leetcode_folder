# LeetCode 268 - Missing Number

使用 .NET Console 專案示範 LeetCode 268 `Missing Number` 的三種常見解法：排序、HashSet、位元 XOR。專案目前將所有題解與可執行範例集中在 `leetcode_268/Program.cs`，方便直接 build 與 run 觀察結果。

## 題目連結

- [LeetCode 268. Missing Number](https://leetcode.com/problems/missing-number/description/)
- [LeetCode CN 268. 丢失的数字](https://leetcode.cn/problems/missing-number/description/)

## 題目說明

給定一個長度為 `n` 的整數陣列 `nums`，其中包含範圍 `[0, n]` 內的 `n` 個互不相同整數。因為完整範圍應該有 `n + 1` 個數字，所以一定會恰好缺少其中一個值。題目要求找出這個缺少的數字。

例如：

- `nums = [3, 0, 1]` 時，完整範圍是 `[0, 1, 2, 3]`，缺少的是 `2`
- `nums = [0, 1]` 時，完整範圍是 `[0, 1, 2]`，缺少的是 `2`

## 限制條件

- `nums` 內共有 `n` 個數字。
- 每個數字都落在 `[0, n]` 範圍內。
- 每個數字都互不重複。
- 完整區間 `[0, n]` 中恰好缺少一個數字。

## 解題概念與出發點

這題的關鍵不是「找一個不存在的值」，而是「利用完整範圍 `[0, n]` 一定已知」這件事。因為題目已經保證：

1. 數字不重複。
2. 範圍固定。
3. 只缺一個。

所以我們可以從三個方向思考：

1. 先排序，再檢查每個位置應該出現什麼數字。
2. 先把所有數字存起來，再反查哪個值沒出現。
3. 利用 XOR 讓成對出現的數字互相抵消，只留下缺失值。

## 解法比較

| 解法 | 核心想法 | 時間複雜度 | 空間複雜度 | 特性 |
| --- | --- | --- | --- | --- |
| `MissingNumber` | 排序後比對索引與數值 | `O(n log n)` | 依排序實作而定 | 最直觀，但會改動原陣列 |
| `MissingNumber2` | 用 `HashSet` 快速查找缺失值 | `O(n)` | `O(n)` | 思路清楚、容易理解 |
| `MissingNumber3` | 用 XOR 抵消重複出現的數字 | `O(n)` | `O(1)` | 最精煉，空間效率最佳 |

> [!NOTE]
> `MissingNumber` 會先排序，因此會原地改動輸入陣列。專案在示例執行時會先複製陣列，避免三種解法互相影響。

## 解法一：排序比對

### 設計說明

這個方法的想法最直接：既然完整範圍是 `[0, n]`，排序完成後，理想情況下索引 `0` 應該放 `0`、索引 `1` 應該放 `1`，依此類推。

一旦某個位置出現「索引和值不同」，就代表那個索引對應的數字缺失了。

例如排序後得到：

- `index 0 -> value 0`
- `index 1 -> value 1`
- `index 2 -> value 3`

走到索引 `2` 時，發現值是 `3` 而不是 `2`，就知道缺少的是 `2`。

如果整個迴圈都沒有發現不一致，代表 `0` 到 `n - 1` 都在，缺少的只能是最後一個值 `n`。

### 範例演示流程

以 `nums = [3, 0, 1]` 為例：

1. 先排序，得到 `[0, 1, 3]`
2. 比對索引 `0`，值是 `0`，正確
3. 比對索引 `1`，值是 `1`，正確
4. 比對索引 `2`，值是 `3`，不等於索引 `2`
5. 因此答案就是 `2`

這個方法的優點是容易看懂，缺點是排序成本較高，而且會改動原始輸入。

## 解法二：HashSet 反查

### 設計說明

這個方法先把陣列中的所有數字丟進 `HashSet<int>`。因為 HashSet 的查找平均時間複雜度是 `O(1)`，接著只要從 `0` 一路檢查到 `n`，看哪個數字不在集合裡即可。

這個方法的思考方式是：

- 題目給我的數字，我先全部記住。
- 完整範圍中每個可能答案，我再逐一確認是否存在。
- 第一個不存在的值，就是答案。

和排序法相比，HashSet 不需要重新排列資料，因此時間複雜度可以壓到 `O(n)`，但需要額外 `O(n)` 的集合空間。

### 範例演示流程

以 `nums = [0, 1]` 為例：

1. 建立集合：`{0, 1}`
2. 檢查 `0`，集合內有，繼續
3. 檢查 `1`，集合內有，繼續
4. 檢查 `2`，集合內沒有
5. 因此答案就是 `2`

這個方法非常適合作為「先求正確、再優化」的版本，因為邏輯清楚、除錯也方便。

## 解法三：位元 XOR 抵消

### 設計說明

這個方法利用 XOR 的三個關鍵特性：

- `x ^ x = 0`
- `x ^ 0 = x`
- XOR 符合交換律與結合律

如果把陣列中的所有值，與完整範圍 `[0, n]` 中的所有值全部做 XOR：

- 有出現的數字會在「陣列」與「完整範圍」中各出現一次
- 這些成對的數字會互相抵消成 `0`
- 最後只剩下那個「完整範圍中存在，但陣列中缺少」的數字

這個方法不需要排序，也不需要額外集合，是三種方法中空間效率最好的做法。

### 範例演示流程

以 `nums = [3, 0, 1]` 為例：

1. 先 XOR 陣列值：`0 ^ 3 ^ 0 ^ 1`
2. 再 XOR 完整範圍：`^ 0 ^ 1 ^ 2 ^ 3`
3. 重新排列後可以寫成：`(3 ^ 3) ^ (0 ^ 0 ^ 0) ^ (1 ^ 1) ^ 2`
4. 成對抵消後，只剩下 `2`
5. 因此答案就是 `2`

如果想要在面試或實戰中展示位元運算思維，這通常是很好的版本。

## 專案中的可執行示例

`Main` 目前會固定執行以下五組案例：

- `[3, 0, 1] -> 2`
- `[0, 1] -> 2`
- `[9, 6, 4, 2, 3, 5, 7, 0, 1] -> 8`
- `[0] -> 1`
- `[1] -> 0`

每組案例都會輸出：

- 原始輸入
- 預期答案
- 三種解法各自的結果
- `PASS/FAIL` 驗證狀態

## 建置與執行

請在此目錄作為 repository root 的情況下執行：

```powershell
dotnet build leetcode_268/leetcode_268.csproj
dotnet run --project leetcode_268/leetcode_268.csproj
```

## 範例輸出

以下輸出對應目前 `Program.cs` 的實際示例流程：

```text
LeetCode 268 - Missing Number
================================

Case 1
Input: [3, 0, 1]
Expected: 2
MissingNumber  (Sort)   : 2 PASS
MissingNumber2 (HashSet): 2 PASS
MissingNumber3 (XOR)    : 2 PASS

Case 2
Input: [0, 1]
Expected: 2
MissingNumber  (Sort)   : 2 PASS
MissingNumber2 (HashSet): 2 PASS
MissingNumber3 (XOR)    : 2 PASS

Case 3
Input: [9, 6, 4, 2, 3, 5, 7, 0, 1]
Expected: 8
MissingNumber  (Sort)   : 8 PASS
MissingNumber2 (HashSet): 8 PASS
MissingNumber3 (XOR)    : 8 PASS

Case 4
Input: [0]
Expected: 1
MissingNumber  (Sort)   : 1 PASS
MissingNumber2 (HashSet): 1 PASS
MissingNumber3 (XOR)    : 1 PASS

Case 5
Input: [1]
Expected: 0
MissingNumber  (Sort)   : 0 PASS
MissingNumber2 (HashSet): 0 PASS
MissingNumber3 (XOR)    : 0 PASS
```

## 專案結構

```text
.
├─ README.md
├─ docs/
│  └─ readme-template.md
└─ leetcode_268/
   ├─ Program.cs
   └─ leetcode_268.csproj
```

## 驗證重點

- `dotnet build` 應成功完成且無錯誤。
- `dotnet run` 應輸出五組案例，且三種解法都顯示 `PASS`。
- `MissingNumber2` 已移除重複將輸入加入 `HashSet` 的冗餘迴圈，讓實作與文件說明一致。
