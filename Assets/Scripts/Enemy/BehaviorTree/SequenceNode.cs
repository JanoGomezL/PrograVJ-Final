using System.Collections.Generic;

public class SequenceNode : BaseNode
{
    private List<BaseNode> nodes;

    public SequenceNode(List<BaseNode> nodes)
    {
        this.nodes = nodes;
    }

    public override bool Execute()
    {
        foreach (var node in nodes)
        {
            if (!node.Execute())
            {
                return false; // Si un nodo falla, la secuencia falla
            }
        }
        return true; // Todos los nodos tienen éxito
    }
}
