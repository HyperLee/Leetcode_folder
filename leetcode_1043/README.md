# LeetCode 1043 — Partition Array for Maximum Sum

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![C%23](https://img.shields.io/badge/C%23-Dynamic%20Programming-239120)

使用 C# 實作 LeetCode 1043「分隔陣列以得到最大和」。專案同時提供 Bottom-up 動態規劃與 Top-down 記憶化遞迴，並透過可直接執行的 console runner 比對兩種解法的結果。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：Bottom-up 動態規劃](#解法一bottom-up-動態規劃)
- [解法二：Top-down 記憶化遞迴](#解法二top-down-記憶化遞迴)
- [解法比較](#解法比較)
- [建置與執行](#建置與執行)

## 題目說明

給定整數陣列 `arr` 與整數 `k`，將陣列切成數個連續子陣列，每一段的長度最多為 `k`。

切割完成後，每一段的所有元素都會被替換成該段的最大值。目標是找出所有合法切割方式中，替換後陣列可以得到的最大總和。

### 官方範例

| 範例 | 輸入 | 輸出 | 說明 |
|---|---|---:|---|
| 1 | `arr = [1,15,7,9,2,5,10]`, `k = 3` | `84` | 可切成 `[1,15,7] / [9] / [2,5,10]`，替換後為 `[15,15,15,9,10,10,10]`。 |
| 2 | `arr = [1,4,1,5,7,3,6,1,9,9,3]`, `k = 4` | `83` | 在每段長度不超過 4 的條件下選出最佳分割。 |
| 3 | `arr = [1]`, `k = 1` | `1` | 只有一個元素，因此唯一分割的總和就是 1。 |

題目連結：[LeetCode 1043](https://leetcode.com/problems/partition-array-for-maximum-sum/description/)

## 限制條件

- `1 <= arr.Length <= 500`
- `0 <= arr[i] <= 10^9`
- `1 <= k <= arr.Length`
- 測試資料保證答案可用 32 位元整數表示。

本專案依照題目限制使用合法輸入，因此演算法方法不另外處理空陣列、非法 `k` 或其他超出題目範圍的資料。

## 解題概念與出發點

如果直接列舉所有切割組合，每個位置都可能選擇不同的段長，會產生大量重複計算。真正影響答案的資訊其實只有兩項：

1. 已經處理到哪個位置。
2. 目前選擇的最後一段或下一段，其最大值與長度是多少。

以最後一段長度為 `length` 為例，該段對總和的貢獻為：

```text
該段最大值 × length
```

只要前一部分的最佳答案已知，就能得到目前選擇的候選值：

```text
前一部分的最佳答案 + 該段最大值 × length
```

Bottom-up 與 Top-down 使用的是同一個最佳子結構，差別只在計算順序：

- Bottom-up 從空前綴開始，逐步求出更長前綴的答案。
- Top-down 從索引 0 開始詢問完整答案，需要某個後綴時才遞迴計算並快取。

## 解法一：Bottom-up 動態規劃

### 狀態定義

```text
dp[i] = arr 前 i 個元素完成分割後可得到的最大總和
```

`dp[0] = 0`，代表尚未處理任何元素時總和為 0。最終答案是 `dp[arr.Length]`。

### 狀態轉移

對每個 `prefixLength`，枚舉最後一段的長度 `partitionLength`：

```text
dp[prefixLength]
    = max(
        dp[prefixLength - partitionLength]
        + maxValue × partitionLength
      )
```

其中：

- `1 <= partitionLength <= min(k, prefixLength)`。
- `maxValue` 是最後 `partitionLength` 個元素中的最大值。
- 內層迴圈每向左多納入一個元素，就更新一次 `maxValue`，不必重新掃描整段。

### 為什麼轉移式正確

假設最後一段長度已決定為 `partitionLength`，它前面的元素必須採用最佳分割；否則把前面換成更好的分割就能提高總和，原答案便不可能是最大值。因此每個候選答案都可拆成：

1. 前 `prefixLength - partitionLength` 個元素的最佳答案。
2. 最後一段替換後產生的總和。

枚舉所有合法的最後一段長度並取最大值，就涵蓋了所有可能的最佳分割。

### 範例演示

使用 `arr = [1,15,7,9,2,5,10]`、`k = 3`。下表列出每個前綴所考慮的候選值：

| `prefixLength` | 最後一段長度的候選值 | `dp[prefixLength]` |
|---:|---|---:|
| 0 | 空前綴 | 0 |
| 1 | `0 + 1×1 = 1` | 1 |
| 2 | `1 + 15×1 = 16`、`0 + 15×2 = 30` | 30 |
| 3 | `30 + 7×1 = 37`、`1 + 15×2 = 31`、`0 + 15×3 = 45` | 45 |
| 4 | `45 + 9×1 = 54`、`30 + 9×2 = 48`、`1 + 9×3 = 28` | 54 |
| 5 | `54 + 2×1 = 56`、`45 + 9×2 = 63`、`30 + 9×3 = 57` | 63 |
| 6 | `63 + 5×1 = 68`、`54 + 5×2 = 64`、`45 + 9×3 = 72` | 72 |
| 7 | `72 + 10×1 = 82`、`63 + 10×2 = 83`、`54 + 10×3 = 84` | 84 |

最後得到 `dp[7] = 84`。

### 複雜度

- 時間複雜度：`O(n × k)`，每個前綴最多枚舉 `k` 種最後一段長度。
- 空間複雜度：`O(n)`，用來保存 `dp` 陣列。

### 優點與取捨

- 沒有遞迴呼叫，執行流程固定且容易觀察所有 DP 狀態。
- 空間使用量容易預估。
- 即使某些狀態最後不影響答案，仍會依序計算所有前綴。

## 解法二：Top-down 記憶化遞迴

### 狀態定義

```text
Solve(startIndex) = 從 startIndex 到陣列結尾可得到的最大總和
```

當 `startIndex == arr.Length` 時，代表所有元素都已分割完成，因此回傳 0。

### 遞迴選擇

從 `startIndex` 開始，枚舉下一段長度 `partitionLength`：

```text
Solve(startIndex)
    = max(
        maxValue × partitionLength
        + Solve(startIndex + partitionLength)
      )
```

每個起點的答案只需要計算一次。`memo[startIndex]` 初始為 `-1`；計算完成後保存結果，之後遇到相同起點便直接回傳快取值。

因為題目中的元素皆為非負整數，合法狀態的答案不會小於 0，所以 `-1` 可以安全表示「尚未計算」。

### 記憶化如何消除重複計算

例如 `Solve(0)` 選擇長度 1 時會呼叫 `Solve(1)`，而其他分割路徑稍後也可能走到相同的 `Solve(1)`。若沒有 memo，該後綴下的所有分割會被重算；有 memo 時只需直接取回先前結果。

### 範例演示

同樣使用 `arr = [1,15,7,9,2,5,10]`、`k = 3`。下表呈現每個起點完成計算後的 memo 結果：

| 起點 | 下一段候選值 | 最佳後綴總和 |
|---:|---|---:|
| 7 | 已到陣列結尾 | 0 |
| 6 | `10×1 + Solve(7) = 10` | 10 |
| 5 | `5×1 + 10 = 15`、`10×2 + 0 = 20` | 20 |
| 4 | `2×1 + 20 = 22`、`5×2 + 10 = 20`、`10×3 + 0 = 30` | 30 |
| 3 | `9×1 + 30 = 39`、`9×2 + 20 = 38`、`9×3 + 10 = 37` | 39 |
| 2 | `7×1 + 39 = 46`、`9×2 + 30 = 48`、`9×3 + 20 = 47` | 48 |
| 1 | `15×1 + 48 = 63`、`15×2 + 39 = 69`、`15×3 + 30 = 75` | 75 |
| 0 | `1×1 + 75 = 76`、`15×2 + 48 = 78`、`15×3 + 39 = 84` | 84 |

因此 `Solve(0) = 84`。實際遞迴會以深度優先方式展開；表格是將完成後的 memo 狀態由陣列尾端往前排列，方便閱讀。

### 複雜度

- 時間複雜度：`O(n × k)`；共有 `n` 個可快取的起點，每個起點最多枚舉 `k` 個終點。
- 空間複雜度：`O(n)` memo，另外最深可能使用 `O(n)` 的遞迴呼叫堆疊。

### 優點與取捨

- 寫法直接表達「從目前位置選擇下一段」的思考方式。
- 記憶化讓每個起點只計算一次。
- 遞迴呼叫會額外使用 call stack；在本題 `arr.Length <= 500` 的限制下仍屬可控範圍。

## 解法比較

| 項目 | Bottom-up DP | Top-down DP |
|---|---|---|
| 公開方法 | `MaxSumAfterPartitioning` | `MaxSumAfterPartitioningTopDown` |
| 狀態 | 前 `i` 個元素的最佳答案 | 從索引 `i` 開始的最佳答案 |
| 計算順序 | 從短前綴逐步填表 | 從完整問題遞迴展開 |
| 避免重算方式 | `dp` 陣列 | `memo` 陣列 |
| 時間複雜度 | `O(nk)` | `O(nk)` |
| 額外空間 | `O(n)` | `O(n)` memo，加上最深 `O(n)` call stack |
| 適合閱讀角度 | 觀察狀態如何逐步累積 | 觀察每個位置有哪些分割選擇 |

兩種解法使用相同的最佳子結構，沒有誰在漸進複雜度上更快。本專案同時保留兩者，讓讀者可以對照不同的 DP 思考方向。

## 建置與執行

需求：安裝支援 .NET 10 的 .NET SDK。

從本專案根目錄執行：

```powershell
dotnet restore .\leetcode_1043\leetcode_1043.csproj
dotnet build .\leetcode_1043\leetcode_1043.csproj
dotnet run --project .\leetcode_1043\leetcode_1043.csproj
```

專案沒有另外建立測試框架；`Main` 會執行五組固定案例，對兩種解法各做一次結果比對。

### 實際執行輸出

```text
LeetCode 1043 - Partition Array for Maximum Sum
=================================================

Case 1: Official example 1
Input: arr = [1, 15, 7, 9, 2, 5, 10], k = 3
Expected: 84
Bottom-up DP: 84 (PASS)
Top-down DP: 84 (PASS)

Case 2: Official example 2
Input: arr = [1, 4, 1, 5, 7, 3, 6, 1, 9, 9, 3], k = 4
Expected: 83
Bottom-up DP: 83 (PASS)
Top-down DP: 83 (PASS)

Case 3: Official example 3
Input: arr = [1], k = 1
Expected: 1
Bottom-up DP: 1 (PASS)
Top-down DP: 1 (PASS)

Case 4: k equals 1
Input: arr = [1, 2, 3], k = 1
Expected: 6
Bottom-up DP: 6 (PASS)
Top-down DP: 6 (PASS)

Case 5: k equals array length
Input: arr = [1, 2, 3], k = 3
Expected: 9
Bottom-up DP: 9 (PASS)
Top-down DP: 9 (PASS)

Summary: 10/10 checks passed.
```

## 專案結構

```text
leetcode_1043/
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── leetcode_1043/
│   ├── Program.cs
│   └── leetcode_1043.csproj
├── AGENTS.md
└── README.md
```

- `leetcode_1043/Program.cs`：題目說明、兩種 DP 解法與可執行測試 runner。
- `leetcode_1043/leetcode_1043.csproj`：以 .NET 10 為目標的 console 專案設定。
- `.vscode/`：VS Code 建置與偵錯設定。
