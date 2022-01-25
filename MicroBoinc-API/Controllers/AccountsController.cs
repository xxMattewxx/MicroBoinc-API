﻿using AutoMapper;
using MicroBoincAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using MicroBoincAPI.Data.Accounts;
using MicroBoincAPI.Dtos.Accounts;
using MicroBoincAPI.Models.Accounts;
using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountsRepo _repository;

        public AccountsController(IMapper mapper, IAccountsRepo repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpPost]
        public ActionResult<CreateAccountResponseDto> CreateAccount([Required, FromBody] CreateAccountDto dto)
        {
            if (_repository.IsDiscordIDLinked(dto.DiscordID.Value))
                return Conflict(new { Message = "An account already exists with this Discord ID." });

            var account = _mapper.Map<Account>(dto);
            account.Offset = 0;

            var key = new AccountKey
            {
                DisplayName = "Main",
                Identifier = Common.GenerateToken(32),
                Account = account,
                IsWeak = false
            };

            _repository.CreateAccountKey(key);
            _repository.SaveChanges();

            return _mapper.Map<CreateAccountResponseDto>(key);
        }
    }
}