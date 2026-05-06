using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using ProjectManagement.Features.Authentication;
using Xunit;

namespace ProjectManagement.Tests.Features.Authentication
{
    public class AuthenticationServiceTests
    {
        private readonly AuthenticationService _service;
        private readonly AppSettings _appSettings;

        public AuthenticationServiceTests()
        {
            _appSettings = new AppSettings
            {
                Key = "ThisIsASecretKeyForJWTTokenGenerationWithAtLeast32Characters",
                Issuer = "ProjectManagementAPI"
            };

            var options = Options.Create(_appSettings);
            _service = new AuthenticationService(options);
        }

        [Fact]
        public void Authenticate_WithValidCredentials_ReturnsTokenResponse()
        {
            // Arrange
            var request = new AuthenticateRequest
            {
                UserName = "mytestuser",
                Password = "test123"
            };

            // Act
            var result = _service.Authenticate(request);

            // Assert
            result.Should().NotBeNull();
            result.Token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Authenticate_WithInvalidUsername_ReturnsNull()
        {
            // Arrange
            var request = new AuthenticateRequest
            {
                UserName = "invaliduser",
                Password = "test123"
            };

            // Act
            var result = _service.Authenticate(request);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void Authenticate_WithInvalidPassword_ReturnsNull()
        {
            // Arrange
            var request = new AuthenticateRequest
            {
                UserName = "mytestuser",
                Password = "wrongpassword"
            };

            // Act
            var result = _service.Authenticate(request);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void Authenticate_WithEmptyCredentials_ReturnsNull()
        {
            // Arrange
            var request = new AuthenticateRequest
            {
                UserName = "",
                Password = ""
            };

            // Act
            var result = _service.Authenticate(request);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void Authenticate_GeneratesValidJwtToken()
        {
            // Arrange
            var request = new AuthenticateRequest
            {
                UserName = "mytestuser",
                Password = "test123"
            };

            // Act
            var result = _service.Authenticate(request);

            // Assert
            result.Should().NotBeNull();
            result.Token.Should().NotBeNullOrEmpty();

            // JWT tokens have 3 parts separated by dots
            var tokenParts = result.Token.Split('.');
            tokenParts.Should().HaveCount(3);
        }
    }
}
