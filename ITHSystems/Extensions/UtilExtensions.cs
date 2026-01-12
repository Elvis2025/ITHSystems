using AutoMapper;
using ITHSystems.Attributes;
using ITHSystems.DTOs;
using ITHSystems.Enums;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;

namespace ITHSystems.Extensions;

public static class UtilExtensions
{
    private static IMapper mapper => CreateInstance<IMapper>();
    public static T CreateInstance<T>() where T : class
    {
        return ActivatorUtilities.GetServiceOrCreateInstance<T>(Application.Current!.Handler.MauiContext!.Services!);
    }

    public static T Map<T>(this object source) where T : class
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        return mapper.Map<T>(source);
    }

    public static void RegisterAsRoutes()
    {
        var typesWithAttr = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a =>
            {
                try { return a.GetTypes(); }
                catch { return Array.Empty<Type>(); }
            })
            .Where(t => t.IsClass && !t.IsAbstract &&
                        t.GetCustomAttribute<RegisterAsRouteAttribute>() is not null &&
                        typeof(Page).IsAssignableFrom(t));

        foreach (var type in typesWithAttr)
        {
            var attr = type.GetCustomAttribute<RegisterAsRouteAttribute>()!;
            var route = attr.RouteName ?? type.Name;
            Routing.RegisterRoute(route, type);
        }
    }

    public static async Task<string> ToBase64(this ImageSource imageSource)
    {
        if (imageSource == null)
            return string.Empty;

        Stream imageStream = await GetImageStreamAsync(imageSource);
        if (imageStream == null)
            return string.Empty;

        using var memoryStream = new MemoryStream();
        await imageStream.CopyToAsync(memoryStream);

        var bytes = memoryStream.ToArray();
        return Convert.ToBase64String(bytes);
    }
    public static async Task<string> EncodeImageAsync(this ImageSource imageSource)
    {
        try
        {
            Stream? stream = null;

            stream = await GetImageStreamAsync(imageSource);

            using var inputStream = stream;
            using var output = new MemoryStream();

            byte[] buffer = new byte[8192];
            int bytesRead;

            while ((bytesRead = await inputStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await output.WriteAsync(buffer, 0, bytesRead);
            }

            var bytes = output.ToArray();
            string encoded = Convert.ToBase64String(bytes);

            return encoded;
        }
        catch (Exception e)
        {
            Debug.Write(e.Message);
            return string.Empty;
        }
    }
    private static async Task<Stream> GetImageStreamAsync(ImageSource imageSource)
    {
        switch (imageSource)
        {
            case FileImageSource fileImage:
                return File.OpenRead(fileImage.File);

            case StreamImageSource streamImage:
                return await streamImage.Stream(CancellationToken.None);

            case UriImageSource uriImage:
                var httpClient = new HttpClient();
                return await httpClient.GetStreamAsync(uriImage.Uri);

            default:
                return null;
        }
    }

    public static IEnumerable<PersonDto> GetPersons()
    {
        var persons = new List<PersonDto>
        {
            new PersonDto {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "",
                CardType = "Visa Clásica",
                Address = "Av siempre Viva 58,Altos de Arroyo Hondo I, Distrito Nacional, Distrito Nacional, Dominican Republic",
                Module = Modules.PENDINGDELIVERIES,
            },
            new PersonDto {
                Id = 2,
                FirstName = "Jesus",
                LastName = "Peña",
                Email = "",
                CardType = "Visa Clásica",
                Address = "Av siempre Viva 58,Altos de Arroyo Hondo I, Distrito Nacional, Distrito Nacional, Dominican Republic",
                Module = Modules.PENDINGDELIVERIES,
            },
            new PersonDto {
                Id = 3,
                FirstName = "Matusalem",
                LastName = "Sepeda",
                Email = "",
                CardType = "Visa Clásica",
                Address = "Av siempre Viva 58,Altos de Arroyo Hondo I, Distrito Nacional, Distrito Nacional, Dominican Republic",
                Module = Modules.DELAYEDDELIVERIES,
            },
            new PersonDto {
                Id = 4,
                FirstName = "Efrain",
                LastName = "Beriguete",
                Email = "",
                CardType = "Visa Clásica",
                Address = "Av siempre Viva 58,Altos de Arroyo Hondo I, Distrito Nacional, Distrito Nacional, Dominican Republic",
                Module = Modules.DELIVERESSHIPMENTSNOTSYNCED,
            },

        };

        return persons;
    }

    public static ObservableCollection<DeliveryOptionDto> GetDeliveryOptions()
    {
        var options = new List<DeliveryOptionDto>
        {
            new() { Id = Receiver.Beneficiary, Name = "Beneficiario" },
            new() { Id = Receiver.SecondPerson, Name = "Segunda Persona" },
        };
        return new(options);
    }


    public static ObservableCollection<CausesOfNonDeliveryDto> GetCausesOfNonDelivery()
    {
        var options = new List<CausesOfNonDeliveryDto>
        {
            new() { Id = 1, Name = "Dirección Incorrecta" },
            new() { Id = 2, Name = "No se encuentra el cliente" },
            new() { Id = 3, Name = "Rechazo de plástico" },
            new() { Id = 4, Name = "Cliente falleció" },
            new() { Id = 5, Name = "Arreglar nombre" },
            new() { Id = 6, Name = "Cliente Incorrecto" },
            new() { Id = 7, Name = "Cliente no Laborando" },
            new() { Id = 8, Name = "Monto incorrecto" },
        };

        return new(options);
    }
    public static ObservableCollection<GenderDto> GetGenders()
    {
        var options = new List<GenderDto>
        {
            new() { Id = Gender.Male, Description = "Masculino" },
            new() { Id = Gender.Famale, Description = "Femenino" },
            new() { Id = Gender.Undefined, Description = "No definido" },
        };

        return new(options);
    }
    public static async Task<(double Latitude, double Longitude)> GetCurrentLocationAsync()
    {
        var request = new GeolocationRequest(
            GeolocationAccuracy.Best,
            TimeSpan.FromSeconds(10));

        var location = await Geolocation.GetLocationAsync(request);

        if (location == null) return (0.00d, 0.00d);

        return (location.Latitude, location.Longitude);
    }
    public static ObservableCollection<CountryDto> GetCountries()
    {
        var options = new List<CountryDto>
        {
            new() { Id = 1, Name = "República Dominicana" },
            new() { Id = 2, Name = "Australia" },
            new() { Id = 3, Name = "Tailandia" },
            new() { Id = 4, Name = "Colombia" },
            new() { Id = 5, Name = "Perú" },
        };

        return new(options);
    }

}
