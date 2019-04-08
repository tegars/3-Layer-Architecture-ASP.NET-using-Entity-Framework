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
namespace YUHU1.BLL.Kelas
{
    class KelasBLL
    {
        private UnitOfWork _unitOfWork;
        IRepository<Kelas> _masterPicRepo = null;

        public KelasBLL()
        {
            _unitOfWork = new UnitOfWork(new SIRS_Entities());
            _masterPicRepo = _unitOfWork.Repository<MasterPIC>();
        }
    }
}
