# LeetCode 187 - Repeated DNA Sequences

以 .NET 10 撰寫的 LeetCode 187 題解專案，包含兩種解法、可直接執行的範例輸出，以及對應的測試專案。

## 題目連結

- LeetCode: <https://leetcode.com/problems/repeated-dna-sequences/description/>
- LeetCode CN: <https://leetcode.cn/problems/repeated-dna-sequences/description/>

## 題目說明

給定一個只包含 `A`、`C`、`G`、`T` 的 DNA 字串 `s`，請找出所有長度剛好為 `10`、而且在字串中出現超過一次的子字串。

題目允許答案以任意順序回傳，因此解題重點不在排序，而在於：

- 如何高效掃描所有長度為 `10` 的視窗。
- 如何判斷某個長度為 `10` 的片段是否已經出現過。
- 如何避免把同一段重複 DNA 加入答案多次。

## 限制條件

- `1 <= s.length <= 10^5`
- `s[i]` 一定是 `A`、`C`、`G` 或 `T`
- 只需要找出長度為 `10` 的重複片段
- 回傳順序不限

## 解題概念與出發點

最直接的觀察是：題目只關心長度固定為 `10` 的子字串，所以整體問題可以轉換成：

1. 由左到右掃描每一個長度為 `10` 的視窗。
2. 記錄每個視窗出現過幾次。
3. 當某個片段第 `2` 次出現時，把它加入答案。

這個方向很自然，但有兩種不同層次的實作：

- 解法一：直接把長度為 `10` 的字串切出來，用字典統計次數。
- 解法二：利用 DNA 只有四種字元的特性，把每個字元壓成 `2 bit`，將整段長度 `10` 的序列轉成 `20 bit` 整數，再配合滑動視窗做 O(1) 更新。

兩種方法都正確，差別在於：

- 解法一較容易理解，適合先建立直覺。
- 解法二減少了重複建立子字串的成本，屬於更進一步的位元最佳化版本。

## 專案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
├── leetcode_187/
│   ├── Program.cs
│   └── leetcode_187.csproj
└── leetcode_187.Tests/
    ├── ProgramOutputTests.cs
    └── leetcode_187.Tests.csproj
