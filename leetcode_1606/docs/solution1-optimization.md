# 解法一：空閒伺服器查找優化

保留原本的「模擬 + SortedSet + 優先佇列」，僅將空閒伺服器的
`foreach` 搜尋改成區間最小值查找，不另增解法三。解法二維持原樣。

每個請求仍先釋放 `finishTime <= arrival[i]` 的伺服器，再從 `i % k`
開始選擇空閒伺服器；若右側沒有空閒伺服器，就繞回最小編號。

```csharp
int serverId = available.Max < target
    ? available.Min
    : available.GetViewBetween(target, k - 1).Min;
```

例如 `available = {0, 2, 4}`：`target = 3` 時選擇 4；`target = 5`
時繞回 0。程式已在前方排除空集合，所以可以安全讀取 `Min`、`Max`。

這等同參考 Java 程式的 `TreeSet.ceiling(target)`，找不到時再取最小編號。
Java 參考：[宮水三葉的 LeetCode 題解](https://leetcode.cn/problems/find-servers-that-handled-most-number-of-requests/solutions/1379851/by-ac_oier-zgm6/)。

在 .NET 10 中，`GetViewBetween` 定位區間與 `Min` 查找均沿平衡樹路徑進行。
避免呼叫區間視圖的 `Count`，因為視圖可能為了重新計數而遍歷區間。
實作依據：[.NET 10 TreeSubSet 原始碼](https://github.com/dotnet/runtime/blob/v10.0.0/src/libraries/System.Collections/src/System/Collections/Generic/SortedSet.TreeSubSet.cs)。

單次選擇由最壞 O(k) 改為 O(log k)。令 n 為請求數，整體時間由
O(nk + k log k) 改為 O((n + k) log k)，額外空間維持 O(k)；
k = 1 時整體時間為 O(n)。

## 驗證

從此問題目錄執行：

```powershell
dotnet build leetcode_1606/leetcode_1606.csproj
dotnet run --project checks/checks.csproj
```

獨立檢查程式直接編譯目前的 Program.cs，以 CheckRunner 作為入口，不執行主專案 Main 的範例，也不需測試套件。
兩種解法都必須通過 12 組固定案例（官方範例、單伺服器、丟棄請求、繞回、
並列、完成時間等於到達時間、右側後繼查找、伺服器多於請求，以及兩組合法數值上界），以及固定種子
1606 的 500 組暴力模擬比對。答案排序後比較，避免依賴回傳順序。
另以 100,000 台伺服器、100,000 個負載為 1 的請求檢查大量空閒伺服器情境；
這是正確性與規模檢查，不以耗時門檻作為測試斷言。
只需執行小型比對時，可加上 `-- --skip-large`。
