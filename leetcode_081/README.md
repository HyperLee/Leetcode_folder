# LeetCode 81: Search in Rotated Sorted Array II

使用 C# 實作 LeetCode 81，整理四種解法（線性掃描、兩種二分策略、旋轉點分段二分），並在 `Main` 提供可直接執行的測資驗證流程。

## 題目說明

給定一個原本為非遞減排序、但在未知 pivot 旋轉過的整數陣列 `nums`（可含重複值），以及整數 `target`。

請判斷 `target` 是否存在於陣列中：
- 存在回傳 `true`
- 不存在回傳 `false`

題目連結：
- https://leetcode.com/problems/search-in-rotated-sorted-array-ii/description/
- https://leetcode.cn/problems/search-in-rotated-sorted-array-ii/description/

## 限制條件與挑戰

- `nums` 原始為非遞減排序，但經旋轉。
- `nums` 可能有大量重複值。
- 重複值會破壞一般旋轉陣列二分法「至少一側有序」的清晰判斷，導致某些情況只能收縮邊界。
- 題目要求盡可能降低整體操作步驟。

## 解題概念與出發點

此題核心難點不是「找 target」，而是「如何在重複值存在時仍能安全縮小搜尋範圍」。

因此本專案保留四種方法，從直覺法到不同二分法策略：
1. 先有保底正確解（`Search`）。
2. 再加入經典重複值二分（`Search2`）。
3. 再示範開區間 + 參考右端點的判斷法（`Search3`）。
4. 最後示範先找旋轉點再分段二分（`Search4`）。

## 各解法設計說明

### 解法一：線性掃描 `Search`

設計：
- 逐一比對 `nums[i] == target`。
- 找到即回傳 `true`，掃描完仍未找到回傳 `false`。

優點：
- 最直覺、最好驗證。
- 不受旋轉與重複值判斷影響。

缺點：
- 時間複雜度較高。

複雜度：
- 時間：`O(n)`
- 空間：`O(1)`

### 解法二：重複值情境下的二分 `Search2`

設計：
- 每輪取 `mid`。
- 若 `nums[mid] == target` 直接成功。
- 若 `nums[left] == nums[mid] == nums[right]`，無法判定有序半邊，執行 `left++`、`right--` 去重後續查。
- 否則：
  - 若左半有序，判斷 target 是否在左半區間。
  - 否則右半有序，判斷 target 是否在右半區間。

優點：
- 多數情況接近二分效率。

缺點：
- 當重複值很多時，可能退化到接近線性。

複雜度：
- 平均時間：`O(log n)`
- 最差時間：`O(n)`（大量重複值）
- 空間：`O(1)`

### 解法三：開區間 + 右端點參考判斷 `Search3`

設計：
- 用開區間概念維護邊界，並以 `nums[right]` 作為旋轉分段參考。
- 若 `nums[mid] == nums[right]`，先 `right--` 移除尾端重複干擾。
- 否則透過 `check(...)` 判斷 `mid` 是否屬於應保留的右界側，進一步收斂。

優點：
- 判斷流程聚焦在「對右端點的相對位置」。
- 便於抽出判斷函式重用思路。

缺點：
- 對閱讀者來說抽象度較高。

複雜度：
- 平均時間：`O(log n)`
- 最差時間：`O(n)`
- 空間：`O(1)`

### 解法四：找旋轉點 + 分段二分 `Search4`

設計：
1. 先削掉尾端與首元素相同的重複值，盡量恢復二段性。
2. 第一次二分找第一段尾端，推得第二段起點 `idx`（旋轉點）。
3. 對 `[0, idx-1]` 與 `[idx, n-1]` 分別做標準二分 `Find(...)`。

優點：
- 觀念清楚：先定位結構，再分段查找。
- `Find` 可作為通用有序區間搜尋工具。

缺點：
- 實作步驟較多。

複雜度：
- 平均時間：`O(log n)`
- 最差時間：`O(n)`
- 空間：`O(1)`

## 範例演示流程

### 範例 A：`nums = [2,5,6,0,0,1,2]`, `target = 0`

- `Search`：線性掃到 `0`，回傳 `true`。
- `Search2`：
  - 初始可判斷右半或左半有序，逐步縮小區間。
  - `mid` 最終落到值 `0`，回傳 `true`。
- `Search3`：
  - 以 `right` 參考，遇到與 `right` 同值時先收縮。
  - 收斂後確認 `nums[right] == 0`，回傳 `true`。
- `Search4`：
  - 先找旋轉點，再在兩段中查找。
  - 於第二段找到 `0`，回傳 `true`。

結果：四種解法一致為 `true`。

### 範例 B：`nums = [2,5,6,0,0,1,2]`, `target = 3`

- `Search`：完整掃描未命中，`false`。
- `Search2`：有序半邊判斷後排除不可能區間，最終未命中，`false`。
- `Search3`：持續收斂後 `nums[right] != 3`，`false`。
- `Search4`：兩段二分都找不到，`false`。

結果：四種解法一致為 `false`。

### 範例 C：`nums = [1,0,1,1,1]`, `target = 0`

- 關鍵在重複值使有序半邊不易判斷。
- `Search2` 與 `Search3` 都會先做去重式邊界收縮，再恢復可判斷狀態。
- 最後四種解法都可正確回傳 `true`。

## 專案結構

```text
leetcode_081/
├─ docs/
│  └─ readme-template.md
└─ leetcode_081/
   ├─ leetcode_081.csproj
   └─ Program.cs
```

## 建置、執行與測試

在專案根目錄執行：

```bash
dotnet build leetcode_081/leetcode_081.csproj
dotnet run --project leetcode_081/leetcode_081.csproj
dotnet test leetcode_081/leetcode_081.csproj
```

說明：
- 本專案為單一主控台專案，`dotnet test` 目前沒有測試專案可執行，因此只會顯示還原/檢查結果，屬正常現象。
- `dotnet run` 會輸出 6 組測資結果，並包含：
  - `expected`（預期）
  - `Search/Search2/Search3/Search4`（四解法結果）
  - `pass`（是否符合預期）
  - `consistent`（四解法是否一致）

## 目前實作對應

- 可執行示例入口：`Main`
- 四種解法：`Search`, `Search2`, `Search3`, `Search4`
- 輔助函式：`check`, `Find`
- 程式中已補齊主要函式 XML `summary` 與關鍵演算法註解。
