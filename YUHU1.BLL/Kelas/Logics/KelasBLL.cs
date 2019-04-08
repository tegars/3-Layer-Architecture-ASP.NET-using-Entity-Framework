using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using AutoMapper;
//using EHS.BLL.SIRSInvestigationAttachmentLogics.Models;
using YUHU1.DAL;
using YUHU1.DataHelpers;
using System.Linq.Expressions;
namespace YUHU1.BLL.KelasBLL
{
    class KelasBLL
    {
        private UnitOfWork _uow;
        IRepository<YUHU1.DAL.Kelas> _repo = null;

        public KelasBLL()
        {
            _uow = new UnitOfWork(new Entities());
            _repo = _uow.Repository<YUHU1.DAL.Kelas>();
        }
    }
}
