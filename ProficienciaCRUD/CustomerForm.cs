using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProficienciaCRUD
{
    public partial class CustomerForm : Form
    {

        private CustomerContext _context;

        public CustomerForm()
        {
            InitializeComponent();
            _context = new CustomerContext();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            dgvCustomers.DataSource = _context.Customer.ToList();
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
        }
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            var customer = new Customer
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text
            };

            _context.Customer.Add(customer);
            _context.SaveChanges();

            LoadCustomers();
            ClearFields();

            MessageBox.Show("Cliente adicionado com sucesso!");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                int rowIndex = dgvCustomers.SelectedRows[0].Index;
                int customerId = (int)dgvCustomers.Rows[rowIndex].Cells["Id"].Value;

                var customer = _context.Customer.Find(customerId);
                if (customer != null)
                {
                    customer.Name = txtName.Text;
                    customer.Email = txtEmail.Text;
                    customer.Phone = txtPhone.Text;

                    _context.SaveChanges();
                    LoadCustomers();
                    ClearFields();
                }
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                int rowIndex = dgvCustomers.SelectedRows[0].Index;
                int customerId = (int)dgvCustomers.Rows[rowIndex].Cells["Id"].Value;

                var customer = _context.Customer.Find(customerId);
                if (customer != null)
                {
                    _context.Customer.Remove(customer);
                    _context.SaveChanges();
                    LoadCustomers();
                    ClearFields();
                }
            }
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];
                row.Cells["Id"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value.ToString();
            }
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];
                row.Cells["Id"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value.ToString();
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}