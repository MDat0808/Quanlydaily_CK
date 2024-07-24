using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNuocNgot.Model
{
    public partial class frmChangePass : Form
    {
        private int userId;
        private HanldeData db = new HanldeData();
        public frmChangePass(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDangky_Click(object sender, EventArgs e)
        {
            string mkcu = txtMKOld.Text.Trim();
            string password = txtPass.Text.Trim();
            string password2 = txtnhaplaimk.Text.Trim();


            if (string.IsNullOrEmpty(mkcu) ||
              string.IsNullOrEmpty(password) ||
              string.IsNullOrEmpty(password2))

            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
            } else if (password.Length < 8 )
            {
                MessageBox.Show("Mật khẩu phải ít nhất 8 ký tự.");
            }
            else if (password != password2)
            {
                MessageBox.Show("Mật khẩu nhập lại không chính xác!");
            }
            else
            {
                string sql = $"Select MatKhau from nguoidung where MaNguoiDung = '{userId}'";
                string pass = db.getMatKhauById(userId);
                if (pass == mkcu)
                {
                    string update = $"update user set MatKhau = '{password}' where MaNguoiDung = '{userId}'";
                    int isCheck = db.Sql(update);
                    if (isCheck >= 0)
                    {
                        MessageBox.Show("Cập nhật mật khẩu thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật mật khẩu thất bại, vui lòng thử lại sau.");
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu hiện tại không chính xác.");

                }
            }
        }
    }
}
