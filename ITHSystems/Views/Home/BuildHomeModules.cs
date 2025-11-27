using ITHSystems.DTOs;
using ITHSystems.Enums;
using ITHSystems.Extensions;
using ITHSystems.UsesCases.IconFonts;

namespace ITHSystems.Views.Home
{
    public static class BuildHomeModules
    {
        public static List<ModuleDto> GetHomeModules()
        {
            return new List<ModuleDto>
            {
                new ModuleDto
                {
                    Title = "Servicio de recogida",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.Route,
                    Modules = Enums.Modules.PICKUPSERVICE,
                    Order = 2,
                    EnableBadges = false
                },
                new ModuleDto
                {
                    Title = "Entregas",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.PeopleCarryBox,
                    Modules = Enums.Modules.DELIVERIES,
                    Order = 1,
                    EnableBadges = false


                }
            };
        }

        public static List<ModuleDto> GetDeliveriesModules()
        {
            return new List<ModuleDto>
            {
                new ModuleDto
                {
                    Id = 1,
                    Title = "Envíos pendientes",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.BoxesStacked,
                    Modules = Modules.PENDINGDELIVERIES,
                    Order = 1,
                    EnableBadges = true,
                    Badges = 0
                },
                new ModuleDto
                {
                   Id = 2,
                   Title = "Envíos postergados",
                   IsActive = true,
                   IsDeleted = false,
                   FontFamaly = FontFamilyIcons.FaSolid,
                   Icon = IconFont.BoxesPacking,
                   Modules = Enums.Modules.DELAYEDDELIVERIES,
                   Order = 2,
                   EnableBadges = true,
                   Badges = 0
                },
                new ModuleDto
                {
                   Id = 3,
                   Title = "Envíos entregados no sincronizados",
                   IsActive = true,
                   IsDeleted = false,
                   FontFamaly = FontFamilyIcons.FaSolid,
                   Icon = IconFont.TruckRampBox,
                   Modules = Enums.Modules.DELIVERESSHIPMENTSNOTSYNCED,
                   Order = 3,
                   EnableBadges = true,
                   Badges = 0
                }
            };
        }

        public static List<ModuleDto> GetPickupModules()
        {
            return new List<ModuleDto>
            {
                new ModuleDto
                {
                   Title = "Valijas pendientes",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.Box,
                    Modules = Enums.Modules.PENDINGSUITCASE,
                    Order = 1
                },
                new ModuleDto
                {
                    Title = "Recibir valija",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.BoxesPacking,
                    Modules = Enums.Modules.RECEIVESUITCASE,
                    Order = 2
                },
                new ModuleDto
                {
                    Title = "Entregar Valija",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.TruckRampBox,
                    Modules = Enums.Modules.DELIVERSUITCASE,
                    Order = 3
                }
            };
        }



    }
}
