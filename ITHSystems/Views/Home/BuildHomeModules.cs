using ITHSystems.DTOs;
using ITHSystems.UsesCases.IconFonts;

namespace ITHSystems.Views.Home
{
    public static class BuildHomeModules
    {
        public static List<ModuleDTO> GetHomeModules()
        {
            return new List<ModuleDTO>
            {
                new ModuleDTO
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
                new ModuleDTO
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

        public static List<ModuleDTO> GetDeliveriesModules()
        {
            return new List<ModuleDTO>
            {
                new ModuleDTO
                {
                    Title = "Envíos pendientes",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.BoxesStacked,
                    Modules = Enums.Modules.PENDINGDELIVERIES,
                    Order = 1,
                    EnableBadges = true,
                    Badges = 2
                },
                new ModuleDTO
                {
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
                new ModuleDTO
                {
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

        public static List<ModuleDTO> GetPickupModules()
        {
            return new List<ModuleDTO>
            {
                new ModuleDTO
                {
                   Title = "Valijas pendientes",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.Box,
                    Modules = Enums.Modules.PENDINGSUITCASE,
                    Order = 1
                },
                new ModuleDTO
                {
                    Title = "Recibir valija",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.BoxesPacking,
                    Modules = Enums.Modules.RECEIVESUITCASE,
                    Order = 2
                },
                new ModuleDTO
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
