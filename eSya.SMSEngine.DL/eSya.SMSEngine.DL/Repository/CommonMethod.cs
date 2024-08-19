using Microsoft.EntityFrameworkCore;
using eSya.SMSEngine.DL.Entities;
using eSya.SMSEngine.DO;
using eSya.SMSEngine.DO.StaticVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSya.SMSEngine.DL.Repository
{
    public class CommonMethod
    {
        public static string GetValidationMessageFromException(DbUpdateException ex)
        {
            string msg = ex.InnerException == null ? ex.ToString() : ex.InnerException.Message;

            if (msg.LastIndexOf(',') == msg.Length - 1)
                msg = msg.Remove(msg.LastIndexOf(','));
            return msg;
        }

        public async Task<List<DO_BusinessLocation>> GetBusinessKey()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var bk = db.GtEcbslns
                        .Where(w => w.ActiveStatus)
                        .Select(r => new DO_BusinessLocation
                        {
                            BusinessKey = r.BusinessKey,
                            LocationDescription = r.BusinessName + "-" + r.LocationDescription
                        }).ToListAsync();

                    return await bk;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_Forms>> GetFormDetails()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = db.GtEcfmfds
                                  .Where(w => w.ActiveStatus)
                                  .Select(r => new DO_Forms
                                  {
                                      FormID = r.FormId,
                                      FormName = r.FormName
                                  }).OrderBy(o => o.FormName).ToListAsync();
                    return await result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_BusinessLocation>> GetBusinessKeyBySMSIntegration()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var bk = db.GtEcbslns.Join(db.GtEcpabls,
                        f => f.BusinessKey,
                        p => p.BusinessKey,
                        (f, p) => new { f, p })
                        .Where(w => w.f.ActiveStatus && w.p.ParameterId == ParameterIdValues.Form_isSMSIntegration
                                  && w.p.ParmAction && w.p.ActiveStatus)
                        .Select(r => new DO_BusinessLocation
                        {
                            BusinessKey = r.f.BusinessKey,
                            LocationDescription = r.f.BusinessName + "-" + r.f.LocationDescription
                        }).ToListAsync();

                    return await bk;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_Forms>> GetFormForSMSlinking()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    var result = await db.GtEcfmfds
                        .Join(db.GtEcfmpas,
                            f => f.FormId,
                            p => p.FormId,
                            (f, p) => new { f, p })
                       .Where(w => w.f.ActiveStatus
                                  && w.p.ParameterId == ParameterIdValues.Form_isSMSIntegration
                                  && w.p.ActiveStatus)
                                  .Select(r => new DO_Forms
                                  {
                                      FormID = r.f.FormId,
                                      FormCode = r.f.FormCode,
                                      FormName = r.f.FormName
                                  }).OrderBy(o => o.FormCode).ToListAsync();
                    var Distinctforms = result.GroupBy(x => x.FormID).Select(y => y.First());
                    return Distinctforms.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
