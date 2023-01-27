﻿using AutoPrime.Forms;
using BusinessLogicModel.Services;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPrime
{
    public partial class FrmIndex : Form
    {
        private OglasServices oglasServices = new OglasServices();
        public FrmIndex()
        {
            InitializeComponent();
        }

        private void btnKreirajOglas_Click(object sender, EventArgs e)
        {
            FrmCreateAds kreirajOglas = new FrmCreateAds();
            kreirajOglas.ShowDialog();
        }

        private void btnKreirajAukciju_Click(object sender, EventArgs e)
        {
            FrmCreateAuction kreirajAukciju = new FrmCreateAuction();
            kreirajAukciju.ShowDialog();
        }

        private void btnLeasing_Click(object sender, EventArgs e)
        {
            FrmLeasing leasingForma = new FrmLeasing();
            leasingForma.ShowDialog();
        }

        private void btnKalkulator_Click(object sender, EventArgs e)
        {
            PrijavljeniKorisnik dude = new PrijavljeniKorisnik();
            var a = dude.VratiPrijavljenog();
            FrmKalkulator kalkulatorForma = new FrmKalkulator();
            kalkulatorForma.ShowDialog();
        }

        private void btnProfil_Click(object sender, EventArgs e)
        {
            FrmShowProfile profil = new FrmShowProfile();
            profil.ShowDialog();
        }

        private void btnPregledOglasaAukcija_Click(object sender, EventArgs e)
        {
            FrmAdAndAuctionReview frmAdAndAuctionReview = new FrmAdAndAuctionReview();
            frmAdAndAuctionReview.ShowDialog();
        }

        private void FrmIndex_Load(object sender, EventArgs e)
        {
            dgvNajtrazeniji.DataSource = oglasServices.GetMostWantedOglas();
        }
    }
}
