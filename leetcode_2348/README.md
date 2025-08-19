# LeetCode 2348 — Number of Zero-Filled Subarrays

簡短說明：此專案示範如何計算陣列中所有只包含 0 的連續子陣列（非空）的總數，並提供清楚的演算法說明與範例。

## 問題
給定一個整數陣列，計算所有元素皆為 0 的連續子陣列（非空）總數。

## 解法（詳細說明）

### 直觀想法
觀察到：當陣列中出現連續的 0 時，對於長度為 k 的連續 0 區段，其貢獻的全0 子陣列數為 1 + 2 + ... + k = k*(k+1)/2。
因此我們可以用單次掃描維護一個當前連續 0 的長度 `count`，遇到 0 時 `count++` 並把 `count` 累加到總和 `res`；遇到非 0 時把 `count` 重置為 0。

> [!note]
> 實作中回傳型別使用 `long` 以避免溢位；若輸入為 `null`，目前實作會丟出 `ArgumentNullException`（可依專案風格改為回傳 0）。

### 演算法步驟

1. 若 `nums` 為 `null`，丟出 `ArgumentNullException`。
2. 設 `res = 0L`（累計答案），`count = 0L`（當前連續 0 的長度）。
3. 從左至右掃描陣列：
   - 若當前元素為 0：`count++`，`res += count`。
   - 否則：`count = 0`。
4. 掃描結束，回傳 `res`。

### 正確性與數學推導
對於任一連續 0 的區段長度 k，所有以該區段內位置為結尾的全0 子陣列數分別為 1,2,...,k，合計為 k*(k+1)/2。演算法逐位置累加當前 `count`，當掃過整段 k 個 0 時會累加出 1+2+...+k，因此會覆蓋所有可能的全0 子陣列且不重複計算，故正確。

### 時間與空間複雜度
- 時間複雜度：O(n)，單次掃描。
- 空間複雜度：O(1)，僅使用常數額外空間（幾個 `long` 變數）。

### 範例逐步推演
輸入：`[1,0,0,0,2,0,0]`

掃描過程（位置 -> count / res）：
- 1  -> 0 / 0
- 0  -> 1 / 1
- 0  -> 2 / 3
- 0  -> 3 / 6   （此段 3 個 0 貢獻 6）
- 2  -> 0 / 6
- 0  -> 1 / 7
- 0  -> 2 / 9   （最後段 2 個 0 貢獻 3，但累加過程已含）

回傳 `9`。

### 邊界與測試建議
- `null` 輸入：檢查是否拋出 `ArgumentNullException`（或根據專案風格改為回傳 0）。
- 空陣列：回傳 0。
- 無 0：回傳 0。
- 大尺寸且大量 0：使用 `long` 避免溢位，再以效能測試驗證。

建議測試集：
- `[]` => `0`
- `[0]` => `1`
- `[0,0]` => `3`
- `[1,2,3]` => `0`
- `[1,0,0,0,2,0,0]` => 驗證實際結果

### 替代方法（比較）
- 批次計算：先找出所有連續 0 的區段長度 k，對每段計算 k*(k+1)/2，最後相加；時間仍 O(n)，實作上直觀。
- 其他如前綴和或更複雜的滑動視窗在本題沒有必要，因為上面方法已達到最佳時間與空間複雜度。

## 程式範例
主專案中已包含 `Program.cs` 的完整實作與簡單示範，請參考 `leetcode_2348/Program.cs`。

---

如果要我再同時 建立 一份英文版 README 或把這份內容加入專案的說明文件（例如 `.github`），我可以繼續協助。

## 替代實作：滑動視窗（ZeroFilledSubarray2）

專案中另外提供 `ZeroFilledSubarray2` 作為替代實作，使用「枚舉右端點 + 記錄最近非0 索引 last」的滑動視窗思路：當右端點為 i 時，若 nums[i] == 0，則以 i 為右端點的全0 子陣列數量為 i - last（last 為最近一個非0 的索引，初始化為 -1）。時間複雜度同樣為 O(n)，空間 O(1)。

注意：`ZeroFilledSubarray` 會對 null 輸入拋出 `ArgumentNullException`；`ZeroFilledSubarray2` 在程式中未額外做 null 檢查，呼叫時請確保參數非 null（或可在呼叫前自行檢查）。

要在本機執行範例：

```bash
dotnet build /Users/qiuzili/Leetcode/Leetcode_folder/leetcode_2348/leetcode_2348.csproj
dotnet run --project /Users/qiuzili/Leetcode/Leetcode_folder/leetcode_2348/leetcode_2348.csproj
```

執行後 `Program.Main` 會印出每個測試案例的 `ZeroFilledSubarray` 與 `ZeroFilledSubarray2` 的結果，便於比較與驗證。
