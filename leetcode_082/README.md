# LeetCode 82 - Remove Duplicates from Sorted List II

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/language-C%23-239120)
![Topic](https://img.shields.io/badge/topic-linked--list-blue)

這個專案使用 C# console app 示範 LeetCode 82「刪除排序鏈結串列中的重複元素 II」。
目標是從已排序的 linked list 中刪除所有出現超過一次的數值，只保留原始串列中完全不重複的節點。

## 題目連結

- [LeetCode - Remove Duplicates from Sorted List II](https://leetcode.com/problems/remove-duplicates-from-sorted-list-ii/description/)
- [LeetCode 中文站 - 删除排序链表中的重复元素 II](https://leetcode.cn/problems/remove-duplicates-from-sorted-list-ii/description/)

## 題目說明

給定一個已排序鏈結串列的頭節點 `head`，刪除所有具有重複數值的節點，只保留原始串列中數值不重複的節點。
回傳處理後仍為已排序的鏈結串列。

範例：

```text
Input:  head = [1,2,3,3,4,4,5]
Output: [1,2,5]

Input:  head = [1,1,1,2,3]
Output: [2,3]
```

## 限制條件

- 節點數量範圍是 `[0, 300]`。
- 節點值範圍是 `-100 <= Node.val <= 100`。
- 鏈結串列保證已依遞增順序排序。

## 解題概念與出發點

題目的關鍵條件是「鏈結串列已排序」。因此相同數值一定會連續出現，不需要額外的雜湊表記錄出現次數。
只要在走訪時辨識一整段相同值的節點，就能一次刪除該重複群組；沒有形成重複群組的節點則保留。

本專案保留兩種解法：

- `DeleteDuplicates`：使用目前節點與上一個保留節點，在原串列上透過值覆蓋與指標切斷處理重複群組。
- `DeleteDuplicates2`：使用 dummy node 與前驅指標，直接略過整段重複值節點。

兩種解法的時間複雜度都是 `O(n)`，額外空間複雜度都是 `O(1)`。

## 解法一：原地覆蓋與切斷

`DeleteDuplicates` 使用 `current` 掃描串列，`prev` 記錄上一個確認保留的節點，`hasDuplicate` 標記目前是否正在處理重複群組。

設計流程：

1. 若 `current.val == current.next.val`，代表遇到重複值，先把 `current.next` 指向下一個不同候選節點。
2. 若接著遇到不同值，且 `hasDuplicate` 為 `true`，代表 `current` 本身也屬於重複群組，要用下一個不同值覆蓋目前節點。
3. 若一路重複到尾端，透過 `prev.next = null` 切斷尾端重複群組。
4. 若整條串列都是重複值，回傳 `null`。

範例演示：

```text
Input: [1,2,3,3,4,4,5]

保留 1，保留 2
遇到 3,3，移除整段 3
遇到 4,4，移除整段 4
保留 5

Output: [1,2,5]
```

```text
Input: [1,1,1,2,3]

開頭 1,1,1 是重複群組
移除所有 1
保留 2，保留 3

Output: [2,3]
```

## 解法二：Dummy Node 與前驅指標

`DeleteDuplicates2` 在串列前方新增 dummy node，讓刪除開頭重複群組時不需要額外處理 head。
`cur` 永遠停在「待判斷區段的前一個節點」。

設計流程：

1. 若 `cur.next.val == cur.next.next.val`，記下重複值 `duplicateValue`。
2. 持續把 `cur.next` 往後接，直到 `cur.next` 為空或值不等於 `duplicateValue`。
3. 若 `cur.next` 與 `cur.next.next` 不同，代表 `cur.next` 是唯一值，`cur` 才往前走。
4. 最後回傳 `dummy.next`。

範例演示：

```text
Input: [1,2,3,3,4,4,5]

dummy -> 1 -> 2 -> 3 -> 3 -> 4 -> 4 -> 5
cur 從 dummy 前進到 2
cur.next 偵測到 3,3，略過所有 3
cur.next 偵測到 4,4，略過所有 4
回傳 dummy.next

Output: [1,2,5]
```

```text
Input: [1,1]

dummy -> 1 -> 1
cur 停在 dummy
cur.next 偵測到 1,1，略過所有 1
dummy.next 變成 null

Output: []
```

## 執行方式

需求：

- .NET SDK 10.0 或相容版本

建置專案：

```bash
dotnet build leetcode_082.sln
```

本機驗證結果：

```text
建置成功。
    0 個警告
    0 個錯誤
```

執行範例：

```bash
dotnet run --project leetcode_082/leetcode_082.csproj
```

目前範例輸出：

```text
LeetCode 82 - Remove Duplicates from Sorted List II
Solution 1: In-place overwrite
Case 1: input=[1,2,3,3,4,4,5] output=[1,2,5]
Case 2: input=[1,1,1,2,3] output=[2,3]
Case 3: input=[1,1] output=[]

Solution 2: Dummy node predecessor
Case 1: input=[1,2,3,3,4,4,5] output=[1,2,5]
Case 2: input=[1,1,1,2,3] output=[2,3]
Case 3: input=[1,1] output=[]
```

檢查空白與換行：

```bash
git diff --check
```

本機驗證結果：沒有輸出，代表未發現多餘空白或換行問題。

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_082.sln
└── leetcode_082/
    ├── Program.cs
    └── leetcode_082.csproj
```
