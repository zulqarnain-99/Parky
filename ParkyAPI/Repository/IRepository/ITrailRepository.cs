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
        ICollection<TrailDto> GetTrail();

        TrailDto GetTrail(int trailId);
        bool TrailExists(int id);
        bool trailExists(string name);
        bool CreateTrail(TrailDto trail);
        bool UpdateTrail(TrailDto trail);
        bool Deletetrail(TrailDto trail);
        bool Save();



    }
}
