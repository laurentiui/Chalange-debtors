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
        private readonly ILogger<DebtorsController> _logger;
        private readonly IDebtorsService _debtorsService;

        public DebtorsController(ILogger<DebtorsController> logger, IDebtorsService debtorsService) {
            _logger = logger;
            _debtorsService = debtorsService;
        }


        [HttpGet("list")]
        public async Task<ActionResult<IList<Debtor>>> List() {
            //await _debtorsService.Insert(new Debtor() {
            //    Id = 2,
            //    Name = "ceva"
            //});
            var list = await _debtorsService.ListAll();
            return Ok(list);
        }
    }


}
