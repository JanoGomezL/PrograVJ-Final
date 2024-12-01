using UnityEngine;

public class ActionNode : BaseNode
{
    private readonly System.Func<bool> action; // Función que retorna un bool

    public ActionNode(System.Func<bool> action)
    {
        this.action = action;
    }

    public override bool Execute()
    {
        if (action == null)
        {
            Debug.LogError("[ActionNode] Acción no asignada.");
            return false;
        }

        return action(); // Ejecuta la acción y retorna su resultado
    }
}

