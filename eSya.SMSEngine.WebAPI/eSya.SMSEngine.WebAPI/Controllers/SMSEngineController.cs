﻿using eSya.SMSEngine.DO;
using eSya.SMSEngine.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.SMSEngine.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SMSEngineController : ControllerBase
    {
        private readonly ISMSEngineRepository _SMSEngineRepository;
        public SMSEngineController(ISMSEngineRepository smsEngineRepository)
        {
            _SMSEngineRepository = smsEngineRepository;
        }
        #region SMS variables need to remove shifted to product setup
        /// <summary>
        /// Get SMS Variable Information.
        /// UI Reffered - SMS Variable
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetSMSVariableInformation()
        {
            var sm_sv = await _SMSEngineRepository.GetSMSVariableInformation();
            return Ok(sm_sv);
        }

        /// <summary>
        /// Insert into SMS Variable .
        /// UI Reffered - SMS Variable
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoSMSVariable(DO_SMSVariable obj)
        {
            var msg = await _SMSEngineRepository.InsertIntoSMSVariable(obj);
            return Ok(msg);

        }

        /// <summary>
        /// Update SMS Variable .
        /// UI Reffered - SMS Variable
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateSMSVariable(DO_SMSVariable obj)
        {
            var msg = await _SMSEngineRepository.UpdateSMSVariable(obj);
            return Ok(msg);

        }

        /// <summary>
        /// Active Or De Active SMS Variable.
        /// UI Reffered - SMS Variable
        /// </summary>
        /// <param name="status-smsvariable"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ActiveOrDeActiveSMSVariable(bool status, string smsvariable)
        {
            var msg = await _SMSEngineRepository.ActiveOrDeActiveSMSVariable(status, smsvariable);
            return Ok(msg);
        }
        #endregion

        #region SMS Template
        /// <summary>
        /// Get Active SMS Variable Information.
        /// UI Reffered - SMS Information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetActiveSMSVariableInformation()
        {
            var sm_sv = await _SMSEngineRepository.GetActiveSMSVariableInformation();
            return Ok(sm_sv);
        }


        /// <summary>
        /// Get Existing Forms from SMS Header.
        /// UI Reffered - SMS Information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetExistingFormsFromSMSHeader()
        {
            var ex_forms = await _SMSEngineRepository.GetExistingFormsFromSMSHeader();
            return Ok(ex_forms);
        }
        /// <summary>
        /// Get Max Sequence Number.
        /// UI Reffered - SMS Information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMaxSequenceNumberbyTriggerEventID(int TeventID)
        {
            var ex_forms = await _SMSEngineRepository.GetMaxSequenceNumberbyTriggerEventID(TeventID);
            return Ok(ex_forms);
        }
        /// <summary>
        /// Get SMS Header Information by Formid.
        /// UI Reffered - SMS Information
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetSMSTriggerEvent()
        {
            var sm_sh = await _SMSEngineRepository.GetSMSTriggerEvent();
            return Ok(sm_sh);
        }

        /// <summary>
        /// Get SMS Header Information by Formid.
        /// UI Reffered - SMS Information
        /// </summary>
        /// <param name="formId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetSMSHeaderInformationByFormId(int formId)
        {
            var sm_sh = await _SMSEngineRepository.GetSMSHeaderInformationByFormId(formId);
            return Ok(sm_sh);
        }

        /// <summary>
        /// Get SMS Header Information by SMSid.
        /// UI Reffered - SMS Information
        /// </summary>
        /// <param name="smsId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetSMSHeaderInformationBySMSId(string smsId)
        {
            var sm_sh = await _SMSEngineRepository.GetSMSHeaderInformationBySMSId(smsId);
            return Ok(sm_sh);
        }


        /// <summary>
        /// Insert into SMS Header .
        /// UI Reffered - SMS Information
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoSMSHeader(DO_SMSHeader obj)
        {
            var msg = await _SMSEngineRepository.InsertIntoSMSHeader(obj);
            return Ok(msg);

        }

        /// <summary>
        /// Update SMS Header .
        /// UI Reffered - SMS Information
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateSMSHeader(DO_SMSHeader obj)
        {
            var msg = await _SMSEngineRepository.UpdateSMSHeader(obj);
            return Ok(msg);

        }

        /// <summary>
        /// Get SMS Header Information For Recipient Parameter by formId and Parameter Id.
        /// UI Reffered - SMS Recipient
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="parameterId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetSMSHeaderForRecipientByFormIdandParamId(int formId, int parameterId)
        {
            var sm_sh = await _SMSEngineRepository.GetSMSHeaderForRecipientByFormIdandParamId(formId, parameterId);
            return Ok(sm_sh);
        }

        /// <summary>
        /// Get SMS Recipient Information by businessKey and SMSId.
        /// UI Reffered - SMS Recipient
        /// </summary>
        /// <param name="businessKey"></param>
        /// <param name="smsId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetSMSRecipientByBusinessKeyAndSMSId(int businessKey, string smsId)
        {
            var sm_sh = await _SMSEngineRepository.GetSMSRecipientByBusinessKeyAndSMSId(businessKey, smsId);
            return Ok(sm_sh);
        }

        /// <summary>
        /// Insert into SMS Recipient .
        /// UI Reffered - SMS Recipient
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertIntoSMSRecipient(DO_SMSRecipient obj)
        {
            var msg = await _SMSEngineRepository.InsertIntoSMSRecipient(obj);
            return Ok(msg);

        }

        /// <summary>
        /// Update SMS Recipient .
        /// UI Reffered - SMS Recipient
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UpdateSMSRecipient(DO_SMSRecipient obj)
        {
            var msg = await _SMSEngineRepository.UpdateSMSRecipient(obj);
            return Ok(msg);

        }
        #endregion

        //#region Trigger Event shifted to product setup API
        ///// <summary>
        ///// Get SMS Trigger Event by Trigger event Id.
        ///// UI Reffered - SMS Trigger Event
        ///// </summary>
        ///// <param name="TeventId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> DeleteSMSTriggerEvent(int TeventId)
        //{
        //    var msg = await _SMSEngineRepository.DeleteSMSTriggerEvent(TeventId);
        //    return Ok(msg);
        //}

        ///// <summary>
        ///// Get SMS Trigger Event.
        ///// UI Reffered - SMS Trigger Event
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> GetAllSMSTriggerEvents()
        //{
        //    var sms_tevents = await _SMSEngineRepository.GetAllSMSTriggerEvents();
        //    return Ok(sms_tevents);
        //}

        ///// <summary>
        ///// Insert into SMS Trigger Event .
        ///// UI Reffered - SMS Trigger Event
        ///// </summary>
        //[HttpPost]
        //public async Task<IActionResult> InsertIntoSMSTriggerEvent(DO_SMSTEvent obj)
        //{
        //    var msg = await _SMSEngineRepository.InsertIntoSMSTriggerEvent(obj);
        //    return Ok(msg);

        //}

        ///// <summary>
        ///// Update SMS Trigger Event .
        ///// UI Reffered - SMS Trigger Event
        ///// </summary>
        //[HttpPost]
        //public async Task<IActionResult> UpdateSMSTriggerEvent(DO_SMSTEvent obj)
        //{
        //    var msg = await _SMSEngineRepository.UpdateSMSTriggerEvent(obj);
        //    return Ok(msg);

        //}

        ///// <summary>
        ///// Active Or De Active SMS Trigger Event.
        ///// UI Reffered - SMS Trigger Event
        ///// </summary>
        ///// <param name="status-smsvariable"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<IActionResult> ActiveOrDeActiveSMSTriggerEvent(bool status, int TriggerEventId)
        //{
        //    var msg = await _SMSEngineRepository.ActiveOrDeActiveSMSTriggerEvent(status, TriggerEventId);
        //    return Ok(msg);
        //}
        //#endregion SMS Trigger Event

        #region Manage SMS Location Wise
        /// <summary>
        /// Get SMS Information by businessKey and FormID.
        /// UI Reffered - Manage SMS Location Wise
        /// </summary>
        /// <param name="businessKey"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetSMSInformationFormLocationWise(int businessKey, int formId)
        {
            var sm_sh = await _SMSEngineRepository.GetSMSInformationFormLocationWise(businessKey, formId);
            return Ok(sm_sh);
        }

        /// <summary>
        /// Insert into SMS Recipient .
        /// UI Reffered - SMS Recipient
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateSMSInformationFLW(List<DO_BusinessFormSMSLink> obj)
        {
            var msg = await _SMSEngineRepository.InsertOrUpdateSMSInformationFLW(obj);
            return Ok(msg);

        }
        #endregion Manage SMS Location Wise
    }
}
