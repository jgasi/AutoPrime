﻿using BusinessLogicModel;
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
    public partial class FrmKalkulatorDetails : Form
    {
        //Andrej Antonić
        //Inicijalizacija svih potrebnih vrijednosti
        int page = 1;
        int year;
        double price;
        double mileage;
        KalkulatorLogic kalkulatorLogic = new KalkulatorLogic();
        public FrmKalkulatorDetails(int year, double price, double mileage) //Konstruktor forme gdje primamo vrijednosti sa prošle forme
        {
            InitializeComponent();
            DateTime now = DateTime.Today;
            this.year = Int32.Parse(now.ToString("yyyy")) - year;
            this.price = price;
            this.mileage = mileage;
        }

        private void FrmKalkulatorDetails_Load(object sender, EventArgs e) //Učitavanje forme
        {
            LoadLabels();
        }

        private void LoadLabels() //Učitavanje labela gdje se prikazuje tekst
        {
            if(page == 1)
            {
                LoadFirstPage();
            }
            else if(page == 2)
            {
                LoadSecondPage();
            }
        }

        private void LoadSecondPage() //Učitavanje druge stranice teksta i poziv druge metode izračuna
        {
            btnBack.Enabled = true;
            btnAhead.Enabled = false;
            double calculaterPrice = kalkulatorLogic.CalculateBasedOnKilometers(price, mileage); //Poziv metoda za računanje druge metode
            lblMethod.Text = "Metoda 2: bazirano na cijeni po kilometru"; //Prikazivanje opisa u labeli
            lblDescription.Text = "Prosječni godišnji trošak posjedovanja i vožnje automobila u 2021. godini iznosio je 10197.07€ od čega je" +
                "\r\n37% prepisano amortizaciji (3772.92€). " +
                "\r\n\r\nProsječna udaljenost prijeđena automobilom na godišnoj razini je 11313km, što stavlja prosječnu" +
                "\r\namortizaciju po kilometru na 0.33€ (3772,92€ / 11313)." +
                "\r\n\r\nPočetna cijena vašeg automobila = " + price + "€, prijeđenih kilometara = " + mileage + "." +
                "\r\n\r\n      " + price + " - (" + mileage + "x0.33) = " + calculaterPrice + "€";
            
        }

        private void LoadFirstPage() //Učitavanje prve stranice teksta i poziv prve metode izračuna
        {
            btnBack.Enabled = false;
            btnAhead.Enabled = true;
            lblMethod.Text = "Metoda 1: bazirano na starosti automobila"; //Prikazivanje opisa u labeli
            lblDescription.Text = "Ova se metoda temelji na nabavnoj cijeni vozila, njegovoj trenutnoj starosti i procijenjenim godišnjim stopama" +
                " \r\namortizacije.\r\n\r\nCijena i starost vozila su poznate vrijednosti, no njegove godišnje stope amortizacije su spekulativne i jako" +
                " \r\nvariraju od marke do modela. Stope amortizacije su procijenjene na temelju prosjeka svih izvještajnih agencija." +
                "\r\n\r\nKonsenzus je da se novi automobili amortiziraju u prosjeku 24% u prvoj godini i 15% u preostalim godinama." +
                "\r\n\r\nPočetna cijena vašeg auta = " + price + "€, razdoblje = " + year + " godina.";
            double tempPrice = price;
            List<double> listPrices = kalkulatorLogic.CalculateBasedOnAge(price, year); //Poziv metoda za računanje prve metode
            var increment = 1;
            foreach (var item in listPrices) //Ispisivanje postupka računa
            {
                lblDescription.Text += "\r\n\r\n      Godina " + increment + ". završna vrijednost = " + tempPrice + " - (" + tempPrice + "x0.24) = " + item + "€";
                tempPrice = item; 
                increment++;
            }
        }

        private void btnAhead_Click(object sender, EventArgs e) //Prebacivanje na drugu stranicu
        {
            page++;
            LoadLabels();
        }

        private void btnBack_Click(object sender, EventArgs e) //Vraćanje na prvu stranicu
        {
            page--;
            LoadLabels();
        }
    }
}
