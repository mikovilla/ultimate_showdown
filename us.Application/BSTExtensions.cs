using Newtonsoft.Json;
using us.Domain;

namespace us.Application
{
    public static class BSTExtensions
    {
        public static BinarySearchTree ToBinarySearchTree(this string json)
        {
            BinarySearchTree tree = new BinarySearchTree();
            Dictionary<string, string> englishDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
            foreach(KeyValuePair<string, string> kvp in englishDictionary)
            {
                tree.Insert(kvp.Key, kvp.Value);
            }
            return tree;
        }

    }
}
