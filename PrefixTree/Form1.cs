using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrefixTree.Classes;

namespace PrefixTree
{
    public partial class Form1 : Form
    {
        private Tree tree;
        private List<string> list;
        public Form1()
        {
            InitializeComponent();
            tree = new Tree();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            tree.DrawTree(this.CreateGraphics());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //label1.Text = $"Word: {tree.SearchWords(textBox1.Text)}";
            tree.InsertKey(textBox1.Text, ref list);
            this.CreateGraphics().Clear(SystemColors.Control);
            tree.DrawTree(this.CreateGraphics());
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            list = new List<string>
            {
                "yes",
                "car",
                "mugging",
                "robber",
                "present",
                "day",
                "time",
                "make",
                "me",
                "feel",
            };

            int count = list.Count;
            var listNew = new List<string>();
            Debug.WriteLine(count);
            for (int i = 0; i < count; i++)
            {
                tree.InsertKey(list[i], ref listNew);
            }
            list = listNew;
        }

        private void Form1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.CreateGraphics().Clear(SystemColors.Control);
            tree.DeleteKey(textBox2.Text, ref list);
            tree = new Tree();
            int count = list.Count;
            var listNew = new List<string>();
            Debug.WriteLine(count);
            for (int i = 0; i < count; i++)
            {
                tree.InsertKey(list[i], ref listNew);
            }
            list = listNew;
            tree.DrawTree(this.CreateGraphics());
        }
    }
}
