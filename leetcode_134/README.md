# LeetCode 134 - Gas Station

使用 C# 實作 LeetCode 134「加油站」。專案目前在 [`leetcode_134/Program.cs`](leetcode_134/Program.cs) 內提供貪心單次遍歷解法，並在 `Main` 內建可執行測資；執行 `dotnet run` 可以直接看到每個案例的輸入、預期答案、實際結果與通過狀態。

## 題目說明

- 題目連結: <https://leetcode.com/problems/gas-station/description/>
- 題目中文: <https://leetcode.cn/problems/gas-station/description/>

環形路線上有 `n` 個加油站，第 `i` 個加油站可以提供 `gas[i]` 單位汽油。從第 `i` 個加油站行駛到下一個加油站 `i + 1`，需要消耗 `cost[i]` 單位汽油。

車子的油箱容量無限，出發時油箱為空。請判斷是否存在一個起始加油站，使車子可以依順時針方向繞完整個環路一圈。

- 若可以完成一圈，回傳起始加油站的索引。
- 若不可能完成一圈，回傳 `-1`。
- 若答案存在，題目保證答案唯一。

## 限制條件

根據 LeetCode 原題：

- `n == gas.length == cost.length`
- `1 <= n <= 10^5`
- `0 <= gas[i], cost[i] <= 10^4`
- 若存在可行答案，答案保證唯一

這個 Console 專案的 `CanCompleteCircuit` 方法假設輸入符合上述限制，因此不另外處理 `null`、長度不一致或空陣列輸入。

## 解題概念與出發點

這題的核心是把每一段路程轉成「淨汽油變化」：

```text
stationBalance = gas[i] - cost[i]
```

如果從某個候選起點出發，一路累積到第 `i` 站時油量變成負數，代表這個候選起點無法到達下一站。更重要的是，這段失敗區間中的任何站也不可能成為有效起點。

原因如下：

1. 假設目前候選起點是 `start`，從 `start` 走到 `i` 時第一次出現油量不足。
2. 在 `start` 到 `i - 1` 的每個中間位置，從 `start` 走到那裡時油量都沒有低於 `0`。
3. 若改從中間某個位置 `k` 出發，等於少拿了 `start` 到 `k - 1` 這段累積下來的非負油量。
4. 少了這段非負油量後，從 `k` 走到 `i` 只會更不利，因此也無法跨過第 `i` 站。

所以一旦目前油量變負，可以直接把下一個位置 `i + 1` 當成新的候選起點，不需要回頭逐一嘗試失敗區間內的其他站。

最後還要看整圈總油量是否足夠：

- 若 `sum(gas) >= sum(cost)`，一定存在某個起點可以完成環路。
- 若 `sum(gas) < sum(cost)`，總汽油量連總消耗都付不起，不論從哪裡出發都會失敗。

## 解法一：貪心單次遍歷

### 設計說明

[`Program.CanCompleteCircuit`](leetcode_134/Program.cs) 使用三個狀態完成判斷：

| 變數 | 用途 |
| --- | --- |
| `start` | 目前候選起點 |
| `totalBalance` | 所有站點的總淨油量，用來判斷整體是否可能完成一圈 |
| `tankBalance` | 從目前候選起點出發，到目前站點為止的油箱剩餘量 |

遍歷每個站點時：

1. 計算 `stationBalance = gas[i] - cost[i]`。
2. 把 `stationBalance` 加到 `totalBalance`，記錄整圈總油量是否足夠。
3. 把 `stationBalance` 加到 `tankBalance`，模擬從候選起點開到目前位置的油量。
4. 若 `tankBalance < 0`，代表目前候選起點與這段前綴都不可能成功，因此把 `start` 更新成 `i + 1`，並重置 `tankBalance = 0`。
5. 遍歷結束後，若 `totalBalance >= 0` 回傳 `start`，否則回傳 `-1`。

### 為什麼這樣設計

這個貪心策略同時處理兩個問題：

- **局部候選起點如何移動**：一旦目前候選起點失敗，就整段跳過，直接測試下一站。
- **整體是否有解**：只看候選起點還不夠，必須確認總油量能支付總成本。

這樣不需要暴力嘗試每個起點，也不需要額外陣列保存每一段的淨油量。

### 複雜度

- 時間複雜度: `O(n)`，每個加油站只拜訪一次。
- 空間複雜度: `O(1)`，只使用固定數量的整數狀態。

### 範例演示流程：可完成環路

以 `gas = [1, 2, 3, 4, 5]`、`cost = [3, 4, 5, 1, 2]` 為例：

| i | gas[i] | cost[i] | stationBalance | tankBalance | totalBalance | 判斷 |
| --- | ---: | ---: | ---: | ---: | ---: | --- |
| 0 | 1 | 3 | -2 | -2 | -2 | 油量不足，`start = 1`，重置油箱 |
| 1 | 2 | 4 | -2 | -2 | -4 | 油量不足，`start = 2`，重置油箱 |
| 2 | 3 | 5 | -2 | -2 | -6 | 油量不足，`start = 3`，重置油箱 |
| 3 | 4 | 1 | 3 | 3 | -3 | 候選起點 3 暫時可行 |
| 4 | 5 | 2 | 3 | 6 | 0 | 總油量剛好足夠 |

