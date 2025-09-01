using ITHSystems.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ITHSystems.Controls;

public partial class ModuleListCard : ContentView
{
	public ModuleListCard()
	{
		InitializeComponent();
	}
    public static readonly BindableProperty ModulesListProperty = BindableProperty.Create(
            nameof(ModulesList), 
            typeof(ObservableCollection<ModuleDTO>), 
            typeof(ModuleListCard),
            new ObservableCollection<ModuleDTO>());

    public ObservableCollection<ModuleDTO> ModulesList
    {
        get => (ObservableCollection<ModuleDTO>)GetValue(ModulesListProperty);
        set => SetValue(ModulesListProperty, value);
    }

    public static readonly BindableProperty ModuleSelectedProperty =
      BindableProperty.Create(nameof(ModuleSelected), typeof(ModuleDTO), typeof(ModuleListCard), null);

    public ModuleDTO ModuleSelected
    {
        get => (ModuleDTO)GetValue(ModuleSelectedProperty);
        set => SetValue(ModuleSelectedProperty, value);
    }
    public static readonly BindableProperty GoToModuleCommandProperty =
      BindableProperty.Create(
          nameof(GoToModuleCommand),
          typeof(ICommand),
          typeof(ModuleListCard),
          default(ICommand));
    public ICommand GoToModuleCommand
    {
        get => (ICommand)GetValue(GoToModuleCommandProperty);
        set => SetValue(GoToModuleCommandProperty, value);
    }




    public static readonly BindableProperty GoToModuleCommandParameterProperty =
        BindableProperty.Create(
            nameof(GoToModuleCommandParameter),
            typeof(ModuleDTO),
            typeof(ModuleListCard),
            default(ModuleDTO));

    public ModuleDTO GoToModuleCommandParameter
    {
        get =>   (ModuleDTO)GetValue(GoToModuleCommandParameterProperty);
        set => SetValue(GoToModuleCommandParameterProperty, value);
    }
    
}