# LeetCode 3190：使所有元素都可以被 3 整除的最少操作數

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![Language](https://img.shields.io/badge/language-C%23-239120?logo=csharp)
![Difficulty](https://img.shields.io/badge/difficulty-Easy-00AF9B)

本專案使用 C# 與 .NET 10 實作 LeetCode 3190，並以可直接執行的主程式比較兩種 O(n) 解法。執行結果會顯示每組案例的預期值、實際值與 PASS/FAIL；只要有一項失敗，程式就會回傳非零結束碼。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：統計非-3-倍數](#解法一統計非-3-倍數)
- [解法二：計算最近倍數距離](#解法二計算最近倍數距離)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

給定一個正整數陣列 `nums`。一次操作可以選擇任一元素，將它加 1 或減 1。請回傳讓陣列中所有元素都可以被 3 整除所需的最少操作次數。

- [LeetCode 英文題目](https://leetcode.com/problems/find-minimum-operations-to-make-all-elements-divisible-by-three/description/?envType=daily-question&envId=2025-11-22)
- [力扣中文題目](https://leetcode.cn/problems/find-minimum-operations-to-make-all-elements-divisible-by-three/description/?envType=daily-question&envId=2025-11-22)

### 官方範例一

```text
輸入：nums = [1, 2, 3, 4]
輸出：3
```

- `1 - 1 = 0`
- `2 + 1 = 3`
- `3` 已能被 3 整除
- `4 - 1 = 3`

共需要 3 次操作。

### 官方範例二

```text
輸入：nums = [3, 6, 9]
輸出：0
```

所有元素原本就能被 3 整除，因此不需要操作。

## 限制條件

- `1 <= nums.Length <= 50`
- `1 <= nums[i] <= 50`

題目保證輸入是非空的正整數陣列，因此解法不需要處理空陣列、零或負數。

## 解題概念與出發點

任一正整數除以 3，餘數只可能是 0、1 或 2：

| 餘數 | 目前狀態 | 最少操作 |
| --- | --- | --- |
| 0 | 已是 3 的倍數 | 0 |
| 1 | 減 1 可到前一個 3 的倍數 | 1 |
| 2 | 加 1 可到下一個 3 的倍數 | 1 |

每次操作只改變一個元素，所以每個元素可以獨立求出最少操作數，再把結果相加。由於除以 3 的非零餘數都只需要一次操作，問題也等價於「計算陣列內有多少元素不能被 3 整除」。

## 解法一：統計非 3 倍數

### 設計說明

`MinimumOperations` 直接使用核心結論。從左到右掃描陣列：

1. 計算目前元素 `num % 3`。
2. 餘數是 0 時，不增加計數器。
3. 餘數不是 0 時，代表該元素恰好需要一次操作，因此計數器加 1。
4. 掃描完成後回傳計數器。

這個設計不需要真的修改陣列，也不需要模擬加減過程；它只記錄每個元素是否需要操作。

### 正確性說明

- 若 `num % 3 == 0`，元素已符合要求，最少操作數是 0。
- 若 `num % 3 == 1`，將元素減 1 就能被 3 整除，最少操作數是 1。
- 若 `num % 3 == 2`，將元素加 1 就能被 3 整除，最少操作數是 1。

演算法對每個需要操作的元素恰好累加 1，對不需要操作的元素累加 0，因此總和就是全陣列的最少操作數。

### 範例演示：`[1, 2, 3, 4]`

| 元素 | `num % 3` | 是否計數 | 累計值 |
| ---: | ---: | --- | ---: |
| 1 | 1 | 是 | 1 |
| 2 | 2 | 是 | 2 |
| 3 | 0 | 否 | 2 |
| 4 | 1 | 是 | 3 |

最後回傳 `3`。

### 複雜度

- 時間複雜度：O(n)，只遍歷陣列一次。
- 空間複雜度：O(1)，只使用固定數量的區域變數。

## 解法二：計算最近倍數距離

### 設計說明

`MinimumOperations2` 從「距離最近的 3 倍數有多遠」出發。令 `remainder = x % 3`：

- 向下到前一個 3 的倍數需要 `remainder` 次減 1。
- 向上到下一個 3 的倍數需要 `3 - remainder` 次加 1。
- 兩個方向取較小值：`Math.Min(remainder, 3 - remainder)`。

最後透過 LINQ 的 `Select` 計算每個元素的最短距離，再由 `Sum` 加總。當餘數為 0 時，向下距離是 0，因此仍會正確選出 0；不需要另外撰寫分支。

### 正確性說明

對單一元素而言，連續加減 1 到達最近的 3 倍數，就是可行操作中的最短路徑。公式同時比較向下與向上的距離並選擇較小者，因此會得到該元素的最少操作數。各元素互不影響，將所有最小值相加即可得到全陣列的最小總操作數。

### 範例演示：`[1, 2, 3, 4]`

| 元素 | 餘數 | 向下距離 | 向上距離 | 取最小值 | 累計值 |
| ---: | ---: | ---: | ---: | ---: | ---: |
| 1 | 1 | 1 | 2 | 1 | 1 |
| 2 | 2 | 2 | 1 | 1 | 2 |
| 3 | 0 | 0 | 3 | 0 | 2 |
| 4 | 1 | 1 | 2 | 1 | 3 |

最後回傳 `3`。

### 複雜度

- 時間複雜度：O(n)，`Select` 與 `Sum` 以串流方式處理每個元素。
- 空間複雜度：O(1)，不建立與輸入大小成比例的集合。

## 兩種解法比較

| 比較項目 | `MinimumOperations` | `MinimumOperations2` |
| --- | --- | --- |
| 核心觀點 | 統計餘數不為 0 的元素 | 計算到上下兩個 3 倍數的距離 |
| 實作風格 | `foreach` 與條件判斷 | LINQ 與數學公式 |
| 時間複雜度 | O(n) | O(n) |
| 空間複雜度 | O(1) | O(1) |
| 是否修改輸入 | 否 | 否 |

第一種解法最直接地利用本題「非零餘數只需一次操作」的特殊性；第二種解法則保留了「比較兩個方向距離」的完整推導，較容易延伸到其他除數或距離問題。

## 可執行測試設計

`Main` 會讓兩種解法各自執行以下 5 組案例，共進行 10 次檢查：

| 案例 | 輸入 | 預期值 | 驗證重點 |
| --- | --- | ---: | --- |
| 官方範例一 | `[1, 2, 3, 4]` | 3 | 混合三種餘數 |
| 已全部整除 | `[3, 6, 9]` | 0 | 不需操作 |
| 最小元素邊界 | `[1]` | 1 | 最短陣列與最小值 |
| 最大值與重複值 | `[50, 50, 50]` | 3 | 最大值及重複元素 |
| 所有元素都需操作 | `[1, 2, 4, 5, 7, 8]` | 6 | 每個元素都需一次操作 |

每項檢查都會輸出 Expected、Actual 與 PASS/FAIL。若未全部通過，`Environment.ExitCode` 會設為 1，方便終端機、CI 或其他自動化流程偵測失敗。

## 專案結構

```text
leetcode_3190/
├── README.md
├── 解法說明.md
├── leetcode_3190.sln
└── leetcode_3190/
    ├── leetcode_3190.csproj
    └── Program.cs
```

## 建置與執行

需要安裝 .NET 10 SDK。請在本專案根目錄執行：

```bash
dotnet restore leetcode_3190/leetcode_3190.csproj
dotnet build leetcode_3190/leetcode_3190.csproj --nologo
dotnet run --no-build --project leetcode_3190/leetcode_3190.csproj
```

目前沒有獨立的自動化測試專案；建置與 `Main` 內的 10 次自我驗證共同作為本專案的驗收方式。

## 實際執行結果

```text
=== MinimumOperations ===
Case: 官方範例一
Input: [1, 2, 3, 4]
Expected: 3
Actual: 3
Result: PASS

Case: 已全部整除
Input: [3, 6, 9]
Expected: 0
Actual: 0
Result: PASS

Case: 最小元素邊界
Input: [1]
Expected: 1
Actual: 1
Result: PASS

Case: 最大值與重複值
Input: [50, 50, 50]
Expected: 3
Actual: 3
Result: PASS

Case: 所有元素都需操作
Input: [1, 2, 4, 5, 7, 8]
Expected: 6
Actual: 6
Result: PASS

=== MinimumOperations2 ===
Case: 官方範例一
Input: [1, 2, 3, 4]
Expected: 3
Actual: 3
Result: PASS

Case: 已全部整除
Input: [3, 6, 9]
Expected: 0
Actual: 0
Result: PASS

Case: 最小元素邊界
Input: [1]
Expected: 1
Actual: 1
Result: PASS

Case: 最大值與重複值
Input: [50, 50, 50]
Expected: 3
Actual: 3
Result: PASS

Case: 所有元素都需操作
Input: [1, 2, 4, 5, 7, 8]
Expected: 6
Actual: 6
Result: PASS

Summary: 10/10 checks passed
```
