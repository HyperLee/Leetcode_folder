# LeetCode 1848

## Minimum Distance to the Target Element

以 C# 實作 LeetCode 第 1848 題，重點在於把題目條件拆成最直接的判斷流程：找出所有等於 target 的位置，並從中挑出最接近 start 的那一個。

> [!NOTE]
> 題目保證 target 一定存在於陣列中，因此解法可以專注在「如何找出最小距離」，不必額外處理找不到答案的分支。

[題目說明](https://leetcode.com/problems/minimum-distance-to-the-target-element/) • [專案程式碼](./leetcode_1848/Program.cs)

## 題目在問什麼

給定一個整數陣列 nums、一個目標值 target，還有一個起始索引 start。

你要在陣列中找出某個索引 i，使得：

- nums[i] == target
- abs(i - start) 最小

最後回傳這個最小距離。

換句話說，這題不是要找 target 第一次出現在哪裡，也不是要找 target 的值，而是要找「哪一個 target 所在位置，距離 start 最近」。

## 解題概念與出發點

一開始最重要的觀察是：

只要某個位置不是 target，它就不可能影響答案。

因此，我們根本不需要對整個陣列做複雜處理，也不需要排序、二分搜尋、額外陣列或雜湊表。這題真正需要比較的，只有每一個 target 出現位置和 start 之間的距離。

可以把問題改寫成：

1. 掃描整個陣列。
2. 遇到 target 時，計算目前索引與 start 的距離。
3. 持續保留最小距離。
4. 掃描結束後回傳結果。

這樣的出發點很直接，因為答案本質上就是「所有候選位置中的最小值」。只要每個候選位置都看過一次，就不會漏掉正確答案。

> [!TIP]
> 這題的核心不是「找到 target」，而是「比較每一個 target 候選位置到 start 的距離」。思路一旦轉成這個角度，線性掃描就是最自然的做法。

## 解法說明

目前專案採用線性掃描。

### 步驟 1：建立最小距離初始值

先用一個變數記錄目前找到的最小距離，初始值設成 int.MaxValue。這代表一開始還沒有找到任何有效答案。

### 步驟 2：從左到右掃描陣列

使用 for 迴圈逐一檢查每個索引。

### 步驟 3：只在遇到 target 時計算距離

如果 nums[index] 不等於 target，就直接跳過。

如果相等，表示這個位置是有效候選，接著計算：

```csharp
Math.Abs(index - start)
```

這就是目前候選位置與 start 的距離。

### 步驟 4：更新最小值

把新算出的距離和目前記錄的最小距離相比，保留較小者。

```csharp
minDistance = Math.Min(minDistance, Math.Abs(index - start));
```

### 步驟 5：回傳答案

整個陣列掃描完後，minDistance 就是所有候選位置中的最小距離，也就是題目答案。

## 為什麼這個解法成立

因為題目要求的是：

$$
\min\left(|i - start|\right) \quad \text{where} \quad nums[i] = target
$$

只要把所有滿足 nums[i] == target 的索引都檢查一遍，再從對應距離中取最小值，就一定能得到正確答案。

線性掃描剛好完整覆蓋所有候選位置，而且每個位置只看一次，因此既正確又簡潔。

## 複雜度

- 時間複雜度：O(n)
- 空間複雜度：O(1)

其中 n 是 nums 的長度。

## 範例流程演示

以這組資料為例：

```text
nums = [1, 2, 3, 4, 5]
target = 5
start = 3
```

掃描流程如下：

1. index = 0，nums[0] = 1，不是 target，跳過。
2. index = 1，nums[1] = 2，不是 target，跳過。
3. index = 2，nums[2] = 3，不是 target，跳過。
4. index = 3，nums[3] = 4，不是 target，跳過。
5. index = 4，nums[4] = 5，符合 target。
6. 計算距離：abs(4 - 3) = 1。
7. 更新最小距離為 1。
8. 掃描結束，回傳 1。

所以答案是：

```text
1
```

再看一個距離為 0 的案例：

```text
nums = [1]
target = 1
start = 0
```

因為 start 本身就是 target 所在位置，距離為：

$$
|0 - 0| = 0
$$

所以答案直接是 0。

## 如何執行

在專案根目錄執行：

```bash
dotnet run --project leetcode_1848/leetcode_1848.csproj
```

目前 Main 已內建幾組測試資料，執行後會直接輸出每組案例的 expected 與 actual 結果，方便快速驗證實作是否正確。