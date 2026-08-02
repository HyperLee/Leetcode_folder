# LeetCode 1460 教學刷新設計

## 目標

在不改變既有兩種解法 API 與核心行為的前提下，讓 `leetcode_1460` 具備可直接執行的教學案例、完整的繁體中文文件，以及可由實際命令驗證的 README。

## 範圍與限制

- 保留 `CanBeEqual(int[] target, int[] arr)` 的原地排序行為；它可以修改傳入的陣列。
- 保留 `CanBeEqual2(int[] target, int[] arr)` 的 `Dictionary` 計數邏輯與方法名稱。
- 不新增測試專案或外部套件；使用目前的 .NET 10 console project 與 `Main` 作為執行驗證入口。
- 現有題目描述型 XML `<summary>` 維持原文；額外使用 `<remarks>`、`<param>` 與 `<returns>` 補充教學資訊。
- README 使用繁體中文，內容必須與目前程式及實際輸出一致。

## 程式設計

### 進入點與測試案例

`Main` 建立一組固定順序的 tuple 測試資料，每筆資料包含案例名稱、`target`、`arr` 與預期結果。案例涵蓋：

1. 一般排列順序不同但元素相同，預期 `true`。
2. 含重複值且元素頻率相同，預期 `true`。
3. 元素頻率不同，預期 `false`。
4. 單一元素與題目值域上界的邊界案例，預期 `true`。
5. 兩個空陣列的額外教學案例，預期 `true`；README 會明確註明它不在題目正式限制內。

`RunCase(string name, int[] target, int[] arr, bool expected)` 負責執行單一案例。它會把輸入分別複製後交給兩種方法，確保排序法對陣列的修改不會污染計數法的輸入。每個案例印出輸入、Expected、兩個方法的 Actual 與 PASS/FAIL；回傳值為兩個方法都符合預期時的布林結果。`Main` 彙總所有結果，任何案例失敗便設定非零程序結束碼。

移除 `Console.ReadKey()`，使 `dotnet run` 能在自動化或文件產生流程中自行結束。

### 既有排序法

`CanBeEqual` 先對兩個陣列排序，再逐項比較。反轉子陣列只能改變元素順序，不能改變元素集合；在題目保證長度相同的前提下，排序後完全相同即代表兩個陣列擁有相同的元素與出現次數。演算法旁保留一則說明此核心判斷，並標註原地排序會修改輸入。

時間複雜度為 `O(n log n)`，額外資料結構空間為 `O(1)`；排序實作本身可能使用 `O(log n)` 的呼叫堆疊空間。

### 既有 Dictionary 計數法

`CanBeEqual2` 先將 `target` 的每個值累計到字典，再逐一處理 `arr`：找不到值或扣除後次數小於零時立即回傳 `false`。題目保證兩陣列長度相同，因此所有元素都成功扣除後即可回傳 `true`。演算法旁只補充「比對元素頻率」與「避免扣除超過可用次數」兩個關鍵判斷。

時間複雜度為 `O(n)`，額外空間為 `O(k)`，其中 `k` 是 `target` 中不同元素的數量；此解法不會修改輸入陣列。

## README 設計

`README.md` 會包含以下內容：

- 題目中英文名稱、題目連結、操作定義與回傳條件。
- 題目正式限制，並區分額外的空陣列教學案例。
- 從「反轉不改變元素頻率」出發的解題概念。
- 排序法與 `Dictionary` 計數法各自的設計步驟、正確性直覺、複雜度、輸入修改注意事項與逐步範例。
- `Main` 測試夾具如何隔離兩種解法的輸入。
- 專案結構、restore/build/run/diff check 命令。
- 由新鮮 `dotnet run` 輸出產生的實際驗證紀錄。

README 不宣稱存在自動化測試專案，也不記錄未實際執行或未由目前程式支援的命令。

## 驗收條件

完成實作後，從 repository root 執行下列命令：

```bash
dotnet restore leetcode_1460/leetcode_1460.csproj
dotnet build leetcode_1460/leetcode_1460.csproj --nologo
dotnet run --project leetcode_1460/leetcode_1460.csproj
git diff --check
```

驗收時確認：

- restore 與 build 結束碼為 0。
- `dotnet run` 的每個案例均輸出 `PASS`，且程序正常結束。
- README 的命令、案例名稱、Expected/Actual 與實際輸出一致。
- `git diff --check` 沒有多餘空白或換行錯誤。

## 變更檔案

- `leetcode_1460/Program.cs`：加入測試夾具、補充 XML 文件與關鍵演算法註解。
- `README.md`：新增繁體中文題目與解法教學文件。
- `docs/superpowers/specs/2026-08-02-leetcode-1460-teaching-refresh-design.md`：保存本設計決策。
