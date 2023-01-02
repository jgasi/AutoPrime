﻿using BusinessLogicModel.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPrime.Forms
{
    public partial class FrmAdAndAuctionReview : Form
    {
        private OglasServices oglasServices = new OglasServices();
        private AukcijeServices aukcijeServices = new AukcijeServices();
        public FrmAdAndAuctionReview()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmAdAndAuctionReview_Load(object sender, EventArgs e)
        {
            PrikaziOglase();
            PrikaziAukcije();
        }

        private void PrikaziAukcije()
        {
            dgvAukcije.DataSource = aukcijeServices.GetAukcije();
        }

        private void PrikaziOglase()
        {
            dgvOglasi.DataSource = oglasServices.GetOglas();
        }
    }
}
