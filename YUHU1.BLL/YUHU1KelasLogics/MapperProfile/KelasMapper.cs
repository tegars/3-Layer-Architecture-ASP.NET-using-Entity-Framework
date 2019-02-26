using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YUHU1.BLL.YUHU1KelasLogics.Models;
using YUHU1.DAL;
using AutoMapper;
namespace YUHU1.BLL.YUHU1KelasLogics.MapperProfile
{
    class KelasMapper
    {
        public KelasMapper() {
            CreateMap<Kelas, KelasDTO>().ReverseMap();
        }
    }
}
