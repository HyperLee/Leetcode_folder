# LeetCode 104 — Maximum Depth of Binary Tree（二元樹的最大深度）

以 C# 遞迴計算二元樹從根節點到最遠葉節點的節點數。本專案使用 .NET 10，保留兩種遞迴寫法，並提供五組可直接執行的固定案例，方便比較兩種解法如何處理空樹、葉節點、平衡樹與偏斜樹。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：直接遞迴合併左右深度](#解法一直接遞迴合併左右深度)
- [解法二：明確處理葉節點與存在的子樹](#解法二明確處理葉節點與存在的子樹)
- [固定驗收案例](#固定驗收案例)
- [建置與執行](#建置與執行)

## 題目說明

給定一棵二元樹的根節點 `root`，回傳這棵樹的最大深度。

最大深度是從根節點到最遠葉節點的最長路徑所包含的節點數。空樹沒有任何節點，因此深度為 `0`；只有根節點的樹深度為 `1`。

例如：

```text
root = [3,9,20,null,null,15,7]

        3
       / \
      9  20
         / \
        15  7
```

最長路徑可以是 `3 -> 20 -> 15` 或 `3 -> 20 -> 7`，兩者都包含三個節點，因此最大深度為 `3`。

題目連結：

- [LeetCode 英文題目](https://leetcode.com/problems/maximum-depth-of-binary-tree/description/)
- [LeetCode 中文題目](https://leetcode.cn/problems/maximum-depth-of-binary-tree/description/)

## 限制條件

- 樹中的節點數量介於 `0` 到 `10^4`。
- `-100 <= Node.val <= 100`。
- `root` 可以是 `null`，代表空樹。
- 節點值不影響深度，只取決於樹的結構；不同節點可以具有相同的值。

## 解題概念與出發點

一棵非空二元樹的最大深度，可以拆成三個部分：

1. 目前根節點所占的一層。
2. 左子樹的最大深度。
3. 右子樹的最大深度。

從根節點到最遠葉節點的路徑只會選擇左右子樹中較深的一側，因此遞迴關係是：

```text
depth(root) = max(depth(root.left), depth(root.right)) + 1
depth(null) = 0
```

這個關係把「整棵樹的最大深度」縮小成兩個相同形式的子問題。當遞迴走到空節點時回傳 `0`，上一層便能將子樹深度與目前節點合併。

兩個實作都依照這個核心關係計算。差異在於終止條件與子樹走訪方式：

| 比較項目 | 解法一 `MaxDepth` | 解法二 `MaxDepth2` |
| --- | --- | --- |
| 空樹 | 回傳 `0` | 回傳 `0` |
| 葉節點 | 繼續呼叫兩個空子節點，再合併為 `1` | 直接回傳 `1` |
| 子樹走訪 | 左右兩側都呼叫 | 只呼叫實際存在的子樹 |
| 深度合併 | 一行 `Math.Max(...) + 1` | 使用 `maxDepth` 保存已知最大值 |
| 教學重點 | 最精簡的遞迴公式 | 展開遞迴判斷與狀態更新 |

## 解法一：直接遞迴合併左右深度

### 設計說明

`MaxDepth(root)` 先判斷目前節點是否為空：

- `root == null`：目前分支不包含節點，回傳 `0`。
- `root != null`：分別計算左右子樹深度，取較大值，再加上目前節點的一層。

核心程式：

```csharp
public static int MaxDepth(TreeNode? root)
{
    if (root == null)
    {
        return 0;
    }

    return Math.Max(MaxDepth(root.right), MaxDepth(root.left)) + 1;
}
```

這種寫法讓每個非空節點都套用完全相同的公式。即使目前節點是葉節點，左右兩次遞迴也會各自對 `null` 回傳 `0`，最後得到 `max(0, 0) + 1 = 1`。

### 正確性說明

- 空樹沒有節點，回傳 `0` 符合最大深度定義。
- 對任一非空節點，所有通往葉節點的路徑都必須先經過目前節點，再進入左子樹或右子樹。
- 左右子樹的遞迴結果分別是各自的最長路徑；取兩者較大值就選出了目前節點以下的最長路徑。
- 最後加 `1`，將目前節點納入路徑。

因此，方法回傳的正是從目前根節點到最遠葉節點的節點數。

### 複雜度分析

令 `n` 為節點數量，`h` 為樹的高度：

- **時間複雜度：`O(n)`**
  - 每個節點只處理一次。
- **空間複雜度：`O(h)`**
  - 額外空間來自遞迴呼叫堆疊。
  - 平衡樹的 `h` 約為 `log n`；完全偏斜時最壞為 `n`。

> [!NOTE]
> 題目允許最多 `10^4` 個節點。遞迴寫法簡潔，但極端偏斜樹會產生很深的呼叫堆疊；實務上若輸入可能遠大於題目範圍，可考慮改用佇列進行廣度優先走訪。

### 範例演示流程

輸入：

```text
[3,9,20,null,null,15,7]
```

由底部向上合併結果：

| 呼叫 | 左子樹深度 | 右子樹深度 | 回傳 |
| --- | ---: | ---: | ---: |
| `MaxDepth(9)` | 0 | 0 | `max(0, 0) + 1 = 1` |
| `MaxDepth(15)` | 0 | 0 | `1` |
| `MaxDepth(7)` | 0 | 0 | `1` |
| `MaxDepth(20)` | 1 | 1 | `max(1, 1) + 1 = 2` |
| `MaxDepth(3)` | 1 | 2 | `max(1, 2) + 1 = 3` |

根節點選擇較深的右子樹，最終回傳 `3`。

## 解法二：明確處理葉節點與存在的子樹

### 設計說明

`MaxDepth2(root)` 將判斷流程展開：

1. 空樹直接回傳 `0`。
2. 沒有左右子節點的葉節點直接回傳 `1`。
3. 以 `maxDepth` 保存目前找到的最大子樹深度。
4. 左子樹存在時才遞迴左側。
5. 右子樹存在時才遞迴右側。
6. 將最大子樹深度加 `1`，把目前節點納入結果。

核心程式：

```csharp
public static int MaxDepth2(TreeNode? root)
{
    if (root == null)
    {
        return 0;
    }

    if (root.left == null && root.right == null)
    {
        return 1;
    }

    int maxDepth = int.MinValue;

    if (root.left != null)
    {
        maxDepth = Math.Max(MaxDepth2(root.left), maxDepth);
    }

    if (root.right != null)
    {
        maxDepth = Math.Max(MaxDepth2(root.right), maxDepth);
    }

    return maxDepth + 1;
}
```

葉節點會在進入 `maxDepth` 更新流程前回傳，因此非葉節點至少有一棵存在的子樹，`maxDepth` 一定會被更新。這也解釋了為什麼最後的 `maxDepth + 1` 不會使用未更新的 `int.MinValue`。

### 正確性說明

- 空樹與葉節點的回傳值分別為 `0` 與 `1`，符合定義。
- 對非葉節點，只需要考慮實際存在的子樹，因為不存在的方向不可能形成根到葉的路徑。
- `maxDepth` 最終保存所有存在子樹中的最大深度。
- 加上目前節點的一層後，即得到目前樹的最大深度。

這種寫法與解法一使用相同的遞迴關係，但把葉節點以及單側子樹的處理方式明確表達出來。

### 複雜度分析

令 `n` 為節點數量，`h` 為樹的高度：

- **時間複雜度：`O(n)`**
  - 每個實際存在的節點只處理一次。
- **空間複雜度：`O(h)`**
  - 使用遞迴呼叫堆疊；平衡樹約為 `O(log n)`，完全偏斜時為 `O(n)`。

解法二可避免對葉節點的兩個空子節點繼續遞迴，但不改變漸進時間複雜度。

### 範例演示流程

同樣使用：

```text
[3,9,20,null,null,15,7]
```

執行流程：

| 步驟 | 目前節點 | 判斷與狀態 |
| --- | ---: | --- |
| 1 | 3 | 不是葉節點，準備比較左右子樹 |
| 2 | 9 | 左右皆空，是葉節點，直接回傳 `1` |
| 3 | 3 | `maxDepth` 更新為左子樹結果 `1` |
| 4 | 20 | 不是葉節點，繼續走訪 15 與 7 |
| 5 | 15 | 葉節點，回傳 `1` |
| 6 | 7 | 葉節點，回傳 `1` |
| 7 | 20 | 最大子樹深度為 `1`，加目前層後回傳 `2` |
| 8 | 3 | `maxDepth` 更新為 `2`，加目前層後回傳 `3` |

在只有單側子樹的情況下，此解法只會遞迴存在的一側，因此流程也適用於完全偏斜樹。

## 固定驗收案例

`Main` 不需要命令列輸入，會對每組樹執行兩種解法：

| 案例 | 層序表示 | 驗證重點 | 預期深度 |
| --- | --- | --- | ---: |
| 空樹 | `[]` | 空輸入邊界 | 0 |
| 單一節點（下限值） | `[-100]` | 單節點與節點值下限 | 1 |
| 官方範例 | `[3,9,20,null,null,15,7]` | 典型左右子樹 | 3 |
| 完全右偏樹 | `[1,null,2,null,3,null,4]` | 單側遞迴鏈 | 4 |
| 左右不等深且含重複值 | `[1,2,2,3,null,null,3,4]` | 選擇較深子樹、節點值不影響深度 | 4 |

五組案例乘以兩種解法，共有十項結果比對。專案目前沒有獨立的自動化測試專案；固定案例執行器、建置結果與主控台輸出共同作為行為驗收依據。

## 建置與執行

需求：

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

請從本 README 所在的 `leetcode_104` 專案根目錄執行：

```powershell
dotnet restore leetcode_104/leetcode_104.csproj
dotnet build leetcode_104/leetcode_104.csproj --nologo --no-restore
dotnet run --no-build --project leetcode_104/leetcode_104.csproj
```

實際執行輸出：

```text
[PASS] 空樹 | MaxDepth | Expected: 0 | Actual: 0
[PASS] 空樹 | MaxDepth2 | Expected: 0 | Actual: 0
[PASS] 單一節點（下限值） | MaxDepth | Expected: 1 | Actual: 1
[PASS] 單一節點（下限值） | MaxDepth2 | Expected: 1 | Actual: 1
[PASS] 官方範例 | MaxDepth | Expected: 3 | Actual: 3
[PASS] 官方範例 | MaxDepth2 | Expected: 3 | Actual: 3
[PASS] 完全右偏樹 | MaxDepth | Expected: 4 | Actual: 4
[PASS] 完全右偏樹 | MaxDepth2 | Expected: 4 | Actual: 4
[PASS] 左右不等深且含重複值 | MaxDepth | Expected: 4 | Actual: 4
[PASS] 左右不等深且含重複值 | MaxDepth2 | Expected: 4 | Actual: 4

10/10 checks passed.
Overall: PASS
```

## 專案結構

```text
leetcode_104/
├─ README.md
├─ docs/
│  └─ readme-template.md
├─ leetcode_104.sln
└─ leetcode_104/
   ├─ leetcode_104.csproj
   └─ Program.cs
```
