這個檔案說明如何在 Visual Studio Code 中直接啟動偵錯（不需在執行時輸入名稱）。

步驟：
1. 打開工作區 `leetcode_3541`。 
2. 按 F5 或到偵錯檢視並啟動 `Launch leetcode_3541`。這個設定會先執行 `build` 任務（使用 `dotnet build`），然後啟動輸出程式 `bin/Debug/net8.0/leetcode_3541.dll`。

備註：
- 若專案使用不同的 target framework 或輸出路徑，請調整 `launch.json` 中的 `program` 路徑。
- 若要讓 `.vscode/launch.json` 使用專案根目錄變數，可把 `${workspaceFolder}/leetcode_3541` 改為 `${workspaceFolder}`（取決於你開啟的資料夾層級）。
