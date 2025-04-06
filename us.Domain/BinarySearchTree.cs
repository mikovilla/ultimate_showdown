namespace us.Domain
{
    public class BinarySearchTree
    {
        public TreeNode Root;

        public void Insert(string key, string value)
        {
            Root = InsertRec(Root, key, value);
        }

        private TreeNode InsertRec(TreeNode node, string key, string value)
        {
            if (node == null) return new TreeNode(key, value);

            int comparison = string.Compare(key, node.Key, StringComparison.Ordinal);
            if (comparison < 0)
                node.Left = InsertRec(node.Left, key, value);
            else if (comparison > 0)
                node.Right = InsertRec(node.Right, key, value);

            return node;
        }

        public string Search(string key)
        {
            return SearchRec(Root, key);
        }

        private string SearchRec(TreeNode node, string key)
        {
            if (node == null) return string.Empty;
            if (key == node.Key) return node.Value;

            return string.Compare(key, node.Key, StringComparison.Ordinal) < 0
                ? SearchRec(node.Left, key)
                : SearchRec(node.Right, key);
        }
    }
}
