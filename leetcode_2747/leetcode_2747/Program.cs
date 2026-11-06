namespace leetcode_2747;

class Program
{
    /// <summary>
    /// 2747. Count Zero Request Servers
    /// https://leetcode.com/problems/count-zero-request-servers/description/
    /// 2747. 统计没有收到请求的服务器数目
    /// https://leetcode.cn/problems/count-zero-request-servers/description/
    ///
    /// English:
    /// You are given an integer n denoting the total number of servers and a 2D 0-indexed
    /// integer array logs, where logs[i] = [server_id, time] denotes that the server with
    /// id server_id received a request at time time.
    ///
    /// You are also given an integer x and a 0-indexed integer array queries.
    ///
    /// Return a 0-indexed integer array arr of length queries.length where arr[i]
    /// represents the number of servers that did not receive any requests during the
    /// time interval [queries[i] - x, queries[i]].
    ///
    /// Note that the time intervals are inclusive.
    ///
    /// 繁體中文：
    /// 給定一個整數 n，表示伺服器總數，以及一個索引從 0 開始的二維整數陣列 logs。
    /// 其中 logs[i] = [server_id, time] 表示編號為 server_id 的伺服器在時間 time 收到一筆請求。
    ///
    /// 另外給定一個整數 x，以及一個索引從 0 開始的整數陣列 queries。
    ///
    /// 回傳一個索引從 0 開始、長度為 queries.length 的整數陣列 arr，
    /// 其中 arr[i] 表示在時間區間 [queries[i] - x, queries[i]] 內沒有收到任何請求的伺服器數量。
    ///
    /// 請注意，時間區間包含起點與終點。
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
