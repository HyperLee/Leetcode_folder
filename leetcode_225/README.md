# LeetCode 225：Implement Stack using Queues

以 C# 實作「只用 queue 模擬 stack」的兩種經典解法，並提供可直接執行的 `Main` 測資，對照雙佇列與單佇列輪轉兩種策略是否都能正確維持 LIFO（後進先出）行為。

## 題目說明

請只使用 queue 的標準操作來實作 stack，並支援以下功能：

- `push(x)`：把元素推到 stack 頂端
- `pop()`：移除並回傳 stack 頂端元素
- `top()`：查看 stack 頂端元素
- `empty()`：判斷 stack 是否為空

題目要求只能使用 queue 的標準操作，也就是：

- 從尾端加入元素
- 從前端查看或移除元素
- 取得元素數量
- 判斷是否為空

題目連結：

- [LeetCode 225 - Implement Stack using Queues](https://leetcode.com/problems/implement-stack-using-queues/description/)
- [LeetCode CN 225 - 用佇列實作堆疊](https://leetcode.cn/problems/implement-stack-using-queues/description/)

## 官方範例

```text
Input
["MyStack", "push", "push", "top", "pop", "empty"]
[[], [1], [2], [], [], []]

Output
[null, null, null, 2, 2, false]
```

```text
Explanation
MyStack myStack = new MyStack();
myStack.push(1);
myStack.push(2);
myStack.top();   // return 2
myStack.pop();   // return 2
myStack.empty(); // return false
```

## 限制條件

根據題目公開內容，限制條件如下：

- `1 <= x <= 9`
- `push`、`pop`、`top`、`empty` 總呼叫次數最多為 `100`
- 題目保證每次呼叫 `pop()` 與 `top()` 時，stack 一定非空

本專案依照上述前提實作，因此不額外處理非法呼叫空 stack 的防禦邏輯。

## 基本知識：Stack 與 Queue 是什麼？

### Stack（堆疊）

Stack 的核心規則是 **LIFO, Last In First Out**，也就是「後進先出」。

可以把它想成一疊盤子：

- 最後放上去的盤子在最上面
- 之後最先被拿走的也是它

常見操作：

- `push(x)`：把元素放到頂端
- `pop()`：把頂端元素拿走
- `top()`：偷看頂端元素，但不拿走
- `empty()`：確認是否沒有元素

### Queue（佇列）

Queue 的核心規則是 **FIFO, First In First Out**，也就是「先進先出」。

可以把它想成排隊：

- 最早排進來的人，最早被服務
- 新加入的人只能站到隊尾

常見操作：

- `enqueue(x)`：從尾端加入元素
- `dequeue()`：從前端移除元素
- `peek()`：查看前端元素

### 這題的難點

Stack 要的是「最後進去的先出來」，queue 卻是「最早進去的先出來」。

所以這題真正要解決的不是資料能不能放進 queue，而是：

> 如何透過 queue 的前後順序調整，讓 queue 的 front 看起來像 stack 的 top？

兩種解法的核心都一樣：

- `pop()` 與 `top()` 想要變快
- 就必須在 `push()` 時先把順序調整好

## 解題概念與出發點

如果我們能保證：

```text
queue front = stack top
```

那麼之後：

- `pop()` 只要直接從 front 取出即可
- `top()` 只要直接看 front 即可
- `empty()` 只要檢查 queue 是否為空即可

因此這題的重點就變成：

- 每次 `push(x)` 後，要把最新加入的元素移到 queue front

本專案示範兩種做法：

| 方法 | 核心想法 | `push` | `pop` | `top` | `empty` | 空間 |
| --- | --- | --- | --- | --- | --- | --- |
| 方法一：雙佇列重排 | 新元素先進輔助 queue，再把舊元素全接到後面 | `O(n)` | `O(1)` | `O(1)` | `O(1)` | `O(n)` |
| 方法二：單佇列輪轉 | 新元素進 queue 後，把前面舊元素依序轉到尾端 | `O(n)` | `O(1)` | `O(1)` | `O(1)` | `O(n)` |

## 方法一：雙佇列重排

對應程式中的 `MyStack`。

### 設計說明

準備兩個 queue：

- `queue1`：主要保存目前 stack 內容
- `queue2`：在 `push()` 時暫時當輔助 queue

每次 `push(x)` 時：

1. 先把 `x` 放進 `queue2`
2. 把 `queue1` 中所有元素依序移到 `queue2` 後方
3. 交換 `queue1` 與 `queue2`

做完之後：

- `queue1` 的 front 一定是最新加入的 `x`
- 也就是 stack 的 top

這代表我們把 queue 的順序主動重排成：

```text
最新元素 -> 次新元素 -> 更早元素
```

雖然底層還是 queue，但從 front 取資料時，行為就像 stack。

### 為什麼可行？

因為 `push()` 完成後，`queue1` 的排列會長這樣：

```text
[stack top, ..., stack bottom]
```

所以：

- `pop()`：直接 `Dequeue()`
- `top()`：直接 `Peek()`
- `empty()`：直接看 `queue1.Count == 0`

### 範例演示流程

以 `push(1) -> push(2) -> top() -> pop() -> empty()` 為例：

| 步驟 | 操作 | `queue1` | `queue2` | 說明 |
| --- | --- | --- | --- | --- |
| 1 | 初始 | `[]` | `[]` | 兩個 queue 都是空的 |
| 2 | `push(1)` | `[1]` | `[]` | `1` 先進輔助 queue，再交換 |
| 3 | `push(2)` | `[2, 1]` | `[]` | `2` 先進 `queue2`，再把 `1` 接到後面 |
| 4 | `top()` | `[2, 1]` | `[]` | front 是 `2`，回傳 `2` |
| 5 | `pop()` | `[1]` | `[]` | 移除 front `2`，回傳 `2` |
| 6 | `empty()` | `[1]` | `[]` | 還有元素，所以回傳 `False` |

### 這個方法的優缺點

優點：

- `pop()`、`top()`、`empty()` 都很直覺
- queue front 永遠就是 stack top，判斷最清楚

缺點：

- 每次 `push()` 都要搬動全部既有元素
- 需要第二個 queue 當輔助結構

## 方法二：單佇列輪轉

對應程式中的 `MyStackSingleQueue`。

這也是題目 follow-up 常見的延伸：只用一個 queue，能不能完成 stack？

答案是可以，關鍵在於「輪轉」。

### 設計說明

只使用一個 queue：

1. `push(x)` 時，先把 `x` 加到 queue 尾端
2. 記錄 queue 目前的元素總數
3. 將除了 `x` 以外的舊元素，逐一從 front 取出再放回尾端

這個動作會把新元素一路轉到 front。

換句話說：

- 新元素雖然先加在尾端
- 但透過輪轉，把舊元素全部排到它後面
- 最終 front 仍然對應 stack top

### 為什麼可行？

假設原本 queue 是：

```text
[top, ..., bottom]
```

當新元素 `x` 加到尾端後：

```text
[top, ..., bottom, x]
```

接著把前面原本的舊元素各輪轉一次：

```text
[x, top, ..., bottom]
```

此時 `x` 就成為新的 front，也就是新的 stack top。

### 範例演示流程

同樣看 `push(1) -> push(2) -> top() -> pop() -> empty()`：

| 步驟 | 操作 | queue 狀態 | 說明 |
| --- | --- | --- | --- |
| 1 | 初始 | `[]` | queue 為空 |
| 2 | `push(1)` | `[1]` | 只有一個元素，不需要輪轉 |
| 3 | `push(2)` | `[1, 2]` -> `[2, 1]` | 新元素先進尾端，再把舊的 `1` 轉到後面 |
| 4 | `top()` | `[2, 1]` | front 是 `2`，回傳 `2` |
| 5 | `pop()` | `[1]` | 移除 front `2`，回傳 `2` |
| 6 | `empty()` | `[1]` | 還有元素，所以回傳 `False` |

### 這個方法的優缺點

優點：

- 只需要一個 queue
- 寫法簡潔，符合 follow-up 延伸方向

缺點：

- `push()` 一樣需要 `O(n)` 輪轉
- 概念上比雙佇列稍微抽象，因為要理解輪轉後順序如何改變

## 兩種解法如何選擇？

在這題裡，兩種方法的漸進複雜度相同：

- 都把成本集中在 `push()`
- 都讓 `pop()` 與 `top()` 保持 `O(1)`

差別主要在實作取向：

- 若想要邏輯更直白、把「搬到輔助 queue 再交換」看得很清楚，雙佇列版本較容易理解
- 若想符合 follow-up 並減少結構數量，單佇列輪轉版本更精煉

因此本專案把兩種都保留，方便直接對照。

## 可執行範例

`Main` 目前固定執行 4 組案例，並對兩種實作各驗證一次，共 8 項檢查：

1. 官方示例：`push(1) -> push(2) -> top() -> pop() -> empty()`
2. 多次推入後驗證 LIFO：確認最後推入的 `30` 最先被彈出
3. 單一元素完整生命週期：`push -> top -> pop -> empty`
4. 清空後再次使用：驗證 stack 清空後仍可正常重複使用

## 執行方式

在此 repository 根目錄執行：

```bash
dotnet build leetcode_225/leetcode_225.csproj --nologo
dotnet test leetcode_225/leetcode_225.csproj --nologo
dotnet run --project leetcode_225/leetcode_225.csproj --no-build
```

補充說明：

- 目前 repo 沒有獨立測試專案，所以 `dotnet test` 在這裡是 smoke check，用來確認專案可還原並接受測試命令。
- 主控台輸出使用 C# 的布林字串格式，因此會看到 `True` / `False`，而不是 LeetCode 頁面常見的 `true` / `false`。

## 實際執行輸出

以下是目前 `dotnet run --project leetcode_225/leetcode_225.csproj --no-build` 的實際輸出：

```text
LeetCode 225 - Implement Stack using Queues
==================================================
[1] 官方示例：先推入 1、2，再依序 top / pop / empty
操作序列：push(1) -> push(2) -> top() -> pop() -> empty()
預期輸出：[null, null, 2, 2, False]
方法一（雙佇列重排）：[null, null, 2, 2, False] (PASS)
方法二（單佇列輪轉）：[null, null, 2, 2, False] (PASS)

[2] 多次推入後驗證後進先出
操作序列：push(10) -> push(20) -> push(30) -> pop() -> top() -> empty()
預期輸出：[null, null, null, 30, 20, False]
方法一（雙佇列重排）：[null, null, null, 30, 20, False] (PASS)
方法二（單佇列輪轉）：[null, null, null, 30, 20, False] (PASS)

[3] 單一元素的完整生命週期
操作序列：push(42) -> top() -> pop() -> empty()
預期輸出：[null, 42, 42, True]
方法一（雙佇列重排）：[null, 42, 42, True] (PASS)
方法二（單佇列輪轉）：[null, 42, 42, True] (PASS)

[4] 清空後再次使用 stack
操作序列：push(7) -> pop() -> empty() -> push(9) -> top() -> pop() -> empty()
預期輸出：[null, 7, True, null, 9, 9, True]
方法一（雙佇列重排）：[null, 7, True, null, 9, 9, True] (PASS)
方法二（單佇列輪轉）：[null, 7, True, null, 9, 9, True] (PASS)

總結：8/8 項驗證通過
```

## 專案結構

```text
.
├── docs/
│   └── readme-template.md
├── leetcode_225/
│   ├── Program.cs
│   └── leetcode_225.csproj
└── README.md
```

## 驗證指令

完成修改後，可再執行以下指令確認格式與輸出都保持一致：

```bash
dotnet build leetcode_225/leetcode_225.csproj --nologo
dotnet test leetcode_225/leetcode_225.csproj --nologo
dotnet run --project leetcode_225/leetcode_225.csproj --no-build
git diff --check
```
