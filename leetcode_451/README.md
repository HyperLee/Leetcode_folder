# LeetCode 451 - Sort Characters By Frequency

這個專案使用 C# 14 與 .NET 10 實作 LeetCode 451「Sort Characters By Frequency」。程式會將輸入字串中的字元依照出現頻率由高到低重新排列；若多個字元頻率相同，任一相對順序都符合題意。

## 題目說明

- 英文題目：451. Sort Characters By Frequency
- 中文題目：根據字元出現頻率排序
- 題目連結：
  - <https://leetcode.com/problems/sort-characters-by-frequency/description/>
  - <https://leetcode.cn/problems/sort-characters-by-frequency/description/>

給定一個字串 `s`，請依照字元在字串中出現的頻率，由高到低排序並回傳新的字串。輸出字串必須包含與輸入相同的字元與數量；頻率相同的字元可以用任意順序排列。

## 限制條件

- `1 <= s.length <= 5 * 10^5`
- `s` 只包含英文大小寫字母與數字
- 大寫與小寫視為不同字元，例如 `A` 與 `a` 不相同

## 解題概念與出發點

題目核心不是依字母大小排序，而是依「每個字元出現幾次」排序。因此解題出發點是先把原字串轉換成頻率表，再根據頻率表的數值排序，最後按照排序結果重建字串。

目前專案提供兩種解法：

- 解法一：`Dictionary<char, int>` 統計頻率、LINQ 排序、`StringBuilder` 組合輸出。
- 解法二：`Dictionary<char, int>` 統計頻率、桶排序、`StringBuilder` 組合輸出。

## 解法一：Dictionary + Sort + StringBuilder

### 設計說明

1. 使用 `Dictionary<char, int>` 掃描輸入字串，記錄每個字元的出現次數。
2. 將字元與頻率的配對依照頻率遞減排序。
3. 依排序後的頻率重複加入字元，使用 `StringBuilder` 避免大量字串串接造成額外成本。
4. 回傳重建後的字串。

時間複雜度為 `O(n + k log k)`，其中 `n` 是字串長度，`k` 是不同字元數量。空間複雜度為 `O(n + k)`，包含頻率表與輸出字串。

### 範例演示流程

#### 範例一：`tree`

1. 頻率統計：`t: 1`、`r: 1`、`e: 2`
2. 依頻率排序：`e` 先於 `t`、`r`
3. 重建輸出：目前程式輸出 `eetr`；`eert` 也符合題意

#### 範例二：`cccaaa`

1. 頻率統計：`c: 3`、`a: 3`
2. 兩個字元頻率相同，因此 `cccaaa` 或 `aaaccc` 都是有效答案
3. 重建輸出：目前程式輸出 `cccaaa`

#### 範例三：`Aabb`

1. 頻率統計：`A: 1`、`a: 1`、`b: 2`
2. `b` 的頻率最高，需排在前面；`A` 與 `a` 視為不同字元
3. 重建輸出：目前程式輸出 `bbAa`

## 解法二：桶排序

### 設計說明

1. 遍歷輸入字串，使用 `Dictionary<char, int>` 統計每個字元出現次數，同時記錄最高頻率 `maxFreq`。
2. 建立長度為 `maxFreq + 1` 的桶陣列，讓索引值代表字元出現頻率。
3. 將每個字元依照它的出現頻率放入對應桶中，例如出現 `3` 次的字元會放入 `buckets[3]`。
4. 從 `maxFreq` 往 `1` 反向遍歷桶，將桶內字元依照該頻率重複加入 `StringBuilder`。
5. 回傳重建後的字串。

時間複雜度為 `O(n + k + maxFreq)`，其中 `n` 是字串長度，`k` 是不同字元數量，且 `maxFreq <= n`。空間複雜度為 `O(n + k + maxFreq)`，包含桶、頻率表與輸出字串。

### 範例演示流程

#### 範例一：`tree`

1. 頻率統計：`t: 1`、`r: 1`、`e: 2`，最高頻率 `maxFreq = 2`
2. 建立桶：`buckets[1]` 放入 `t`、`r`，`buckets[2]` 放入 `e`
3. 從高頻桶開始重建：先加入 `ee`，再加入 `t`、`r`
4. 重建輸出：目前程式輸出 `eetr`；`eert` 也符合題意

#### 範例二：`cccaaa`

1. 頻率統計：`c: 3`、`a: 3`，最高頻率 `maxFreq = 3`
2. `c` 與 `a` 都放入 `buckets[3]`
3. 從 `buckets[3]` 取出字元並各重複 `3` 次
4. 重建輸出：目前程式輸出 `cccaaa`；`aaaccc` 也符合題意

#### 範例三：`Aabb`

1. 頻率統計：`A: 1`、`a: 1`、`b: 2`，最高頻率 `maxFreq = 2`
2. `buckets[1]` 放入 `A`、`a`，`buckets[2]` 放入 `b`
3. 從高頻桶開始重建：先加入 `bb`，再加入 `A`、`a`
4. 重建輸出：目前程式輸出 `bbAa`

## 執行方式

需求：

- .NET 10 SDK

建置專案：

```powershell
dotnet build .\leetcode_451\leetcode_451.csproj
```

執行範例資料：

```powershell
dotnet run --project .\leetcode_451\leetcode_451.csproj
```

目前範例輸出：

```text
Input: tree
Method 1 Output: eetr
Method 2 Output: eetr

Input: cccaaa
Method 1 Output: cccaaa
Method 2 Output: cccaaa

Input: Aabb
Method 1 Output: bbAa
Method 2 Output: bbAa
```

> [!NOTE]
> 此專案目前沒有獨立測試專案，驗證方式是執行建置與 console 範例資料；`Main` 會同時輸出兩種解法的結果。

## 專案結構

```text
.
├── docs/
│   └── readme-template.md
├── leetcode_451/
│   ├── Program.cs
│   └── leetcode_451.csproj
├── AGENTS.md
├── README.md
└── leetcode_451.slnx
```
