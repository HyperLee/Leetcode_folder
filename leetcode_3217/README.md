# leetcode_3217

這個專案放置 LeetCode 題目的 C# 練習程式與解題說明（主要使用 .NET 8）。

## 專案目的

- 收錄題目解法程式碼與詳細說明。
- 在 README 中補充重要概念與範例，方便快速回顧與教學。

## 本篇重點

這份 README 針對題目解法的詳細說明與延伸知識：什麼是哨兵節點（dummy node）、它的用意為何，以及在 C# 中的實作範例和常見陷阱。

---

## 解法概述（範例題目）

> 注意：專案中可能包含多個題目，下面以鏈結串列（Linked List）相關操作的常見解法作為範例說明（例如合併、插入或刪除節點）。請視專案中具體題目調整。

步驟摘要：

1. 分析題目需求 —— 是否需要處理頭節點可能改變的情況（例如刪除頭節點、在頭部插入）？
2. 決定是否使用哨兵節點（dummy node）來統一處理邊界情況。
3. 使用一或多個指標（pointer）遍歷或重連節點。
4. 返回結果時，通常從 `dummy.next` 開始。

時間複雜度：O(n)

空間複雜度：O(1)（不含輸出節點的空間）

---

## 什麼是哨兵節點（dummy node）？

哨兵節點（dummy node）是一個臨時建立、不存放實際資料的節點，通常放在鏈結串列的最前端（即新頭節點前）。它的用途是簡化邊界條件處理，例如：

- 刪除頭節點時，不需要特別分支來處理空或單一元素的情況。
- 插入或合併時，可統一使用前驅節點（prev）進行操作，避免多處條件判斷。
- 當操作會改變原始頭節點時，最後可從 `dummy.next` 取得新的頭節點。

哨兵節點的好處：

- 讓程式邏輯更直線化（less branching），較容易理解與維護。
- 減少錯誤（例如 NullReferenceException）發生的機率。

---

## C# 範例：刪除鏈結串列中所有目標值的節點

下面是一個示範函式，刪除所有值等於 target 的節點：

```csharp
public class ListNode {
    public int val;
    public ListNode next;
    public ListNode(int val=0, ListNode next=null) { this.val = val; this.next = next; }
}

public ListNode RemoveElements(ListNode head, int val) {
    var dummy = new ListNode(0) { next = head };
    var prev = dummy;
    while (prev.next != null) {
        if (prev.next.val == val) {
            prev.next = prev.next.next; // 跳過目標節點
        } else {
            prev = prev.next; // 否則移動
        }
    }
    return dummy.next;
}
```

說明：

- 建立一個 `dummy`，其 `next` 指向原本的 `head`。
- 使用 `prev` 指向目前檢查節點的前一個節點。
- 當 `prev.next` 的值等於目標值時，直接修改 `prev.next` 指向下一個節點，刪除節點；否則將 `prev` 往前移動。
- 回傳 `dummy.next` 作為新的頭節點（可能跟原本一樣，也可能不同）。

---

## 常見陷阱與注意事項

- 別忘了回傳 `dummy.next` 而不是 `dummy`。
- 如果題目需要保留原始節點引用（例如外部參考），請小心操作以免破壞預期。
- 當操作多個鏈結串列或合併節點時，確認每個 pointer 都被正確地更新以避免形成環（cycle）。

---

## 如何在本專案中執行

專案使用 .NET 8。你可以在 `leetcode_3217` 目錄下使用以下命令建構與執行：

```bash
cd leetcode_3217
dotnet build
dotnet run
```

---

## 小結

這份 README 提供了關於哨兵節點的概念、用途與 C# 的實作範例，能幫助你在處理鏈結串列相關題目時更簡潔、安全地編寫程式。

如需把 README 調整為針對某一題目的專用說明，請告訴我題號或貼上題目內容，我會補齊。