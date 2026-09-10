# LeetCode 2265 - Count Nodes Equal to Average of Subtree

## 統計值等於子樹平均值的節點數

- [English problem](https://leetcode.com/problems/count-nodes-equal-to-average-of-subtree/)
- [中文題目](https://leetcode.cn/problems/count-nodes-equal-to-average-of-subtree/)

> 這題的關鍵是：先用後序走訪完成每個子樹的總和與節點數，再用整數除法判斷目前節點是否等於該子樹的平均值。

## 閱讀導覽

- [題目與規格](#1-題目與規格)
- [快速開始](#2-快速開始)
- [解法總覽](#3-解法總覽)
- [方法一：tuple 後序彙總](#4-方法一tuple-後序彙總)
- [方法二：`int[]` 後序彙總](#5-方法二int-後序彙總)
- [官方範例走查](#6-官方範例走查)
- [複雜度比較](#7-複雜度比較)
- [Acceptance harness 與實際輸出](#8-acceptance-harness-與實際輸出)
- [專案資訊](#9-專案資訊)

## 1. 題目與規格

### 題意

給定一棵二元樹，對每個節點取其完整子樹（含自己）的所有節點值，以整數除法求平均；若該平均值等於目前節點值，就把此節點計入答案。

### 限制條件

- 樹的節點數 `n` 為 `1 <= n <= 1000`。
- 每個節點值為 `0 <= Node.val <= 1000`。
- 平均值採題目定義的整數除法，因此小數部分直接捨去。

## 2. 快速開始

這是一個巢狀的 .NET 10 主控台專案。請先建置，再使用 `--no-build` 執行。

### 從 repository 根目錄執行

```bash
dotnet build leetcode_2265/leetcode_2265/leetcode_2265.csproj --nologo
dotnet run --no-build --project leetcode_2265/leetcode_2265/leetcode_2265.csproj
```

### 直接位於題目根目錄執行

```bash
dotnet build leetcode_2265.csproj --nologo
dotnet run --no-build --project leetcode_2265.csproj
```

成功時最後一行應為：

```text
Summary: 9/9 checks passed.
```

## 3. 解法總覽

### 共同核心：後序彙總

兩種方法都使用後序深度優先搜尋（postorder DFS）：

1. 先完成左子樹。
2. 再完成右子樹。
3. 合併左右子樹的總和與節點數。
4. 使用 `sum / count` 判斷目前節點是否匹配。

只有在左右子樹都完成後，才取得目前節點完整子樹的資料，因此每個節點只需要處理一次。

### 兩種方法的差異

| 方法 | 公開方法 | 子樹回傳資料 | `matches` 的承載方式 | 教學重點 |
| --- | --- | --- | --- | --- |
| 方法一 | `AverageOfSubtree` | `(Sum, Count, Matches)` | 隨 tuple 一起往父節點回傳 | 具名欄位直接表達子樹摘要 |
| 方法二 | `AverageOfSubtree2` | `[sum, count]` | 由區域 `ref int matches` 累計 | 固定索引陣列與區域參考參數 |

兩種方法都符合以下行為契約：

- 不輸出主控台。
- 不修改原始樹的節點值或左右子節點參考。
- 不依賴跨呼叫的可變全域或類別狀態。
- 每個節點只走訪一次，時間複雜度為 `O(n)`。

## 4. 方法一：tuple 後序彙總

### 狀態定義

`Traverse(node)` 的回傳值固定表示該節點完整子樹的 `(Sum, Count, Matches)`：

| 情況 | 回傳值 | 意義 |
| --- | --- | --- |
| `node` 為空 | `(0, 0, 0)` | 沒有總和、沒有節點、沒有匹配 |
| `node` 非空 | 合併左右子樹後的 tuple | 回傳完整子樹摘要給父節點 |

### 執行流程

1. 空子節點直接回傳 `(0, 0, 0)`。
2. 以後序順序取得左右子樹的彙總。
3. 將目前節點值加入 `Sum`，並把目前節點加入 `Count`。
4. 使用 `Sum / Count` 計算整數平均。
5. 將目前節點的判斷結果加到左右子樹的 `Matches`。
6. 回傳新的 `(Sum, Count, Matches)`。

因此 `AverageOfSubtree` 不需要共享的計數欄位：每個子樹都把自己的匹配數交給父節點，根節點收到的 `Matches` 就是完整答案。

### 核心不變量

對每個已完成的 `Traverse(node)` 呼叫：

- `Sum` 等於目前子樹所有節點值的總和。
- `Count` 等於目前子樹的節點數。
- `Matches` 等於目前子樹中符合條件的節點數。

## 5. 方法二：`int[]` 後序彙總

### 方法簽名與狀態分工

`Dfs2` 的完整簽名是：

```csharp
private static int[] Dfs2(TreeNode? node, ref int matches)
```

- `node` 是目前子樹的根節點；`TreeNode?` 表示遞迴也能接收空的左右子節點。
- `matches` 是由 `AverageOfSubtree2` 建立的區域計數器，使用 `ref` 傳入後，所有遞迴呼叫都會更新同一個變數。
- 回傳型別 `int[]` 只負責把目前子樹的總和與節點數交給父節點。

陣列索引約定如下：

| 索引 | 資料 | 說明 |
| --- | --- | --- |
| `result[0]` | `sum` | 子樹節點值總和 |
| `result[1]` | `count` | 子樹節點數 |

空節點回傳 `[0, 0]`，因此父節點可以直接合併左右結果。

`AverageOfSubtree2` 在每次公開呼叫時先建立新的區域變數：

```csharp
int matches = 0;
Dfs2(root, ref matches);
return matches;
```

因此 `Dfs2` 使用的是本次公開呼叫的計數器，而不是類別欄位或全域狀態；重複呼叫 `AverageOfSubtree2` 時，每次都會從零開始。`int` 是值型別，若不使用 `ref`，遞迴收到的會是計數器副本，子樹內的 `matches++` 就無法累計回最外層。

### `Dfs2` 的執行流程

1. `node` 是 `null` 時，回傳 `[0, 0]`，不增加 `matches`。
2. 呼叫 `Dfs2(node.left, ref matches)`，取得左子樹的 `[sum, count]`。
3. 呼叫 `Dfs2(node.right, ref matches)`，取得右子樹的 `[sum, count]`。
4. 左右子樹都完成後，合併目前節點：`sum = left[0] + right[0] + node.val`，`count = left[1] + right[1] + 1`。
5. 使用題目指定的整數除法檢查 `node.val == sum / count`；成立時執行一次 `matches++`。
6. 回傳 `[sum, count]` 給父節點。匹配數不放在陣列內，而是已經透過 `ref matches` 累計完成。

這個順序是後序走訪：左子樹 → 右子樹 → 目前節點。由於非空節點一定會把自己算入 `count`，進入平均值判斷時 `count` 不會是零。

### 核心不變量與成本

`Dfs2` 完成後：

- 回傳陣列索引 0 永遠是目前子樹總和。
- 回傳陣列索引 1 永遠是目前子樹節點數。
- `matches` 等於進入本次呼叫前的值，加上目前子樹內的匹配節點數。

函式只讀取 `node.val`、`node.left` 與 `node.right`，不修改原樹；額外成本為 `O(n)` 時間與 `O(h)` 遞迴呼叫堆疊，另有每個非空節點建立長度二陣列的 `O(n)` 總配置量。

方法二與方法一的核心數學不變量相同，差異只在資料承載方式：方法一把總和、節點數、匹配數都放在具名 tuple 中回傳；方法二只回傳總和與節點數，把匹配數集中由區域計數器累計。

## 6. 官方範例走查

以官方樹 `[4,8,5,0,1,null,6]` 為例，後序處理順序是 `0 → 1 → 8 → 6 → 5 → 4`：

| 處理節點 | 子樹總和 `sum` | 節點數 `count` | 整數平均 `sum / count` | 是否匹配 |
| --- | ---: | ---: | ---: | --- |
| `0` | `0` | `1` | `0` | 是 |
| `1` | `1` | `1` | `1` | 是 |
| `8` | `9` | `3` | `3` | 否 |
| `6` | `6` | `1` | `6` | 是 |
| `5` | `11` | `2` | `5` | 是 |
| `4` | `24` | `6` | `4` | 是 |

方法一會在每一列同時回傳該列的 `sum`、`count` 與累計 `Matches`；方法二則回傳 `[sum, count]`，並在「是否匹配」為是時更新區域 `matches`。因此兩者都會得到葉節點 `0、1、6`、節點 `5` 與根節點 `4` 共 5 個匹配，且不需要修改原樹。

## 7. 複雜度比較

令 `n` 為節點數，`h` 為樹高：

| 項目 | 時間 | 額外空間 |
| --- | ---: | --- |
| 方法一 | `O(n)` | `O(h)` 遞迴呼叫堆疊；tuple 為值回傳 |
| 方法二 | `O(n)` | `O(h)` 遞迴呼叫堆疊，另有每個非空節點的長度二陣列配置；總配置量為 `O(n)` |
| 每個節點重新掃描子樹的作法 | `O(n²)` | 依實作而定 |

題目最多有 1000 個節點，後序彙總能避免重複計算；代價是遞迴呼叫堆疊深度會隨樹高變化。由於節點值最多為 1000，總和上限為 `1,000,000`，使用 `int` 足以保存題目範圍內的彙總值。

## 8. Acceptance harness 與實際輸出

`Main` 是唯一 Console I/O 邊界，以下九項皆以手算 literal expected value 驗證；每個案例同時比較方法一與方法二，通常以「方法一結果；方法二結果」呈現。任一案例失敗時，程式會將 exit code 設為 `1`。

### 九項固定案例

| # | 案例 | 預期（方法一；方法二） | 驗證目的 |
| --- | --- | --- | --- |
| 1 | `[4,8,5,0,1,null,6]` | `5; 5` | 官方範例與一般分支 |
| 2 | `[1]` | `1; 1` | 最小有效樹 |
| 3 | `[2,1,4]` | `3; 3` | 根節點等於截斷平均 |
| 4 | `[9,1,1]` | `2; 2` | 根節點不等於平均 |
| 5 | `[0,0,0]` | `3; 3` | 零值邊界 |
| 6 | `[3,null,1,null,0]` | `1; 1` | 不對稱右斜子樹 |
| 7 | 同一官方樹呼叫兩次 | `(5, 5); (5, 5)` | 防止兩種方法的跨呼叫狀態累積 |
| 8 | `[2,1]` 呼叫前後節點快照 | `1; 1; True` | 驗證 `3 / 2` 截斷為 `1`、兩方法結果一致，以及值與左右物件參考均維持不變 |
| 9 | 1000 個零值右斜節點 | `1000; 1000` | 題目上限 spot check，並比較兩方法 |

### Fresh run 完整輸出

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

## 9. 專案資訊

### 舊版檔案整理

已逐檔移除舊式 `leetcode_2265.sln`、`App.config` 與 `Properties/AssemblyInfo.cs`。SDK-style `net10.0` 專案由 `leetcode_2265.csproj` 集中管理組件資訊與建置設定，因此不保留這些 .NET Framework 產物。

### 專案結構

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
