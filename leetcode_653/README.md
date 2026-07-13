# 653. Two Sum IV - Input is a BST

[LeetCode 題目](https://leetcode.com/problems/two-sum-iv-input-is-a-bst/) · [力扣中文題目](https://leetcode.cn/problems/two-sum-iv-input-is-a-bst/)

給定一棵二元搜尋樹（BST）與目標和 `k`，判斷樹中是否存在**兩個不同節點**，其節點值相加剛好等於 `k`。本專案以 .NET 10 主控台程式實作，並將九個確定性案例放在 `Main` 的 acceptance harness 中。

## 題目與限制

樹中的節點可為正數、負數或零；演算法不能因為遇到同一個值就重複使用同一節點。題目的輸入是一棵有效 BST，但此解法只依賴「可走訪所有節點」，因此不需要使用 BST 的排序特性。

- 節點數量：`1` 到 `10^4`。
- 節點值：`-10^4` 到 `10^4`。
- `root` 是有效的二元搜尋樹。
- 目標值 `k`：`-10^5` 到 `10^5`。

公開 API 為 `public static bool FindTarget(TreeNode? root, int k)`。`TreeNode` 是巢狀 `public` 類別，左右子節點均為可為空的 `TreeNode?`；`FindTarget` 不輸出主控台、不修改輸入樹，並且不保留任何跨呼叫的解題狀態。

## 解法：區域 HashSet 的迭代 DFS

每次 `FindTarget` 呼叫都會新建一個 `HashSet<int>` 與一個 `Stack<TreeNode>`。堆疊負責迭代 DFS；集合只保存「已經彈出並處理過」的節點值。

不變量如下：在處理目前節點 `current` 前，`seenValues` 恰好包含先前處理過的節點值。因此先查詢 `k - current.Val` 是否已在集合內；若存在，兩個值來自不同節點，可立即回傳 `true`。若不存在，才把目前值加入集合，接著把非空的左右子節點推入堆疊。

「先查補數、再加入自己」是避免重複使用單一節點的關鍵。例如單節點 `[5]` 與 `k=10`，查詢 `5` 時集合尚為空，之後才加入 `5`，所以結果正確地為 `false`。集合與堆疊都是方法內區域變數，連續兩次呼叫也不會受到前一次的資料污染。

以 `[5,3,6,2,4,null,7]`、`k=9` 為例：

```plaintext
處理 5：seenValues = {}，尋找 4，未找到；加入 5。
處理 6：seenValues = {5}，尋找 3，未找到；加入 6。
處理 7：seenValues = {5,6}，尋找 2，未找到；加入 7。
處理 3：seenValues = {5,6,7}，尋找 6，找到；回傳 true。
```

## 為何使用迭代

遞迴 DFS 的時間與額外記憶體複雜度也能達到需求，但當 BST 完全右斜時，遞迴深度會等於節點數。本專案的第 8、9 案例會建立 10,000 個節點的右斜 BST；使用顯式 `Stack<TreeNode>` 可避免把這條深度交給呼叫堆疊。

- 時間複雜度：`O(n)`，每個節點最多走訪一次。
- 空間複雜度：`O(n)`，最壞情況下集合與堆疊合計保存線性數量的節點資料。

## 九項 acceptance checks

| 案例 | 輸入與 `k` | 預期 | 驗證目的 |
| --- | --- | --- | --- |
| 1 | `[5,3,6,2,4,null,7]`, `k=9` | `true` | 基本正向配對。 |
| 2 | `[5,3,6,2,4,null,7]`, `k=28` | `false` | 基本找不到配對。 |
| 3 | `[5]`, `k=10` | `false` | 不可重複使用唯一節點。 |
| 4 | `[-2,-3,-1]`, `k=-5` | `true` | 負數配對。 |
| 5 | `[0,-2,2]`, `k=0` | `true` | 零目標值與正負數配對。 |
| 6 | 第一個獨立呼叫 `[5]`, `k=8` | `false` | 建立不應遺留的前次集合內容。 |
| 7 | 緊接的新樹 `[3]`, `k=8` | `false` | 防止 static HashSet 污染：不能和案例 6 的 `5` 配對。 |
| 8 | 右斜 BST `[1..10000]`, `k=19999` | `true` | 上界規模與迭代 DFS；`9999 + 10000`。 |
| 9 | 同一棵右斜 BST `[1..10000]`, `k=20001` | `false` | 上界規模下的不存在配對。 |

每個案例都會印出案例/輸入標籤、預期值、實際值與 `PASS` 或 `FAIL`。若任一檢查失敗，程式會設定 `Environment.ExitCode = 1`；全部通過時最後一行必為 `Summary: 9/9 checks passed.`。

## 在本題目根目錄建置與執行

此資料夾沒有獨立 solution 檔或正式測試專案。請在 `leetcode_653/` 目錄執行明確的巢狀專案路徑；先建置，才可使用 `--no-build`。

```plaintext
dotnet build leetcode_653/leetcode_653.csproj --nologo
dotnet run --no-build --project leetcode_653/leetcode_653.csproj
```

VS Code 開啟 `leetcode_653/` 後，可選擇 `Debug leetcode_653`；它會先執行 `build leetcode_653`，再啟動 `net10.0` DLL。

## 已驗證的執行輸出

下列內容來自本次新鮮執行 `dotnet run --no-build --project leetcode_653/leetcode_653.csproj` 的逐字輸出：

```text
Case 1: [5,3,6,2,4,null,7], k=9 | Expected: True | Actual: True | PASS
Case 2: [5,3,6,2,4,null,7], k=28 | Expected: False | Actual: False | PASS
Case 3: [5], k=10 | Expected: False | Actual: False | PASS
Case 4: [-2,-3,-1], k=-5 | Expected: True | Actual: True | PASS
Case 5: [0,-2,2], k=0 | Expected: True | Actual: True | PASS
Case 6: first isolated [5], k=8 | Expected: False | Actual: False | PASS
Case 7: following new tree [3], k=8 | Expected: False | Actual: False | PASS
Case 8: right-skewed BST [1..10000], k=19999 | Expected: True | Actual: True | PASS
Case 9: same upper-bound BST [1..10000], k=20001 | Expected: False | Actual: False | PASS
Summary: 9/9 checks passed.
```

## 專案結構

```plaintext
leetcode_653/
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
└── leetcode_653/
    ├── leetcode_653.csproj
    └── Program.cs
```

舊式 `leetcode_653.sln`、`App.config` 與 `Properties/AssemblyInfo.cs` 已移除；SDK-style 專案只需要巢狀的 `leetcode_653.csproj` 與程式碼檔案。
