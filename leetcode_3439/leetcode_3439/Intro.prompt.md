
說明題目以及題目需求
再來說 解題出發點
最後詳細介紹解法解題

> Copilot: ### 題目說明
> 
> 本題為 LeetCode 3439. 重新安排會議得到最多空餘時間 I。  
> 給定一個事件總時長 `eventTime`（事件發生於 t = 0 到 t = eventTime），以及兩個長度為 n 的整數陣列 `startTime` 和 `endTime`，分別代表 n 個不重疊會議的開始與結束時間。  
> 你最多可以重新安排 k 個會議的開始時間（會議長度不變），以最大化事件期間內最長的連續空閒時間。  
> **限制條件：**  
> - 所有會議的相對順序必須保持不變，且會議之間不得重疊。
> - 會議不能被安排到事件之外的時間。
> - 請返回重新安排會議後，能獲得的最大連續空閒時間。
> 
> ---
> 
> ### 解題出發點
> 
> - 事件期間內，n 個會議會把整個時間分割成 n+1 個空閒區間（包含最左、最右與中間）。
> - 重新安排 k 個會議的目標，是讓 k+1 個空閒區間合併成一段，取得最大長度。
> - 只要維持會議順序且不重疊，會議可以盡量往左或往右移動，讓空閒區間合併。
> - 問題轉化為：在 n+1 個空閒區間中，找出連續 k+1 個空閒區間的總長度最大值。
> 
> ---
> 
> ### 詳細解法說明
> 
> 1. **計算空閒區間：**  
>    - 空閒區間有 n+1 個，分別為：
>      - 最左側：從 0 到第一個會議開始。
>      - 最右側：從最後一個會議結束到 eventTime。
>      - 中間：兩個相鄰會議之間。
> 2. **滑動視窗法：**  
>    - 用滑動視窗遍歷所有連續 k+1 個空閒區間，計算其總長度，並取最大值。
>    - 每次視窗右移一格，減去最左側空閒區間，加上新進來的空閒區間。
> 3. **程式碼實作重點：**
>    - `GetGap` 函式計算第 i 個空閒區間的長度。
>    - 主程式用 for 迴圈與滑動視窗，維護目前視窗內的空閒總長度，並更新最大值。
> 
> ---
> 
> #### 程式碼片段（已在你的程式碼中）
> 
> ````csharp
> // ...existing code...
> public int MaxFreeTime(int eventTime, int k, int[] startTime, int[] endTime)
> {
>     int ans = 0;
>     int s = 0;
>     int n = startTime.Length;
>     // 使用滑動視窗，合併連續 k+1 個空閒區間
>     for (int i = 0; i <= n; i++)
>     {
>         s += GetGap(i, eventTime, startTime, endTime);
>         if (i < k)
>         {
>             continue;
>         }
>         ans = Math.Max(ans, s);
>         s -= GetGap(i - k, eventTime, startTime, endTime);
>     }
>     return ans;
> }
> // ...existing code...
> ````
> 
> ---
>
> #### 為什麼滑動視窗是找出 k + 1 個 空閑區間
> 因為每移動（重新安排）1 個會議，就能把原本被該會議分隔的 2 個空閒區間合併成 1 個，所以最多移動 k 個會議時，最多可以把 **k + 1 個連續的空閒區間合併成一段**，這段合併後的空閒時間就是你能取得的最大連續空閒時間。
>
>換句話說：
>- n 個會議會把整個事件分成 n+1 個空閒區間。
>- 每移動 1 個會議，可以讓 2 個空閒區間合併。
>- 移動 k 個會議，最多能合併出 k+1 個連續空閒區間。
>- 所以用滑動視窗找出所有連續 k+1 個空閒區間的總長度，取最大值，就是答案。

這是因為只有把中間的 k 個會議都移開，才能讓 k+1 個空閒區間完全連在一起。

> ### 小結
> 
> 本題的核心在於將「重新安排會議」轉化為「合併空閒區間」的問題，利用滑動視窗快速計算所有可能的最大連續空閒時間，達到最佳化目標。
