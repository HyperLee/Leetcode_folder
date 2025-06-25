# leetcode_2040

## 題目簡介

**2040. 兩個有序陣列的第 K 小乘積**

給定兩個已排序的整數陣列 `nums1` 和 `nums2`，以及一個整數 `k`，請回傳所有 `nums1[i] * nums2[j]`（0 <= i < nums1.length, 0 <= j < nums2.length）中的第 k 小（1-based）乘積。

- [LeetCode 英文題目連結](https://leetcode.com/problems/kth-smallest-product-of-two-sorted-arrays/description/?envType=daily-question&envId=2025-06-25)
- [LeetCode 中文題目連結](https://leetcode.cn/problems/kth-smallest-product-of-two-sorted-arrays/description/?envType=daily-question&envId=2025-06-25)

## 解題思路

- 乘積的取值範圍為 [-1e10, 1e10]，可在此區間進行二分搜尋。
- 對於每個二分值 v，計算小於等於 v 的乘積數目 count。
- 若 count < k，代表答案偏小，需調整左界；否則調整右界。
- 對於每個 nums1[i]：
  - 若 >=0，nums2[j]*nums1[i] 單調遞增，直接二分找 <=v 的個數；
  - 若 <0，nums2[j]*nums1[i] 單調遞減，二分找 >v 的個數，答案為 n2-t。
- 綜合所有 nums1[i] 統計即可。

## 詳細解題步驟說明

1. **確定乘積範圍**
   - 由於 nums1、nums2 可能包含正負數，所有乘積的最小值為 -1e10，最大值為 1e10。

2. **二分搜尋答案**
   - 設定搜尋區間 [left, right] = [-1e10, 1e10]。
   - 每次取中間值 mid，計算所有 nums1[i] * nums2[j] ≤ mid 的個數 count。

3. **計算小於等於 mid 的乘積數量**
   - 對每個 nums1[i]：
     - 若 nums1[i] ≥ 0，nums2[j]*nums1[i] 單調遞增，直接二分搜尋找出 ≤ mid 的個數。
     - 若 nums1[i] < 0，nums2[j]*nums1[i] 單調遞減，二分搜尋找出 > mid 的個數 t，答案為 nums2 長度 - t。
   - 將所有 nums1[i] 的結果加總得到 count。

4. **調整搜尋區間**
   - 若 count < k，代表 mid 太小，left = mid + 1。
   - 若 count ≥ k，代表 mid 可能是答案，right = mid - 1。

5. **結束條件**
   - 當 left > right 時，left 即為第 k 小乘積。

6. **複雜度說明**
   - 每次二分搜尋 O(logM)，每次計算 count 需 O(n1*logN)，總複雜度 O((n1+n2) * logM * logN)。

## 程式碼架構

- `Program.cs`：主程式與解題邏輯，包含：
  - `KthSmallestProduct`：主演算法，二分搜尋第 k 小乘積。
  - `F`：輔助函式，計算 nums2 中與 x1 相乘後小於等於 v 的個數。
  - `Main`：內含多組測試資料與預期結果。

## 執行與偵錯

1. 進入專案根目錄，執行：
   ```sh
   dotnet build
   dotnet run --project leetcode_2040/leetcode_2040.csproj
   ```
2. VS Code 可直接使用 `.vscode/launch.json` 與 `tasks.json` 進行偵錯。

## 複雜度分析
- 時間複雜度：O((n1+n2) * logM * logN)，M 為乘積區間範圍，N 為 nums2 長度。
- 空間複雜度：O(1)

## 範例輸出
```
第2小乘積: 8
第6小乘積: 16
```
