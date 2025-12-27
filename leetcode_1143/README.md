# LeetCode 1143 - 最長公共子序列 (Longest Common Subsequence)

[![LeetCode](https://img.shields.io/badge/LeetCode-1143-orange?style=flat-square&logo=leetcode)](https://leetcode.com/problems/longest-common-subsequence/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Medium-yellow?style=flat-square)](https://leetcode.com/problems/longest-common-subsequence/)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14-239120?style=flat-square&logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)

## 題目描述

給定兩個字串 `text1` 和 `text2`，回傳它們**最長公共子序列 (Longest Common Subsequence, LCS)** 的長度。如果沒有公共子序列，則回傳 `0`。

### 什麼是子序列？

**子序列 (Subsequence)** 是從原字串刪除某些字元（可以不刪除任何字元）且**不改變其餘字元的相對順序**所產生的新字串。

例如，`"ace"` 是 `"abcde"` 的一個子序列（刪除 `b` 和 `d`）。

### 什麼是公共子序列？

**公共子序列 (Common Subsequence)** 是同時為兩個字串的子序列的字串。

### 範例

| 範例 | text1 | text2 | 輸出 | 說明 |
|:---:|-------|-------|:----:|------|
| 1 | `"abcde"` | `"ace"` | 3 | LCS 為 `"ace"` |
| 2 | `"abc"` | `"abc"` | 3 | LCS 為 `"abc"` |
| 3 | `"abc"` | `"def"` | 0 | 無公共子序列 |

### 限制條件

- `1 <= text1.length, text2.length <= 1000`
- `text1` 和 `text2` 僅由小寫英文字母組成

---

## 解題概念與出發點

### 為什麼選擇動態規劃？

這是一個典型的**最佳化問題**，具有以下特性：

1. **重疊子問題 (Overlapping Subproblems)**：計算 LCS 時，會重複計算相同的子問題
2. **最優子結構 (Optimal Substructure)**：問題的最優解可以由子問題的最優解構成

這兩個特性正是動態規劃的適用條件。

### 思考方向

假設我們有兩個字串：
- `text1 = "abcde"` (長度 m = 5)
- `text2 = "ace"` (長度 n = 3)

我們可以這樣思考：從兩個字串的**最後一個字元**開始比較：

1. **若最後字元相同**：這個字元一定屬於 LCS，問題縮小為比較剩餘的字串
2. **若最後字元不同**：LCS 不會同時包含這兩個字元，需要分別嘗試去掉其中一個

---

## 解法詳解

### 狀態定義

定義 `dp[i][j]` 為：`text1` 的前 `i` 個字元與 `text2` 的前 `j` 個字元的最長公共子序列長度。

### 狀態轉移方程式

$$
dp[i][j] = \begin{cases} 
dp[i-1][j-1] + 1 & \text{if } text1[i-1] = text2[j-1] \\
\max(dp[i-1][j], dp[i][j-1]) & \text{if } text1[i-1] \neq text2[j-1]
\end{cases}
$$

#### 方程式解釋

1. **當 `text1[i-1] == text2[j-1]` 時**：
   - 找到一個匹配的字元！
   - 這個字元可以加入 LCS
   - 所以 `dp[i][j] = dp[i-1][j-1] + 1`

2. **當 `text1[i-1] != text2[j-1]` 時**：
   - 這兩個字元不能同時出現在 LCS 中
   - 有兩個選擇：
     - 忽略 `text1[i-1]`，取 `dp[i-1][j]`
     - 忽略 `text2[j-1]`，取 `dp[i][j-1]`
   - 取兩者的最大值

### 邊界條件

- `dp[0][j] = 0`：空字串與任何字串的 LCS 長度為 0
- `dp[i][0] = 0`：任何字串與空字串的 LCS 長度為 0

### 程式碼實作

```csharp
public int LongestCommonSubsequence(string text1, string text2)
{
    // 建立 DP 陣列，大小為 (m+1) x (n+1)
    int[,] dp = new int[text1.Length + 1, text2.Length + 1];

    // 遍歷 text1 的每個字元
    for (int i = 1; i <= text1.Length; i++)
    {
        // 遍歷 text2 的每個字元
        for (int j = 1; j <= text2.Length; j++)
        {
            // 情況一：當前字元相同
            if (text1[i - 1] == text2[j - 1])
            {
                dp[i, j] = dp[i - 1, j - 1] + 1;
            }
            // 情況二：當前字元不同
            else
            {
                dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
            }
        }
    }

    return dp[text1.Length, text2.Length];
}
```

### 複雜度分析

| 指標 | 複雜度 | 說明 |
|------|--------|------|
| 時間複雜度 | O(m × n) | 需要填滿整個 DP 表格 |
| 空間複雜度 | O(m × n) | 儲存 DP 陣列 |

---

## 範例演示流程

以 `text1 = "abcde"` 和 `text2 = "ace"` 為例，逐步建構 DP 表格：

### 初始狀態

```
      ""  a   c   e
  ""   0   0   0   0
  a    0   
  b    0   
  c    0   
  d    0   
  e    0   
```

### 填表過程

#### Step 1: i=1 (text1[0]='a')

| j | text2[j-1] | 比較 | 結果 |
|---|------------|------|------|
| 1 | 'a' | 'a'=='a' ✓ | dp[1,1] = dp[0,0] + 1 = **1** |
| 2 | 'c' | 'a'≠'c' | dp[1,2] = max(dp[0,2], dp[1,1]) = max(0, 1) = **1** |
| 3 | 'e' | 'a'≠'e' | dp[1,3] = max(dp[0,3], dp[1,2]) = max(0, 1) = **1** |

#### Step 2: i=2 (text1[1]='b')

| j | text2[j-1] | 比較 | 結果 |
|---|------------|------|------|
| 1 | 'a' | 'b'≠'a' | dp[2,1] = max(dp[1,1], dp[2,0]) = max(1, 0) = **1** |
| 2 | 'c' | 'b'≠'c' | dp[2,2] = max(dp[1,2], dp[2,1]) = max(1, 1) = **1** |
| 3 | 'e' | 'b'≠'e' | dp[2,3] = max(dp[1,3], dp[2,2]) = max(1, 1) = **1** |

#### Step 3: i=3 (text1[2]='c')

| j | text2[j-1] | 比較 | 結果 |
|---|------------|------|------|
| 1 | 'a' | 'c'≠'a' | dp[3,1] = max(dp[2,1], dp[3,0]) = max(1, 0) = **1** |
| 2 | 'c' | 'c'=='c' ✓ | dp[3,2] = dp[2,1] + 1 = 1 + 1 = **2** |
| 3 | 'e' | 'c'≠'e' | dp[3,3] = max(dp[2,3], dp[3,2]) = max(1, 2) = **2** |

#### Step 4: i=4 (text1[3]='d')

| j | text2[j-1] | 比較 | 結果 |
|---|------------|------|------|
| 1 | 'a' | 'd'≠'a' | dp[4,1] = max(dp[3,1], dp[4,0]) = max(1, 0) = **1** |
| 2 | 'c' | 'd'≠'c' | dp[4,2] = max(dp[3,2], dp[4,1]) = max(2, 1) = **2** |
| 3 | 'e' | 'd'≠'e' | dp[4,3] = max(dp[3,3], dp[4,2]) = max(2, 2) = **2** |

#### Step 5: i=5 (text1[4]='e')

| j | text2[j-1] | 比較 | 結果 |
|---|------------|------|------|
| 1 | 'a' | 'e'≠'a' | dp[5,1] = max(dp[4,1], dp[5,0]) = max(1, 0) = **1** |
| 2 | 'c' | 'e'≠'c' | dp[5,2] = max(dp[4,2], dp[5,1]) = max(2, 1) = **2** |
| 3 | 'e' | 'e'=='e' ✓ | dp[5,3] = dp[4,2] + 1 = 2 + 1 = **3** |

### 最終 DP 表格

```
      ""  a   c   e
  ""   0   0   0   0
  a    0   1   1   1
  b    0   1   1   1
  c    0   1   2   2
  d    0   1   2   2
  e    0   1   2   3  ← 答案
```

**結果**：`dp[5][3] = 3`，表示 LCS 長度為 3，即 `"ace"`。

---

## 動態規劃公式推理

### 數學歸納法證明

設 $X = x_1x_2...x_m$ 和 $Y = y_1y_2...y_n$ 為兩個字串，$Z = z_1z_2...z_k$ 為它們的一個 LCS。

#### 性質 1：末字元相同的情況

若 $x_m = y_n$（兩字串最後字元相同），則：
- $z_k = x_m = y_n$（LCS 的最後字元必為此公共字元）
- $Z_{k-1}$ 是 $X_{m-1}$ 和 $Y_{n-1}$ 的 LCS

**證明**（反證法）：
1. 假設 $z_k \neq x_m$，則 $Z$ 可以加上 $x_m = y_n$ 形成更長的公共子序列，矛盾
2. 假設存在 $X_{m-1}$ 和 $Y_{n-1}$ 的公共子序列 $W$ 比 $Z_{k-1}$ 更長，則 $W + z_k$ 會比 $Z$ 更長，矛盾

#### 性質 2：末字元不同的情況

若 $x_m \neq y_n$，則：
- 若 $z_k \neq x_m$，則 $Z$ 是 $X_{m-1}$ 和 $Y$ 的 LCS
- 若 $z_k \neq y_n$，則 $Z$ 是 $X$ 和 $Y_{n-1}$ 的 LCS

**結論**：LCS 長度為 $\max(LCS(X_{m-1}, Y), LCS(X, Y_{n-1}))$

### 遞迴關係式推導

根據上述性質，可得遞迴關係式：

$$
LCS(X_i, Y_j) = \begin{cases}
0 & \text{if } i = 0 \text{ or } j = 0 \\
LCS(X_{i-1}, Y_{j-1}) + 1 & \text{if } x_i = y_j \\
\max(LCS(X_{i-1}, Y_j), LCS(X_i, Y_{j-1})) & \text{if } x_i \neq y_j
\end{cases}
$$

這正是我們在動態規劃中使用的狀態轉移方程式。

---

## 執行程式

```bash
cd leetcode_1143
dotnet run
```

### 預期輸出

```
測試案例 1: text1="abcde", text2="ace"
結果: 3 (預期: 3，LCS 為 "ace")

測試案例 2: text1="abc", text2="def"
結果: 0 (預期: 0，無公共子序列)

測試案例 3: text1="bl", text2="yby"
結果: 1 (預期: 1，LCS 為 "b")

測試案例 4: text1="AGGTAB", text2="GXTXAYB"
結果: 4 (預期: 4，LCS 為 "GTAB")

測試案例 5: text1="abc", text2="abc"
結果: 3 (預期: 3，LCS 為 "abc")
```

---

## 延伸思考

### 空間優化

由於 `dp[i][j]` 只依賴於 `dp[i-1][j-1]`、`dp[i-1][j]` 和 `dp[i][j-1]`，可以將空間複雜度優化至 **O(min(m, n))**。

### 回溯找出 LCS 字串

若需要回傳實際的 LCS 字串（而非僅長度），可以從 `dp[m][n]` 開始回溯：
- 若 `text1[i-1] == text2[j-1]`，將此字元加入結果，移至 `dp[i-1][j-1]`
- 否則，移至 `dp[i-1][j]` 或 `dp[i][j-1]` 中較大者

---

## 參考資源

- [LeetCode 1143 - Longest Common Subsequence](https://leetcode.com/problems/longest-common-subsequence/)
- [LeetCode 1143 - 最長公共子序列（中文版）](https://leetcode.cn/problems/longest-common-subsequence/)

## 參考解法

以下為值得參考的中文解法與教學：

- 力扣官方題解：
  https://leetcode.cn/problems/longest-common-subsequence/solutions/696763/zui-chang-gong-gong-zi-xu-lie-by-leetcod-y7u0/
- 負雪明燭（二維動態規劃解法、步驟詳解）：
  https://leetcode.cn/problems/longest-common-subsequence/solutions/696989/fu-xue-ming-zhu-er-wei-dong-tai-gui-hua-r5ez6/
- 灵茶山艾府（思路引導與舉例）：
  https://leetcode.cn/problems/longest-common-subsequence/solutions/2133188/jiao-ni-yi-bu-bu-si-kao-dong-tai-gui-hua-lbz5/

> 建議閱讀順序：先看 **力扣官方題解** 理解基礎與演算法，接著閱讀 **負雪明燭** 與 **灵茶山艾府** 的實作和進階說明以加深理解。
