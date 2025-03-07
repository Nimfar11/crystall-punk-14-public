using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.CustomControls;
using Robust.Client.UserInterface.XAML;

namespace Content.Client._CP14.UserInterface.Systems.Knowledge.Windows;

[GenerateTypedNameReferences]
public sealed partial class CP14KnowledgeWindow : DefaultWindow
{
    public CP14KnowledgeWindow()
    {
        RobustXamlLoader.Load(this);
    }
}
