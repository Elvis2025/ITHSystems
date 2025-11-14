

using ITHSystems.Extensions;
using ITHSystems.Views.Login;
using System.Threading.Tasks;

namespace ITHSystems.IntegrationTests.src.LoginTests;
public class LoginViewModelTest
{
    [Fact]
    public async Task eset()
    {
        var service = UtilExtensions.CreateInstance<LoginPageViewModel>();

        Assert.NotNull(service);
        Assert.IsType<LoginPageViewModel>(service);

        await service.Init();
        await service.Login();

    }



}
