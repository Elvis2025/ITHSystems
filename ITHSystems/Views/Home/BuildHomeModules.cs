using ITHSystems.Model;
using ITHSystems.UsesCases.IconFonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITHSystems.Views.Home
{
    public static class BuildHomeModules
    {
        public static List<Module> GetHomeModules()
        {
            return new List<Module>
            {
                new Module
                {
                    Title = "Servicio de recogida",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.Route
                },
                new Module
                {
                    Title = "Servicio de recogida",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.PeopleCarryBox
                }
            };
        }

        public static List<Module> GetDeliveriesModules()
        {
            return new List<Module>
            {
                new Module
                {
                    Title = "Envíos pendientes",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.Route
                },
                new Module
                {
                   Title = "Envíos postergados",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.Route
                },
                new Module
                {
                   Title = "Envíos entregados no sincronizados",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.Route
                },
                new Module
                {
                   Title = "Sincronizar",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.Route
                }

            };
        }

        public static List<Module> GetPickupModules()
        {
            return new List<Module>
            {
                new Module
                {
                   Title = "Valijas pendientes",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.Route
                },
                new Module
                {
                    Title = "Recibir valija",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.Route
                },
                new Module
                {
                    Title = "Entregar Valija",
                    IsActive = true,
                    IsDeleted = false,
                    FontFamaly = FontFamilyIcons.FaSolid,
                    Icon = IconFont.Route
                }
            };
        }



    }
}
