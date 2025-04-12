using System.Diagnostics;
using System.Net;
using System.Text;
using us.Application;
using us.Domain;
using us.Domain.Constants;

namespace us.Service
{
    public static class BinarySearchTreeService
    {
        public static void GetResponse(HttpListener listener, SearchType searchType, string wordToSearch)
        {
            HttpListenerContext context = listener.GetContext();

            Stopwatch sw = Stopwatch.StartNew();
            string message = new StreamReader(context.Request.InputStream).ReadToEnd();
            Console.WriteLine($"Message Length: {message.Length}");
            var tree = message.ToBinarySearchTree();
            var root = tree.Root;
            
            var value = searchType switch
            {
                SearchType.Greedy => root.Search(wordToSearch, SearchType.Greedy),
                SearchType.DivideAndConquer => root.Search(wordToSearch, SearchType.DivideAndConquer),
                SearchType.DynamicProgramming => root.Search(wordToSearch, SearchType.DynamicProgramming),
                _ => ""
            };
            Console.WriteLine($"{searchType.ToString()} search ran for {sw.Elapsed}ms.");

            string reply = $"From {searchType.ToString()},\r\nValue:\r\n{wordToSearch}: {value}.";
            Console.WriteLine($"{reply}");
            sw.Restart();

            byte[] buffer = Encoding.UTF8.GetBytes(reply);

            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.OutputStream.Close();
        }

        public static string? Search(this TreeNode root, string key, SearchType searchType)
        {
            return searchType switch
            {
                SearchType.Greedy => GreedySearchRec(root, key),
                SearchType.DivideAndConquer => DivideAndConquerSearchRec(root, key),
                SearchType.DynamicProgramming => DynamicProgrammingSearchRec(root, key),
                _ => ""
            };
        }

        private static TreeNode InsertRec(TreeNode node, string key, string value)
        {
            if (node == null) return new TreeNode(key, value);

            int comparison = string.Compare(key, node.Key, StringComparison.Ordinal);
            if (comparison < 0)
                node.Left = InsertRec(node.Left, key, value);
            else if (comparison > 0)
                node.Right = InsertRec(node.Right, key, value);

            return node;
        }

        /// <summary>
        /// The greedy approach finds the desired key by traversing the tree step by step, making the "best" decision at each step 
        /// (i.e., going left if the key is smaller, or right if it's larger).
        /// </summary>
        /// <param name="node"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GreedySearchRec(TreeNode node, string key)
        {
            while (node != null)
            {
                if (key == node.Key)
                    return node.Value;

                node = string.Compare(key, node.Key, StringComparison.Ordinal) < 0
                    ? node.Left
                    : node.Right;
            }
            return string.Empty;
        }

        /// <summary>
        /// Solves the search by breaking it into smaller subsearches, by recursively dividing searches into left and right subtrees.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DivideAndConquerSearchRec(this TreeNode node, string key)
        {
            if (node == null)
                return string.Empty;

            if (key == node.Key)
                return node.Value;

            if (string.Compare(key, node.Key, StringComparison.Ordinal) < 0)
                return DivideAndConquerSearchRec(node.Left, key);
            else
                return DivideAndConquerSearchRec(node.Right, key);
        }

        private static Dictionary<string, string> cache = new Dictionary<string, string>();
        /// <summary>
        /// Optimize repeated searches by storing already-searched keys and their values in a cache (memoized approach).
        /// </summary>
        /// <param name="node"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DynamicProgrammingSearchRec(this TreeNode node, string key)
        {
            if (cache.ContainsKey(key))
                return cache[key];

            if (node == null)
                return string.Empty;

            if (key == node.Key)
            {
                cache[key] = node.Value;
                return node.Value;
            }

            string result = string.Compare(key, node.Key, StringComparison.Ordinal) < 0
                ? DynamicProgrammingSearchRec(node.Left, key)
                : DynamicProgrammingSearchRec(node.Right, key);

            cache[key] = result;
            return result;
        }
    }
}