遍歷結束時 `totalBalance = 0`，代表總油量足以完成一圈，因此回傳目前候選起點 `3`。

從第 `3` 站實際走一圈：

```text
start = 3
到站 3: tank = 0 + gas[3] - cost[3] = 3
到站 4: tank = 3 + gas[4] - cost[4] = 6
到站 0: tank = 6 + gas[0] - cost[0] = 4
到站 1: tank = 4 + gas[1] - cost[1] = 2
到站 2: tank = 2 + gas[2] - cost[2] = 0
```

整段過程油量都沒有低於 `0`，所以答案是 `3`。

### 範例演示流程：不可能完成

以 `gas = [2, 3, 4]`、`cost = [3, 4, 3]` 為例：

| i | gas[i] | cost[i] | stationBalance | tankBalance | totalBalance | 判斷 |
| --- | ---: | ---: | ---: | ---: | ---: | --- |
| 0 | 2 | 3 | -1 | -1 | -1 | 油量不足，`start = 1`，重置油箱 |
| 1 | 3 | 4 | -1 | -1 | -2 | 油量不足，`start = 2`，重置油箱 |
| 2 | 4 | 3 | 1 | 1 | -1 | 候選起點 2 暫時可行 |

雖然最後候選起點停在 `2`，但 `totalBalance = -1`，代表總汽油量比總消耗少 `1`。因此任何起點都不可能完成一圈，回傳 `-1`。

### 範例演示流程：重置候選起點

以 `gas = [5, 1, 2, 3, 4]`、`cost = [4, 4, 1, 5, 1]` 為例：

| i | stationBalance | tankBalance | totalBalance | start | 判斷 |
| --- | ---: | ---: | ---: | ---: | --- |
| 0 | 1 | 1 | 1 | 0 | 從 0 出發暫時可行 |
| 1 | -3 | -2 | -2 | 2 | 油量不足，跳過 0 到 1 |
| 2 | 1 | 1 | -1 | 2 | 從 2 出發暫時可行 |
| 3 | -2 | -1 | -3 | 4 | 油量不足，跳過 2 到 3 |
| 4 | 3 | 3 | 0 | 4 | 從 4 出發可行，總油量足夠 |

最後回傳 `4`。這個案例展示了貪心法的重點：當某段前綴讓油箱變負時，不需要逐一回頭測試區間內的起點。

## 可執行範例資料

主程式目前內建 5 組測資：

1. `gas = [1, 2, 3, 4, 5]`、`cost = [3, 4, 5, 1, 2]`，預期 `3`
2. `gas = [2, 3, 4]`、`cost = [3, 4, 3]`，預期 `-1`
3. `gas = [5]`、`cost = [4]`，預期 `0`
4. `gas = [1]`、`cost = [2]`，預期 `-1`
5. `gas = [5, 1, 2, 3, 4]`、`cost = [4, 4, 1, 5, 1]`，預期 `4`

每個案例都會輸出：

- 案例名稱
- `gas` 與 `cost`
- 預期答案
- 實際答案
- 是否通過比對
- 簡短說明

## 建置與執行

### 建置

```bash
dotnet build leetcode_134/leetcode_134.csproj
```

本地驗證結果：

```text
建置成功。
    0 個警告
    0 個錯誤
```

### 執行範例

```bash
dotnet run --project leetcode_134/leetcode_134.csproj
```

> [!NOTE]
> 在這台機器上執行 `dotnet` 時，SDK 額外印出了 `CSSM_ModuleLoad(): One or more parameters passed to a function were not valid.`。這不是本專案邏輯輸出，也不影響建置成功或演算法結果。

主程式輸出如下：

```text
LeetCode 134 - Gas Station
==========================
[Example 1]
Gas: [1, 2, 3, 4, 5]
Cost: [3, 4, 5, 1, 2]
Expected: 3
Result: 3
Pass: PASS
Notes: Start at index 3, then the tank never drops below zero.

[Example 2]
Gas: [2, 3, 4]
Cost: [3, 4, 3]
Expected: -1
Result: -1
Pass: PASS
Notes: The total gas is less than the total cost, so no full circuit exists.

[Single station success]
Gas: [5]
Cost: [4]
Expected: 0
Result: 0
Pass: PASS
Notes: The only station gives enough gas to return to itself.

[Single station failure]
Gas: [1]
Cost: [2]
Expected: -1
Result: -1
Pass: PASS
Notes: The only station cannot pay for its outgoing route.

[Reset start after failed prefix]
Gas: [5, 1, 2, 3, 4]
Cost: [4, 4, 1, 5, 1]
Expected: 4
Result: 4
Pass: PASS
Notes: Prefixes that make the tank negative cannot contain a valid start.
```

### 空白檢查

```bash
git diff --check
```

本地驗證結果沒有輸出，代表目前 diff 沒有多餘空白或換行問題。

## 檔案結構

```text
.
├── README.md
├── docs/
│   └── readme-template.md
└── leetcode_134/
    ├── Program.cs
    └── leetcode_134.csproj
```

本專案僅用於 LeetCode 題目練習與學習參考。
