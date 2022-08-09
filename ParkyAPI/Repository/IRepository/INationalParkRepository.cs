using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
    public interface INationalParkRepository
    {
        ICollection<NationalParkDto> GetNationalParks();

        NationalParkDto GetNationalPark(int nationalParkId);
        bool NationalParkExists(int id);
        bool NationalParkExists(string name);
        bool CreateNationalPark(NationalParkDto nationalPark);
        bool UpdateNationalPark(NationalParkDto nationalPark);
        bool DeleteNationalPark(NationalParkDto nationalPark);
        bool Save();



    }
}
