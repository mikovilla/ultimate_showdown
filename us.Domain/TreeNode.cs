namespace us.Domain
{
    public class TreeNode
    {
        public string Key;
        public string Value;
        public TreeNode Left, Right;

        public TreeNode(string key, string value)
        {
            Key = key;
            Value = value;
            Left = null;
            Right = null;
        }
    }
}
