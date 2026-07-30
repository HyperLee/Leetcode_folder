# LeetCode 100 — Same Tree（相同的樹）

以 C# 與同步遞迴判斷兩棵二元樹是否具有完全相同的結構與節點值。本專案使用 .NET 10，並提供五組可直接執行的固定案例，方便觀察空樹、結構差異與節點值差異如何影響結果。

## 快速導覽

- [題目說明](#題目說明)
- [限制條件](#限制條件)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：同步遞迴比較](#解法一同步遞迴比較)
- [範例演示流程](#範例演示流程)
- [建置與執行](#建置與執行)

## 題目說明

給定兩棵二元樹的根節點 `p` 與 `q`，判斷它們是否為相同的樹。

兩棵樹必須同時符合以下條件，才算相同：

1. 結構相同：每一個對應位置都同時有節點，或同時沒有節點。
2. 節點值相同：每一個對應位置的節點值都相等。

例如：

```text
p = [1,2,3]      q = [1,2,3]

      1                1
     / \              / \
    2   3            2   3
```

兩棵樹的結構與對應節點值都相同，因此答案是 `true`。

```text
p = [1,2]        q = [1,null,2]

      1                1
     /                  \
    2                    2
```

雖然兩棵樹都包含 `1` 與 `2`，但 `2` 出現在不同方向，因此結構不同，答案是 `false`。

題目連結：

- [LeetCode 英文題目](https://leetcode.com/problems/same-tree/description/)
- [LeetCode 中文題目](https://leetcode.cn/problems/same-tree/description/)

## 限制條件

- 兩棵樹中的節點數量皆介於 `0` 到 `100`。
- `-10^4 <= Node.val <= 10^4`。
- 根節點可以是 `null`，代表空樹。

## 解題概念與出發點

只比較根節點的值並不足夠，因為根節點相同時，子樹仍可能有不同的結構或數值。完整答案必須繼續確認：

- `p.left` 是否與 `q.left` 相同。
- `p.right` 是否與 `q.right` 相同。

左右子樹本身又是規模較小的二元樹，因此可以重複使用同一套判斷。這就是遞迴的出發點：把「兩棵完整樹是否相同」拆成「目前節點是否相容，以及左右兩組子樹是否相同」。

每一層遞迴都會遇到下列四種情況：

| 判斷順序 | `p` | `q` | 結論 |
| --- | --- | --- | --- |
| 1 | `null` | `null` | 目前分支同時結束，回傳 `true` |
| 2 | `null` | 非 `null`，或相反 | 結構不同，回傳 `false` |
| 3 | 非 `null` | 非 `null`，但值不同 | 內容不同，回傳 `false` |
| 4 | 非 `null` | 非 `null`，且值相同 | 繼續比較左右子樹 |

判斷順序很重要。只有在確定兩個節點都不是 `null` 後，才能安全讀取 `val`、`left` 與 `right`。

## 解法一：同步遞迴比較

### 設計說明

`IsSameTree(p, q)` 同時接收兩棵樹目前位置的節點，並依序處理三個終止條件：

1. **兩邊都是空節點**
   - 代表兩棵樹在這個位置同時結束。
   - 這一條分支相同，因此回傳 `true`。
2. **只有一邊是空節點**
   - 代表一棵樹有節點，另一棵樹沒有。
   - 即使其他節點值相同，結構仍然不同，因此立即回傳 `false`。
3. **兩邊都有節點，但節點值不同**
   - 對應位置的內容不同，立即回傳 `false`。

通過三個終止條件後，可確定目前兩個節點都存在且值相同。此時問題縮小成兩個子問題：

```text
左子樹是否相同 = IsSameTree(p.left, q.left)
右子樹是否相同 = IsSameTree(p.right, q.right)
```

最終使用邏輯 AND 組合：

```text
IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right)
```

只有左右子樹都回傳 `true`，目前這兩棵樹才相同。C# 的 `&&` 具有短路特性：若左子樹已經不同，右子樹不再執行，因為整體答案必定是 `false`。

### 核心程式

```csharp
public static bool IsSameTree(TreeNode? p, TreeNode? q)
{
    if (p == null && q == null)
    {
        return true;
    }
    else if (p == null || q == null)
    {
        return false;
    }
    else if (p.val != q.val)
    {
        return false;
    }
    else
    {
        return IsSameTree(p.left, q.left)
            && IsSameTree(p.right, q.right);
    }
}
```

### 正確性說明

可以依目前節點的狀態說明演算法為何正確：

- 若兩個節點都是 `null`，兩棵子樹皆為空，必定相同。
- 若只有一個節點是 `null`，兩棵子樹的根部結構已不同，必定不相同。
- 若兩個節點都存在但值不同，對應內容不同，必定不相同。
- 剩下的情況是兩個根節點都存在且值相同。此時兩棵樹相同，若且唯若它們的左子樹相同且右子樹也相同；遞迴呼叫正好驗證這兩個必要條件。

因此，演算法對空樹、葉節點與任意深度的二元樹都能給出正確結果。

### 複雜度分析

令 `n` 為實際比較到的對應節點數量，`h` 為樹的高度：

- **時間複雜度：`O(n)`**
  - 最壞情況下，兩棵樹完全相同，或差異出現在最後才檢查的位置，因此必須走訪所有對應節點。
  - 若較早發現結構或節點值不同，會提前回傳，實際走訪數可能少於 `n`。
- **空間複雜度：`O(h)`**
  - 額外空間來自遞迴呼叫堆疊。
  - 平衡二元樹的高度約為 `O(log n)`；完全偏斜的樹最壞可達 `O(n)`。

## 範例演示流程

### 演示一：兩棵樹完全相同

輸入：

```text
p = [1,2,3]
q = [1,2,3]
```

遞迴流程：

| 步驟 | 比較位置 | 判斷 | 結果 |
| --- | --- | --- | --- |
| 1 | 根節點 `1` 與 `1` | 都存在且值相同 | 繼續左右子樹 |
| 2 | 左節點 `2` 與 `2` | 都存在且值相同 | 繼續比較其空子樹 |
| 3 | `2` 的左側 `null` 與 `null` | 同時為空 | `true` |
| 4 | `2` 的右側 `null` 與 `null` | 同時為空 | `true` |
| 5 | 右節點 `3` 與 `3` | 都存在且值相同 | 兩個空子樹皆為 `true` |
| 6 | 合併根節點左右結果 | `true && true` | `true` |

### 演示二：節點方向不同

輸入：

```text
p = [1,2]
q = [1,null,2]
```

1. 根節點 `1` 與 `1` 相同，先比較左子樹。
2. `p.left` 是節點 `2`，`q.left` 是 `null`。
3. 僅一邊為空，立即回傳 `false`。
4. 因為 `&&` 短路，不需要再比較右子樹，整體答案是 `false`。

這個案例說明：擁有相同的一組節點值，不代表兩棵樹的結構相同。

### 演示三：結構相同但節點值不同

輸入：

```text
p = [1,2,1]
q = [1,1,2]
```

1. 根節點 `1` 與 `1` 相同，進入左子樹。
2. 左節點分別為 `2` 與 `1`。
3. 節點值不同，立即回傳 `false`。
4. 不必檢查剩餘分支，整體答案是 `false`。

## 固定驗收案例

`Main` 會執行下列案例，不需要輸入命令列參數：

| 案例 | 重點 | 預期結果 |
| --- | --- | --- |
| 兩棵空樹 | 空輸入邊界 | `true` |
| 相同的三節點樹 | 典型相同案例 | `true` |
| 左右結構不同 | 節點值相同但位置不同 | `false` |
| 相同結構但節點值不同 | 含重複值的內容差異 | `false` |
| 單邊為空樹 | 只有一棵樹有根節點 | `false` |

本專案目前沒有獨立的自動化測試專案；固定案例執行器、建置結果與主控台輸出共同作為驗收依據。

## 建置與執行

需求：

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

請從本 README 所在的專案根目錄執行：

```powershell
dotnet restore leetcode_100/leetcode_100.csproj
dotnet build leetcode_100/leetcode_100.csproj --nologo --no-restore
dotnet run --project leetcode_100/leetcode_100.csproj --no-build
```

實際執行輸出：

```text
[PASS] 兩棵空樹 | Expected: True | Actual: True
[PASS] 相同的三節點樹 | Expected: True | Actual: True
[PASS] 左右結構不同 | Expected: False | Actual: False
[PASS] 相同結構但節點值不同 | Expected: False | Actual: False
[PASS] 單邊為空樹 | Expected: False | Actual: False

5/5 test cases passed.
Overall: PASS
```

## 專案結構

```text
leetcode_100/
├─ README.md
├─ docs/
│  └─ readme-template.md
├─ leetcode_100.sln
└─ leetcode_100/
   ├─ leetcode_100.csproj
   └─ Program.cs
```
