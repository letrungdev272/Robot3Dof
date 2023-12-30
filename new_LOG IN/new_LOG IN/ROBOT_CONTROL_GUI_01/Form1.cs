using System;
using System.Windows.Forms;
using System.IO.Ports;

namespace ROBOT_CONTROL_GUI_01
{
    public partial class Form1 : Form
    {
        private int count1 = 0;
        int flag, flag2, b;
        double theta1, theta2, theta3, theta4, theta5, theta6;
        double theta1_pre, theta2_pre, theta3_pre;
        double t, t_int, tc;
        double d = 0.425;
        double L1 = 9.52, L2 = 8.1, L3 = 10.2;
        //chiều dài link1  9.52
        //chiều dài link2  8.1
        //chiều dài link3  10.2
        double x1, x2, x3, x4, x5, x6;
        double X, Y, Z, theta1c, theta2c, theta3c, theta1o, theta2o, theta3o;
        string COMRecievedData;
        string data1, data2, data3, data4, data5, data6;
        string strthe6 = "", strthe5 = "", strthe4 = "", strthe3 = "", strthe2 = "", strthe1 = "";
        string tg;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //cmbBaurate.SelectedIndex = 0;
            string[] COMPortname = SerialPort.GetPortNames();
            cmbCOMPort.Items.AddRange(COMPortname);
            //cmbCOMPort.SelectedIndex = 0;
            count1 = 0;
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
            theta4 = 0;
            theta5 = 0;
            theta6 = 0;
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

        private void COMsendData1(string data)
        {
            if (serialPort1.IsOpen)
            {
                if(serialPort1.WriteBufferSize > data.Length)
                    serialPort1.WriteLine(data);
            }
        }
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string[] abc = serialPort1.ReadLine().Split('*');
            if (abc.Length == 5)
            {
                data1 = abc[1];
                data2 = abc[2];
                data3 = abc[3];
            }
        }

        private void run_btn1_Click(object sender, EventArgs e)
        {
         
            strthe1 = textBox6.Text;
            strthe2 = textBox5.Text;
            strthe3 = textBox4.Text;

            String data = strthe1 + " " + strthe2 + "," + strthe3;
            serialPort1.WriteLine(data);
        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            /*    double a1 = theta1_pre;
                double b1 = 0;
                double c1 = 3 * (theta1 - theta1_pre) / Math.Pow(tc, 2);
                double d1 = -2 * (theta1 - theta1_pre) / Math.Pow(tc, 3);

                double a2 = theta2_pre;
                double b2 = 0;
                double c2 = 3 * (theta2 - theta2_pre) / Math.Pow(tc, 2);
                double d2 = -2 * (theta2 - theta2_pre) / Math.Pow(tc, 3);

                double a3 = theta3_pre;
                double b3 = 0;
                double c3 = 3 * (theta3 - theta3_pre) / Math.Pow(tc, 2);
                double d3 = -2 * (theta3 - theta3_pre) / Math.Pow(tc, 3);

                if (t_int < t)
                {
                    t_int++;
                    double theta1_flex = Math.Round(a1 + b1 * t_int + c1 * Math.Pow(t_int, 2) + d1 * Math.Pow(t_int, 3));
                    double theta2_flex = Math.Round(a2 + b2 * t_int + c2 * Math.Pow(t_int, 2) + d2 * Math.Pow(t_int, 3));
                    double theta3_flex = Math.Round(a3 + b3 * t_int + c3 * Math.Pow(t_int, 2) + d3 * Math.Pow(t_int, 3));

                    strthe1 = theta1_flex.ToString();
                    strthe2 = theta2_flex.ToString();
                    strthe3 = theta3_flex.ToString();
                    strthe4 = theta4.ToString();
                    strthe5 = theta5.ToString();
                    strthe6 = theta6.ToString();

                }
                */
        }

