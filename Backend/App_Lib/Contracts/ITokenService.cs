﻿using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Backend.AppLib.Contracts;

public interface ITokenService
{
    string CreateToken(string UserName);
    string CreateToken(ClaimsIdentity UserClaims);
    string CreateTokenEncrypted(ClaimsIdentity UserClaims);
    bool ValidateToken(string Token, out string Message, out SecurityToken? JWT);
    bool ValidateTokenEncrypted(string Token, out string Message, out SecurityToken? JWT);
}