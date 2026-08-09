# LeetCode 3016：輸入單詞需要的最少按鍵次數 II

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-console-239120?logo=csharp)

這個專案以 C# 示範 LeetCode 3016「Minimum Number of Pushes to Type Word II」。程式保留一個排序貪心解法，並加入頻率桶與重複挑選最大值兩種比較實作；`Main` 會用固定案例驗證三種解法是否得到相同的最小按鍵次數。

## 快速連結

- [題目說明](#題目說明)
- [核心解題概念](#核心解題概念)
- [三種解法比較](#三種解法比較)
- [解法一排序頻率](#解法一排序頻率)
- [解法二頻率桶](#解法二頻率桶)
- [解法三重複挑選最大值](#解法三重複挑選最大值)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

電話鍵盤可以使用數字鍵 `2` 到 `9` 輸入小寫英文字母，共有 8 個可配置字母的按鍵。現在可以重新安排每個字母所屬的按鍵，但必須符合：

- 每個小寫英文字母恰好映射到一個按鍵。
- 每個按鍵可以配置任意數量的字母，也可以不配置字母。
- 同一按鍵上的第一個字母按 1 次、第二個字母按 2 次，依此類推。

給定字串 `word`，目標是找出最佳的按鍵映射，使輸入整個字串所需的按鍵次數最少，並回傳這個最小值。

題目原文：[Minimum Number of Pushes to Type Word II](https://leetcode.com/problems/minimum-number-of-pushes-to-type-word-ii/)

### 限制條件

- `1 <= word.length <= 10^5`
- `word` 只包含小寫英文字母 `a` 到 `z`

### 官方範例

| 輸入 | 輸出 | 說明 |
|---|---:|---|
| `abcde` | 5 | 五個字母都能放在只需按 1 次的位置。 |
| `xyzxyzxyzxyz` | 12 | 三個字母各出現 4 次，全部放在只需按 1 次的位置。 |
| `aabbccddeeffgghhiiiiii` | 24 | 先讓最高頻的 `i` 與另外七個字母使用第一層位置，最後一個字母進入第二層。 |

## 核心解題概念

### 1. 按鍵位置的成本固定

可用按鍵只有 `2` 到 `9`，所以每一層最多容納 8 個字母：

| 字母頻率排名 | 每次輸入該字母的按鍵次數 |
|---:|---:|
| 第 1～8 名 | 1 |
| 第 9～16 名 | 2 |
| 第 17～24 名 | 3 |
| 第 25～26 名 | 4 |

若排名從 0 開始，某個字母的按鍵成本就是：

```text
pushCost = (rank / 8) + 1
```

### 2. 高頻字母必須搭配低成本位置

假設兩個字母的頻率分別是 `high`、`low`，且 `high >= low`；兩個位置的成本分別是 `cheap`、`expensive`，且 `cheap <= expensive`。

把高頻字母放在便宜位置的成本為：

```text
high * cheap + low * expensive
```

反過來配置的成本為：

```text
high * expensive + low * cheap
```

第二種配置減去第一種配置，可得：

```text
(high - low) * (expensive - cheap) >= 0
```

因此，只要出現「高頻字母使用較貴位置、低頻字母使用較便宜位置」，交換兩者就不會讓答案變差。反覆交換後，必然得到「頻率由高到低、成本由低到高」的最佳配置。這就是三種解法共同使用的貪心依據，也可視為排列不等式的應用。

## 三種解法比較

令 `n = word.Length`，英文字母數量固定為 26。

| 方法 | API | 時間複雜度 | 額外空間 | 特點 |
|---|---|---:|---:|---|
| 排序頻率 | `MinimumPushes` | `O(n + 26 log 26)` | `O(26)` | 最精簡，直接對應頻率排名。 |
| 頻率桶 | `MinimumPushesByFrequencyBuckets` | `O(n)` | `O(n)` | 不做比較排序，按頻率由高到低展開。 |
| 重複挑選最大值 | `MinimumPushesByRepeatedSelection` | `O(n + 26²)` | `O(26)` | 不使用排序，選取過程最直觀。 |

因為字母種類固定為 26，三種方法在漸進意義上都可視為對輸入長度 `n` 的線性處理；表格仍保留固定字母集合上的實際工作量，方便比較設計差異。

## 解法一：排序頻率

### 設計出發點

最佳配置只取決於每個字母出現幾次，不需要真的建立電話鍵盤。只要將 26 個頻率排序，就能依排名直接乘上對應的按鍵成本。

### 執行步驟

1. 建立長度為 26 的 `frequencies` 陣列。
2. 掃描 `word`，以 `c - 'a'` 作為索引累加頻率。
3. 將頻率由小到大排序。
4. 從陣列尾端取出最高頻率，排名 `rank` 的成本是 `(rank / 8) + 1`。
5. 累加 `frequency * pushCost`。

### 範例演示

輸入 `aabbccddeeffgghhiiiiii`：

- 非零頻率由高到低為 `[6, 2, 2, 2, 2, 2, 2, 2, 2]`。
- 頻率 `6` 與前七個頻率 `2` 位於第 1～8 名，成本都是 1。
- 最後一個頻率 `2` 位於第 9 名，成本為 2。
- 總成本為 `6 * 1 + 7 * 2 * 1 + 2 * 2 = 24`。

### 優缺點

- 優點：程式短、排名概念清楚、空間固定。
- 缺點：使用一般排序；雖然只排序 26 個元素，仍可進一步利用頻率範圍避免比較排序。

## 解法二：頻率桶

### 設計出發點

每個字母的頻率一定介於 0 到 `word.Length`。可以用陣列索引代表頻率，陣列值代表「具有這個頻率的字母數量」，再從最高頻率往下掃描。

### 執行步驟

1. 統計 26 個字母頻率。
2. 建立長度為 `word.Length + 1` 的 `frequencyBuckets`。
3. 對每個非零頻率 `f` 執行 `frequencyBuckets[f]++`。
4. 從最高頻率往 1 掃描；若某個桶有字母，就依桶內數量逐一配置排名。
5. 依目前排名算出按鍵成本，累加 `frequency * pushCost`。

### 範例演示

同樣輸入 `aabbccddeeffgghhiiiiii`：

- `frequencyBuckets[6] = 1`，代表一個字母出現 6 次。
- `frequencyBuckets[2] = 8`，代表八個字母各出現 2 次。
- 先展開頻率 6，再展開八個頻率 2。
- 展開後的排名與排序解法相同，因此成本為 `6 + 14 + 4 = 24`。

### 優缺點

- 優點：完全不需要元素比較，可在線性時間依頻率遞減處理。
- 缺點：桶陣列長度與輸入長度相同；題目上限為 `10^5`，可接受但比另外兩種方法使用更多空間。

## 解法三：重複挑選最大值

### 設計出發點

字母種類只有 26 個，即使每次都線性掃描所有頻率尋找最大值，最多也只做 26 輪。這種方法像選擇排序，但不需要真的重新排列陣列。

### 執行步驟

1. 統計 26 個字母頻率。
2. 掃描整個頻率陣列，找出尚未配置的最大值。
3. 依該字母目前的排名計算成本並加入答案。
4. 將選中的頻率設為 0，避免下一輪重複選取。
5. 找不到正頻率時提早結束。

### 範例演示

輸入 `aabbccddeeffgghhiiiiii`：

- 第一輪選到頻率 6，排名 0、成本 1，貢獻 6。
- 接下來七輪各選到頻率 2，排名 1～7、成本 1，共貢獻 14。
- 第九輪仍選到頻率 2，但排名 8 已進入第二層，成本 2，貢獻 4。
- 合計 `6 + 14 + 4 = 24`。

### 優缺點

- 優點：不依賴排序或長度為 `n` 的桶，選取邏輯容易逐輪追蹤。
- 缺點：最多掃描 `26 * 26` 次；雖然在本題是固定常數，若字元種類很多就不適合。

## 正確性摘要

三種解法都會產生相同的頻率排名。根據交換論證，任何高頻字母被配置到較高成本位置的方案，都可以透過交換改善或維持總成本。因此，依頻率遞減順序配置成本遞增的位置必為最佳解。三種實作只是在「如何取得這個順序」上不同，最終計算的最小按鍵次數相同。

## 測試設計

專案目前沒有獨立的自動化測試專案，因此使用 `Main` 中可重跑的驗收案例。每組案例都會呼叫三個公開 API，比對 Expected 與 Actual；若任何一項失敗，程序會將結束碼設為 1。

| 案例 | 輸入 | 預期結果 | 驗證重點 |
|---|---|---:|---|
| 官方案例 1 | `abcde` | 5 | 少於 8 個不同字母。 |
| 官方案例 2 | `xyzxyzxyzxyz` | 12 | 重複字母全部位於第一層。 |
| 官方案例 3 | `aabbccddeeffgghhiiiiii` | 24 | 高頻字母優先與第二層成本。 |
| 最小長度 | `a` | 1 | 題目允許的最短輸入。 |
| 跨入第二按鍵層 | `abcdefghi` | 10 | 第 9 個不同字母必須按 2 次。 |
| 26 個不同字母 | `abcdefghijklmnopqrstuvwxyz` | 56 | 完整覆蓋四個成本層。 |
| 最大長度重複字母 | `a` 重複 100000 次 | 100000 | 最大輸入長度與長字串顯示縮寫。 |

## 專案結構

```text
leetcode_3016/
|-- README.md
|-- docs/
|   `-- readme-template.md
|-- leetcode_3016.sln
`-- leetcode_3016/
    |-- Program.cs
    `-- leetcode_3016.csproj
```

## 建置與執行

需要安裝支援 `net10.0` 的 .NET 10 SDK。以下命令皆從此題目的 repository 根目錄執行。

還原相依套件：

```bash
dotnet restore leetcode_3016/leetcode_3016.csproj
```

建置：

```bash
dotnet build leetcode_3016/leetcode_3016.csproj --nologo
```

執行固定案例：

```bash
dotnet run --no-build --project leetcode_3016/leetcode_3016.csproj
```

檢查格式與空白：

```bash
dotnet format leetcode_3016/leetcode_3016.csproj --verify-no-changes --no-restore
git diff --check
```

## 實際執行結果

以下內容來自 `dotnet run --no-build --project leetcode_3016/leetcode_3016.csproj` 的實際輸出：

```text
LeetCode 3016 - Minimum Number of Pushes to Type Word II
三種解法對照驗證

案例 1：官方案例 1
輸入：word = "abcde" (length = 5)
MinimumPushes: Expected = 5, Actual = 5 => PASS
MinimumPushesByFrequencyBuckets: Expected = 5, Actual = 5 => PASS
MinimumPushesByRepeatedSelection: Expected = 5, Actual = 5 => PASS

案例 2：官方案例 2
輸入：word = "xyzxyzxyzxyz" (length = 12)
MinimumPushes: Expected = 12, Actual = 12 => PASS
MinimumPushesByFrequencyBuckets: Expected = 12, Actual = 12 => PASS
MinimumPushesByRepeatedSelection: Expected = 12, Actual = 12 => PASS

案例 3：官方案例 3
輸入：word = "aabbccddeeffgghhiiiiii" (length = 22)
MinimumPushes: Expected = 24, Actual = 24 => PASS
MinimumPushesByFrequencyBuckets: Expected = 24, Actual = 24 => PASS
MinimumPushesByRepeatedSelection: Expected = 24, Actual = 24 => PASS

案例 4：最小長度
輸入：word = "a" (length = 1)
MinimumPushes: Expected = 1, Actual = 1 => PASS
MinimumPushesByFrequencyBuckets: Expected = 1, Actual = 1 => PASS
MinimumPushesByRepeatedSelection: Expected = 1, Actual = 1 => PASS

案例 5：跨入第二按鍵層
輸入：word = "abcdefghi" (length = 9)
MinimumPushes: Expected = 10, Actual = 10 => PASS
MinimumPushesByFrequencyBuckets: Expected = 10, Actual = 10 => PASS
MinimumPushesByRepeatedSelection: Expected = 10, Actual = 10 => PASS

案例 6：26 個不同字母
輸入：word = "abcdefghijkl...opqrstuvwxyz" (length = 26)
MinimumPushes: Expected = 56, Actual = 56 => PASS
MinimumPushesByFrequencyBuckets: Expected = 56, Actual = 56 => PASS
MinimumPushesByRepeatedSelection: Expected = 56, Actual = 56 => PASS

案例 7：最大長度重複字母
輸入：word = "aaaaaaaaaaaa...aaaaaaaaaaaa" (length = 100000)
MinimumPushes: Expected = 100000, Actual = 100000 => PASS
MinimumPushesByFrequencyBuckets: Expected = 100000, Actual = 100000 => PASS
MinimumPushesByRepeatedSelection: Expected = 100000, Actual = 100000 => PASS

總結：21/21 項測試通過
```
