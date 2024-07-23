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
    public partial class frmDangNhap : Form
    {
        private HanldeData db = new HanldeData();
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            string account = txtUser.Text.Trim();
            string mk = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(mk))
            {
                MessageBox.Show("Vui lòng nhập thông tin.");
                return;
            }
            // Kiểm tra tài khoản
         
            int check = db.Login(account, mk);
            int id = db.getMaNguoiDung(account, mk);
            if (check == 1)
            {
                this.Hide();
                frmMain form = new frmMain(id);
                form.ShowDialog();

            }
            else if (check == 0)
            {
                this.Hide();
                frmNguoiDung form = new frmNguoiDung(id);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại.");
            }
        }

        private void btnDangky_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDangKy form = new frmDangKy();
            form.ShowDialog();
        }
    }
}
