# LeetCode 234 - Palindrome Linked List

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![C%23](https://img.shields.io/badge/language-C%23-239120)

這是一個 C#/.NET console 專案，整理 LeetCode 234「Palindrome Linked List」的兩種解法，並在 `Main` 中提供可直接執行的範例資料。

## 題目說明

給定一個單向連結串列 `head`，判斷節點值由前往後讀取，是否與由後往前讀取完全相同。若相同，回傳 `true`；否則回傳 `false`。

範例：

```text
Input:  head = [1, 2, 2, 1]
Output: true

Input:  head = [1, 2]
Output: false
```

## 限制條件

- 節點數量範圍：`1 <= n <= 10^5`
- 節點值範圍：`0 <= Node.val <= 9`
- 單向連結串列只能從目前節點走到下一個節點，不能直接由尾端往前走。
- 本專案的解法方法額外接受 `null` 作為防禦性輸入，並將空串列視為回文。
- LeetCode 進階目標會詢問能否做到 `O(n)` 時間與 `O(1)` 額外空間；本專案目前示範的是遞迴與陣列雙指針版本，兩者額外空間皆為 `O(n)`。

## 解題概念與出發點

回文判斷本質上需要比較「最左」與「最右」的值，但單向連結串列無法從尾端往前移動。因此目前提供兩種做法：

1. 用遞迴呼叫堆疊模擬右側指標由尾端往前走，同時保留一個左側指標由頭往後走。
2. 先把所有節點值複製到 `List<int>`，再用陣列索引從頭尾往中間比較。

## 解法一：遞迴模擬前後雙指針

實作位置：`Program.IsPalindrome` 與私有方法 `CompareFromTail`

設計說明：

- `IsPalindrome` 是公開入口，每次呼叫都會重置 `realHead` 與 `isCrossed`，避免連續測試案例互相污染狀態。
- `CompareFromTail` 先遞迴走到尾端；遞迴回傳時，當前節點就等同右側指標，由右往左移動。
- `realHead` 從串列頭端開始，於每次比較後往右移動。
- 當左右指標相遇或交錯時，代表已經比到中間，可以停止後續比較。

複雜度：

- 時間：`O(n)`
- 額外空間：`O(n)`，來自遞迴呼叫堆疊。

範例演示流程：

```text
head = [1, 2, 2, 1]

1. realHead 指向第一個 1，遞迴一路走到最後一個 1。
2. 回傳時比較：右側 1 vs 左側 1，相同，realHead 移到第一個 2。
3. 繼續比較：右側 2 vs 左側 2，相同，左右指標交錯。
4. 中間以前的值都已一致，回傳 true。
```

非回文流程：

```text
head = [1, 2]

1. 遞迴走到尾端 2。
2. 回傳時比較：右側 2 vs 左側 1，不相同。
3. 立即保留 false 結果並往上回傳。
```

## 解法二：複製到 List 後雙指針

實作位置：`Program.IsPalindrome2`

設計說明：

- 先走訪整個連結串列，將每個節點值依序加入 `List<int>`。
- 使用 `front` 指向陣列開頭，`back` 指向陣列尾端。
- 每一輪比較 `values[front]` 與 `values[back]`。
- 若任一輪不同，直接回傳 `false`；若雙指針完成交會，回傳 `true`。

複雜度：

- 時間：`O(n)`
- 額外空間：`O(n)`，來自儲存所有節點值的 `List<int>`。

範例演示流程：

```text
head = [1, 2, 2, 1]
values = [1, 2, 2, 1]

1. front = 0, back = 3，比較 1 與 1，相同。
2. front = 1, back = 2，比較 2 與 2，相同。
3. front 與 back 交會，所有頭尾值一致，回傳 true。
```

```text
head = [1, 2]
values = [1, 2]

1. front = 0, back = 1，比較 1 與 2，不同。
2. 回傳 false。
```

## 執行方式

從 repository 根目錄執行：

```bash
dotnet restore leetcode_234.sln
dotnet build leetcode_234.sln
dotnet run --project leetcode_234/leetcode_234.csproj
```

在本機 macOS 環境執行 `dotnet` 命令時，可能會額外看到 `CSSM_ModuleLoad()` 訊息；目前命令仍以 exit code `0` 完成。`Main` 會執行四組範例資料，程式輸出如下：

```text
Palindrome Linked List sample checks

Case 1 - even palindrome: [1 -> 2 -> 2 -> 1]
  Recursive compare: True (expected: True)
  List two-pointer:  True (expected: True)
  Status: PASS

Case 2 - not palindrome: [1 -> 2]
  Recursive compare: False (expected: False)
  List two-pointer:  False (expected: False)
  Status: PASS

Case 3 - odd palindrome: [1 -> 2 -> 3 -> 2 -> 1]
  Recursive compare: True (expected: True)
  List two-pointer:  True (expected: True)
  Status: PASS

Case 4 - single node: [1]
  Recursive compare: True (expected: True)
  List two-pointer:  True (expected: True)
  Status: PASS
```

> [!NOTE]
> 目前尚未建立測試專案，因此驗證以 `dotnet build` 與 `dotnet run` 為主。新增測試專案後，可使用 `dotnet test leetcode_234.sln`。

## 專案結構

```text
.
├── leetcode_234.sln
├── leetcode_234/
│   ├── Program.cs
│   └── leetcode_234.csproj
└── docs/
    └── readme-template.md
```
