# LeetCode 142 - Linked List Cycle II

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/language-C%23-239120)
![Topic](https://img.shields.io/badge/topic-linked--list-blue)

這個專案使用 C# console app 示範 LeetCode 142「環形鏈結串列 II」。
目標是在不修改 linked list 的前提下，找出環開始的節點；若沒有環則回傳 `null`。

## 快速連結

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法說明](#解法說明)
- [流程演示](#流程演示)
- [建置與執行](#建置與執行)
- [專案結構](#專案結構)

## 題目說明

給定一個鏈結串列的頭節點 `head`，請回傳環開始的節點。
如果鏈結串列中沒有環，請回傳 `null`。

當串列中的某個節點可以透過持續走訪 `next` 指標再次到達時，代表鏈結串列中存在環。
題目內部使用 `pos` 表示尾節點的 `next` 指標連回哪一個節點索引；`pos = -1` 表示沒有環。
`pos` 不會作為參數傳入，解法只能透過節點連結本身判斷。

要求：不可修改原本的鏈結串列。

## 限制條件

- 鏈結串列節點數量範圍是 `[0, 10^4]`。
- `-10^5 <= Node.val <= 10^5`。
- `pos` 為 `-1` 或鏈結串列中的合法索引。
- 演算法不可修改鏈結串列。

## 解題概念與出發點

這題的重點是「只靠節點參考判斷是否有環，以及環從哪裡開始」。
若使用雜湊集合記錄走訪過的節點，可以直接找出第一個重複節點，但會需要 `O(n)` 額外空間。

Floyd 快慢指標可以用 `O(1)` 額外空間完成：

1. `slow` 每次走一步，`fast` 每次走兩步。
2. 若沒有環，`fast` 會先走到 `null`。
3. 若有環，`slow` 與 `fast` 一定會在環中相遇。
4. 相遇後，將其中一個指標移回 `head`，兩個指標每次都走一步。
5. 兩者再次相遇的位置就是環的入口。

距離推導的核心是：假設 `head` 到入環點距離為 `a`，入環點到第一次相遇點距離為 `b`，相遇點回到入環點距離為 `c`。
快指標速度是慢指標兩倍，因此可推出 `a = c + (n - 1)(b + c)`。
也就是從 `head` 走到入口，會和從相遇點繼續走回入口在同一個節點會合。

## 解法說明

### 解法一：`DetectCycle`

`DetectCycle(ListNode? head)` 使用 `oneStep` 與 `twoStep` 表示慢指標與快指標。

設計流程：

1. 兩個指標都從 `head` 出發。
2. 每輪讓 `oneStep` 走一步、`twoStep` 走兩步。
3. 若 `twoStep` 或 `twoStep.next` 為 `null`，代表沒有環，回傳 `null`。
4. 若兩個指標相遇，建立 `oneStep2` 從 `head` 出發。
5. `oneStep` 從相遇點繼續走，`oneStep2` 從頭開始走，兩者每次都走一步。
6. 再次相遇的節點就是入環點。

複雜度：

- 時間複雜度：`O(n)`。
- 空間複雜度：`O(1)`。

### 解法二：`DetectCycle2`

`DetectCycle2(ListNode? head)` 保留同樣的 Floyd 解法，但變數命名更接近常見官方說明：
`slow`、`fast` 用於第一次相遇，`ptr` 用於從 `head` 出發尋找入口。

設計流程：

1. 若 `head` 為 `null`，直接回傳 `null`。
2. 使用 `slow` 與 `fast` 偵測是否存在環。
3. 找到相遇點後，令 `ptr = head`。
4. `ptr` 與 `slow` 同速前進。
5. `ptr == slow` 時回傳該節點。
6. 若快指標先走到尾端，回傳 `null`。

複雜度：

- 時間複雜度：`O(n)`。
- 空間複雜度：`O(1)`。

## 流程演示

### 解法一範例：`DetectCycle`，`head = [3,2,0,-4]`，`pos = 1`

串列尾端 `-4` 的 `next` 指回索引 `1` 的節點 `2`：

```text
3 -> 2 -> 0 -> -4
     ^         |
     |_________|
```

| 階段 | oneStep | twoStep | 說明 |
| --- | --- | --- | --- |
| 起點 | 3 | 3 | 兩者都從 head 出發 |
| 第 1 輪 | 2 | 0 | 慢指標走一步，快指標走兩步 |
| 第 2 輪 | 0 | 2 | 快指標已進入環並繼續追趕 |
| 第 3 輪 | -4 | -4 | 第一次在環內相遇 |
| 尋找入口 | oneStep2 = 3, oneStep = -4 |  | 一個從 head 出發，一個從相遇點出發 |
| 會合 | 2 | 2 | 再次相遇位置是入環點 |

結果回傳節點值 `2`。

### 解法二範例：`DetectCycle2`，`head = [1,2,3,4]`，`pos = 0`

串列尾端 `4` 的 `next` 指回索引 `0` 的節點 `1`：

```text
1 -> 2 -> 3 -> 4
^              |
|______________|
```

| 階段 | slow | fast 或 ptr | 說明 |
| --- | --- | --- | --- |
| 起點 | 1 | 1 | `slow` 與 `fast` 都從 head 出發 |
| 第 1 輪 | 2 | 3 | 快指標每次多走一步 |
| 第 2 輪 | 3 | 1 | 快指標繞回環內 |
| 第 3 輪 | 4 | 3 | 繼續追趕 |
| 第 4 輪 | 1 | 1 | 第一次相遇 |
| 尋找入口 | slow = 1 | ptr = 1 | `ptr` 從 head 出發，已與 slow 同在入口 |

結果回傳節點值 `1`。

## 建置與執行

需求：

- .NET SDK 10.0 或相容版本

建置專案：

```bash
dotnet build leetcode_142.sln
```

本機驗證結果：

```text
建置成功。
    0 個警告
    0 個錯誤
```

執行範例：

```bash
dotnet run --project leetcode_142/leetcode_142.csproj
```

目前範例輸出：

```text
LeetCode 142 - Linked List Cycle II (DetectCycle)
Empty list: values=[], pos=-1, expected=null, actual=null, Result: PASS
Single node without cycle: values=[1], pos=-1, expected=null, actual=null, Result: PASS
Single node cycle to self: values=[1], pos=0, expected=1, actual=1, Result: PASS
No cycle: values=[3,2,0,-4], pos=-1, expected=null, actual=null, Result: PASS
Cycle at head: values=[1,2,3,4], pos=0, expected=1, actual=1, Result: PASS
Cycle in middle: values=[3,2,0,-4], pos=1, expected=2, actual=2, Result: PASS

LeetCode 142 - Linked List Cycle II (DetectCycle2)
Empty list: values=[], pos=-1, expected=null, actual=null, Result: PASS
Single node without cycle: values=[1], pos=-1, expected=null, actual=null, Result: PASS
Single node cycle to self: values=[1], pos=0, expected=1, actual=1, Result: PASS
No cycle: values=[3,2,0,-4], pos=-1, expected=null, actual=null, Result: PASS
Cycle at head: values=[1,2,3,4], pos=0, expected=1, actual=1, Result: PASS
Cycle in middle: values=[3,2,0,-4], pos=1, expected=2, actual=2, Result: PASS
```

目前沒有獨立測試專案；`Program.Main` 會執行內建案例並印出實際輸出、預期輸出與比對結果。

檢查空白與換行：

```bash
git diff --check
```

本機驗證結果：沒有輸出，代表未發現多餘空白或換行問題。

> 在此 macOS 環境中，`dotnet` CLI 可能會額外輸出 `CSSM_ModuleLoad(): One or more parameters passed to a function were not valid.`。
> 這是環境層訊息，不是此專案的程式輸出。

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_142.sln
└── leetcode_142/
    ├── Program.cs
    └── leetcode_142.csproj
```

## 參考連結

- [LeetCode - Linked List Cycle II](https://leetcode.com/problems/linked-list-cycle-ii/description/)
- [LeetCode 中文站 - 环形链表 II](https://leetcode.cn/problems/linked-list-cycle-ii/description/)
