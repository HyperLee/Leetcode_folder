# leetcode_3439 重新安排會議得到最多空餘時間 I

## 題目簡介

本專案為 LeetCode 3439「重新安排會議得到最多空餘時間 I」的 C# 解法。

- 題目連結：[LeetCode 英文](https://leetcode.com/problems/reschedule-meetings-for-maximum-free-time-i/description/?envType=daily-question&envId=2025-07-09)
- 題目連結：[LeetCode 中文](https://leetcode.cn/problems/reschedule-meetings-for-maximum-free-time-i/description/?envType=daily-question&envId=2025-07-09)

## 題目說明

給定一個事件總時長 `eventTime`（事件發生於 t = 0 到 t = eventTime），以及兩個長度為 n 的整數陣列 `startTime` 和 `endTime`，分別代表 n 個不重疊會議的開始與結束時間。

你最多可以重新安排 k 個會議的開始時間（會議長度不變），以最大化事件期間內最長的連續空閒時間。

- 所有會議的相對順序必須保持不變，且會議之間不得重疊。
- 會議不能被安排到事件之外的時間。

請返回重新安排會議後，能獲得的最大連續空閒時間。

## 專案結構

- `Program.cs`：主程式與解題邏輯。
- `leetcode_3439.csproj`：C# 專案檔。
- 其他為 .NET 產生的建構檔案。

## 解題思路

### 1. 空閒區間的劃分

n 個會議會把整個事件分割成 n+1 個空閒區間：
- 最左側：從 0 到第一個會議開始。
- 最右側：從最後一個會議結束到 eventTime。
- 中間：每兩個相鄰會議之間。

### 2. 為什麼要取 k+1 個空閒區間？

每移動（重新安排）1 個會議，就能把原本被該會議分隔的 2 個空閒區間合併成 1 個。

- 例如：原本有空閒A、會議1、空閒B，若移動會議1，A 和 B 就能合併。
- 所以移動 k 個會議，最多能合併出 k+1 個連續空閒區間。
- 換句話說，只有把中間的 k 個會議都移開，才能讓 k+1 個空閒區間完全連在一起。

因此，最佳策略就是在所有空閒區間中，找出連續 k+1 個空閒區間合併後的總長度最大值。

### 3. 空閒時間的計算

空閒區間的長度計算方式如下：
- 若為最左側（i == 0）：`startTime[0]`
- 若為最右側（i == n）：`eventTime - endTime[n-1]`
- 其他（中間）：`startTime[i] - endTime[i-1]`

### 4. 滑動視窗解法

- 使用一個長度為 k+1 的滑動視窗，從左到右遍歷所有空閒區間。
- 每次計算視窗內 k+1 個空閒區間的總長度，並維護最大值。
- 這樣能有效率地找出最大連續空閒時間。

#### 程式碼片段

```csharp
for (int i = 0; i <= n; i++)
{
    s += GetGap(i, eventTime, startTime, endTime);
    if (i < k)
    {
        continue;
    }
    ans = Math.Max(ans, s);
    s -= GetGap(i - k, eventTime, startTime, endTime);
}
```

## 執行方式

1. 使用 .NET 8 或以上版本。
2. 在專案目錄下執行：

```sh
dotnet run --project leetcode_3439/leetcode_3439.csproj
```

## 測試資料

程式內建範例：
- `eventTime = 5`
- `k = 1`
- `startTime = [1, 3]`
- `endTime = [2, 4]`
- 預期輸出：2

## 參考
- [C# 官方文件](https://learn.microsoft.com/zh-tw/dotnet/csharp/)
- [LeetCode 題解討論區](https://leetcode.cn/problems/reschedule-meetings-for-maximum-free-time-i/solutions/)
