# LeetCode 1401：Circle and Rectangle Overlapping

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/language-C%23-239120?logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)

這是一個以 .NET 10 建立的 C# 主控台專案，用來解 LeetCode 1401「Circle and Rectangle Overlapping」。程式保留兩種幾何判斷方式：第一種依圓心相對於矩形的位置分區討論；第二種直接計算圓心到矩形的最短距離。

專案的 `Main` 會自動執行 10 組固定案例，讓兩種解法各接受一次驗證，共 20 項檢查。每項檢查都會列出 `Expected`、`Actual` 與 `PASS/FAIL`，因此不需要手動輸入資料。

## 快速開始

請在目前目錄 `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_1401` 執行：

```bash
dotnet restore leetcode_1401/leetcode_1401.csproj
dotnet build leetcode_1401/leetcode_1401.csproj --nologo --no-restore
dotnet run --project leetcode_1401/leetcode_1401.csproj --no-build --no-restore --nologo
dotnet format leetcode_1401/leetcode_1401.csproj --verify-no-changes --no-restore
```

命令的用途分別是還原專案、建置、執行固定案例，以及確認程式符合 `.editorconfig` 的格式設定。專案沒有另外建立測試框架；`Main` 中的固定案例是目前可直接執行的 smoke test。

## 題目說明

給定一個圓與一個和座標軸平行的矩形：

- 圓以 `(radius, xCenter, yCenter)` 表示。
- 矩形以 `(x1, y1, x2, y2)` 表示。
- `(x1, y1)` 是矩形左下角，`(x2, y2)` 是矩形右上角。

如果圓與矩形至少共享一個點，就回傳 `true`；否則回傳 `false`。圓周上的點也屬於圓，因此只要剛好相切，就算重疊。

官方題目與範例：

