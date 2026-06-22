# LeetCode 219：Contains Duplicate II

以 C# 實作「存在重複元素 II」的兩種線性時間解法，並提供可直接執行的完整測資，對照雜湊表與滑動視窗的結果。

## 題目說明

給定整數陣列 <code>nums</code> 與整數 <code>k</code>，請判斷是否存在兩個不同索引 <code>i</code>、<code>j</code>，使得：

~~~text
nums[i] == nums[j]
abs(i - j) <= k
~~~

若存在這樣的一對索引，回傳 <code>true</code>；否則回傳 <code>false</code>。

題目連結：[LeetCode 219 - Contains Duplicate II](https://leetcode.com/problems/contains-duplicate-ii/description/)

## 限制條件

- <code>1 &lt;= nums.Length &lt;= 100000</code>
- <code>-10^9 &lt;= nums[i] &lt;= 10^9</code>
- <code>0 &lt;= k &lt;= 100000</code>

本專案依照題目限制處理輸入，因此兩個解法不另外處理 <code>nums</code> 為 <code>null</code> 或 <code>k</code> 為負數的情況。

## 解題出發點

單純知道「某個數字重複出現」還不夠；還必須確認兩次出現的索引距離不超過 <code>k</code>。兩個解法都由左到右掃描陣列，差別在於各自保存的歷史資訊：

| 方法 | 保存的資訊 | 找到答案的時機 |
| --- | --- | --- |
| 方法一：雜湊表 | 每個數值最近一次出現的索引 | 目前索引與最近相同值索引的距離 <code>&lt;= k</code> |
| 方法二：滑動視窗 | 前方距離目前索引不超過 <code>k</code> 的數值集合 | 將目前值加入集合時發現已存在 |

兩種方法的時間複雜度皆為 <code>O(n)</code>，適合長度可達 <code>100000</code> 的輸入。

## 方法一：雜湊表記錄最近索引

<code>ContainsNearbyDuplicate</code> 使用 <code>Dictionary&lt;int, int&gt;</code>，鍵是陣列值，值是該數值目前為止**最近一次**出現的索引。

### 設計說明

掃描到索引 <code>i</code> 的值 <code>nums[i]</code> 時：

1. 若字典中沒有 <code>nums[i]</code>，代表它第一次出現，記錄 <code>nums[i] -> i</code>。
2. 若字典中已經有它，取出最近索引 <code>j</code>：
   - <code>i - j &lt;= k</code>：找到符合題意的一對索引，立刻回傳 <code>true</code>。
   - <code>i - j > k</code>：目前這一對不符合，但仍把索引更新為 <code>i</code>。
3. 掃描結束仍未找到符合條件的配對時，回傳 <code>false</code>。

只需要保留最近索引，因為對同一個值而言，最近索引會讓距離 <code>i - j</code> 最小。若最近索引都超出 <code>k</code>，更早的索引距離只會更大，無須保留。

### 範例演示

輸入 <code>nums = [1, 2, 3, 1]</code>、<code>k = 3</code>：

| 目前索引 <code>i</code> | <code>nums[i]</code> | 字典檢查與更新 | 結果 |
| --- | --- | --- | --- |
| 0 | 1 | <code>1</code> 未出現，記錄 <code>{ 1: 0 }</code> | 繼續 |
| 1 | 2 | <code>2</code> 未出現，記錄 <code>{ 1: 0, 2: 1 }</code> | 繼續 |
| 2 | 3 | <code>3</code> 未出現，記錄 <code>{ 1: 0, 2: 1, 3: 2 }</code> | 繼續 |
| 3 | 1 | 最近索引是 <code>0</code>，距離 <code>3 - 0 = 3 &lt;= k</code> | 回傳 <code>true</code> |

### 複雜度

- 時間：<code>O(n)</code>，每個元素只會查詢與更新一次字典。
- 空間：<code>O(n)</code>，最壞情況下字典保存每個不同元素的最近索引。

## 方法二：滑動視窗加雜湊集合

<code>ContainsNearbyDuplicate2</code> 使用 <code>HashSet&lt;int&gt;</code>，讓集合永遠只保存目前索引前方、距離不超過 <code>k</code> 的元素。

### 設計說明

當目前索引為 <code>i</code> 時，合法配對的前一個索引必須落在：

~~~text
max(0, i - k) 到 i - 1
~~~

因此：

1. 若 <code>i > k</code>，先移除索引 <code>i - k - 1</code> 的元素；它與目前索引的距離已經是 <code>k + 1</code>，不可能再成為合法配對。
2. 嘗試將 <code>nums[i]</code> 加入集合：
   - <code>Add</code> 回傳 <code>false</code>：集合中已有相同元素，兩個索引必在同一個長度至多為 <code>k + 1</code> 的視窗中，回傳 <code>true</code>。
   - <code>Add</code> 回傳 <code>true</code>：目前元素第一次出現在這個視窗內，繼續掃描。

集合的關鍵不變量是：處理 <code>nums[i]</code> 前，它只包含索引範圍 <code>max(0, i - k)</code> 到 <code>i - 1</code> 的值。

### 範例演示

輸入 <code>nums = [1, 0, 1, 1]</code>、<code>k = 1</code>：

| 目前索引 <code>i</code> | 移出視窗的值 | 加入前集合 | 加入 <code>nums[i]</code> 的結果 | 加入後集合／結果 |
| --- | --- | --- | --- | --- |
| 0 | 無 | <code>{}</code> | 加入 <code>1</code> 成功 | <code>{ 1 }</code>，繼續 |
| 1 | 無 | <code>{ 1 }</code> | 加入 <code>0</code> 成功 | <code>{ 1, 0 }</code>，繼續 |
| 2 | 移除索引 0 的 <code>1</code> | <code>{ 0 }</code> | 加入 <code>1</code> 成功 | <code>{ 0, 1 }</code>，繼續 |
| 3 | 移除索引 1 的 <code>0</code> | <code>{ 1 }</code> | 加入 <code>1</code> 失敗 | 已有相同值，回傳 <code>true</code> |

索引 2 的 <code>1</code> 與索引 3 的 <code>1</code> 相距 1，恰好符合 <code>k = 1</code>。

### 複雜度

- 時間：<code>O(n)</code>，每個元素最多加入與移出集合各一次。
- 空間：<code>O(min(n, k + 1))</code>，集合最多保存目前視窗內的不同元素。

## 可執行範例

<code>Main</code> 會執行三個官方案例與四個邊界案例，並對每一筆資料檢查兩種解法，因此共有 14 項驗證：

1. 重複元素距離等於 <code>k</code>。
2. 相鄰重複元素。
3. 所有重複元素都超出 <code>k</code>。
4. <code>k = 0</code>，即使值重複也不可能有兩個不同索引距離為零。
5. 重複元素距離恰為 <code>k</code>。
6. 重複元素距離為 <code>k + 1</code>。
7. 陣列含負數。

在此專案根目錄執行：

~~~bash
dotnet build leetcode_219/leetcode_219.csproj
dotnet run --project leetcode_219/leetcode_219.csproj
~~~

執行結果：

~~~text
LeetCode 219 - Contains Duplicate II
==================================================
[1] 官方案例 1：重複元素距離等於 k
nums = [1, 2, 3, 1], k = 3
預期結果：True
方法一（雜湊表）：True (PASS)
方法二（滑動視窗）：True (PASS)

[2] 官方案例 2：相鄰重複元素
nums = [1, 0, 1, 1], k = 1
預期結果：True
方法一（雜湊表）：True (PASS)
方法二（滑動視窗）：True (PASS)

[3] 官方案例 3：所有重複元素距離都超出 k
nums = [1, 2, 3, 1, 2, 3], k = 2
預期結果：False
方法一（雜湊表）：False (PASS)
方法二（滑動視窗）：False (PASS)

[4] 邊界：k = 0
nums = [1, 1], k = 0
預期結果：False
方法一（雜湊表）：False (PASS)
方法二（滑動視窗）：False (PASS)

[5] 邊界：距離恰為 k
nums = [1, 2, 1], k = 2
預期結果：True
方法一（雜湊表）：True (PASS)
方法二（滑動視窗）：True (PASS)

[6] 邊界：距離超出 k
nums = [1, 2, 1], k = 1
預期結果：False
方法一（雜湊表）：False (PASS)
方法二（滑動視窗）：False (PASS)

[7] 邊界：包含負數元素
nums = [-1, 2, -1], k = 2
預期結果：True
方法一（雜湊表）：True (PASS)
方法二（滑動視窗）：True (PASS)

總結：14/14 項驗證通過
~~~

## 專案結構

~~~text
.
├── leetcode_219/
│   ├── Program.cs             # 兩種解法與可執行測資
│   └── leetcode_219.csproj    # .NET 10 Console 專案
└── README.md
~~~

## 驗證

完成修改後，可使用以下指令確認沒有多餘空白或換行問題：

~~~bash
git diff --check
~~~
