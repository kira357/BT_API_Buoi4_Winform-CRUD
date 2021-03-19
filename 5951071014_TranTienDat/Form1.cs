using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5951071014_TranTienDat
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        public Form1()
        {
            InitializeComponent();
            GetStudentsRecord();
        }

        private bool IsValidData()
        {
            if (txtNName.Text == String.Empty || txtAddress.Text == String.Empty || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtRoll.Text))
            {
                MessageBox.Show("Có chỗ chưa nhập dữ liệu !!! ", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void GetStudentsRecord()
        {
            con = new SqlConnection(@"Data Source=LAPTOP-0II0U9OF\SQLEXPRESS01;Initial Catalog=DemoCRUD;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentsTb", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dgv_Students.DataSource = dt;


        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into StudentsTb(Name , FatherName , RollNumber, Address ,Mobile) values (@Name, @FatherName,@RollNumber,@Address,@Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtPhone.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();

            }
        }
        public int StudentID;
        private void dgv_Students_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int n = dgv_Students.CurrentRow.Index;
            StudentID = Convert.ToInt32(dgv_Students.Rows[n].Cells[0].Value);
            txtHName.Text = dgv_Students.Rows[n].Cells[1].Value.ToString();
            txtNName.Text = dgv_Students.Rows[n].Cells[2].Value.ToString();
            txtRoll.Text = dgv_Students.Rows[n].Cells[3].Value.ToString();
            txtAddress.Text = dgv_Students.Rows[n].Cells[4].Value.ToString();
            txtPhone.Text = dgv_Students.Rows[n].Cells[5].Value.ToString();
        }

        private void btnCapnhat_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update StudentsTb set Name = @Name, FatherName= @FatherName , RollNumber = @RollNumber, Address= @Address ,Mobile = @Mobile where StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name ", txtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName ", txtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtRoll.Text);
                cmd.Parameters.AddWithValue("@Address ", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile ", txtPhone.Text);
                cmd.Parameters.AddWithValue("@ID ", this.StudentID);
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();
                ResetData();

            }
            else
            {
                MessageBox.Show("Bạn cập nhật không thành công rồi ", "Lỗi !!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void ResetData()
        {
            txtHName.Text = "";
            txtNName.Text = "";
            txtPhone.Text = "";
            txtRoll.Text = "";
            txtAddress.Text = "";
        }

        private void btnXoá_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete From StudentsTb Where StudentID = @ID ", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID ", this.StudentID);
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentsRecord();
                ResetData();

            }
            else
            {
                MessageBox.Show("Bạn xoá không thành công rồi ", "Lỗi !!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn có muốn thoát ko ", "Thông báo ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Application.Exit();


        }
    }
}
