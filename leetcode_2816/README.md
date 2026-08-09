# LeetCode 2816：翻倍以鏈結串列表示的數字

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![Language](https://img.shields.io/badge/Language-C%23-239120?logo=csharp)

這是一個以 .NET 10 Console App 實作的教學專案，示範如何將「最高位在前」的鏈結串列所表示的非負整數乘以二。專案保留原有的向右預看進位法，並加入堆疊回推與反轉鏈結串列兩種比較解法。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [三種解法比較](#三種解法比較)
- [解法一：向右預看進位](#解法一向右預看進位)
- [解法二：堆疊回推](#解法二堆疊回推)
- [解法三：反轉鏈結串列](#解法三反轉鏈結串列)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

給定一個非空單向鏈結串列 `head`，其中每個節點保存一個十進位位數，且最高位位於串列開頭。鏈結串列整體表示一個沒有多餘前導零的非負整數。

請將此數字乘以二，並回傳翻倍後鏈結串列的頭節點。

例如：

- `[1,8,9]` 表示 `189`，翻倍後為 `378`，因此回傳 `[3,7,8]`。
- `[9,9,9]` 表示 `999`，翻倍後為 `1998`，因此回傳 `[1,9,9,8]`。

題目來源：[LeetCode 2816. Double a Number Represented as a Linked List](https://leetcode.com/problems/double-a-number-represented-as-a-linked-list/)

## 限制條件

- 鏈結串列的節點數量介於 `1` 與 `10^4` 之間。
- `0 <= Node.val <= 9`。
- 輸入不會包含前導零，唯一例外是數字 `0` 本身。
- 輸入保證是非空鏈結串列，因此三個公開解法都以非空 `ListNode head` 為契約。

## 鏈結串列如何表示數字

每個節點只保存一個位數，節點順序與一般書寫數字的順序相同：

```text
[1] -> [8] -> [9] -> null
 百位    十位    個位
```

這個方向造成一個核心困難：乘法進位從低位傳向高位，但單向鏈結只能直接從高位走到低位。以 `189 × 2` 為例，必須先知道個位數 `9 × 2` 會向十位進一，十位的計算才完整。

## 解題概念與出發點

三種解法都在處理同一個方向不一致問題：

1. **改寫進位判斷方式**：不等待低位算完，而是觀察下一個節點的原值是否大於等於 5。
2. **保存走訪路徑**：先把節點壓入堆疊，再由尾端彈出，依正常直式乘法方向計算。
3. **改變鏈結方向**：先反轉串列，讓最低位成為頭節點，完成計算後再反轉回來。

若某位原值為 `digit`，它乘以二後是否向左進位，只取決於：

```text
digit >= 5
```

因為 `0..4` 的兩倍小於 10，而 `5..9` 的兩倍介於 10 與 18 之間。解法一正是利用這個條件避開反向走訪。

> [!IMPORTANT]
> 三種解法都會原地修改輸入節點的值；解法三還會在計算過程中暫時改寫 `next`。若最高位產生進位，三種解法都會新增一個頭節點。範例程式因此會為每個解法重新建立獨立輸入。

## 三種解法比較

| 解法 | 方法 | 時間複雜度 | 額外空間 | 輸入副作用 |
| --- | --- | --- | --- | --- |
| `DoubleIt` | 向右預看下一位是否進位 | O(n) | O(1) | 修改節點值，可能新增頭節點 |
| `DoubleIt2` | 堆疊由低位往高位回推 | O(n) | O(n) | 修改節點值，可能新增頭節點 |
| `DoubleIt3` | 反轉、計算、再反轉 | O(n) | O(1) | 修改節點值與鏈結，可能新增節點 |

複雜度中的額外空間不計回傳結果本身。`DoubleIt` 最精簡且空間最佳；`DoubleIt2` 最貼近一般直式乘法；`DoubleIt3` 則示範如何用鏈結反轉換取 O(1) 額外空間。

## 解法一：向右預看進位

### 設計說明

`DoubleIt` 從左到右走訪，但不保存傳回來的進位。計算目前節點時，直接查看下一節點的**原始值**：

- 目前位的新個位值為 `(current.val * 2) % 10`。
- 若 `current.next.val >= 5`，下一位乘以二後一定會向目前位進一，所以目前結果再加一。
- 若原始最高位大於等於 5，結果會多一位；先在開頭補一個值為 0 的節點，讓它承接進位。

走訪到某節點時，只會修改目前節點，不會提前修改下一節點，因此預看的仍是下一節點的原始值。

### 正確性理由

任一位乘以二後最多只會產生 0 或 1 的進位。下一位原值大於等於 5，等價於下一位乘以二後產生 1；因此目前位使用「自身兩倍的個位數，加上下一位的進位」即可得到正確結果。最高位若需要進位，預先加入的 0 節點也會透過同一規則變為 1，所以所有位數都能在一次正向走訪中完成。

### `[9,9,9]` 演示

1. 原最高位 `9 >= 5`，先補 0，得到 `[0,9,9,9]`。
2. 新頭節點：`0 × 2 % 10 = 0`；下一位是 9，補進位後為 1。
3. 第一個 9：`9 × 2 % 10 = 8`；下一位是 9，補進位後為 9。
4. 第二個 9 同理變為 9。
5. 最後一個 9 沒有下一位，只保留 `18` 的個位數 8。
6. 結果為 `[1,9,9,8]`。

## 解法二：堆疊回推

### 設計說明

`DoubleIt2` 先由頭到尾走訪，將每個 `ListNode` 參考壓入堆疊。堆疊的後進先出特性讓尾端最低位最先被取出，之後即可使用標準直式乘法：

```text
doubled = current.val * 2 + carry
current.val = doubled % 10
carry = doubled / 10
```

所有節點處理完後，若 `carry` 仍為 1，就在原頭節點前新增一個節點。

### 正確性理由

堆疊彈出順序是從最低位到最高位。處理任一位時，`carry` 已經是右側相鄰低位產生的進位，因此公式與紙筆直式乘法完全相同。迴圈結束後留下的 `carry` 只可能屬於最高位，將它放到新頭節點即可得到完整結果。

### `[9,9,9]` 演示

1. 依序將三個 9 節點壓入堆疊。
2. 彈出最低位：`9 × 2 + 0 = 18`，寫入 8，`carry = 1`。
3. 彈出十位：`9 × 2 + 1 = 19`，寫入 9，`carry = 1`。
4. 彈出百位：`9 × 2 + 1 = 19`，寫入 9，`carry = 1`。
5. 堆疊清空但仍有進位，在頭部加入 1。
6. 結果為 `[1,9,9,8]`。

## 解法三：反轉鏈結串列

### 設計說明

`DoubleIt3` 透過兩次原地反轉改變計算方向：

1. 反轉輸入，讓最低位成為頭節點。
2. 由新頭節點開始，以 `digit * 2 + carry` 更新每一位。
3. 若最後仍有進位，將新節點接在目前串列尾端；此處正好代表原數字的最高位外側。
4. 再反轉一次，恢復最高位在前的題目格式。

`ReverseList` 每一步先保存尚未處理的 `next`，再將目前節點指回前一個節點，因此不需要額外集合。

### 正確性理由

第一次反轉後，走訪順序與進位傳遞方向一致，所以每個節點都能取得已算出的低位進位，並套用標準乘法公式。若最高位產生額外進位，它會被接在反向串列尾端；第二次反轉後，此節點自然成為結果的新頭。兩次反轉也會恢復其他節點的高低位順序。

### `[9,9,9]` 演示

1. 反轉後仍顯示為 `[9,9,9]`；雖然值相同，節點方向已顛倒。
2. 第一個節點：`9 × 2 + 0 = 18`，得到 8，進位 1。
3. 第二、第三個節點各得到 9，進位持續為 1。
4. 將最後進位接到尾端，反向串列成為 `[8,9,9,1]`。
5. 再次反轉，結果為 `[1,9,9,8]`。

## 測試設計

`Main` 使用六組固定案例，涵蓋零值、完全無進位、一般內部進位、單節點最高位進位、連續進位與原本專案案例。每組案例都執行三個解法，共 18 項檢查。

每次執行解法前都呼叫 `BuildList` 建立新節點，因此其中一個原地修改解法不會污染下一個解法的輸入。實際結果會轉成陣列逐位比較；任一檢查失敗時，程式最後會設定非零結束碼，方便命令列或 CI 判斷失敗。

## 專案結構

```text
leetcode_2816/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_2816.sln
└── leetcode_2816/
    ├── leetcode_2816.csproj
    └── Program.cs
```

## 建置與執行

需要安裝支援 `net10.0` 的 .NET 10 SDK。請從 repository 根目錄執行：

```bash
dotnet restore leetcode_2816/leetcode_2816.csproj
dotnet build leetcode_2816/leetcode_2816.csproj --nologo
dotnet run --no-build --project leetcode_2816/leetcode_2816.csproj
```

專案目前沒有獨立的自動化測試專案；Console 內建的 Expected/Actual/PASS-FAIL 案例就是行為驗證入口。

## 實際執行結果

以下內容來自上述 `dotnet run --no-build` 命令：

```text
Case: 零值
Input: [0]
  Solution: DoubleIt - 向右預看
  Expected: [0]
  Actual: [0]
  Result: PASS
  Solution: DoubleIt2 - 堆疊回推
  Expected: [0]
  Actual: [0]
  Result: PASS
  Solution: DoubleIt3 - 反轉鏈結串列
  Expected: [0]
  Actual: [0]
  Result: PASS

Case: 無進位
Input: [1,2,3]
  Solution: DoubleIt - 向右預看
  Expected: [2,4,6]
  Actual: [2,4,6]
  Result: PASS
  Solution: DoubleIt2 - 堆疊回推
  Expected: [2,4,6]
  Actual: [2,4,6]
  Result: PASS
  Solution: DoubleIt3 - 反轉鏈結串列
  Expected: [2,4,6]
  Actual: [2,4,6]
  Result: PASS

Case: 官方一般案例
Input: [1,8,9]
  Solution: DoubleIt - 向右預看
  Expected: [3,7,8]
  Actual: [3,7,8]
  Result: PASS
  Solution: DoubleIt2 - 堆疊回推
  Expected: [3,7,8]
  Actual: [3,7,8]
  Result: PASS
  Solution: DoubleIt3 - 反轉鏈結串列
  Expected: [3,7,8]
  Actual: [3,7,8]
  Result: PASS

Case: 單節點最高位進位
Input: [5]
  Solution: DoubleIt - 向右預看
  Expected: [1,0]
  Actual: [1,0]
  Result: PASS
  Solution: DoubleIt2 - 堆疊回推
  Expected: [1,0]
  Actual: [1,0]
  Result: PASS
  Solution: DoubleIt3 - 反轉鏈結串列
  Expected: [1,0]
  Actual: [1,0]
  Result: PASS

Case: 連續進位與重複值
Input: [9,9,9]
  Solution: DoubleIt - 向右預看
  Expected: [1,9,9,8]
  Actual: [1,9,9,8]
  Result: PASS
  Solution: DoubleIt2 - 堆疊回推
  Expected: [1,9,9,8]
  Actual: [1,9,9,8]
  Result: PASS
  Solution: DoubleIt3 - 反轉鏈結串列
  Expected: [1,9,9,8]
  Actual: [1,9,9,8]
  Result: PASS

Case: 保留既有案例
Input: [5,1,1]
  Solution: DoubleIt - 向右預看
  Expected: [1,0,2,2]
  Actual: [1,0,2,2]
  Result: PASS
  Solution: DoubleIt2 - 堆疊回推
  Expected: [1,0,2,2]
  Actual: [1,0,2,2]
  Result: PASS
  Solution: DoubleIt3 - 反轉鏈結串列
  Expected: [1,0,2,2]
  Actual: [1,0,2,2]
  Result: PASS

Summary: 18/18 checks passed.
```
