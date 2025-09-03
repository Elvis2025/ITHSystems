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
            typeof(ObservableCollection<ModuleDto>), 
            typeof(ModuleListCard),
            new ObservableCollection<ModuleDto>());

    public ObservableCollection<ModuleDto> ModulesList
    {
        get => (ObservableCollection<ModuleDto>)GetValue(ModulesListProperty);
        set => SetValue(ModulesListProperty, value);
    }

    public static readonly BindableProperty ModuleSelectedProperty =
      BindableProperty.Create(nameof(ModuleSelected), typeof(ModuleDto), typeof(ModuleListCard), null);

    public ModuleDto ModuleSelected
    {
        get => (ModuleDto)GetValue(ModuleSelectedProperty);
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
            typeof(ModuleDto),
            typeof(ModuleListCard),
            default(ModuleDto));

    public ModuleDto GoToModuleCommandParameter
    {
        get =>   (ModuleDto)GetValue(GoToModuleCommandParameterProperty);
        set => SetValue(GoToModuleCommandParameterProperty, value);
    }
    
}