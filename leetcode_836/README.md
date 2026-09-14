# LeetCode 836：矩形重疊

本專案使用 C# / .NET 10 實作 LeetCode 836「Rectangle Overlap」，並以同一組固定測試資料驗證兩種幾何判斷方法。

- [English problem：Rectangle Overlap](https://leetcode.com/problems/rectangle-overlap/)
- [中文題目：矩形重疊](https://leetcode.cn/problems/rectangle-overlap/description/)

## 閱讀導覽

1. [題目說明與限制條件](#1-題目說明與限制條件)
2. [快速開始](#2-快速開始)
3. [解題概念與出發點](#3-解題概念與出發點)
4. [方法一：檢查相對位置](#4-方法一檢查相對位置)
5. [方法二：檢查投影區域](#5-方法二檢查投影區域)
6. [方法比較與複雜度](#6-方法比較與複雜度)
7. [測試資料與實際輸出](#7-測試資料與實際輸出)
8. [專案結構與參考資料](#8-專案結構與參考資料)

## 1. 題目說明與限制條件

### 題目說明

一個軸對齊矩形使用四個整數 `[x1, y1, x2, y2]` 表示：

- `(x1, y1)` 是左下角。
- `(x2, y2)` 是右上角。
- 上、下邊平行於 x 軸，左、右邊平行於 y 軸。

給定兩個矩形 `rec1` 與 `rec2`，如果它們的交集面積大於 0，就回傳 `true`；如果只是邊接觸或角落接觸，交集面積為 0，必須回傳 `false`。

### 輸入與輸出

在 C# 中，兩個輸入都是 `int[]`：

```text
rec = [x1, y1, x2, y2]
```

兩個解法的公開方法都是：

```csharp
bool IsRectangleOverlap(int[] rec1, int[] rec2)
bool IsRectangleOverlap2(int[] rec1, int[] rec2)
```

回傳值的意義：

- `true`：兩個矩形在 x 方向與 y 方向都保有正長度交集，因此交集面積大於 0。
- `false`：至少有一個方向沒有正長度交集，包含完全分離、邊接觸與角落接觸。

### 限制條件

依 [LeetCode 836 官方題目頁](https://leetcode.com/problems/rectangle-overlap/)，輸入契約如下：

- `rec1.Length == 4`。
- `rec2.Length == 4`。
- `rec1[i]` 與 `rec2[i]` 都介於 `-10^9` 與 `10^9` 之間。
- `rec1` 與 `rec2` 都代表合法且非零面積的矩形，也就是 `x1 < x2` 且 `y1 < y2`。

本專案依賴題目保證的輸入格式，不額外處理 `null` 或錯誤長度的陣列。方法一保留零寬或零高矩形的防禦性判斷；這類輸入不屬於題目正式限制，但可避免退化矩形被誤判為重疊。

## 2. 快速開始

請在本專案根目錄，也就是包含本 README 的 `leetcode_836` 目錄中執行：

### 還原與建置

```bash
dotnet restore leetcode_836/leetcode_836.csproj
dotnet build leetcode_836/leetcode_836.csproj --nologo
```

### 執行固定測試

```bash
dotnet run --project leetcode_836/leetcode_836.csproj --no-build --nologo
```

`Program.cs` 的 `Main` 會執行 10 組案例。每組案例都會呼叫兩個解法，因此總共會執行 20 項檢查；成功時最後一行應為：

```text
Summary: 20/20 checks passed.
```

如果任一方法得到錯誤結果，測試 harness 會輸出 `FAIL`，並將程式結束碼設為 `1`，方便在腳本或 CI 中辨識失敗。

本專案目前沒有獨立的測試專案或測試套件；`Main` 中的固定案例是可直接執行的 smoke test 與教學示範入口。

## 3. 解題概念與出發點

### 從「面積」轉成「兩個方向的長度」

兩個軸對齊矩形的交集若存在，交集本身仍然是軸對齊矩形。它的面積可以看成：

```text
交集面積 = 交集寬度 × 交集高度
```

要讓面積大於 0，必須同時滿足：

1. x 軸方向的交集寬度大於 0。
2. y 軸方向的交集高度大於 0。

因此「是否重疊」可以轉化成「兩個區間在 x 軸與 y 軸上是否都具有嚴格交集」。

### 為什麼不能把接觸算成重疊

假設兩個矩形在 x 方向的邊界剛好相等：

```text
rec1 的右邊界 = rec2 的左邊界
```

它們最多只共享一條垂直邊，交集寬度是 0，所以交集面積也是 0。角落接觸則是在 x、y 兩個方向都只剩下邊界點，同樣不算重疊。

這也是兩個方法都使用「嚴格交集」的原因：

- 方法一用 `<=` 或 `>=` 表示完全分離，等號會把接觸邊界歸類為不重疊。
- 方法二用 `>` 檢查投影交集長度，等號會直接判定該方向沒有正長度交集。

## 4. 方法一：檢查相對位置

### 核心想法

方法一先反向思考：如果兩個矩形沒有正面積重疊，那麼 `rec1` 必定完全位於 `rec2` 的某一側：

### 幾何圖示：相對位置

以下簡圖只強調兩個矩形的相對位置，不代表實際座標比例。只要 `rec1` 在 `rec2` 的左、右、上、下任一側完全分離，就沒有正面積交集：

```text
不重疊：rec1 在左側       不重疊：rec1 在右側
+-------+ +-------+       +-------+ +-------+
| rec1  | | rec2  |       | rec2  | | rec1  |
+-------+ +-------+       +-------+ +-------+

不重疊：rec1 在上方       不重疊：rec1 在下方
   +-------+                   +-------+
   | rec1  |                   | rec2  |
   +-------+                   +-------+
   +-------+                   +-------+
   | rec2  |                   | rec1  |
   +-------+                   +-------+

正面積重疊：
+-----------+
|    rec1   |
|      +-----------+
|      |  overlap  |
+------+-----------+
       |    rec2   |
       +-----------+
```

圖中的上、下、左、右分離，分別對應「某一個邊界碰不到另一個矩形」；最後一張圖則表示兩個方向都保留一段共同區域，才會形成正面積重疊。

| 分離方向 | 判斷條件 | 幾何意義 |
| --- | --- | --- |
| 左側 | `rec1[2] <= rec2[0]` | `rec1` 的右邊界不在 `rec2` 左邊界右方 |
| 下方 | `rec1[3] <= rec2[1]` | `rec1` 的上邊界不在 `rec2` 下邊界上方 |
| 右側 | `rec1[0] >= rec2[2]` | `rec1` 的左邊界不在 `rec2` 右邊界左方 |
| 上方 | `rec1[1] >= rec2[3]` | `rec1` 的下邊界不在 `rec2` 上邊界下方 |

只要四個條件有任何一個成立，就可以立刻知道沒有正面積交集。程式最後使用 `!` 反轉這個「不重疊條件」：四個條件全部不成立時，才回傳 `true`。

### 執行流程

1. 先檢查兩個矩形是否有零寬或零高；若有，回傳 `false`。
2. 檢查 `rec1` 是否完全在 `rec2` 左側、下方、右側或上方。
3. 若任一分離條件成立，回傳 `false`。
4. 若四個條件都不成立，代表 x 與 y 方向都仍有正長度交集，回傳 `true`。

對應的核心判斷可寫成：

```csharp
return !(rec1[2] <= rec2[0] ||
         rec1[3] <= rec2[1] ||
         rec1[0] >= rec2[2] ||
         rec1[1] >= rec2[3]);
```

### 方法一範例走查

使用官方範例一：

```text
rec1 = [0, 0, 2, 2]
rec2 = [1, 1, 3, 3]
```

逐一檢查：

1. `rec1[2] <= rec2[0]` 是 `2 <= 1`，不成立。
2. `rec1[3] <= rec2[1]` 是 `2 <= 1`，不成立。
3. `rec1[0] >= rec2[2]` 是 `0 >= 3`，不成立。
4. `rec1[1] >= rec2[3]` 是 `0 >= 3`，不成立。
5. 沒有任何完全分離條件成立，因此回傳 `true`。

再看官方範例二的邊緣接觸：

```text
rec1 = [0, 0, 1, 1]
rec2 = [1, 0, 2, 1]
```

此時 `rec1[2] <= rec2[0]` 為 `1 <= 1`，等號成立，代表兩者只共享邊界，方法一回傳 `false`。

### 正確性重點

在兩個輸入都是合法非零矩形的前提下，若 x 方向沒有正長度交集，兩個矩形必定左、右分離；若 y 方向沒有正長度交集，必定上、下分離。因此四種分離條件完整涵蓋所有不重疊情況。反過來，四種條件都不成立表示兩個方向都保有正長度交集，交集面積必定大於 0。

## 5. 方法二：檢查投影區域

### 核心想法

把矩形投影到兩個座標軸上：

- x 軸投影是區間 `[x1, x2]`。
- y 軸投影是區間 `[y1, y2]`。

### 幾何圖示：x/y 軸投影

以 `rec1 = [0, 0, 2, 2]`、`rec2 = [1, 1, 3, 3]` 為例。每個矩形在單一座標軸上都會變成一條線段；兩條線段共同保留的部分，就是該方向的投影交集：

```text
x 軸（水平投影）
rec1: 0 |----------| 2
rec2:     1 |----------| 3
              +----+
              1    2   <- 投影交集 [1, 2]

y 軸（垂直投影）
rec1: 0 |----------| 2
rec2:     1 |----------| 3
              +----+
              1    2   <- 投影交集 [1, 2]

邊緣接觸的 x 軸投影
rec1: 0 |----| 1
rec2:          1 |----| 2
              max(left) = min(right) = 1
              1 > 1 -> false
```

在兩個方向上，左端點都是 `max(0, 1) = 1`，右端點都是 `min(2, 3) = 2`，所以交集保有正長度 `2 - 1`。x、y 兩軸都成立時，二維平面上才會留下正面積的矩形區域。

邊緣接觸時，投影交集只剩下一個端點，不能算正長度。上圖的 `1 > 1 -> false` 直接說明為什麼程式必須使用嚴格的 `>`；若使用 `>=`，只有邊界接觸的矩形就會被誤判為重疊。

兩個區間的交集右端點是兩個右端點的較小值，左端點是兩個左端點的較大值。因此 x 軸必須滿足：

```text
min(rec1[2], rec2[2]) > max(rec1[0], rec2[0])
```

y 軸同理：

```text
min(rec1[3], rec2[3]) > max(rec1[1], rec2[1])
```

兩個條件都成立，才代表交集同時具有正寬度與正高度。

### 執行流程

1. 取兩個矩形右邊界的較小值，以及左邊界的較大值，判斷水平投影是否有正長度交集。
2. 取兩個矩形上邊界的較小值，以及下邊界的較大值，判斷垂直投影是否有正長度交集。
3. 用 `&&` 合併兩個方向的結果。

核心程式概念如下：

```csharp
bool hasHorizontalOverlap =
    Math.Min(rec1[2], rec2[2]) > Math.Max(rec1[0], rec2[0]);
bool hasVerticalOverlap =
    Math.Min(rec1[3], rec2[3]) > Math.Max(rec1[1], rec2[1]);

return hasHorizontalOverlap && hasVerticalOverlap;
```

### 方法二範例走查

仍使用官方範例一：

```text
rec1 = [0, 0, 2, 2]
rec2 = [1, 1, 3, 3]
```

水平投影：

```text
min(2, 3) = 2
max(0, 1) = 1
2 > 1  => 有正長度水平交集
```

垂直投影：

```text
min(2, 3) = 2
max(0, 1) = 1
2 > 1  => 有正長度垂直交集
```

兩個方向都成立，所以回傳 `true`。

對於邊緣接觸的官方範例二，水平投影會得到：

```text
min(1, 2) = 1
max(0, 1) = 1
1 > 1  => false
```

雖然垂直投影仍有正長度交集，但水平條件已經是 `false`，最後的 `&&` 會回傳 `false`，正確排除只有邊接觸的情況。

### 正確性重點

矩形交集存在且面積大於 0 的必要條件，是 x 投影與 y 投影都必須有正長度交集；對軸對齊矩形而言，這個條件也充分保證兩個矩形的交集包含一個具有正寬度與正高度的區域。因此兩個嚴格投影條件的合取，與題目要求完全等價。

## 6. 方法比較與複雜度

| 項目 | 方法一：相對位置 | 方法二：投影區域 |
| --- | --- | --- |
| 判斷角度 | 先找出不重疊的四種情況 | 直接確認兩個方向都有正長度交集 |
| 核心運算 | 比較四組邊界 | `Math.Min`、`Math.Max` 與嚴格 `>` |
| 時間複雜度 | `O(1)` | `O(1)` |
| 額外空間 | `O(1)` | `O(1)` |
| 邊接觸處理 | `<=`、`>=` 使接觸歸類為不重疊 | `>` 排除零長度投影交集 |
| 教學重點 | 從反例列舉所有分離方向 | 將二維問題拆成兩個一維區間問題 |

兩個方法都只讀取輸入座標，不修改 `rec1` 或 `rec2`。`Main` 會使用同一個 `Program` 物件反覆呼叫兩個方法，讓固定案例也能檢查解法沒有依賴跨呼叫的可變狀態。

## 7. 測試資料與實際輸出

### 固定測試資料

每一組案例都會同時通過方法一與方法二；表格中的預期值是單一方法應該得到的結果，因此 10 組案例共計 20 項檢查。

| # | 案例 | `rec1` | `rec2` | 預期 | 驗證重點 |
| ---: | --- | --- | --- | --- | --- |
| 1 | 官方範例一：部分重疊 | `[0,0,2,2]` | `[1,1,3,3]` | `true` | 一般正面積重疊 |
| 2 | 官方範例二：邊緣接觸 | `[0,0,1,1]` | `[1,0,2,1]` | `false` | x 方向等號接觸 |
| 3 | 官方範例三：角落接觸 | `[0,0,1,1]` | `[2,2,3,3]` | `false` | x、y 方向都沒有正長度交集 |
| 4 | 包含關係 | `[-1,-1,4,4]` | `[0,0,2,2]` | `true` | 一個矩形完全包含另一個 |
| 5 | `rec1` 位於 `rec2` 右側 | `[1,0,2,1]` | `[0,0,1,1]` | `false` | 右側分離與對稱輸入 |
| 6 | `rec1` 位於 `rec2` 下方 | `[0,0,2,2]` | `[0,2,2,4]` | `false` | 下方分離 |
| 7 | `rec1` 位於 `rec2` 上方 | `[0,2,2,4]` | `[0,0,2,2]` | `false` | 上方分離 |
| 8 | 負座標部分重疊 | `[-4,-3,1,2]` | `[-2,-1,3,4]` | `true` | 負座標與一般重疊 |
| 9 | 完全相同矩形 | `[-2,-2,3,3]` | `[-2,-2,3,3]` | `true` | 相同矩形 |
| 10 | 座標邊界部分重疊 | `[-10^9,-10^9,0,0]` | `[-1,-1,10^9,10^9]` | `true` | 題目座標範圍邊界 |

測試案例沒有把零寬或零高矩形列入正式清單，因為那不符合題目「合法且非零面積矩形」的輸入限制；方法一的額外防禦判斷仍保留在實作中。

### 實際執行輸出

以下內容是執行 `dotnet run --project leetcode_836/leetcode_836.csproj --no-build --nologo` 所得到的完整輸出：

```text
Case: 官方範例一：部分重疊
Input: rec1 = [0, 0, 2, 2], rec2 = [1, 1, 3, 3]
Expected: True
Method 1 Actual: True - PASS
Method 2 Actual: True - PASS

Case: 官方範例二：邊緣接觸
Input: rec1 = [0, 0, 1, 1], rec2 = [1, 0, 2, 1]
Expected: False
Method 1 Actual: False - PASS
Method 2 Actual: False - PASS

Case: 官方範例三：角落接觸
Input: rec1 = [0, 0, 1, 1], rec2 = [2, 2, 3, 3]
Expected: False
Method 1 Actual: False - PASS
Method 2 Actual: False - PASS

Case: 包含關係
Input: rec1 = [-1, -1, 4, 4], rec2 = [0, 0, 2, 2]
Expected: True
Method 1 Actual: True - PASS
Method 2 Actual: True - PASS

Case: rec1 位於 rec2 右側
Input: rec1 = [1, 0, 2, 1], rec2 = [0, 0, 1, 1]
Expected: False
Method 1 Actual: False - PASS
Method 2 Actual: False - PASS

Case: rec1 位於 rec2 下方
Input: rec1 = [0, 0, 2, 2], rec2 = [0, 2, 2, 4]
Expected: False
Method 1 Actual: False - PASS
Method 2 Actual: False - PASS

Case: rec1 位於 rec2 上方
Input: rec1 = [0, 2, 2, 4], rec2 = [0, 0, 2, 2]
Expected: False
Method 1 Actual: False - PASS
Method 2 Actual: False - PASS

Case: 負座標部分重疊
Input: rec1 = [-4, -3, 1, 2], rec2 = [-2, -1, 3, 4]
Expected: True
Method 1 Actual: True - PASS
Method 2 Actual: True - PASS

Case: 完全相同矩形
Input: rec1 = [-2, -2, 3, 3], rec2 = [-2, -2, 3, 3]
Expected: True
Method 1 Actual: True - PASS
Method 2 Actual: True - PASS

Case: 座標邊界部分重疊
Input: rec1 = [-1000000000, -1000000000, 0, 0], rec2 = [-1, -1, 1000000000, 1000000000]
Expected: True
Method 1 Actual: True - PASS
Method 2 Actual: True - PASS

Summary: 20/20 checks passed.
```

## 8. 專案結構與參考資料

### 專案結構

```text
leetcode_836/
├── AGENTS.md
├── README.md
├── .editorconfig
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── docs/
│   └── readme-template.md
└── leetcode_836/
    ├── Program.cs
    └── leetcode_836.csproj
```

- `leetcode_836/Program.cs`：主控台入口、固定測試資料與兩個矩形重疊解法。
- `leetcode_836/leetcode_836.csproj`：.NET 10 可執行專案設定。
- `.vscode/launch.json`、`.vscode/tasks.json`：VS Code 建置與偵錯設定。
- `docs/readme-template.md`：README 初次建立時使用的文件範本。

### 參考資料

- [LeetCode 836：Rectangle Overlap](https://leetcode.com/problems/rectangle-overlap/)
- [LeetCode 中文：矩形重疊](https://leetcode.cn/problems/rectangle-overlap/description/)
