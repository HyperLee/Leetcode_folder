# LeetCode 2265 - Count Nodes Equal to Average of Subtree

## 統計值等於子樹平均值的節點數

- [English problem](https://leetcode.com/problems/count-nodes-equal-to-average-of-subtree/)
- [中文題目](https://leetcode.cn/problems/count-nodes-equal-to-average-of-subtree/)

給定一棵二元樹，對每個節點取其完整子樹（含自己）的所有節點值，以整數除法求平均；若該平均值等於目前節點值，就把此節點計入答案。

## 限制條件

- 樹的節點數 `n` 為 `1 <= n <= 1000`。
- 每個節點值為 `0 <= Node.val <= 1000`。
- 平均值採題目定義的整數除法，因此小數部分直接捨去。

## 核心不變量與解法

兩種方法都使用後序深度優先搜尋（postorder DFS）。處理目前節點以前，必須先完成左右子樹，才能取得目前完整子樹的總和與節點數，再依題目指定的整數除法判斷是否匹配。

### 方法一：tuple 後序彙總

`Traverse(node)` 的回傳值固定表示該節點完整子樹的 `(Sum, Count, Matches)`：

- 空子節點回傳 `(0, 0, 0)`。
- 非空節點先以後序取得左右子樹的彙總，再把目前值加入 `Sum`、把自己加入 `Count`。
- 此時 `Sum / Count` 正是目前節點子樹的整數平均；`Matches` 則是左右匹配數加上目前節點的判斷結果。

因此 `AverageOfSubtree` 不需要共享的計數欄位：每個子樹都把自己的匹配數交給父節點，根節點收到的 `Matches` 就是完整答案。這讓方法不修改樹、不輸出 Console，也不依賴跨呼叫狀態。

### 方法二：`int[]` 後序彙總

`Dfs2` 的完整簽名是：

```csharp
private static int[] Dfs2(TreeNode? node, ref int matches)
```

其中 `node` 是目前子樹的根節點；`TreeNode?` 表示遞迴也能接收空的左右子節點。`matches` 是由 `AverageOfSubtree2` 建立的區域計數器，使用 `ref` 傳入後，所有遞迴呼叫都會更新同一個變數。回傳型別 `int[]` 則只負責把目前子樹的總和與節點數交給父節點。

`Dfs2(node, ref matches)` 使用長度為二的陣列回傳子樹資訊，索引約定如下：

- `result[0]`：子樹節點值總和 `sum`。
- `result[1]`：子樹節點數 `count`。
- 空節點回傳 `[0, 0]`，因此父節點可以直接合併左右結果。

`AverageOfSubtree2` 在每次公開呼叫時先建立新的區域變數：

```csharp
int matches = 0;
Dfs2(root, ref matches);
return matches;
```

因此 `Dfs2` 增加的是本次公開呼叫的計數器，而不是類別欄位或全域狀態；重複呼叫 `AverageOfSubtree2` 時，每次都會從零開始。`int` 是值型別，若不使用 `ref`，遞迴收到的會是計數器副本，子樹內的 `matches++` 就無法累計回最外層。

#### `Dfs2` 的執行流程

1. `node` 是 `null` 時，回傳 `[0, 0]`，代表沒有總和、沒有節點，也不增加 `matches`。
2. 先呼叫 `Dfs2(node.left, ref matches)`，取得左子樹的 `[sum, count]`。
3. 再呼叫 `Dfs2(node.right, ref matches)`，取得右子樹的 `[sum, count]`。
4. 左右子樹都完成後，才合併目前節點：`sum = left[0] + right[0] + node.val`，`count = left[1] + right[1] + 1`。
5. 使用題目指定的整數除法檢查 `node.val == sum / count`；成立時執行一次 `matches++`。
6. 回傳 `[sum, count]` 給父節點。匹配數不放在陣列內，而是已經透過 `ref matches` 累計完成。

這個順序是後序走訪：左子樹 → 右子樹 → 目前節點。只有在左右結果都回來後，`sum / count` 才代表目前節點完整子樹的整數平均。由於非空節點一定會把自己算入 `count`，進入平均值判斷時 `count` 不會是零。

以官方樹 `[4,8,5,0,1,null,6]` 為例，後序順序是 `0 → 1 → 8 → 6 → 5 → 4`：節點 `0` 回傳 `[0, 1]` 並匹配，節點 `1` 回傳 `[1, 1]` 並匹配；節點 `8` 合併成 `[9, 3]`，整數平均為 `3`，不匹配；節點 `6` 匹配並回傳 `[6, 1]`；節點 `5` 合併成 `[11, 2]`，`11 / 2` 以整數除法為 `5`，因此匹配；最後根節點 `4` 得到 `[24, 6]`，平均為 `4`，也匹配，總數為 `5`。

`Dfs2` 完成後的核心不變量是：回傳陣列索引 0 永遠是目前子樹總和，索引 1 永遠是目前子樹節點數，而 `matches` 等於進入本次呼叫前的值加上目前子樹內的匹配節點數。函式只讀取 `node.val`、`node.left` 與 `node.right`，不修改原樹；額外成本為每個節點一次處理的 `O(n)` 時間與 `O(h)` 遞迴呼叫堆疊，另有每個非空節點建立長度二陣列的 `O(n)` 總配置量。

方法二與方法一的核心數學不變量相同，差異在於資料承載方式：方法一把總和、節點數、匹配數都放在具名 tuple 中回傳；方法二只回傳總和與節點數，把匹配數集中由區域計數器累計。方法一的欄位名稱較直接，方法二則示範如何用固定索引陣列傳遞子樹摘要。

相較於每個節點重新掃描其整棵子樹的 `O(n²)` 作法，兩種方法都只走訪每個節點一次。題目最多有 1000 個節點，後序彙總能避免重複計算；代價是遞迴呼叫堆疊深度會隨樹高變化。由於節點值最多為 1000，總和上限為 `1,000,000`，使用 `int` 足以保存題目範圍內的彙總值。

### 複雜度比較

| 項目 | 複雜度 |
| --- | --- |
| 方法一時間 | `O(n)` |
| 方法一額外空間 | `O(h)` 遞迴呼叫堆疊，`h` 為樹高；tuple 為值回傳 |
| 方法二時間 | `O(n)` |
| 方法二額外空間 | `O(h)` 遞迴呼叫堆疊，且每個非空節點建立一個長度二陣列，總配置量為 `O(n)` |

## 逐步走查

以官方樹 `[4,8,5,0,1,null,6]` 為例，後序處理順序是 `0 → 1 → 8 → 6 → 5 → 4`：

| 處理節點 | 子樹總和 `sum` | 節點數 `count` | 整數平均 `sum / count` | 是否匹配 |
| --- | ---: | ---: | ---: | --- |
| `0` | `0` | `1` | `0` | 是 |
| `1` | `1` | `1` | `1` | 是 |
| `8` | `9` | `3` | `3` | 否 |
| `6` | `6` | `1` | `6` | 是 |
| `5` | `11` | `2` | `5` | 是 |
| `4` | `24` | `6` | `4` | 是 |

方法一會在每一列同時回傳這一列的 `sum`、`count` 與累計 `Matches`；方法二則回傳 `[sum, count]`，並在「是否匹配」為是時更新區域 `matches`。因此兩者都會得到葉節點 `0、1、6`、節點 `5` 與根節點 `4` 共 5 個匹配，且不需要修改原樹。

## Acceptance harness

`Main` 是唯一 Console I/O 邊界，以下九項皆以手算 literal expected value 驗證；每個案例同時比較方法一與方法二，通常以「方法一結果；方法二結果」呈現：

| # | 案例 | 預期（方法一；方法二） | 驗證目的 |
| --- | --- | --- | --- |
| 1 | `[4,8,5,0,1,null,6]` | `5; 5` | 官方範例與一般分支 |
| 2 | `[1]` | `1; 1` | 最小有效樹 |
| 3 | `[2,1,4]` | `3; 3` | 根節點等於截斷平均 |
| 4 | `[9,1,1]` | `2; 2` | 根節點不等於平均 |
| 5 | `[0,0,0]` | `3; 3` | 零值邊界 |
| 6 | `[3,null,1,null,0]` | `1; 1` | 不對稱右斜子樹 |
| 7 | 同一官方樹呼叫兩次 | `(5, 5); (5, 5)` | 防止兩種方法的跨呼叫狀態累積 |
| 8 | `[2,1]` 呼叫前後節點快照 | `1; 1; True` | 同時驗證 `3 / 2` 截斷為 `1`、兩方法結果一致，以及值與左右物件參考均維持不變 |
| 9 | 1000 個零值右斜節點 | `1000; 1000` | 題目上限 spot check，並比較兩方法 |

## 建置與執行

已從 repository 根目錄實際驗證：

```bash
dotnet build leetcode_2265/leetcode_2265/leetcode_2265.csproj --nologo
dotnet run --no-build --project leetcode_2265/leetcode_2265/leetcode_2265.csproj
```

若直接開啟題目根目錄 `leetcode_2265/`，使用：

```bash
dotnet build leetcode_2265/leetcode_2265.csproj --nologo
dotnet run --no-build --project leetcode_2265/leetcode_2265.csproj
```

以下為 fresh run 的完整輸出：

```text
Case: Official example; Input: [4,8,5,0,1,null,6]
Expected: 5; 5
Actual: 5; 5
PASS
Case: Single node; Input: [1]
Expected: 1; 1
Actual: 1; 1
PASS
Case: Root equals truncated average; Input: [2,1,4]
Expected: 3; 3
Actual: 3; 3
PASS
Case: Root does not equal average; Input: [9,1,1]
Expected: 2; 2
Actual: 2; 2
PASS
Case: All zeroes; Input: [0,0,0]
Expected: 3; 3
Actual: 3; 3
PASS
Case: Right-skewed mixed values; Input: [3,null,1,null,0]
Expected: 1; 1
Actual: 1; 1
PASS
Case: Repeated call on same official tree; Input: same [4,8,5,0,1,null,6] instance
Expected: (5, 5); (5, 5)
Actual: (5, 5); (5, 5)
PASS
Case: Truncating average and tree topology preservation; Input: snapshot [2,1]
Expected: 1; 1; True
Actual: 1; 1; True
PASS
Case: Right-skewed limit spot check; Input: 1000 zero-valued nodes
Expected: 1000; 1000
Actual: 1000; 1000
PASS
Summary: 9/9 checks passed.
```

## 舊版檔案整理

已逐檔移除舊式 `leetcode_2265.sln`、`App.config` 與 `Properties/AssemblyInfo.cs`。SDK-style `net10.0` 專案由 `leetcode_2265.csproj` 集中管理組件資訊與建置設定，因此不保留這些 .NET Framework 產物。

## 專案結構

```plaintext
leetcode_2265/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_2265/
    ├── Program.cs
    └── leetcode_2265.csproj
```
