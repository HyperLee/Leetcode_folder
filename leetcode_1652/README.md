# LeetCode 1652：拆炸彈（Defuse the Bomb）

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/download/dotnet/10.0)

本專案是 LeetCode 1652 的 .NET 10 console 教學範例。程式保留兩種既有的固定長度滑動視窗解法，並加入一種容易理解與驗證的暴力模擬解法。`Main` 內建六組可重複執行的案例，會同時檢查答案及三種解法是否保持輸入陣列不變。

## 目錄

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：取模滑動視窗](#解法一取模滑動視窗)
- [解法二：雙倍陣列滑動視窗](#解法二雙倍陣列滑動視窗)
- [解法三：暴力模擬](#解法三暴力模擬)
- [三種解法比較](#三種解法比較)
- [可執行測試資料](#可執行測試資料)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)
- [專案結構](#專案結構)

## 題目說明

官方題目：

- [LeetCode 英文題目](https://leetcode.com/problems/defuse-the-bomb/)
- [LeetCode 中文題目](https://leetcode.cn/problems/defuse-the-bomb/description/)

給定一個長度為 `n` 的循環陣列 `code` 與整數 `k`，必須同時替換陣列中的每一個位置：

- `k > 0`：第 `i` 個位置替換為後面 `k` 個數字的總和。
- `k < 0`：第 `i` 個位置替換為前面 `|k|` 個數字的總和。
- `k == 0`：所有位置都替換為 `0`。

「循環」表示 `code[n - 1]` 的下一個元素是 `code[0]`，而 `code[0]` 的前一個元素是 `code[n - 1]`。所有位置都以原始 `code` 為基準計算，不能讓先算出的答案影響後續位置。

例如：

```text
code = [2, 4, 9, 3], k = -2
```

每個位置都取前兩個元素：

```text
result[0] = 3 + 9 = 12
result[1] = 2 + 3 = 5
result[2] = 4 + 2 = 6
result[3] = 9 + 4 = 13
```

答案為 `[12, 5, 6, 13]`。

## 限制條件

| 條件 | 官方範圍 |
| --- | --- |
| 陣列長度 | `n == code.Length` |
| `n` | `1 <= n <= 100` |
| 元素值 | `1 <= code[i] <= 100` |
| `k` | `-(n - 1) <= k <= n - 1` |

由於 `|k|` 最多是 `n - 1`，一個位置不會把自己納入加總。題目也保證輸入陣列非空，因此本專案不額外定義空陣列或超出範圍的錯誤處理行為。

## 解題概念與出發點

### 1. 先釐清方向與視窗大小

無論 `k` 是正是負，每個答案都來自固定數量的相鄰元素：

```text
windowSize = |k|
```

差別只在第一個視窗的位置：

- `k > 0`：`result[0]` 使用原陣列索引 `[1, k]`。
- `k < 0`：`result[0]` 使用原陣列索引 `[n - |k|, n - 1]`。
- `k == 0`：視窗大小為零，答案自然全部是零。

### 2. 循環索引的兩種表示方式

本專案示範兩種處理環狀邊界的方法：

1. 使用 `% n` 將超出右邊界的索引映射回陣列開頭。
2. 建立 `code + code` 的雙倍陣列，使跨界視窗變成普通連續區間。

第一種方法節省額外空間；第二種方法則讓左右邊界更直觀。

### 3. 固定長度視窗可以重用總和

從 `result[i]` 移動到 `result[i + 1]` 時，整個視窗只改變兩個元素：

```text
新視窗總和 = 舊視窗總和 - 離開的元素 + 進入的元素
```

因此兩種滑動視窗解法都不必為每一個位置重新加總 `|k|` 次，可以把時間複雜度從 `O(n × |k|)` 降為 `O(n)`。

### 4. 輸入不變契約

三個公開方法都建立新的結果陣列，不會修改呼叫端傳入的 `code`。測試入口仍會為每次呼叫建立獨立複本，並在執行後比較內容，讓這項契約可以被實際驗證。

## 解法一：取模滑動視窗

### 設計說明

`Decrypt` 不建立雙倍陣列，而是維護：

- `rightExclusive`：目前視窗右界的下一個索引，可持續向右增長。
- `windowSize`：固定為 `|k|`。
- `windowSum`：目前視窗內所有元素的總和。

建立第一個視窗後，每次先把 `windowSum` 寫入答案，再計算：

```text
enteringIndex = rightExclusive % n
leavingIndex  = (rightExclusive - windowSize) % n
```

加入 `enteringIndex`、扣除 `leavingIndex`，便得到下一個位置的視窗總和。

### 範例演示

使用 `code = [5, 7, 1, 4]`、`k = 3`：

第一個視窗是索引 `[1, 2, 3]`，所以：

```text
windowSum = 7 + 1 + 4 = 12
```

| 答案位置 | 使用的循環索引 | 視窗內容 | 寫入答案 | 下一輪更新 |
| --- | --- | --- | --- | --- |
| `0` | `1, 2, 3` | `7, 1, 4` | `12` | 扣 `7`、加 `5` |
| `1` | `2, 3, 0` | `1, 4, 5` | `10` | 扣 `1`、加 `7` |
| `2` | `3, 0, 1` | `4, 5, 7` | `16` | 扣 `4`、加 `1` |
| `3` | `0, 1, 2` | `5, 7, 1` | `13` | 已完成 |

因此得到 `[12, 10, 16, 13]`。

### 正確性說明

初始化時，`windowSum` 正好包含 `result[0]` 需要的 `|k|` 個元素。每次右移時只移除舊視窗最左側元素並加入新視窗最右側元素，所以更新後仍精確包含下一個答案需要的元素。取模讓超出 `n - 1` 的索引回到陣列開頭，因此環狀邊界也維持相同不變量。

### 複雜度

- 時間：`O(n)`。初始化最多處理 `n - 1` 個元素，之後每個位置以常數時間更新。
- 回傳陣列：`O(n)`。
- 輸出以外額外空間：`O(1)`。

## 解法二：雙倍陣列滑動視窗

### 設計說明

`Decrypt2` 先建立：

```text
extendedCode = code + code
```

所有原本跨越陣列尾端的環狀區間，都能在 `extendedCode` 中表示為普通連續區間。接著用 `left`、`right` 維護包含左右端點的固定長度視窗：

- `k > 0`：第一個視窗為 `[1, k]`。
- `k < 0`：第一個視窗為 `[n + k, n - 1]`。

每產生一個答案後，扣除 `extendedCode[left]`、加入 `extendedCode[right + 1]`，再讓兩個邊界一起右移。

### 範例演示

使用 `code = [2, 4, 9, 3]`、`k = -2`：

```text
extendedCode = [2, 4, 9, 3, 2, 4, 9, 3]
left = n + k = 2
right = n - 1 = 3
```

| 答案位置 | `left..right` | 視窗內容 | 寫入答案 | 更新後邊界 |
| --- | --- | --- | --- | --- |
| `0` | `2..3` | `9, 3` | `12` | `3..4` |
| `1` | `3..4` | `3, 2` | `5` | `4..5` |
| `2` | `4..5` | `2, 4` | `6` | `5..6` |
| `3` | `5..6` | `4, 9` | `13` | 已完成 |

因此得到 `[12, 5, 6, 13]`。

### 正確性說明

雙倍陣列的第二份副本與第一份內容相同，因此任何長度不超過 `n - 1` 的環狀區間，都對應到其中一段連續區間。初始化邊界精確選出第零個答案需要的元素；之後左右邊界同步右移，始終保持視窗長度為 `|k|`，所以每個 `windowSum` 都是對應位置的正確答案。

### 複雜度

- 時間：`O(n)`。複製陣列、初始化視窗與產生答案都只需線性時間。
- 回傳陣列：`O(n)`。
- 輸出以外額外空間：`O(n)`，用於雙倍陣列。

## 解法三：暴力模擬

### 設計說明

`DecryptBruteForce` 直接翻譯題意：對每個位置 `i`，依 `k` 的正負決定方向，逐步走訪 `1` 到 `|k|`：

```text
direction = k > 0 ? 1 : -1
index = (i + direction × step + n) % n
```

加上 `n` 可以避免向前走時產生負索引，再以 `% n` 映射回合法範圍。當 `k == 0` 時，內層迴圈不執行，預設為零的結果陣列就是答案。

### 範例演示

使用 `code = [5, 7, 1, 4]`、`k = 3`，方向為 `+1`：

| 答案位置 | 逐步索引 | 加總內容 | 結果 |
| --- | --- | --- | --- |
| `0` | `1, 2, 3` | `7 + 1 + 4` | `12` |
| `1` | `2, 3, 0` | `1 + 4 + 5` | `10` |
| `2` | `3, 0, 1` | `4 + 5 + 7` | `16` |
| `3` | `0, 1, 2` | `5 + 7 + 1` | `13` |

這個流程與題目敘述最接近，很適合作為理解基準，也能用來交叉檢查滑動視窗解法。

### 正確性說明

對每個位置，內層迴圈恰好執行 `|k|` 次。`direction` 保證正數 `k` 只走訪後方元素、負數 `k` 只走訪前方元素，而取模保證每一步都遵循循環陣列順序。因此累加的集合與題目要求完全相同。

### 複雜度

- 時間：`O(n × |k|)`。
- 回傳陣列：`O(n)`。
- 輸出以外額外空間：`O(1)`。

## 三種解法比較

| 比較項目 | `Decrypt` | `Decrypt2` | `DecryptBruteForce` |
| --- | --- | --- | --- |
| 核心方法 | 取模固定長度滑動視窗 | 雙倍陣列固定長度滑動視窗 | 依題意逐位置模擬 |
| 循環處理 | `% n` | `code + code` | 方向與 `% n` |
| 時間複雜度 | `O(n)` | `O(n)` | `O(n × |k|)` |
| 輸出外空間 | `O(1)` | `O(n)` | `O(1)` |
| 修改輸入 | 否 | 否 | 否 |
| 教學重點 | 視窗不變量與模數索引 | 將環狀問題線性化 | 直接對照題意、適合作為基準 |

若重視效能與空間，`Decrypt` 最完整；若希望索引容易觀察，`Decrypt2` 最直觀；若剛開始理解題目或需要交叉驗證，`DecryptBruteForce` 最容易追蹤。

## 可執行測試資料

`Main` 會執行六組固定案例，每組分別呼叫三種解法，因此共有 18 項解法級檢查。每一項都驗證：

1. `Actual` 是否與 `Expected` 完全相同。
2. 解法執行後，工作輸入是否仍與原始 `code` 相同。
3. 任一檢查失敗時，程式是否回傳非零結束代碼。

| 案例 | `code` | `k` | 預期結果 | 涵蓋重點 |
| --- | --- | --- | --- | --- |
| 官方範例（正） | `[5,7,1,4]` | `3` | `[12,10,16,13]` | 向後跨界 |
| 官方範例（零） | `[1,2,3,4]` | `0` | `[0,0,0,0]` | 零長度視窗 |
| 官方範例（負） | `[2,4,9,3]` | `-2` | `[12,5,6,13]` | 向前跨界 |
| 最小長度 | `[8]` | `0` | `[0]` | `n = 1` |
| 正向最大視窗 | `[1,1,1,1]` | `3` | `[3,3,3,3]` | `k = n - 1`、重複值 |
| 反向最大視窗 | `[10,20,30,40,50]` | `-4` | `[140,130,120,110,100]` | `k = -(n - 1)` |

題目限制不接受空陣列，因此測試不加入不合法的空輸入；邊界改由 `n = 1` 與正負最大視窗覆蓋。

## 建置與執行

在本 repository 根目錄執行：

```bash
dotnet restore leetcode_1652/leetcode_1652.csproj
dotnet build leetcode_1652/leetcode_1652.csproj --nologo
dotnet run --no-build --project leetcode_1652/leetcode_1652.csproj
```

若只執行 `dotnet run --no-build`，請先確認已有最新建置結果。成功時最後一行應為：

```text
總結：18/18 項測試通過
```

## 實際執行結果

以下內容來自本專案的最新實際執行：

```text

案例：1. 官方範例（k > 0）
Input：code = [5, 7, 1, 4], k = 3
解法一：Decrypt（取模滑動視窗）
Expected：[12, 10, 16, 13]
Actual：[12, 10, 16, 13]
Input unchanged：PASS
Result：PASS
解法二：Decrypt2（雙倍陣列滑動視窗）
Expected：[12, 10, 16, 13]
Actual：[12, 10, 16, 13]
Input unchanged：PASS
Result：PASS
解法三：DecryptBruteForce（暴力模擬）
Expected：[12, 10, 16, 13]
Actual：[12, 10, 16, 13]
Input unchanged：PASS
Result：PASS

案例：2. 官方範例（k = 0）
Input：code = [1, 2, 3, 4], k = 0
解法一：Decrypt（取模滑動視窗）
Expected：[0, 0, 0, 0]
Actual：[0, 0, 0, 0]
Input unchanged：PASS
Result：PASS
解法二：Decrypt2（雙倍陣列滑動視窗）
Expected：[0, 0, 0, 0]
Actual：[0, 0, 0, 0]
Input unchanged：PASS
Result：PASS
解法三：DecryptBruteForce（暴力模擬）
Expected：[0, 0, 0, 0]
Actual：[0, 0, 0, 0]
Input unchanged：PASS
Result：PASS

案例：3. 官方範例（k < 0）
Input：code = [2, 4, 9, 3], k = -2
解法一：Decrypt（取模滑動視窗）
Expected：[12, 5, 6, 13]
Actual：[12, 5, 6, 13]
Input unchanged：PASS
Result：PASS
解法二：Decrypt2（雙倍陣列滑動視窗）
Expected：[12, 5, 6, 13]
Actual：[12, 5, 6, 13]
Input unchanged：PASS
Result：PASS
解法三：DecryptBruteForce（暴力模擬）
Expected：[12, 5, 6, 13]
Actual：[12, 5, 6, 13]
Input unchanged：PASS
Result：PASS

案例：4. 最小長度
Input：code = [8], k = 0
解法一：Decrypt（取模滑動視窗）
Expected：[0]
Actual：[0]
Input unchanged：PASS
Result：PASS
解法二：Decrypt2（雙倍陣列滑動視窗）
Expected：[0]
Actual：[0]
Input unchanged：PASS
Result：PASS
解法三：DecryptBruteForce（暴力模擬）
Expected：[0]
Actual：[0]
Input unchanged：PASS
Result：PASS

案例：5. 正向最大視窗與重複值
Input：code = [1, 1, 1, 1], k = 3
解法一：Decrypt（取模滑動視窗）
Expected：[3, 3, 3, 3]
Actual：[3, 3, 3, 3]
Input unchanged：PASS
Result：PASS
解法二：Decrypt2（雙倍陣列滑動視窗）
Expected：[3, 3, 3, 3]
Actual：[3, 3, 3, 3]
Input unchanged：PASS
Result：PASS
解法三：DecryptBruteForce（暴力模擬）
Expected：[3, 3, 3, 3]
Actual：[3, 3, 3, 3]
Input unchanged：PASS
Result：PASS

案例：6. 反向最大視窗
Input：code = [10, 20, 30, 40, 50], k = -4
解法一：Decrypt（取模滑動視窗）
Expected：[140, 130, 120, 110, 100]
Actual：[140, 130, 120, 110, 100]
Input unchanged：PASS
Result：PASS
解法二：Decrypt2（雙倍陣列滑動視窗）
Expected：[140, 130, 120, 110, 100]
Actual：[140, 130, 120, 110, 100]
Input unchanged：PASS
Result：PASS
解法三：DecryptBruteForce（暴力模擬）
Expected：[140, 130, 120, 110, 100]
Actual：[140, 130, 120, 110, 100]
Input unchanged：PASS
Result：PASS

總結：18/18 項測試通過
```

## 專案結構

```text
leetcode_1652/
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_1652.sln
└── leetcode_1652/
    ├── leetcode_1652.csproj
    └── Program.cs
```

- `Program.cs`：三種演算法、XML 文件、測試資料與輸出格式。
- `leetcode_1652.csproj`：目標為 `net10.0` 的 console 專案設定。
- `docs/readme-template.md`：本 README 使用的初始文件指引。