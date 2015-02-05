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
    using System.Data.OleDb;
    public partial class frmImportCsv : Form
    {
        DataFrame dataset = null;
        public frmImportCsv()
        {
            InitializeComponent();
        }
        public frmImportCsv(string filepath)
        {
            InitializeComponent();
            openCSV(filepath);
           

        }
        private void frmImportCsv_Load(object sender, EventArgs e)
        {

           // this.dataGridView1.DataSource = ReadCVS("C:\\Users\\wm\\Desktop\\煤与瓦斯2.csv", "煤与瓦斯2.csv");



        }
        //private  DataTable ReadCVS(string filepath, string filename)
        //{
        //    //string cvsDir = filepath;//要读取的CVS路径
        //    DataTable dt = new DataTable();
        //    if (filename.Trim().ToUpper().EndsWith("CSV"))//判断所要读取的扩展名
        //    {
        //        string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='text;HDR=NO;FMT=Delimited'";//有列的读取
        //        string commandText = "select * from [" + filename + "]";//SQL语句

        //        OleDbConnection olconn = new OleDbConnection(connStr);
        //        olconn.Open();
        //        OleDbDataAdapter odp = new OleDbDataAdapter(commandText, olconn);
        //        odp.Fill(dt);
        //        olconn.Close();
        //        odp.Dispose();
        //        olconn.Dispose();
        //    }
        //    return dt;
        //}

        private void openCSV(string filepach)
        {

            REngine engine = REngine.GetInstanceFromID("RDotNet") ?? REngine.CreateInstance("RDotNet");
            //bool isok = engine.IsRunning;
            string cmd = string.Format("dataset=read.csv('{0}')", filepach).Replace("\\","\\\\");//不加Replace错误
            //string cmd = @"dataset=read.csv('C:\\Users\\wm\\Desktop\\煤与瓦斯2.csv')";//不加@错误
            string cmd2 = @"levels(dataset$突出规模)";

            engine.Evaluate(cmd);
            RDotNet.CharacterVector levels = engine.Evaluate(cmd2).AsCharacter();
            dataset = engine.Evaluate("dataset").AsDataFrame();

            for (int i = 0; i < dataset.ColumnCount; ++i)
            {
                dataGridView1.ColumnCount++;
                dataGridView1.Columns[i].Name = dataset.ColumnNames[i];
            }

            for (int i = 0; i < dataset.RowCount; ++i)
            {
                dataGridView1.RowCount++;
                dataGridView1.Rows[i].HeaderCell.Value = dataset.RowNames[i];

                for (int k = 0; k < dataset.ColumnCount; ++k)
                {

                    dataGridView1[k, i].Value = dataset[i, k];


                }




            }

        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            if (dataset != null)
            {
                if (txtTrainRatio.Text.Trim()!="")
                {
                    int num = int.Parse(txtTrainRatio.Text.Trim());
                    double index = Math.Round((double)(dataset.RowCount * num / 100), 0);

                   


                }


            }
        }


    }
}
