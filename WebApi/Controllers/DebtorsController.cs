using AutoMapper.Configuration;
using Data.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class DebtorsController : ControllerBase {
        private readonly ILogger<AccountController> _logger;
        private readonly IDebtorsService _debtorsService;

        public DebtorsController(ILogger<AccountController> logger, IDebtorsService debtorsService) {
            _logger = logger;
            _debtorsService = debtorsService;
        }


        [HttpGet("list")]
        public async Task<ActionResult<IList<Debtor>>> List() {
            var list = await _debtorsService.ListAll();
            return Ok(list);
        }
    }


}
