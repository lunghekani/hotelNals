using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Layer;

namespace hotel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BusinessLogic objReadOps = new BusinessLogic();  // CREATING A LOCAL INSTANCE OF THE BUSINESS LAYER CLASS AND ITS MYSQL METHODS 

            var dt = objReadOps.BookingsDataTable(); // CREATING A DATASOURCE FOR THE GRIDVIEW 

            dataGridView1.DataSource = dt; // ASSIGNING THE GRIDVIEW

        }
    }
}
