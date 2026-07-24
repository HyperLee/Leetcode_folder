# LeetCode 1909 — Remove One Element to Make the Array Strictly Increasing

> 刪除一個元素使陣列嚴格遞增｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/remove-one-element-to-make-the-array-strictly-increasing/)
- [中文題目](https://leetcode.cn/problems/remove-one-element-to-make-the-array-strictly-increasing/)

## 題目說明

給定零起始整數陣列 `nums`，判斷刪除恰好一個元素後，剩餘元素能否嚴格遞增。若陣列
原本已嚴格遞增，仍可刪除其中任一元素，因此答案為 `true`。

題目限制：

- `2 <= nums.length <= 1000`
- `1 <= nums[i] <= 1000`
- 嚴格遞增要求每個後項都必須大於前項；相等也算違規

## 解法一：單次掃描

標準公開 API：

```csharp
public static bool CanBeIncreasing(int[] nums)
```

由左至右檢查每一組相鄰元素。遇到 `nums[i] <= nums[i - 1]` 時，這是必須由唯一一次
刪除處理的違規位置。此時只有兩個可能：

1. 刪除前項 `nums[i - 1]`：`i == 1`，或 `nums[i] > nums[i - 2]`。
2. 刪除目前項 `nums[i]`：`i` 位於最後，或 `nums[i + 1] > nums[i - 1]`。

只要第一次違規的兩種刪法至少一種能把兩側接回，便可繼續掃描；若兩種都不可行，或又
遇到第二個違規，答案就是 `false`。方法只讀取輸入陣列，不修改元素或輸出主控台。

核心不變量是：

> 已掃描前綴最多只有一個非嚴格遞增位置，而且該位置至少存在一種合法刪除方式。

時間複雜度為 `O(n)`，輔助空間與結果空間皆為 `O(1)`。

## 解法二：逐一略過元素

教學比較 API：

```csharp
public static bool CanBeIncreasingBruteForce(int[] nums)
```

這個版本保留舊專案的思路：依序把每個索引視為被刪除的位置，再掃描其餘元素是否嚴格
遞增。翻新後不再為每個候選建立 `List<int>`；helper 直接略過指定索引，因此不修改輸入，
輔助空間也保持固定。

時間複雜度為 `O(n²)`，輔助空間與結果空間皆為 `O(1)`。它比標準解法慢，但能清楚展示
「枚舉刪除位置，再驗證剩餘序列」的直接推導過程，適合作為最佳化前的教學基準。

### 方法比較

| 方法 | 時間 | 輔助空間 | 取捨 |
| --- | --- | --- | --- |
| `CanBeIncreasing` | `O(n)` | `O(1)` | 單次掃描，只分析唯一違規位置的兩種刪除候選 |
| `CanBeIncreasingBruteForce` | `O(n²)` | `O(1)` | 保留直觀枚舉思路，較容易理解但上限輸入較慢 |

## 逐步範例

以 `[1, 2, 5, 3, 4]` 為例：

```plaintext
1 < 2 < 5，但 3 <= 5，因此 i = 3 是第一個違規位置。

刪除前項 5：
3 > 2，兩側可接回，得到 [1, 2, 3, 4]。

刪除目前項 3：
下一項 4 並未大於前項 5，因此這條路不可行。

至少一種刪除方式成立，且後續沒有第二個違規，所以答案為 true。
```

相較之下，`[1, 4, 5, 3, 4]` 在 `5, 3` 處雖只有一個相鄰違規，但刪除 `5` 後
`4, 3` 仍不遞增，刪除 `3` 後 `5, 4` 也不遞增，因此答案為 `false`。

## Acceptance Harness

`Main` 執行 11 個確定性案例。每案以獨立陣列副本呼叫兩個方法，分別驗證答案與輸入
保存，因此共有 44 個檢查；任何失敗都會把 process exit code 設為 `1`。

| # | 輸入 | 預期 | 驗證目的 |
| ---: | --- | --- | --- |
| 1 | `[1, 2, 10, 5, 7]` | `true` | 官方範例；刪除中間前項 |
| 2 | `[2, 3, 1, 2]` | `false` | 官方範例；不只一處無法修復 |
| 3 | `[1, 1, 1]` | `false` | 官方範例；重複值形成多個違規 |
| 4 | `[2, 1]` | `true` | 最小有效長度 |
| 5 | `[1, 2, 3]` | `true` | 原本已嚴格遞增 |
| 6 | `[10, 1, 2, 3]` | `true` | 刪除第一個元素 |
| 7 | `[1, 2, 3, 0]` | `true` | 刪除最後一個元素 |
| 8 | `[1, 2, 5, 3, 4]` | `true` | 必須刪除違規位置的前項 |
| 9 | `[1, 1, 2]` | `true` | 刪除開頭重複值 |
| 10 | `[1, 4, 5, 3, 4]` | `false` | 單一局部違規但兩種刪法都無法接回 |
| 11 | `[1..1000]` | `true` | 最大長度 spot check |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_1909/leetcode_1909/leetcode_1909.csproj --nologo
dotnet run --no-build --project leetcode_1909/leetcode_1909/leetcode_1909.csproj
```

若直接開啟題目根目錄 `leetcode_1909/`，使用：

```bash
dotnet build leetcode_1909/leetcode_1909.csproj --nologo
dotnet run --no-build --project leetcode_1909/leetcode_1909.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: 1 - Official example 1
Input: [1, 2, 10, 5, 7]
PASS CanBeIncreasing result | Expected: True | Actual: True
PASS CanBeIncreasing input preserved | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce result | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce input preserved | Expected: True | Actual: True

Case: 2 - Official example 2
Input: [2, 3, 1, 2]
PASS CanBeIncreasing result | Expected: False | Actual: False
PASS CanBeIncreasing input preserved | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce result | Expected: False | Actual: False
PASS CanBeIncreasingBruteForce input preserved | Expected: True | Actual: True

Case: 3 - Official example 3
Input: [1, 1, 1]
PASS CanBeIncreasing result | Expected: False | Actual: False
PASS CanBeIncreasing input preserved | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce result | Expected: False | Actual: False
PASS CanBeIncreasingBruteForce input preserved | Expected: True | Actual: True

Case: 4 - Minimum input
Input: [2, 1]
PASS CanBeIncreasing result | Expected: True | Actual: True
PASS CanBeIncreasing input preserved | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce result | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce input preserved | Expected: True | Actual: True

Case: 5 - Already strictly increasing
Input: [1, 2, 3]
PASS CanBeIncreasing result | Expected: True | Actual: True
PASS CanBeIncreasing input preserved | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce result | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce input preserved | Expected: True | Actual: True

Case: 6 - Remove first element
Input: [10, 1, 2, 3]
PASS CanBeIncreasing result | Expected: True | Actual: True
PASS CanBeIncreasing input preserved | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce result | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce input preserved | Expected: True | Actual: True

Case: 7 - Remove last element
Input: [1, 2, 3, 0]
PASS CanBeIncreasing result | Expected: True | Actual: True
PASS CanBeIncreasing input preserved | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce result | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce input preserved | Expected: True | Actual: True

Case: 8 - Remove previous middle element
Input: [1, 2, 5, 3, 4]
PASS CanBeIncreasing result | Expected: True | Actual: True
PASS CanBeIncreasing input preserved | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce result | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce input preserved | Expected: True | Actual: True

Case: 9 - Duplicate at the beginning
Input: [1, 1, 2]
PASS CanBeIncreasing result | Expected: True | Actual: True
PASS CanBeIncreasing input preserved | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce result | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce input preserved | Expected: True | Actual: True

Case: 10 - Single violation cannot be repaired
Input: [1, 4, 5, 3, 4]
PASS CanBeIncreasing result | Expected: False | Actual: False
PASS CanBeIncreasing input preserved | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce result | Expected: False | Actual: False
PASS CanBeIncreasingBruteForce input preserved | Expected: True | Actual: True

Case: 11 - Maximum length strictly increasing
Input: [1..1000]
PASS CanBeIncreasing result | Expected: True | Actual: True
PASS CanBeIncreasing input preserved | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce result | Expected: True | Actual: True
PASS CanBeIncreasingBruteForce input preserved | Expected: True | Actual: True

Summary: 44/44 checks passed.
```

## 專案結構

```plaintext
leetcode_1909/
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
└── leetcode_1909/
    ├── Program.cs
    └── leetcode_1909.csproj
```
