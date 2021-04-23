using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Collections;

namespace Draw
{
    public partial class Window : Form
    {
        bool isConnected = false;
        String[] ports;
        SerialPort port;

        bool paint = false;
        Point prev, pres;

        int size = 10;
        SolidBrush color = new SolidBrush(Color.Black);
        Graphics g;

        Queue queue = new Queue();

        public Window()
        {
            InitializeComponent();
            Disable();

            g = canvas.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


            ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                combo.Items.Add(port);
                Console.WriteLine(port);
                if (ports[0] != null)
                {
                    combo.SelectedItem = ports[0];
                }
            }
        }


        ////////////////////////////////////////////////////////////////////////////////Serial

        private void Disable()
        {
            canvas.Enabled = false;
            groupDraw.Enabled = false;
            connect.Text = "Connect";

            combo.Enabled = true;
        }

        private void Enable()
        {
            canvas.Enabled = true;
            groupDraw.Enabled = true;
            connect.Text = "Disconnect";

            combo.Enabled = false;
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                isConnected = true;
                string selectedPort = combo.GetItemText(combo.SelectedItem);
                port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);

                port.Open();
                port.Write("#R~");
                port.Write("#S" + (char)(size / 5) + "~");
                
                Enable();
            }
            else
            {
                isConnected = false;
                port.Write("#R~");
                port.Close();
                
                Disable();
            }
        }



        ////////////////////////////////////////////////////////////////////////////////Drawing
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            prev.X = e.X;
            prev.Y = e.Y;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (paint)
            {
                pres = new Point(e.X, e.Y);
                g.DrawLine(new Pen(color, size), prev, pres);
                g.FillEllipse(color, e.X - size / 2, e.Y - size / 2, size, size);

                queue.Enqueue(prev);
                prev = pres;
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;
            pres = new Point(e.X, e.Y);
            g.FillEllipse(color, e.X - size / 2, e.Y - size / 2, size, size);

            queue.Enqueue(pres);
            string toSend = "#D";

            foreach (Point p in queue)
            {
                if (p.X >= 0 && p.Y >= 0 && p.X < 420 && p.Y < 240)
                {
                    toSend += (char)(p.X / 5);
                    toSend += (char)(p.Y / 5);
                }

            }

            //MessageBox.Show(toSend);
            toSend += "~";
            port.Write(toSend);
            queue.Clear();
        }

        private void Canvas_MouseClick(object sender, MouseEventArgs e)
        {
            g.FillEllipse(color, e.X - size / 2, e.Y - size / 2, size, size);
            port.Write("#D" + (char)(e.X) + (char)(e.Y) + "~");
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            g.Clear(canvas.BackColor);
            port.Write("#R~");
        }


        ////////////////////////////////////////////////////////////////////////////////Settings
        private void SetSize_Scroll(object sender, EventArgs e)
        {
            size = setSize.Value;
            port.Write("#S" + (char)(size / 5) + "~");
        }

        private void Erase_CheckedChanged(object sender, EventArgs e)
        {
            color = new SolidBrush(Color.White);
            port.Write("#C" + (char)0 + "~");
        }

        private void Draw_CheckedChanged(object sender, EventArgs e)
        {
            color = new SolidBrush(Color.Black);
            port.Write("#C" + (char)1 + "~");
        }
    }
}
