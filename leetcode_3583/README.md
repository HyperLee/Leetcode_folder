# leetcode_3583 — Count Special Triplets (3583. Count Special Triplets)

> 題目來源：LeetCode（3583. Count Special Triplets）
> 原題連結：[LeetCode - Count Special Triplets](https://leetcode.com/problems/count-special-triplets/)
> 題目中文：[LeetCode 中文 - Count Special Triplets](https://leetcode.cn/problems/count-special-triplets/)

## 目錄

- [leetcode\_3583 — Count Special Triplets (3583. Count Special Triplets)](#leetcode_3583--count-special-triplets-3583-count-special-triplets)
  - [目錄](#目錄)
  - [題目描述](#題目描述)
  - [核心想法（概覽）](#核心想法概覽)
  - [解法（兩種實作）](#解法兩種實作)
    - [方法一：陣列計數](#方法一陣列計數)
    - [方法二：Dictionary（哈希表）](#方法二dictionary哈希表)
  - [範例說明](#範例說明)
  - [複雜度分析](#複雜度分析)
  - [程式碼執行與測試（.NET）](#程式碼執行與測試net)
  - [注意事項與優化建議](#注意事項與優化建議)
  - [來源與參考](#來源與參考)

---

## 題目描述

給你一個整數陣列 `nums`。

一個「特殊三元組」被定義為索引三元組 `(i, j, k)`，使得：

- 0 <= i < j < k < n
- `nums[i] == nums[j] * 2`
- `nums[k] == nums[j] * 2`

請回傳陣列中所有特殊三元組的數量，答案可能很大，請對 `10^9 + 7` 取模。

---

## 核心想法（概覽）

以中間位置 `j` 作為枚舉目標。對於每個 `j`，

- 計算左側（索引 < j）中等於 `nums[j] * 2` 的次數 L。
- 計算右側（索引 > j）中等於 `nums[j] * 2` 的次數 R。

對每個 `j`，貢獻為 L * R，整體答案為所有 `j` 的總和（模 1e9+7）。

---

## 解法（兩種實作）

下面將兩個常見做法並列，並給出應用建議與實作重點：

### 方法一：陣列計數

前提：`nums` 的值為非負且最大值 `mx` 在可接受記憶體範圍內（可以直接建立大小為 mx+1 的陣列）。

步驟概述：

1. 若 `nums == null` 或 `nums.Length < 3`，回傳 0。
2. 用 `rightCount`（陣列）統計整個陣列的出現次數。
3. 建立 `leftCount`（初始為 0）。
4. 遍歷每個 `j`：
   - `rightCount[nums[j]]--`。
   - `target = nums[j] * 2`（確認 target 在陣列索引範圍內）：
     - `ans += (long)leftCount[target] * rightCount[target]`；並對 MOD 取模。
   - `leftCount[nums[j]]++`。
5. 回傳 `ans % MOD`。

---

### 方法二：Dictionary（哈希表）

前提：`nums` 可能包含負數或值域很大，建立大陣列不切實際。

步驟概述：

1. 若 `nums == null` 或 `nums.Length < 3`，回傳 0。
2. 建立 `left` 與 `right` 為 `Dictionary<int, long>`。
3. 將整個陣列統計到 `right`。
4. 遍歷每個 `j`（v = nums[j]）：
   - `right[v]--`（若為 0 則移除鍵）。
   - `targetLong = (long)v * 2`，確認在 `int` 範圍內，轉為 `int target`。
   - 若 `left.TryGetValue(target, out leftCnt)` 和 `right.TryGetValue(target, out rightCnt)`，則 `ans += leftCnt * rightCnt`。
   - `left[v]++`。
5. 回傳 `ans % MOD`。

---

## 範例說明

輸入：`nums = [2, 1, 2]`。

答案：`1`（因為以 `j=1` 作為中間的 L*R = 1）。

---

## 複雜度分析

- 時間複雜度：
  - 方法一（陣列）：O(n + m)，m 為最大值 mx（建立陣列成本）。
  - 方法二（Dictionary）：平均 O(n)（哈希查詢），最壞情況視哈希退化而定。
- 空間複雜度：
  - 方法一：O(m)。
  - 方法二：O(u)，u 為獨特值數量。

---

## 程式碼執行與測試（.NET）

```pwsh
cd leetcode_3583
dotnet build
dotnet run --project leetcode_3583.csproj
```

---

## 注意事項與優化建議

- 若 `nums` 值域為非負且 mx 合理（可建立陣列），選方法一（效能最好）。
- 若 `nums` 有負數或值域極大，選方法二（避免巨量記憶體）。
- 使用 `long` 儲存次數與乘法結果，避免溢位；在必要時對 `ans` 取模。
- 若輸入值極為稀疏，考慮座標壓縮 (coordinate compression)。

---

## 來源與參考

- LeetCode 題目：3583. Count Special Triplets

---

如需，我可以替 `Program.cs` 加上更多測試案例、範例輸出說明或程式碼注釋。
