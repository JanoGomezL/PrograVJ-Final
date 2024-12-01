using System.Collections.Generic;

public class SelectorNode : BaseNode
{
    private List<BaseNode> nodes;

    public SelectorNode(List<BaseNode> nodes)
    {
        this.nodes = nodes;
    }

    public override bool Execute()
    {
        foreach (var node in nodes)
        {
            if (node.Execute())
            {
                return true; // Si un nodo tiene éxito, el selector tiene éxito
            }
        }
        return false; // Todos los nodos fallaron
    }
}
