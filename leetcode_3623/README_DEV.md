# Debug / Run config (VS Code)

✅ 這份設定讓你在 VS Code 按下 F5 (或選擇 Run) 時，不會被要求輸入任何名稱，可以直接執行與偵錯。

主要項目：
- `launch.json`：包含一個 `.NET: Launch leetcode_3623` 的偵錯設定，會先呼叫 `build`，然後啟動 bin/Debug/net10.0/leetcode_3623.dll。
- `tasks.json`：包含 `build` 與 `run` 兩個 tasks。`build` 為預設的建置任務。

如何使用：
1. 打開 `leetcode_3623` 專案資料夾於 VS Code。
2. 按 `F5` (Start Debugging) 或選擇左側 Run 面板，選 ` .NET: Launch leetcode_3623` 開始偵錯。
3. 若要直接執行 (不偵錯)：使用 `Terminal` → `Run Task...` → 選 `run`，或使用 `dotnet run --project leetcode_3623/leetcode_3623.csproj`。

常見調整：
- 如果你改變 `TargetFramework`（例如 `net7.0` 或 `net8.0`），請更新 `launch.json` 中 `program` 的路徑 `bin/Debug/{TargetFramework}/leetcode_3623.dll`。
- 若要從 Release 組態執行，請在 `tasks.json` 中的 `args` 把 `Debug` 改成 `Release`，並更新 `launch.json` 的 `program` 路徑。

---

若要我幫你把 `launch.json` 支援多個專案或加入 Attach config / 複合啟動 (compound launch)，告訴我你想要哪些功能，我可以替你加上。
