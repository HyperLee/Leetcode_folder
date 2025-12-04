# leetcode_2211 (Count Collisions on a Road)

📌 **題目背景 (2211. Count Collisions on a Road)**

在一條無限長的道路上有 n 輛車，依序編號 0 到 n-1。每輛車的位置唯一，你會得到一個長度為 n 的字串 `directions`，其中 `directions[i]` 可為 `'L'`、`'R'` 或 `'S'`，代表該車向左、向右或靜止（速度相同）。碰撞規則如下：

- 兩輛相向移動車互撞 (R 與 L 碰撞) → 碰撞次數 +2
- 移動車撞靜止車 (R 或 L 與 S) → 碰撞次數 +1
- 發生碰撞後，碰撞參與車輛會變為靜止 (S) 並不再移動

目標：回傳所有碰撞發生後的總碰撞次數。

---

## 專案內容概述

- 語言：C# (.NET)
- 位置：`leetcode_2211/Program.cs`
- 內含兩個解法：`CountCollisions` 與 `CountCollisions2`，兩者皆為 O(n) 時間複雜度與 O(1) 空間複雜度。

---

## 建置與執行 (快速指南)

```powershell
# 在 workspace 根目錄下
dotnet build ./leetcode_2211.sln --configuration Debug
dotnet run --project ./leetcode_2211/leetcode_2211.csproj --configuration Debug
```

---

## 解法 1：去掉最左與最右無需碰撞的車（`CountCollisions`）

### 核心思路 🔧

- 左側連續 `L` 的車會一直往左離開，不會與其他車相撞；右側連續 `R` 的車會一直往右離開，不會與其他車相撞。
- 去除這些最左與最右的車後，在剩下的區間中，每一台非靜止車（`'R'` 或 `'L'`）最終都會發生碰撞（不一定與哪個車發生，但一定會停下）。因此，只要計算該區間內非 `'S'` 的數量即可得到總碰撞次數。

### 步驟摘要

1. 從左到右跳過所有最左連續的 `'L'`。
2. 從右到左跳過所有最右連續的 `'R'`。
3. 在保留下來的區間內，統計所有不是 `'S'` 的車（`'R'` 或 `'L'`）數量。該數量即為碰撞次數。

### 時間/空間複雜度

- 時間複雜度：O(n)
- 空間複雜度：O(1)

---

## 解法 2：單趟掃描與待處理右向車計數（`CountCollisions2`）

### 核心思路 💡

- 使用單個變數 `pendingRightCount` 追蹤目前還未處理的連續 `'R'` 的數量：
  - `-1` 表示目前沒有待處理的 `'R'`（或尚未開始）。
  - `>= 0` 表示有連續的 `'R'` 數量（待處理）。
- 依照讀到目前字元做不同處理：
  - 遇到 `'R'`：增加 `pendingRightCount`（開始或延續一段 `'R'`）。
  - 遇到 `'S'`：若 `pendingRightCount > 0`，代表所有這些 `'R'` 都會與 `'S'` 碰撞（每台 `'R'` +1），將該計數累加到結果，並把 `pendingRightCount` 設為 0。
  - 遇到 `'L'`：如果 `pendingRightCount >= 0`（代表有右向車在前面），則該 `'L'` 與所有前面的右向車發生碰撞，碰撞次數為 `pendingRightCount + 1`（前面每個 `'R'` +1，且這台 `'L'` 也與至少一輛車碰撞），將該值加入結果並把 `pendingRightCount` 設為 0。若 `pendingRightCount == -1`，則此 `'L'` 不會與前面的車碰撞。

### 時間/空間複雜度

- 時間複雜度：O(n)
- 空間複雜度：O(1)

---

## 範例推演（一次流程示範）

使用測資：`"RLRSLL"`，期望輸出：`5`。

### 方法 A：`CountCollisions` 的計算流程

1. 原字串：`R L R S L L`
2. 左側連續 `'L'` 數量：0（開頭不是 `'L'`），右側連續 `'R'` 數量：0（結尾不是 `'R'`）。所以保留整串。
3. 統計保留下來區段中非 `'S'` 的數量：`R(0) L(1) R(2) S(3) L(4) L(5)` → 非 `'S'` 為 5 個。
4. 結果：5。

### 方法 B：`CountCollisions2` 的單趟掃描流程（帶變數狀態）

初始化：`res = 0`，`pendingRightCount = -1`。

- 看到 `R`（index 0）→ `pendingRightCount` = 1
- 看到 `L`（index 1）→ 有 pendingRightCount（=1），因此 `res += 1 + 1 = 2`，並設 `pendingRightCount = 0`（總 res = 2）
- 看到 `R`（index 2）→ `pendingRightCount = 1`
- 看到 `S`（index 3）→ 有 pendingRightCount（=1），因此 `res += 1`（res = 3），`pendingRightCount = 0`
- 看到 `L`（index 4）→ pendingRightCount >= 0（=0），因此 `res += 0 + 1 = 1`（res = 4），`pendingRightCount = 0`
- 看到 `L`（index 5）→ 同上 `res += 0 + 1 = 1`（res = 5），`pendingRightCount = 0`

最後 `res = 5`。

此演算流程的結果與方法 A 相同，並且能在單趟遍歷下完成。

---

## 測試樣例

在 `Program.cs` 中已包含測資：

```csharp
var tests = new Dictionary<string, int>
{
    { "RLRSLL", 5 },
    { "LLRRS", 2 },
    { "SSSS", 0 }
};
```

你也可以自己嘗試其它輸入，例如：`R`、`L`、`RS`、`LR`、`RRSLL` 等。

---

## 參考與備註

- 題目連結（英文）：https://leetcode.com/problems/count-collisions-on-a-road/
- 題目連結（中文）：https://leetcode.cn/problems/count-collisions-on-a-road/

---

如果你想針對更多測資執行或觀察不同步驟的模擬結果，歡迎在 `Program.cs` 中稍做擴充或新增 Log。祝你編寫愉快！
