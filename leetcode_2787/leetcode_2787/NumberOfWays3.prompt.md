---
description: New prompt created from chat session
mode: agent
tools: ['codebase', 'usages', 'vscodeAPI', 'problems', 'changes', 'testFailure', 'terminalSelection', 'terminalLastCommand', 'fetch', 'findTestFiles', 'searchResults', 'githubRepo', 'extensions', 'runTests', 'editFiles', 'runNotebooks', 'search', 'new', 'runTasks']
model: GPT-4.1
---
<user>
#sym:NumberOfWays3  詳細解釋
</user>
<assistant>
以下是 `NumberOfWays3` 函式的詳細中文解釋：

---

### 函式功能

`NumberOfWays3(int n, int x)` 用來計算「將 n 表示為若干個不同正整數的 x 次冪之和」的所有方案數，並對 10^9+7 取模。

---

### 解題思路

這題本質是「0-1 背包問題」的變形：

- 背包容量：n
- 物品：所有 1^x, 2^x, 3^x, ...，且每個物品只能選一次（互不相同）
- 問題：有幾種方式選取若干物品，使得總和剛好等於 n

---

### 主要變數

- `MOD`：取模常數 10^9+7，避免數字溢位
- `dp[j]`：和為 j 的方案數（只用一維陣列，空間優化）

---

### 運作流程

1. **初始化**
   - `dp[0] = 1`：和為 0 的方案只有一種（什麼都不選）

2. **枚舉所有可選數字 i**
   - 從 1 開始，計算 `val = i^x`
   - 如果 `val > n`，代表後面都不可能湊出 n，直接跳出

3. **倒序更新 dp 陣列**
   - 倒序枚舉 j（從 n 到 val）
   - 狀態轉移：`dp[j] = (dp[j] + dp[j - val]) % MOD`
     - 意思是：如果已經有「和為 j-val」的方案，現在加上 i^x 這個數字，就能組成「和為 j」的新方案

4. **回傳答案**
   - `dp[n]` 就是最終答案

---

### 為什麼要倒序？

倒序是 0-1 背包的標準寫法，確保每個數字只被選一次。如果用正序，會導致一個數字被重複選取。

---

### 範例

以 `n=10, x=2` 為例：

- 可選數字的平方：1, 4, 9
- 只有一種組合：1+9=10

---

### 時間複雜度

- 外層 i 最多到 n 的 x 次方根
- 內層 j 從 n 到 val
- 整體複雜度約 O(n * 根號n)（x=2 時）

---

### 註解

- 這種寫法是經典的「空間優化 0-1 背包」。
- 只需一維 dp 陣列，且每次倒序更新，確保每個數字只用一次。

---

如需更深入的數學推導或程式碼細節，歡迎再追問！
</assistant>