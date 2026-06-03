# LeetCode 129 - Sum Root to Leaf Numbers

使用 C# 實作 LeetCode 129，並在主程式中同時展示 `DFS` 與 `BFS` 兩種解法。專案目前把可執行測資直接放在 [`leetcode_129/Program.cs`](/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_129/leetcode_129/Program.cs)，執行 `dotnet run` 就能看到每個案例的預期值、兩種解法結果與比對結果。

## 題目說明

- 題目連結: <https://leetcode.com/problems/sum-root-to-leaf-numbers/>
- 題目中文: <https://leetcode.cn/problems/sum-root-to-leaf-numbers/description/>

給定一棵二元樹，節點值只會是 `0` 到 `9` 的單一數字。從根節點一路走到葉節點時，沿途的數字會依照十進位串接成一個整數。

例如路徑 `1 -> 2 -> 3` 代表數字 `123`。

題目要求回傳整棵樹所有「根到葉」路徑所形成數字的總和。

## 限制條件

根據 LeetCode 原題：

- 節點數量介於 `1` 到 `1000`
- `0 <= Node.val <= 9`
- 樹的深度不超過 `10`
- 題目保證答案落在 32 位元整數範圍內

> [!NOTE]
> 原題節點數下限是 `1`，但這個 Console 專案額外示範了 `root = []` 的空樹案例，方便本地驗證 `null` 輸入時回傳 `0` 的行為。

## 解題概念與出發點

這題的關鍵不是把每條路徑真的轉成字串，而是觀察「走到下一層」時，原本的數字只需要左移一位，也就是乘上 `10`，再加上目前節點值。

假設目前已經走出數字 `49`，下一個節點值是 `5`，那麼新的路徑數字就是：

```text
49 * 10 + 5 = 495
```

因此整題可以轉成：

1. 走訪所有從根到葉的路徑。
2. 在走訪過程中同步維護「目前這條路徑代表的數字」。
3. 只有遇到葉節點時，才把完整路徑值加進答案。

這個觀察同時適用於 DFS 與 BFS，差別只在於：

- `DFS` 用遞迴把同一路徑一路走到底。
- `BFS` 用佇列保存「節點 + 目前路徑值」，按層擴展。

## 解法一：DFS 深度優先搜尋

### 設計說明

[`Program.SumNumbersDfs`](/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_129/leetcode_129/Program.cs) 會呼叫私有 helper `CalculateDepthFirstSum(node, currentValue)`。

設計重點如下：

1. `currentValue` 代表從根節點走到父節點時，已經組出來的十進位數字。
2. 進入目前節點後，立刻用 `currentValue * 10 + node.Val` 算出新的路徑值。
3. 如果目前節點是葉節點，表示這條路徑已經完整，可以直接回傳該數字。
4. 如果不是葉節點，就把左子樹與右子樹各自算出的總和相加。
5. 如果節點為 `null`，直接回傳 `0`，讓遞迴加總邏輯保持單純。

這種寫法的優點是邏輯直接，與題意幾乎一一對應。每次遞迴都只關心目前節點、目前路徑值，以及左右子樹的結果。

### 為什麼這樣設計

- 不需要字串拼接，也不需要在葉節點時回頭重建路徑。
- 每個節點只會進入一次，遞迴狀態只需要保存當前數值。
- `null` 回傳 `0` 後，左右子樹總和可以自然相加，不需要額外特殊分支。

### 複雜度

- 時間複雜度: `O(n)`，每個節點只拜訪一次。
- 空間複雜度: `O(h)`，來自遞迴呼叫堆疊；`h` 為樹高。

### 範例流程

以 `root = [4,9,0,5,1]` 為例：

```text
        4
      /   \
     9     0
    / \
   5   1
```

遞迴展開流程如下：

1. 走到 `4`，目前值從 `0` 變成 `4`
2. 往左走到 `9`，目前值從 `4` 變成 `49`
3. 往左走到 `5`，目前值從 `49` 變成 `495`
4. `5` 是葉節點，回傳 `495`
5. 回到 `9`，改走右邊到 `1`，目前值從 `49` 變成 `491`
6. `1` 是葉節點，回傳 `491`
7. 回到 `4`，改走右邊到 `0`，目前值從 `4` 變成 `40`
8. `0` 是葉節點，回傳 `40`
9. 全部加總得到 `495 + 491 + 40 = 1026`

## 解法二：BFS 廣度優先搜尋

### 設計說明

[`Program.SumNumbersBfs`](/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_129/leetcode_129/Program.cs) 使用佇列保存一組狀態：

```text
(目前節點, 走到目前節點時的路徑數字)
```

設計重點如下：

