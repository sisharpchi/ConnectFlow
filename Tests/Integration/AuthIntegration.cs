using Application.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Server;
using System.Net;
using System.Net.Http.Json;

namespace Tests.Integration;

public class AuthIntegration
{
    private readonly HttpClient _client;

    public AuthIntegration(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task SignUp_ReturnsOk()
    {
        var user = new UserCreateDto
        {
            FirstName = "Test",
            LastName = "User",
            UserName = "testuser_" + Guid.NewGuid(),
            Email = $"test_{Guid.NewGuid()}@example.com",
            Password = "Test@1234",
            PhoneNumber = "1234567890"
        };

        var response = await _client.PostAsJsonAsync("/api/auth/sighUp", user);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task SignIn_ReturnsOk()
    {
        // First, sign up a user
        var user = new UserCreateDto
        {
            FirstName = "Test",
            LastName = "User",
            UserName = "signinuser_" + Guid.NewGuid(),
            Email = $"signin_{Guid.NewGuid()}@example.com",
            Password = "Test@1234",
            PhoneNumber = "1234567890"
        };
        await _client.PostAsJsonAsync("/api/auth/sighUp", user);

        var login = new UserLogInDto
        {
            UserName = user.UserName,
            Password = user.Password
        };

        var response = await _client.PostAsJsonAsync("/api/auth/signIn", login);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var loginResponse = await response.Content.ReadFromJsonAsync<LogInResponseDto>();
        Assert.NotNull(loginResponse);
        Assert.False(string.IsNullOrEmpty(loginResponse.AccessToken));
        Assert.False(string.IsNullOrEmpty(loginResponse.RefreshToken));
    }

    [Fact]
    public async Task RefreshToken_ReturnsOk()
    {
        // First, sign up and sign in to get tokens
        var user = new UserCreateDto
        {
            FirstName = "Test",
            LastName = "User",
            UserName = "refreshuser_" + Guid.NewGuid(),
            Email = $"refresh_{Guid.NewGuid()}@example.com",
            Password = "Test@1234",
            PhoneNumber = "1234567890"
        };
        await _client.PostAsJsonAsync("/api/auth/sighUp", user);

        var login = new UserLogInDto
        {
            UserName = user.UserName,
            Password = user.Password
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/auth/signIn", login);
        var tokens = await loginResponse.Content.ReadFromJsonAsync<LogInResponseDto>();

        var refreshRequest = new RefreshRequestDto
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken
        };

        var response = await _client.PostAsJsonAsync("/api/auth/refreshToken", refreshRequest);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var refreshResponse = await response.Content.ReadFromJsonAsync<LogInResponseDto>();
        Assert.NotNull(refreshResponse);
        Assert.False(string.IsNullOrEmpty(refreshResponse.AccessToken));
        Assert.False(string.IsNullOrEmpty(refreshResponse.RefreshToken));
    }

    [Fact]
    public async Task LogOut_ReturnsOk()
    {
        // First, sign up and sign in to get token
        var user = new UserCreateDto
        {
            FirstName = "Test",
            LastName = "User",
            UserName = "logoutuser_" + Guid.NewGuid(),
            Email = $"logout_{Guid.NewGuid()}@example.com",
            Password = "Test@1234",
            PhoneNumber = "1234567890"
        };
        await _client.PostAsJsonAsync("/api/auth/sighUp", user);

        var login = new UserLogInDto
        {
            UserName = user.UserName,
            Password = user.Password
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/auth/signIn", login);
        var tokens = await loginResponse.Content.ReadFromJsonAsync<LogInResponseDto>();

        var response = await _client.DeleteAsync($"/api/auth/LogOut?token={tokens.AccessToken}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetUser_WithoutToken_ReturnsForbidden()
    {
        var response = await _client.GetAsync("/api/auth/getUser");
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task GetUser_WithToken_ReturnsOk()
    {
        // First, sign up and sign in to get token
        var user = new UserCreateDto
        {
            FirstName = "Test",
            LastName = "User",
            UserName = "getuser_" + Guid.NewGuid(),
            Email = $"getuser_{Guid.NewGuid()}@example.com",
            Password = "Test@1234",
            PhoneNumber = "1234567890"
        };
        await _client.PostAsJsonAsync("/api/auth/sighUp", user);

        var login = new UserLogInDto
        {
            UserName = user.UserName,
            Password = user.Password
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/auth/signIn", login);
        var tokens = await loginResponse.Content.ReadFromJsonAsync<LogInResponseDto>();

        // Set the Authorization header
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokens.AccessToken);

        var response = await _client.GetAsync("/api/auth/getUser");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var userResponse = await response.Content.ReadFromJsonAsync<UserGetDto>();
        Assert.NotNull(userResponse);
        Assert.Equal(user.UserName, userResponse.UserName);
    }
}
