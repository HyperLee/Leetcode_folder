# LeetCode 297 — 二元樹的序列化與反序列化

![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![LeetCode Hard](https://img.shields.io/badge/LeetCode-Hard-ef4743)

這是一個以 .NET 10 console project 實作的教學範例。程式使用 DFS 前序走訪，把二元樹轉換成可保存或傳輸的字串，再從相同格式完整重建原始樹形。

- [題目說明](#題目說明)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：前序 DFS 與 `null` 標記](#解法一前序-dfs-與-null-標記)
- [範例演示流程](#範例演示流程)
- [建置與執行](#建置與執行)

## 題目說明

設計一組序列化與反序列化方法：

- `serialize` 將二元樹轉換成單一字串。
- `deserialize` 將該字串還原成原本的二元樹結構。
- 題目不限制編碼格式，但同一套格式必須能無損往返。

例如，輸入樹的層序表示為 `[1,2,3,null,null,4,5]` 時，經過序列化再反序列化後，節點值、左右子樹位置與所有空分支都必須保持一致。

題目連結：[LeetCode 297](https://leetcode.com/problems/serialize-and-deserialize-binary-tree/)

### 限制條件

- 樹中的節點數介於 `0` 與 `10^4`。
- `-1000 <= Node.val <= 1000`。
- 空樹也是合法輸入。

## 解題概念與出發點

只記錄節點值不足以還原樹形。例如，一個值為 `1` 的根節點，其子節點 `2` 位於左側或右側時，如果省略空分支，兩棵不同的樹都可能得到 `1,2`。

本實作使用兩個互相配合的決定：

1. **前序走訪（根 → 左 → 右）**：讀到非空 token 時，可以立即建立目前節點，接著遞迴處理其左、右子樹。
2. **明確記錄 `null`**：每個缺少的子節點都會占用一個 token，因此序列同時保存節點值與樹形。

這使序列具有明確的解析規則：非空 token 一定代表一個節點，並要求後續序列依序描述它的左子樹與右子樹；`null` token 則立即結束目前分支。

## 解法一：前序 DFS 與 `null` 標記

目前專案只有這一種解法。公開方法名稱維持 LeetCode 題目要求的 `serialize` 與 `deserialize`。

### 資料格式

- 節點值直接轉成十進位字串。
- 空節點記為 `null`。
- token 之間以逗號分隔。
- 空樹的完整序列是 `null`。

一般範例樹會得到：

`1,2,null,null,3,4,null,null,5,null,null`

### 序列化設計

`serialize(TreeNode? root)` 建立 token 清單，再交給遞迴 helper：

1. 目前節點為 `null` 時，加入 `null` 並結束這個分支。
2. 否則先加入目前節點值。
3. 依序序列化左子樹與右子樹。
4. 最後以逗號串接全部 token。

演算法不需要額外儲存節點關係；遞迴呼叫順序本身就是樹的結構，而 `null` token 負責保留每個分支的終點。

### 反序列化設計

`deserialize(string data)` 先按逗號切割字串並放入 `Queue<string>`。遞迴 helper 每次取出佇列前端 token：

1. token 是 `null` 時，回傳空節點。
2. 否則建立具有該值的新節點。
3. 下一段 token 必定描述左子樹，因此先遞迴建立 `left`。
4. 左子樹完成後，再以後續 token 遞迴建立 `right`。
5. 回傳完成的目前節點。

Queue 保證每個 token 只被取出一次，也讓反序列化的消耗順序與序列化完全對稱。

### 正確性要點

- 每個非空節點都輸出一次自身值。
- 每個空分支都輸出一次 `null`，所以不會遺失左右方向。
- 反序列化遇到節點值時，固定依序重建左、右子樹。
- 因為兩個方向使用相同的前序規則，每個 token 都有唯一用途，重建結果與原樹一致。

### 複雜度

令 `n` 為非空節點數，`h` 為樹高：

| 操作 | 時間複雜度 | 輔助空間 | 其他輸出或結果空間 |
| --- | --- | --- | --- |
| 序列化 | `O(n)` | 遞迴堆疊 `O(h)` | token 與結果字串 `O(n)` |
| 反序列化 | `O(n)` | Queue `O(n)`、遞迴堆疊 `O(h)` | 重建的樹 `O(n)` |

最壞情況是不平衡鏈狀樹，此時 `h = n`；平衡樹則約為 `h = log n`。

## 範例演示流程

### 一般樹

範例樹的結構為：

- 根節點 `1`
  - 左節點 `2`
  - 右節點 `3`
    - 左節點 `4`
    - 右節點 `5`

### 序列化逐步流程

| 步驟 | 目前位置 | 寫入 token | 累積序列 |
| ---: | --- | --- | --- |
| 1 | 根節點 | `1` | `1` |
| 2 | `1` 的左節點 | `2` | `1,2` |
| 3 | `2` 的左節點 | `null` | `1,2,null` |
| 4 | `2` 的右節點 | `null` | `1,2,null,null` |
| 5 | `1` 的右節點 | `3` | `1,2,null,null,3` |
| 6 | `3` 的左節點 | `4` | `1,2,null,null,3,4` |
| 7 | `4` 的左節點 | `null` | `1,2,null,null,3,4,null` |
| 8 | `4` 的右節點 | `null` | `1,2,null,null,3,4,null,null` |
| 9 | `3` 的右節點 | `5` | `1,2,null,null,3,4,null,null,5` |
| 10 | `5` 的左節點 | `null` | `1,2,null,null,3,4,null,null,5,null` |
| 11 | `5` 的右節點 | `null` | `1,2,null,null,3,4,null,null,5,null,null` |

### 反序列化逐步流程

| 取出 token | 動作 | 所屬位置 |
| --- | --- | --- |
| `1` | 建立節點 `1` | 根節點 |
| `2` | 建立節點 `2` | `1.left` |
| `null` | 左分支結束 | `2.left` |
| `null` | 右分支結束，節點 `2` 完成 | `2.right` |
| `3` | 建立節點 `3` | `1.right` |
| `4` | 建立節點 `4` | `3.left` |
| `null`, `null` | 兩個分支結束，節點 `4` 完成 | `4.left`, `4.right` |
| `5` | 建立節點 `5` | `3.right` |
| `null`, `null` | 兩個分支結束，節點 `5` 完成 | `5.left`, `5.right` |

佇列耗盡時，節點 `3` 與根節點 `1` 也依序完成，得到與輸入相同的樹。

### 可執行案例

| 案例 | 驗證目的 | 預期序列 |
| --- | --- | --- |
| 一般二元樹 | 驗證左右子樹與多層結構 | `1,2,null,null,3,4,null,null,5,null,null` |
| 空樹 | 驗證 nullable 根節點 | `null` |
| 單一節點 | 驗證兩個空子節點都被保存 | `1,null,null` |
| 含負值的不平衡樹 | 驗證負值及非對稱結構 | `-1,-2,null,3,null,null,null` |
| 重複值樹 | 驗證樹形不依賴節點值唯一性 | `7,7,null,null,7,null,null` |

每個案例同時比對首次序列化結果，以及反序列化後再次序列化的往返結果。

## 建置與執行

請從此 repository 根目錄執行：

```bash
dotnet restore leetcode_297/leetcode_297.csproj
dotnet build leetcode_297/leetcode_297.csproj --nologo --no-restore
dotnet run --project leetcode_297/leetcode_297.csproj --no-build
```

### 實際執行結果

```text
案例 1：一般二元樹
預期序列化：1,2,null,null,3,4,null,null,5,null,null
實際序列化：1,2,null,null,3,4,null,null,5,null,null
往返序列化：1,2,null,null,3,4,null,null,5,null,null
結果：PASS

案例 2：空樹
預期序列化：null
實際序列化：null
往返序列化：null
結果：PASS

案例 3：單一節點
預期序列化：1,null,null
實際序列化：1,null,null
往返序列化：1,null,null
結果：PASS

案例 4：含負值的不平衡樹
預期序列化：-1,-2,null,3,null,null,null
實際序列化：-1,-2,null,3,null,null,null
往返序列化：-1,-2,null,3,null,null,null
結果：PASS

案例 5：重複值樹
預期序列化：7,7,null,null,7,null,null
實際序列化：7,7,null,null,7,null,null
往返序列化：7,7,null,null,7,null,null
結果：PASS

總結：5/5 筆測試通過
```

## 驗證方式

目前沒有獨立的自動化測試專案。此教學範例以以下項目作為驗收：

- `dotnet build` 必須為 0 個錯誤、0 個警告。
- console harness 的 5 個案例必須全部顯示 `PASS`。
- README 記錄的完整輸出必須與 fresh `dotnet run` 結果一致。
- `git diff --check` 必須通過。

## 專案結構

- `leetcode_297/Program.cs`：進入點、可執行案例、二元樹模型與 Codec 實作。
- `leetcode_297/leetcode_297.csproj`：目標為 `net10.0` 的 console project。
- `docs/readme-template.md`：首次建立 README 時使用的內容與驗證指引。
- `README.md`：題目、演算法設計、演示流程與實際執行結果。
