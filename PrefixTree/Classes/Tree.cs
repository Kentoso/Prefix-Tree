using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace PrefixTree.Classes
{
    public class Tree
    {
        public Node Root;

        public void InsertKey(string key, ref List<string> words)
        {
            if (Root == null)
            {
                Root = new Node(' ');
                InsertKey(key, ref words);
            }
            else
            {
                InsertNode(Root, key.ToCharArray());
                if (key.Length > 0) words.Add(key);
            }
        }
        public void InsertNode(Node currentNode, char[] keys)  
        {
            if (keys.Length > 0)
            {
                if (!currentNode.Children.ContainsKey(keys[0]))
                {
                    currentNode.Children.Add(keys[0], new Node(keys[0]));
                    InsertNode(currentNode.Children[keys[0]], keys.Skip(1).ToArray());
                }
                else if (currentNode.Children.ContainsKey(keys[0]))
                {
                    InsertNode(currentNode.Children[keys[0]], keys.Skip(1).ToArray());
                }
                if (keys.Length == 1)
                {
                    currentNode.Children[keys[0]].IsEndOfWord = true;
                }
            }
        }

        public void DrawTree(Graphics graphics)
        {
            DrawNode(graphics, Root, (780, 20), 1, 1);
        }
        public void DrawNode(Graphics graphics, Node currentNode, (float X, float Y) position, float scaleX, float scaleY)
        {
            if (currentNode.Children.Count > 0)
            {
                int i = 0;
                float offsetX = 70;
                float offsetY = 20;
                foreach (var key in currentNode.Children.Keys)
                {
                    float X = (position.X - i * offsetX * (float)Math.Pow(scaleX, 3));
                    float Y = position.Y + offsetY * scaleY;
                    Brush brush = currentNode.Children[key].IsEndOfWord ? Brushes.DodgerBlue : Brushes.Black;
                    graphics.DrawString(currentNode.Children[key].Character.ToString(), new Font(FontFamily.GenericSansSerif, 10), brush, X, Y);
                    graphics.DrawLine(Pens.Black, position.X, position.Y, X, Y);
                    graphics.DrawRectangle(Pens.Black, X, Y, 12, 18);
                    DrawNode(graphics, currentNode.Children[key], (X, Y + 10), scaleX * 0.60f, scaleY);
                    i++;
                }
                
            }
        }

        public void DeleteKey(string key, ref List<string> words)
        {
            char[] chars = key.ToCharArray();
            Node node = Root;
            foreach (var c in chars)
            {
                if (node.Children.ContainsKey(c))
                {
                    var child = node.Children[c];
                    node.Children.Remove(c);
                    node = child;
                }
            }
            words.Remove(key);

        }

        public string SearchWords(string search)
        {
            return SearchChar(Root, search.ToCharArray());
        }

        private string SearchChar(Node currentNode, char[] keys)
        {
            string result = "";
           
            for (int j = 0; j < keys.Length; j++)
            {
                if (currentNode.Children.ContainsKey(keys[j]))
                {
                    currentNode = currentNode.Children[keys[j]];
                    result += keys[j];
                }
            }
            while (!currentNode.IsEndOfWord)
            {
                foreach (var childrenKey in currentNode.Children.Keys)
                {
                    if (currentNode.Children.ContainsKey(childrenKey))
                    {
                        currentNode = currentNode.Children[childrenKey];
                        result += childrenKey;
                    }
                }
            }
            int i = 0;
            
            return result;
        }

    }
}