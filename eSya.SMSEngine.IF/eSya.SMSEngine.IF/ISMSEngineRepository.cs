﻿using eSya.SMSEngine.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eSya.SMSEngine.IF
{
    public interface ISMSEngineRepository
    {
        #region SMS Variable need to remove shifted to product setup

        Task<List<DO_SMSVariable>> GetSMSVariableInformation();
        Task<DO_ReturnParameter> InsertIntoSMSVariable(DO_SMSVariable obj);

        Task<DO_ReturnParameter> UpdateSMSVariable(DO_SMSVariable obj);

        Task<DO_ReturnParameter> ActiveOrDeActiveSMSVariable(bool status, string smsvariable);

        #endregion SMS Variable

        #region SMS Template
        Task<List<DO_SMSVariable>> GetActiveSMSVariableInformation();
        Task<List<DO_Forms>> GetExistingFormsFromSMSHeader();
        Task<int> GetMaxSequenceNumberbyTriggerEventID(int TeventID);
        Task<List<DO_SMSTEvent>> GetSMSTriggerEvent();

        Task<List<DO_SMSHeader>> GetSMSHeaderInformationByFormId(int formId);

        Task<DO_SMSHeader> GetSMSHeaderInformationBySMSId(string smsId);

        Task<DO_ReturnParameter> InsertIntoSMSHeader(DO_SMSHeader obj);

        Task<DO_ReturnParameter> UpdateSMSHeader(DO_SMSHeader obj);


        #endregion SMS Information

        #region SMS Recipient

        Task<List<DO_SMSHeader>> GetSMSHeaderForRecipientByFormIdandParamId(int formId,int parameterId);

        Task<List<DO_SMSRecipient>> GetSMSRecipientByBusinessKeyAndSMSId(int businessKey, string smsId);

        Task<DO_ReturnParameter> InsertIntoSMSRecipient(DO_SMSRecipient obj);

        Task<DO_ReturnParameter> UpdateSMSRecipient(DO_SMSRecipient obj);

        #endregion SMS Recipient

        //#region Trigger Event shifted to product setup API
        //Task<List<DO_SMSTEvent>> GetAllSMSTriggerEvents();

        //Task<DO_ReturnParameter> InsertIntoSMSTriggerEvent(DO_SMSTEvent obj);

        //Task<DO_ReturnParameter> UpdateSMSTriggerEvent(DO_SMSTEvent obj);

        //Task<DO_ReturnParameter> DeleteSMSTriggerEvent(int TeventId);

        //Task<DO_ReturnParameter> ActiveOrDeActiveSMSTriggerEvent(bool status, int TriggerEventId);
        //#endregion SMS Trigger Event

        #region Manage SMS Location Wise
        Task<List<DO_SMSHeader>> GetSMSInformationFormLocationWise(int businessKey, int formId);
        Task<DO_ReturnParameter> InsertOrUpdateSMSInformationFLW(List<DO_BusinessFormSMSLink> obj);
        #endregion Manage SMS Location Wise
    }
}
