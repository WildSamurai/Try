using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region SettingsForm
        private bool isClickMouse = false;
        private Point currentPosition = new Point(0, 0);
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            isClickMouse = false;
        }

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            isClickMouse = true;
            currentPosition = new Point(e.X, e.Y);
        }

        private void menuStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isClickMouse) { return; }
            Point buf = new Point(this.Location.X, this.Location.Y);

            buf.X += e.X - currentPosition.X;
            buf.Y += e.Y - currentPosition.Y;
        }
        #endregion

        private bool isPoint = false;
        private bool isNum2 = false;

        private string num1 = null;
        private string num2 = null;

        private string currentOperation = "";
        private void AddNum(string txt)
        {
            if (isNum2)
            {
                num2 += txt;
                TextResult.Text = num2;
            }
            else
            {
                num1 += txt;
                TextResult.Text = num1;
            }



        }

        private void SetNum(string txt)
        {
            if (isNum2)
            {
                num2 = txt;
                TextResult.Text = num2;
            }
            else
            {
                num1 = txt;
                TextResult.Text = num1;
            }
        }

        private void buttonNumberClick(object obj, EventArgs e)
        {
            var txt = ((Button)obj).Text;
            {
                if (isPoint && txt == ".") { return; }
                if (txt == ",") { isPoint = true; }
                if (txt == "+/-")
                {
                    if (TextResult.Text.Length > 0)
                        if (TextResult.Text[0] == '-')
                        {
                            TextResult.Text = TextResult.Text.Substring(1, TextResult.Text.Length - 1);
                        }
                        else
                        {
                            TextResult.Text = "-" + TextResult.Text;
                        }
                    SetNum(TextResult.Text);
                    return;

                }
                AddNum(txt);
            }
        }
                
               private  void buttonOperationClick(object obj, EventArgs e)
                {
                    if(num1 == null) { if (TextResult.Text.Length > 0) num1 = TextResult.Text; else return; }


                    isNum2 = true;
                    currentOperation = ((Button)obj).Text;
                    SetResult(currentOperation);

                }

                private void SetResult(string Operation)
                {
                    string result = null;
                    switch (Operation)
                    {
                        case "+": { result = MathOperation.Add(num1,num2); break; }
                        case "-": { result = MathOperation.Sub(num1, num2); break; }
                        case "x": { result = MathOperation.Mul(num1, num2); break; }
                        case "/": { result = MathOperation.Dev(num1, num2); break; }
                        case "%": { result = MathOperation.Proc(num1, num2); break; }
                        case "√": { result = MathOperation.Sqr(num1); isNum2 = false; break; }
                        case "Х²": { result = MathOperation.Pow(num1); isNum2 = false; break; }
                        case "1/x": { result = MathOperation.OneDev(num1); isNum2 = false; break; }
                        default:break;
                    }
                    OutputResult(result, Operation);
            if (isNum2) { if (result != null) num1 = result; }
            else
            {
                num1 = null;
            }
            isPoint = false;
                }
        private void OutputResult(string result, string Operation)
        {
            switch(Operation)
            {
                case "√": { if (num1 != null) textHistory.Text = "√" + num1 + " = "; break; }
                case "Х²": { if (num1 != null) textHistory.Text =  num1 + "² = "; break; }
                case "1/x": { if (num1 != null) textHistory.Text = "1/" + num1 + " = "; break; }
                default:
                    {
                        if(num2 != null)
                        {
                            textHistory.Text = num1 + " " + Operation + " " + num2 + " = ";
                        }
                        else
                        {
                            if(num1 != null)
                            {
                                textHistory.Text = num1 + " " + Operation + " ";
                                break;
                            }
                        }
                    }
                    break;
            }
            num2 = null;
            if (result != null)
            {
                TextResult.Text = result;
            }
        }

           
        private void buttonClear(object obj, EventArgs e)
        {
            TextResult.Text = "";
            textHistory.Text = "";
            isNum2 = false;
            currentOperation = null;
            num1 = null;
            num2 = null;
            isPoint = false;

        }
        

        private void buttonResultClick(object obj, EventArgs e)
        {
            SetResult(currentOperation);
            isNum2 = false;
            num1 = null;
            num2 =null;
        }

        private void buttonResetNumber(object obj, EventArgs e)
        {
            if (TextResult.Text.Length <= 0) { return; }
            TextResult.Text = TextResult.Text.Substring(0, TextResult.Text.Length - 1);
            SetNum(TextResult.Text);
        }
        private void button24_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
