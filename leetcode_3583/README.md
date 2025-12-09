# leetcode_3583 — Count Special Triplets (3583. Count Special Triplets)

> 題目來源：LeetCode（3583. Count Special Triplets）
> 原題連結: https://leetcode.com/problems/count-special-triplets/
> 題目中文: https://leetcode.cn/problems/count-special-triplets/

## 題目描述（繁體中文）
給你一個整數陣列 `nums`。

一個「特殊三元組」被定義為索引三元組 `(i, j, k)`，使得：
- 0 <= i < j < k < n
- `nums[i] == nums[j] * 2`
- `nums[k] == nums[j] * 2`

請回傳陣列中所有特殊三元組的數量，答案可能很大，請對 `10^9 + 7` 取模。


## 解題想法與概念（概覽）
核心想法是：將「中間元素」位置 `j` 作為枚舉目標，對於每個 `j`，我們需要計算出：
- 左邊（索引小於 `j`）中值為 `nums[j] * 2` 的出現次數 `L`
- 右邊（索引大於 `j`）中值為 `nums[j] * 2` 的出現次數 `R`

對於固定的 `j`，以 `j` 為中間的特殊三元組數量就是 `L * R`。
將所有 `j` 的 `L * R` 累加並對 `1e9 + 7` 取模，即為答案。

我們可以利用計數陣列或哈希表（字典）來維護左右側 `target = nums[j]*2` 的出現次數：

- 初始時，右側（right）計數器統計整個陣列。
- 當遍歷陣列（視為中間 `j`），先把當前元素從右側計數器減 1（因為中間位置不算在右側），
  計算 `target = nums[j] * 2` 在左側（left）與右側（right）的次數 `L * R`，累加。
- 最後把當前元素加入左側計數器。

此方法時間複雜度接近 O(n + m)（若使用陣列計數，m = max value in nums），空間則是 O(m)。若資料範圍很大，建議改用 Dictionary（O(n log n) 或 O(n) 時間）以節省記憶體。

---

## 詳細解法步驟（以陣列計數為例）
1. 若 `nums` 為 `null` 或長度 `< 3`，直接回傳 `0`。
2. 決定計數器資料結構：
   - 若 `nums` 的值非負且最大值 `mx` 不大（例如 < 幾百萬），可使用 `int[]` 大小為 `mx + 1` 來快速存取次數。
   - 若值域很大或含負數，請改用 `Dictionary<int, long>` 以避免大量空間開銷。
3. 用 `rightCount` 陣列計算整個 `nums` 的出現次數（右側初始包含所有元素）。
4. 建立 `leftCount` 陣列（初始為 0）。
5. 以 `foreach`（或 `for`）遍歷每個 `nums[j]`：
   - `rightCount[nums[j]]--`（把當前元素從右側移除）
   - 計算 `target = nums[j] * 2`，若 `target` 在可索引範圍內：
     - `ans += leftCount[target] * rightCount[target]`（並保持對 `MOD` 取模）
   - `leftCount[nums[j]]++`（把當前元素加入左側）
6. 返回 `ans % MOD`。

> [!TIP]
> 如果數值可能超過 int 範圍，務必在乘法前將變數轉成 `long`，避免溢位問題。

---

## 範例說明
例如 `nums = [2, 1, 2]`：
- 當 `j = 1`（nums[1] = 1）時，target = 2
  - 左邊：有一個 `2`，右邊：有一個 `2`，所以 `L * R = 1 * 1 = 1`。
  - 其他 `j` 則無符合的特殊三元組。
總數為 `1`。

## 邊界條件與注意事項
- 範例程式採用 `int[]` 為計數器，因此**假設 `nums` 的元素皆為非負整數**且最大值不會太大。
- 若 `nums` 含負數或最大數很大，請改用 `Dictionary<int, int>` 或 `Dictionary<int, long>` 來計數。
- 結果需要對 `10^9 + 7` 取模。

---

## 複雜度分析
- 時間複雜度：O(n + m)（使用陣列作為計數器時），其中 `m` 為 `max(nums)`，`n` 為陣列長度。
- 空間複雜度：O(m)（使用陣列）；若改用 `Dictionary`，空間會是 O(u)，`u` 是陣列中不同數值的數量。

---

## 程式碼執行與測試
此專案是 C#/.NET（net10.0）範例，可使用 `dotnet` 建構與執行：

```pwsh
# 進入專案資料夾
cd leetcode_3583

# 建構專案
dotnet build

# 執行
dotnet run --project leetcode_3583.csproj
```

> [!NOTE]
> 專案中 `Program.cs` 已內建一些測試案例，執行 `dotnet run` 可以看到範例輸出與是否 PASS/FAIL 的訊息。

---

## 可改良或延伸的地方
- 若要支援負數或大範圍的數值，改用 `Dictionary<int, long>`。
- 若希望在記憶體與時間間做權衡，可在資料量小時使用 `Dictionary`，在數值域穩定且不大的情況下使用陣列加速。
- 針對輸入非常大且稀疏的情況，可考慮先壓縮座標（coordinate compression）以減少計數陣列大小。

---

## 來源與參考
- LeetCode 題目：3583. Count Special Triplets
- 題目示例與本程式說明由原始題目與程式實作補充

---

如果你想要我將 `Dictionary` 版本或更完善的單元測試加入專案，請告訴我！
