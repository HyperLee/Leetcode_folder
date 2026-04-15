# LeetCode 2515

## Shortest Distance to Target String in a Circular Array

以 C# 實作環狀陣列最短距離問題，核心做法是一次線性遍歷，對每個符合 target 的位置同時計算「直接走」與「繞一圈走」兩種距離，最後取全域最小值。

[題目說明](#題目說明) • [解題概念與出發點](#解題概念與出發點) • [解法詳解](#解法詳解) • [流程示範](#流程示範) • [複雜度](#複雜度) • [執行方式](#執行方式)

> [!NOTE]
> 這題的關鍵不是真的去模擬往左走或往右走，而是先把兩種可能距離寫清楚：
>
> - 直接距離 = |i - startIndex|
> - 繞環距離 = n - |i - startIndex|
> - 兩者取最小值後，再和目前答案比較

## 題目說明

給定一個 0-indexed 的環狀字串陣列 words、一個目標字串 target，以及起點 startIndex。

- 你每一步可以往左或往右移動一格。
- 陣列是環狀的，代表尾端與開頭相連。
- 目標是找出從 startIndex 出發，到任一個 target 所需的最短步數。
- 如果陣列中根本沒有 target，答案就是 -1。

這題的本質，是在環狀陣列中比較兩點之間的最短距離。

## 解題概念與出發點

假設目前掃描到索引 i，且 words[i] 恰好等於 target。

從 startIndex 到 i 會有兩條可行路徑：

1. 不跨越首尾，直接走過去，距離是 |i - startIndex|。
2. 反方向繞過陣列首尾，距離是 n - |i - startIndex|。

因此，對每一個符合條件的索引 i，我們只要計算：

min(|i - startIndex|, n - |i - startIndex|)

接著把所有符合 target 的位置都掃過一次，持續更新最小值即可。

> [!TIP]
> 看到環狀陣列時，先把問題拆成「直接走」與「繞一圈走」兩種距離，通常就能把題目還原成一般的距離比較問題。

## 解法詳解

整體步驟如下：

1. 先用變數 minDistance 記錄目前最小答案，初始值設為 int.MaxValue。
2. 從左到右遍歷整個 words 陣列。
3. 如果目前 words[i] 不是 target，就直接略過。
4. 如果 words[i] 等於 target，就計算：
   - directDistance = |i - startIndex|
   - wrappedDistance = n - directDistance
5. 用兩者較小值更新 minDistance。
6. 遍歷結束後，如果 minDistance 仍然沒有被更新，表示 target 不存在，回傳 -1；否則回傳 minDistance。

這個方法不需要額外資料結構，也不需要真的模擬往左或往右一步一步走，因為最短距離已經可以直接由公式得到。

## 流程示範

以下用一組代表性測資示範：

```text
words = ["hello", "i", "am", "leetcode", "hello"]
target = "hello"
startIndex = 1
n = 5
```

### 掃描過程

| 索引 i | words[i] | 是否命中 target | 直接距離 | 繞環距離 | 本次最短 | 目前答案 |
| --- | --- | --- | --- | --- | --- | --- |
| 0 | hello | 是 | 1 | 4 | 1 | 1 |
| 1 | i | 否 | - | - | - | 1 |
| 2 | am | 否 | - | - | - | 1 |
| 3 | leetcode | 否 | - | - | - | 1 |
| 4 | hello | 是 | 3 | 2 | 2 | 1 |

最後答案為 1。

原因是從索引 1 往左走一步就能到索引 0 的 hello，不需要繞更遠的路。

### 找不到 target 的情況

如果：

```text
words = ["x", "y", "z"]
target = "hello"
startIndex = 1
```

整個遍歷過程都不會命中 target，minDistance 會維持 int.MaxValue，最後回傳 -1。

## 複雜度

- 時間複雜度：O(n)
- 空間複雜度：O(1)

其中 n 是 words 的長度。

## 專案內容

| 路徑 | 說明 |
| --- | --- |
| leetcode_2515/Program.cs | 題目解法與 Main 內建測試資料 |
| leetcode_2515/leetcode_2515.csproj | .NET 10 主控台專案設定 |

## 執行方式

在專案根目錄執行：

```bash
dotnet build leetcode_2515/leetcode_2515.csproj
dotnet run --project leetcode_2515/leetcode_2515.csproj
```

程式會輸出 Main 中內建的測試資料，方便快速確認：

- 一般情況
- 需要考慮環狀繞行的情況
- target 不存在的情況
