﻿using Microsoft.EntityFrameworkCore;
using eSya.SMSEngine.DL.Entities;
using eSya.SMSEngine.DO;
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
    }
}
