using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext _db;

        public TrailRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateTrail(Trail Trial)
        {
            _db.Trail.Add(Trial);
            return Save();
        }

        public bool Deletetrail(Trail trail)
        {
            _db.Trail.Remove(trail);
            return Save();
        }

        public Trail GetTrail(int TrialId)
        {
            return _db.Trail.Include(c => c.NationalPark).FirstOrDefault(a => a.Id == TrialId);
        }

        public ICollection<Trail> GetTrail()
        {
            return _db.Trail.Include(c => c.NationalPark).OrderBy(a => a.Name).ToList();
        }

        public bool TrailExists(int id)
        {
            return _db.Trail.Any(a => a.Id == id);
        }

        public bool trailExists(string name)
        {
            bool value = _db.Trail.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;

        }

        public bool UpdateTrail(Trail Trial)
        {
            _db.Trail.Update(Trial);
            return Save(); 
        }

        public ICollection<Trail> GetTrailsInNationalPark(int npId)
        {
            return _db.Trail.Include(c => c.NationalPark).Where(c => c.NationalParkId == npId).ToList();
        }

    }
}
