# LeetCode 232：用堆疊實作佇列

這個 .NET 10 Console 專案示範如何只使用兩個堆疊，實作支援
`Push`、`Pop`、`Peek` 與 `Empty` 的先進先出（FIFO）佇列。
專案保留兩種不同的時間複雜度取捨，並在 `Main` 使用相同操作序列自動驗證結果。

## 題目說明

實作 `MyQueue` 類別，使它提供一般佇列的四種操作：

- `Push(int x)`：將元素加入佇列尾端。
- `Pop()`：移除並回傳佇列前端元素。
- `Peek()`：回傳但不移除佇列前端元素。
- `Empty()`：判斷佇列是否為空。

只能使用標準堆疊操作，也就是從頂端加入、查看或移除元素，以及取得大小或判斷是否為空。

## 限制條件

- `1 <= x <= 9`
- `Push`、`Pop`、`Peek` 與 `Empty` 合計最多呼叫 `100` 次。
- 每次呼叫 `Pop` 或 `Peek` 時，佇列都保證非空。
- 只能透過標準堆疊操作模擬佇列。
- 進階目標是讓每個操作達到均攤 `O(1)` 時間複雜度。

## 解題概念與出發點

堆疊是後進先出（LIFO），佇列則是先進先出（FIFO）。若依序將 `1`、`2` 推入同一個堆疊，
頂端會是較晚加入的 `2`，正好與佇列需要的隊首順序相反。

兩個解法都利用第二個堆疊反轉一次元素順序，但選擇在不同時間支付搬移成本：

| 解法 | 搬移時機 | `Push` | `Pop` | `Peek` | `Empty` | 額外空間 |
| --- | --- | --- | --- | --- | --- | --- |
| `MyQueue` 延遲搬移 | 輸出堆疊為空且需要隊首時 | `O(1)` | 均攤 `O(1)`，單次最差 `O(n)` | 均攤 `O(1)`，單次最差 `O(n)` | `O(1)` | `O(n)` |
| `MyQueue2` Push 時重排 | 每次加入新元素時 | `O(n)` | `O(1)` | `O(1)` | `O(1)` | `O(n)` |

## 解法一：延遲搬移

### 設計

`MyQueue` 使用兩個各司其職的堆疊：

- `inStack` 接收所有新元素，頂端是最新加入的元素。
- `outStack` 保存已反轉的元素，頂端是目前隊首。

`Push` 永遠只把新值放入 `inStack`。`Pop` 或 `Peek` 需要讀取隊首時，先檢查
`outStack`：只要它仍有元素，頂端就是正確隊首，不可提早混入較新的資料；只有
`outStack` 為空時，才把 `inStack` 全部搬過去。

一次完整搬移會反轉元素順序。每個元素先進入 `inStack`，之後至多被搬入
`outStack` 一次，最後再被移除一次，因此一連串 `n` 次操作的總成本是 `O(n)`，
`Pop` 與 `Peek` 的均攤成本為 `O(1)`。

### 範例演示

以下以「左側為堆疊頂端」表示狀態：

| 步驟 | 操作 | `inStack`（頂端 → 底部） | `outStack`（頂端 → 底部） | 結果 |
| --- | --- | --- | --- | --- |
| 1 | `Push(1)` | `[1]` | `[]` | 佇列為 `[1]` |
| 2 | `Push(2)` | `[2, 1]` | `[]` | 佇列為 `[1, 2]` |
| 3 | `Peek()` | `[]` | `[1, 2]` | 搬移後回傳 `1` |
| 4 | `Pop()` | `[]` | `[2]` | 移除 `1` |
| 5 | `Push(3)` | `[3]` | `[2]` | 新值留在輸入端 |
| 6 | `Peek()` | `[3]` | `[2]` | 不搬移，仍回傳舊隊首 `2` |
| 7 | `Pop()` | `[3]` | `[]` | 移除 `2` |
| 8 | `Pop()` | `[]` | `[]` | 搬移後移除 `3` |

步驟 5 是重要的不變量：只要 `outStack` 還有舊資料，新加入的 `3` 就不能搬到它上方，
否則會破壞 FIFO 順序。

## 解法二：Push 時重排

### 設計

`MyQueue2` 讓 `mainStack` 的頂端永遠保持為隊首，`tempStack` 只在 `Push` 期間暫存資料：

1. 將 `mainStack` 的既有元素全部搬到 `tempStack`。
2. 把新值放進空的 `mainStack`；此時新值位於未來的最底部，也就是佇列尾端。
3. 將 `tempStack` 的元素全部搬回 `mainStack`，恢復原本的 FIFO 次序。

