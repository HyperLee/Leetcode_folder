# LeetCode 3625: 統計梯形的數目 II

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-14-239120?style=flat-square&logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)
[![LeetCode](https://img.shields.io/badge/LeetCode-3625-FFA116?style=flat-square&logo=leetcode)](https://leetcode.cn/problems/count-number-of-trapezoids-ii/)

使用雜湊表與幾何數學方法解決 LeetCode 3625「統計梯形的數目 II」問題。

## 題目說明

給定一組 2D 平面上的點，計算可以形成**梯形**的四元組數量。

**梯形定義**：

- 恰好有一對邊平行（斜率相同但不共線）
- 不能是平行四邊形

## 解題思路

### 核心概念

本題使用**雜湊表 + 幾何數學**的方法：

1. **平行線段識別**：相同斜率但不同截距的線段為平行線段
2. **平行四邊形排除**：平行四邊形的兩條對角線交於同一中點，透過中點識別並排除

### 資料結構

| 雜湊表 | 鍵 (Key) | 值 (Value) | 用途 |
|:------|:---------|:-----------|:-----|
| `slopeToIntercept` | 斜率 k | 截距列表 | 找出平行線段對 |
| `midToSlope` | 中點編碼 | 斜率列表 | 識別平行四邊形 |

## 數學公式

### 斜率計算

$$k = \frac{y_2 - y_1}{x_2 - x_1}$$

> [!NOTE]
> 當 $x_2 = x_1$ 時（垂直線），使用特殊值 `MOD = 1e9 + 7` 表示斜率。

### 截距計算

$$b = y_1 - k \cdot x_1 = \frac{y_1 \cdot dx - x_1 \cdot dy}{dx}$$

其中 $dx = x_1 - x_2$，$dy = y_1 - y_2$

### 中點編碼

$$mid = (x_1 + x_2) \times 10000 + (y_1 + y_2)$$

> [!IMPORTANT]
> 乘以 10000 是為了確保不同中點產生唯一的編碼值。

### 結果計算

$$梯形數量 = \sum_{斜率相同} (不同截距的配對數) - \sum_{中點相同} (不同斜率的配對數)$$

## 範例推演

### 輸入範例

```text
points = [[0,0], [1,1], [1,0], [2,0]]
```

### 步驟 1：計算所有線段的斜率、截距和中點

| 線段 | 點 1 | 點 2 | 斜率 k | 截距 b | 中點編碼 |
|:-----|:-----|:-----|:-------|:-------|:---------|
| L1 | (0,0) | (1,1) | 1 | 0 | 10001 |
| L2 | (0,0) | (1,0) | 0 | 0 | 10000 |
| L3 | (0,0) | (2,0) | 0 | 0 | 20000 |
| L4 | (1,1) | (1,0) | MOD | 1 | 20001 |
| L5 | (1,1) | (2,0) | -1 | 2 | 30001 |
| L6 | (1,0) | (2,0) | 0 | 0 | 30000 |

### 步驟 2：依斜率分組

```text
slopeToIntercept:
  k=0   → [0, 0, 0]  (L2, L3, L6 都是水平線)
  k=1   → [0]
  k=MOD → [1]
  k=-1  → [2]
```

### 步驟 3：計算平行線段配對

對於 `k=0`，有三條線段都是截距 0：

- 相同截距的線段屬於同一條直線，不能組成梯形
- 此例中所有水平線都共線，無法配對

### 步驟 4：扣除平行四邊形

檢查中點相同但斜率不同的線段對，將其從結果中扣除。

### 最終結果

此範例中，所有水平線 (k=0) 的線段都共線（截距都是 0），無法組成梯形。

```text
梯形數量 = 0
```

## 複雜度分析

| 項目 | 複雜度 | 說明 |
|:-----|:-------|:-----|
| 時間複雜度 | $O(n^2)$ | 遍歷所有點對 |
| 空間複雜度 | $O(n^2)$ | 儲存所有線段資訊 |

## 快速開始

### 環境需求

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### 執行程式

```bash
# 建構專案
dotnet build

# 執行程式
dotnet run --project leetcode_3625/leetcode_3625.csproj
```

### 預期輸出

```text
測試範例 1: 0 個梯形
測試範例 2: 3 個梯形
測試範例 3: 1 個梯形
測試範例 4: 0 個梯形 (空陣列)
測試範例 5: 0 個梯形 (三角形)

所有測試完成！
```

## 程式碼結構

```text
leetcode_3625/
├── leetcode_3625.csproj    # 專案檔
├── Program.cs              # 主程式與解題實作
└── README.md               # 本文件
```

## 關鍵程式碼片段

```csharp
// 計算斜率與截距
if (x2 == x1)
{
    k = MOD;     // 垂直線使用特殊值
    b = x1;
}
else
{
    k = (double)(y2 - y1) / (x2 - x1);
    b = (double)(y1 * dx - x1 * dy) / dx;
}

// 中點編碼
double mid = (x1 + x2) * 10000.0 + (y1 + y2);
```

## 相關題目

- [LeetCode 3623: 統計梯形的數目 I](https://leetcode.cn/problems/count-number-of-trapezoids-i/) - 本題的簡化版本
- [LeetCode 149: 直線上最多的點數](https://leetcode.cn/problems/max-points-on-a-line/) - 使用斜率判斷共線

## 參考資料

- [力扣官方題解](https://leetcode.cn/problems/count-number-of-trapezoids-ii/solutions/3844283/tong-ji-ti-xing-de-shu-mu-ii-by-leetcode-6uwd/)
