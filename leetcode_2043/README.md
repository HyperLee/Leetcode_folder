# LeetCode 2043 - Simple Bank System

C# 實作簡易銀行系統，支援轉帳、存款和提款等基本銀行交易操作。

## 問題描述

您被要求為一家受歡迎的銀行編寫程式，以自動化所有 incoming 交易（轉帳、存款和提款）。銀行有 n 個帳戶，編號從 1 到 n。每個帳戶的初始餘額存儲在 0 索引的整數陣列 `balance` 中，第 (i + 1) 個帳戶的初始餘額為 `balance[i]`。

執行所有有效的交易。一個交易是有效的，如果：

- 給定的帳戶號碼在 1 到 n 之間
- 從帳戶中提取或轉帳的金額小於或等於帳戶的餘額

### 實作要求

實作 `Bank` 類別：

- `Bank(long[] balance)` - 使用 0 索引的整數陣列 balance 初始化物件
- `bool Transfer(int account1, int account2, long money)` - 從編號為 account1 的帳戶轉帳 money 美元到編號為 account2 的帳戶。如果交易成功，返回 true，否則返回 false
- `bool Deposit(int account, long money)` - 將 money 美元存入編號為 account 的帳戶。如果交易成功，返回 true，否則返回 false
- `bool Withdraw(int account, long money)` - 從編號為 account 的帳戶提取 money 美元。如果交易成功，返回 true，否則返回 false

## 解題思路

### 方法：模擬

已有的帳號為 1 到 n，分別對三種操作進行分析：

#### Transfer 操作

轉帳操作需要驗證兩個條件：

1. **帳號有效性**：如果要進行操作的帳號不在已有的帳號中，即 `account1 > n` 或者 `account2 > n`，那麼交易無效
2. **餘額充足性**：如果帳號 account1 的餘額小於 money，那麼交易無效

交易有效時，我們將帳號 account1 的餘額減少 money，帳號 account2 的餘額增加 money。

```csharp
public bool Transfer(int account1, int account2, long money)
{
    // 驗證帳號是否有效
    if (account1 < 1 || account1 > balances.Length || account2 < 1 || account2 > balances.Length)
    {
        return false;
    }
    
    // 驗證餘額是否足夠
    if (balances[account1 - 1] < money)
    {
        return false;
    }
    
    // 執行轉帳
    balances[account1 - 1] -= money;
    balances[account2 - 1] += money;
    return true;
}
```

#### Deposit 操作

存款操作只需要驗證一個條件：

1. **帳號有效性**：如果要進行操作的帳號不在已有的帳號中，即 `account > n`，那麼交易無效

交易有效時，我們將帳號 account 的餘額增加 money。

```csharp
public bool Deposit(int account, long money)
{
    // 驗證帳號是否有效
    if (account < 1 || account > balances.Length)
    {
        return false;
    }
    
    // 執行存款
    balances[account - 1] += money;
    return true;
}
```

#### Withdraw 操作

提款操作需要驗證兩個條件：

1. **帳號有效性**：如果要進行操作的帳號不在已有的帳號中，即 `account > n`，那麼交易無效
2. **餘額充足性**：如果帳號 account 的餘額小於 money，那麼交易無效

交易有效時，我們將帳號 account 的餘額減少 money。

```csharp
public bool Withdraw(int account, long money)
{
    // 驗證帳號是否有效
    if (account < 1 || account > balances.Length)
    {
        return false;
    }
    
    // 驗證餘額是否足夠
    if (balances[account - 1] < money)
    {
        return false;
    }
    
    // 執行提款
    balances[account - 1] -= money;
    return true;
}
```

## 複雜度分析

- **時間複雜度**：O(1)，每次操作都是常數時間
- **空間複雜度**：O(n)，需要儲存 n 個帳戶的餘額

## 實作細節

### 索引轉換

由於題目中帳號編號從 1 開始，而陣列索引從 0 開始，因此在存取陣列時需要進行轉換：

```csharp
// 帳號 account 對應的陣列索引為 account - 1
balances[account - 1]
```

### 驗證順序

在執行操作前，應先驗證所有條件：

1. 先驗證帳號的有效性
2. 再驗證餘額的充足性
3. 最後執行實際的操作

這樣可以確保所有無效操作都被攔截，不會對系統狀態造成影響。

## 測試案例

### 官方測試案例

```csharp
Input:
["Bank", "withdraw", "transfer", "deposit", "transfer", "withdraw"]
[[[10, 100, 20, 50, 30]], [3, 10], [5, 1, 20], [5, 20], [3, 4, 15], [10, 50]]

Output:
[null, true, true, true, false, false]

說明：
Bank bank = new Bank([10, 100, 20, 50, 30]);
bank.Withdraw(3, 10);    // 返回 true，帳號 3 餘額為 20，提款 10 後變為 10
bank.Transfer(5, 1, 20); // 返回 true，帳號 5 餘額為 30，轉 20 給帳號 1
bank.Deposit(5, 20);     // 返回 true，帳號 5 餘額為 10，存款 20 後變為 30
bank.Transfer(3, 4, 15); // 返回 false，帳號 3 餘額只有 10，無法轉帳 15
bank.Withdraw(10, 50);   // 返回 false，帳號 10 不存在
```

### 自訂測試案例

程式中包含了額外的測試案例，涵蓋各種邊界情況：

- 餘額不足的轉帳/提款
- 無效的帳號（不存在或超出範圍）
- 成功的交易操作

## 執行程式

### 前置需求

- .NET 8.0 或更高版本

### 建置

```bash
cd leetcode_2043
dotnet build
```

### 執行

```bash
dotnet run
```

### 除錯

專案已配置好 VS Code 的 debugging 設定，可以直接在 VS Code 中按 F5 啟動除錯。

## 專案結構

```text
leetcode_2043/
├── leetcode_2043/
│   ├── Program.cs          # 主程式和 Bank 類別實作
│   └── leetcode_2043.csproj
├── .vscode/
│   ├── launch.json         # VS Code 除錯配置
│   └── tasks.json          # 建置任務配置
├── .editorconfig           # 程式碼風格配置
└── README.md               # 本檔案
```

## 相關連結

- [LeetCode 問題連結（英文）](https://leetcode.com/problems/simple-bank-system/)
- [LeetCode 問題連結（中文）](https://leetcode.cn/problems/simple-bank-system/)

## 關鍵要點

> [!TIP]
> 這道題目是一個經典的模擬題，重點在於正確處理邊界條件和狀態管理。

> [!NOTE]
> 帳號編號從 1 開始，但陣列索引從 0 開始，需要注意索引轉換。

> [!IMPORTANT]
> 在執行操作前務必先驗證所有條件，避免無效操作影響系統狀態。
