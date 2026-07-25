# LeetCode 2130 — Maximum Twin Sum of a Linked List

> 鏈結串列最大孿生和｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/maximum-twin-sum-of-a-linked-list/)
- [中文題目](https://leetcode.cn/problems/maximum-twin-sum-of-a-linked-list/)

## 題目說明

在含有 `n` 個節點的鏈結串列中，若 `n` 為偶數，索引 `i` 的節點與索引 `n - 1 - i`
的節點互為孿生節點。孿生和是兩個孿生節點的值相加；題目要求回傳所有孿生和中的最大值。

題目限制：

- 節點數是 `[2, 100000]` 範圍內的偶數。
- `1 <= Node.val <= 100000`。

## 解法

公開 API：

```csharp
public static int PairSum(ListNode head)
public static int PairSum2(ListNode head)
```

### Stack 教學解法：`PairSum`

先依原順序把所有節點值推入 Stack。接著從串列頭走訪前 `n / 2` 個節點，同時從 Stack
彈出值。第 `i` 次彈出的值正是原串列第 `n - 1 - i` 個節點值，因此每輪都能直接計算一組
孿生和。

這個版本不改動任何節點，控制流程也容易觀察，代價是 Stack 會保存 `n` 個整數。

### 反轉後半最佳化：`PairSum2`

快指標每次走兩步、慢指標每次走一步。由於輸入長度保證為偶數，快指標抵達尾端時，慢指標
正好位於後半起點。原地反轉後半後，前半從開頭向中間走，後半則以原串列從尾端向中間的順序
走，因此兩個指標每輪恰好指向一組孿生節點。

最大值計算完成後，再對同一區段反轉一次。這會恢復每個原節點的 `next`，讓呼叫端觀察到的
節點參考、值、順序與拓撲都與呼叫前相同。

### 核心不變量與易錯處

- 只有索引 `0` 至 `n / 2 - 1` 需要各計算一次，否則同一孿生對會重複。
- Stack 的後進先出順序必須與串列前半的正向順序同步。
- 反轉後半時，慢指標必須停在第 `n / 2` 個節點。
- `PairSum2` 回傳前必須還原後半；只比較輸出值而不檢查串列拓撲會漏掉這個錯誤。
- 節點值可達 100,000，不能把多位數節點值串接成字元後再相加。

### 逐步範例

以 `head = [4,2,2,3]` 為例：

```plaintext
索引 0 與 3：4 + 3 = 7
索引 1 與 2：2 + 2 = 4
最大孿生和：max(7, 4) = 7
```

### 複雜度

令 `n` 為節點數。

| 方法 | 時間 | 輔助空間 | 結果空間 |
| --- | --- | --- | --- |
| `PairSum` | `O(n)` | `O(n)` | `O(1)` |
| `PairSum2` | `O(n)` | `O(1)` | `O(1)` |

兩個方法都回傳單一整數。`PairSum2` 會暫時改變後半連結，但在回傳前完整還原。

## Acceptance Harness

`Main` 是唯一的 console I/O 邊界。八個案例都用兩份獨立建立的串列分別執行 `PairSum` 與
`PairSum2`；每個方法各檢查結果與輸入拓撲，共 32 個檢查。拓撲檢查包含原節點參考、節點值、
順序及每個 `next` 參考。任何失敗都會將 process exit code 設為 `1`。

| # | 輸入摘要 | 預期 | 驗證目的 |
| ---: | --- | ---: | --- |
| 1 | `[5,4,2,1]` | 6 | 官方範例一，兩組孿生和相同 |
| 2 | `[4,2,2,3]` | 7 | 官方範例二，最大值在外層 |
| 3 | `[1,100000]` | 100001 | 官方範例三、最小節點數 |
| 4 | `[1,100000,100000,1]` | 200000 | 最大值在內層且達答案上限 |
| 5 | `[100000,1,2,100000]` | 200000 | 最大值在外層且達答案上限 |
| 6 | `[9,1,2,8,7,3]` | 12 | 六節點配對與中間非最大案例 |
| 7 | `[10,20,30,40]` | 50 | 多位數節點，防止字元相加錯誤 |
| 8 | 100,000 個節點，尾端為 100,000 | 100001 | 節點數上限與線性走訪 |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_2130/leetcode_2130/leetcode_2130.csproj --nologo
dotnet run --no-build --project leetcode_2130/leetcode_2130/leetcode_2130.csproj
```

若直接開啟題目根目錄 `leetcode_2130/`，使用：

```bash
dotnet build leetcode_2130/leetcode_2130.csproj --nologo
dotnet run --no-build --project leetcode_2130/leetcode_2130.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: 1 - Official example 1
Input: head=[5,4,2,1]
PASS PairSum result | Expected: 6 | Actual: 6
PASS PairSum input preserved | Expected: True | Actual: True
PASS PairSum2 result | Expected: 6 | Actual: 6
PASS PairSum2 input preserved | Expected: True | Actual: True

Case: 2 - Official example 2
Input: head=[4,2,2,3]
PASS PairSum result | Expected: 7 | Actual: 7
PASS PairSum input preserved | Expected: True | Actual: True
PASS PairSum2 result | Expected: 7 | Actual: 7
PASS PairSum2 input preserved | Expected: True | Actual: True

Case: 3 - Official example 3
Input: head=[1,100000]
PASS PairSum result | Expected: 100001 | Actual: 100001
PASS PairSum input preserved | Expected: True | Actual: True
PASS PairSum2 result | Expected: 100001 | Actual: 100001
PASS PairSum2 input preserved | Expected: True | Actual: True

Case: 4 - Maximum inner twin sum
Input: head=[1,100000,100000,1]
PASS PairSum result | Expected: 200000 | Actual: 200000
PASS PairSum input preserved | Expected: True | Actual: True
PASS PairSum2 result | Expected: 200000 | Actual: 200000
PASS PairSum2 input preserved | Expected: True | Actual: True

Case: 5 - Maximum outer twin sum
Input: head=[100000,1,2,100000]
PASS PairSum result | Expected: 200000 | Actual: 200000
PASS PairSum input preserved | Expected: True | Actual: True
PASS PairSum2 result | Expected: 200000 | Actual: 200000
PASS PairSum2 input preserved | Expected: True | Actual: True

Case: 6 - Six-node mixed twin sums
Input: head=[9,1,2,8,7,3]
PASS PairSum result | Expected: 12 | Actual: 12
PASS PairSum input preserved | Expected: True | Actual: True
PASS PairSum2 result | Expected: 12 | Actual: 12
PASS PairSum2 input preserved | Expected: True | Actual: True

Case: 7 - Multi-digit node values
Input: head=[10,20,30,40]
PASS PairSum result | Expected: 50 | Actual: 50
PASS PairSum input preserved | Expected: True | Actual: True
PASS PairSum2 result | Expected: 50 | Actual: 50
PASS PairSum2 input preserved | Expected: True | Actual: True

Case: 8 - Maximum node count
Input: head=[1 x 99999, 100000] (100000 nodes)
PASS PairSum result | Expected: 100001 | Actual: 100001
PASS PairSum input preserved | Expected: True | Actual: True
PASS PairSum2 result | Expected: 100001 | Actual: 100001
PASS PairSum2 input preserved | Expected: True | Actual: True

Summary: 32/32 checks passed.
```

## 專案結構

```plaintext
leetcode_2130/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_2130/
    ├── Program.cs
    └── leetcode_2130.csproj
```