```

## 解法一：Dictionary 統計固定長度子字串

### 設計說明

這個解法的核心是把每個長度為 `10` 的片段當成 key，使用 `Dictionary<string, int>` 統計它出現了幾次。

掃描流程如下：

1. 從 `i = 0` 開始，取出 `s[i..i+10)`。
2. 如果這段字串第一次出現，就記錄次數為 `1`。
3. 如果已經出現過，就把計數加 `1`。
4. 當計數剛好變成 `2` 時，把這段字串加入答案。

為什麼是「剛好等於 `2`」時加入，而不是「大於 `1`」就加入？

- 因為題目只需要每個重複片段出現在答案中一次。
- 如果某片段第 `3` 次、第 `4` 次出現時都繼續加入，答案就會重複。
- 所以第 `2` 次出現是最剛好的加入時機。

### 資料結構

- `Dictionary<string, int>`
  - key: 長度為 `10` 的 DNA 子字串
  - value: 該子字串出現次數
- `List<string>`
  - 收集所有已確認重複的片段

### 時間複雜度

- 外層一共掃描 `n - 10 + 1` 個視窗
- 每次都要建立長度為 `10` 的子字串
- 總時間可視為 `O(n * L)`，其中 `L = 10`
- 因為 `10` 是固定常數，實務上也可視為近似 `O(n)`

### 空間複雜度

- `O(n)`，最壞情況下許多長度 `10` 的片段都不同，需要存進字典

### 範例演示流程

以輸入 `AAAAACCCCCAAAAACCCCCCAAAAAGGGTTT` 為例：

1. 先看到 `AAAAACCCCC`，記錄次數為 `1`
2. 視窗持續右移，會陸續看到不同片段
3. 當再次看到 `AAAAACCCCC` 時，次數從 `1` 變 `2`
4. 因為這是第二次出現，所以把 `AAAAACCCCC` 加入答案
5. 之後再次掃描時，`CCCCCAAAAA` 也會在第二次出現時加入答案
6. 最終結果為 `["AAAAACCCCC", "CCCCCAAAAA"]`

再看輸入 `AAAAAAAAAAAAA`：

1. 第一個長度 `10` 視窗是 `AAAAAAAAAA`
2. 視窗每往右移一格，新的長度 `10` 視窗仍然是 `AAAAAAAAAA`
3. 第二次遇到時就加入答案
4. 後面即使繼續出現，也不再重複加入
5. 最終結果為 `["AAAAAAAAAA"]`

## 解法二：Hash + Sliding Window + Bitmask

### 設計說明

這個解法利用 DNA 字元只有四種：

- `A -> 00`
- `C -> 01`
- `G -> 10`
- `T -> 11`

因此一個長度為 `10` 的 DNA 片段，最多只需要 `20 bit` 就能表示。`int` 有 `32 bit`，足以容納。

這代表：

- 不必把每個視窗都保存成字串來比對
- 可以把視窗內容壓成整數，再用 `Dictionary<int, int>` 統計次數

### 為什麼滑動視窗能 O(1) 更新

假設目前視窗已經編碼成整數 `x`，當視窗往右移一格時：

1. `x << 2`
   - 每個字元占 `2 bit`
   - 左移 `2` 位，等於替新字元騰出位置
2. `| newCharCode`
   - 將新進入視窗的字元編碼塞到最低位
3. `& mask`
   - 視窗固定只保留 `10` 個字元，也就是低 `20 bit`
   - 更左邊已經滑出視窗的舊資料要用遮罩清掉

整合後的更新式為：

```text
x = ((x << 2) | codeOfNewChar) & ((1 << 20) - 1)
```

這樣每次視窗右移時，不需要重建整個長度 `10` 的字串編碼。

### 資料結構

- `Dictionary<char, int>`
  - 將 `A/C/G/T` 映射到 `0/1/2/3`
- `Dictionary<int, int>`
  - key: 長度 `10` 視窗壓縮後的整數值
  - value: 此整數編碼出現的次數
- `List<string>`
  - 儲存重複的原始 DNA 子字串

### 時間複雜度

- 初始化前 `9` 個字元的編碼：`O(L)`
- 主迴圈掃描所有視窗：`O(n)`
- 每次更新視窗編碼為 `O(1)`
- 整體時間複雜度為 `O(n)`

### 空間複雜度

- `O(n)`，最壞情況下需要記錄大量不同的視窗編碼

### 範例演示流程

仍以 `AAAAACCCCCAAAAACCCCCCAAAAAGGGTTT` 為例，先看前 `10` 個字元：

1. `AAAAACCCCC`
2. 將 `A` 視為 `00`，`C` 視為 `01`
3. 這十個字元會被壓成一個長度 `20 bit` 的整數
4. 記錄此整數編碼出現次數為 `1`

接著視窗右移：

1. 舊視窗左側字元滑出
2. 新字元從右側進入
3. 透過 `((x << 2) | newCode) & mask` 取得新視窗編碼
4. 若該編碼第二次出現，將對應的原始字串加入答案

這個流程會一路把所有長度 `10` 的視窗掃過，最後也得到：

```text
["AAAAACCCCC", "CCCCCAAAAA"]
```

### 與解法一的取捨

- 解法一
  - 優點：直觀、好讀、最容易對照題意
  - 缺點：每次都要建立新的子字串
- 解法二
  - 優點：視窗更新快、表示方式更緊湊
  - 缺點：位元運算較抽象，可讀性不如解法一

如果是面試或第一次寫這題，通常可以先從解法一開始，再進一步說明如何最佳化到解法二。

## 建置、測試與執行

### 建置

```bash
dotnet build leetcode_187/leetcode_187.csproj
```

### 測試

```bash
dotnet test leetcode_187.Tests/leetcode_187.Tests.csproj
```

目前測試會驗證主程式是否真的輸出可執行 sample 結果，而不是只保留預設主控台訊息。

### 執行範例

```bash
dotnet run --project leetcode_187/leetcode_187.csproj
```

## 實際執行輸出

以下為目前專案實際執行結果：

```text
Repeated DNA Sequences Sample Runner

Solution 1 - Dictionary Counting
Case 1
Input   : AAAAACCCCCAAAAACCCCCCAAAAAGGGTTT
Expected: ["AAAAACCCCC", "CCCCCAAAAA"]
Actual  : ["AAAAACCCCC", "CCCCCAAAAA"]
Pass    : True

Case 2
Input   : AAAAAAAAAAAAA
Expected: ["AAAAAAAAAA"]
Actual  : ["AAAAAAAAAA"]
Pass    : True

Case 3
Input   : ACGTACGT
Expected: []
Actual  : []
Pass    : True

Solution 2 - Bitmask Sliding Window
Case 1
Input   : AAAAACCCCCAAAAACCCCCCAAAAAGGGTTT
Expected: ["AAAAACCCCC", "CCCCCAAAAA"]
Actual  : ["AAAAACCCCC", "CCCCCAAAAA"]
Pass    : True

Case 2
Input   : AAAAAAAAAAAAA
Expected: ["AAAAAAAAAA"]
Actual  : ["AAAAAAAAAA"]
Pass    : True

Case 3
Input   : ACGTACGT
Expected: []
Actual  : []
Pass    : True
```

## 本次整理重點

- 在 `Main` 中加入可直接執行的測資
- 補齊主要方法的 XML `summary`
- 在關鍵演算法位置加入必要註解
- 新增 `README.md`，整理題意、限制、兩種解法與範例流程
- 新增 `leetcode_187.Tests` 測試專案，讓主程式輸出具備可驗證性
