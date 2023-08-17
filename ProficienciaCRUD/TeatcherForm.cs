using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ProficienciaCRUD
{
    public partial class TeatcherForm : Form
    {

        private TeacherContext _context;

        public TeatcherForm()
        {
            InitializeComponent();
            _context = new TeacherContext();
        }

        private void TeacherForm_Load(object sender, EventArgs e)
        {
            LoadTeachers();
            cbGenero.Items.Add("Masculino");
            cbGenero.Items.Add("Feminino");
            cbGenero.Items.Add("Outros");

            cbxDisciplina.Items.Add("Portugues");
            cbxDisciplina.Items.Add("Matematica");
            cbxDisciplina.Items.Add("Historia");
        }

        private void LoadTeachers()
        {
            dgvTeachers.DataSource = _context.Teacher.ToList();
            dgvTeachers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            cbxDisciplina.SelectedIndex = -1;
            cbGenero.SelectedIndex = -1;
            rdbYes.Checked = false;
            rdbNo.Checked = false;
            nmYears.Value = 0;
            ckbAtivo.Checked = false;
        }

        private string validaExperiencia()
        {
            string experiencia = "";
            if (rdbYes.Checked)
            {
                experiencia = "Sim";
            }
            else if (rdbNo.Checked)
            {
                experiencia = "Não";
            }
            else
            {
                MessageBox.Show("Selecione uma opção antes de salvar.");
            }

            return experiencia;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string active = ckbAtivo.Checked ? "Y" : "N";

            if (!string.IsNullOrWhiteSpace(txtName.Text) &&
                !string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !string.IsNullOrWhiteSpace(txtPhone.Text) &&
                !string.IsNullOrWhiteSpace(cbGenero.Text) &&
                !string.IsNullOrWhiteSpace(cbxDisciplina.Text))
            {
                var teacher = new Teacher
                {
                    Name = txtName.Text,
                    Email = txtEmail.Text,
                    Phone = txtPhone.Text,
                    Gender = cbGenero.Text,
                    Experience = validaExperiencia(),
                    YearsExperience = (int)nmYears.Value,
                    AdmissionDate = dtAdmissao.Value,
                    Active = active,
                    Discipline = cbxDisciplina.Text
                };

                _context.Teacher.Add(teacher);
                _context.SaveChanges();

                LoadTeachers();
                ClearFields();

                MessageBox.Show("Professor adicionado com sucesso!");
            }
            else
            {
                MessageBox.Show("Preencha todos os campos obrigatórios.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvTeachers.SelectedRows.Count > 0)
            {
                int rowIndex = dgvTeachers.SelectedRows[0].Index;
                int TeacherId = (int)dgvTeachers.Rows[rowIndex].Cells["Id"].Value;

                var teatcher = _context.Teacher.Find(TeacherId);
                string active = ckbAtivo.Checked ? "Y" : "N";

                if (teatcher != null)
                {
                    teatcher.Name = txtName.Text;
                    teatcher.Email = txtEmail.Text;
                    teatcher.Phone = txtPhone.Text;
                    teatcher.Gender = cbGenero.Text;
                    teatcher.Experience = validaExperiencia();
                    teatcher.YearsExperience = (int)nmYears.Value;
                    teatcher.AdmissionDate = dtAdmissao.Value;
                    teatcher.Active = active;
                    teatcher.Discipline = cbxDisciplina.Text;

                    _context.SaveChanges();
                    LoadTeachers();
                    ClearFields();

                    MessageBox.Show("Professor atualizado com sucesso!");
                }
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (dgvTeachers.SelectedRows.Count > 0)
            {
                int rowIndex = dgvTeachers.SelectedRows[0].Index;
                int TeacherId = (int)dgvTeachers.Rows[rowIndex].Cells["Id"].Value;

                var teacher = _context.Teacher.Find(TeacherId);
                if (teacher != null)
                {
                    _context.Teacher.Remove(teacher);
                    _context.SaveChanges();
                    LoadTeachers();
                    ClearFields();
                    MessageBox.Show("Professor apagado com sucesso!");
                }
            }
        }

        private void dgvTeachers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTeachers.Rows[e.RowIndex];
                row.Cells["Id"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value.ToString();
                cbGenero.Text = row.Cells["Gender"].Value.ToString();
                cbxDisciplina.Text = row.Cells["Discipline"].Value.ToString();
                string experiencia = row.Cells["Experience"].Value.ToString();

                if (experiencia == "Sim")
                {
                    rdbYes.Checked = true;
                    rdbNo.Checked = false;
                }
                else if (experiencia == "Não")
                {
                    rdbYes.Checked = false;
                    rdbNo.Checked = true;
                }
                else
                {
                    rdbYes.Checked = false;
                    rdbNo.Checked = false;
                }

                // Preenche o NumericUpDown com o valor apropriado
                if (int.TryParse(row.Cells["YearsExperience"].Value.ToString(), out int years))
                {
                    nmYears.Value = years;
                }
                else
                {
                    nmYears.Value = 0; // Valor padrão se a conversão falhar
                }
                // Preenche o CheckBox Ativo
                string ativo = row.Cells["Active"].Value.ToString();
                ckbAtivo.Checked = ativo == "Y";

                // Preenche o DateTimePicker para AdmissionDate (dtAdmissao)
                DateTime admissionDate;
                if (DateTime.TryParse(row.Cells["AdmissionDate"].Value.ToString(), out admissionDate))
                {
                    dtAdmissao.Value = admissionDate;
                }
                else
                {
                    dtAdmissao.Value = DateTime.Today; // Valor padrão se a conversão falhar
                }
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}