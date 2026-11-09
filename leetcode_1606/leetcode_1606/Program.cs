namespace leetcode_1606;

class Program
{
    /// <summary>
    /// 1606. Find Servers That Handled Most Number of Requests
    /// https://leetcode.com/problems/find-servers-that-handled-most-number-of-requests/description/
    ///
    /// English:
    /// You have k servers numbered from 0 to k-1 that are being used to handle multiple requests simultaneously.
    /// Each server has infinite computational capacity but cannot handle more than one request at a time.
    /// The requests are assigned to servers according to a specific algorithm:
    ///
    /// - The ith (0-indexed) request arrives.
    /// - If all servers are busy, the request is dropped (not handled at all).
    /// - If the (i % k)th server is available, assign the request to that server.
    /// - Otherwise, assign the request to the next available server (wrapping around the list of servers
    ///   and starting from 0 if necessary). For example, if the ith server is busy, try to assign the
    ///   request to the (i+1)th server, then the (i+2)th server, and so on.
    ///
    /// You are given a strictly increasing array arrival of positive integers, where arrival[i] represents
    /// the arrival time of the ith request, and another array load, where load[i] represents the load of
    /// the ith request (the time it takes to complete). Your goal is to find the busiest server(s).
    /// A server is considered busiest if it handled the most number of requests successfully among all the servers.
    ///
    /// Return a list containing the IDs (0-indexed) of the busiest server(s). You may return the IDs in any order.
    ///
    /// 繁體中文：
    /// 1606. 找出處理最多請求的伺服器
    /// https://leetcode.cn/problems/find-servers-that-handled-most-number-of-requests/description/
    ///
    /// 你有 k 台伺服器，編號從 0 到 k-1，用來同時處理多個請求。
    /// 每台伺服器都具有無限的運算能力，但同一時間不能處理超過一個請求。
    /// 請求會依照以下演算法分派給伺服器：
    ///
    /// - 第 i 個請求到達（索引從 0 開始）。
    /// - 如果所有伺服器都忙碌，則丟棄該請求（完全不處理）。
    /// - 如果編號為 i % k 的伺服器可用，則將該請求分派給它。
    /// - 否則，將該請求分派給編號順序中的下一台可用伺服器；必要時繞回編號 0 繼續尋找。
    ///   例如，如果第 i 台伺服器忙碌，則嘗試將請求分派給第 i+1 台，再嘗試第 i+2 台，依此類推。
    ///
    /// 給定一個由正整數組成且嚴格遞增的陣列 arrival，其中 arrival[i] 表示第 i 個請求的到達時間；
    /// 另有一個陣列 load，其中 load[i] 表示第 i 個請求的負載（完成該請求所需的時間）。
    /// 你的目標是找出最忙碌的伺服器。如果某台伺服器成功處理的請求數量為所有伺服器中最多，
    /// 則該伺服器被視為最忙碌的伺服器。
    ///
    /// 回傳一個清單，包含所有最忙碌伺服器的編號（從 0 開始）。你可以依任意順序回傳這些編號。
    /// </summary>
    /// <param name="args">命令列引數。</param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
