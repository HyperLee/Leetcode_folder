# LeetCode 3069 - 將元素分配到兩個陣列中 I

[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![LeetCode 3069](https://img.shields.io/badge/LeetCode-3069-F89F1B?logo=leetcode&logoColor=white)](https://leetcode.com/problems/distribute-elements-into-two-arrays-i/description/)

這是一個使用 .NET 10 console application 實作的 LeetCode 3069 教學專案。程式保留兩種線性時間解法：第一種使用兩個 <code>List&lt;int&gt;</code> 直接模擬，第二種使用單一結果陣列與雙指標降低額外空間。<code>Main</code> 會以五組固定案例同時驗證兩種解法。

- 英文題目：[Distribute Elements Into Two Arrays I](https://leetcode.com/problems/distribute-elements-into-two-arrays-i/description/)
- 中文題目：[將元素分配到兩個陣列中 I](https://leetcode.cn/problems/distribute-elements-into-two-arrays-i/description/)
- Target framework：<code>net10.0</code>

## 題目說明

給定一個長度為 <code>n</code>、元素互不相同的整數陣列 <code>nums</code>，依序把所有元素分配到 <code>arr1</code> 與 <code>arr2</code>：

1. 將 <code>nums[0]</code> 加入 <code>arr1</code>。
2. 將 <code>nums[1]</code> 加入 <code>arr2</code>。
3. 從 <code>nums[2]</code> 開始處理剩餘元素：
   - 若 <code>arr1</code> 的尾端元素大於 <code>arr2</code> 的尾端元素，將目前元素加入 <code>arr1</code>。
   - 否則，將目前元素加入 <code>arr2</code>。
4. 回傳 <code>arr1</code> 串接 <code>arr2</code> 的結果。

例如 <code>nums = [5, 4, 3, 8]</code>：

- 初始狀態為 <code>arr1 = [5]</code>、<code>arr2 = [4]</code>。
- 因為 <code>5 &gt; 4</code>，所以將 <code>3</code> 加入 <code>arr1</code>。
- 接著比較 <code>3</code> 與 <code>4</code>；條件不成立，因此將 <code>8</code> 加入 <code>arr2</code>。
- 最後得到 <code>[5, 3] + [4, 8] = [5, 3, 4, 8]</code>。

## 限制條件

- <code>2 &lt;= nums.Length &lt;= 50</code>
- <code>1 &lt;= nums[i] &lt;= 100</code>
- <code>nums</code> 中的所有元素互不相同

因為陣列至少有兩個元素，所以兩種解法都可以直接使用 <code>nums[0]</code> 與 <code>nums[1]</code> 建立初始狀態。實作遵循 LeetCode 的有效輸入契約，不額外處理 <code>null</code>、長度不足或重複元素。

## 解題概念與出發點

題目的每一步只關心兩項資訊：

- <code>arr1</code> 目前的最後一個元素。
- <code>arr2</code> 目前的最後一個元素。

不需要搜尋先前所有元素，也不需要排序。每個 <code>nums[i]</code> 只會被判斷一次並加入其中一個陣列，因此核心流程可以在 <code>O(n)</code> 時間內完成。

另一個重要性質是：元素一旦加入 <code>arr1</code> 或 <code>arr2</code>，在該陣列中的相對順序就不能改變。兩種解法的主要差異，就是如何保存這兩組順序：

1. 直接維護兩個可增長的 List，最後串接。
2. 在單一結果陣列中，讓 <code>arr1</code> 從左向右寫入、<code>arr2</code> 從右向左暫存，再反轉 <code>arr2</code> 區段。

兩個公開 API 都只讀取 <code>nums</code>，不會排序或覆寫輸入內容。

## 解法一：兩個 List 直接模擬

API：<code>ResultArray(int[] nums)</code>

### 設計說明

這個方法最直接對應題目敘述：

1. 建立 <code>firstArray</code> 與 <code>secondArray</code>。
2. 分別放入 <code>nums[0]</code> 與 <code>nums[1]</code>。
3. 從索引 2 開始走訪：
   - 讀取兩個 List 的最後一個元素。
   - 將目前數字加入尾端較大的那一組；若條件不成立則加入第二組。
4. 使用 <code>AddRange</code> 把第二組接到第一組後面。
5. 轉成新的整數陣列並回傳。

List 會完整保存兩個邏輯陣列，因此實作容易閱讀、除錯與逐步對照題目規則。

### 範例演示

使用 <code>nums = [5, 4, 3, 8]</code>：

| 步驟 | 目前元素 | 尾端比較 | firstArray | secondArray |
| --- | ---: | --- | --- | --- |
| 初始化 | 5、4 | 不比較 | <code>[5]</code> | <code>[4]</code> |
| 處理索引 2 | 3 | <code>5 &gt; 4</code>，加入 firstArray | <code>[5, 3]</code> | <code>[4]</code> |
| 處理索引 3 | 8 | <code>3 &gt; 4</code> 不成立，加入 secondArray | <code>[5, 3]</code> | <code>[4, 8]</code> |
| 串接 | — | — | <code>[5, 3, 4, 8]</code> | — |

回傳結果為 <code>[5, 3, 4, 8]</code>。

### 複雜度

- 時間複雜度：<code>O(n)</code>。走訪、串接與建立回傳陣列都各自是線性工作。
- 輔助空間複雜度：<code>O(n)</code>。兩個 List 會保存所有輸入元素；回傳陣列本身另需 <code>O(n)</code> 空間。

## 解法二：單一結果陣列與雙指標

API：<code>ResultArray2(int[] nums)</code>

### 設計說明

這個方法直接建立最終長度的 <code>result</code>，並把尚未完成的兩個邏輯陣列放在結果陣列的兩端：

- <code>arr1</code> 從索引 0 開始向右寫入。
- <code>arr2</code> 從最後一個索引開始向左寫入。
- <code>firstTailIndex</code> 指向 <code>arr1</code> 最新加入的元素。
- <code>secondTailIndex</code> 指向 <code>arr2</code> 最新加入的元素。

由於 <code>arr2</code> 是從右向左寫入，它在 <code>result</code> 中的暫存順序會與真正的追加順序相反。所有元素分配完成後，只要反轉 <code>secondTailIndex</code> 到陣列末端的區段，就能得到正確的 <code>arr1 + arr2</code>。

寫入新元素時必須先移動指標再賦值：

- 加入 <code>arr1</code>：先增加 <code>firstTailIndex</code>。
- 加入 <code>arr2</code>：先減少 <code>secondTailIndex</code>。

這能避免覆寫目前正在用來比較的尾端元素。

### 範例演示

使用 <code>nums = [10, 20, 30, 5, 6]</code>。底線代表尚未使用的位置：

| 步驟 | 比較與動作 | result 暫存狀態 | 邏輯 arr1 / arr2 |
| --- | --- | --- | --- |
| 初始化 | 左端放 10，右端放 20 | <code>[10, _, _, _, 20]</code> | <code>[10] / [20]</code> |
| 加入 30 | <code>10 &gt; 20</code> 不成立，向左寫入 arr2 | <code>[10, _, _, 30, 20]</code> | <code>[10] / [20, 30]</code> |
| 加入 5 | <code>10 &gt; 30</code> 不成立，向左寫入 arr2 | <code>[10, _, 5, 30, 20]</code> | <code>[10] / [20, 30, 5]</code> |
| 加入 6 | <code>10 &gt; 5</code>，向右寫入 arr1 | <code>[10, 6, 5, 30, 20]</code> | <code>[10, 6] / [20, 30, 5]</code> |
| 反轉 arr2 區段 | 反轉 <code>[5, 30, 20]</code> | <code>[10, 6, 20, 30, 5]</code> | 完成 |

回傳結果為 <code>[10, 6, 20, 30, 5]</code>。

### 複雜度

- 時間複雜度：<code>O(n)</code>。分配流程與最後的區段反轉都是線性時間。
- 輔助空間複雜度：排除必須回傳的 <code>result</code> 後為 <code>O(1)</code>；結果陣列本身占用 <code>O(n)</code> 空間。

## 兩種解法比較

| 解法 | 狀態表示 | 優點 | 時間 | 額外空間（不含回傳值） |
| --- | --- | --- | --- | --- |
| <code>ResultArray</code> | 兩個 <code>List&lt;int&gt;</code> | 最貼近題目敘述，容易理解 | <code>O(n)</code> | <code>O(n)</code> |
| <code>ResultArray2</code> | 單一結果陣列、左右雙指標 | 不需要兩個額外集合 | <code>O(n)</code> | <code>O(1)</code> |

若重點是快速理解規則，第一種解法較直觀；若希望利用回傳陣列本身保存中間狀態，第二種解法能減少額外配置，但需要維護雙指標與反轉區段的不變量。

## 可執行驗證

專案目前沒有獨立測試專案，因此 <code>Main</code> 是 deterministic console harness。五組案例涵蓋：

- 兩個官方範例。
- 最小長度輸入。
- 兩個比較分支多次切換。
- <code>arr2</code> 連續追加後的反轉順序。

每組案例會對兩個 API 使用獨立輸入副本，比對手工推導的 Expected 陣列。任一解法失敗時，程式會輸出 <code>FAIL</code> 並設定非零結束碼。

### 建置與執行

請在本 README 所在目錄執行：

~~~bash
dotnet restore leetcode_3069/leetcode_3069.csproj
dotnet build leetcode_3069/leetcode_3069.csproj --nologo
dotnet run --no-build --project leetcode_3069/leetcode_3069.csproj
~~~

XML 文件與格式可使用更嚴格的檢查：

~~~bash
dotnet build leetcode_3069/leetcode_3069.csproj --nologo -p:GenerateDocumentationFile=true -p:TreatWarningsAsErrors=true
dotnet format leetcode_3069/leetcode_3069.csproj --verify-no-changes --no-restore
git diff --check
~~~

### 最新驗證輸出

以下內容直接來自完成修改後的 <code>dotnet run --no-build --project leetcode_3069/leetcode_3069.csproj</code>：

~~~text
[官方範例 1] Input: [2, 1, 3] | Expected: [2, 3, 1] | Actual: M1=[2, 3, 1], M2=[2, 3, 1] | PASS
[官方範例 2] Input: [5, 4, 3, 8] | Expected: [5, 3, 4, 8] | Actual: M1=[5, 3, 4, 8], M2=[5, 3, 4, 8] | PASS
[最小長度] Input: [1, 2] | Expected: [1, 2] | Actual: M1=[1, 2], M2=[1, 2] | PASS
[多次切換分配方向] Input: [10, 20, 30, 5, 6] | Expected: [10, 6, 20, 30, 5] | Actual: M1=[10, 6, 20, 30, 5], M2=[10, 6, 20, 30, 5] | PASS
[arr2 連續追加與反轉順序] Input: [1, 100, 90, 80, 70] | Expected: [1, 100, 90, 80, 70] | Actual: M1=[1, 100, 90, 80, 70], M2=[1, 100, 90, 80, 70] | PASS
總結：10/10 項驗證通過
~~~

## 專案結構

~~~text
.
├── leetcode_3069/
│   ├── Program.cs
│   └── leetcode_3069.csproj
├── docs/
│   └── readme-template.md
└── README.md
~~~

<code>bin/</code> 與 <code>obj/</code> 是 .NET 建置產物，不應提交。演算法方法只負責計算與回傳結果，console 輸出集中在 <code>Main</code> 的驗證流程。
