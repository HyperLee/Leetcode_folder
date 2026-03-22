# LeetCode 1886. Determine Whether Matrix Can Be Obtained By Rotation

> **判斷矩陣經旋轉後是否一致**

| 項目 | 內容 |
|------|------|
| 難度 | Easy |
| 題目連結 | [English](https://leetcode.com/problems/determine-whether-matrix-can-be-obtained-by-rotation/) · [中文](https://leetcode.cn/problems/determine-whether-matrix-can-be-obtained-by-rotation/) |
| 分類 | Array, Matrix |
| 語言 | C# (.NET 10) |

## 題目描述

給定兩個 `n x n` 的二進位矩陣 `mat` 與 `target`，若可以透過將 `mat` 以 **90 度**為單位旋轉（0°、90°、180°、270°），使其與 `target` 相等，則回傳 `true`，否則回傳 `false`。

> Given two `n x n` binary matrices `mat` and `target`, return `true` if it is possible to make `mat` equal to `target` by rotating `mat` in 90-degree increments, or `false` otherwise.

### 限制條件

- `n == mat.length == target.length`
- `n == mat[i].length == target[i].length`
- `1 <= n <= 10`
- `mat[i][j]` 和 `target[i][j]` 只有 `0` 或 `1`

### 範例

**Example 1**

```
Input:  mat = [[0,1],[1,0]],  target = [[1,0],[0,1]]
Output: true
```

旋轉 90° 一次即可與 target 一致。

**Example 2**

```
Input:  mat = [[0,1],[1,1]],  target = [[1,0],[0,1]]
Output: false
```

無論旋轉幾次都無法與 target 一致。

**Example 3**

```
Input:  mat = [[0,0,0],[0,1,0],[1,1,1]],  target = [[1,1,1],[0,1,0],[0,0,0]]
Output: true
```

旋轉 180° 後與 target 一致。

---

## 解題概念與出發點

### 核心觀察

一個矩陣順時針旋轉 90° 共 **4 次**後，會回到原始狀態（360°）。因此：

- 只需要檢查 **最多 4 種旋轉結果**（0°、90°、180°、270°）是否有任一與 `target` 相等。
- 若 4 次旋轉都不匹配，即可確定答案為 `false`。

### 思路

1. 對 `mat` 進行**原地順時針 90° 旋轉**。
2. 旋轉完成後，與 `target` **逐元素比較**。
3. 若比對相等，立即回傳 `true`。
4. 重複上述步驟，最多執行 4 輪。
5. 4 輪皆不相等，回傳 `false`。

---

## 解法詳細說明

### 原地旋轉 — 四點循環交換法

順時針旋轉 90° 的等價操作：對矩陣左上角 1/4 區域中的每個位置 `(i, j)`，執行四點循環交換：

```
(i, j) ← (n-1-j, i) ← (n-1-i, n-1-j) ← (j, n-1-i)
```

只需遍歷 `i ∈ [0, n/2)`、`j ∈ [0, (n+1)/2)` 即可完成整個矩陣旋轉，空間複雜度 O(1)。

#### 補充 1：座標映射公式推導

對 n×n 矩陣做順時針旋轉 90°，每個位置的變換規則為：

```
(r, c)  →  (c, n-1-r)
```

#### 補充 2：四點循環的由來

從任意起點 `(i, j)` 連續套用旋轉公式四次，會形成一個循環並回到原點：

```
(i,     j    )
   → (j,     n-1-i)
      → (n-1-i, n-1-j)
         → (n-1-j, i    )
            → (i,     j    )  ← 回到原點
```

命名這四個點為 **A、B、C、D**：

```
A = (i,     j    )
B = (j,     n-1-i)
C = (n-1-i, n-1-j)
D = (n-1-j, i    )
```

#### 補充 3：圖示 — 4×4 矩陣角落（i=0, j=0）

```
旋轉前 mat:              旋轉後 mat:

  A  .  .  B               D  .  .  A
  .  .  .  .     →         .  .  .  .
  .  .  .  .               .  .  .  .
  D  .  .  C               C  .  .  B

A=(0,0)  B=(0,3)         值的流向（順時針）：
C=(3,3)  D=(3,0)
                               A ──→ B
                               ↑     │
                               │     ↓
                               D ←── C
```

#### 補充 4：圖示 — 4×4 矩陣第二圈（i=0, j=1）

```
旋轉前 mat:              旋轉後 mat:

  .  A  .  .               .  D  .  .
  .  .  .  B     →         .  .  .  A
  D  .  .  .               C  .  .  .
  .  .  C  .               .  .  B  .

A=(0,1)  B=(1,3)
C=(3,2)  D=(2,0)
```

#### 補充 5：原地交換逐步拆解

因為是**原地**操作，若直接 `A ← D` 就會蓋掉 A 的原始值，後續無法把 A 的值給 B。  
解法：先用 `temp` 備份 A，最後再從 `temp` 寫入 B。

```csharp
int temp          = mat[i][j];              // ① 備份 A
mat[i][j]         = mat[n-1-j][i];          // ② A ← D
mat[n-1-j][i]     = mat[n-1-i][n-1-j];     // ③ D ← C
mat[n-1-i][n-1-j] = mat[j][n-1-i];         // ④ C ← B
mat[j][n-1-i]     = temp;                   // ⑤ B ← A（從備份）
```

以 A=1、B=2、C=3、D=4 為例，追蹤每個步驟的記憶體狀態：

```
步驟前：  A=1  B=2  C=3  D=4

① temp = 1               A=1  B=2  C=3  D=4  temp=1
② A ← D(4)               A=4  B=2  C=3  D=4  temp=1
③ D ← C(3)               A=4  B=2  C=3  D=3  temp=1
④ C ← B(2)               A=4  B=2  C=2  D=3  temp=1
⑤ B ← temp(1)            A=4  B=1  C=2  D=3  temp=1

步驟後：  A=4  B=1  C=2  D=3  ✓ 順時針旋轉完成
```

#### 補充 6：迴圈範圍說明

每次四點交換同時處理 4 個格子，整個 n×n 矩陣共分成 n²/4 組，對應左上角 1/4 區域：

| 迴圈變數 | 範圍 | 原因 |
|----------|------|------|
| `i` | `0 ～ n/2 - 1` | 只需上半部（n 為奇數時中間行旋轉後位置不變） |
| `j` | `0 ～ (n+1)/2 - 1` | 含奇數 n 的中間列 |

以 n=3 為例（`n/2=1`、`(n+1)/2=2`），需處理的格子為 `(0,0)` 和 `(0,1)`：

```
X  X  .
.  .  .    ← 中心點 (1,1) 旋轉後位置不變，不需處理
.  .  .
```

### 比較函式

遍歷矩陣所有位置 `(i, j)`，逐一比較 `mat[i][j]` 與 `target[i][j]`，任一不同即回傳 `false`。

### 複雜度分析

| | 複雜度 |
|---|---|
| 時間 | O(n²) — 最多旋轉 4 次，每次旋轉與比較皆為 O(n²)，常數 4 可忽略 |
| 空間 | O(1) — 原地旋轉，不需額外空間 |

---

## 舉例演示流程

以 `mat = [[0,1],[1,0]]`、`target = [[1,0],[0,1]]` 為例（n = 2）：

### 第 1 次旋轉（90°）

四點循環交換 `(0,0)` 位置：

```
旋轉前 mat:       旋轉後 mat:
  0  1               1  0
  1  0               0  1
```

交換過程：

```
temp = mat[0][0] = 0
mat[0][0] = mat[1][0] = 1
mat[1][0] = mat[1][1] = 0
mat[1][1] = mat[0][1] = 1
mat[0][1] = temp      = 0
```

旋轉後 `mat = [[1,0],[0,1]]`。

### 比較

```
mat:        target:
  1  0        1  0
  0  1        0  1
```

所有元素皆相等 → 回傳 **true**。

---

## 快速開始

```bash
# 建構
dotnet build

# 執行
dotnet run --project leetcode_1886
```

預期輸出：

```
Example 1: True
Example 2: False
Example 3: True
```

ref:
- [48. 旋转图像](https://leetcode.cn/problems/rotate-image/description/)

- [48. Rotate Image](https://leetcode.com/problems/rotate-image/description/)
