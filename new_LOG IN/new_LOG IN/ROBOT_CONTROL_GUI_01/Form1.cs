using System;
using System.Windows.Forms;
using System.IO.Ports;

namespace ROBOT_CONTROL_GUI_01
{
    public partial class Form1 : Form
    {
        int flag2;
        double theta1, theta2, theta3;
        double L1 = 9.52, L2 = 8.1, L3 = 10.2;
        //chiều dài link1  9.52
        //chiều dài link2  8.1
        //chiều dài link3  10.2
        double X, Y, Z;
        string strthe3 = "", strthe2 = "", strthe1 = "";

        /// <summary>
        /// Động học thuận
        /// </summary>
        private void send_btn_Click(object sender, EventArgs e)
        {
            double.TryParse(the1.Text, out theta1);
            double.TryParse(the2.Text, out theta2);
            double.TryParse(the3.Text, out theta3);
            String data = 0 + " " + 0 + "," + 0; // Gộp 3 góc nhập vào thành chuỗi VD: 90 90,90
            serialPort1.WriteLine(data); // Gửi chuỗi sang arduino
            // Động Học Thuận
            double a = Math.Round((L2 * Math.Cos(theta2 * Math.PI / 180) + L3 * Math.Cos((theta2 + theta3) * Math.PI / 180)) * Math.Cos(theta1 * Math.PI / 180), 2);
            double b = Math.Round((L2 * Math.Cos(theta2 * Math.PI / 180) + L3 * Math.Cos((theta2 + theta3) * Math.PI / 180)) * Math.Sin(theta1 * Math.PI / 180), 2);
            double c = Math.Round(L2 * Math.Sin(theta2 * Math.PI / 180) + L3 * Math.Sin((theta2 + theta3) * Math.PI / 180) + L1, 2);
            //Hiển thị ra textbox
            textBox3.Text = a.ToString();
            textBox2.Text = b.ToString();
            textBox1.Text = c.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Phương trình đường thẳng 
            //double X = X1 + t * (X2 - X1);
            //double X = Y1 + t * (Y2 - Y1);

            //Phương trình đường tròn trong mặt phẳng X, Y
            //double X = r * Math.Cos(t * Math.PI / 180); 
            //double Y = r * Math.Sin(t * Math.PI / 180); // t chạy từ 0 đến 360
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] COMPortname = SerialPort.GetPortNames();
            cmbCOMPort.Items.AddRange(COMPortname);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            the1.Text = 0.ToString();
            the2.Text = 0.ToString();
            the3.Text = 0.ToString();
            theta1 = 0;
            theta2 = 0;
            theta3 = 0;
            String data = 0 + " " + 0 + "," + 0; // Gộp 3 góc nhập vào thành chuỗi VD: 90 90,90
            serialPort1.WriteLine(data); // Gửi chuỗi sang arduino
            // Động Học Thuận
            double a = Math.Round((L2 * Math.Cos(theta2 * Math.PI / 180) + L3 * Math.Cos((theta2 + theta3) * Math.PI / 180)) * Math.Cos(theta1 * Math.PI / 180), 2);
            double b = Math.Round((L2 * Math.Cos(theta2 * Math.PI / 180) + L3 * Math.Cos((theta2 + theta3) * Math.PI / 180)) * Math.Sin(theta1 * Math.PI / 180), 2);
            double c = Math.Round(L2 * Math.Sin(theta2 * Math.PI / 180) + L3 * Math.Sin((theta2 + theta3) * Math.PI / 180) + L1, 2);
            //Hiển thị ra textbox
            textBox3.Text = a.ToString();
            textBox2.Text = b.ToString();
            textBox1.Text = c.ToString();
        }

        private void btnCon_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = cmbCOMPort.Text;
            serialPort1.BaudRate = Convert.ToInt32(cmbBaurate.Text);
            if (serialPort1.IsOpen) return;
            serialPort1.Open();

            btnCon.Enabled = false;
            btnDiscon.Enabled = true;

            theta1 = 0;
            theta2 = 0;
            theta3 = 0;
            value_x.Text = "0";
            value_y.Text = "0";
            value_z.Text = "0";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }

        private void btnDis_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false) return;
            serialPort1.Close();
            btnCon.Enabled = true;
            btnDiscon.Enabled = false;
        }

        private void run_btn1_Click(object sender, EventArgs e)
        {
            strthe1 = textBox6.Text;
            strthe2 = textBox5.Text;
            strthe3 = textBox4.Text;

            String data = strthe1 + " " + strthe2 + "," + strthe3;
            serialPort1.WriteLine(data);
        }

        /// <summary>
        /// Động học nghịch
        /// </summary>
        private void compute_btn1_Click(object sender, EventArgs e)
        {
            X = Convert.ToDouble(value_x.Text);
            Y = Convert.ToDouble(value_y.Text);
            Z = Convert.ToDouble(value_z.Text);

            //Động học nghịch
            double r = -Math.Sqrt(X * X + Y * Y);
            theta1 = Math.Round(Math.Atan2(Y, X) * 180 / Math.PI, 2);
            double a = ((L1 - Z) * (L1 - Z) - L2 * L2 - L3 * L3 + X * X + Y * Y) / (2 * L2 * L3);
            //a = Math.Max(Math.Min(a, 1), -1);
            theta3 = -Math.Round(Math.Acos(a) * 180 / Math.PI, 2);
            theta2 = Math.Round((Math.Atan2(Z - L1, -r) - Math.Atan2(L3 * Math.Sin(theta3 * Math.PI / 180), L2 + L3 * Math.Cos(theta3 * Math.PI / 180))) * 180 / Math.PI, 2);

            textBox6.Text = theta1.ToString();
            textBox5.Text = theta2.ToString();
            textBox4.Text = theta3.ToString();
        }
    }
}
