using Orchard.ImportExport;
using Orchard.Localization;
using Orchard.UI.Navigation;

namespace Contrib.PasteImport
{
    public class AdminMenu : INavigationProvider
    {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder)
        {
            builder.Add(T("Import/Export"),
                menu => menu.Add(T("Paste"), "-1", 
                    item => item.Action("Paste", "Admin", new { area = "Contrib.PasteImport" }).Permission(Permissions.Import).LocalNav()));
        }
    }
}