1. 起點把 `(root, root.Val)` 放進佇列。
2. 每次出佇列一筆狀態。
3. 如果該節點是葉節點，就把目前路徑值加進總和。
4. 如果有左子節點或右子節點，就把新的數字 `currentValue * 10 + child.Val` 一起入佇列。
5. 佇列清空後，代表所有路徑都處理完，總和就是答案。

這個做法把遞迴狀態顯式化。DFS 把狀態放在 call stack，BFS 則是把狀態放在 queue。

### 為什麼這樣設計

- 能明確看出「節點」與「目前數字」是一起前進的。
- 如果日後想把題目延伸成逐層觀察、列印或除錯，BFS 的狀態可視性更高。
- 對不想使用遞迴的人來說，這是同樣直觀而且穩定的替代方案。

### 複雜度

- 時間複雜度: `O(n)`，每個節點仍然只處理一次。
- 空間複雜度: `O(w)`，`w` 為同一層可能同時存在於佇列中的最大節點數。

### 範例流程

同樣以 `root = [4,9,0,5,1]` 為例，佇列狀態變化如下：

1. 初始入佇列：`[(4, 4)]`
2. 取出 `(4, 4)`，不是葉節點  
   新增左子節點 `(9, 49)`，新增右子節點 `(0, 40)`
3. 佇列變成：`[(9, 49), (0, 40)]`
4. 取出 `(9, 49)`，不是葉節點  
   新增 `(5, 495)`、`(1, 491)`
5. 佇列變成：`[(0, 40), (5, 495), (1, 491)]`
6. 取出 `(0, 40)`，它是葉節點，總和變成 `40`
7. 取出 `(5, 495)`，它是葉節點，總和變成 `535`
8. 取出 `(1, 491)`，它是葉節點，總和變成 `1026`
9. 佇列清空，結束

## 兩種解法的差異

| 面向 | DFS | BFS |
| --- | --- | --- |
| 狀態保存位置 | 遞迴呼叫堆疊 | 顯式佇列 |
| 寫法風格 | 簡潔、貼近題意 | 狀態明確、容易觀察 |
| 空間主因 | 樹高 `h` | 最大層寬 `w` |
| 適合情境 | 一般樹遞迴題 | 想避免遞迴或要看逐層狀態 |

在這個專案中，`SumNumbers(root)` 預設委派給 `SumNumbersDfs(root)`，同時保留 `SumNumbersBfs(root)` 作為對照與學習版本。

## 可執行範例資料

主程式目前內建 4 組測資：

1. `root = [1,2,3]`
2. `root = [4,9,0,5,1]`
3. `root = [1,0,4,5]`
4. `root = []`

每個案例都會輸出：

- 案例名稱
- 輸入表示法
- 路徑組成說明
- 預期答案
- DFS 結果
- BFS 結果
- 是否通過比對

## 建置與執行

### 建置

```bash
dotnet build leetcode_129/leetcode_129.csproj
```

本地驗證結果：

```text
Build succeeded.
    0 Warning(s)
    0 Error(s)
```

### 執行範例

```bash
dotnet run --project leetcode_129/leetcode_129.csproj
```

> [!NOTE]
> 在這台機器上執行 `dotnet` 時，SDK 額外印出了 `CSSM_ModuleLoad(): One or more parameters passed to a function were not valid.`。這不是本專案邏輯輸出，也不影響建置成功或演算法結果。

主程式輸出如下：

```text
LeetCode 129 - Sum Root to Leaf Numbers
======================================
[Example 1]
Input: root = [1,2,3]
Paths: 12 + 13 = 25
Expected: 25
DFS Result: 25
BFS Result: 25
Pass: PASS

[Example 2]
Input: root = [4,9,0,5,1]
Paths: 495 + 491 + 40 = 1026
Expected: 1026
DFS Result: 1026
BFS Result: 1026
Pass: PASS

[Zero In Path]
Input: root = [1,0,4,5]
Paths: 105 + 14 = 119
Expected: 119
DFS Result: 119
BFS Result: 119
Pass: PASS

[Empty Tree]
Input: root = []
Paths: none, total = 0
Expected: 0
DFS Result: 0
BFS Result: 0
Pass: PASS
```

### 測試現況

目前專案沒有獨立的 `xUnit` / `NUnit` 測試專案，因此這份題解以主程式內建案例作為可執行驗證資料。若後續新增正式測試專案，再補充 `dotnet test` 指令即可。

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_129/
    ├── Program.cs
    └── leetcode_129.csproj
```

## 這份實作整理了什麼

- 保留題目描述 XML 註解，不改動原始題意內容
- 為主要方法補齊 XML `summary`
- 在關鍵演算法處加入必要註解
- 同時提供 DFS 與 BFS 解法
- 在 `Main` 中加入可直接執行的範例測資
- 讓 README 與實際執行輸出保持一致
