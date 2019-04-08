using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using YUHU1.BLL.Kelas.Models;
using YUHU1.DAL;


namespace YUHU1.BLL.Kelas.MapperProfile
{
    public class KelasMapper : Profile
    {
        public KelasMapper() {
            CreateMap<YUHU1.DAL.Kelas, KelasDto>().ReverseMap();
        }
    }
}
