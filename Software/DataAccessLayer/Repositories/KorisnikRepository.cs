﻿using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class KorisnikRepository : Repository<Korisnik>
    {
        public KorisnikRepository() : base(new AutoPrimeModel())
        {

        }

        public override IQueryable<Korisnik> GetAll()
        {
            var query = from e in Entities
                        select e;

            return query;
        }

        public IQueryable<Korisnik> GetCertainKorisnik(string phrase)
        {
            var query = from e in Entities
                        where e.Ime.Contains(phrase) || e.Prezime.Contains(phrase)
                        select e;

            return query;
        }

        public IQueryable<Korisnik> GetKorisnikById(int id)
        {
            var query = from e in Entities
                        where e.Id_korisnika == id
                        select e;

            return query;
        }

        //Danijel Žebčević
        public IQueryable<Korisnik> PrijaviKorisnika(string korisnickoIme, string lozinka)
        {
            var query = from e in Entities
                        where e.Lozinka == lozinka && e.Korimme == korisnickoIme
                        select e;

            return query;
        }
        //Danijel Žebčević
        public IQueryable<Korisnik> MijenjajLozinku(string korime, string telefon, string ime,string prezime)
        {
            var query = from e in Entities
                        where e.Korimme == korime && e.Broj_telefona == telefon && e.Ime == ime && e.Prezime == prezime
                        select e;

            return query;
        }

        public override int Add(Korisnik entity, bool saveChanges = true)
        {
            var korisnikk = new Korisnik
            {
                Ime = entity.Ime,
                Prezime = entity.Prezime,
                Korimme = entity.Korimme,
                Lozinka = entity.Lozinka,
                Broj_telefona = entity.Broj_telefona,
                Grad = entity.Grad
            };

            Entities.Add(korisnikk);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public override int Update(Korisnik entity, bool saveChanges = true)
        {
            var korisnikk = Entities.SingleOrDefault(k => k.Id_korisnika == entity.Id_korisnika);

            korisnikk.Id_korisnika = entity.Id_korisnika;
            korisnikk.Ime = entity.Ime;
            korisnikk.Prezime = entity.Prezime;
            korisnikk.Korimme = entity.Korimme;
            korisnikk.Lozinka = entity.Lozinka;
            korisnikk.Broj_telefona = entity.Broj_telefona;
            korisnikk.Grad = entity.Grad;

            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
    }
}
