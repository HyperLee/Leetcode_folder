# 1372. Longest ZigZag Path in a Binary Tree（二元樹中的最長交錯路徑）

[LeetCode 題目](https://leetcode.com/problems/longest-zigzag-path-in-a-binary-tree/) · [力扣中文題目](https://leetcode.cn/problems/longest-zigzag-path-in-a-binary-tree/)

給定一棵二元樹，可以從任意節點選擇第一步向左或向右，之後每一步都必須交替方向。
路徑長度以經過的**邊數**計算，因此單一節點的長度是 `0`。本專案使用 .NET 10
主控台程式與迭代 DFS，並由 `Main` 執行九個確定性 acceptance checks。

## 題目與限制

公開 API 為 `public int LongestZigZag(TreeNode? root)`。`TreeNode` 保留 LeetCode
使用的 `val`、`left`、`right` 公開欄位；解法不輸出、不改寫輸入樹，也不保存跨
呼叫狀態。

- 節點數量：`1` 到 `50,000`。
- 節點值：`1` 到 `100`。
- 題目有效輸入至少有一個節點；為了與舊程式相容，實作對 `null` 根節點回傳 `0`。

## 解法：保存上一條邊方向的迭代 DFS

stack 中每個狀態包含目前節點、上一條邊是否向左，以及目前交錯路徑長度。若上一條
邊向左，下一條向右便可把長度加一；若下一條仍向左，原本交錯路徑已中斷，但這條左
邊可以成為新路徑的第一步，因此長度重設為 `1`。上一條邊向右時使用完全對稱的規則。

例如某個 root 下方存在 `2 -> 3 -> 4 -> 5`，方向依序為左、右、左。即使 root 到
節點 `2` 的前一條邊也是向左，走到 `3` 時會把長度重設為 `1`，之後向右、向左延續
到 `3`；因此演算法能找到從任意節點開始，而不一定從 root 開始的最佳路徑。

迭代方式不會受到 50,000 節點深鏈的函式呼叫堆疊限制，也不需要舊解法的 `maxAns`
instance 欄位。

- 時間複雜度：`O(n)`；root 用來初始化走訪，而每個非 root 節點恰好進出 stack 一次。
- 結果空間複雜度：`O(1)`，只回傳一個整數。
- 輔助空間複雜度：最壞 `O(h)`，其中 `h` 是樹高；stack 保存尚待走訪的 DFS 狀態。

## 九項 acceptance checks

| 案例 | 預期 | 驗證目的 |
| --- | ---: | --- |
| 官方範例一 | `3` | 官方基本交錯路徑。 |
| 官方範例二 | `4` | 較長的左、右交替路徑。 |
| 單節點 `[1]` | `0` | 最小有效輸入與 edge-count 定義。 |
| 單一左邊 | `1` | 第一條邊可向左。 |
| 單一右邊 | `1` | 第一條邊可向右。 |
| 最佳路徑從 root 下方開始 | `3` | 同方向時必須重設，而非丟失新的起點。 |
| 50,000 節點左鏈 | `1` | 上限深度、非遞迴與連續同方向重設。 |
| 50,000 節點完整交錯鏈 | `49,999` | 上限長度與每一步方向延續。 |
| 大型案例後再算單節點 | `0` | 同一 solution 實體不得累積狀態。 |

每個案例都印出名稱、Expected、Actual 與 PASS/FAIL；任一失敗會設定
`Environment.ExitCode = 1`。

## 在本題目根目錄建置與執行

本資料夾沒有獨立 solution 或正式測試專案。請在外層 `leetcode_1372/` 內使用明確
的巢狀 project 路徑：

```bash
dotnet build leetcode_1372/leetcode_1372.csproj --nologo
dotnet run --no-build --project leetcode_1372/leetcode_1372.csproj
```

VS Code 開啟外層 `leetcode_1372/` 後，選擇 `Debug leetcode_1372`；它會先執行
`build leetcode_1372`，再啟動 .NET 10 DLL。

## 已驗證的執行輸出

下列內容來自新鮮執行 `dotnet run --no-build --project leetcode_1372/leetcode_1372.csproj`：

```text
Case 1: official example 1 | Expected: 3 | Actual: 3 | PASS
Case 2: official example 2 | Expected: 4 | Actual: 4 | PASS
Case 3: single node [1] | Expected: 0 | Actual: 0 | PASS
Case 4: one left edge [1,2] | Expected: 1 | Actual: 1 | PASS
Case 5: one right edge [1,null,2] | Expected: 1 | Actual: 1 | PASS
Case 6: best path starts below root | Expected: 3 | Actual: 3 | PASS
Case 7: 50000-node left chain | Expected: 1 | Actual: 1 | PASS
Case 8: 50000-node alternating chain | Expected: 49999 | Actual: 49999 | PASS
Case 9: same instance after large cases | Expected: 0 | Actual: 0 | PASS
Summary: 9/9 checks passed.
```

## 專案結構

```plaintext
leetcode_1372/
├── .editorconfig
├── .gitattributes
├── .gitignore
├── .vscode/
│   ├── launch.json
│   └── tasks.json
├── AGENTS.md
├── README.md
├── docs/
│   ├── readme-template.md
│   └── superpowers/
│       ├── plans/
│       └── specs/
└── leetcode_1372/
    ├── leetcode_1372.csproj
    └── Program.cs
```

舊式 `leetcode_1372.sln`、`App.config` 與 `Properties/AssemblyInfo.cs` 已移除。
