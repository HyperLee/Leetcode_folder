<div align="center">

# LeetCode 1026: Maximum Difference Between Node and Ancestor

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-DFS-239120?style=flat-square&logo=csharp&logoColor=white)
[![LeetCode](https://img.shields.io/badge/LeetCode-1026-FFA116?style=flat-square&logo=leetcode&logoColor=black)](https://leetcode.com/problems/maximum-difference-between-node-and-ancestor/)

使用 C# 與深度優先搜尋解決「節點與其祖先之間的最大差值」。核心技巧是沿著搜尋路徑保存祖先最小值與最大值，讓每個節點只需 O(1) 比較即可更新答案。

[題目](#題目) • [解題出發點](#解題出發點) • [解法](#解法) • [執行專案](#執行專案) • [流程演示](#流程演示)

</div>

## 題目

給定一棵二元樹的根節點 `root`，找出最大值 `v`，使得存在兩個不同節點 `a` 與 `b`，並且：

- `a` 是 `b` 的祖先節點
- `v = |a.val - b.val|`

也就是說，我們要找出所有「祖先節點」與「子孫節點」之間的最大絕對差值。

> [!NOTE]
> 題目連結：[LeetCode 1026](https://leetcode.com/problems/maximum-difference-between-node-and-ancestor/) / [力扣 1026](https://leetcode.cn/problems/maximum-difference-between-node-and-ancestor/description/)

## 解題出發點

直覺枚舉法有兩種方向：

- 枚舉每個子孫節點，再回頭比較它的所有祖先節點。
- 枚舉每個祖先節點，再往下比較它的所有子孫節點。

如果真的把所有祖先或子孫都拿出來比較，會做很多重複工作。觀察後可以發現：對任一目前節點 `x` 來說，不需要知道路徑上的所有祖先值，只需要知道祖先中的最小值 `mi` 與最大值 `ma`。

原因是任一祖先節點 `y` 都滿足 `mi <= y <= ma`：

- 若 `x <= y`，則 `|x - y| = y - x <= ma - x = |x - ma|`
- 若 `x > y`，則 `|x - y| = x - y <= x - mi = |x - mi|`

因此，當我們站在節點 `x` 時，最大差值只可能來自 `mi` 或 `ma`。

## 解法

本專案採用第一種思路：枚舉子孫節點，並在 DFS 過程中保存從根節點到目前節點路徑上的祖先範圍。

DFS 狀態包含三個值：

| 狀態 | 說明 |
| --- | --- |
| `root` | 目前拜訪的節點 |
| `ancestorMin` | 從根節點到目前節點路徑上的最小祖先值 |
| `ancestorMax` | 從根節點到目前節點路徑上的最大祖先值 |

每次拜訪節點時：

1. 計算目前節點與祖先最小值、最大值的差：`max(|root.val - ancestorMin|, |root.val - ancestorMax|)`。
2. 用目前節點值更新下一層 DFS 的祖先範圍：`nextMin = min(ancestorMin, root.val)`，`nextMax = max(ancestorMax, root.val)`。
3. 遞迴搜尋左子樹與右子樹。
4. 回傳目前節點、左子樹、右子樹三者中的最大值。

> [!TIP]
> 第二種思路也可行：讓遞迴回傳目前子樹的最小值與最大值，再從祖先端計算差值。本題程式採用「往下傳路徑狀態」的寫法，因為狀態清楚、實作也更直接。

### 複雜度

- 時間複雜度：`O(n)`，每個節點只拜訪一次。
- 空間複雜度：`O(h)`，其中 `h` 是二元樹高度，來自遞迴呼叫堆疊。

## 專案結構

| 路徑 | 說明 |
| --- | --- |
| [leetcode_1026/Program.cs](leetcode_1026/Program.cs) | 題解、DFS 輔助函式、範例測試資料 |
| [leetcode_1026/leetcode_1026.csproj](leetcode_1026/leetcode_1026.csproj) | .NET 10 console 專案設定 |
| [leetcode_1026.sln](leetcode_1026.sln) | Visual Studio / VS Code 可開啟的 solution |

## 執行專案

需要先安裝 [.NET SDK 10](https://dotnet.microsoft.com/download)。

```bash
dotnet build leetcode_1026/leetcode_1026.csproj
dotnet run --project leetcode_1026/leetcode_1026.csproj
```

預期輸出：

```text
Example 1: actual = 7, expected = 7
Example 2: actual = 3, expected = 3
```

## 流程演示

以第一個測試資料為例：

```text
        8
       / \
      3   10
     / \    \
    1   6    14
       / \   /
      4   7 13
```

DFS 會從根節點 `8` 出發，沿路記錄目前路徑上的 `ancestorMin` 與 `ancestorMax`。

| 拜訪節點 | 進入時的祖先範圍 | 目前差值 | 更新後傳給子樹 |
| --- | --- | --- | --- |
| `8` | `min = 8`, `max = 8` | `0` | `min = 8`, `max = 8` |
| `3` | `min = 8`, `max = 8` | `5` | `min = 3`, `max = 8` |
| `1` | `min = 3`, `max = 8` | `7` | `min = 1`, `max = 8` |
| `6` | `min = 3`, `max = 8` | `3` | `min = 3`, `max = 8` |
| `14` | `min = 8`, `max = 10` | `6` | `min = 8`, `max = 14` |
| `13` | `min = 8`, `max = 14` | `5` | `min = 8`, `max = 14` |

最大值出現在路徑 `8 -> 3 -> 1`：

```text
|8 - 1| = 7
```

所以答案是 `7`。