重排完成後，`Pop` 與 `Peek` 可以直接操作 `mainStack` 頂端。這種設計把成本集中在
`Push`，每次加入可能搬動所有既有元素，所以 `Push` 是 `O(n)`；讀取與移除隊首則是
`O(1)`。

### 範例演示

同樣以「左側為堆疊頂端」表示：

| 步驟 | 操作 | 搬移過程 | `mainStack`（頂端 → 底部） | 結果 |
| --- | --- | --- | --- | --- |
| 1 | `Push(1)` | 主堆疊原本為空 | `[1]` | 隊首與隊尾皆為 `1` |
| 2 | `Push(2)` | `[1]` 搬至暫存，放入 `2`，再搬回 `1` | `[1, 2]` | 隊首仍為 `1` |
| 3 | `Peek()` | 不需搬移 | `[1, 2]` | 回傳 `1` |
| 4 | `Pop()` | 不需搬移 | `[2]` | 移除 `1` |
| 5 | `Push(3)` | `[2]` 搬至暫存，放入 `3`，再搬回 `2` | `[2, 3]` | 隊首為 `2`、隊尾為 `3` |
| 6 | `Pop()` | 不需搬移 | `[3]` | 移除 `2` |

這個版本的流程較容易直接觀察隊首，但若操作以大量 `Push` 為主，搬移成本會高於解法一。

## 可執行驗證

`Main` 讓兩種實作各自執行相同的 11 項檢查，涵蓋：

- 初始空佇列。
- 官方範例的加入、查看與移除順序。
- 已移除部分資料後再加入新值。
- 完全清空。
- 清空後重新使用同一個佇列。

每一項都會比對 Expected 與 Actual；只要有任何失敗，程式便設定非零結束碼。

## 建置與執行

請從此 README 所在的 `leetcode_232` 儲存庫目錄執行：

```bash
dotnet restore leetcode_232/leetcode_232.csproj
dotnet build leetcode_232/leetcode_232.csproj --nologo --no-restore
dotnet run --project leetcode_232/leetcode_232.csproj --no-build
```

本專案目前沒有自動測試專案；驗收方式是成功建置，加上 `Main` 內可重複執行的自動比對。

### 實際執行結果

```text
解法一：延遲搬移
  初始 Empty() | Expected: True | Actual: True | PASS
  Push(1), Push(2), Peek() | Expected: 1 | Actual: 1 | PASS
  Pop() | Expected: 1 | Actual: 1 | PASS
  剩餘元素時 Empty() | Expected: False | Actual: False | PASS
  Pop 後 Push(3), Peek() | Expected: 2 | Actual: 2 | PASS
  Pop() 取得舊資料 | Expected: 2 | Actual: 2 | PASS
  Pop() 取得新資料 | Expected: 3 | Actual: 3 | PASS
  完全取出後 Empty() | Expected: True | Actual: True | PASS
  重用後 Peek() | Expected: 9 | Actual: 9 | PASS
  重用後 Pop() | Expected: 9 | Actual: 9 | PASS
  重用並清空後 Empty() | Expected: True | Actual: True | PASS

解法二：Push 時重排
  初始 Empty() | Expected: True | Actual: True | PASS
  Push(1), Push(2), Peek() | Expected: 1 | Actual: 1 | PASS
  Pop() | Expected: 1 | Actual: 1 | PASS
  剩餘元素時 Empty() | Expected: False | Actual: False | PASS
  Pop 後 Push(3), Peek() | Expected: 2 | Actual: 2 | PASS
  Pop() 取得舊資料 | Expected: 2 | Actual: 2 | PASS
  Pop() 取得新資料 | Expected: 3 | Actual: 3 | PASS
  完全取出後 Empty() | Expected: True | Actual: True | PASS
  重用後 Peek() | Expected: 9 | Actual: 9 | PASS
  重用後 Pop() | Expected: 9 | Actual: 9 | PASS
  重用並清空後 Empty() | Expected: True | Actual: True | PASS

總結：22/22 項驗證通過
```

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_232/
    ├── leetcode_232.csproj
    └── Program.cs
```

## 參考資料

- [LeetCode 232：Implement Queue using Stacks](https://leetcode.com/problems/implement-queue-using-stacks/description/)
- [力扣 232：用棧實現佇列](https://leetcode.cn/problems/implement-queue-using-stacks/description/)
- [力扣官方解法說明](https://leetcode.cn/problems/implement-queue-using-stacks/solution/yong-zhan-shi-xian-dui-lie-by-leetcode-s-xnb6/)
