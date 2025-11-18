using ITHSystems.DTOs;
using ITHSystems.Repositories.SQLite;
using ITHSystems.Services.General;
using ITHSystems.Views.Login;
using NSubstitute;

namespace ITHSystems.IntegrationTests.src.LoginTest;

public class Login
{
    [Fact]
    public async Task LoginIntegrationTest()
    {
        var userDto = new UserDto
        {
            UserName = "testuser",
            Password = "Test@1234",
            Email = "testUser@integrationtest.com.do",
            Name = "Elvis Jesus Hernandez Suarez"
        };

        var sqliteManager = Substitute.For<ISQLiteManager>();
        var preferenceService = Substitute.For<IPreferenceService>();
        var iThNavigation = Substitute.For<IITHNavigationService>();
        var user = Substitute.For<IRepository<Model.User>>();
        var viewModel = new LoginPageViewModel(sqliteManager, user, preferenceService, iThNavigation);

        Assert.NotNull(viewModel);
        Assert.IsType<LoginPageViewModel>(viewModel);

        await viewModel.RegisterUser(userDto);
        await viewModel.Login(userDto);

    }
}
