# LeetCode 812: 最大三角形面積 (Largest Triangle Area)

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-13-green.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![LeetCode](https://img.shields.io/badge/LeetCode-812-orange.svg)](https://leetcode.com/problems/largest-triangle-area/)

一個基於 .NET 8 和 C# 13 的 LeetCode 解題專案，實作了兩種高效算法來解決「最大三角形面積」問題。

## 問題描述

給定一組位於 X-Y 平面上的點陣列 `points`，其中 `points[i] = [xi, yi]`。請回傳由任意三個不同點所能形成的**最大三角形的面積**。

> **注意**: 答案在 10⁻⁵ 的誤差範圍內視為正確。

### 範例

```text
輸入: points = [[0,0],[0,1],[1,0],[0,2],[2,0]]
輸出: 2.00000
解釋: 點 (0,2), (2,0), (0,0) 形成面積最大的三角形
```

## 解法實作

本專案提供了兩種數學上等價但概念不同的解法：

### 方法一：鞋帶公式 (Shoelace Formula)

**核心概念**: 利用行列式計算多邊形面積的經典公式

**數學公式**:

```text
對於三個頂點 A(x₁,y₁), B(x₂,y₂), C(x₃,y₃)：
面積 S = 0.5 × |(x₁y₂ + x₂y₃ + x₃y₁) - (y₁x₂ + y₂x₃ + y₃x₁)|
```

**算法步驟**:

1. 將三個頂點座標按順序列出
2. 計算正向乘積和：`x₁y₂ + x₂y₃ + x₃y₁`
3. 計算反向乘積和：`y₁x₂ + y₂x₃ + y₃x₁`
4. 取差值的絕對值並乘以 0.5

**視覺化理解**:

```text
(x₁, y₁) ──→ (x₂, y₂)
   ↓            ↓
(x₃, y₃) ←── (x₁, y₁)

正向: x₁y₂, x₂y₃, x₃y₁ (順時針)
反向: y₁x₂, y₂x₃, y₃x₁ (逆時針)
```

### 方法二：向量叉積法 (Cross Product Method)

**核心概念**: 利用向量幾何中叉積的性質計算平行四邊形面積

**數學公式**:

```text
對於三個點 A(x₁,y₁), B(x₂,y₂), C(x₃,y₃)：
向量 AB = (x₂-x₁, y₂-y₁)
向量 AC = (x₃-x₁, y₃-y₁)
叉積 AB × AC = (x₂-x₁)(y₃-y₁) - (y₂-y₁)(x₃-x₁)
面積 S = 0.5 × |AB × AC|
```

**幾何意義**:

- 兩個向量的叉積等於它們形成的平行四邊形面積
- 三角形面積是平行四邊形面積的一半
- 叉積的符號表示向量的相對方向（順時針或逆時針）

**視覺化理解**:

```text
     C(x₃,y₃)
    /|
   / |
  /  | AC
 /   |
A────→ B
  AB   (x₂,y₂)

平行四邊形面積 = |AB × AC|
三角形面積 = 0.5 × |AB × AC|
```

## 算法比較

| 特性 | 鞋帶公式 | 向量叉積法 |
|------|----------|------------|
| **數學基礎** | 行列式理論 | 向量代數 |
| **幾何直觀** | ⭐⭐⭐ | ⭐⭐⭐⭐⭐ |
| **計算步驟** | 6次乘法 + 1次減法 | 4次減法 + 2次乘法 + 1次減法 |
| **記憶難度** | 中等（需記住公式模式） | 簡單（概念直觀） |
| **擴展性** | 適用於任意多邊形 | 主要用於三角形 |
| **數值穩定性** | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐⭐ |

### 共同優點

- ✅ 時間複雜度：O(n³) - 遍歷所有三點組合
- ✅ 空間複雜度：O(1) - 只使用常數額外空間
- ✅ 數值穩定性高 - 只需基本算術運算
- ✅ 適合整數座標輸入
- ✅ 不需要平方根運算（相比海倫公式）

### 實際效能

兩種方法在數學上完全等價，實際執行結果完全一致：

- 計算精度：雙精度浮點數 (double)
- 誤差範圍：< 10⁻¹⁰（遠小於題目要求的 10⁻⁵）
- 執行效率：相近（編譯器優化後差異可忽略）

## 快速開始

### 前置需求
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) 或更高版本

### 建置與執行

```bash
# 複製專案
git clone <repository-url>
cd leetcode_812

# 建置專案
dotnet build

# 執行測試
dotnet run
```

### 預期輸出

```text
=== LeetCode 812: 最大三角形面積 - 雙解法比較 ===

測試案例 1: points = [[0,0],[0,1],[1,0],[0,2],[2,0]]
方法一 (鞋帶公式): 2.00000
方法二 (向量叉積): 2.00000
預期結果: 2.00000
結果一致: True

測試案例 2: points = [[1,0],[0,0],[0,1]]
方法一 (鞋帶公式): 0.50000
方法二 (向量叉積): 0.50000
預期結果: 0.50000
結果一致: True

測試案例 3: points = [[0,0],[1,1],[2,0],[1,2],[3,3]]
方法一 (鞋帶公式): 3.00000
方法二 (向量叉積): 3.00000
結果一致: True

=== 效能與精度測試 ===
兩種方法在數學上等價，計算結果完全一致
時間複雜度: O(n³) - 遍歷所有三點組合
空間複雜度: O(1) - 只使用常數額外空間
```

## 程式碼結構

```text
leetcode_812/
├── Program.cs                          # 主程式檔案
│   ├── LargestTriangleArea()          # 方法一：鞋帶公式
│   ├── LargestTriangleAreaCrossProduct()  # 方法二：向量叉積法
│   └── Main()                         # 測試案例與結果比較
├── leetcode_812.csproj                # 專案設定檔
└── README.md                          # 本文件
```

## 技術特色

### C# 現代語法
- 檔案範圍命名空間 (File-scoped namespace)
- 集合表達式 (Collection expressions): `[[0, 0], [0, 1]]`
- 字串插值 (String interpolation): `$"結果: {result:F5}"`
- XML 文件註解完整覆蓋

### 數學精確性
- 使用 `Math.Abs()` 確保面積為正值
- 使用 `Math.Max()` 高效比較
- 雙精度浮點數避免精度損失

### 程式碼品質
- 清晰的變數命名
- 詳細的演算法註解
- 完整的公式推導說明
- 單一職責原則

## 延伸閱讀

### 相關算法
- **海倫公式**: 使用三邊長計算面積（需要開方運算）
- **行列式法**: 鞋帶公式的矩陣表示形式
- **座標變換法**: 將頂點移至原點簡化計算

### LeetCode 相關題目
- [836. Rectangle Overlap](https://leetcode.com/problems/rectangle-overlap/)
- [858. Mirror Reflection](https://leetcode.com/problems/mirror-reflection/)
- [892. Surface Area of 3D Shapes](https://leetcode.com/problems/surface-area-of-3d-shapes/)

### 數學背景
- [鞋帶公式 - 維基百科](https://zh.wikipedia.org/wiki/鞋帶公式)
- [向量叉積 - 維基百科](https://zh.wikipedia.org/wiki/叉積)
- [計算幾何基礎](https://zh.wikipedia.org/wiki/計算幾何)

## 專案資訊

- **LeetCode 題號**: 812
- **難度**: Easy
- **標籤**: Math, Geometry
- **時間複雜度**: O(n³)
- **空間複雜度**: O(1)
- **.NET 版本**: 8.0
- **C# 版本**: 13.0
