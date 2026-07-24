# LeetCode 1913 — Maximum Product Difference Between Two Pairs

> 兩個數對之間的最大乘積差｜.NET 10 主控台專案

- [English problem](https://leetcode.com/problems/maximum-product-difference-between-two-pairs/)
- [中文題目](https://leetcode.cn/problems/maximum-product-difference-between-two-pairs/)

## 題目說明

給定整數陣列 `nums`，選出四個相異索引 `w`、`x`、`y`、`z`，使下列乘積差最大：

```plaintext
(nums[w] * nums[x]) - (nums[y] * nums[z])
```

題目限制：

- `4 <= nums.length <= 10^4`
- `1 <= nums[i] <= 10^4`

## 核心不變量與一次掃描解法

標準公開 API：

```csharp
public static int MaxProductDifference(int[] nums)
```

最大乘積必定由兩個最大值相乘，最小乘積必定由兩個最小值相乘。因此由左到右掃描時，只需維護：

- `largest` 與 `secondLargest`：目前看過的兩個最大值。
- `smallest` 與 `secondSmallest`：目前看過的兩個最小值。

更新順序是最容易出錯的部分。新值成為最大值時，必須先將舊 `largest` 下移到
`secondLargest`，再寫入新的 `largest`；最小值亦然。這個「先下移、後覆寫」的不變量讓
`[1, 1, 10, 10]` 中的重複極值仍能被正確保留兩次。

舊專案以 `Array.Sort(nums)` 取兩端值，雖然答案正確，但會排序並改變呼叫者的輸入，且成本為
`O(n log n)`。本版本以一次掃描取代它，方法只讀取 `nums`，不輸出主控台，也不加入題目未要求的
無效輸入處理。

| 項目 | 複雜度 |
| --- | --- |
| 時間 | `O(n)` |
| 結果空間 | `O(1)` |
| 輔助空間 | `O(1)` |

## 逐步走查

以 `[3, 4, 9, 10, 2, 1]` 為例：

```plaintext
掃描 3、4、9、10 後：largest = 10、secondLargest = 9，smallest = 3、secondSmallest = 4。
掃描 2 時：先把舊 smallest = 3 下移為 secondSmallest，再令 smallest = 2。
掃描 1 時：先把舊 smallest = 2 下移為 secondSmallest，再令 smallest = 1。
最後：(10 * 9) - (1 * 2) = 88。
```

## Acceptance Harness

`Main` 執行 7 個確定性案例。每案先複製輸入，再驗證回傳答案與輸入保存，因此共有 14 個檢查；
任何失敗都會將 process exit code 設為 `1`。

| # | 輸入 | 預期 | 驗證目的 |
| ---: | --- | ---: | --- |
| 1 | `[5, 6, 2, 7, 4]` | 34 | 官方範例 1 |
| 2 | `[4, 2, 5, 9, 7, 4, 8]` | 64 | 官方範例 2 |
| 3 | `[1, 2, 3, 4]` | 10 | 最小長度 |
| 4 | `[1, 1, 10, 10]` | 99 | 重複極值與先下移再覆寫 |
| 5 | `[5, 5, 5, 5]` | 0 | 全部相等 |
| 6 | `[3, 4, 9, 10, 2, 1]` | 88 | 極值在掃描後段才出現 |
| 7 | `5000 × 9996、1 × 2、10000 × 2` | 99,999,999 | 長度 10,000 spot check |

## 建置與執行

已從 repository 根目錄驗證：

```bash
dotnet build leetcode_1913/leetcode_1913/leetcode_1913.csproj --nologo
dotnet run --no-build --project leetcode_1913/leetcode_1913/leetcode_1913.csproj
```

若直接開啟題目根目錄 `leetcode_1913/`，使用：

```bash
dotnet build leetcode_1913/leetcode_1913.csproj --nologo
dotnet run --no-build --project leetcode_1913/leetcode_1913.csproj
```

以下是 fresh run 的完整輸出：

```text
Case: 1 - Official example 1
Input: [5, 6, 2, 7, 4]
PASS MaxProductDifference result | Expected: 34 | Actual: 34
PASS MaxProductDifference input preserved | Expected: True | Actual: True

Case: 2 - Official example 2
Input: [4, 2, 5, 9, 7, 4, 8]
PASS MaxProductDifference result | Expected: 64 | Actual: 64
PASS MaxProductDifference input preserved | Expected: True | Actual: True

Case: 3 - Minimum length
Input: [1, 2, 3, 4]
PASS MaxProductDifference result | Expected: 10 | Actual: 10
PASS MaxProductDifference input preserved | Expected: True | Actual: True

Case: 4 - Duplicate extrema
Input: [1, 1, 10, 10]
PASS MaxProductDifference result | Expected: 99 | Actual: 99
PASS MaxProductDifference input preserved | Expected: True | Actual: True

Case: 5 - All equal
Input: [5, 5, 5, 5]
PASS MaxProductDifference result | Expected: 0 | Actual: 0
PASS MaxProductDifference input preserved | Expected: True | Actual: True

Case: 6 - Late extrema
Input: [3, 4, 9, 10, 2, 1]
PASS MaxProductDifference result | Expected: 88 | Actual: 88
PASS MaxProductDifference input preserved | Expected: True | Actual: True

Case: 7 - Maximum-length spot check
Input: [5000 × 9996, 1 × 2, 10000 × 2]
PASS MaxProductDifference result | Expected: 99999999 | Actual: 99999999
PASS MaxProductDifference input preserved | Expected: True | Actual: True

Summary: 14/14 checks passed.
```

## 專案結構

```plaintext
leetcode_1913/
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
└── leetcode_1913/
    ├── Program.cs
    └── leetcode_1913.csproj
```
