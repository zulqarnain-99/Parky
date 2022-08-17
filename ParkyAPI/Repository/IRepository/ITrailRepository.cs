using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
    public interface ITrailRepository
    {
        ICollection<Trail> GetTrail();
        ICollection<Trail> GetTrailsInNationalPark(int npId);

        Trail GetTrail(int trailId);
        bool TrailExists(int id);
        bool trailExists(string name);
        bool CreateTrail(Trail trail);
        bool UpdateTrail(Trail trail);
        bool Deletetrail(Trail trail);
        bool Save();



    }
}
