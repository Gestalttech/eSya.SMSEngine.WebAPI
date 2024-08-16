using eSya.SMSEngine.DL;
using eSya.SMSEngine.DL.Entities;
using eSya.SMSEngine.DO;
using eSya.SMSEngine.IF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.SMSEngine.DL.Repository
{
    public class SMSEngineRepository : ISMSEngineRepository
    {
        private readonly IStringLocalizer<SMSEngineRepository> _localizer;
        public SMSEngineRepository(IStringLocalizer<SMSEngineRepository> localizer)
        {
            _localizer = localizer;
        }
        #region SMS Variable

        public async Task<List<DO_SMSVariable>> GetSMSVariableInformation()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEcsmsvs
                         .Select(r => new DO_SMSVariable
                         {
                             Smsvariable = r.Smsvariable,
                             Smscomponent = r.Smscomponent,
                             ActiveStatus = r.ActiveStatus
                         }).OrderBy(o => o.Smsvariable).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_SMSVariable>> GetActiveSMSVariableInformation()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEcsmsvs
                        .Where(w => w.ActiveStatus)
                         .Select(r => new DO_SMSVariable
                         {
                             Smsvariable = r.Smsvariable,
                             Smscomponent = r.Smscomponent
                         }).OrderBy(o => o.Smsvariable).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertIntoSMSVariable(DO_SMSVariable obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        bool is_SMSVariableExist = db.GtEcsmsvs.Any(a => a.Smsvariable.Trim().ToUpper() == obj.Smsvariable.Trim().ToUpper());
                        if (is_SMSVariableExist)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0113", Message = string.Format(_localizer[name: "W0113"]) };
                        }

                        bool is_SMSComponentExist = db.GtEcsmsvs.Any(a => a.Smscomponent.Trim().ToUpper() == obj.Smscomponent.Trim().ToUpper());
                        if (is_SMSComponentExist)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0114", Message = string.Format(_localizer[name: "W0114"]) };
                        }

                        var sm_sv = new GtEcsmsv
                        {
                            Smsvariable = obj.Smsvariable,
                            Smscomponent = obj.Smscomponent,
                            ActiveStatus = obj.ActiveStatus,
                            FormId=obj.FormID,
                            CreatedBy = obj.UserID,
                            CreatedOn = DateTime.Now,
                            CreatedTerminal = obj.TerminalID

                        };
                        db.GtEcsmsvs.Add(sm_sv);

                        await db.SaveChangesAsync();
                        dbContext.Commit();


                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<DO_ReturnParameter> UpdateSMSVariable(DO_SMSVariable obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var is_SMSComponentExist = db.GtEcsmsvs.Where(w => w.Smscomponent.Trim().ToUpper().Replace(" ", "") == obj.Smscomponent.Trim().ToUpper().Replace(" ", "")
                                && w.Smsvariable != obj.Smsvariable).FirstOrDefault();
                        if (is_SMSComponentExist != null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0114", Message = string.Format(_localizer[name: "W0114"]) };
                        }

                        GtEcsmsv sm_sv = db.GtEcsmsvs.Where(w => w.Smsvariable == obj.Smsvariable).FirstOrDefault();
                        if (sm_sv == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0115", Message = string.Format(_localizer[name: "W0115"]) };
                        }

                        sm_sv.Smscomponent = obj.Smscomponent;
                        sm_sv.ActiveStatus = obj.ActiveStatus;
                        sm_sv.ModifiedBy = obj.UserID;
                        sm_sv.ModifiedOn = DateTime.Now;
                        sm_sv.ModifiedTerminal = obj.TerminalID;
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<DO_ReturnParameter> ActiveOrDeActiveSMSVariable(bool status, string smsvariable)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEcsmsv sms_var = db.GtEcsmsvs.Where(w => w.Smsvariable.Trim().ToUpper().Replace(" ", "") == smsvariable.Trim().ToUpper().Replace(" ", "")).FirstOrDefault();
                        if (sms_var == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0115", Message = string.Format(_localizer[name: "W0115"]) };
                        }

                        sms_var.ActiveStatus = status;
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        if (status == true)
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0003", Message = string.Format(_localizer[name: "S0003"]) };
                        else
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0004", Message = string.Format(_localizer[name: "S0004"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));

                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        #endregion SMS Variable

        #region SMS Information
        public async Task<List<DO_Forms>> GetExistingFormsFromSMSHeader()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var result = db.GtEcsmshes.Where(x => x.ActiveStatus == true).Join(db.GtEcfmfds,
                        d => d.FormId,
                        f => f.FormId,
                        (d, f) => new DO_Forms
                        {
                            FormID = d.FormId,
                            FormName = f.FormName,

                        }).GroupBy(x => x.FormID).Select(y => y.First()).Distinct().ToListAsync();
                    return await result;
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_SMSTEvent>> GetSMSTriggerEvent()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEcsmsts
                        .Where(w => w.ActiveStatus)
                         .Select(r => new DO_SMSTEvent
                         {
                             TEventID = r.TeventId,
                             TEventDesc = r.TeventDesc,
                             ActiveStatus = r.ActiveStatus
                         }).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_SMSHeader>> GetSMSHeaderInformationByFormId(int formId)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {

                    var ds = db.GtEcsmshes.Join(db.GtEcsmsts,
                        x=> new {x.TeventId},
                        y => new {y.TeventId},
                        (x,y)=>new {x,y})
                       .Where(w => w.x.FormId == formId)
                       .Select(r => new DO_SMSHeader
                         {
                             Smsid = r.x.Smsid,
                             Smsdescription = r.x.Smsdescription,
                             IsVariable = r.x.IsVariable,
                             TEventID = r.x.TeventId,
                             Smsstatement = r.x.Smsstatement,
                             ActiveStatus = r.x.ActiveStatus,
                             TEventDesc=r.y.TeventDesc
                       }).OrderBy(o => o.Smsid).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_SMSHeader> GetSMSHeaderInformationBySMSId(string smsId)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEcsmshes
                        .Where(w => w.Smsid == smsId)
                         .Select(r => new DO_SMSHeader
                         {
                             Smsid = r.Smsid,
                             Smsdescription = r.Smsdescription,
                             IsVariable = r.IsVariable,
                             TEventID = r.TeventId,
                             Smsstatement = r.Smsstatement,
                             ActiveStatus = r.ActiveStatus,
                             l_SMSParameter = r.GtEcsmsds.Select(p => new DO_eSyaParameter
                             {
                                 ParameterID = p.ParameterId,
                                 ParmAction = p.ParmAction
                             }).ToList()
                         }).FirstOrDefaultAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertIntoSMSHeader(DO_SMSHeader obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        bool is_SMSDescExist = db.GtEcsmshes.Any(a => a.Smsdescription.Trim().ToUpper() == obj.Smsdescription.Trim().ToUpper());
                        if (is_SMSDescExist)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0116", Message = string.Format(_localizer[name: "W0116"]) };
                        }

                        var smsIdNumber = db.GtEcsmshes.Where(w => w.FormId == obj.FormId).Count();
                        string smsId = obj.FormId.ToString() + "_" + (smsIdNumber + 1).ToString();

                        var sm_sh = new GtEcsmsh
                        {
                            Smsid = smsId,
                            FormId = obj.FormId,
                            Smsdescription = obj.Smsdescription,
                            IsVariable = obj.IsVariable,
                            TeventId = obj.TEventID,
                            Smsstatement = obj.Smsstatement,
                            ActiveStatus = obj.ActiveStatus,
                            CreatedBy = obj.UserID,
                            CreatedOn = DateTime.Now,
                            CreatedTerminal = obj.TerminalID

                        };
                        db.GtEcsmshes.Add(sm_sh);

                        foreach (var p in obj.l_SMSParameter)
                        {
                            var sm_sd = new GtEcsmsd
                            {
                                Smsid = smsId,
                                ParameterId = p.ParameterID,
                                ParmAction = p.ParmAction,
                                ActiveStatus = obj.ActiveStatus,
                                FormId=obj.FormId.ToString(),
                                CreatedOn = DateTime.Now,
                                CreatedTerminal = obj.TerminalID,
                                CreatedBy = obj.UserID,
                            };
                            db.GtEcsmsds.Add(sm_sd);
                        }

                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<DO_ReturnParameter> UpdateSMSHeader(DO_SMSHeader obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var is_SMSComponentExist = db.GtEcsmshes.Where(w => w.Smsdescription.Trim().ToUpper().Replace(" ", "") == obj.Smsdescription.Trim().ToUpper().Replace(" ", "")
                                && w.Smsid != obj.Smsid && w.FormId == obj.FormId).FirstOrDefault();
                        if (is_SMSComponentExist != null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0116", Message = string.Format(_localizer[name: "W0116"]) };
                        }

                        GtEcsmsh sm_sh = db.GtEcsmshes.Where(w => w.Smsid == obj.Smsid).FirstOrDefault();
                        if (sm_sh == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0117", Message = string.Format(_localizer[name: "W0117"]) };
                        }

                        sm_sh.Smsdescription = obj.Smsdescription;
                        sm_sh.IsVariable = obj.IsVariable;
                        sm_sh.TeventId = obj.TEventID;
                        sm_sh.Smsstatement = obj.Smsstatement;
                        sm_sh.ActiveStatus = obj.ActiveStatus;
                        sm_sh.ModifiedBy = obj.UserID;
                        sm_sh.ModifiedOn = DateTime.Now;
                        sm_sh.ModifiedTerminal = obj.TerminalID;

                        foreach (var p in obj.l_SMSParameter)
                        {
                            var sm_sd = db.GtEcsmsds.Where(w => w.Smsid == obj.Smsid && w.ParameterId == p.ParameterID).FirstOrDefault();
                            if (sm_sd == null)
                            {
                                sm_sd = new GtEcsmsd
                                {
                                    Smsid = obj.Smsid,
                                    ParameterId = p.ParameterID,
                                    ParmAction = p.ParmAction,
                                    ActiveStatus = obj.ActiveStatus,
                                    FormId=obj.FormId.ToString(),
                                    CreatedOn = DateTime.Now,
                                    CreatedTerminal = obj.TerminalID,
                                    CreatedBy = obj.UserID,
                                };
                                db.GtEcsmsds.Add(sm_sd);
                            }
                            else
                            {
                                sm_sd.ParmAction = p.ParmAction;
                                sm_sd.ActiveStatus = obj.ActiveStatus;
                                sm_sd.ModifiedBy = obj.UserID;
                                sm_sd.ModifiedOn = System.DateTime.Now;
                                sm_sd.ModifiedTerminal = obj.TerminalID;
                            }
                        }

                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        #endregion SMS Information

        #region SMS Recipient

        public async Task<List<DO_SMSHeader>> GetSMSHeaderForRecipientByFormIdandParamId(int formId,int parameterId)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEcsmsds
                        .Where(w => w.ParameterId == parameterId && w.Sms.FormId == formId && w.ActiveStatus && w.Sms.ActiveStatus)
                         .Select(r => new DO_SMSHeader
                         {
                             Smsid = r.Smsid,
                             Smsdescription = r.Sms.Smsdescription,
                             Smsstatement = r.Sms.Smsstatement,
                             ActiveStatus = r.ActiveStatus
                         }).OrderBy(o => o.Smsid).ToListAsync();

                    return await ds;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DO_SMSRecipient>> GetSMSRecipientByBusinessKeyAndSMSId(int businessKey,string smsId)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEcsmsrs
                        .Where(w => w.BusinessKey == businessKey && w.Smsid == smsId)
                         .Select(r => new DO_SMSRecipient
                         {
                             Smsid = r.Smsid,
                             Isdcode=r.Isdcode,
                             MobileNumber = r.MobileNumber,
                             RecipientName = r.RecipientName,
                             Remarks = r.Remarks,
                             ActiveStatus = r.ActiveStatus
                         }).OrderBy(o => o.RecipientName).ToListAsync();

                    return await ds;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertIntoSMSRecipient(DO_SMSRecipient obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        bool is_MobilenumberExist = db.GtEcsmsrs.Any(a =>a.Isdcode==obj.Isdcode && a.MobileNumber.Trim() == obj.MobileNumber.Trim() && a.Smsid == obj.Smsid);
                        if (is_MobilenumberExist)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0118", Message = string.Format(_localizer[name: "W0118"]) };
                        }

                        var sm_sr = new GtEcsmsr
                        {
                            BusinessKey = obj.BusinessKey,
                            Smsid = obj.Smsid,
                            Isdcode=obj.Isdcode,
                            MobileNumber = obj.MobileNumber,
                            RecipientName = obj.RecipientName,
                            Remarks = obj.Remarks,
                            ActiveStatus = obj.ActiveStatus,
                            FormId=obj.FormId,
                            CreatedBy = obj.UserID,
                            CreatedOn = DateTime.Now,
                            CreatedTerminal = obj.TerminalID

                        };
                        db.GtEcsmsrs.Add(sm_sr);

                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<DO_ReturnParameter> UpdateSMSRecipient(DO_SMSRecipient obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEcsmsr sm_sr = db.GtEcsmsrs.Where(w => w.BusinessKey == obj.BusinessKey && w.Smsid == obj.Smsid && w.Isdcode==obj.Isdcode && w.MobileNumber == obj.MobileNumber).FirstOrDefault();
                        if (sm_sr == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0119", Message = string.Format(_localizer[name: "W0119"]) };
                        }

                        sm_sr.RecipientName = obj.RecipientName;
                        sm_sr.Remarks = obj.Remarks;
                        sm_sr.ActiveStatus = obj.ActiveStatus;
                        sm_sr.ModifiedBy = obj.UserID;
                        sm_sr.ModifiedOn = DateTime.Now;
                        sm_sr.ModifiedTerminal = obj.TerminalID;

                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }


        #endregion SMS Recipient

        #region Trigger Event

        public async Task<List<DO_SMSTEvent>> GetAllSMSTriggerEvents()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEcsmsts
                         .Select(t => new DO_SMSTEvent
                         {
                             TEventID = t.TeventId,
                             TEventDesc = t.TeventDesc,
                             ActiveStatus = t.ActiveStatus
                         }).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertIntoSMSTriggerEvent(DO_SMSTEvent obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {

                        bool is_SMSTeventExist = db.GtEcsmsts.Any(a => a.TeventId == obj.TEventID);
                        if (is_SMSTeventExist)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0120", Message = string.Format(_localizer[name: "W0120"]) };
                        }

                        bool is_SMSTdescExist = db.GtEcsmsts.Any(a => a.TeventDesc.Trim().ToUpper() == obj.TEventDesc.Trim().ToUpper());
                        if (is_SMSTdescExist)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0121", Message = string.Format(_localizer[name: "W0121"]) };
                        }

                        var sm_tevnt = new GtEcsmst
                        {
                            TeventId = obj.TEventID,
                            TeventDesc = obj.TEventDesc,
                            ActiveStatus = obj.ActiveStatus,
                            FormId=obj.FormID,
                            CreatedBy = obj.UserID,
                            CreatedOn = DateTime.Now,
                            CreatedTerminal = obj.TerminalID

                        };
                        db.GtEcsmsts.Add(sm_tevnt);

                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<DO_ReturnParameter> UpdateSMSTriggerEvent(DO_SMSTEvent obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var is_SMSTEventExist = db.GtEcsmsts.Where(w => w.TeventDesc.Trim().ToUpper().Replace(" ", "") == obj.TEventDesc.Trim().ToUpper().Replace(" ", "")
                                && w.TeventId != obj.TEventID).FirstOrDefault();
                        if (is_SMSTEventExist != null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0121", Message = string.Format(_localizer[name: "W0121"]) };
                        }

                        GtEcsmst sm_tevent = db.GtEcsmsts.Where(w => w.TeventId == obj.TEventID).FirstOrDefault();
                        if (sm_tevent == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0122", Message = string.Format(_localizer[name: "W0122"]) };
                        }

                        sm_tevent.TeventDesc = obj.TEventDesc;
                        sm_tevent.ActiveStatus = obj.ActiveStatus;
                        sm_tevent.ModifiedBy = obj.UserID;
                        sm_tevent.ModifiedOn = DateTime.Now;
                        sm_tevent.ModifiedTerminal = obj.TerminalID;
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<DO_ReturnParameter> DeleteSMSTriggerEvent(int TeventId)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEcsmst sm_tevent = db.GtEcsmsts.Where(w => w.TeventId == TeventId).FirstOrDefault();

                        if (sm_tevent == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0123", Message = string.Format(_localizer[name: "W0123"]) };
                        }

                        db.GtEcsmsts.Remove(sm_tevent);

                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0005", Message = string.Format(_localizer[name: "S0005"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<DO_ReturnParameter> ActiveOrDeActiveSMSTriggerEvent(bool status, int TriggerEventId)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        GtEcsmst t_evevt = db.GtEcsmsts.Where(w => w.TeventId == TriggerEventId).FirstOrDefault();
                        if (t_evevt == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0124", Message = string.Format(_localizer[name: "W0124"]) };
                        }

                        t_evevt.ActiveStatus = status;
                        await db.SaveChangesAsync();
                        dbContext.Commit();

                        if (status == true)
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0003", Message = string.Format(_localizer[name: "S0003"]) };
                        else
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0004", Message = string.Format(_localizer[name: "S0004"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));

                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        #endregion SMS Trigger Event

        #region Manage SMS Location Wise

        public async Task<List<DO_SMSHeader>> GetSMSInformationFormLocationWise(int businessKey, int formId)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {

                    var ds = await db.GtEcsmshes.Join(db.GtEcsmsts,
                        x => new { x.TeventId },
                        y => new { y.TeventId },
                        (x, y) => new { x, y })
                       .Where(w => w.x.FormId == formId)
                       .Select(r => new DO_SMSHeader
                       {
                           Smsid = r.x.Smsid,
                           Smsdescription = r.x.Smsdescription,
                           IsVariable = r.x.IsVariable,
                           TEventID = r.x.TeventId,
                           TEventDesc = r.y.TeventDesc,
                           Smsstatement = r.x.Smsstatement,
                           ActiveStatus = r.x.ActiveStatus
                       }).OrderBy(o => o.Smsid).ToListAsync();

                    foreach (var obj in ds)
                    {
                        GtSmsloc pf = db.GtSmslocs.Where(x => x.BusinessKey == businessKey && x.FormId == formId).FirstOrDefault();
                        if (pf != null)
                        {
                            obj.ActiveStatus = pf.ActiveStatus;
                        }
                        else
                        {
                            obj.ActiveStatus = false;

                        }
                    }
                    return ds;
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DO_ReturnParameter> InsertOrUpdateSMSInformationFLW(List<DO_SMSHeader> obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var Sms_loc in obj)
                        {
                            GtSmsloc Smsloc = db.GtSmslocs.Where(x => x.BusinessKey == Sms_loc.BusinessKey && x.FormId == Sms_loc.FormId && x.Smsid == Sms_loc.Smsid).FirstOrDefault();
                            if (Smsloc != null)
                            {
                                Smsloc.BusinessKey = Sms_loc.BusinessKey;
                                Smsloc.FormId = Sms_loc.FormId;
                                Smsloc.Smsid = Sms_loc.Smsid;
                                Smsloc.ActiveStatus = Sms_loc.ActiveStatus;
                                Smsloc.ModifiedBy = Sms_loc.UserID;
                                Smsloc.ModifiedOn = System.DateTime.Now;
                                Smsloc.ModifiedTerminal = Sms_loc.TerminalID;
                            }
                            else
                            {
                                var Smsloc1 = new GtSmsloc
                                {
                                    BusinessKey = Sms_loc.BusinessKey,
                                    FormId = Sms_loc.FormId,
                                    Smsid = Sms_loc.Smsid,
                                    ActiveStatus = Sms_loc.ActiveStatus,
                                    CreatedBy = Sms_loc.UserID,
                                    FormId1 = Sms_loc.FormId1,
                                    CreatedOn = System.DateTime.Now,
                                    CreatedTerminal = Sms_loc.TerminalID
                                };
                                db.GtSmslocs.Add(Smsloc1);
                            }
                                await db.SaveChangesAsync();
                        }
                        dbContext.Commit();
                        return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                    catch (Exception ex)
                    {
                        dbContext.Rollback();
                        throw ex;
                    }
                }
            }
        }
        #endregion Manage SMS Location Wise
    }
}
