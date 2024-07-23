using QuanLyNuocNgot.Admin;
using QuanLyNuocNgot.User;
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
    public partial class frmMain : Form
    {
        private int  userId ;
        public frmMain(int id)
        {
            this.userId = id;
            InitializeComponent();
        }



        void MoFormCon(Form f)
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child.Name == f.Name)
                {
                    child.Activate();
                    return;
                }
            }
            f.MdiParent = this;
            f.Show();
        }

        private void quảnLýHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmQuanLyHoaDon());

        }

        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void quảnLýMặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmQuanLyMatHang());

        }

        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmQuanLyNguoiDung());

        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDangNhap form = new frmDangNhap();
            form.ShowDialog();

        }

        private void muaHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmMuaHang(userId));

        }

        private void xemThôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmThongTinCaNhan(userId));

        }

        private void xemThốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoFormCon(new frmThongKeDoanhSo());

        }
    }
}
