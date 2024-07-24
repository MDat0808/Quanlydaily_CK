using QuanLyNuocNgot.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNuocNgot
{
    public partial class frmDangKy : Form
    {
        private HanldeData db = new HanldeData();
        public frmDangKy()
        {
            InitializeComponent();
        }

        private void btnDangky_Click(object sender, EventArgs e)
        {
            try
            {
            string account = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();
            string password2 = txtpass2.Text.Trim();
            string email = txtEmail.Text.Trim();
            string name = txtName.Text;
            string diaChi = "null";
        

            if (string.IsNullOrEmpty(account) ||
              string.IsNullOrEmpty(password) ||
              string.IsNullOrEmpty(password2) ||
              string.IsNullOrEmpty(email) ||
              string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
            } else if (password.Length < 8) {
                MessageBox.Show("Mật khẩu phải ít nhất 8 ký tự.");
            }
            else if (password != password2)
            {
                MessageBox.Show("Mật khẩu nhập lại không chính xác!");
            }
            else
            {

                string sql = $"INSERT INTO user (HoTen, TaiKhoan, MatKhau, email, DiaChi, SDT) VALUES ('{name}', '{account}', '{db.MD5Hash(password)}', '{email}', '{diaChi}', '{09999999}')";
                int check = db.dangky(sql);
                if (check >= 0)
                {
                    MessageBox.Show("Bạn đã đăng ký thành công.");
                    txtUser.Text = "";
                    txtPass.Text = "";
                    txtpass2.Text = "";
                    txtEmail.Text = "";
                    txtName.Text = "";
                    
                }
                else
                {
                    MessageBox.Show("Bạn đã đăng ký thất bại");

                }
            }
            } catch {
                MessageBox.Show("Tài khoản đã tồn tại.");
            }

        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDangNhap from = new frmDangNhap();
            from.ShowDialog();
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
