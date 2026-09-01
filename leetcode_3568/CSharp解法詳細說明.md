# LeetCode 3568：C# 解法詳細說明

題目：[3568. Minimum Moves to Clean the Classroom](https://leetcode.com/problems/minimum-moves-to-clean-the-classroom/)

這份文件說明使用 **BFS（廣度優先搜尋）＋Bitmask（位元遮罩）＋支配剪枝** 解決「清理教室的最少移動」的方法。

## 完整解法

```csharp
public class Solution
{
    static readonly int[] dx = new int[] { 0, 1, 0, -1 };
    static readonly int[] dy = new int[] { 1, 0, -1, 0 };

    public int MinMoves(string[] classroom, int energy)
    {
        int rowCount = classroom.Length;
        int columnCount = classroom[0].Length;
        int[,] litterBitByCell = new int[rowCount, columnCount];
        int startRow = 0, startColumn = 0, litterCount = 0;

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                char cellType = classroom[i][j];
                if (cellType == 'S')
                {
                    startRow = i;
                    startColumn = j;
                }
                else if (cellType == 'L')
                {
                    litterBitByCell[i, j] = 1 << litterCount;
                    litterCount++;
                }
            }
        }

        int maskStateCount = 1 << litterCount;
        int allLitterMask = maskStateCount - 1;
        int[,,] bestEnergy = new int[rowCount, columnCount, maskStateCount];

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                for (int k = 0; k < maskStateCount; k++)
                {
                    bestEnergy[i, j, k] = -1;
                }
            }
        }

        bestEnergy[startRow, startColumn, 0] = energy;

        var queue = new Queue<(int row, int column, int litterMask, int remainingEnergy, int steps)>();
        queue.Enqueue((startRow, startColumn, 0, energy, 0));

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            if (state.litterMask == allLitterMask)
            {
                return state.steps;
            }

            if (state.remainingEnergy == 0)
            {
                continue;
            }

            for (int directionIndex = 0; directionIndex < 4; directionIndex++)
            {
                int nextRow = state.row + dx[directionIndex];
                int nextColumn = state.column + dy[directionIndex];

                if (nextRow < 0 || nextRow >= rowCount ||
                    nextColumn < 0 || nextColumn >= columnCount ||
                    classroom[nextRow][nextColumn] == 'X')
                {
                    continue;
                }

                int nextEnergy = classroom[nextRow][nextColumn] == 'R' ? energy : state.remainingEnergy - 1;
                int nextLitterMask = state.litterMask | litterBitByCell[nextRow, nextColumn];

                if (nextEnergy > bestEnergy[nextRow, nextColumn, nextLitterMask])
                {
                    bestEnergy[nextRow, nextColumn, nextLitterMask] = nextEnergy;
                    queue.Enqueue((nextRow, nextColumn, nextLitterMask, nextEnergy, state.steps + 1));
                }
            }
        }

        return -1;
    }
}
```

## 核心概念

這份解法的核心是 **BFS（廣度優先搜尋）＋位元遮罩 Bitmask＋狀態剪枝**。

最重要的觀念是：不能只記錄「目前人在 `(x, y)`」，因為即使站在同一格，如果「已清理的垃圾不同」或「剩餘能量不同」，後續結果也可能完全不同。

因此真正的 BFS 狀態可以表示為：

```text
位置 + 已清理垃圾 + 剩餘能量
(row, column, litterMask, remainingEnergy)
```

BFS 會按照移動步數由少到多搜尋，所以第一次找到「所有垃圾都清完」的狀態時，就是最少移動次數。

## 1. 整體流程

這份程式大致執行以下步驟：

1. 掃描教室，找出起點 `S`，並替每個垃圾 `L` 分配一個 bit。
2. 使用 `litterMask` 記錄目前已清理哪些垃圾。
3. 使用 BFS 搜尋所有可能的移動狀態。
4. 每移動一步，能量減少一點。
5. 走到 `R` 時，能量恢復成最大值 `energy`。
6. `X` 是障礙物，不能進入。
7. 使用 `bestEnergy[x, y, litterMask]` 剪掉沒有必要再次搜尋的狀態。
8. 第一次取出 `litterMask == allLitterMask` 的狀態時立即回傳步數。

例如有三個垃圾：

```text
L0 L1 L2
```

可以使用三個 bit 表示所有清理狀態：

```text
000  都沒清理
001  清理 L0
010  清理 L1
011  清理 L0、L1
100  清理 L2
101  清理 L0、L2
110  清理 L1、L2
111  全部清理完成
```

## 2. 四個移動方向

```csharp
static readonly int[] dx = new int[] { 0, 1, 0, -1 };
static readonly int[] dy = new int[] { 1, 0, -1, 0 };
```

這兩個陣列表示四個方向：

```text
directionIndex = 0：右
directionIndex = 1：下
directionIndex = 2：左
directionIndex = 3：上
```

例如：

```text
dx[0] = 0
dy[0] = 1
```

代表：

```text
(x, y) → (x, y + 1)
```

也就是往右移動。因此後面只需要使用一個迴圈，就能依序檢查四個方向：

```csharp
for (int directionIndex = 0; directionIndex < 4; directionIndex++)
{
    int nextRow = state.row + dx[directionIndex];
    int nextColumn = state.column + dy[directionIndex];
}
```

## 3. 取得教室大小

```csharp
int rowCount = classroom.Length;
int columnCount = classroom[0].Length;
```

假設教室如下：

```text
classroom =
[
    "S.L",
    ".X.",
    "R.L"
]
```

則：

```text
rowCount = 3   // 列數 row
columnCount = 3   // 欄數 column
```

程式使用 `(x, y)` 表示座標，其中：

```text
x = row
y = column
```

## 4. `litterBitByCell` 陣列

```csharp
int[,] litterBitByCell = new int[rowCount, columnCount];
```

`litterBitByCell[i, j]` 用來記錄：如果 `(i, j)` 是垃圾 `L`，它對應到 `litterMask` 中的哪一個 bit。

例如教室如下：

```text
S . L
. X .
L . L
```

依照掃描順序，三個垃圾可能得到以下 bit：

```text
第一個 L → 001
第二個 L → 010
第三個 L → 100
```

也就是：

```text
litterBitByCell[0, 2] = 1
litterBitByCell[2, 0] = 2
litterBitByCell[2, 2] = 4
```

非垃圾位置的 `litterBitByCell` 預設為 `0`。這讓程式可以直接寫成：

```csharp
int nextLitterMask = state.litterMask | litterBitByCell[nextRow, nextColumn];
```

不需要另外判斷下一格是否為 `L`。

## 5. 找起點與垃圾

```csharp
int startRow = 0, startColumn = 0, litterCount = 0;
```

這三個變數分別代表：

```text
startRow      起點的列座標
startColumn  起點的欄座標
litterCount  已找到的垃圾數量
```

接著掃描整張地圖：

```csharp
for (int i = 0; i < rowCount; i++)
{
    for (int j = 0; j < columnCount; j++)
    {
        char cellType = classroom[i][j];

        if (cellType == 'S')
        {
            startRow = i;
            startColumn = j;
        }
        else if (cellType == 'L')
        {
            litterBitByCell[i, j] = 1 << litterCount;
            litterCount++;
        }
    }
}
```

遇到 `S` 時，記錄起點座標；遇到 `L` 時，替該垃圾分配一個獨立 bit。

## 6. `1 << litterCount` 的意義

`1 << litterCount` 是 Bitmask 的核心操作。

例如：

```csharp
1 << 0
```

二進位結果是：

```text
0001
```

十進位就是 `1`。

接著：

```csharp
1 << 1
```

得到：

```text
0010 = 2
```

再來：

```csharp
1 << 2
```

得到：

```text
0100 = 4
```

所以如果有三個垃圾：

```text
垃圾 0 → 001
垃圾 1 → 010
垃圾 2 → 100
```

每個垃圾都擁有一個獨立 bit，之後就能用一個整數表示目前已清理的垃圾集合。

## 7. `maskStateCount` 與 `allLitterMask`

```csharp
int maskStateCount = 1 << litterCount;
int allLitterMask = maskStateCount - 1;
```

假設共有三個垃圾：

```text
litterCount = 3
maskStateCount = 1 << litterCount = 8
```

三個 bit 一共有 `2³ = 8` 種組合：

```text
000
001
010
011
100
101
110
111
```

因此 `litterMask` 的範圍是：

```text
0 ~ allLitterMask
0 ~ 7
```

最後一個狀態 `111` 的數值是 `7`，也就是：

```csharp
allLitterMask
```

所以：

```csharp
if (state.litterMask == allLitterMask)
```

代表所有垃圾都已經清理完成。

## 8. `bestEnergy`：狀態剪枝的關鍵

```csharp
int[,,] bestEnergy = new int[rowCount, columnCount, maskStateCount];
```

`bestEnergy[x, y, litterMask]` 代表：

> 到達 `(x, y)`，而且已經清理 `litterMask` 所表示的垃圾時，曾經擁有的最大剩餘能量。

這不是普通的 `visited` 陣列，而是用來保存同一個位置與同一個垃圾集合下的最佳能量。

### 為什麼不能只使用 `visited[x, y]`？

假設兩個狀態來到同一個位置 `(3, 4)`：

```text
第一次：已清理垃圾 = 001，剩餘能量 = 1
第二次：已清理垃圾 = 101，剩餘能量 = 4
```

雖然位置相同，但已清理的垃圾集合與剩餘能量都可能不同，後續可以走的路線也不同。因此不能只用：

```csharp
bool[,] visited;
```

### 為什麼 `visited[x, y, litterMask]` 仍然不夠？

即使位置與 `litterMask` 都相同，剩餘能量不同也代表不同的能力：

```text
(x, y, litterMask = 001, remainingEnergy = 2)
(x, y, litterMask = 001, remainingEnergy = 5)
```

能量為 `5` 的狀態可以執行能量為 `2` 的狀態能做的所有事情，還可能走得更遠。`bestEnergy` 正是用來利用這項特性。

## 9. 為什麼只保存最大能量？

假設目前的位置與垃圾狀態相同：

```text
位置 = (2, 3)
litterMask = 011
```

之前曾經以剩餘能量 `5` 到達：

```text
bestEnergy[2, 3, 011] = 5
```

現在另一條路線以剩餘能量 `3` 到達：

```text
(x = 2, y = 3, litterMask = 011, remainingEnergy = 3)
```

新的狀態沒有任何優勢，因為能量為 `5` 的舊狀態可以執行能量為 `3` 的新狀態能執行的所有後續行動。

因此新狀態可以被剪掉。這就是以下判斷的意義：

```csharp
if (nextEnergy > bestEnergy[nextRow, nextColumn, nextLitterMask])
```

## 10. 為什麼初始化為 `-1`？

```csharp
for (int i = 0; i < rowCount; i++)
{
    for (int j = 0; j < columnCount; j++)
    {
        for (int k = 0; k < maskStateCount; k++)
        {
            bestEnergy[i, j, k] = -1;
        }
    }
}
```

剩餘能量的合法最低值是 `0`，所以不能使用 `0` 表示「這個狀態尚未到達」。

使用 `-1` 代表：

```text
這個位置與 litterMask 組合尚未被拜訪
```

這樣即使新狀態的能量是 `0`，仍然可以透過 `0 > -1` 正確加入搜尋佇列。

## 11. 初始化起點

```csharp
bestEnergy[startRow, startColumn, 0] = energy;
```

一開始的狀態是：

```text
位置             = S
垃圾             = 0 個
litterMask       = 0
能量             = 最大值 energy
步數             = 0
```

因此起點 `(startRow, startColumn)` 與空集合 `litterMask = 0` 的最佳能量就是 `energy`。

## 12. BFS Queue 的內容

```csharp
var queue = new Queue<(int row, int column, int litterMask, int remainingEnergy, int steps)>();
```

Queue 中的每一筆狀態包含：

```text
row              目前所在的列
column           目前所在的欄
litterMask       已清理哪些垃圾
remainingEnergy  剩餘能量
steps            已經移動幾步
```

起始狀態加入方式如下：

```csharp
queue.Enqueue((startRow, startColumn, 0, energy, 0));
```

代表：

```text
位置   = S
litterMask = 000...
energy = 滿能量
steps  = 0
```

## 13. 開始 BFS

```csharp
while (queue.Count > 0)
{
    var state = queue.Dequeue();
```

每次取出 Queue 最前面的狀態。BFS 的重要特性是依照距離逐層搜尋：

```text
steps = 0
steps = 1
steps = 2
steps = 3
...
```

因此當第一次取出完成所有垃圾的狀態時，該狀態一定是最少步數。

## 14. 判斷是否完成所有垃圾

```csharp
if (state.litterMask == allLitterMask)
{
    return state.steps;
}
```

例如有三個垃圾：

```text
maskStateCount = 1000₂ = 8
allLitterMask = 0111₂ = 7
```

當：

```text
litterMask = 111
```

就代表：

```text
L0 ✓
L1 ✓
L2 ✓
```

BFS 會按照步數從小到大取出狀態，因此可以立即回傳 `state.steps`，不必繼續搜尋。

## 15. 沒有能量時停止展開

```csharp
if (state.remainingEnergy == 0)
{
    continue;
}
```

如果目前剩餘能量為 `0`，便沒有能量移動到下一格，因此停止展開這個狀態。

這裡必須使用 `continue`，而不是 `return`：

```csharp
continue; // 只放棄目前狀態
return;   // 會錯誤地結束整個 BFS
```

Queue 中可能還有其他路線仍然能夠完成清理，所以不能因為單一路線無法前進就直接回傳。

此外，完成判斷放在能量判斷之前很重要。學生可以使用最後一點能量走到最後一個垃圾；即使抵達後能量為 `0`，任務仍然已完成。

## 16. 嘗試上下左右

```csharp
for (int directionIndex = 0; directionIndex < 4; directionIndex++)
{
    int nextRow = state.row + dx[directionIndex];
    int nextColumn = state.column + dy[directionIndex];
```

程式依序計算四個方向的下一個位置 `(nextRow, nextColumn)`。

例如目前位於 `(2, 3)`，可能產生：

```text
往右 → (2, 4)
往下 → (3, 3)
往左 → (2, 2)
往上 → (1, 3)
```

## 17. 排除不能走的位置

```csharp
if (nextRow < 0 || nextRow >= rowCount ||
    nextColumn < 0 || nextColumn >= columnCount ||
    classroom[nextRow][nextColumn] == 'X')
{
    continue;
}
```

這段程式排除兩類位置。

第一類是超出地圖邊界：

```text
nextRow < 0
nextRow >= rowCount
nextColumn < 0
nextColumn >= columnCount
```

第二類是障礙物：

```text
classroom[nextRow][nextColumn] == 'X'
```

這些位置都不能加入下一輪 BFS。

## 18. 計算新的能量

```csharp
int nextEnergy = classroom[nextRow][nextColumn] == 'R'
    ? energy
    : state.remainingEnergy - 1;
```

這裡分成兩種情況。

### 走到普通格

```text
新能量 = 目前能量 - 1
```

例如目前有 `5` 點能量，移動一步後剩下 `4` 點。

### 走到重置區域 `R`

```text
新能量 = energy
```

例如最大能量是 `5`，目前只剩 `1` 點，走進 `R` 後就會恢復成 `5` 點。

注意：走到 `R` 仍然算一次移動，只有抵達後能量才會恢復為最大值。

## 19. 使用 OR 清理垃圾

```csharp
int nextLitterMask = state.litterMask | litterBitByCell[nextRow, nextColumn];
```

這是 Bitmask 的另一個核心操作。

假設目前已清理的垃圾是：

```text
litterMask = 001
```

現在走到第二個垃圾，而該垃圾的 bit 是：

```text
litterBitByCell[nextRow, nextColumn] = 010
```

使用 OR 運算：

```text
  001
OR 010
-----
  011
```

因此：

```text
nextLitterMask = 011
```

代表第一個與第二個垃圾都已清理，第三個垃圾尚未清理。

## 20. 走到普通格時的 `litterMask`

非垃圾格的 `litterBitByCell` 預設為 `0`：

```text
  011
OR 000
-----
  011
```

因此：

```csharp
nextLitterMask = state.litterMask | 0;
```

不會改變原本的垃圾集合。這讓程式不需要另外寫：

```csharp
if (classroom[nextRow][nextColumn] == 'L')
{
    // 另外處理垃圾
}
```

## 21. 再次走到已清理的垃圾

假設垃圾 `L1` 對應到 bit `010`，而目前狀態是：

```text
litterMask = 011
```

代表 `L1` 已經清理過。再次走進去時：

```text
  011
OR 010
-----
  011
```

結果仍然是 `011`，所以同一個垃圾不會被重複計算。

Bitmask 透過 OR 運算自然處理了「垃圾只能算一次」的規則。

## 22. 最重要的剪枝

```csharp
if (nextEnergy > bestEnergy[nextRow, nextColumn, nextLitterMask])
{
    bestEnergy[nextRow, nextColumn, nextLitterMask] = nextEnergy;
    queue.Enqueue((nextRow, nextColumn, nextLitterMask, nextEnergy, state.steps + 1));
}
```

假設之前已經有：

```text
bestEnergy[2, 3, 011] = 4
```

這表示曾經以 `4` 點能量到達 `(2, 3)`，且當時的垃圾集合是 `011`。

現在新路線以 `2` 點能量到達相同狀態：

```text
nextEnergy = 2
```

由於：

```text
2 <= 4
```

新狀態被舊狀態完全支配，不需要加入 Queue。

### 新能量比較高時

若原本是：

```text
bestEnergy[2, 3, 011] = 2
```

而新狀態是：

```text
nextEnergy = 5
```

因為：

```text
5 > 2
```

新狀態具有更高能量，值得繼續搜尋，因此更新陣列並加入 Queue：

```csharp
bestEnergy[2, 3, 011] = 5;
queue.Enqueue(...);
```

## 23. 為什麼 Queue 還需要 `steps`？

題目要求回傳最少移動次數，所以每個 BFS 狀態都需要保存目前步數：

```csharp
queue.Enqueue((nextRow, nextColumn, nextLitterMask, nextEnergy, state.steps + 1));
```

每移動一格，步數加一。例如：

```text
S
↓ 1
.
↓ 2
R
↓ 3
L
```

抵達垃圾時：

```text
steps = 3
```

若此時全部垃圾已清理，便回傳 `3`。

## 24. 找不到答案

當 Queue 清空後仍未找到完成狀態，程式會執行：

```csharp
return -1;
```

這表示所有可行狀態都已經檢查過，但仍然無法清理全部垃圾。

可能原因包括：

```text
垃圾被障礙物完全隔開
能量不足以抵達必要位置
沒有可行路徑連接所有垃圾
```

## 25. 小例子：觀察完整 BFS

假設教室如下：

```text
S . L
. R .
. . L
```

最大能量：

```text
energy = 3
```

兩個垃圾對應到：

```text
L0 = 01
L1 = 10
```

因此：

```text
maskStateCount = 100₂ = 4
allLitterMask = 11₂
```

一開始的狀態：

```text
位置   = S
litterMask = 00
energy = 3
steps  = 0
```

走一步到第一個普通格：

```text
S → .
```

狀態變成：

```text
litterMask = 00
energy = 2
steps  = 1
```

接著走到第一個垃圾 `L0`：

```text
. → L0
```

更新為：

```text
litterMask = 00 OR 01 = 01
energy = 1
steps = 2
```

之後經過重置區域 `R`，能量重新恢復：

```text
energy = 3
```

最後走到第二個垃圾 `L1`：

```text
litterMask = 01 OR 10 = 11
```

此時：

```csharp
litterMask == allLitterMask
```

所以 BFS 回傳目前的 `steps`。

## 26. 為什麼使用 BFS，而不是 DFS？

題目要求的是：

> Minimum Moves，也就是最少移動次數。

每一次上下左右移動的成本都是 `1`，因此這是一張每條邊權重相同的圖。

BFS 會依照距離分層：

```text
第 0 層：0 步
第 1 層：1 步
第 2 層：2 步
第 3 層：3 步
...
```

因此第一次抵達：

```text
litterMask == allLitterMask
```

一定是最短路徑。

如果使用 DFS，可能先找到一條 `20` 步的路徑，但另一條 `12` 步的路徑可能尚未被搜尋到；除非額外完整搜尋並比較所有答案，否則不能直接保證最優解。

## 27. 這題真正困難的是狀態設計

若把普通迷宮 BFS 寫成只記錄位置：

```text
(x, y)
```

這題會出錯，因為同一個位置可能對應多種不同情況：

```text
(x, y, litterMask = 001, remainingEnergy = 2)
(x, y, litterMask = 011, remainingEnergy = 2)
(x, y, litterMask = 001, remainingEnergy = 5)
```

這些狀態的後續選擇不相同。完整狀態可以先理解成：

```text
(x, y, litterMask, remainingEnergy)
```

不過這份解法沒有直接建立四維 `visited`：

```csharp
bool[,,,] visited;
```

而是使用：

```csharp
bestEnergy[x, y, litterMask]
```

保存同一位置、同一垃圾集合下曾經出現過的最大能量，淘汰能量較低的劣勢狀態。

## 28. `bestEnergy` 與支配關係

假設有兩個狀態：

```text
A：位置 (3, 2)，litterMask = 101，remainingEnergy = 5
B：位置 (3, 2)，litterMask = 101，remainingEnergy = 2
```

A 與 B 的位置及已清理垃圾完全相同，唯一差別是 A 的剩餘能量較高。

因此 A 完全支配 B：

```text
從 B 能走的任何路徑，A 一定也能走
A 還可能因為能量較多而走得更遠
```

所以 B 沒有繼續搜尋的必要，這就是以下判斷背後的演算法概念：

```csharp
if (nextEnergy > bestEnergy[nextRow, nextColumn, nextLitterMask])
```

它把原本可以想成四維的狀態：

```text
(x, y, litterMask, remainingEnergy)
```

壓縮為：

```text
(x, y, litterMask) → 記錄目前最大 remainingEnergy
```

## 29. 時間與空間複雜度

令：

```text
rowCount = 列數
columnCount = 欄數
L = 垃圾數量
E = 最大能量
```

垃圾集合 `litterMask` 有：

```text
2^L
```

種可能，因此 `bestEnergy` 的大小是：

```text
rowCount × columnCount × 2^L
```

空間複雜度主要是：

```text
O(rowCount × columnCount × 2^L)
```

對於同一個 `(x, y, litterMask)`，`bestEnergy` 可能因為找到更高能量的路徑而更新；從保守角度估計，更新次數與能量範圍 `E` 有關，因此時間複雜度可寫成：

```text
O(rowCount × columnCount × 2^L × E)
```

每個狀態最多檢查四個方向，而 `4` 是常數，所以在大 O 表示法中省略。

## 30. 最值得記住的三個技巧

### BFS

因為每一步的成本都是 `1`：

```text
BFS → 求最少步數
```

### Bitmask

使用：

```csharp
1 << litterCount
```

替垃圾分配 bit，再使用：

```csharp
litterMask | litterBitByCell[x, y]
```

記錄哪些垃圾已經清理完成。例如：

```text
10101
```

就能用一個整數表示多個垃圾的完成狀態。

### Dominance Pruning（支配剪枝）

使用：

```csharp
bestEnergy[x, y, litterMask]
```

如果新狀態與舊狀態的：

```text
位置相同
litterMask 相同
```

但新狀態的 `energy` 更少，就沒有繼續搜尋的必要。

## 演算法流程圖

```text
┌────────────────────────────────────────────────────┐
│ S 起點                                             │
│ litterMask = 0                                    │
│ energy = E                                        │
└──────────────────────┬─────────────────────────────┘
                       │
                       ▼
                 BFS Queue
                       │
        ┌──────────────┴──────────────┐
        │ 嘗試上、下、左、右           │
        └──────────────┬──────────────┘
                       │
          ┌────────────┴────────────┐
          │                         │
      超出範圍 / X                可以走
          │                         ▼
        丟棄                 計算 nextEnergy
                                  │
                    ┌─────────────┴─────────────┐
                    │                           │
                走到 R                      走到普通格
                    │                           │
                    ▼                           ▼
              nextEnergy = E      nextEnergy = remainingEnergy - 1
                    │                           │
                    └─────────────┬─────────────┘
                                  ▼
                  更新 litterMask
                                  │
                  litterMask | litterBitByCell[nextRow, nextColumn]
                                  │
                                  ▼
             nextEnergy > bestEnergy？
                     │                  │
                    否                  是
                     │                  │
                   丟棄             加入 Queue
                                        │
                                        ▼
                      litterMask == allLitterMask？
                                        │
                                       是
                                        │
                                        ▼
                                   回傳 steps
```

## 一句話總結

> 使用 BFS 尋找最少步數，用 Bitmask 記錄已清理的垃圾，並用 `bestEnergy[x, y, litterMask]` 保留同一狀態下最高的剩餘能量，剪掉明顯較差的路線。

其中最需要理解的程式碼是：

```csharp
if (nextEnergy > bestEnergy[nextRow, nextColumn, nextLitterMask])
```

它將原本可以想成四維的狀態：

```text
(x, y, litterMask, remainingEnergy)
```

壓縮為：

```text
(x, y, litterMask) → 目前見過的最大 remainingEnergy
```

這正是這份解法能有效搜尋狀態、避免重複探索的關鍵。

