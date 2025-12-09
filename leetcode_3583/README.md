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

## 方法二：使用 Dictionary（哈希表）詳細說明 🔍

### 概念說明
- 當陣列 `nums` 的值含有負數或值域很大時，使用陣列作為計數器會造成大量記憶體浪費，或無法處理負數。
- `SpecialTripletsWithDictionary` 透過兩個 `Dictionary<int, long>`（`left` 與 `right`）維護左右兩側各數值的出現次數，達成與陣列計數相同的邏輯，但更通用。
- `right` 初始為整個陣列的出現次數；遍歷每個元素 `nums[j]` 時，先把 `nums[j]` 從 `right` 減少（表示 j 已從右側移除），再以 `nums[j]*2` 當作 `target`，若 `left` 與 `right` 中都含有 `target`，則以 `left[target] * right[target]` 累加到答案；最後把 `nums[j]` 加回 `left`。

### 詳細步驟（伪程式化描述）
1. 若 `nums == null` 或 `nums.Length < 3`，回傳 0。
2. 建立兩個 `Dictionary<int, long>`：`left`（初始空）與 `right`（用來統計整個 `nums`）。
3. 先用迴圈把整個陣列的出現次數填入 `right`。
4. 初始化 `ans = 0L`。
5. 對於陣列中的每個值 `v = nums[j]`：
   - 把 `right[v]--`，若為 0 則移除 `right[v]` 的鍵值，因為右側已不再包含該個體。
   - 計算 `targetLong = (long)v * 2L`（用 `long` 以避免溢位），確保 `targetLong` 落在 `int.MinValue..int.MaxValue` 的範圍內後再轉回 `int target = (int)targetLong`。
   - 若 `left.TryGetValue(target, out leftCnt)` 與 `right.TryGetValue(target, out rightCnt)` 都回傳 true，則 `ans += leftCnt * rightCnt`（用 `long` 作乘法）；為避免 `ans` 過大，並在迴圈內定期對 `MOD` 取模。
   - 把 `v` 加入 `left`：`left[v]++`。
6. 回傳 `(int)(ans % MOD)`。

### 程式碼重點說明（為何如此實作）
- 使用 `long` 做為 `Dictionary` 的 value：當元素重複次數很大（例如陣列長度非常大），`leftCnt * rightCnt` 可能超過 `int` 範圍，使用 `long` 可以避免中間結果溢位情況。
- 在計算 `target` 時先轉 `v` 到 `long` 再乘 2，可以避免在極端數值時出現整數溢位（例如 `v = int.MaxValue`），並透過邊界檢查來確保只把在 `int` 範圍內的 `target` 當作字典鍵。
- 使用 `TryGetValue` 可避免多次查找，提高效率。
- 在每次加入 `left` 之前先從 `right` 扣除，以保證 `left` 代表索引 < j，`right` 代表索引 > j（中間 j 不算在左右側）。

### 範例
- 輸入：`nums = new int[] { 2, 1, 2 }`。
  - `right` 初始為 {2:2, 1:1}；當遍歷到 `j=1`（v=1）時：
    - `right` 減為 {2:2, 1:0}（移除 1），`left` 尚空。
    - `target = 2`，`left[target] = 1`?（否），`right[target] = 2`，累加為 0；最後把 `1` 加入 `left`。
  - 當遍歷到 `j=2`（v=2）之前，若有符合狀況則會累計到 `ans`，最終得到 `ans = 1`。

### 時間 / 空間複雜度分析
- 時間複雜度：平均 O(n)，其中 n = `nums.Length`。字典的查找與插入平均為 O(1)；最壞情況為 O(n)（哈希碰撞/退化）。
- 空間複雜度：O(u)，u = `nums` 中不同數值的數量（獨特值數量）。

### 優點與注意事項
- 優點：
  - 支援負數、超大範圍或稀疏值域；不需要額外做座標壓縮。
  - 空間只依賴於不同元素的數量，當值域很大但不同值少時很節省記憶體。
- 注意事項：
  - 因為使用 `Dictionary`，對極端輸入會有哈希碰撞風險而影響效能；若值域小、且皆為非負數，陣列計數法會比 `Dictionary` 更快。
  - 計算 `target` 時要小心整數溢位，因此必須先以 `long` 儲存，並判斷是否在 `int` 範圍內。
  - 當 `leftCnt` 與 `rightCnt` 都很大時，二者相乘可能超出 32-bit，因此使用 `long` 以確保正確性。

---

## 方法比較：陣列計數 vs. Dictionary（簡表） 📊

| 特性 | 陣列計數 (`SpecialTriplets`) | Dictionary (`SpecialTripletsWithDictionary`) |
|---|---:|---:|
| 值域限制 | 需值為非負且最大值 `mx` 不大（須建立 `mx+1` 的陣列） | 支援負數與稀疏、超大範圍值 | 
| 時間複雜度 | O(n + m)（m = mx） | 平均 O(n)；最差 O(n * 哈希退化) |
| 空間複雜度 | O(m)（mx + 1） | O(u)（唯一值數量） |
| 記憶體使用 | 當 `mx` 大時浪費；但若 `mx` 小且非負則最快 | 僅占獨特值空間，稀疏或有負數時更優 |
| 實作複雜度 | 稍微簡單且高效 | 稍微通用，需注意溢位與鍵值判斷 |
| 何時選用 | 值域合理、非負、執行時間需最快 | 值域包含負數或非常大、或稀疏分布 |

### 選擇建議 💡
- 若 `nums` 的值域已知為非負、且最大值 `mx` 在可接受的記憶體範圍內（例如 `mx` 幾億以內會造成記憶體問題），則使用陣列計數 `SpecialTriplets` 會更快且更簡潔。
- 若 `nums` 可能包含負數、或分布稀疏、或值域過大導致建立陣列不切實際時，請改用 `SpecialTripletsWithDictionary`。

---

若你希望我把 `Dictionary` 版本的單元測試也加入專案（包含負數、極端範例與大筆重複值），請告訴我，我可以再幫你建立測試檔案並加入 `dotnet test` 的範例指令。

---

## 來源與參考
- LeetCode 題目：3583. Count Special Triplets
- 題目示例與本程式說明由原始題目與程式實作補充

---

如果你想要我將 `Dictionary` 版本或更完善的單元測試加入專案，請告訴我！
