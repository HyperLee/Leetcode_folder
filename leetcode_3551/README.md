# LeetCode 3551：數位和排序需要的最少交換次數

本專案使用 .NET 10 與 C#，實作「並查集」及「置換環」兩種解法。直接執行即可驗證 12 組固定資料、共 24 次解法呼叫，包含十萬筆資料與輸入不變性檢查。

[題目說明](#題目說明) · [快速執行](#快速執行) · [解題出發點](#解題出發點) · [測試資料與實際輸出](#測試資料與實際輸出)

## 題目說明

題目：[3551. Minimum Swaps to Sort by Digit Sum](https://leetcode.com/problems/minimum-swaps-to-sort-by-digit-sum/description/)／[中文題目](https://leetcode.cn/problems/minimum-swaps-to-sort-by-digit-sum/description/)。

給定陣列 `nums`，其中所有元素都是互不相同的正整數。請依照以下順序排列元素：

1. 數位和較小的元素排在前面。
2. 數位和相同時，數值較小的元素排在前面。

一次操作可以交換任意兩個不同位置上的元素，請回傳完成排序所需的最少交換次數。回傳型別為 `int`。

例如，`digitSum(43) = 4 + 3 = 7`，`digitSum(100) = 1 + 0 + 0 = 1`。排序鍵可以寫成 `(數位和, 數值)`，先比較第一項，相同時再比較第二項。

### 限制條件

依官方題目，合法輸入須滿足：

- `1 <= nums.Length <= 100000`。
- `1 <= nums[i] <= 1000000000`。
- 所有元素皆為互異正整數。

兩個解法以這些條件為前提，不另外驗證 `null`、空陣列、重複值或非正整數。兩個方法均不修改傳入的 `nums`。

### 官方範例

| 輸入 | 數位和 | 目標順序 | 最少交換次數 |
|---|---|---|---:|
| `[37, 100]` | `[10, 1]` | `[100, 37]` | 1 |
| `[22, 14, 33, 7]` | `[4, 5, 6, 7]` | `[22, 14, 33, 7]` | 0 |
| `[18, 43, 34, 16]` | `[9, 7, 7, 7]` | `[16, 34, 43, 18]` | 2 |

第三個範例中，`16`、`34`、`43` 的數位和都是 `7`，所以它們必須再依數值排列成 `16, 34, 43`。

## 快速執行

需要可建置及執行 `net10.0` 的 .NET 10 SDK。以下命令均從包含本 README、`.vscode` 與內層 `leetcode_3551/` 的專案工作目錄執行。

```sh
dotnet restore leetcode_3551/leetcode_3551.csproj
dotnet build leetcode_3551/leetcode_3551.csproj
dotnet run --no-build --project leetcode_3551/leetcode_3551.csproj
```

程式不需要輸入資料或命令列參數。每個案例都會執行 `MinSwaps` 與 `MinSwaps2`，最後輸出：

```text
Summary: 24/24 checks passed.
```

全部檢查通過時，程序結束碼為 `0`；有答案錯誤或輸入遭修改時，結束碼為 `1`。每次修改程式後，請先完成建置，再使用 `--no-build` 執行，以確保執行的是最新程式。

現有 VS Code 設定也提供預設建置工作與 `Debug leetcode_3551` 偵錯設定，啟動時不帶參數。

### 專案結構與入口

```text
.
├── README.md
├── AGENTS.md
├── .editorconfig
├── .vscode/
│   ├── tasks.json
│   └── launch.json
├── docs/
│   └── readme-template.md
└── leetcode_3551/
    ├── leetcode_3551.csproj
    └── Program.cs
```

| 方法 | 責任 |
|---|---|
| `Main(string[] args)` | 啟動固定測試並設定程序結束碼；保留原題的雙語 XML 說明 |
| `RunSamples()` | 建立案例、分別複製輸入、比對預期結果與輸入內容、列印總結 |
| `MinSwaps(int[] nums)` | 透過並查集計算置換的連通分量數，回傳最少交換次數 |
| `Find(int[] parent, int x)` | 迭代尋找根節點，並就地壓縮父節點路徑 |
| `MinSwaps2(int[] nums)` | 透過 `visited` 逐環走訪，回傳最少交換次數 |

## 解題出發點

### 先確定每個元素最後要去哪裡

題目的操作次數取決於元素目前位置與目標位置之間的關係。由於所有數值互異，加上數位和相同時要比較數值，每個元素的目標位置都能唯一確定。

因此，可以先取得目標排列，再建立位置之間的對應：

```text
原位置 i → 原本放在 i 的元素，排序後應前往的位置 targetPos[i]
```

位置也是從 `0` 到 `n - 1`，每個目標位置恰好對應一個元素，所以 `targetPos` 是一個置換。置換可以拆成互不重疊的環；兩種解法的差異，就是如何計算這些環。

### 為什麼長度 k 的環需要 k - 1 次交換

以 `nums = [2, 3, 1]` 為例，目標是 `[1, 2, 3]`，對應的 `targetPos = [1, 2, 0]`：

```text
原位置 0 的值 2 應前往 1
原位置 1 的值 3 應前往 2
原位置 2 的值 1 應前往 0

位置環：0 → 1 → 2 → 0
```

這個長度為 `3` 的環可以用兩次交換解開：

| 步驟 | 操作 | 陣列 |
|---|---|---|
| 初始 | 尚未交換 | `[2, 3, 1]` |
| 1 | 交換位置 `0`、`2`，使 `1` 就位 | `[1, 3, 2]` |
| 2 | 交換位置 `1`、`2`，使其餘兩個元素就位 | `[1, 2, 3]` |

一般而言，長度為 `k` 的環可以依序讓 `k - 1` 個元素就位，最後一個元素也會自然就位，因此 `k - 1` 次一定足夠。

最少次數的下界可以由環數判斷：一次交換最多讓置換的環數增加 `1`，而完全排序時每個位置都成為獨立的長度 `1` 環。若原本共有 `c` 個環，要增加到 `n` 個環，至少需要 `n - c` 次交換。

各環分別使用 `k - 1` 次的做法，總次數剛好是：

```text
(k1 - 1) + (k2 - 1) + ... + (kc - 1)
= (k1 + k2 + ... + kc) - c
= n - c
```

上下界相同，所以最少交換次數就是 `n - 環數`。

固定點 `targetPos[i] == i` 是長度 `1` 的環，貢獻 `1 - 1 = 0` 次交換；計算環數時也必須把它算進去。

## 共用前置流程：把原始陣列轉成置換

以下皆以 `nums = [18, 43, 34, 16]` 為例。

### 1. 計算各元素的數位和

程式使用 `x % 10` 取得個位數，再用整數除法 `x /= 10` 移除已處理的位數。`x` 是局部副本，因此不會改動 `nums[i]`。

例如 `43` 的處理過程如下：

| 處理前 x | 加入的數字 `x % 10` | 累積 sum | 移除個位數後 x |
|---:|---:|---:|---:|
| 43 | 3 | 3 | 4 |
| 4 | 4 | 7 | 0 |

得到：

| 原位置 i | `nums[i]` | `digitSum[i]` |
|---:|---:|---:|
| 0 | 18 | 9 |
| 1 | 43 | 7 |
| 2 | 34 | 7 |
| 3 | 16 | 7 |

### 2. 依排序鍵排列原始下標

先建立 `indices = [0, 1, 2, 3]`，接著對下標排序。比較下標 `a`、`b` 時，先比較 `digitSum[a]`、`digitSum[b]`，相同時再比較 `nums[a]`、`nums[b]`。

```csharp
Array.Sort(indices, (a, b) =>
{
    if (digitSum[a] != digitSum[b])
    {
        return digitSum[a].CompareTo(digitSum[b]);
    }
    return nums[a].CompareTo(nums[b]);
});
```

排序後得到 `indices = [3, 2, 1, 0]`：

| 目標位置 pos | `indices[pos]`：元素的原位置 | 該元素 | 排序鍵 |
|---:|---:|---:|---|
| 0 | 3 | 16 | `(7, 16)` |
| 1 | 2 | 34 | `(7, 34)` |
| 2 | 1 | 43 | `(7, 43)` |
| 3 | 0 | 18 | `(9, 18)` |

這樣既能得知目標順序，也能保留每個元素原本所在的位置。`Array.Sort` 只修改 `indices`。

### 3. 反向建立 targetPos

`indices[pos]` 表示「目標位置 `pos` 放的是哪個原位置的元素」。後續要查的是「原位置 `i` 的元素應前往哪裡」，因此要建立相反方向的對應：

```csharp
targetPos[indices[pos]] = pos;
```

逐次填入如下：

| pos | `indices[pos]` | 寫入內容 |
|---:|---:|---|
| 0 | 3 | `targetPos[3] = 0` |
| 1 | 2 | `targetPos[2] = 1` |
| 2 | 1 | `targetPos[1] = 2` |
| 3 | 0 | `targetPos[0] = 3` |

最後得到：

```text
nums      = [18, 43, 34, 16]
digitSum  = [ 9,  7,  7,  7]
indices   = [ 3,  2,  1,  0]
targetPos = [ 3,  2,  1,  0]

環一：0 → 3 → 0
環二：1 → 2 → 1
```

本例的 `indices` 和 `targetPos` 恰好相同，但它們的意義不同。用 `[2, 3, 1]` 可以看出差異：`indices = [2, 0, 1]`，而 `targetPos = [1, 2, 0]`。本專案兩種解法都明確建立 `targetPos` 後再計算環數。

## 解法一：並查集 MinSwaps

### 設計概念

把每個位置視為節點，對每個 `i` 連接 `i` 與 `targetPos[i]`。因為這些邊來自置換，圖中每個連通分量都對應一個完整的置換環。

並查集用 `parent` 表示集合關係：

- 初始化時 `parent[i] = i`，每個位置各自成為一個集合，`components = n`。
- `Find(parent, i)` 取得位置 `i` 所屬集合的根節點。
- 對每條邊，取得 `ri = Find(parent, i)` 與 `rj = Find(parent, targetPos[i])`。
- 若根不同，令 `parent[ri] = rj`，把兩個集合合併，並讓 `components--`。
- 若根相同，兩個位置已經連通，不再減少集合數。

長度為 `k` 的環，前 `k - 1` 次有效連接會將節點合併成同一集合，最後閉合環的邊不會再次合併。處理完全部位置後，`components` 就是置換環數，回傳 `n - components`。

### Find 的兩段迴圈

第一段以 `root` 從 `x` 沿父節點向上走，直到 `parent[root] == root`，找到集合代表。

第二段重新從 `x` 出發，把路徑上的節點直接接到 `root`。修改 `parent[x]` 前必須先保存原父節點 `next`，才能繼續沿原路徑向上處理：

```csharp
int root = x;
while (parent[root] != root)
{
    root = parent[root];
}

while (parent[x] != x)
{
    int next = parent[x];
    parent[x] = root;
    x = next;
}

return root;
```

例如 `parent = [1, 2, 3, 3]`，呼叫 `Find(parent, 0)` 時，第一段走過 `0 → 1 → 2 → 3`，回傳根節點 `3`；第二段將陣列壓縮為 `[3, 3, 3, 3]`。這只會改變集合的表示方式，不會改變節點所屬的集合。

左旋一格的排序資料會產生 `targetPos = [1, 2, ..., n - 1, 0]`。依目前合併順序，父節點可能先形成長鏈，因此本實作使用迴圈處理查找與壓縮，查找所需的呼叫堆疊不會隨鏈長增加。

### 範例演示：nums = [18, 43, 34, 16]

共用前置流程已得到 `targetPos = [3, 2, 1, 0]`。接著執行：

| 步驟 | i | `targetPos[i]` | 根節點 `(ri, rj)` | 動作 | 處理後 parent | components |
|---|---:|---:|---|---|---|---:|
| 初始化 | — | — | — | 每個位置獨立 | `[0, 1, 2, 3]` | 4 |
| 1 | 0 | 3 | `(0, 3)` | `parent[0] = 3` | `[3, 1, 2, 3]` | 3 |
| 2 | 1 | 2 | `(1, 2)` | `parent[1] = 2` | `[3, 2, 2, 3]` | 2 |
| 3 | 2 | 1 | `(2, 2)` | 已在同一集合 | `[3, 2, 2, 3]` | 2 |
| 4 | 3 | 0 | `(3, 3)` | 已在同一集合 | `[3, 2, 2, 3]` | 2 |

最後有 `{0, 3}` 與 `{1, 2}` 兩個集合，也就是兩個環，所以答案為 `4 - 2 = 2`。

實際交換可以是：

```text
[18, 43, 34, 16]
交換位置 0、3 → [16, 43, 34, 18]
交換位置 1、2 → [16, 34, 43, 18]
```

方法本身只計算最少次數，這段交換流程用來說明答案如何實現。

## 解法二：置換環 MinSwaps2

### 設計概念

既然 `targetPos` 已經是一個置換，就能直接沿著它的邊走訪各個環。

- 建立 `visited`，初始化全部為 `false`，並令 `cycles = 0`。
- 從左到右檢查每個位置 `i`。
- 若 `visited[i]` 為 `true`，代表它已在先前的環走訪中被計算過。
- 遇到未訪問的 `i`，就找到一個新環，先令 `cycles++`。
- 令 `j = i`，不斷將 `visited[j] = true`，再令 `j = targetPos[j]`。
- 碰到已訪問位置時停止，繼續檢查下一個 `i`。

因為置換中每個位置恰有一條入邊與一條出邊，所有位置都在環上。從未訪問的位置出發，不會接入另一個已完成的環；走訪最終會回到本次環內已標記的位置。

外層迴圈雖然搭配內層 `while`，但每個位置只會在內層被標記一次，所以整個走訪階段是 `O(n)`。

### 範例演示：nums = [18, 43, 34, 16]

同樣先得到 `targetPos = [3, 2, 1, 0]`，初始化 `visited = [false, false, false, false]`。

| 階段 | 外層 i | 本次 j | 動作與下一個位置 | 處理後 visited | cycles |
|---|---:|---:|---|---|---:|
| 初始化 | — | — | 所有位置尚未訪問 | `[false, false, false, false]` | 0 |
| 開始環一 | 0 | 0 | 新環；標記 0，下一個為 3 | `[true, false, false, false]` | 1 |
| 繼續環一 | 0 | 3 | 標記 3，下一個為 0 | `[true, false, false, true]` | 1 |
| 結束環一 | 0 | 0 | 0 已訪問，停止 | `[true, false, false, true]` | 1 |
| 開始環二 | 1 | 1 | 新環；標記 1，下一個為 2 | `[true, true, false, true]` | 2 |
| 繼續環二 | 1 | 2 | 標記 2，下一個為 1 | `[true, true, true, true]` | 2 |
| 結束環二 | 1 | 1 | 1 已訪問，停止 | `[true, true, true, true]` | 2 |
| 略過 | 2 | — | 2 已訪問 | `[true, true, true, true]` | 2 |
| 略過 | 3 | — | 3 已訪問 | `[true, true, true, true]` | 2 |

最後 `cycles = 2`，回傳 `n - cycles = 4 - 2 = 2`。它所辨識的環同樣是 `0 → 3 → 0` 與 `1 → 2 → 1`，可對應到前述兩次交換。

## 正確性與複雜度

### 正確性整理

1. **排序目標正確且唯一。** 比較器完整套用數位和與數值兩層規則；元素互異，因此每個元素的目標位置唯一。
2. **位置對應正確。** `targetPos[indices[pos]] = pos` 將每個元素的原位置映射到它的目標位置，各目標位置恰好出現一次。
3. **兩種方法都正確計算環數。** 並查集合併全部置換邊，得到每個環對應的連通分量；直接走訪則每遇到未訪問位置就完整標記一個環，沒有遺漏或重複。
4. **答案達到最少次數。** 一次交換最多增加一個環，各環也能用長度減一的交換次數解開，因此兩種方法回傳的 `n - 環數` 恰為最優解。

### 時間與空間

令 `n = nums.Length`，`D` 為輸入中最大十進位位數；本題 `D <= 10`。

| 階段 | 並查集 `MinSwaps` | 置換環 `MinSwaps2` |
|---|---|---|
| 計算數位和 | `O(nD)` | `O(nD)` |
| 排序下標 | `O(n log n)` | `O(n log n)` |
| 建立目標位置 | `O(n)` | `O(n)` |
| 統計環數 | 路徑壓縮、未採按秩合併，採 `O(n log n)` 的保守攤銷上界 | 每個位置訪問一次，`O(n)` |
| 總時間 | `O(n log n + nD)` | `O(n log n + nD)` |
| 額外空間 | `O(n)` | `O(n)` |

數位和已預先計算，排序比較器只做整數比較，不會在每次比較時重新掃描位數。`digitSum`、`indices`、`targetPos`，以及 `parent` 或 `visited` 都是長度為 `n` 的陣列。

`Find` 具有路徑壓縮，但沒有按秩或大小合併，因此這裡不將它宣稱為「按秩合併加路徑壓縮」的反阿克曼複雜度；單次查找仍可能走過長鏈。迭代查找只使用固定數量的局部變數。

上述分析針對兩個解法方法本身。console 測試入口另外持有固定案例、兩份大型資料與每次呼叫的輸入副本。

### 如何選擇

| 面向 | 並查集 | 置換環 |
|---|---|---|
| 核心狀態 | `parent`、`components` | `visited`、`cycles` |
| 思考方式 | 以連接位置後剩下的集合數表示環數 | 沿目標位置直接數環 |
| 適合練習 | 集合合併、根節點查找、路徑壓縮 | 置換結構、圖走訪、訪問標記 |
| 本題實作特性 | 保留通用的連通分量觀點 | 直接使用置換結構，統計階段較單純 |

## 測試資料與實際輸出

### 固定案例

| 案例 | 輸入或生成方式 | 預期 | 驗證重點 |
|---|---|---:|---|
| 官方範例一 | `[37, 100]` | 1 | 最基本的兩元素交換 |
| 官方範例二 | `[22, 14, 33, 7]` | 0 | 已排序；全部是固定點 |
| 官方範例三 | `[18, 43, 34, 16]` | 2 | 數位和相同時的第二排序鍵 |
| 單一元素 | `[1]` | 0 | 最小合法長度 |
| 相同數位和 | `[21, 12]` | 1 | 數值較小者先排 |
| 數值順序與數位和順序不同 | `[19, 20]` | 1 | 以數位和為第一排序鍵 |
| 三元素環 | `[2, 3, 1]` | 2 | 單一長度 3 的環 |
| 兩個獨立環 | `[2, 1, 4, 3]` | 2 | 環數累計 |
| 固定點與環共存 | `[2, 3, 1, 4]` | 2 | 固定點也要計入環數 |
| 最大數值 | `[999999999, 1000000000, 1]` | 1 | 十位數與多個 9 的數位和 |
| 十萬筆已排序 | 將 `1..100000` 依 `(數位和, 數值)` 排序 | 0 | 最大合法長度、十萬個固定點 |
| 十萬筆長環 | 將上一列的排序結果左旋一格 | 99999 | 最大合法長度、單一環及長父節點鏈 |

大型排序基準由測試入口使用十進位字元加總、`OrderBy` 與 `ThenBy` 建立，與兩個解法內的取餘數寫法分開。左旋後的 `targetPos` 必定是 `[1, 2, ..., 99999, 0]`，因此預期次數可以由環長直接得到 `100000 - 1 = 99999`。

每次解法呼叫前都複製原始輸入，並以固定預期值判斷結果。`InputUnchanged=True` 表示副本在呼叫後仍與原案例逐項相同；單次 PASS 必須同時滿足答案正確及輸入不變。

### 實際執行輸出

以下區塊由本次完成建置後的 `dotnet run --no-build --project leetcode_3551/leetcode_3551.csproj` 標準輸出取得，並以逐字比對確認一致。

```text
LeetCode 3551 - Minimum Swaps to Sort by Digit Sum
Official example 1: nums=[37, 100]
  MinSwaps: Expected=1, Actual=1, InputUnchanged=True => PASS
  MinSwaps2: Expected=1, Actual=1, InputUnchanged=True => PASS
Official example 2: nums=[22, 14, 33, 7]
  MinSwaps: Expected=0, Actual=0, InputUnchanged=True => PASS
  MinSwaps2: Expected=0, Actual=0, InputUnchanged=True => PASS
Official example 3: nums=[18, 43, 34, 16]
  MinSwaps: Expected=2, Actual=2, InputUnchanged=True => PASS
  MinSwaps2: Expected=2, Actual=2, InputUnchanged=True => PASS
Single element: nums=[1]
  MinSwaps: Expected=0, Actual=0, InputUnchanged=True => PASS
  MinSwaps2: Expected=0, Actual=0, InputUnchanged=True => PASS
Equal digit sums: nums=[21, 12]
  MinSwaps: Expected=1, Actual=1, InputUnchanged=True => PASS
  MinSwaps2: Expected=1, Actual=1, InputUnchanged=True => PASS
Digit order differs from numeric order: nums=[19, 20]
  MinSwaps: Expected=1, Actual=1, InputUnchanged=True => PASS
  MinSwaps2: Expected=1, Actual=1, InputUnchanged=True => PASS
Three-element cycle: nums=[2, 3, 1]
  MinSwaps: Expected=2, Actual=2, InputUnchanged=True => PASS
  MinSwaps2: Expected=2, Actual=2, InputUnchanged=True => PASS
Two disjoint cycles: nums=[2, 1, 4, 3]
  MinSwaps: Expected=2, Actual=2, InputUnchanged=True => PASS
  MinSwaps2: Expected=2, Actual=2, InputUnchanged=True => PASS
Fixed point and cycle: nums=[2, 3, 1, 4]
  MinSwaps: Expected=2, Actual=2, InputUnchanged=True => PASS
  MinSwaps2: Expected=2, Actual=2, InputUnchanged=True => PASS
Maximum value: nums=[999999999, 1000000000, 1]
  MinSwaps: Expected=1, Actual=1, InputUnchanged=True => PASS
  MinSwaps2: Expected=1, Actual=1, InputUnchanged=True => PASS
Maximum length sorted: length=100000
  MinSwaps: Expected=0, Actual=0, InputUnchanged=True => PASS
  MinSwaps2: Expected=0, Actual=0, InputUnchanged=True => PASS
Maximum length single cycle: length=100000
  MinSwaps: Expected=99999, Actual=99999, InputUnchanged=True => PASS
  MinSwaps2: Expected=99999, Actual=99999, InputUnchanged=True => PASS
Summary: 24/24 checks passed.
```

## 驗證命令

專案沒有獨立測試專案或測試框架，驗證由上述 console 固定案例完成。完整檢查順序如下：

```sh
dotnet restore leetcode_3551/leetcode_3551.csproj
dotnet build leetcode_3551/leetcode_3551.csproj
dotnet build leetcode_3551/leetcode_3551.csproj --no-restore -t:Rebuild -p:GenerateDocumentationFile=true -p:TreatWarningsAsErrors=true
dotnet run --no-build --project leetcode_3551/leetcode_3551.csproj
dotnet format leetcode_3551/leetcode_3551.csproj --verify-no-changes --no-restore
git diff --check
```

第三個命令重新編譯並啟用 XML 文件產生，將警告視為錯誤，用來檢查文件標記與編譯品質。格式檢查遵守現有 `.editorconfig`，包含四空白縮排、另起一行的大括號，以及 C# 檔案結尾不加換行。

對新建但尚未加入 Git 索引的 README，另以 `git diff --no-index --check /dev/null README.md` 檢查空白問題；此命令比較不同檔案時可能回傳 `1`，應同時確認有無空白錯誤診斷。

本次驗證另於儲存庫外執行十萬筆長環診斷：在 256 KiB 執行緒堆疊下，原本的遞迴 `Find` 重現 Stack overflow；改成迭代後，相同案例回傳 `99999`。一般執行時的堆疊容量依環境而異，這項診斷用來確認查找不再依賴遞迴深度。
