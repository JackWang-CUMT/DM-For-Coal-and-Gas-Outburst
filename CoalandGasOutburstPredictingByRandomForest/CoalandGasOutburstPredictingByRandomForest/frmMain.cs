using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CoalandGasOutburstPredictingByRandomForest
{
    using RDotNet;
    
    public partial class frmMain : Form
    {
       // public static REngine  engine = null;
       
        public frmMain()
        {
            InitializeComponent();
            

        }

        private void btnSumbit_Click(object sender, EventArgs e)
        {
              // REngine.SetDllDirectory(@"C:\Program Files\R\R-2.15.0\bin\i386");
            if (!string.IsNullOrEmpty(this.txtPram1.Text) && !string.IsNullOrEmpty(this.txtPram2.Text) && !string.IsNullOrEmpty(this.txtPram3.Text) && !string.IsNullOrEmpty(this.txtPram4.Text) && !string.IsNullOrEmpty(this.txtPram5.Text))
            {
                REngine engine = REngine.GetInstanceFromID("RDotNet") ?? REngine.CreateInstance("RDotNet");
                //engine.Initialize();//只能初始化一次

                string strCreateTest = string.Format("test2=data.frame(X1={0},X2={1},X3={2},X4={3},X5={4},Y=0)", double.Parse(this.txtPram1.Text), double.Parse(this.txtPram2.Text), double.Parse(this.txtPram3.Text), double.Parse(this.txtPram4.Text), double.Parse(this.txtPram5.Text));
                string cmd8 = @"predict(RF.Model,test2[,-6],type='response')";

                engine.Evaluate(strCreateTest);

                IntegerVector index = engine.Evaluate(cmd8).AsInteger();


                this.txtOutput.Text = getResult(index[0]);
            }
            else
            {
                MessageBox.Show("参数不能为空!","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
               
               
               
           
               
           
             


        }

        private void frmMain_Load(object sender, EventArgs e)
        {
           

            REngine engine = REngine.GetInstanceFromID("RDotNet") ?? REngine.CreateInstance("RDotNet");
            engine.Initialize();
            //this.txtOutput.Text = engine.IsRunning.ToString();
            string RFDir = System.IO.Directory.GetCurrentDirectory().ToString() + "\\model\\RF.Model.RData";
           // string cmd = @"load(file='C:\\Users\\wm\\Desktop\\RF.Model.RData',envir = parent.frame())";
            string cmd = string.Format("load(file='{0}',envir = parent.frame())",RFDir.Replace("\\","\\\\"));
            string cmd2 = @"library(randomForest)";
            engine.Evaluate(cmd2);
            engine.Evaluate(cmd);
           // MessageBox.Show(System.IO.Directory.GetCurrentDirectory().ToString());
            
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            var engine = REngine.GetInstanceFromID("RDotNet");
            if (engine != null)
            {
                engine.Dispose();
            }
        }
        private string getResult(int index)
        {
            string res = "NA";   
            if (index == 1)
            {
                res = "大型突出";
                this.txtIndicator.BackColor = System.Drawing.Color.Red;
            }
            else if(index==2)
            {
                res = "无突出";
                 this.txtIndicator.BackColor = System.Drawing.Color.Green;

            }
             else if(index==3)
            {
                res = "小型突出";
                 this.txtIndicator.BackColor = System.Drawing.Color.Yellow;

            }
             else if(index==4)
            {
                res = "中型突出";
                 this.txtIndicator.BackColor = System.Drawing.Color.Coral;

            }
            else
            {
                res = "未知";

             }
            return res;


        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            
            ofdCSV.CheckFileExists = true;
            ofdCSV.CheckPathExists = true;
            if (ofdCSV.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofdCSV.FileName;
                if (System.IO.File.Exists(filePath))
                {
                    frmImportCsv frmCSV = new frmImportCsv(filePath);
                    frmCSV.Show();
                }
                else
                {

                    MessageBox.Show("该文件不存在", "提示", MessageBoxButtons.OK);

                }
            }
          
        }

       
      



    }
}
