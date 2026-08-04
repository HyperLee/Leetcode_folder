# LeetCode 1530：好葉子節點對的數量

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4)
![Language](https://img.shields.io/badge/language-C%23-239120)

本專案使用 C# 與 .NET 10 實作 LeetCode 1530「好葉子節點對的數量」，並提供兩種
可以獨立呼叫的解法：

1. 後序遞迴合併左右子樹的葉節點距離。
2. 收集所有 root-to-leaf 路徑，再逐對計算葉節點距離。

`Main` 會執行 8 組固定案例，讓兩種解法各自接受相同的預期值驗證，總共進行
16 項 `PASS/FAIL` 檢查。

- [題目說明](#題目說明)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：後序遞迴合併葉節點距離](#解法一後序遞迴合併葉節點距離)
- [解法二：葉節點路徑配對](#解法二葉節點路徑配對)
- [兩種解法比較](#兩種解法比較)
- [可執行驗證](#可執行驗證)
- [建置與執行](#建置與執行)

## 題目說明

給定一棵二元樹的根節點 `root`，以及一個整數 `distance`。兩個不同的葉節點之間，
如果最短路徑長度小於或等於 `distance`，就稱為一組「好葉節點對」。請回傳樹中
好葉節點對的數量。

葉節點是沒有左子節點與右子節點的節點。路徑長度計算的是邊的數量，而不是節點
數量。例如下列樹中，葉節點 `3` 與 `4` 的路徑為：

```text
4 → 2 → 1 → 3
```

因此距離是 `3`。

### 官方範例一

```text
輸入：root = [1,2,3,null,4], distance = 3
輸出：1
```

葉節點是 `3` 與 `4`，兩者距離為 `3`，所以只有一組好葉節點對。

### 官方範例二

```text
輸入：root = [1,2,3,4,5,6,7], distance = 3
輸出：2
```

好葉節點對是 `[4,5]` 與 `[6,7]`。它們各自在同一側子樹內，距離都是 `2`；
例如 `[4,6]` 的距離是 `4`，不符合條件。

### 官方範例三

```text
輸入：root = [7,1,4,6,null,5,3,null,null,null,null,null,2], distance = 3
輸出：1
```

這個案例只有葉節點 `5` 與 `2` 的距離符合上限，因此答案是 `1`。

### 限制條件

依 [LeetCode 1530 官方題面](https://leetcode.com/problems/number-of-good-leaf-nodes-pairs/)：

- 節點數量介於 `1` 到 `2^10`。
- `1 <= Node.val <= 100`。
- `1 <= distance <= 10`。

本專案另外示範 `root == null` 的防禦性處理；空樹不屬於官方限制中的必要輸入，
兩種方法在空樹時都回傳 `0`。

## 解題概念與出發點

### 先把問題拆成「葉節點距離」

任意一組葉節點對的最短路徑，都會經過它們的最低共同祖先（Lowest Common
Ancestor，LCA）。如果兩個葉節點分別位於某個節點的左、右子樹，那麼它們經過
目前節點的距離可以拆成：

```text
左葉節點到目前節點的距離
+ 右葉節點到目前節點的距離
+ 左右子樹根到目前節點各多走的一條邊
```

兩種解法的差別，在於如何保存與取得這些葉節點距離：

- 解法一只保存每個子樹需要的距離數值，邊走訪邊合併。
- 解法二保存每個葉節點的完整 root-to-leaf 路徑，最後逐對找出共同祖先。

### 為什麼不能只比較節點值

節點值不是節點身分。不同節點可能具有相同的 `val`，因此計算共同路徑時必須
比較節點物件本身。`CountPairsByLeafPaths` 使用 `ReferenceEquals` 比較路徑中的
節點，`Main` 的「重複值節點」案例用來驗證這個條件。

## 解法一：後序遞迴合併葉節點距離

公開方法：

```csharp
public static int CountPairs(TreeNode? root, int distance)
```

### 設計說明

私有 helper `CollectDistances` 對每個子樹回傳一份距離清單。清單中的每個整數
代表：

> 目前子樹根節點到某個葉節點的邊數，而且該距離不超過 `distance`。

對每個節點採用後序處理：

1. `node == null`：沒有葉節點，回傳空清單。
2. 目前節點是葉節點：回傳 `[0]`，因為節點到自己的距離是 `0`。
3. 遞迴處理左、右子樹，取得兩份子樹距離清單。
4. 子樹距離往目前節點移動一層，所以每個距離加 `1`；超過上限的距離不再保留。
5. 對左清單與右清單做交叉配對。若：

   ```text
   leftDistance + rightDistance + 2 <= distance
   ```

   就代表這是一組好葉節點對。

6. 將目前節點可繼續向上傳遞的距離清單回傳給父節點。

`pairCount` 是 `CountPairs` 每次呼叫時建立的區域變數，因此連續執行不同案例時，
不會沿用上一棵樹的答案。

### 以官方範例一逐步演示

輸入為 `[1,2,3,null,4]`，`distance = 3`：

```text
        1
       / \
      2   3
       \
        4
```

| 處理位置 | 左側距離 | 右側距離 | 合併結果 |
| --- | --- | --- | --- |
| `4` | - | - | 葉節點回傳 `[0]` |
| `2` | `[]` | `[0]` | `4` 往上變成距離 `1`，回傳 `[1]` |
| `3` | - | - | 葉節點回傳 `[0]` |
| `1` | `[1]` | `[0]` | `1`：來自葉節點 `4`；`4` 先回傳 `[0]`，經過節點 `2` 時沿 `2 → 4` 多走 1 條邊，成為左側子樹根 `2` 到葉節點 `4` 的距離 `1`。<br>`0`：來自葉節點 `3`；節點 `3` 本身就是葉節點，從 `3` 到自己不需走邊。<br>`2`：根節點 `1` 到左右子樹根 `2`、`3` 的兩條邊，即 `1 → 2` 與 `1 → 3`。因此 `1 + 0 + 2 = 3`，也就是 `4 → 2 → 1 → 3` 的距離，符合 `distance = 3`，計入 1 組。 |

根節點處理完成後，`pairCount` 為 `1`。

### 正確性說明

對任一節點而言，遞迴先完整處理左右子樹，所以左右清單包含該節點兩側所有
仍可能形成合法配對的葉節點距離。兩個葉節點若分屬左右子樹，其最短路徑必須
先分別走到左右子樹根，再經過目前節點，故距離正好是
`leftDistance + rightDistance + 2`。所有跨左右子樹的組合都被檢查，單側葉節點
距離則被傳回父節點；因此每組葉節點對會在其最低共同祖先處被計算一次。

### 複雜度

令 `Sleft(v)` 與 `Sright(v)` 表示節點 `v` 左右子樹回傳的距離清單大小：

- 時間：`O(n + Σ Sleft(v) × Sright(v))`。每個節點只走訪一次，配對成本來自
  左右距離清單的交叉檢查；清單只保留不超過 `distance` 的距離。
- 額外空間：最壞為 `O(n)`，包含遞迴堆疊與目前存活的距離清單。

在題目 `distance <= 10` 的限制下，保留距離上限能有效限制每個子樹向上傳遞的資料量。

## 解法二：葉節點路徑配對

公開方法：

```csharp
public static int CountPairsByLeafPaths(TreeNode? root, int distance)
```

### 設計說明

此方法先用 DFS 收集所有葉節點的完整路徑。以官方範例一為例，收集結果為：

```text
葉節點 4：1 → 2 → 4
葉節點 3：1 → 3
```

接著只枚舉一次每個葉節點對 `i < j`：

1. 從 root 開始比較兩條路徑的節點參考。
2. 計算兩條路徑共有多少個開頭節點。
3. 共同前綴的最後一個節點就是兩個葉節點的 LCA。
4. 若共同前綴長度為 `commonNodeCount`，兩葉節點的邊距離為：

   ```text
   leftPath.Count + rightPath.Count - 2 × commonNodeCount
   ```

5. 距離不超過 `distance` 時，將答案加一。

收集葉節點路徑時，抵達葉節點必須複製目前路徑；否則 DFS 回溯後，所有已保存
的路徑都會受到同一個可變清單的影響。

### 以官方範例一演示

兩條路徑如下：

```text
leftPath  = [1, 2, 4]
rightPath = [1, 3]
```

| 步驟 | 結果 |
| --- | --- |
| 比較索引 `0` | 兩條路徑都是節點 `1`，共同前綴長度變成 `1` |
| 比較索引 `1` | 節點 `2` 與 `3` 不同，停止比較 |
| 計算距離 | `3 + 2 - 2 × 1 = 3` |
| 判斷 | `3 <= distance(3)`，答案加一 |

### 正確性說明

每條 root-to-leaf 路徑都包含該葉節點的所有祖先。兩條路徑的最長共同前綴必定
從根開始延伸到它們的最低共同祖先；共同前綴之後的節點數，分別就是 LCA 到兩個
葉節點的邊數。相加後即可得到兩葉節點的最短路徑長度。由於每個索引組合只處理
一次，所有不同葉節點對都會被計算一次且不重複。

### 複雜度

令 `L` 為葉節點數、`H` 為樹高：

- 時間：`O(n + L²H)`。DFS 收集路徑需要 `O(n)`，每個葉節點對最壞比較 `H` 個節點。
- 額外空間：`O(LH + H)`。完整路徑需要 `O(LH)`，DFS 目前路徑與呼叫堆疊需要 `O(H)`。

此方法比解法一保留更多資料，但不需要在遞迴合併時維護配對累加器，適合作為理解
LCA 與路徑距離的直觀版本。

## 兩種解法比較

| 比較項目 | 解法一：距離清單合併 | 解法二：路徑配對 |
| --- | --- | --- |
| 核心資料 | 子樹根到葉節點的距離清單 | 每個葉節點的完整 root-to-leaf 路徑 |
| 配對時機 | 在葉節點對的最低共同祖先處立即計算 | 收集完所有葉節點後逐對計算 |
| LCA 表示 | 由目前遞迴節點隱含表示 | 由兩條路徑的共同前綴表示 |
| 優點 | 不需保存完整路徑，直接合併答案 | 概念直觀，容易逐步檢查路徑距離 |
| 代價 | 需要理解後序回傳資料與跨子樹合併 | 需要保存路徑，葉節點多時配對成本較高 |
| 本專案方法 | `CountPairs` | `CountPairsByLeafPaths` |

兩種方法都不修改輸入樹。`Main` 仍使用 tree factory 讓每個方法取得獨立的樹，
因此即使未來其中一個方法改成原地處理，也不會污染另一個方法的案例。

## 可執行驗證

`Main` 使用手動推導的預期值，不以其中一個解法的輸出作為另一個解法的預期值：

| 案例 | distance | 預期 | 驗證重點 |
| --- | ---: | ---: | --- |
| 官方範例一 | 3 | 1 | 距離剛好等於上限 |
| 官方範例二 | 3 | 2 | 兩組獨立的好葉節點對 |
| 官方範例三 | 3 | 1 | 較深樹與單一合法配對 |
| 距離剛好符合 | 2 | 1 | 兩個直接子葉節點 |
| 距離不足無法配對 | 1 | 0 | 最小合法葉節點距離也超過上限 |
| 重複值節點 | 3 | 3 | 確認以節點參考而非 `val` 判斷路徑 |
| 單一節點 | 10 | 0 | 沒有兩個不同葉節點 |
| 空樹防禦案例 | 3 | 0 | 額外的 null root 處理 |

每個案例會分別執行 `CountPairs` 與 `CountPairsByLeafPaths`，共 16 項檢查。任一
案例失敗時，console 會顯示 `FAIL`，並設定非零結束碼。

### 實際執行結果

以下內容來自 `dotnet run --project leetcode_1530/leetcode_1530.csproj --no-build`：

```text
LeetCode 1530：好葉子節點對的數量

案例 1：官方範例一：葉節點距離剛好 3
輸入：root = [1,2,3,null,4], distance = 3
預期：1
CountPairs：實際 = 1，結果 = PASS
CountPairsByLeafPaths：實際 = 1，結果 = PASS

案例 2：官方範例二：兩組好葉節點對
輸入：root = [1,2,3,4,5,6,7], distance = 3
預期：2
CountPairs：實際 = 2，結果 = PASS
CountPairsByLeafPaths：實際 = 2，結果 = PASS

案例 3：官方範例三：只有一組好葉節點對
輸入：root = [7,1,4,6,null,5,3,null,null,null,null,null,2], distance = 3
預期：1
CountPairs：實際 = 1，結果 = PASS
CountPairsByLeafPaths：實際 = 1，結果 = PASS

案例 4：距離剛好符合
輸入：root = [1,2,3], distance = 2
預期：1
CountPairs：實際 = 1，結果 = PASS
CountPairsByLeafPaths：實際 = 1，結果 = PASS

案例 5：距離不足無法配對
輸入：root = [1,2,3], distance = 1
預期：0
CountPairs：實際 = 0，結果 = PASS
CountPairsByLeafPaths：實際 = 0，結果 = PASS

案例 6：重複值節點
輸入：root = [1,1,1,null,null,1,1], distance = 3
預期：3
CountPairs：實際 = 3，結果 = PASS
CountPairsByLeafPaths：實際 = 3，結果 = PASS

案例 7：單一節點
輸入：root = [1], distance = 10
預期：0
CountPairs：實際 = 0，結果 = PASS
CountPairsByLeafPaths：實際 = 0，結果 = PASS

案例 8：空樹防禦案例
輸入：root = [], distance = 3
預期：0
CountPairs：實際 = 0，結果 = PASS
CountPairsByLeafPaths：實際 = 0，結果 = PASS

總結：16/16 項驗證通過
```

## 專案結構

```text
leetcode_1530/
├── docs/
│   └── readme-template.md
├── leetcode_1530/
│   ├── leetcode_1530.csproj
│   └── Program.cs
├── leetcode_1530.sln
└── README.md
```

- `leetcode_1530/Program.cs`：`TreeNode`、兩種解法、固定案例與 console 驗證器。
- `leetcode_1530/leetcode_1530.csproj`：目標框架為 `net10.0` 的 console project。
- `docs/readme-template.md`：建立 README 時使用的結構與驗證指引。

本專案目前沒有獨立的自動化測試專案；固定 `Main` 案例是本專案的可執行驗收入口。

## 建置與執行

需求：

- .NET 10 SDK

請在此 README 所在的 repository root 執行：

```powershell
dotnet restore leetcode_1530/leetcode_1530.csproj
dotnet build leetcode_1530/leetcode_1530.csproj --nologo
dotnet run --project leetcode_1530/leetcode_1530.csproj --no-build
```

若只想直接執行，也可以省略 `--no-build`，讓 `dotnet run` 先確認專案是否需要
重新建置。建置完成後，預期會看到 `16/16 項驗證通過`。

### 格式檢查

從父 Git 根目錄執行：

```powershell
git -c safe.directory=C:/GitHubFolder/Leetcode_folder diff --check -- leetcode_1530
```

新建且尚未被 Git 索引的 README 仍應另外檢查行尾空白；這是 `git diff --check`
對未追蹤檔案的限制。