- [LeetCode 英文題目](https://leetcode.com/problems/circle-and-rectangle-overlapping/description/)
- [LeetCode 中文題目](https://leetcode.cn/problems/circle-and-rectangle-overlapping/description/)

### 限制條件

依照題目限制：

- `1 <= radius <= 2000`
- `-10^4 <= xCenter, yCenter <= 10^4`
- `-10^4 <= x1 < x2 <= 10^4`
- `-10^4 <= y1 < y2 <= 10^4`

這些限制代表座標差值與平方距離可以使用 64 位元整數安全保存。程式仍明確使用 `long` 計算平方，避免 `int` 乘法溢位，也避免使用浮點數造成邊界比較誤差。

## 解題概念與出發點

判斷兩個幾何圖形是否相交時，不需要找出所有共同點，只需要找出「矩形上離圓心最近的點」：

1. 如果最近距離小於半徑，圓與矩形有內部交集。
2. 如果最近距離等於半徑，圓與矩形相切，仍然算重疊。
3. 如果最近距離大於半徑，兩者完全分離。

本專案保留兩種從不同角度出發的實作：

| 解法 | 主要觀點 | 優點 | 複雜度 |
| --- | --- | --- | --- |
| `CheckOverlap` | 將圓心相對於矩形的位置分成內部、四側與四角 | 能清楚展示幾何區域分類 | 時間 `O(1)`、空間 `O(1)` |
| `CheckOverlap2` | 分別求圓心到矩形 x/y 投影的最短距離 | 判斷規則集中、程式較精簡 | 時間 `O(1)`、空間 `O(1)` |

兩種方法都使用平方距離比較，因此不需要計算平方根；比較時使用 `<=`，以符合「相切也算重疊」的題意。

## 解法一：依圓心位置分區討論

實作位於 [`CheckOverlap`](leetcode_1401/Program.cs) 。這個方法先判斷圓心在哪一個相對區域，再決定使用邊界距離或角落距離。

### ASCII 圖解：圓心相對於矩形的分區

下圖的 `○` 代表圓心位置，矩形內的 `⊙` 代表圓心位於矩形內；圖形不按實際比例繪製。圓心在四側時檢查到邊的距離，圓心在四角對角區域時檢查到對應角落的距離。

```text
                              y
                              ↑
        ○ 左上角區域       ○ 上方區域       ○ 右上角區域
              ↘                 ↓                 ↙

        ○ 左方區域       ┌────────────────┐       ○ 右方區域
                         │  ⊙ 矩形內部   │
                         │    Rectangle  │
                         └────────────────┘

        ○ 左下角區域       ○ 下方區域       ○ 右下角區域
              ↗                 ↑                 ↖
                              └────→ x
```

分區和程式判斷的對應關係如下：

| 圖中的位置 | 使用的幾何量 | 判斷方式 |
| --- | --- | --- |
| 矩形內部 | 圓心本身 | 直接回傳 `true` |
| 上方／下方 | 圓心到水平邊的垂直距離 | 距離不超過 `radius` |
| 左方／右方 | 圓心到垂直邊的水平距離 | 距離不超過 `radius` |
| 四個角落區域 | 圓心到最近角落的距離平方 | `distanceSquared <= radius²` |

### 設計流程

#### 1. 圓心在矩形內部

若：

```text
x1 <= xCenter <= x2
y1 <= yCenter <= y2
```

圓心本身同時屬於圓與矩形，所以可以立即回傳 `true`。

#### 2. 圓心在矩形的上方或下方

當圓心的 x 座標落在矩形的水平範圍內，圓只需要靠近上邊或下邊即可相交：

```text
上方：y2 <= yCenter <= y2 + radius
下方：y1 - radius <= yCenter <= y1
```

因為圓心的水平投影已經和矩形重疊，所以只要垂直距離不超過半徑，就能找到共同點。

#### 3. 圓心在矩形的左方或右方

當圓心的 y 座標落在矩形的垂直範圍內，判斷圓心到左邊或右邊的水平距離：

```text
左方：x1 - radius <= xCenter <= x1
右方：x2 <= xCenter <= x2 + radius
```

這兩類和上下方完全對稱，只是將 x、y 軸互換。

#### 4. 圓心位於矩形角落的對角區域

如果圓心同時位於矩形的水平與垂直外側，最近點會是矩形的某個角落。對每一個角落使用距離平方公式：

```text
distanceSquared = (xCenter - cornerX)^2 + (yCenter - cornerY)^2
```

只要其中一個角落滿足：

```text
distanceSquared <= radius^2
```

就代表圓與矩形重疊。四個角落必須分別使用：

```text
左上角 (x1, y2)    左下角 (x1, y1)
右上角 (x2, y2)    右下角 (x2, y1)
```

### 解法一範例演示

以測試案例「右下角剛好落在半徑內」為例：

```text
radius = 3
圓心 = (12, -2)
矩形 = (0, 0, 10, 10)
```

1. 圓心不在矩形內。
2. 圓心的 x 座標在右側，y 座標在下側，因此不符合單純右方或下方的投影條件。
3. 最近的矩形角落是右下角 `(10, 0)`。
4. 距離平方為 `(12 - 10)^2 + (-2 - 0)^2 = 4 + 4 = 8`。
5. 半徑平方為 `3^2 = 9`。
6. 因為 `8 <= 9`，所以圓與右下角相交，結果為 `true`。

### 解法一正確性重點

矩形是軸對齊的封閉區域。對於任一圓心位置：

- 圓心在矩形內時，已經找到共同點。
- 圓心只在一個軸向外側時，最近點位於對應的矩形邊上。
- 圓心同時在兩個軸向外側時，最近點位於其中一個角落。

這三種情況涵蓋整個平面，因此檢查內部、四條邊與四個角落即可完成判斷。

## 解法二：圓心到矩形的最短距離

實作位於 [`CheckOverlap2`](leetcode_1401/Program.cs) 。這個方法不需要列出九個區域，而是直接計算圓心到矩形的最短距離平方。

### ASCII 圖解：x/y 軸投影與最近點

以下以圓心位於矩形右上方為例。`P` 是矩形上離圓心最近的角落，`dx` 與 `dy` 分別是兩個軸向的最短距離；斜線 `d` 是圓心到矩形的最短距離。

```text
                              ● C(xCenter, yCenter)
                              │╲
                           dy │ ╲ d
                              │  ╲  d² = dx² + dy²
                P=(x2, y2) ●─┴───● Q=(xCenter, y2)
                              └── dx ──→
                         ┌────────────────┐
                         │    Rectangle   │
                         └────────────────┘

x 軸投影：     x1 ├────────────────┤ x2 ────── dx ────── ● xCenter
y 軸投影：     y1 ├────────────────┤ y2 ────── dy ────── ● yCenter
```

如果圓心落在某個軸的投影區間內，該軸的距離就是 `0`；只有落在區間外，才需要計算到最近端點的距離。因此同一套圖解也涵蓋以下情況：

```text
圓心在投影區間內：       x1 ├──────── ● ────────┤ x2    → dx = 0
圓心在區間右側：         x1 ├──────────────────┤ x2 ── dx ── ●
圓心在區間左側：         ● ── dx ── x1 ├──────────────────┤ x2
```

### 設計流程

先處理 x 軸：

```text
如果 xCenter < x1：dx = x1 - xCenter
如果 xCenter > x2：dx = xCenter - x2
否則：             dx = 0
```

再以完全相同的方式處理 y 軸：

```text
如果 yCenter < y1：dy = y1 - yCenter
如果 yCenter > y2：dy = yCenter - y2
否則：             dy = 0
```

接著套用畢氏定理：

```text
distanceSquared = dx^2 + dy^2
```

最後比較：

```text
distanceSquared <= radius^2
```

當圓心落在矩形的 x 範圍內時，x 軸距離為零；當圓心落在 y 範圍內時，y 軸距離為零。因此：

- 圓心在矩形內：`dx = 0` 且 `dy = 0`。
- 圓心在矩形邊的外側：只有一個軸有距離。
- 圓心在角落的對角區域：兩個軸都有距離，合成後就是到角落的距離。

### 解法二範例演示

以官方範例 2 為例：

```text
radius = 1
圓心 = (1, 1)
矩形 = (1, -3, 2, -1)
```

1. x 軸上，`xCenter = 1` 落在 `[x1, x2] = [1, 2]`，所以 `dx = 0`。
2. y 軸上，圓心在矩形上方，最近的 y 座標是 `y2 = -1`，所以 `dy = 1 - (-1) = 2`。
3. 距離平方為 `0^2 + 2^2 = 4`。
4. 半徑平方為 `1^2 = 1`。
5. 因為 `4 > 1`，最近點也在圓外，結果為 `false`。

### 解法二正確性重點

軸對齊矩形可以拆成 x 軸區間 `[x1, x2]` 與 y 軸區間 `[y1, y2]`。圓心到矩形的最近點，必定由兩個軸上各自最近的合法座標組成；因此先求 `dx`、`dy`，再計算 `dx^2 + dy^2`，就能得到真正的最短距離平方。

## 方法比較與選擇

如果目標是學習幾何分類，`CheckOverlap` 能把圓心在矩形內、四側與四角的關係展示得很清楚；如果目標是實際寫出較集中且容易泛化的判斷，`CheckOverlap2` 通常更適合。

兩種方法的輸出應完全一致。主控台測試會對每一組輸入同時呼叫兩個方法，因此其中一個方法的分支出錯時，能在相同案例中立即看出差異。

## 可執行測試資料

目前共有 10 組案例，涵蓋官方範例、圓心在矩形內、邊界相切、角落相交、四個方向分離，以及負座標：

| 編號 | 案例 | 預期結果 | 覆蓋重點 |
| --- | --- | --- | --- |
| 01 | 官方範例 1：右側邊界相交 | `true` | 圓與矩形共享邊界點 |
| 02 | 官方範例 2：垂直距離超過半徑 | `false` | 上方完全分離 |
| 03 | 官方範例 3：矩形角落相交 | `true` | 角落與圓相交 |
| 04 | 圓心位於矩形內 | `true` | 內部快速判斷 |
| 05 | 上側邊界剛好相切 | `true` | 上邊界等於半徑 |
| 06 | 右下角剛好落在半徑內 | `true` | 右下角距離平方判斷 |
| 07 | 左側完全分離 | `false` | 左側距離超過半徑 |
| 08 | 右側邊界剛好相切 | `true` | 右邊界等於半徑 |
| 09 | 上側完全分離 | `false` | 上方距離超過半徑 |
| 10 | 負座標矩形包含圓心 | `true` | 負座標與內部判斷 |

每組案例會各執行 `CheckOverlap` 與 `CheckOverlap2`，所以總檢查數是 `10 * 2 = 20`。

## 實際執行輸出

以下內容由固定命令重新執行後產生：

```bash
dotnet run --project leetcode_1401/leetcode_1401.csproj --no-build --no-restore --nologo
```

```text
Case 01 - 官方範例 1：右側邊界相交
  CheckOverlap  Expected: True, Actual: True, PASS
  CheckOverlap2 Expected: True, Actual: True, PASS
Case 02 - 官方範例 2：垂直距離超過半徑
  CheckOverlap  Expected: False, Actual: False, PASS
  CheckOverlap2 Expected: False, Actual: False, PASS
Case 03 - 官方範例 3：矩形角落相交
  CheckOverlap  Expected: True, Actual: True, PASS
  CheckOverlap2 Expected: True, Actual: True, PASS
Case 04 - 圓心位於矩形內
  CheckOverlap  Expected: True, Actual: True, PASS
  CheckOverlap2 Expected: True, Actual: True, PASS
Case 05 - 上側邊界剛好相切
  CheckOverlap  Expected: True, Actual: True, PASS
  CheckOverlap2 Expected: True, Actual: True, PASS
Case 06 - 右下角剛好落在半徑內
  CheckOverlap  Expected: True, Actual: True, PASS
  CheckOverlap2 Expected: True, Actual: True, PASS
Case 07 - 左側完全分離
  CheckOverlap  Expected: False, Actual: False, PASS
  CheckOverlap2 Expected: False, Actual: False, PASS
Case 08 - 右側邊界剛好相切
  CheckOverlap  Expected: True, Actual: True, PASS
  CheckOverlap2 Expected: True, Actual: True, PASS
Case 09 - 上側完全分離
  CheckOverlap  Expected: False, Actual: False, PASS
  CheckOverlap2 Expected: False, Actual: False, PASS
Case 10 - 負座標矩形包含圓心
  CheckOverlap  Expected: True, Actual: True, PASS
  CheckOverlap2 Expected: True, Actual: True, PASS
Summary: 20/20 checks passed.
```

如果任何 `Actual` 與 `Expected` 不同，該行會顯示 `FAIL`，並且 `Main` 會設定 `Environment.ExitCode = 1`；全部通過時則以 `0` 結束。

## 專案結構

```text
.
├── leetcode_1401/
│   ├── Program.cs                 # 題目 XML、兩種解法與 Main 測試資料
│   └── leetcode_1401.csproj       # .NET 10 主控台專案設定
├── docs/
│   └── readme-template.md         # README 建立規範
├── AGENTS.md                      # 專案協作與驗證命令
└── README.md
```

主要程式碼請參考 [`leetcode_1401/Program.cs`](leetcode_1401/Program.cs)。原始題目描述 XML 保留在程式入口附近；解題教學、測試案例與執行說明集中在本 README。