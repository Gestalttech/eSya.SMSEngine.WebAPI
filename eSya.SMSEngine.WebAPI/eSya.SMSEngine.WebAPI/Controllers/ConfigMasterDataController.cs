﻿using eSya.SMSEngine.DL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.SMSEngine.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigMasterDataController : ControllerBase
    { /// <summary>
      /// Get Business key.
      /// </summary>
      /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetBusinessKey()
        {
            var ds = await new CommonMethod().GetBusinessKey();
            return Ok(ds);
        }

        /// <summary>
        /// Get Form Detail.
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFormDetails()
        {
            var ds = await new CommonMethod().GetFormDetails();
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetFormDetailsbyBusinessKey(int businesskey)
        {
            var ds = await new CommonMethod().GetFormDetailsbyBusinessKey(businesskey);
            return Ok(ds);
        }
        /// <summary>
        /// Get Business key with SMS Integration.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetBusinessKeyBySMSIntegration()
        {
            var ds = await new CommonMethod().GetBusinessKeyBySMSIntegration();
            return Ok(ds);
        }

        /// <summary>
        /// Get Form Detail.
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFormForSMSlinking()
        {
            var ds = await new CommonMethod().GetFormForSMSlinking();
            return Ok(ds);
        }
    }
}
