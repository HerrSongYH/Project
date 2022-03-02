using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


/// <summary>
/// 1. 숫자 입력시 결과창에 숫자가 표시됩니다.
/// 2. 입력을 완료하면 입력창에 숫자가 표시됩니다.
/// 3. 최종 연산 결과는 결과창에 표시됩니다.
/// 
/// </summary>

namespace M_Calc
{
    public partial class Form1 : Form
    {
        private char operate = '\0';
        private bool operateFlag = false;
        private bool memoryFlag = false;
        private double savedValue;
        private double memory;
        private int value;

        public Form1()
        {
            InitializeComponent();

            btnMC.Enabled = false;
            btnMR.Enabled = false;


        }

        //숫자 버튼(btn0~btn9)을 클릭했을 때 처리하는 메서드
        private void btnNumber_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

           
            if (txtResult.Text == "0" || operateFlag == true || memoryFlag == true)
            {
                txtResult.Text = btn.Text;
                operateFlag = false;
                memoryFlag = false;
            }
            else
                txtResult.Text = txtResult.Text + btn.Text;


            //3자리마다 콤마 삽입
            double v = Double.Parse(txtResult.Text);
            txtResult.Text = commaProcedure(v, txtResult.Text);
            //}

        }

        //소수점 처리
        private void btnDot_Click(object sender, EventArgs e)
        {
            if (txtResult.Text.Contains("."))
                return;
            else
                txtResult.Text += ".";
        }

        //근사값의 정밀도
        private void btnPlusMinus_Click(object sender, EventArgs e)
        {
            double v = Double.Parse(txtResult.Text);
            txtResult.Text = (-v).ToString();
        }

        //+버튼
        private void btnPlus_Click(object sender, EventArgs e)
        {
            savedValue = Double.Parse(txtResult.Text);
            txtExpress.Text = txtResult.Text + "+";
            operate = '+';
            operateFlag = true;
        }

        //-버튼
        private void btnMinus_Click(object sender, EventArgs e)
        {
            savedValue = Double.Parse(txtResult.Text);
            txtExpress.Text = txtResult.Text + "-";
            operate = '-';
            operateFlag = true;
        }


        //X 버튼
        private void btnTimes_Click(object sender, EventArgs e)
        {
            savedValue = Double.Parse(txtResult.Text);
            txtExpress.Text = txtResult.Text + "X";
            operate = '*';
            operateFlag = true;
        }

        //÷버튼
        private void btnDivide_Click(object sender, EventArgs e)
        {
            savedValue = Double.Parse(txtResult.Text);
            txtExpress.Text = txtResult.Text + "÷";
            operate = '/';
            operateFlag = true;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {

            Double v = Double.Parse(txtResult.Text);
            

            switch (operate)
            {
                case '+':
                    txtResult.Text = (savedValue + v).ToString();
                    break;
                case '-':
                    txtResult.Text = (savedValue - v).ToString();
                    break;
                case '*':
                    txtResult.Text = (savedValue * v).ToString();
                    break;
                case '/':
                    txtResult.Text = (savedValue / v).ToString();
                    break;
            }
            txtExpress.Text = "";
        }

        // 백분율 처리
        private void button6_Click(object sender, EventArgs e)
        {
            txtExpress.Text = "%(" + txtResult.Text + ") ";
            txtResult.Text = Math.Round(Double.Parse(txtResult.Text)).ToString();
        }



        //제곱근 처리
        private void btnSqrt_Click(object sender, EventArgs e)
        {
            txtExpress.Text = "√(" + txtResult.Text + ") ";
            txtResult.Text = Math.Sqrt(Double.Parse(txtResult.Text)).ToString();
        }

        //제곱 처리
        private void button8_Click(object sender, EventArgs e)
        {
            txtExpress.Text = "sqr(" + txtResult.Text + ") ";
            txtResult.Text = (Double.Parse(txtResult.Text) * Double.Parse(txtResult.Text)).ToString();
        }

        //역수 연산 처리
        private void btnRecip_Click(object sender, EventArgs e)
        {
            txtExpress.Text = "1 / (" + txtResult.Text + ") ";
            txtResult.Text = (1 / Double.Parse(txtResult.Text)).ToString();
        }

        //txtResult에 있는 값을 0으로
        private void btnCE_Click(object sender, EventArgs e)
        {
            txtResult.Text = "0";
        }

        //초기화
        private void btnC_Click(object sender, EventArgs e)
        {
            txtResult.Text = "0";
            txtExpress.Text = "";
            savedValue = 0;
            operate = '\0';
            operateFlag = false;
        }

        //맨 뒤의 한 글자 지우기
        private void btnDelete_Click(object sender, EventArgs e)
        {
            txtResult.Text = txtResult.Text.Remove(txtResult.Text.Length - 1);
            if (txtResult.Text.Length == 0)
                txtResult.Text = "0";
        }

        //Memory Clear
        private void btnMC_Click(object sender, EventArgs e)
        {
            txtResult.Text = "0";
            memory = 0;
            btnMR.Enabled = false;
            btnMC.Enabled = false;
        }

        //Memory Read
        private void btnMR_Click(object sender, EventArgs e)
        {
            txtResult.Text = memory.ToString();
            memoryFlag = true;
        }

        //M+
        private void btnMPlus_Click(object sender, EventArgs e)
        {
            memory += Double.Parse(txtResult.Text);
        }

        //M-
        private void btnMMius_Click(object sender, EventArgs e)
        {
            memory -= Double.Parse(txtResult.Text);
        }

        //Memory Save
        private void btnMS_Click(object sender, EventArgs e)
        {
            memory = Double.Parse(txtResult.Text);
            btnMC.Enabled = true;
            btnMR.Enabled = true;
            memoryFlag = true;
        }

        //3자리마다 쉼표 삽입
        private static string commaProcedure(double value, string str)
        {
            int position = 0;
            if (str.Contains("."))
            {
                position = str.Length - str.IndexOf('.'); //소수점 아래 자리수 + 1
                if (position == 1) //맨 뒤에 소수점이 있으면 그대로 리턴
                    return str;
                string formatStr = "{0:N" + (position - 1) + "}";
                str = string.Format(formatStr, value);
            }
            else
                str = string.Format("{0:N0}", value);
            return str;
        }

        //숫자만 입력 가능 처리
        private void txtResult_TextChanged(object sender, EventArgs e)
        {
            //숫자만 입력이 가능
            if (Regex.IsMatch(txtResult.Text, "[^0-9, +-]"))
            {
                MessageBox.Show("숫자만 입력하세요");
                txtResult.Text = txtResult.Text.Remove(txtResult.Text.Length - 1);

            }
        }

        //private void KeyPress()
        //{
        //    if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
        //    {
        //        e.Handled = true;
        //    }
        //}

        //public bool isNumeric(string str)
        //{
        //    double Num;
        //    bool isNum = double.TryParse(str, out Num);
        //    if (isNum)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

    }
}
