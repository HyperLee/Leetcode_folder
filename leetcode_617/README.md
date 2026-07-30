# 617. Merge Two Binary Trees

[![LeetCode](https://img.shields.io/badge/LeetCode-617-orange?style=flat-square)](https://leetcode.com/problems/merge-two-binary-trees/)
[![Difficulty](https://img.shields.io/badge/Difficulty-Easy-green?style=flat-square)](https://leetcode.com/problems/merge-two-binary-trees/)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square)](https://dotnet.microsoft.com/)

以 C# 與遞迴方式合併兩棵二元樹，並提供六組可直接執行、會自動判定整棵樹是否正確的範例資料。

## 題目說明

給定兩棵二元樹 `root1` 與 `root2`。將兩棵樹從根節點開始疊合，並依下列規則產生合併結果：

- 兩個對應節點都存在時，新節點值為兩者之和。
- 只有一個對應節點存在時，使用該非 `null` 節點及其子樹。
- 兩個對應節點都不存在時，結果也是 `null`。

例如：

```text
root1:            root2:            merged:
    1                 2                 3
   / \               / \               / \
  3   2             1   3             4   5
 /                   \   \           / \   \
5                     4   7         5   4   7

root1 = [1,3,2,5]
root2 = [2,1,3,null,4,null,7]
output = [3,4,5,5,4,null,7]
```

> [!NOTE]
> 陣列採用 level-order（由上到下、由左到右）表示，`null` 代表該位置沒有節點。

## 限制條件

- 兩棵樹的節點總數皆在 `[0, 2000]` 範圍內。
- `-10⁴ <= Node.val <= 10⁴`。
- `root1` 與 `root2` 都可能是空樹。

## 解題概念與出發點

### 為什麼適合使用遞迴

二元樹本身就是遞迴結構：每個節點都可視為「目前節點、左子樹、右子樹」。因此「合併兩棵樹」可以拆成三個相同型態的小問題：

1. 決定目前兩個節點要如何合併。
2. 合併兩者的左子樹。
3. 合併兩者的右子樹。

每次遞迴都只需要處理一對對應節點，不需要額外維護父節點或整棵樹的狀態。

### 邊界條件

當任一節點為 `null` 時，不必繼續走訪另一棵樹的剩餘分支，可直接回傳非空節點：

- `root1 == null`：回傳 `root2`，包括 `root2` 的完整子樹。
- `root2 == null`：回傳 `root1`，包括 `root1` 的完整子樹。
- 兩者皆為 `null`：第一個條件自然回傳 `null`。

這個設計讓遞迴只深入兩棵樹實際重疊的部分。

## 解法一：深度優先遞迴

### 設計流程

1. 檢查 `root1` 是否為 `null`；若是，直接回傳 `root2`。
2. 檢查 `root2` 是否為 `null`；若是，直接回傳 `root1`。
3. 兩個節點都存在時，建立值為 `root1.val + root2.val` 的新節點。
4. 使用相同規則遞迴合併左右子樹。
5. 將左右合併結果接到新節點並回傳。

```csharp
public TreeNode? MergeTrees(TreeNode? root1, TreeNode? root2)
{
    if (root1 is null)
    {
        return root2;
    }

    if (root2 is null)
    {
        return root1;
    }

    TreeNode mergedNode = new(root1.val + root2.val)
    {
        left = MergeTrees(root1.left, root2.left),
        right = MergeTrees(root1.right, root2.right)
    };

    return mergedNode;
}
```

### 節點配置與輸入影響

- 對應位置都有節點時，解法會配置一個新的合併節點，不會改寫兩個原節點的值或連結。
- 只有一側存在時，解法直接沿用該側既有的節點參考與完整子樹，避免不必要的複製。
- 因此輸入樹本身不會被修改，但輸出樹的非重疊分支可能與輸入樹共享節點。

若呼叫端需要一棵與輸入完全沒有共享參考的深層副本，必須在非重疊分支額外複製節點；本專案維持題目常見的直接沿用設計。

### 複雜度分析

令 `m`、`n` 分別為兩棵樹的節點數，`h1`、`h2` 為兩棵樹的高度，`k` 為兩棵樹重疊位置的節點對數。

| 項目 | 複雜度 | 說明 |
|---|---:|---|
| 時間 | `O(min(m, n))` | 只需繼續走訪兩棵樹都存在的重疊區域。 |
| 遞迴輔助空間 | `O(min(h1, h2))` | 最深呼叫鏈不會超過兩棵樹重疊的高度。 |
| 新配置節點空間 | `O(k)` | 每個重疊位置建立一個新節點；非重疊子樹直接沿用。 |

## 範例演示流程

以官方完整範例說明：

| 步驟 | 對應節點 | 判斷 | 回傳結果 |
|---:|---|---|---|
| 1 | `(1, 2)` | 兩者存在 | 建立 `3`，繼續處理左右子樹 |
| 2 | `(3, 1)` | 兩者存在 | 建立 `4` |
| 3 | `(5, null)` | 右側為空 | 沿用節點 `5` |
| 4 | `(null, 4)` | 左側為空 | 沿用節點 `4` |
| 5 | `(2, 3)` | 兩者存在 | 建立 `5` |
| 6 | `(null, null)` | 兩者皆空 | 回傳 `null` |
| 7 | `(null, 7)` | 左側為空 | 沿用節點 `7` |

組合遞迴結果後：

```text
        3
       / \
      4   5
     / \   \
    5   4   7
```

level-order 結果為 `[3,4,5,5,4,null,7]`。

## 可執行驗證

`Main` 內含六組案例。每組資料會：

1. 將 level-order 陣列建立為二元樹。
2. 呼叫 `MergeTrees`。
3. 將完整結果樹序列化回 level-order。
4. 比對 `expected` 與 `actual` 的所有節點位置。
5. 輸出 `PASS` 或 `FAIL`；任一案例失敗時，程式會設定非零結束碼。

案例涵蓋官方範例、兩棵空樹、僅單側存在，以及含負值的完全重疊樹。

## 建置與執行

環境需求：[.NET 10 SDK](https://dotnet.microsoft.com/download)

從本 README 所在的專案根目錄執行：

```bash
dotnet build leetcode_617/leetcode_617.csproj --nologo
dotnet run --no-build --project leetcode_617/leetcode_617.csproj
```

### 實際執行結果

以下內容來自 `dotnet run --no-build --project leetcode_617/leetcode_617.csproj`：

```text
617. Merge Two Binary Trees
===========================

Case 1: 官方完整範例
  root1:    [1,3,2,5]
  root2:    [2,1,3,null,4,null,7]
  expected: [3,4,5,5,4,null,7]
  actual:   [3,4,5,5,4,null,7]
  result:   PASS

Case 2: 官方第二範例
  root1:    [1]
  root2:    [1,2]
  expected: [2,2]
  actual:   [2,2]
  result:   PASS

Case 3: 兩棵空樹
  root1:    []
  root2:    []
  expected: []
  actual:   []
  result:   PASS

Case 4: 僅第一棵存在
  root1:    [1,null,2]
  root2:    []
  expected: [1,null,2]
  actual:   [1,null,2]
  result:   PASS

Case 5: 僅第二棵存在
  root1:    []
  root2:    [0,-1,1]
  expected: [0,-1,1]
  actual:   [0,-1,1]
  result:   PASS

Case 6: 含負值且完全重疊
  root1:    [-10,-5,3]
  root2:    [10,5,-3]
  expected: [0,0,0]
  actual:   [0,0,0]
  result:   PASS

Summary: 6/6 checks passed.
```

## 專案結構

| 路徑 | 用途 |
|---|---|
| `leetcode_617/Program.cs` | 遞迴解法、樹資料結構、測試資料與 console harness |
| `leetcode_617/leetcode_617.csproj` | .NET 10 console 專案設定 |
| `docs/readme-template.md` | README 的內容與驗證原則 |

## 參考資料

- [LeetCode 617 — Merge Two Binary Trees](https://leetcode.com/problems/merge-two-binary-trees/)
- [LeetCode 中國站 617 — 合併二叉樹](https://leetcode.cn/problems/merge-two-binary-trees/)
