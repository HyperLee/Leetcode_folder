# LeetCode 203. Remove Linked List Elements

## 題目說明

- 英文題目: [203. Remove Linked List Elements](https://leetcode.com/problems/remove-linked-list-elements/description/)
- 中文題目: [203. 移除鏈結串列元素](https://leetcode.cn/problems/remove-linked-list-elements/description/)

給定一個鏈結串列的頭節點 `head` 與一個整數 `val`，請移除所有節點值等於 `val` 的節點，並回傳新的頭節點。

換句話說，這題不是只刪掉第一個符合條件的節點，而是要把整條鏈結串列中所有 `Node.val == val` 的節點全部清掉，最後留下新的串列結果。

## 限制條件

以下是本題常見的 LeetCode 題目限制：

- 鏈結串列節點數量介於 `0` 到 `10^4` 之間
- `1 <= Node.val <= 50`
- `0 <= val <= 50`

這些限制代表：

- 空串列是合法輸入
- 目標值可能完全不存在
- 目標值可能出現在開頭、中間、尾端，甚至整條串列全部都是目標值

## 解題概念與出發點

本題的真正難點不是「找到值等於 `val` 的節點」，而是「在刪除節點後，如何正確重新接回鏈結」。

因為鏈結串列不像陣列可以直接覆蓋元素，所以每次刪除時都要處理指標關係：

- 若目前節點要刪除，就要把前一個節點直接接到下一個節點
- 若頭節點剛好就要刪除，新的頭節點也會改變
- 若連續多個節點都要刪除，必須能連續修正鏈結而不漏掉節點

這題最常見的兩條思路如下：

1. 遞迴
2. 迭代 + Dummy Head

本專案兩種都保留，方便直接比較。

## 解法一：遞迴

### 設計思路

遞迴版的核心想法是：

1. 先處理後面的子鏈結串列
2. 等後面都已經完成刪除後，再決定目前 `head` 要不要保留

也就是說，對於目前節點 `head` 而言：

- 先把 `head.next` 交給遞迴去處理
- 回來之後，`head.next` 已經變成「刪除完成後的後半段串列」
- 此時只要判斷 `head.val == val` 是否成立
  - 若成立，回傳 `head.next`，等於把自己跳過
  - 若不成立，回傳 `head`，表示保留自己

### 關鍵判斷流程

```csharp
if (head is null)
{
    return null;
}

head.next = RemoveElements(head.next, val);

if (head.val == val)
{
    return head.next;
}

return head;
```

### 為什麼可行

因為每一層遞迴都只做兩件事：

- 讓後面的結果先正確
- 再處理自己的去留

這讓問題自然拆成「較短的同型問題」，非常符合鏈結串列的遞迴結構。

### 時間與空間複雜度

- 時間複雜度: `O(n)`
- 空間複雜度: `O(n)`

雖然每個節點只會拜訪一次，但遞迴呼叫堆疊會額外佔用 `O(n)` 空間。

### 適用情境與注意事項

- 優點是寫法直觀、程式碼短、概念清楚
- 缺點是當串列很長時，呼叫堆疊會增加
- 在面試或教學情境中很適合用來說明鏈結串列的遞迴思考方式

### 範例演示流程

以 `head = [1,2,6,3,4,5,6]`、`val = 6` 為例：

1. `1` 先遞迴處理後面 `[2,6,3,4,5,6]`
2. `2` 再遞迴處理後面 `[6,3,4,5,6]`
3. 第一個 `6` 遞迴處理後面 `[3,4,5,6]`
4. 最尾端 `6` 會先回傳 `null`
5. `5` 收到後面是 `null`，保留自己，回傳 `[5]`
6. `4` 保留，回傳 `[4,5]`
7. `3` 保留，回傳 `[3,4,5]`
8. 第一個 `6` 因為等於目標值，直接回傳 `[3,4,5]`
9. `2` 保留，回傳 `[2,3,4,5]`
10. `1` 保留，回傳 `[1,2,3,4,5]`

## 解法二：迭代 + Dummy Head

### 設計思路

迭代版的重點是：刪除節點時一定要能取得「前一個節點」。

如果直接從原本的 `head` 開始做，當第一個節點本身就要刪除時，處理會比較麻煩，所以我們先建立一個 `dummyHead`：

- `dummyHead.next = head`
- 讓 `current` 一開始指向 `dummyHead`

這樣一來，就算真正的 `head` 要被刪掉，也能透過 `dummyHead` 這個穩定的前驅節點來重新接線。

### 關鍵判斷流程

```csharp
ListNode dummyHead = new ListNode(0, head);
ListNode current = dummyHead;

while (current.next is not null)
{
    if (current.next.val == val)
    {
        current.next = current.next.next;
    }
    else
    {
        current = current.next;
    }
}

return dummyHead.next;
```

### 為什麼可行

`current` 永遠代表「目前正在檢查之節點的前一個節點」。

因此：

- 若 `current.next` 要刪除，就直接把 `current.next` 改接到 `current.next.next`
- 若 `current.next` 不刪除，才把 `current` 往前移

這能避免：

- 刪除頭節點時沒有前驅節點
- 連續刪除多個節點時漏檢查下一個節點

### 時間與空間複雜度

- 時間複雜度: `O(n)`
- 空間複雜度: `O(1)`

只使用固定額外節點與指標，沒有遞迴堆疊成本。

### 適用情境與注意事項

- 優點是空間效率較好
- 缺點是如果沒有 dummy head，頭節點刪除情境很容易寫錯
- 實務上若題目要求就地修改鏈結串列，這種寫法通常更穩定

### 範例演示流程

以 `head = [6,1,2,6,3,6]`、`val = 6` 為例：

1. 建立 `dummyHead -> 6 -> 1 -> 2 -> 6 -> 3 -> 6`
2. `current` 先停在 `dummyHead`
3. `current.next.val == 6`，刪掉第一個 `6`
   - 串列變成 `dummyHead -> 1 -> 2 -> 6 -> 3 -> 6`
4. `current` 仍停在 `dummyHead`，繼續檢查新的 `current.next`
5. `1` 不等於 `6`，`current` 移到 `1`
6. `2` 不等於 `6`，`current` 移到 `2`
7. 下一個節點是 `6`，刪掉它
   - 串列變成 `dummyHead -> 1 -> 2 -> 3 -> 6`
8. `3` 不等於 `6`，`current` 移到 `3`
9. 下一個節點是 `6`，再刪掉它
10. 最後回傳 `dummyHead.next`，得到 `[1,2,3]`

## 兩種解法比較

| 項目 | 遞迴 | 迭代 + Dummy Head |
| --- | --- | --- |
| 時間複雜度 | `O(n)` | `O(n)` |
| 空間複雜度 | `O(n)` | `O(1)` |
| 可讀性 | 高，邏輯精簡 | 高，流程穩定 |
| 實作風險 | 遞迴堆疊較深 | 容易在沒用 dummy head 時寫錯 |
| 適合情境 | 教學、遞迴思維 | 面試、實務、空間要求較嚴格 |

## 每一種解法的範例演示流程

本專案在 `Main` 中放入 5 組固定案例，並且每一組都同時跑：

- `RemoveElements` 遞迴解法
- `RemoveElements2` 迭代 + Dummy Head 解法

案例如下：

| 案例 | 輸入 | `val` | 預期輸出 | 測試目的 |
| --- | --- | --- | --- | --- |
| Case 1 | `[1,2,6,3,4,5,6]` | `6` | `[1,2,3,4,5]` | 中間與尾端都含目標值 |
| Case 2 | `[]` | `1` | `[]` | 驗證空串列 |
| Case 3 | `[7,7,7,7]` | `7` | `[]` | 驗證全部刪除 |
| Case 4 | `[6,1,2,6,3,6]` | `6` | `[1,2,3]` | 驗證頭節點與中間節點刪除 |
| Case 5 | `[1,2,3]` | `4` | `[1,2,3]` | 驗證完全不需刪除 |

## Main 測試案例說明

`Main` 採用「對照驗證」輸出格式，每組案例都會列出：

- `Input`
- `Remove`
- `Expected`
- `Recursive`
- `Iterative`
- `PASS` / `FAIL`

另外，程式在執行兩種解法前會先複製輸入鏈結串列，原因是兩種解法都會修改 `next` 指標；若共用同一條串列，第二個解法看到的就不是原始資料。

## 實際執行輸出

以下內容來自實際執行：

```text
LeetCode 203 - Remove Linked List Elements
==================================================
Case 1 - Middle and tail nodes match
Input: [1,2,6,3,4,5,6]
Remove: 6
Expected: [1,2,3,4,5]
Recursive: [1,2,3,4,5] | PASS
Iterative: [1,2,3,4,5] | PASS

Case 2 - Empty list
Input: []
Remove: 1
Expected: []
Recursive: [] | PASS
Iterative: [] | PASS

Case 3 - Every node is removed
Input: [7,7,7,7]
Remove: 7
Expected: []
Recursive: [] | PASS
Iterative: [] | PASS

Case 4 - Head and internal nodes match
Input: [6,1,2,6,3,6]
Remove: 6
Expected: [1,2,3]
Recursive: [1,2,3] | PASS
Iterative: [1,2,3] | PASS

Case 5 - No node matches
Input: [1,2,3]
Remove: 4
Expected: [1,2,3]
Recursive: [1,2,3] | PASS
Iterative: [1,2,3] | PASS
```

## 專案指令

請從 repository root `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_203` 執行：

```bash
dotnet build leetcode_203/leetcode_203.csproj
dotnet run --project leetcode_203/leetcode_203.csproj
```

> 目前這個 repository 沒有獨立的 test project，因此 `dotnet test` 不是這個專案目前可用的驗證流程。
