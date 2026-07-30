# LeetCode 54 — Spiral Matrix（螺旋矩陣）

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/Language-C%23-239120)
![LeetCode](https://img.shields.io/badge/LeetCode-54-FFA116)

這是一個以 C# 與 .NET 10 撰寫的主控台教學專案。程式使用「方向陣列 + 原地訪問標記」模擬順時針螺旋移動，並在 `Main` 內執行五個固定案例，自動比對預期與實際結果。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法設計：方向模擬與原地標記](#解法設計方向模擬與原地標記)
- [五個案例的演示流程](#五個案例的演示流程)
- [複雜度分析](#複雜度分析)
- [建置與執行](#建置與執行)

## 題目說明

[LeetCode 54. Spiral Matrix](https://leetcode.com/problems/spiral-matrix/) 要求：

> 給定一個 `m × n` 的整數矩陣 `matrix`，請從左上角開始，按照順時針螺旋順序回傳所有元素。

例如：

```text
1 2 3
4 5 6
7 8 9
```

走訪順序為：

```text
1 → 2 → 3
        ↓
4 → 5   6
↑       ↓
7 ← 8 ← 9
```

因此結果是：

```text
[1, 2, 3, 6, 9, 8, 7, 4, 5]
```

## 限制條件

官方題目限制如下：

- `m == matrix.length`
- `n == matrix[i].length`
- `1 <= m, n <= 10`
- `-100 <= matrix[i][j] <= 100`

本專案另外保留了空矩陣的防禦性處理：傳入 `Array.Empty<int[]>()` 時會回傳空集合。空矩陣不是官方測資要求，但能讓方法在這個額外邊界條件下安全結束。

## 解題概念與出發點

看到「順時針螺旋」時，最直覺的想法是模擬人在矩陣中走路：

1. 從左上角 `(0, 0)` 出發。
2. 一開始面向右方。
3. 只要前方仍在矩陣內，而且尚未走過，就繼續直走。
4. 如果前方越界或已經走過，向右轉 90 度。
5. 重複以上動作，直到收集 `m × n` 個元素。

這個想法需要解決兩個核心問題：

- 如何用一致的方式表示「右、下、左、上」？
- 如何知道某個位置已經走過，避免再次進入？

目前專案只有一種解法：使用 `DIRS` 方向陣列控制移動，並以 `int.MaxValue` 直接標記走過的元素。

## 解法設計：方向模擬與原地標記

### 1. 用方向陣列表示四個方向

`DIRS` 依照順時針順序儲存四組位移：

| 方向索引 `di` | 方向 | 列位移 | 欄位移 |
| ---: | --- | ---: | ---: |
| `0` | 右 | `0` | `+1` |
| `1` | 下 | `+1` | `0` |
| `2` | 左 | `0` | `-1` |
| `3` | 上 | `-1` | `0` |

假設目前位於 `(i, j)`，下一步就是：

```text
nextRow = i + DIRS[di, 0]
nextCol = j + DIRS[di, 1]
```

如此不需要為四個方向分別撰寫四套移動邏輯。

### 2. 用方向索引完成右轉

方向陣列已排成「右 → 下 → 左 → 上」，因此右轉只需要：

```csharp
di = (di + 1) % 4;
```

取餘數 `% 4` 的作用是讓索引從 `3`（上）再前進時回到 `0`（右），形成不斷循環的順時針方向。

### 3. 判斷何時需要轉向

算出下一步位置後，只要符合下列任一條件就必須右轉：

- `nextRow < 0`：超出上邊界。
- `nextRow >= m`：超出下邊界。
- `nextCol < 0`：超出左邊界。
- `nextCol >= n`：超出右邊界。
- `matrix[nextRow][nextCol] == int.MaxValue`：下一格已經走過。

轉向後再依新的 `di` 更新 `(i, j)`。

### 4. 使用 `int.MaxValue` 標記已訪問位置

每次把目前元素加入結果後，立即執行：

```csharp
matrix[i][j] = int.MaxValue;
```

官方限制保證元素只會介於 `-100` 到 `100`，所以合法輸入不可能原本就是 `int.MaxValue`。這讓 `int.MaxValue` 可以安全地代表「此格已訪問」。

這種方式不需要另外建立 `bool[][] visited`，但代價是 `SpiralOrder` 會改寫傳入的矩陣。若呼叫端之後仍需要原始資料，必須先自行複製矩陣。

本專案的 `RunTestCase` 正是先逐列複製：

```csharp
int[][] workingMatrix = matrix.Select(row => row.ToArray()).ToArray();
```

之後才把 `workingMatrix` 傳給 `SpiralOrder`，所以同一份案例資料仍可用於顯示或其他驗證。

### 5. 為什麼迴圈固定執行 `m × n` 次？

矩陣總共有 `m × n` 格。每次迴圈恰好收集一格，而且訪問標記保證不會再次進入同一格，因此固定執行 `m × n` 次後，結果一定包含所有元素，不需要額外判斷「是否已走完」。

## 五個案例的演示流程

### 案例一：官方 3×3 方陣

```text
1 2 3
4 5 6
7 8 9
```

移動過程：

1. 從 `(0,0)` 開始向右，收集 `1, 2, 3`。
2. 右側越界，右轉向下，收集 `6, 9`。
3. 下方越界，右轉向左，收集 `8, 7`。
4. 左側越界，右轉向上，收集 `4`。
5. 上方的 `1` 已被標記，右轉向右，收集中心的 `5`。

結果：

```text
[1, 2, 3, 6, 9, 8, 7, 4, 5]
```

### 案例二：官方 3×4 長方形

```text
 1  2  3  4
 5  6  7  8
 9 10 11 12
```

外圈先依序收集：

```text
1 → 2 → 3 → 4 → 8 → 12 → 11 → 10 → 9 → 5
```

接著遇到已訪問的 `1`，轉向進入內圈，收集：

```text
6 → 7
```

結果：

```text
[1, 2, 3, 4, 8, 12, 11, 10, 9, 5, 6, 7]
```

### 案例三：單列矩陣

```text
1 2 3 4
```

初始方向就是向右，因此一路收集所有元素，不需要在完成前轉向：

```text
[1, 2, 3, 4]
```

這個案例用來確認演算法不會假設矩陣至少有兩列。

### 案例四：單欄矩陣

```text
1
2
3
```

1. 收集 `1` 後，向右會越界。
2. 右轉向下，依序收集 `2, 3`。

結果：

```text
[1, 2, 3]
```

這個案例用來確認第一次移動就必須轉向時仍能正確運作。

### 案例五：空矩陣

```text
[]
```

`matrix.Length` 為 `0`，方法會在讀取第一列前直接回傳空集合：

```text
[]
```

## 複雜度分析

令矩陣大小為 `m × n`：

| 項目 | 複雜度 | 說明 |
| --- | --- | --- |
| 時間 | `O(mn)` | 每個元素恰好被收集一次。 |
| 額外空間 | `O(1)` | 解法直接在輸入矩陣上標記，方向陣列大小固定；不計回傳結果。 |
| 回傳結果 | `O(mn)` | 結果串列需要保存所有矩陣元素。 |

> [!IMPORTANT]
> `SpiralOrder` 會把走過的元素改成 `int.MaxValue`。測試 runner 的矩陣複製需要 `O(mn)` 空間，但那是展示與驗證層的成本，不是 `SpiralOrder` 本身的額外空間。

## 專案結構

```text
leetcode_054/
├── docs/
│   └── readme-template.md
├── leetcode_054/
│   ├── leetcode_054.csproj
│   └── Program.cs
├── leetcode_054.sln
└── README.md
```

## 建置與執行

需求：

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

請在此 README 所在的 repository root 執行：

```powershell
dotnet build leetcode_054/leetcode_054.csproj --nologo
dotnet run --project leetcode_054/leetcode_054.csproj
```

目前專案沒有獨立的自動化測試專案；`Main` 的固定案例與 PASS/FAIL 比對就是可重複執行的驗收檢查。

### 實際執行輸出

```text
Case: Official 3x3
Expected: [1, 2, 3, 6, 9, 8, 7, 4, 5]
Actual:   [1, 2, 3, 6, 9, 8, 7, 4, 5]
Result:   PASS

Case: Official 3x4
Expected: [1, 2, 3, 4, 8, 12, 11, 10, 9, 5, 6, 7]
Actual:   [1, 2, 3, 4, 8, 12, 11, 10, 9, 5, 6, 7]
Result:   PASS

Case: Single row
Expected: [1, 2, 3, 4]
Actual:   [1, 2, 3, 4]
Result:   PASS

Case: Single column
Expected: [1, 2, 3]
Actual:   [1, 2, 3]
Result:   PASS

Case: Empty matrix
Expected: []
Actual:   []
Result:   PASS

Overall: 5/5 passed.
```