        private void compute_btn1_Click(object sender, EventArgs e)
        {
            X = Convert.ToDouble(value_x.Text);
            Y = Convert.ToDouble(value_y.Text);
            Z = Convert.ToDouble(value_z.Text);

             flag2 = 1;
           
            if (flag2 == 1)
            {

                //double theta1a = Math.Atan2(Math.Sqrt(X * X + Y * Y - d * d), d) + Math.Atan2(X, -Y);
                //double theta1b = Math.Atan2(-Math.Sqrt(X * X + Y * Y - d * d), d) + Math.Atan2(X, -Y);
                //double coth3a = (Math.Pow(((X - d * Math.Sin(theta1a)) / Math.Cos(theta1a)), 2) + Math.Pow(Z, 2) - Math.Pow(L2, 2) - Math.Pow(L3, 2)) / (2 * L2 * L3);
                //double coth3b = (Math.Pow(((X - d * Math.Sin(theta1b)) / Math.Cos(theta1b)), 2) + Math.Pow(Z, 2) - Math.Pow(L2, 2) - Math.Pow(L3, 2)) / (2 * L2 * L3);
                //double theta3a = Math.Atan2(Math.Sqrt(1 - Math.Pow(coth3a, 2)), coth3a);
                //double theta3b = Math.Atan2(-Math.Sqrt(1 - Math.Pow(coth3a, 2)), coth3a);
                //double theta3c = Math.Atan2(Math.Sqrt(1 - Math.Pow(coth3b, 2)), coth3b);
                //double theta3d = Math.Atan2(-Math.Sqrt(1 - Math.Pow(coth3b, 2)), coth3b);
                //double a1 = L3 * Math.Sin(theta3a);
                //double b1 = L3 * Math.Cos(theta3a) + L2;
                //double c1 = Z;
                //double e1 = L3 * Math.Cos(theta3a) + L2;
                //double f1 = -L3 * Math.Sin(theta3a);
                //double g1;
                //if (theta1a == 0 || theta1a == Math.PI)
                //{  g1 = (X - d * Math.Sin(theta1a)) / Math.Cos(theta1a); }
                //else
                //{  g1 = (Y + d * Math.Cos(theta1a)) / Math.Sin(theta1a); }
                //double a2 = L3 * Math.Sin(theta3b);
                //double b2 = L3 * Math.Cos(theta3b) + L2;
                //double c2 = Z;
                //double e2 = L3 * Math.Cos(theta3b) + L2;
                //double f2 = -L3 * Math.Sin(theta3b);
                //double g2;
                //if (theta1a == 0 || theta1a == Math.PI)
                //{ g2 = (X - d * Math.Sin(theta1a)) / Math.Cos(theta1a); }
                //else
                //{ g2 = (Y + d * Math.Cos(theta1a)) / Math.Sin(theta1a); }
                //double a3 = L3 * Math.Sin(theta3c);
                //double b3 = L3 * Math.Cos(theta3c) + L2;
                //double c3 = Z;
                //double e3 = L3 * Math.Cos(theta3c) + L2;
                //double f3 = -L3 * Math.Sin(theta3c);
                //double g3;
                //if (theta1b == 0 || theta1b == Math.PI)
                //{ g3 = (X - d * Math.Sin(theta1b)) / Math.Cos(theta1b); }
                //else
                //{ g3 = (Y + d * Math.Cos(theta1b)) / Math.Sin(theta1b); }
                //double a4 = L3 * Math.Sin(theta3d);
                //double b4 = L3 * Math.Cos(theta3d) + L2;
                //double c4 = Z;
                //double e4 = L3 * Math.Cos(theta3d) + L2;
                //double f4 = -L3 * Math.Sin(theta3d);
                //double g4;
                //if (theta1b == 0 || theta1b == Math.PI)
                //{ g4 = (X - d * Math.Sin(theta1b)) / Math.Cos(theta1b); }
                //else
                //{ g4 = (Y + d * Math.Cos(theta1b)) / Math.Sin(theta1b); }
                //double theta2a = Math.Atan2((c1 * e1 - g1 * a1) / (e1 * b1 - a1 * f1), (c1 * f1 - g1 * b1) / (a1 * f1 - e1 * b1));
                //double theta2b = Math.Atan2((c2 * e2 - g2 * a2) / (e2 * b2 - a2 * f2), (c2 * f2 - g2 * b2) / (a2 * f2 - e2 * b2));
                //double theta2c = Math.Atan2((c3 * e3 - g3 * a3) / (e3 * b3 - a3 * f3), (c3 * f3 - g3 * b3) / (a3 * f3 - e3 * b3));
                //double theta2d = Math.Atan2((c4 * e4 - g4 * a4) / (e4 * b4 - a4 * f4), (c4 * f4 - g4 * b4) / (a4 * f4 - e4 * b4));

                //double Ea = Math.Pow(theta1a * 180 / Math.PI - theta1_pre, 2) + Math.Pow(theta2a * 180 / Math.PI - theta2_pre, 2) + Math.Pow(theta3a * 180 / Math.PI - theta3_pre, 2);
                //double Eb = Math.Pow(theta1a * 180 / Math.PI - theta1_pre, 2) + Math.Pow(theta2b * 180 / Math.PI - theta2_pre, 2) + Math.Pow(theta3b * 180 / Math.PI - theta3_pre, 2);
                //double Ec = Math.Pow(theta1b * 180 / Math.PI - theta1_pre, 2) + Math.Pow(theta2c * 180 / Math.PI - theta2_pre, 2) + Math.Pow(theta3c * 180 / Math.PI - theta3_pre, 2);
                //double Ed = Math.Pow(theta1b * 180 / Math.PI - theta1_pre, 2) + Math.Pow(theta2d * 180 / Math.PI - theta2_pre, 2) + Math.Pow(theta3d * 180 / Math.PI - theta3_pre, 2);
                //if (Ea == Math.Min(Ea, Math.Min(Eb, Math.Min(Ec, Ed))) && theta2a <= (Math.PI) && theta2a >= 0 && theta1a <= (Math.PI / 2) && theta1a >= (-Math.PI / 2))
                //{
                //        theta1 = Math.Round(theta1a * 180 / Math.PI,0);
                //        theta2 = Math.Round(theta2a * 180 / Math.PI,0);
                //        theta3 = Math.Round(theta3a * 180 / Math.PI,0);
                //}
                //else if (Eb == Math.Min(Eb,Math.Min(Ec, Ed)) && theta2b <= (Math.PI) && theta2b >= 0 && theta1a <= (Math.PI / 2) && theta1a >= (-Math.PI / 2)) 
                //{
                //        theta1 = Math.Round(theta1a * 180 / Math.PI,0);
                //        theta2 = Math.Round(theta2b * 180 / Math.PI,0);
                //        theta3 = Math.Round(theta3b * 180 / Math.PI,0);
                //}

                //else if (Ec == Math.Min(Ec, Ed) && theta2c <= (Math.PI) && theta2c >= 0 && theta1b <= (Math.PI / 2) && theta1b >= (-Math.PI / 2))
                //{
                //        theta1 = Math.Round(theta1b * 180 / Math.PI,0);
                //        theta2 = Math.Round(theta2c * 180 / Math.PI,0);
                //        theta3 = Math.Round(theta3c * 180 / Math.PI,0);
                //}
                //else
                //{
                //    theta1 = Math.Round(theta1b * 180 / Math.PI,0);
                //    theta2 = Math.Round(theta2d * 180 / Math.PI,0);
                //    theta3 = Math.Round(theta3d * 180 / Math.PI,0);
                //}
                //Động học nghịch
        double r = -Math.Sqrt(X * X + Y * Y);
        theta1 = Math.Round(Math.Atan2(Y, X) * 180 / Math.PI, 2);
        double a = ((L1 - Z) * (L1 - Z) - L2 * L2 - L3 * L3 + X * X + Y * Y) / (2 * L2 * L3);
        //a = Math.Max(Math.Min(a, 1), -1);
        theta3 = -Math.Round(Math.Acos(a) * 180 / Math.PI,2);
        theta2 = Math.Round((Math.Atan2( Z - L1, -r) - Math.Atan2( L3 * Math.Sin(theta3 * Math.PI / 180), L2 + L3 * Math.Cos(theta3 * Math.PI / 180) )) * 180 / Math.PI,2);
                //if (X == 18.3 && Y == 0 && Z == 9.52)
                //              {
                //                  theta1 = 0.01;
                //                  theta2 = 0;
                //                  theta3 = 0;
                //              }
                if (X == 18.3 && Y == 0 && Z == 9.52)
                {
                    theta2 = 0;
                    theta3 = 0;
                }
                textBox6.Text = theta1.ToString();
                textBox5.Text = theta2.ToString();
                textBox4.Text = theta3.ToString();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
