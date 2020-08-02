using System.Collections.Generic;

namespace PrefixTree.Classes
{
    public class Node
    {
        public char Character;
        public Dictionary<char, Node> Children;
        public bool IsEndOfWord;

        public Node(char character)
        {
            Character = character;
            Children = new Dictionary<char, Node>();
            IsEndOfWord = false;
        }
    }
}