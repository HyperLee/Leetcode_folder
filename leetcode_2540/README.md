# LeetCode 2540：最小公共值（Minimum Common Value）

本專案以 .NET 10 console application 示範 LeetCode 2540「最小公共值」的三種解法，並在 `Main` 中以固定測試案例比較每種解法的結果。

- 題目：[Minimum Common Value](https://leetcode.com/problems/minimum-common-value/description/?envType=daily-question&envId=2024-03-09)
- 中文題目：[最小公共值](https://leetcode.cn/problems/minimum-common-value/)
- Target framework：`net10.0`

## 題目說明

給定兩個已依非遞減順序排列的整數陣列 `nums1` 與 `nums2`，找出同時出現在兩個陣列中的最小整數。

如果兩個陣列沒有任何公共值，回傳 `-1`。

例如：

```text
nums1 = [1, 2, 3, 6]
nums2 = [2, 3, 4, 5]
```

兩個陣列的公共值是 `2` 與 `3`，其中最小值為 `2`，因此答案是 `2`。

## 限制條件

依照[官方題目限制](https://leetcode.com/problems/minimum-common-value/description/?envType=daily-question&envId=2024-03-09)：

- `1 <= nums1.Length, nums2.Length <= 10^5`
- `1 <= nums1[i], nums2[j] <= 10^9`
- `nums1` 與 `nums2` 都是非遞減排序。
- 陣列長度至少為 `1`，因此本題不需要處理空陣列輸入。
- 非遞減排序允許重複值，但重複值不會改變最小公共值。

## 解題概念與出發點

這題的核心不是只判斷「是否有交集」，而是要回傳交集中的最小值。

兩個陣列都已經排序，因此可以利用以下觀察：

1. 若依照排序順序掃描某個陣列，第一個找到的公共值就是最小公共值。
2. 如果目前比較的兩個值不同，較小的值不可能與另一邊目前較大的值匹配。
3. 因此可以選擇建立成員查找結構，或直接利用排序特性同步移動兩個指標。

本專案保留兩種以雜湊結構為基礎的解法，另外加入一種不需要額外集合的雙指標解法，方便比較不同的思考方式與空間成本。

## 解法一：Dictionary 記錄 `nums1` 的值

對應方法：`GetCommon`

### 設計步驟

1. 建立 `Dictionary<int, int>`，將 `nums1` 中出現過的值放入 dictionary 的 key。
2. dictionary 的 value 在本題沒有實際用途，真正需要的是「某個值是否出現在 `nums1`」的查找能力。
3. 按照原本順序掃描 `nums2`。
4. 若目前的 `nums2` 值存在於 dictionary，立即回傳該值。
5. 因為 `nums2` 是非遞減排序，第一個命中的值就是最小公共值；掃描結束仍未命中時回傳 `-1`。

### 範例演示

使用官方第二個範例：

```text
nums1 = [1, 2, 3, 6]
nums2 = [2, 3, 4, 5]
```

建立 dictionary 後，key 可以視為：

```text
{ 1, 2, 3, 6 }
```

接著依序查看 `nums2`：

| nums2 目前值 | 是否存在於 nums1 的 dictionary | 動作 |
| --- | --- | --- |
| `2` | 是 | 立即回傳 `2` |

雖然後面還有公共值 `3`，但因為 `2` 已經是排序後第一個命中值，所以不需要繼續掃描。

### 複雜度

- 時間複雜度：`O(nums1.Length + nums2.Length)`
- 額外空間複雜度：`O(nums1.Length)`

## 解法二：HashSet 記錄 `nums1` 的值

對應方法：`GetCommon2`

### 設計步驟

1. 建立 `HashSet<int>`，將 `nums1` 的每個值加入集合。
2. `HashSet` 直接表達「值是否存在」，比使用只有 key 會被利用的 dictionary 更貼合需求。
3. 按照排序順序掃描 `nums2`。
4. 第一個被 `HashSet.Contains` 找到的值就是最小公共值。
5. 如果所有元素都沒有命中，回傳 `-1`。

### 範例演示

仍使用：

```text
nums1 = [1, 2, 3, 6]
nums2 = [2, 3, 4, 5]
```

先建立集合：

```text
HashSet = { 1, 2, 3, 6 }
```

掃描 `nums2` 時，第一個值 `2` 已經存在於集合中，因此直接回傳 `2`。值 `3` 雖然也是公共值，但不會影響最小答案。

### 與解法一的差異

兩者的查找概念相同，都是先索引 `nums1`，再掃描 `nums2`。差異在於：

- `Dictionary<int, int>` 還有 value 欄位，但本題不使用它。
- `HashSet<int>` 專門表達成員資格，因此資料結構語意更直接。
- 兩者的時間與額外空間複雜度相同。

### 複雜度

- 時間複雜度：`O(nums1.Length + nums2.Length)`
- 額外空間複雜度：`O(nums1.Length)`

## 解法三：雙指標同步掃描

對應方法：`GetCommon3`

這是最能直接利用題目「兩個陣列已排序」條件的解法，不需要建立 dictionary 或 set。

### 設計步驟

令：

- `index1` 指向 `nums1` 的目前位置。
- `index2` 指向 `nums2` 的目前位置。

每輪比較兩個目前值：

1. 如果 `nums1[index1] == nums2[index2]`，找到公共值，立即回傳。
2. 如果 `nums1[index1] < nums2[index2]`，移動 `index1`。
3. 如果 `nums1[index1] > nums2[index2]`，移動 `index2`。
4. 任一指標超出陣列範圍，代表剩餘元素不可能再形成公共值，回傳 `-1`。

### 為什麼可以跳過較小值

假設目前：

```text
nums1[index1] < nums2[index2]
```

由於 `nums1` 已排序，`nums1[index1]` 之後的值只會大於或等於目前值；而 `nums2[index2]` 目前比它大。如果不移動 `index1`，目前兩個值不可能相等，因此可以安全跳過 `nums1[index1]`。

另一種情況 `nums1[index1] > nums2[index2]` 也是相同道理，改為移動 `index2`。

### 範例演示

使用：

```text
nums1 = [1, 2, 3, 6]
nums2 = [2, 3, 4, 5]
```

逐步比較如下：

| `index1` | `index2` | `nums1[index1]` | `nums2[index2]` | 動作 |
| ---: | ---: | ---: | ---: | --- |
| `0` | `0` | `1` | `2` | `1 < 2`，移動 `index1` |
| `1` | `0` | `2` | `2` | 相等，回傳 `2` |

因為兩個指標都只會向右移動，每個元素最多被檢查一次，所以不需要額外集合即可完成搜尋。

### 複雜度

- 時間複雜度：`O(nums1.Length + nums2.Length)`
- 額外空間複雜度：`O(1)`

## 三種解法比較

| 方法 | 查找結構 | 是否利用排序 | 時間複雜度 | 額外空間 | 特點 |
| --- | --- | --- | --- | --- | --- |
| `GetCommon` | `Dictionary` | 利用 `nums2` 的掃描順序 | `O(n + m)` | `O(n)` | 保留原始 dictionary 教學版本 |
| `GetCommon2` | `HashSet` | 利用 `nums2` 的掃描順序 | `O(n + m)` | `O(n)` | 成員查找語意直接 |
| `GetCommon3` | 雙指標 | 充分利用兩邊排序 | `O(n + m)` | `O(1)` | 不需額外集合，空間效率最佳 |

其中 `n = nums1.Length`、`m = nums2.Length`。

## Main 測試 harness

`Main` 會執行以下 6 個固定案例，並讓三種解法各驗證一次：

| 案例 | `nums1` | `nums2` | 預期結果 |
| --- | --- | --- | ---: |
| 官方範例一 | `[1, 2, 3]` | `[2, 4]` | `2` |
| 官方範例二 | `[1, 2, 3, 6]` | `[2, 3, 4, 5]` | `2` |
| 無交集 | `[1, 3, 5]` | `[2, 4, 6]` | `-1` |
| 單元素邊界 | `[7]` | `[7]` | `7` |
| 重複值 | `[1, 2, 2, 4]` | `[2, 2, 3]` | `2` |
| 最大值邊界 | `[1, 1000000000]` | `[1000000000]` | `1000000000` |

每個案例都會顯示：

- 輸入陣列
- 預期結果
- 三個方法的實際結果
- `PASS` 或 `FAIL`

如果 18 項檢查全部通過，程式最後回傳結束碼 `0`；否則回傳 `1`。這讓 console harness 可以在重新導向輸出或 CI 環境中直接判斷成功與否，不依賴互動式按鍵輸入。

## 執行方式

請在本專案根目錄 `/Users/qiuzili/Leetcode/Leetcode_folder/leetcode_2540` 執行：

```bash
dotnet restore leetcode_2540/leetcode_2540.csproj
dotnet build leetcode_2540/leetcode_2540.csproj --nologo
dotnet run --project leetcode_2540/leetcode_2540.csproj
```

本專案目前沒有自動化測試專案，因此以明確 project path 的 `dotnet build` 與可執行的 Main harness 作為驗證方式。

## 範例執行結果

以下內容取自 `dotnet run --project leetcode_2540/leetcode_2540.csproj` 的實際執行結果：

```text
案例：官方範例一
nums1 = [1, 2, 3]
nums2 = [2, 4]
預期 = 2
GetCommon   實際 = 2 => PASS
GetCommon2  實際 = 2 => PASS
GetCommon3  實際 = 2 => PASS

案例：官方範例二
nums1 = [1, 2, 3, 6]
nums2 = [2, 3, 4, 5]
預期 = 2
GetCommon   實際 = 2 => PASS
GetCommon2  實際 = 2 => PASS
GetCommon3  實際 = 2 => PASS

案例：無交集
nums1 = [1, 3, 5]
nums2 = [2, 4, 6]
預期 = -1
GetCommon   實際 = -1 => PASS
GetCommon2  實際 = -1 => PASS
GetCommon3  實際 = -1 => PASS

案例：單元素邊界
nums1 = [7]
nums2 = [7]
預期 = 7
GetCommon   實際 = 7 => PASS
GetCommon2  實際 = 7 => PASS
GetCommon3  實際 = 7 => PASS

案例：重複值
nums1 = [1, 2, 2, 4]
nums2 = [2, 2, 3]
預期 = 2
GetCommon   實際 = 2 => PASS
GetCommon2  實際 = 2 => PASS
GetCommon3  實際 = 2 => PASS

案例：最大值邊界
nums1 = [1, 1000000000]
nums2 = [1000000000]
預期 = 1000000000
GetCommon   實際 = 1000000000 => PASS
GetCommon2  實際 = 1000000000 => PASS
GetCommon3  實際 = 1000000000 => PASS

總結：18/18 項驗證通過
```

## 專案結構

```text
leetcode_2540/
├── leetcode_2540/
│   ├── Program.cs
│   └── leetcode_2540.csproj
├── docs/
│   └── readme-template.md
├── leetcode_2540.sln
└── README.md
```