# LeetCode 1207 — 獨一無二的出現次數

這個 .NET 10 主控台專案示範如何判斷整數陣列中，每個不同數值的「出現次數」是否彼此唯一。專案保留兩種解法：線性時間的 Dictionary + HashSet，以及先複製、排序後再分組計數的 Sorting + HashSet。

- [題目說明](#題目說明)
- [解題概念與出發點](#解題概念與出發點)
- [解法一dictionary--hashset](#解法一dictionary--hashset)
- [解法二sorting--hashset](#解法二sorting--hashset)
- [兩種解法比較](#兩種解法比較)
- [建置與執行](#建置與執行)

## 題目說明

給定一個整數陣列 `arr`，若每個不同數值的出現次數皆不相同，回傳 `true`；否則回傳 `false`。

題目連結：[1207. Unique Number of Occurrences](https://leetcode.com/problems/unique-number-of-occurrences/)

### 範例

| 範例 | 輸入 | 各數值的出現次數 | 輸出 |
| --- | --- | --- | --- |
| 1 | `[1,2,2,1,1,3]` | `1 → 3`、`2 → 2`、`3 → 1`，次數互不重複 | `true` |
| 2 | `[1,2]` | `1 → 1`、`2 → 1`，兩者的次數相同 | `false` |
| 3 | `[-3,0,1,-3,1,1,1,-3,10,0]` | `-3 → 3`、`0 → 2`、`1 → 4`、`10 → 1` | `true` |

### 限制條件

- `1 <= arr.length <= 1000`
- `-1000 <= arr[i] <= 1000`

## 解題概念與出發點

這題不是檢查「陣列中的數值是否重複」，而是檢查「不同數值的出現次數是否重複」。因此問題可以拆成兩個階段：

1. 找出每個數值出現幾次。
2. 檢查這些次數是否全部唯一。

以 `[1,2,2,1,1,3]` 為例，第一階段得到 `{ 1:3, 2:2, 3:1 }`，第二階段檢查的是 `[3,2,1]`。因為 `3`、`2`、`1` 互不相同，所以答案是 `true`。

`HashSet<int>` 很適合處理第二階段：呼叫 `Add(count)` 時，若該次數已存在，`Add` 會回傳 `false`，可以立即判定答案為 `false`。

## 解法一：Dictionary + HashSet

對應方法：`UniqueOccurrences(int[] arr)`

### 設計說明

這個解法直接使用兩種雜湊集合，各自負責一個明確工作：

1. `Dictionary<int, int>` 保存「數值 → 出現次數」。
2. 走訪輸入陣列；第一次看到某個數值時記為 `1`，之後再次看到便將次數加一。
3. `HashSet<int>` 保存已經看過的出現次數。
4. 走訪 Dictionary 的每個計數，嘗試加入 HashSet。
5. 如果 `HashSet.Add` 回傳 `false`，表示另一個數值已使用相同次數，立刻回傳 `false`。
6. 所有次數都成功加入後，回傳 `true`。

### 範例演示：`[1,2,2,1,1,3]`

第一階段建立 Dictionary：

| 讀取數值 | Dictionary 狀態 |
| --- | --- |
| `1` | `{ 1:1 }` |
| `2` | `{ 1:1, 2:1 }` |
| `2` | `{ 1:1, 2:2 }` |
| `1` | `{ 1:2, 2:2 }` |
| `1` | `{ 1:3, 2:2 }` |
| `3` | `{ 1:3, 2:2, 3:1 }` |

第二階段檢查次數：

| 檢查次數 | HashSet 狀態 | 結果 |
| --- | --- | --- |
| `3` | `{ 3 }` | 尚未重複 |
| `2` | `{ 3,2 }` | 尚未重複 |
| `1` | `{ 3,2,1 }` | 尚未重複 |

所有出現次數都能成功加入 HashSet，因此回傳 `true`。

### 為什麼正確

Dictionary 完整記錄每個不同數值的實際出現次數。HashSet 則保證同一個次數最多只能加入一次；只要第二次加入相同次數，就代表至少兩個不同數值具有相同頻率，恰好違反題目條件。若所有次數皆能加入，便表示它們彼此唯一。

### 複雜度

- 時間複雜度：平均 `O(n)`。建立 Dictionary 需要 `O(n)`，檢查至多 `n` 個不同數值也需要 `O(n)`。
- 空間複雜度：`O(n)`。Dictionary 與 HashSet 最多都保存與不同數值數量同階的資料。

## 解法二：Sorting + HashSet

對應方法：`UniqueOccurrencesBySorting(int[] arr)`

### 設計說明

排序後，相同數值一定會連續排列，因此不需要 Dictionary 也能算出每個數值的出現次數：

1. 使用 `Clone()` 複製輸入陣列，確保呼叫完成後原始陣列順序不變。
2. 對複本呼叫 `Array.Sort`，使相同數值聚集在一起。
3. 從左到右掃描排序結果，以 `currentCount` 計算目前這一段相同數值的長度。
4. 到達一段的尾端時，將 `currentCount` 加入 HashSet。
5. 若加入失敗，表示前面某一段有相同長度，立即回傳 `false`。
6. 重設 `currentCount`，繼續計算下一段；所有分組都通過後回傳 `true`。

迴圈會讓索引走到 `sorted.Length`，把「陣列結尾」當作最後一段的邊界，因此最後一組計數不需要額外寫一份重複的處理程式碼。

### 範例演示：`[1,2,2,1,1,3]`

輸入先被複製；原始陣列仍為 `[1,2,2,1,1,3]`。複本排序後為：

```text
[1,1,1,2,2,3]
```

逐段掃描：

| 分組 | 出現次數 | 加入 HashSet 後 | 結果 |
| --- | ---: | --- | --- |
| `[1,1,1]` | `3` | `{ 3 }` | 尚未重複 |
| `[2,2]` | `2` | `{ 3,2 }` | 尚未重複 |
| `[3]` | `1` | `{ 3,2,1 }` | 尚未重複 |

三段長度互不相同，因此回傳 `true`。

反例 `[1,2]` 排序後仍為 `[1,2]`。第一段 `[1]` 的次數 `1` 可以加入 HashSet；第二段 `[2]` 也想加入 `1` 時失敗，因此回傳 `false`。

### 為什麼正確

排序保證每個不同數值只會形成一段連續區間，而區間長度就是該數值的完整出現次數。因此掃描所有區間等同於取得所有數值的頻率。HashSet 對區間長度的唯一性檢查，便直接對應題目要求。

### 複雜度

- 時間複雜度：`O(n log n)`，主要成本來自排序；複製與線性掃描皆為 `O(n)`。
- 空間複雜度：`O(n)`，包含為避免修改輸入而建立的陣列複本，以及保存出現次數的 HashSet。

## 兩種解法比較

| 比較項目 | Dictionary + HashSet | Sorting + HashSet |
| --- | --- | --- |
| 主要概念 | 直接以 Dictionary 累計頻率 | 排序後以連續分組計算頻率 |
| 時間複雜度 | 平均 `O(n)` | `O(n log n)` |
| 空間複雜度 | `O(n)` | `O(n)` |
| 是否修改輸入 | 否 | 否，方法內先複製再排序 |
| 優點 | 效率較高，直接表達數值與頻率的關係 | 能練習排序後分組與邊界判斷 |
| 取捨 | 需要 Dictionary 與 HashSet 兩個雜湊結構 | 排序較慢，且為保留輸入需建立複本 |

若目標是這題的執行效率與可讀性，Dictionary + HashSet 通常是首選；Sorting + HashSet 則提供另一種從資料排列特性出發的思考方式。

## 建置與執行

需求：安裝 [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)。

請在此 README 所在的 workspace 根目錄執行：

```powershell
dotnet build .\leetcode_1207\leetcode_1207.csproj
dotnet run --project .\leetcode_1207\leetcode_1207.csproj
```

目前 runner 會讓兩種解法執行三組官方範例，實際輸出如下：

```text
Example 1: [1, 2, 2, 1, 1, 3], Expected: True
  Dictionary + HashSet: True (PASS)
  Sorting + HashSet:    True (PASS)
Example 2: [1, 2], Expected: False
  Dictionary + HashSet: False (PASS)
  Sorting + HashSet:    False (PASS)
Example 3: [-3, 0, 1, -3, 1, 1, 1, -3, 10, 0], Expected: True
  Dictionary + HashSet: True (PASS)
  Sorting + HashSet:    True (PASS)
```

## 專案結構

```text
leetcode_1207/
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_1207/
    ├── leetcode_1207.csproj
    └── Program.cs
```

- `Program.cs`：題目解法、XML 文件與可執行範例。
- `leetcode_1207.csproj`：目標框架為 `net10.0` 的主控台專案設定。
- `docs/readme-template.md`：README 的內容與驗證指引。
