# leetcode_1277

此專案示範 LeetCode 題目「1277. Count Square Submatrices with All Ones」的 C# 範例實作。

## 題目摘要
給定一個 m × n 的二元矩陣（元素為 0 或 1），請計算矩陣中所有由 1 組成的正方形子矩陣數量。

## 演算法概觀（動態規劃）

- 核心想法：對每個格子 `(i,j)`，計算以它為右下角的、且全部為 1 的最大正方形邊長 `dp[i,j]`。若已知所有 `dp`，將它們相加即可得到所有合法正方形的數量。

### 狀態定義
- `dp[i,j]`：以格子 `(i,j)` 為右下角的最大全為 1 的正方形之邊長（若 `matrix[i][j] == 0`，則為 0）。

### 轉移方程與推理

若 `matrix[i][j] == 0`，則 `dp[i,j] = 0`。

若 `matrix[i][j] == 1`：

```
若 i == 0 或 j == 0:
  dp[i,j] = 1
否則:
  dp[i,j] = 1 + min( dp[i-1,j], dp[i,j-1], dp[i-1,j-1] )
```

推導直觀說明：要在 `(i,j)` 形成邊長 L (>1) 的正方形，必須確保上方 `(i-1,j)`、左方 `(i,j-1)` 以及左上 `(i-1,j-1)` 三處各自至少能支撐邊長 L-1 的全 1 正方形；因此 `(i,j)` 能達到的最大邊長為三者的最小值加 1。

另外，若 `dp[i,j] = t`，表示以該右下角可形成邊長 1..t 的 t 個正方形，故最終答案為所有 `dp[i,j]` 的總和。

### 証明要點（簡述）
- 充分性：若三者最小值為 k，則其上、左、左上區域皆包含 k×k 的全 1 正方形，和目前格子 1 合併即可擴展為 (k+1)×(k+1)。
- 必要性（保守）：若任一方向不足（小於 k），則無法形成更大正方形，故取最小值是正確且必要的限制。

## 複雜度

- 時間：O(m * n) — 每個格子只處理一次。
- 空間：O(m * n) 使用二維 `dp`；可優化為 O(n)（一維 `dp`）或 O(1) 額外空間（就地覆寫 `matrix`）。

## 範例推演（快速）

輸入：

```
0 1 1 1
1 1 1 1
0 1 1 1
```

計算後的 `dp`：

```
0 1 1 1
1 1 2 2
0 1 2 3
```

將所有 `dp` 值相加得到 15。

## 優化（一維 dp 的思路）

可以用長度為 `n` 的一維陣列 `dp[j]` 保存上一列（或當前列）資訊，並用變數 `prev` 暫存上一列的左上格值。更新時注意保存舊值以供下一格使用。此法把空間複雜度降到 O(n)。

## 檔案說明

- `leetcode_1277/Program.cs`：主程式與 `CountSquares` 方法的 C# 實作，`Main` 含兩組範例輸出。

## 如何 建構 與 執行

在專案資料夾中使用 .NET CLI：

```bash
dotnet build ./leetcode_1277.csproj -c Debug
dotnet run --project ./leetcode_1277.csproj -c Debug
```

（或從上層工作區執行：`dotnet build ./leetcode_1277/leetcode_1277.csproj`）

執行後主控台會輸出 `CountSquares` 的範例結果（預期 15 與 7）。

## 參考資料

- LeetCode 題目： https://leetcode.com/problems/count-square-submatrices-with-all-ones/
- 中文圖解與討論： https://leetcode.cn/problems/count-square-submatrices-with-all-ones/
