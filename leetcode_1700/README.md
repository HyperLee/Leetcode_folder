# LeetCode 1700：無法吃午餐的學生數量

![LeetCode](https://img.shields.io/badge/LeetCode-1700-FFA116?logo=leetcode&logoColor=black)
![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)

這是一個以 .NET 10 主控台程式實作的教學專案。專案保留一個利用偏好數量直接求解的高效率方法，並加入一個忠實重現題目流程的佇列模擬方法；執行程式即可比較兩種解法，並驗證它們不會修改輸入陣列。

- [題目說明](#題目說明)
- [解題概念與出發點](#解題概念與出發點)
- [解法一：偏好人數計數法](#解法一偏好人數計數法)
- [解法二：佇列模擬法](#解法二佇列模擬法)
- [兩種解法比較](#兩種解法比較)
- [建置與執行](#建置與執行)
- [實際執行結果](#實際執行結果)

## 題目說明

學校午餐提供兩種三明治：

- `0`：圓形三明治。
- `1`：方形三明治。

所有學生排成一個佇列，每位學生只偏好其中一種三明治。三明治依固定順序疊放，索引 `0` 表示目前最上方的三明治。

每一輪會檢查隊首學生：

1. 若學生偏好最上方的三明治，學生取走三明治並離開佇列。
2. 若學生不偏好該三明治，學生移到佇列尾端，三明治維持不動。
3. 當佇列中的所有學生都不偏好最上方的三明治時，流程停止。

輸入為學生偏好陣列 `students` 與三明治順序陣列 `sandwiches`，輸出為最後無法吃到午餐的學生人數。

### 限制條件

- `1 <= students.Length, sandwiches.Length <= 100`
- `students.Length == sandwiches.Length`
- `students[i]` 只能是 `0` 或 `1`
- `sandwiches[i]` 只能是 `0` 或 `1`

本專案依照 LeetCode 的合法輸入契約實作，不另外處理空陣列、長度不同或含有其他數字的輸入。

題目連結：[1700. Number of Students Unable to Eat Lunch](https://leetcode.com/problems/number-of-students-unable-to-eat-lunch/)

## 解題概念與出發點

最直觀的做法是完整模擬學生佇列：不喜歡目前三明治的學生就移到隊尾，直到有人取餐，或整個佇列輪轉一圈仍無人取餐。這正是本專案的第二種解法。

再進一步觀察，學生可以不限次數地移到隊尾，所以只要佇列中「還有任何一位」學生偏好目前的三明治，該學生最終一定能輪轉到隊首並取餐。換句話說，決定流程能否繼續的不是學生排列順序，而是偏好 `0` 與 `1` 的剩餘人數。這個觀察將佇列模擬簡化成第一種計數法。

兩種解法共同遵守以下契約：

- 三明治順序不可改變。
- 回傳尚未離開佇列的學生人數。
- 不修改 `students` 或 `sandwiches` 輸入陣列。

## 解法一：偏好人數計數法

API：`CountStudents(int[] students, int[] sandwiches)`

### 設計步驟

1. 將 `students` 的元素加總，得到偏好方形三明治 `1` 的人數。
2. 用學生總數減去方形偏好人數，得到偏好圓形三明治 `0` 的人數。
3. 依序讀取每一份三明治：
   - 若目前是 `0` 且仍有圓形偏好的學生，將圓形人數減一。
   - 若目前是 `1` 且仍有方形偏好的學生，將方形人數減一。
   - 若對應偏好人數已是零，代表剩餘學生全部拒絕目前三明治，立即停止。
4. 回傳兩種偏好剩餘人數的總和。

### 為什麼正確

只要仍有學生偏好目前的三明治，其他學生就能依序移到隊尾，直到該學生抵達隊首並取餐。因此每成功處理一份三明治，只需要扣除相同偏好的學生人數。當目前三明治對應的偏好人數為零時，剩餘學生無論如何輪轉都不會取走它，流程必然停止；此時剩餘偏好總數正好就是無法用餐的人數。

### 複雜度與輸入副作用

- 時間複雜度：O(n)。統計學生偏好與掃描三明治各進行一次。
- 額外空間複雜度：O(1)。只保存兩種偏好的剩餘人數。
- 輸入副作用：無，不會修改兩個輸入陣列。

### 範例演示：官方案例二

`students = [1, 1, 1, 0, 0, 1]`，初始圓形偏好為 2 人，方形偏好為 4 人。

| 步驟 | 目前三明治 | 圓形剩餘 | 方形剩餘 | 判斷 |
| ---: | ---: | ---: | ---: | --- |
| 初始 | - | 2 | 4 | 尚未取餐 |
| 1 | 1 | 2 | 3 | 一位方形偏好學生取餐 |
| 2 | 0 | 1 | 3 | 一位圓形偏好學生取餐 |
| 3 | 0 | 0 | 3 | 最後一位圓形偏好學生取餐 |
| 4 | 0 | 0 | 3 | 已無圓形偏好學生，停止 |

最後仍有 3 位方形偏好的學生，但頂端是無人偏好的圓形三明治，因此答案是 `3`。

## 解法二：佇列模擬法

API：`CountStudents2(int[] students, int[] sandwiches)`

### 設計步驟

1. 以 `students` 建立新的 `Queue<int>`，因此不會修改原始陣列。
2. 取出隊首學生，比較其偏好與目前三明治：
   - 相同：學生取餐，三明治索引前進，並把連續拒絕次數歸零。
   - 不同：學生加入隊尾，連續拒絕次數加一。
3. 若連續拒絕次數等於目前佇列人數，表示所有剩餘學生都已拒絕同一份頂端三明治，停止模擬。
4. 回傳佇列中的剩餘人數。

連續拒絕次數必須在每次成功取餐後歸零，因為三明治與佇列狀態都已改變，新狀態必須重新觀察完整一輪。

### 為什麼正確

這個方法逐步執行題目定義的兩個合法動作：取餐離隊，或拒絕後移到隊尾，因此每一步的佇列狀態都與題目流程一致。若一整輪的學生都拒絕同一份三明治，繼續輪轉只會重複相同狀態，不可能再有人取餐；反之，只要有人成功取餐，就應改看下一份三明治並重新計算拒絕次數。

### 複雜度與輸入副作用

- 時間複雜度：最壞 O(n²)。每取走一份三明治前，可能需要輪轉目前的整個佇列。
- 額外空間複雜度：O(n)。需要保存學生佇列。
- 輸入副作用：無，佇列由輸入內容建立，不會修改輸入陣列。

### 範例演示：官方案例一

`students = [1, 1, 0, 0]`，`sandwiches = [0, 1, 0, 1]`。

| 步驟 | 隊首偏好 | 頂端三明治 | 動作 | 動作後學生佇列 |
| ---: | ---: | ---: | --- | --- |
| 1 | 1 | 0 | 拒絕並移到隊尾 | `[1, 0, 0, 1]` |
| 2 | 1 | 0 | 拒絕並移到隊尾 | `[0, 0, 1, 1]` |
| 3 | 0 | 0 | 取餐並離隊 | `[0, 1, 1]` |
| 4 | 0 | 1 | 拒絕並移到隊尾 | `[1, 1, 0]` |
| 5 | 1 | 1 | 取餐並離隊 | `[1, 0]` |
| 6 | 1 | 0 | 拒絕並移到隊尾 | `[0, 1]` |
| 7 | 0 | 0 | 取餐並離隊 | `[1]` |
| 8 | 1 | 1 | 取餐並離隊 | `[]` |

所有學生都成功取餐，因此答案是 `0`。

## 兩種解法比較

| 項目 | 偏好人數計數法 | 佇列模擬法 |
| --- | --- | --- |
| 方法 | `CountStudents` | `CountStudents2` |
| 核心觀察 | 學生順序不影響是否有人偏好頂端三明治 | 直接重現取餐與移到隊尾的規則 |
| 時間複雜度 | O(n) | 最壞 O(n²) |
| 額外空間 | O(1) | O(n) |
| 教學價值 | 展示如何從模擬推導出狀態壓縮 | 容易對照題意並觀察停止條件 |
| 是否修改輸入 | 否 | 否 |

## 可執行測試資料

`Main` 固定執行六組案例：兩個官方案例、單一學生成功、單一學生阻塞、全部相同，以及包含重複值且中途阻塞的案例。

每組案例對每種解法進行兩項檢查：

1. 實際回傳值等於 `Expected`。
2. 執行後的學生與三明治陣列都與執行前相同。

因此六組案例共有 `6 × 2 × 2 = 24` 項檢查。任一項失敗時，主控台會顯示 `FAIL`，而程序結束碼會設為 `1`。

## 專案結構

```text
leetcode_1700/
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_1700/
    ├── leetcode_1700.csproj
    └── Program.cs
```

## 建置與執行

需求：已安裝支援 `net10.0` 的 .NET 10 SDK。

從本 repository 根目錄執行：

```bash
dotnet restore leetcode_1700/leetcode_1700.csproj
dotnet build leetcode_1700/leetcode_1700.csproj --nologo
dotnet format leetcode_1700/leetcode_1700.csproj --verify-no-changes --no-restore
dotnet run --no-build --project leetcode_1700/leetcode_1700.csproj
```

目前沒有獨立的自動化測試專案；`Main` 中的 deterministic harness 與建置結果共同作為本專案的驗收檢查。

## 實際執行結果

以下內容來自 `dotnet run --no-build --project leetcode_1700/leetcode_1700.csproj` 的實際輸出：

```text
Case: 官方範例一
Students: [1, 1, 0, 0]
Sandwiches: [0, 1, 0, 1]
Expected: 0
CountStudents  Actual: 0 | Input preserved: True | PASS
CountStudents2 Actual: 0 | Input preserved: True | PASS

Case: 官方範例二
Students: [1, 1, 1, 0, 0, 1]
Sandwiches: [1, 0, 0, 0, 1, 1]
Expected: 3
CountStudents  Actual: 3 | Input preserved: True | PASS
CountStudents2 Actual: 3 | Input preserved: True | PASS

Case: 最小成功
Students: [0]
Sandwiches: [0]
Expected: 0
CountStudents  Actual: 0 | Input preserved: True | PASS
CountStudents2 Actual: 0 | Input preserved: True | PASS

Case: 最小阻塞
Students: [0]
Sandwiches: [1]
Expected: 1
CountStudents  Actual: 1 | Input preserved: True | PASS
CountStudents2 Actual: 1 | Input preserved: True | PASS

Case: 全部相同
Students: [1, 1, 1]
Sandwiches: [1, 1, 1]
Expected: 0
CountStudents  Actual: 0 | Input preserved: True | PASS
CountStudents2 Actual: 0 | Input preserved: True | PASS

Case: 重複值且中途阻塞
Students: [0, 1, 0, 1]
Sandwiches: [1, 1, 1, 0]
Expected: 2
CountStudents  Actual: 2 | Input preserved: True | PASS
CountStudents2 Actual: 2 | Input preserved: True | PASS

Summary: 24/24 checks passed.
```
