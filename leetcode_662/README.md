# 662. Maximum Width of Binary Tree（二元樹的最大寬度）

[LeetCode 題目](https://leetcode.com/problems/maximum-width-of-binary-tree/) · [力扣中文題目](https://leetcode.cn/problems/maximum-width-of-binary-tree/)

給定一棵二元樹，找出所有層級中的**最大寬度**。某一層的寬度不只是非空節點數量；若最左與最右非空節點之間在完整二元樹中會有 `null` 節點，那些位置也必須計入。本專案以 .NET 10 主控台程式實作，並在 `Main` 提供八個確定性的 acceptance checks。

## 題目與限制

公開 API 為 `public int WidthOfBinaryTree(TreeNode? root)`。`TreeNode` 保留 LeetCode 使用的 `val`、`left`、`right` 公開欄位；解法不輸出主控台、不改寫輸入樹，也不保存跨呼叫狀態。

- 節點數量：`1` 到 `3,000`。
- 節點值：`-100` 到 `100`。
- 題目保證答案在 32 位元 signed integer 範圍內。

雖然有效題目輸入至少有一個節點，實作對 `null` 根節點回傳 `0`，與舊程式的遞迴 base case 相容。

## 解法：逐層正規化索引的 BFS

把樹視為完整二元樹並以位置編號：某節點位置為 `p` 時，左子節點為 `2p`，右子節點為 `2p + 1`。同一層若首尾非空節點位置為 `first`、`last`，寬度就是 `last - first + 1`，因此自然會保留中間缺口。

直接把根節點一路編號到深層會讓位置快速成長。本實作在每一層開始時記住最左位置，讓該層每個節點改用 `position - levelFirstPosition`；位置差不變，所以計算出的寬度不變，但下一層的子節點位置會從很小的數字重新開始。暫存位置使用 `long`，最後依題目保證轉為 `int`。

以 `[1,3,2,5,null,null,9,6,null,7]` 為例：第三層的 `5`、`9` 在完整樹位置相差三格。下一層先把 `5` 視為位置 `0`、`9` 視為位置 `3`，其子節點 `6`、`7` 便位於 `0`、`6`；因此寬度為 `6 - 0 + 1 = 7`，正確計入中間六個位置中的空缺。

- 時間複雜度：`O(n)`，每個節點恰好出隊一次。
- 空間複雜度：`O(n)`，最壞情況下 queue 可包含某一整層節點。

## 八項 acceptance checks

| 案例 | 輸入 | 預期 | 驗證目的 |
| --- | --- | --- | --- |
| 1 | `[1,3,2,5,3,null,9]` | `4` | 官方範例的基本缺口計數。 |
| 2 | `[1,3,2,5,null,null,9,6,null,7]` | `7` | 官方範例的跨多個 null 缺口。 |
| 3 | `[1,3,2,5]` | `2` | 官方範例的較淺樹。 |
| 4 | `[1]` | `1` | 最小有效輸入。 |
| 5 | 3,000 節點左鏈 | `1` | 題目上限深度；不可依賴遞迴深度。 |
| 6 | 深度 3 的左右極端路徑 | `8` | 兩端位置與中間空缺的完整樹寬度。 |
| 7 | 同一個 `Program` 實體的寬樹 | `4` | 解法不可累積先前呼叫的層級狀態。 |
| 8 | 同一實體接續單節點 `[42]` | `1` | 連續呼叫的狀態隔離。 |

每個案例都會印出標籤、預期、實際與 PASS/FAIL；任一失敗會設定 `Environment.ExitCode = 1`。

## 在本題目根目錄建置與執行

此資料夾沒有獨立 solution 或正式測試專案。請在 `leetcode_662/` 內使用明確的巢狀 project 路徑：

```plaintext
dotnet build leetcode_662/leetcode_662.csproj --nologo
dotnet run --no-build --project leetcode_662/leetcode_662.csproj
```

VS Code 開啟 `leetcode_662/` 後，選擇 `Debug leetcode_662`；它會先執行 `build leetcode_662`，再啟動 .NET 10 DLL。

## 已驗證的執行輸出

下列內容來自本次新鮮執行 `dotnet run --no-build --project leetcode_662/leetcode_662.csproj` 的逐字輸出：

```text
Case 1: [1,3,2,5,3,null,9] | Expected: 4 | Actual: 4 | PASS
Case 2: [1,3,2,5,null,null,9,6,null,7] | Expected: 7 | Actual: 7 | PASS
Case 3: [1,3,2,5] | Expected: 2 | Actual: 2 | PASS
Case 4: single node [1] | Expected: 1 | Actual: 1 | PASS
Case 5: 3000-node left chain | Expected: 1 | Actual: 1 | PASS
Case 6: sparse extreme paths at depth 3 | Expected: 8 | Actual: 8 | PASS
Case 7: same instance wide tree | Expected: 4 | Actual: 4 | PASS
Case 8: same instance then single node | Expected: 1 | Actual: 1 | PASS
Summary: 8/8 checks passed.
```

## 專案結構

```plaintext
leetcode_662/
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
└── leetcode_662/
    ├── leetcode_662.csproj
    └── Program.cs
```

舊式 `leetcode_662.sln`、`App.config` 與 `Properties/AssemblyInfo.cs` 已移除；SDK-style 專案只保留必要的 `.csproj` 與程式碼檔案。
