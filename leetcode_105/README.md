# LeetCode 105：從前序與中序遍歷序列構造二元樹

以 .NET 10 主控台程式示範兩種區間遞迴解法：先用線性搜尋理解左右子樹如何切分，再以雜湊表將根節點定位最佳化。程式內建五組可重複執行的案例，會自動比對重建結果的前序與中序遍歷。

- [LeetCode 英文題目](https://leetcode.com/problems/construct-binary-tree-from-preorder-and-inorder-traversal/description/)
- [LeetCode 中文題目](https://leetcode.cn/problems/construct-binary-tree-from-preorder-and-inorder-traversal/description/)

## 題目說明

給定兩個整數陣列：

- `preorder`：同一棵二元樹的前序遍歷，順序是「根 → 左 → 右」。
- `inorder`：同一棵二元樹的中序遍歷，順序是「左 → 根 → 右」。

請依據這兩組遍歷重建原本的二元樹，並回傳根節點。

例如：

```text
preorder = [3, 9, 20, 15, 7]
inorder  = [9, 3, 15, 20, 7]

重建結果：
        3
       / \
      9  20
        /  \
       15   7
```

## 限制條件

- `1 <= preorder.Length <= 3000`
- `inorder.Length == preorder.Length`
- `-3000 <= preorder[i], inorder[i] <= 3000`
- `preorder` 與 `inorder` 的元素皆不重複。
- `inorder` 中的每個值都會出現在 `preorder`。
- 兩個陣列保證是同一棵有效二元樹的前序與中序遍歷。

專案中的兩個公開解法另外接受 `null` 或空陣列，並在這兩種情況回傳 `null`；其餘不符合題目保證的輸入不另外驗證。

## 解題概念與出發點

前序遍歷的第一個值一定是目前子樹的根節點。只要在中序遍歷中找到該值，就能知道：

- 根節點左側都是左子樹。
- 根節點右側都是右子樹。
- 中序左側的元素數量，也就是前序中左子樹占用的元素數量。

若目前區間為：

```text
preorder[preStart..preEnd]
inorder[inStart..inEnd]
```

根節點在中序的索引為 `rootIndex`，則：

```text
leftSize = rootIndex - inStart

左子樹：
preorder[preStart + 1 .. preStart + leftSize]
inorder[inStart .. rootIndex - 1]

右子樹：
preorder[preStart + leftSize + 1 .. preEnd]
inorder[rootIndex + 1 .. inEnd]
```

當起始索引大於結束索引時，代表該子樹不存在，遞迴回傳 `null`。

## 解法一：區間遞迴與線性搜尋

### 設計說明

`BuildTree` 將完整陣列範圍交給 `BuildTreeHelper`：

1. 以前序區間的第一個值建立根節點。
2. 使用 `Array.IndexOf`，只在目前的中序區間搜尋根節點。
3. 依 `leftSize` 換算左右子樹在兩個陣列中的閉區間。
4. 遞迴建立左右子樹，再將結果接回根節點。

這個版本直接呈現「前序定根、中序分割」的核心。缺點是每層遞迴都可能重新掃描一段中序陣列；偏斜樹會依序搜尋 `n`、`n - 1`、`n - 2` 個元素，因此最壞時間為 `O(n²)`。

### 範例演示流程

以官方一般案例為例：

| 步驟 | 前序區間的根 | 中序根索引 | 左子樹範圍 | 右子樹範圍 |
| --- | ---: | ---: | --- | --- |
| 1 | `3` | `1` | 前序 `[9]`、中序 `[9]` | 前序 `[20,15,7]`、中序 `[15,20,7]` |
| 2 | `9` | `0` | 空 | 空 |
| 3 | `20` | `3` | 前序 `[15]`、中序 `[15]` | 前序 `[7]`、中序 `[7]` |
| 4 | `15` | `2` | 空 | 空 |
| 5 | `7` | `4` | 空 | 空 |

遞迴完成後，重新走訪樹會得到原本的前序 `[3, 9, 20, 15, 7]` 與中序 `[9, 3, 15, 20, 7]`。

## 解法二：區間遞迴與雜湊表索引

### 設計說明

`BuildTree2` 保留相同的區間切分規則，但先建立：

```text
值 9  -> 索引 0
值 3  -> 索引 1
值 15 -> 索引 2
值 20 -> 索引 3
值 7  -> 索引 4
```

`BuildTreeHelper2` 之後可直接以根節點值查到中序索引，不必在每層呼叫 `Array.IndexOf`。每個節點只會在建表及建樹時處理固定次數，因此總時間降為 `O(n)`；代價是索引表需要 `O(n)` 額外空間。

### 範例演示流程

同樣使用官方一般案例：

1. 一次掃描 `inorder` 建立五筆「值 → 索引」對應。
2. 前序第一個值 `3` 直接查得中序索引 `1`，切出左子樹 `[9]` 與右子樹 `[20, 15, 7]`。
3. 左子樹根 `9` 查得索引 `0`，左右區間皆為空。
4. 右子樹根 `20` 查得索引 `3`，切出左節點 `15` 與右節點 `7`。
5. `15`、`7` 也由索引表直接定位；空區間回傳 `null`，完成整棵樹的串接。

雜湊表只改善「如何找到根節點」，不改變 `leftSize` 及左右區間的計算方式，因此兩個版本會重建出相同的樹。

## 複雜度比較

| 解法 | 根節點定位 | 時間複雜度 | 額外空間 | 適合用途 |
| --- | --- | --- | --- | --- |
| `BuildTree` | 每層以 `Array.IndexOf` 線性搜尋 | 最壞 `O(n²)` | 遞迴最壞 `O(n)` | 直接理解區間切分 |
| `BuildTree2` | 先建立 `Dictionary<int, int>` | `O(n)` | 索引表與遞迴共 `O(n)` | 大型輸入與效能最佳化 |

兩種解法都不會修改輸入陣列。

## 可執行案例

測試器涵蓋：

| 案例 | 驗證重點 |
| --- | --- |
| 官方一般案例 | 同時包含左右子樹與不同深度 |
| 單節點 | 題目允許的最小輸入 |
| 全左偏樹 | 最壞遞迴深度與左區間切分 |
| 全右偏樹 | 最壞遞迴深度與右區間切分 |
| 含負數且左右不對稱 | 負值、不同子樹形狀與較複雜區間 |

每組案例會對兩種解法各驗證前序與中序遍歷，共 `5 × 2 × 2 = 20` 項檢查。

## 建置與執行

需求：安裝支援 `net10.0` 的 .NET 10 SDK。

從此目錄執行：

```bash
dotnet restore leetcode_105/leetcode_105.csproj
dotnet build leetcode_105/leetcode_105.csproj --nologo
dotnet run --project leetcode_105/leetcode_105.csproj --no-build
```

目前沒有獨立的自動化測試專案；驗收方式是零警告建置，加上 `Main` 中可重複執行的 Expected／Actual 檢查。

## 實際執行結果

```text
案例 1：官方一般案例
輸入 preorder = [3, 9, 20, 15, 7]
輸入 inorder  = [9, 3, 15, 20, 7]
  解法一：區間遞迴 + 線性搜尋
    前序 Expected: [3, 9, 20, 15, 7]
    前序 Actual:   [3, 9, 20, 15, 7] => PASS
    中序 Expected: [9, 3, 15, 20, 7]
    中序 Actual:   [9, 3, 15, 20, 7] => PASS
  解法二：區間遞迴 + 雜湊表索引
    前序 Expected: [3, 9, 20, 15, 7]
    前序 Actual:   [3, 9, 20, 15, 7] => PASS
    中序 Expected: [9, 3, 15, 20, 7]
    中序 Actual:   [9, 3, 15, 20, 7] => PASS

案例 2：單節點
輸入 preorder = [-1]
輸入 inorder  = [-1]
  解法一：區間遞迴 + 線性搜尋
    前序 Expected: [-1]
    前序 Actual:   [-1] => PASS
    中序 Expected: [-1]
    中序 Actual:   [-1] => PASS
  解法二：區間遞迴 + 雜湊表索引
    前序 Expected: [-1]
    前序 Actual:   [-1] => PASS
    中序 Expected: [-1]
    中序 Actual:   [-1] => PASS

案例 3：全左偏樹
輸入 preorder = [3, 2, 1]
輸入 inorder  = [1, 2, 3]
  解法一：區間遞迴 + 線性搜尋
    前序 Expected: [3, 2, 1]
    前序 Actual:   [3, 2, 1] => PASS
    中序 Expected: [1, 2, 3]
    中序 Actual:   [1, 2, 3] => PASS
  解法二：區間遞迴 + 雜湊表索引
    前序 Expected: [3, 2, 1]
    前序 Actual:   [3, 2, 1] => PASS
    中序 Expected: [1, 2, 3]
    中序 Actual:   [1, 2, 3] => PASS

案例 4：全右偏樹
輸入 preorder = [1, 2, 3]
輸入 inorder  = [1, 2, 3]
  解法一：區間遞迴 + 線性搜尋
    前序 Expected: [1, 2, 3]
    前序 Actual:   [1, 2, 3] => PASS
    中序 Expected: [1, 2, 3]
    中序 Actual:   [1, 2, 3] => PASS
  解法二：區間遞迴 + 雜湊表索引
    前序 Expected: [1, 2, 3]
    前序 Actual:   [1, 2, 3] => PASS
    中序 Expected: [1, 2, 3]
    中序 Actual:   [1, 2, 3] => PASS

案例 5：含負數且左右不對稱
輸入 preorder = [0, -3, -4, -1, 9, 12]
輸入 inorder  = [-4, -3, -1, 0, 9, 12]
  解法一：區間遞迴 + 線性搜尋
    前序 Expected: [0, -3, -4, -1, 9, 12]
    前序 Actual:   [0, -3, -4, -1, 9, 12] => PASS
    中序 Expected: [-4, -3, -1, 0, 9, 12]
    中序 Actual:   [-4, -3, -1, 0, 9, 12] => PASS
  解法二：區間遞迴 + 雜湊表索引
    前序 Expected: [0, -3, -4, -1, 9, 12]
    前序 Actual:   [0, -3, -4, -1, 9, 12] => PASS
    中序 Expected: [-4, -3, -1, 0, 9, 12]
    中序 Actual:   [-4, -3, -1, 0, 9, 12] => PASS

總結：20/20 項驗證通過
```

## 專案結構

```text
.
├── leetcode_105/
│   ├── leetcode_105.csproj
│   └── Program.cs
├── docs/
│   └── readme-template.md
├── .vscode/
├── leetcode_105.sln
└── README.md
```

## 參考資料

- [圖解從 O(n²) 到 O(n) 的最佳化思路](https://leetcode.cn/problems/construct-binary-tree-from-preorder-and-inorder-traversal/solutions/2646359/tu-jie-cong-on2-dao-onpythonjavacgojsrus-aob8/)
- [前序與中序遍歷重建二元樹解析](https://leetcode.cn/problems/construct-binary-tree-from-preorder-and-inorder-traversal/solutions/255811/cong-qian-xu-yu-zhong-xu-bian-li-xu-lie-gou-zao-9/)
