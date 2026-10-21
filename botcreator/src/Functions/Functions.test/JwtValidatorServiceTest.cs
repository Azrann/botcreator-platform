using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Functions.Services;

namespace Functions.test
{
    public class JwtValidatorServiceTest
    {
        [Fact]
        public void ICanValidateAToken()
        {
            JwtTokenService service = new JwtTokenService();
            var result = service.Validate(
                "REPLACE_WITH_YOUR_VALUE");
            Assert.True(result);
        }

        [Fact]
        public void ICanReadAToken()
        {
            JwtTokenService service = new JwtTokenService();
            var result = service.Read(
                "REPLACE_WITH_YOUR_VALUE");
            Assert.True(result.UserId == "4");
            Assert.True(result.Email == "user@example.invalid");
        }

        [Fact]
        public void ICanValidateATokenWithBearer1()
        {
            JwtTokenService service = new JwtTokenService();
            var result = service.Validate(
                "bearer REPLACE_WITH_YOUR_VALUE");
            Assert.True(result);
        }


        [Fact]
        public void ICanValidateATokenWithBearer2()
        {
            JwtTokenService service = new JwtTokenService();
            var result = service.Validate(
                "Bearer REPLACE_WITH_YOUR_VALUE");
            Assert.True(result);
        }

        [Fact]
        public void ICanReadATokenWithBearer1()
        {
            JwtTokenService service = new JwtTokenService();
            var result = service.Read(
                "bearer REPLACE_WITH_YOUR_VALUE");
            Assert.True(result.UserId == "4");
            Assert.True(result.Email == "user@example.invalid");
        }

        [Fact]
        public void ICanReadATokenWithBearer2()
        {
            JwtTokenService service = new JwtTokenService();
            var result = service.Read(
                "Bearer REPLACE_WITH_YOUR_VALUE");
            Assert.True(result.UserId == "4");
            Assert.True(result.Email == "user@example.invalid");
        }
    }
}
