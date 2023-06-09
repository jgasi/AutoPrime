﻿using BusinessLogicModel.Services;
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoPrime.Forms
{
    public partial class FrmCreateAds : Form
    {
        //Juraj Gaši
        private OglasServices oglasServis = new OglasServices();
        private ModelServices modelServis = new ModelServices();
        private MotorServices motorServis = new MotorServices();
        private MarkaServices markaServices = new MarkaServices();
        private OstecenjaServices ostecenjaServis = new OstecenjaServices();
        private PrijavljeniKorisnik prijavljeni = new PrijavljeniKorisnik();


        public FrmCreateAds()
        {
            InitializeComponent();
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDodajOglas_Click(object sender, EventArgs e)
        {
            try
            {
                //kreiranje novog oglasa preuzimajuci unese podatke sa forme
                var oglas = new Ogla
                {
                    korisnik_id = prijavljeni.VratiPrijavljeniId(),
                    naziv = txtNaslovOglasa.Text,
                    Marka = cmbMarkaVozila.SelectedItem as Marka,
                    Model = cmbModelVozila.SelectedItem as Model,
                    lokacija_vozila = txtLokacija.Text,
                    godina = Int32.Parse(txtGodinaProizvodnje.Text),
                    cijena = txtCijena.Text,
                    kilometraza = txtKilometraza.Text,
                    Motor = cmbMotor.SelectedItem as Motor,
                    ostecenje = (byte)cbOstecenja.CheckState,
                    leasing = (byte)cbLeasing.CheckState,
                    datum = dtpDatum.Value
                };

                /*var ostecenja = new Oštećenja
                {

                };

                var slika = new Slika
                {

                };*/

                oglasServis.AddOglas(oglas);
                //ostecenjaServis.AddOstecenja(ostecenja);
                //slikaServis.AddSlika(slika);
                Close();
            }
            catch
            {
                //upozorenje da nisu pravilno upisani podaci
                MessageBox.Show("Potrebno je popuniti sve podatke!");
            }
        }

        private void FrmCreateAds_Load(object sender, EventArgs e)
        {
            //ucitavanje podataka u comboboxeve
            LoadAllMarke();
            LoadAllMotori();
            LoadAllModeli();
        }

        private void LoadAllModeli()
        {
            cmbModelVozila.DataSource = modelServis.GetModels();
        }

        private void LoadAllMotori()
        {
            cmbMotor.DataSource = motorServis.GetMotors();
        }

        private void LoadAllMarke()
        {
            cmbMarkaVozila.DataSource = markaServices.GetMarkas();
        }

        private void cmbMarkaVozila_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadWantedModels();
        }

        private void LoadWantedModels()
        {
            //ucitavanje modela ovisno o marci vozila (npr. ako je Nissan marka, model ne moze biti Tiguan)
            var odabrano = cmbMarkaVozila.SelectedItem as Marka;
            int zeljeno;
            zeljeno = odabrano.Id_marka;
            cmbModelVozila.DataSource = modelServis.GetCertainModels(zeljeno);
        }

        string imgLocation = "";


        private void btnPretraziSliku_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = " png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                pbSlika.ImageLocation = imgLocation;
            }
        }

        private void btnDodajSliku_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] images = null;
                FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                BinaryReader binaryReader = new BinaryReader(stream);
                images = binaryReader.ReadBytes((int)stream.Length);

                using (var repo = new SlikaRepository())
                {
                    Slika slika = new Slika { slika1 = images?.Length > 0 ? images[0] : (byte?)null };
                    var slikaServis = new SlikaServices();
                    slikaServis.AddSlika(slika);
                }

                MessageBox.Show("Data saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDodajOstecenja_Click(object sender, EventArgs e)
        {
            try
            {

                // kreiranje objekta OpenFileDialog za pretragu slike

                OpenFileDialog open = new OpenFileDialog();

                // filtriranje dialogboxa kako bi korisnik mogao izabrati samo određene vrste slike

                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

                //ako je korisnik odabrao neku sliku radi sljedece

                if (open.ShowDialog() == DialogResult.OK)
                {
                    //kreiraj objekt Image klase i pridruzi ime slike, dodaj sliku u bazu pomocu funkcije

                    Image img = new Bitmap(open.FileName);
                    //converterDemo(img);
                }
            }
            catch
            {
                MessageBox.Show("Nije moguce izabrati takvu sliku.");
            }
        }
        public static byte converterDemo(Image x)
        {
            //Converter slike sa racunala u oblik byte
            ImageConverter _imageConverter = new ImageConverter();
            byte xByte = (byte)_imageConverter.ConvertTo(x, typeof(byte));
            return xByte;
        }

        //F1 pomoć
        private void FrmCreateAds_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            string presentationLayerRoot = Directory.GetParent(Directory.GetParent(Directory.GetParent(Application.ExecutablePath).FullName).FullName).FullName;
            string pdfPath = presentationLayerRoot + "\\HelpDocumentation\\HelpDocumentationFrmCreateAds.pdf";
            Process.Start(pdfPath);
        }

        
    }
}
