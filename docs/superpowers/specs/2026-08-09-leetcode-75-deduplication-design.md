# LeetCode 75 重複專案整理設計

## 目標

將同樣解答 LeetCode 75「Sort Colors」的 `leetcode_75` 與 `leetcode_075` 整理為單一專案。保留命名、歷史、文件及驗證較完整的 `leetcode_075`，避免根目錄索引繼續出現兩筆題號 75。

本次只做本機修改，不建立 commit、不 push，也不變更其他題目。

## 保留內容

- 保留 `leetcode_075` 的專案名稱、目錄結構、.NET 10 設定、VS Code 設定與既有專案歷史。
- 將 `leetcode_075.SortColors4` 的標準荷蘭國旗三指標演算法提升為唯一公開解題入口 `SortColors`。
- 保留原地修改契約：方法接受 `int[] nums`，不使用內建排序，且不回傳新陣列。
- 演算法維持 `O(n)` 時間、`O(1)` 額外空間，並以 `low`、`mid`、`high` 維護已分類與未分類區間。

## 移除內容

- 從 `leetcode_075/leetcode_075/Program.cs` 移除泡沫排序、兩種計數排序、左右雙指標變體及舊的 `SortColors4` 名稱，只留下標準荷蘭國旗解法。
- 移除根 `README.md` 中指向 `leetcode_75` 的重複題號 75 索引列。
- 移除 `leetcode_75` 內所有受 Git 追蹤的專案檔案；不使用遞迴刪除命令。
- 本機既有的 ignored `bin/`、`obj/` 產物不屬於版本控制內容。受限於禁止批次刪除的規則，本次不遞迴清除；若需要讓本機空目錄完全消失，將明確列出殘留物並交由使用者手動處理。

## 執行與驗證設計

`Main` 保留 deterministic Expected/Actual/PASS-FAIL 驗證，但只呼叫唯一的 `SortColors`。案例至少涵蓋：

- 兩個官方範例。
- 單一元素。
- 已排序輸入。
- 反向排列。
- 全部相同。
- 只含兩種顏色，以及需要從右端換回尚未分類值的案例。

只要任一案例失敗，程式應設定非零結束碼，使主控台驗證能作為可靠的自動化 gate。README 將同步改成單一推薦解法，並以修改後程式的最新執行輸出作為文件內容。

完成後依序驗證：

1. `dotnet restore leetcode_075/leetcode_075.csproj`
2. `dotnet build leetcode_075/leetcode_075.csproj --no-restore --nologo`
3. `dotnet run --project leetcode_075/leetcode_075.csproj --no-build`
4. README transcript 與實際輸出一致。
5. 根 README 只剩一筆題號 75，且 Git 追蹤的專案不再有正規化題號重複。
6. `git diff --check` 通過，Git 差異只涵蓋本規格所列範圍。

## 安全與交付邊界

- 不執行遞迴或批次刪除。
- 不修改其他 LeetCode 題目。
- 不建立 commit、不 push、不建立 PR。
- 若受 Git 追蹤檔案移除後仍有 ignored 建置產物，交付時清楚說明，而不擴大刪除權限。
