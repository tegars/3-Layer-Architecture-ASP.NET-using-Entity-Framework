using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YUHU1.DAL;
using YUHU1.DataHelpers;
using System.Linq.Expressions;
using YUHU1.BLL.Kelas.Models;
using AutoMapper;

namespace YUHU1.BLL.KelasBLL
{
    public class KelasBLL
    {
        UnitOfWork _uow;
        IRepository<YUHU1.DAL.Kelas> _repo = null;

        public KelasBLL()
        {
            _uow = new UnitOfWork(new Entities());
            _repo = _uow.Repository<YUHU1.DAL.Kelas>();
        }
        public DataResult<IEnumerable<KelasDto>> GetAll(Expression<Func<YUHU1.DAL.Kelas, bool>> expr = null)
        {
            bool success = true;
            List<string> errors = new List<string>();
            IEnumerable<KelasDto> result = null;
            try
            {
                var getResult = _repo.GetAll(expr);
                if (!getResult.Status.IsSuccess)
                {
                    return new DataResult<IEnumerable<KelasDto>>(null, getResult.Status);
                }
                result = Mapper.Map<IEnumerable<YUHU1.DAL.Kelas>, IEnumerable<KelasDto>>(getResult.Data);
            }
            catch (Exception ex)
            {
                success = false;
                errors.Add(ex.InnerException.Message);
            }
            return new DataResult<IEnumerable<KelasDto>>(result, new StatusResult(success, errors));
        }
    }
}
