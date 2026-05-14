# LeetCode 19 - Remove Nth Node From End of List

使用 C# 與 .NET 10 實作 LeetCode 第 19 題，主體解法採用雙指針，在一次走訪中移除 linked list 的倒數第 `n` 個節點。專案同時提供可直接執行的主程式示例，方便對照輸入與輸出。

## 題目說明

給定一個 singly linked list 的 `head`，請刪除倒數第 `n` 個節點，並回傳刪除後的 linked list 頭節點。

### 限制條件

- linked list 節點數量介於 `1` 到 `30`
- 節點值介於 `0` 到 `100`
- `1 <= n <= sz`

## 專案重點

- 主程式位置: `leetcode_019/Program.cs`
- 目前實作解法: 雙指針
- 主程式提供 4 組可直接執行的示例資料

## 解題概念與出發點

題目的關鍵不是「找到倒數第 `n` 個節點本身」，而是「找到它的前一個節點」，因為真正的刪除動作是把前一個節點的 `next` 直接跨過目標節點。

雙指針的核心想法如下：

1. 讓前指針 `first` 先走 `n` 步。
2. 讓後指針 `second` 從 `dummy` 節點開始，和 `first` 一起往前。
3. 當 `first` 走到結尾時，`second` 會剛好停在待刪節點的前一格。
4. 把 `second.next` 改成 `second.next.next`，即可完成刪除。

之所以加入 `dummy` 節點，是因為當要刪除的節點剛好是原本的 `head` 時，也能沿用相同邏輯，不需要再拆出特殊判斷。

## 解法設計說明

### 解法一: 雙指針

**設計目的**

在不額外計算 linked list 長度的前提下，直接用一次走訪完成定位與刪除。

**流程**

1. 建立 `dummy -> head`
2. `first` 先前進 `n` 步
3. `first` 與 `second` 同步前進
4. `first == null` 時，`second.next` 就是目標節點
5. 將目標節點略過後回傳 `dummy.next`

**為什麼這樣設計**

- 能把時間複雜度維持在 `O(L)`，其中 `L` 是 linked list 長度
- 額外空間只使用固定數量指標，空間複雜度為 `O(1)`
- `dummy` 節點讓刪除頭節點的情況與一般情況共用同一條邏輯

**複雜度**

- 時間複雜度: `O(L)`
- 空間複雜度: `O(1)`

## 範例演示流程

### 範例 1

輸入:

```text
head = [1, 2, 3, 4, 5], n = 2
```

流程:

1. `first` 先走 2 步，停在值 `3`
2. `second` 從 `dummy` 出發，和 `first` 同步前進
3. 當 `first` 走到 `null` 時，`second` 停在值 `3`
4. `second.next` 原本是值 `4`，改接到值 `5`

輸出:

```text
[1, 2, 3, 5]
```

### 範例 2

輸入:

```text
head = [1], n = 1
```

流程:

1. `first` 先走 1 步後變成 `null`
2. `second` 仍停在 `dummy`
3. 直接把原本的 `head` 略過

輸出:

```text
[]
```

### 範例 3

輸入:

```text
head = [1, 2], n = 2
```

流程:

1. `first` 先走 2 步後變成 `null`
2. `second` 停在 `dummy`
3. 刪除原本的第一個節點 `1`

輸出:

```text
[2]
```

## 執行方式

在專案根目錄執行:

```powershell
dotnet build leetcode_019/leetcode_019.csproj
dotnet run --project leetcode_019/leetcode_019.csproj
```

## 主程式示例輸出

以下為目前 `Main` 內建示例執行結果:

```text
LeetCode 19 - Remove Nth Node From End of List
Case 1
Input : head = [1, 2, 3, 4, 5], n = 2
Output: [1, 2, 3, 5]

Case 2
Input : head = [1], n = 1
Output: []

Case 3
Input : head = [1, 2], n = 1
Output: [1]

Case 4
Input : head = [1, 2], n = 2
Output: [2]
```

## 專案結構

```text
leetcode_019/
├─ docs/
│  └─ readme-template.md
├─ leetcode_019/
│  ├─ Program.cs
│  └─ leetcode_019.csproj
└─ leetcode_019.slnx
```
