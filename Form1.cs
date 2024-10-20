using BUS;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QlSach
{
    public partial class frmQuanLySach : Form
    {
        private LoaiSachBUS loaiSachBUS = new LoaiSachBUS();
        private SachBUS sachBUS = new SachBUS();

        public frmQuanLySach()
        {
            InitializeComponent();
        }

        private void frmQuanLySach_Load(object sender, EventArgs e)
        {
            LoadLoaiSachToComboBox(); 
            LoadSachToDataGridView();   
        }
        private void LoadLoaiSachToComboBox()
        {
            List<LoaiSach> listLoaiSach = loaiSachBUS.GetAllLoaiSach();
            cmbTheLoai.DataSource = listLoaiSach;
            cmbTheLoai.DisplayMember = "TenLoai"; 
            cmbTheLoai.ValueMember = "MaLoai"; 
        }

        private void LoadSachToDataGridView()
        {
            List<object> listSach = sachBUS.GetAllSachWithLoaiSach();
            dgvQLSach.DataSource = listSach;

            dgvQLSach.Columns["MaSach"].HeaderText = "Mã Sách";
            dgvQLSach.Columns["TenSach"].HeaderText = "Tên Sách";
            dgvQLSach.Columns["NamXB"].HeaderText = "Năm Xuất Bản";
            dgvQLSach.Columns["TenLoai"].HeaderText = "Thể Loại";
        }

        private void dgvQLSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = dgvQLSach.Rows[e.RowIndex];

                txtMaSach.Text = row.Cells["MaSach"].Value.ToString();
                txtTenSach.Text = row.Cells["TenSach"].Value.ToString();
                txtNamXB.Text = row.Cells["NamXB"].Value.ToString();
                cmbTheLoai.Text = row.Cells["TenLoai"].Value.ToString(); 
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maSach = txtMaSach.Text;

            if (string.IsNullOrEmpty(maSach))
            {
                MessageBox.Show("Vui lòng chọn sách cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Sach sach = sachBUS.GetSachByMa(maSach);
            if (sach == null)
            {
                MessageBox.Show("Sách cần xóa không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                sachBUS.DeleteSach(maSach);
                LoadSachToDataGridView(); 
                MessageBox.Show("Xóa sách thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSach.Text) || string.IsNullOrEmpty(txtTenSach.Text) || string.IsNullOrEmpty(txtNamXB.Text) || cmbTheLoai.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtMaSach.Text.Length != 6)
            {
                MessageBox.Show("Mã sách phải có 6 kí tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Sach sach = new Sach
            {
                MaSach = txtMaSach.Text,
                TenSach = txtTenSach.Text,
                NamXB = int.Parse(txtNamXB.Text),
                MaLoai = (int)cmbTheLoai.SelectedValue
            };

            sachBUS.AddSach(sach);
            LoadSachToDataGridView();
            MessageBox.Show("Thêm mới thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ResetForm();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSach.Text) || string.IsNullOrEmpty(txtTenSach.Text) || string.IsNullOrEmpty(txtNamXB.Text) || cmbTheLoai.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtMaSach.Text.Length != 6)
            {
                MessageBox.Show("Mã sách phải có 6 kí tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Sach sach = sachBUS.GetSachByMa(txtMaSach.Text);
            if (sach == null)
            {
                MessageBox.Show("Sách cần sửa không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            sach.TenSach = txtTenSach.Text;
            sach.NamXB = int.Parse(txtNamXB.Text);
            sach.MaLoai = (int)cmbTheLoai.SelectedValue;

            sachBUS.UpdateSach(sach);
            LoadSachToDataGridView();
            MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ResetForm();
        }
        private void ResetForm()
        {
            txtMaSach.Text = string.Empty;
            txtTenSach.Text = string.Empty;
            txtNamXB.Text = string.Empty;
            cmbTheLoai.SelectedIndex = -1;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadSachToDataGridView(); 
                return;
            }

            List<Sach> listSach = sachBUS.SearchSach(keyword);
            dgvQLSach.DataSource = listSach;

            dgvQLSach.Columns["MaSach"].HeaderText = "Mã Sách";
            dgvQLSach.Columns["TenSach"].HeaderText = "Tên Sách";
            dgvQLSach.Columns["NamXB"].HeaderText = "Năm Xuất Bản";
            dgvQLSach.Columns["LoaiSach"].Visible = false;
        }
    }
}
