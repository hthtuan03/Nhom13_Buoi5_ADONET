using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Nhom13_Buoi5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string strcnn = "Data Source=DESKTOP-GHISU8I\\SQLEXPRESS;Initial Catalog=QuanLySinhVien;Integrated Security=True";
        BindingSource bs = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection(strcnn);
            if (cnn == null)
                cnn = new SqlConnection(strcnn);
            SqlDataAdapter adt = new SqlDataAdapter("Select * from Student", cnn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adt);
            DataSet ds = new DataSet();
            adt.Fill(ds, "student");
            bs = new BindingSource(ds, "student");
            txtMa.DataBindings.Add("text", bs, "StudentID");
            txtTen.DataBindings.Add("text", bs, "FullName");
            txtDiem.DataBindings.Add("text", bs, "AverageScore");
            txtKhoa.DataBindings.Add("text", bs, "FacultyID");
        }

        private void btnPhai_Click(object sender, EventArgs e)
        {
            if (bs.Position < bs.Count - 1)
                bs.Position++;
            label5.Text = (bs.Position + 1).ToString() + "/" + (bs.Count).ToString();
        }

        private void btnTrai_Click(object sender, EventArgs e)
        {
            if (bs.Position > 0)
                bs.Position--;
            label5.Text = (bs.Position + 1).ToString() + "/" + (bs.Count).ToString();
        }

        private void btnHetTrai_Click(object sender, EventArgs e)
        {
            bs.Position = 0;
            label5.Text = (bs.Position + 1).ToString() + "/" + (bs.Count).ToString();

        }

        private void btnHetPhai_Click(object sender, EventArgs e)
        {
            bs.Position = bs.Count -1;
            label5.Text = (bs.Position + 1).ToString() + "/" + (bs.Count).ToString();
        }
         
        private void ThemSV (string StudentID, string FullName, float AverageScore, string FacultyID)
        {
            SqlConnection sqlConnection = new SqlConnection(strcnn);
            string query = "insert into Student(StudentID, FullName, AverageScore, FacultyID) values(@StudentID, @FullName, @AverageScore, @FacultyID)";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.Add("@StudentID", StudentID);
            sqlCommand.Parameters.Add("@FullName", FullName);
            sqlCommand.Parameters.Add("@AverageScore", AverageScore);
            sqlCommand.Parameters.Add("@FacultyID", FacultyID);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string mssv = txtMa.Text;
            string hoten = txtTen.Text;
            float diem = float.Parse(txtDiem.Text);
            string khoa = txtKhoa.Text;
            ThemSV(mssv, hoten, diem, khoa);
            MessageBox.Show("Đã thêm thành công!");
        }

        private void XoaSV(string ma)
        {
            SqlConnection connec = new SqlConnection(strcnn);
            string query = "Delete from Student where StudentID = @ma";
            SqlCommand cmp = new SqlCommand(query, connec);
            cmp.CommandType = CommandType.Text;
            cmp.Parameters.AddWithValue("ma", ma);
            connec.Open();
            cmp.ExecuteNonQuery();
            connec.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMa.Text;
            XoaSV(ma);
            MessageBox.Show("Đã xóa thành công!");
        }
    }
}
